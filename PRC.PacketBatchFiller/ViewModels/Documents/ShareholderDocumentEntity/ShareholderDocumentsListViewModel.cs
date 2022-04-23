using System.Collections.ObjectModel;
using Catel.Data;
using Catel.MVVM;
using PRC.PacketBatchFiller.Models.BaseClasses;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.Models.Documents.ShareholderDocuments;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity.ShareholderAuthorizesDocumentEntity;
using PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity.ShareholderQuestionaryEntity;
using PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity.ShareholderTransferOrderEntity;

namespace PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity
{
    public class ShareholderDocumentsListViewModel : ViewModelBase
    {
        private readonly IDocumentService _documentService;

        public ShareholderDocumentsListViewModel(ObservableCollection<Document> documents, ShareholderAccount shareholderAccount, IDocumentService documentService)
        {
            _documentService = documentService;
            MainAccount = shareholderAccount;

            AuthorizedUnitsCollection = MainAccount.GetAuthorizedUnitsCollection();

            DocumentsCollection = documents ?? new ObservableCollection<Document>();
            DocumentViewModelsCollection = new ObservableCollection<IViewModel>();

            foreach (var document in DocumentsCollection)
            {
                DocumentViewModelsCollection.Add(GetDocumentViewModel(document));
            }

            AddShareholderQuestionaryCommand = new Command(AddShareholderQuestionaryExecuted);
            AddShareholderAuthorizesDocumentCommand = new Command(AddShareholderAuthorizesDocumentExecute);
            AddShareholderTransferOrderCommand = new Command(AddShareholderTransferOrderExecute);
        }

        #region MainAccount property

        public ShareholderAccount MainAccount
        {
            get { return GetValue<ShareholderAccount>(MainAccountProperty); }
            set { SetValue(MainAccountProperty, value); }
        }

        public static readonly PropertyData MainAccountProperty = RegisterProperty("MainAccount", typeof (ShareholderAccount));

        #endregion

        #region DocumentsCollection property

        public ObservableCollection<Document> DocumentsCollection
        {
            get { return GetValue<ObservableCollection<Document>>(DocumentsCollectionProperty); }
            set { SetValue(DocumentsCollectionProperty, value); }
        }

        public static readonly PropertyData DocumentsCollectionProperty = RegisterProperty("DocumentsCollection", typeof (ObservableCollection<Document>));

        #endregion

        #region AuthorizedUnitsCollection property

        public ObservableCollection<Unit> AuthorizedUnitsCollection
        {
            get { return GetValue<ObservableCollection<Unit>>(AuthorizedUnitsCollectionProperty); }
            set { SetValue(AuthorizedUnitsCollectionProperty, value); }
        }

        public static readonly PropertyData AuthorizedUnitsCollectionProperty = RegisterProperty("AuthorizedUnitsCollection", typeof(ObservableCollection<Unit>));

        #endregion

        #region DocumentViewModelsCollection property

        public ObservableCollection<IViewModel> DocumentViewModelsCollection
        {
            get { return GetValue<ObservableCollection<IViewModel>>(DocumentViewModelsCollectionProperty); }
            set { SetValue(DocumentViewModelsCollectionProperty, value); }
        }

        public static readonly PropertyData DocumentViewModelsCollectionProperty = RegisterProperty("DocumentViewModelsCollection", typeof (ObservableCollection<IViewModel>));

        #endregion

        #region Commands

        #region AddShareholderQuestionary command

        public Command AddShareholderQuestionaryCommand { get; set; }
        
        private async void AddShareholderQuestionaryExecuted()
        {
            var shq = new ShareholderQuestionary {ShareholderAccount = MainAccount};

            shq = (ShareholderQuestionary) await _documentService.OpenDocumentEditWindow(shq, AuthorizedUnitsCollection);

            AddDocumentToCollections(shq);
        }

        #endregion

        #region AddShareholderAuthorizesDocument command

        public Command AddShareholderAuthorizesDocumentCommand { get; private set;}
        
        private async void AddShareholderAuthorizesDocumentExecute()
        {
            var shad = new ShareholderAuthorizesDocument {WhoGivingAuthority = MainAccount};

            shad = (ShareholderAuthorizesDocument) await _documentService.OpenDocumentEditWindow(shad, AuthorizedUnitsCollection);

            foreach (var authorizedUnit in shad.AuthorizedUnits)
            {
                AuthorizedUnitsCollection.Add(authorizedUnit);
            }


            AddDocumentToCollections(shad);
        }

        #endregion

        #region AddShareholderTransferOrder command

        public Command AddShareholderTransferOrderCommand {get; private set; }
        
        private async void AddShareholderTransferOrderExecute()
        {
            var shto = new ShareholderTransferOrder {DebitingAccount = MainAccount};

            shto = (ShareholderTransferOrder) await _documentService.OpenDocumentEditWindow(shto, AuthorizedUnitsCollection);

            AddDocumentToCollections(shto);
        }

        #endregion

        #endregion


        #region Methods

        private IViewModel GetDocumentViewModel(Document document)
        {
            var questionary = document as ShareholderQuestionary;
            var shareholderAuthorizesDocument = document as ShareholderAuthorizesDocument;
            var shareholderTransferOrder = document as ShareholderTransferOrder;

            if (questionary != null) return new ShareholderQuestionaryViewModel(questionary, _documentService);
            if (shareholderAuthorizesDocument != null) return new ShareholderAuthorizesDocumentViewModel(shareholderAuthorizesDocument, _documentService);
            if (shareholderTransferOrder != null)  return new ShareholderTransferOrderViewModel(shareholderTransferOrder, _documentService);

            return null;
        }

        private void AddDocumentToCollections(Document doc)
        {
            if (doc.DocumentId == 0) return;

            DocumentsCollection.Add(doc);
            DocumentViewModelsCollection.Add(GetDocumentViewModel(doc));
        }

        #endregion

    }
}