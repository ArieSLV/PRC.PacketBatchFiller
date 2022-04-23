using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.Data;
using Catel.IoC;
using Catel.Services;
using PRC.PacketBatchFiller.Models.BaseClasses;
using PRC.PacketBatchFiller.Models.BaseClasses.Documents;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.Models.Documents.ShareholderDocuments;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity;
using PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity.ShareholderAuthorizesDocumentEntity;
using PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity.ShareholderQuestionaryEntity;
using PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity.ShareholderTransferOrderEntity;
using PRC.PacketBatchFiller.Views.Documents.ShareholderDocument;


namespace PRC.PacketBatchFiller.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IUIVisualizerService _uiVisualizerService;
        private readonly IMessageService _messageService;

        public DocumentService(IUIVisualizerService uiVisualizerService, IMessageService messageService)
        {
            _uiVisualizerService = uiVisualizerService;
            _messageService = messageService;
        }

        #region Document Packages

        public async Task<DocumentPackage> OpenDocumentPackageWindow(DocumentPackage documentPackage)
        {
            DocumentPackage documentPackageToReturn = null;

            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
            {
                if (documentPackage is ShareholderDocumentPackage)
                {
                    var shdp = dbContextManager.Context.ShareholderDocumentPackages.Find(documentPackage.DocumentPackageId);

                    if (shdp != null)
                    {
                        dbContextManager.Context.Entry(shdp).Reference(o => o.MainAccount).Load();

                        if (shdp.MainAccount != null)
                        {
                            var ma = dbContextManager.Context.ShareholderAccounts.Find(shdp.MainAccount.ShareholderAccountId);
                            dbContextManager.Context.Entry(ma).Reference(o => o.Unit).Load();


                            //if (ma.Unit != null)
                            //{
                            //    var u = dbContextManager.Context.Units.Find(ma.Unit.UnitId);
                            //    dbContextManager.Context.Entry(u).Collection(o => o.AuthorizedDocumentsCollection).Load();
                            //}
                        }


                        dbContextManager.Context.Entry(shdp).Collection(o => o.DocumentsCollection).Load();
                        foreach (var document in shdp.DocumentsCollection)
                        {
                            if (document is ShareholderAuthorizesDocument)
                            {
                                var shad = dbContextManager.Context.ShareholderAuthorizesDocuments.Find(document.DocumentId);
                                dbContextManager.Context.Entry(shad).Collection(o => o.AuthorizedUnits).Load();
                            }
                        }
                    }
                    else { shdp = (ShareholderDocumentPackage)documentPackage; }

                    var shdpWindowModel = RegisterViewModel(shdp);

                    if (await _uiVisualizerService.ShowDialogAsync(shdpWindowModel) ?? false)
                    {
                        SaveDocumentPackageToContext(shdpWindowModel.ShareholderDocumentPackageModel);
                    }

                    documentPackageToReturn = shdpWindowModel.ShareholderDocumentPackageModel;
                }
                return documentPackageToReturn;
            }

        }

        public long SaveDocumentPackageToContext(ShareholderDocumentPackage shareholderDocumentPackage)
        {
            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
            {
                #region Check Flags
                #endregion

                var isNew = dbContextManager.Context.ShareholderDocumentPackages.Find(shareholderDocumentPackage.DocumentPackageId) == null;

                shareholderDocumentPackage.TimeStamp = DateTime.Now;

                if (isNew) dbContextManager.Context.ShareholderDocumentPackages.Add(shareholderDocumentPackage);

                dbContextManager.Context.SaveChanges();

                return shareholderDocumentPackage.DocumentPackageId;
            }

        }

        public void RemoveDocumentPackageFromDataContext(DocumentPackage documentPackage)
        {
            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
            {
                DocumentPackage documentPackageToRemove = null;

                if (documentPackage is ShareholderDocumentPackage)
                {
                    documentPackageToRemove = dbContextManager.Context.ShareholderDocumentPackages
                        .Include(o => o.DocumentsCollection)
                        .FirstOrDefault(o => o.DocumentPackageId == documentPackage.DocumentPackageId);
                }

                if (documentPackageToRemove == null) return;

                if (documentPackageToRemove.DocumentsCollection != null)
                    foreach (var document in documentPackageToRemove.DocumentsCollection.ToList())
                    {
                        var shareholderQuestionary = dbContextManager.Context.ShareholderQuestionaries.Find(document.DocumentId);
                        if (shareholderQuestionary != null)
                        {
                            var shq = dbContextManager.Context.ShareholderQuestionaries.Include(o => o.MailingAddress).FirstOrDefault(o => o.DocumentId == shareholderQuestionary.DocumentId);
                            dbContextManager.Context.ShareholderQuestionaries.Remove(shq);
                            continue;
                        }
                    }

                dbContextManager.Context.DocumentPackages.Remove(documentPackageToRemove);

                #region dbContextManager.Context.SaveChanges();

                try
                {
                    dbContextManager.Context.SaveChanges();
                }
                catch (Exception c)
                {
                    var messageText = new StringBuilder();
                    messageText.AppendLine(c.Message);
                    messageText.Append(Environment.NewLine);

                    var innerException = c.InnerException;
                    while (innerException != null)
                    {
                        messageText.AppendLine("Внутреннее сообщение:");
                        messageText.AppendLine(innerException.Message);
                        messageText.Append(Environment.NewLine);
                        innerException = innerException.InnerException;
                    }
                    _messageService.ShowAsync(messageText.ToString(), "Ошибка", MessageButton.OK, MessageImage.Error);
                }
                
                #endregion
            }

        }

        #endregion

        #region Document
        
        public async Task<Document> OpenDocumentEditWindow(Document doc, ObservableCollection<Unit> authorizedUnitsCollection)
        {
            Document docToReturn = null;

            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
            {

                if (doc is ShareholderQuestionary)
                {
                    var shq = dbContextManager.Context.ShareholderQuestionaries.Find(doc.DocumentId);

                    if (shq != null)
                    {
                        dbContextManager.Context.Entry(shq).Reference(o => o.Signatory).Load();
                        dbContextManager.Context.Entry(shq).Reference(o => o.ShareholderAccount).Load();
                        if (!shq.AddressForNotificationAsMailingAddressFlag)
                            dbContextManager.Context.Entry(shq).Reference(o => o.MailingAddress).Load();
                        
                    }
                    else { shq = (ShareholderQuestionary) doc; }

                    var shqWindowModel = RegisterViewModel(shq);
                    shqWindowModel.AuthorizedUnitsCollection = authorizedUnitsCollection;

                    if (await _uiVisualizerService.ShowDialogAsync(shqWindowModel) ?? false)
                    {
                        SaveDocumentToContext(shqWindowModel.ShareholderQuestionaryModel);
                    }

                    docToReturn = shqWindowModel.ShareholderQuestionaryModel;
                }
                else if (doc is ShareholderAuthorizesDocument)
                {
                    var shad = dbContextManager.Context.ShareholderAuthorizesDocuments.Find(doc.DocumentId);

                    if (shad != null)
                    {
                        dbContextManager.Context.Entry(shad).Reference(o => o.WhoGivingAuthority).Load();
                        dbContextManager.Context.Entry(shad).Collection(o => o.AuthorizedUnits).Load();
                    }
                    else { shad = (ShareholderAuthorizesDocument) doc; }

                    var shadWindowModel = RegisterViewModel(shad);

                    if (await _uiVisualizerService.ShowDialogAsync(shadWindowModel) ?? false)
                    {
                        SaveDocumentToContext(shadWindowModel.ShareholderAuthorizesDocumentModel);
                    }

                    docToReturn = shadWindowModel.ShareholderAuthorizesDocumentModel;
                }
                else if (doc is ShareholderTransferOrder)
                {
                    var shto = dbContextManager.Context.ShareholderTransferOrders.Find(doc.DocumentId);

                    if (shto != null)
                    {
                        dbContextManager.Context.Entry(shto).Reference(o => o.CreditingAccount).Load();
                        dbContextManager.Context.Entry(shto).Reference(o => o.DebitingAccount).Load();
                        dbContextManager.Context.Entry(shto).Collection(o => o.TransferReasonDocuments).Load();
                        dbContextManager.Context.Entry(shto).Reference(o => o.IssueOfSecurities).Load();
                        dbContextManager.Context.Entry(shto).Reference(o => o.Signatory).Load();
                        dbContextManager.Context.Entry(shto).Reference(o => o.SignedByAuthorizesDocument).Load();
                    }
                    else { shto = (ShareholderTransferOrder) doc; }

                    var shtoViewModel = RegisterViewModel(shto);

                    if (await _uiVisualizerService.ShowDialogAsync(shtoViewModel) ?? false)
                    {
                        SaveDocumentToContext(shtoViewModel.ShareholderTransferOrderModel);
                    }

                    docToReturn = shtoViewModel.ShareholderTransferOrderModel;
                }
            }

            return docToReturn;
        }
        
        public long SaveDocumentToContext(ShareholderQuestionary shareholderQuestionary)
        {
            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
            {
                #region Check Flags

                if (shareholderQuestionary.SubmittingReason != QuestionnaireSubmittingReason.Open)
                {
                    shareholderQuestionary.Signatory = null;
                    shareholderQuestionary.NotificationRequiredFlag = false;
                    shareholderQuestionary.MailingAddress = null;
                }
                else
                {
                    if (shareholderQuestionary.AddressForNotificationAsMailingAddressFlag)
                    {
                        shareholderQuestionary.MailingAddress = shareholderQuestionary.ShareholderAccount.Unit?.MailingAddress;
                    }

                    if (!shareholderQuestionary.NotificationRequiredFlag)
                    {
                        shareholderQuestionary.MailingAddress = null;
                    }
                }

                #endregion

                var isNew = dbContextManager.Context.ShareholderQuestionaries.Find(shareholderQuestionary.DocumentId) == null;
                
                //if (shareholderQuestionary.ShareholderAccount != null) dbContextManager.Context.ShareholderAccounts.Attach(shareholderQuestionary.ShareholderAccount);
                ////if (shareholderQuestionary.MailingAddress != null && shareholderQuestionary.MailingAddress.AddressId !=0) dbContextManager.Context.Address.Attach(shareholderQuestionary.MailingAddress);
                //if (shareholderQuestionary.Signatory != null) dbContextManager.Context.Persons.Attach(shareholderQuestionary.Signatory);

                shareholderQuestionary.TimeStamp = DateTime.Now;

                if (isNew) dbContextManager.Context.ShareholderQuestionaries.Add(shareholderQuestionary);

                dbContextManager.Context.SaveChanges();

                return shareholderQuestionary.DocumentId;
            }
        }

        public long SaveDocumentToContext(ShareholderAuthorizesDocument shareholderAuthorizesDocument)
        {
            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
            {
                //#region Check Flags

                //if (shareholderAuthorizesDocument.SubmittingReason != QuestionnaireSubmittingReason.Open)
                //{
                //    shareholderAuthorizesDocument.Signatory = null;
                //    shareholderAuthorizesDocument.NotificationRequiredFlag = false;
                //    shareholderAuthorizesDocument.MailingAddress = null;
                //}
                //else
                //{
                //    if (shareholderAuthorizesDocument.AddressForNotificationAsMailingAddressFlag)
                //    {
                //        shareholderAuthorizesDocument.MailingAddress = shareholderQuestionary.ShareholderAccount.Unit?.MailingAddress;
                //    }

                //    if (!shareholderAuthorizesDocument.NotificationRequiredFlag)
                //    {
                //        shareholderAuthorizesDocument.MailingAddress = null;
                //    }
                //}

                //#endregion

                var isNew = dbContextManager.Context.ShareholderAuthorizesDocuments.Find(shareholderAuthorizesDocument.DocumentId) == null;

                //if (shareholderQuestionary.ShareholderAccount != null) dbContextManager.Context.ShareholderAccounts.Attach(shareholderQuestionary.ShareholderAccount);
                ////if (shareholderQuestionary.MailingAddress != null && shareholderQuestionary.MailingAddress.AddressId !=0) dbContextManager.Context.Address.Attach(shareholderQuestionary.MailingAddress);
                //if (shareholderQuestionary.Signatory != null) dbContextManager.Context.Persons.Attach(shareholderQuestionary.Signatory);

                shareholderAuthorizesDocument.TimeStamp = DateTime.Now;

                if (isNew) dbContextManager.Context.ShareholderAuthorizesDocuments.Add(shareholderAuthorizesDocument);

                dbContextManager.Context.SaveChanges();

                return shareholderAuthorizesDocument.DocumentId;
            }
        }
        
        public long SaveDocumentToContext(ShareholderTransferOrder shareholderTransferOrder)
        {
            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
            {
                //Check Flags

                var isNew = dbContextManager.Context.ShareholderTransferOrders.Find(shareholderTransferOrder.DocumentId) == null;

                shareholderTransferOrder.TimeStamp = DateTime.Now;

                if (isNew) dbContextManager.Context.ShareholderTransferOrders.Add(shareholderTransferOrder);

                dbContextManager.Context.SaveChanges();

                return shareholderTransferOrder.DocumentId;
            }
        }

        //Если понадобится сделать метод удаления документа из контекста - надо обратить внимание, 
        //что удаление документа уже частично реализовано в методе удаления ПакетаДокументов

        #endregion

        #region ViewModels Registering

        public ShareholderDocumentPackageWindowModel RegisterViewModel(ShareholderDocumentPackage obj)
        {
            var typeFactory = TypeFactory.Default;

            _uiVisualizerService.Unregister(typeof(ShareholderDocumentPackageWindowModel));
            _uiVisualizerService.Register(typeof(ShareholderDocumentPackageWindowModel), typeof(ShareholderDocumentPackageWindow));

            return typeFactory.CreateInstanceWithParametersAndAutoCompletion<ShareholderDocumentPackageWindowModel>(obj);
        }

        public ShareholderQuestionaryEditWindowModel RegisterViewModel(ShareholderQuestionary obj)
        {
            var typeFactory = TypeFactory.Default;

            _uiVisualizerService.Unregister(typeof(ShareholderQuestionaryEditWindowModel));
            _uiVisualizerService.Register(typeof(ShareholderQuestionaryEditWindowModel), typeof(ShareholderQuestionaryEditWindow));

            return typeFactory.CreateInstanceWithParametersAndAutoCompletion<ShareholderQuestionaryEditWindowModel>(obj);
        }

        public ShareholderAuthorizesDocumentEditWindowModel RegisterViewModel(ShareholderAuthorizesDocument obj)
        {
            var typeFactory = TypeFactory.Default;

            _uiVisualizerService.Unregister(typeof(ShareholderAuthorizesDocumentEditWindowModel));
            _uiVisualizerService.Register(typeof(ShareholderAuthorizesDocumentEditWindowModel), typeof(ShareholderAuthorizesDocumentEditWindow));

            return typeFactory.CreateInstanceWithParametersAndAutoCompletion<ShareholderAuthorizesDocumentEditWindowModel>(obj);
        }

        public ShareholderTransferOrderEditWindowModel RegisterViewModel(ShareholderTransferOrder obj)
        {
            var typeFactory = TypeFactory.Default;

            _uiVisualizerService.Unregister(typeof(ShareholderTransferOrderEditWindowModel));
            _uiVisualizerService.Register(typeof(ShareholderTransferOrderEditWindowModel), typeof(ShareholderTransferOrderEditWindow));

            return typeFactory.CreateInstanceWithParametersAndAutoCompletion<ShareholderTransferOrderEditWindowModel>(obj);
        }

        #endregion



        //public async Task<AuthorizesDocument> OpenAuthorizesDocumentWindow(AuthorizesDocument authorizesDocument)
        //{
        //    _uiVisualizerService.Unregister(typeof(AuthorizesDocumentWindowModel));
        //    _uiVisualizerService.Register(typeof(AuthorizesDocumentWindowModel), typeof(Views.AuthorizesDocumentWindow));

        //    var typeFactory = TypeFactory.Default;
        //    var authorizesDocumentWindowModel = typeFactory.CreateInstanceWithParametersAndAutoCompletion<AuthorizesDocumentWindowModel>(authorizesDocument);

        //    if (await _uiVisualizerService.ShowDialogAsync(authorizesDocumentWindowModel) ?? false)
        //    {
        //        using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
        //        {
        //            var unit = dbContextManager.Context.Units.Find(_unitService.GetLastUnitFromHistory().UnitId);

        //            var ad = dbContextManager.Context.AuthorizesDocuments.Find(authorizesDocument.DocumentId);
        //            var isNew = ad == null;

        //            if (isNew) unit.AuthorizesDocuments.Add(authorizesDocument);

        //            dbContextManager.Context.SaveChanges();

        //            await _unitService.OpenUnitWindow(unit);

        //            return dbContextManager.Context.AuthorizesDocuments.Find(authorizesDocument.DocumentId);
        //        }
        //    }

        //    return null;
        //}
    }
}