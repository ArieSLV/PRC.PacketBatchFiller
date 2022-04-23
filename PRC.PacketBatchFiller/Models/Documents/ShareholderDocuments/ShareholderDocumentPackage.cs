using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Catel.Data;
using PRC.PacketBatchFiller.Models.BaseClasses;
using PRC.PacketBatchFiller.Models.BaseClasses.Documents;
using PRC.PacketBatchFiller.Models.LegalEntityEntity;

namespace PRC.PacketBatchFiller.Models.Documents.ShareholderDocuments
{
    [Table("ShareholderDocumentPackages")]
    public class ShareholderDocumentPackage : DocumentPackage
    {
        #region MainAccount property

        public ShareholderAccount MainAccount
        {
            get { return GetValue<ShareholderAccount>(MainAccountProperty); }
            set { SetValue(MainAccountProperty, value); }
        }

        public static readonly PropertyData MainAccountProperty = RegisterProperty("MainAccount", typeof (ShareholderAccount));

        #endregion

        public override string ToString()
        {
            var docList = new StringBuilder();

            if (DocumentsCollection != null)
                foreach (var document in DocumentsCollection)
                {
                    if (!string.IsNullOrWhiteSpace(docList.ToString())) docList.Append(", ");


                    #region Анкета

                    var shareholderQuestionary = document as ShareholderQuestionary;
                    if (shareholderQuestionary != null)
                    {
                        switch (shareholderQuestionary.SubmittingReason)
                        {
                            case QuestionnaireSubmittingReason.Unknown: break;
                            case QuestionnaireSubmittingReason.Open:
                                docList.Append("Анкета+Заявление");
                                break;
                            case QuestionnaireSubmittingReason.Edit:
                                docList.Append("Анкета");
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }

                    #endregion


                    #region Доверка

                    var shareholderAuthorizesDocument = document as ShareholderAuthorizesDocument;
                    if (shareholderAuthorizesDocument != null)
                    {
                        using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
                        {
                            var adt = dbContextManager.Context.AuthorizesDocuments.Find(shareholderAuthorizesDocument.DocumentId);
                            dbContextManager.Context.Entry(adt).Reference(o => o.AuthorizesDocumentType).Load();
                            docList.Append(adt?.AuthorizesDocumentType != null ? $"{adt.AuthorizesDocumentType}" : "");
                        }
                    } 

                    #endregion

                    var shareholderTransferOrder = document as ShareholderTransferOrder;
                    if (shareholderTransferOrder != null)
                    {
                        using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
                        {
                            docList.Append("Списание ЦБ");

                            var sto = dbContextManager.Context.ShareholderTransferOrders.Find(shareholderTransferOrder.DocumentId);
                            dbContextManager.Context.Entry(sto).Reference(o => o.IssueOfSecurities).Load();

                            //var ios = dbContextManager.Context.IssuesOfSecurities.Find(sto.IssueOfSecurities.IssueOfSecuritiesId);
                            //dbContextManager.Context.Entry(ios).re

                            if (sto.IssueOfSecurities == null) continue;

                            switch (sto.IssueOfSecurities.Type)
                            {
                                case SecuritiesTypes.SimpleShare:
                                    docList.Append(" (АОИ)");
                                    break;
                                case SecuritiesTypes.PreferredTypaAShare:
                                    docList.Append(" (АПИтА)");
                                    break;
                                case SecuritiesTypes.PreferredShare:
                                    docList.Append(" (АПИ)");
                                    break;
                                case SecuritiesTypes.Unknown:
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                    }
                }

            return MainAccount != null ? $"{MainAccount}: {docList}" : "ОШИБКА!";
        }
    }
}