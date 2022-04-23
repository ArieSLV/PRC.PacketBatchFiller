using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Catel.Data;
using Catel.MVVM;
using Catel.Services;
using PRC.PacketBatchFiller.Models.BaseClasses;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.Models.Documents.ShareholderDocuments;
using PRC.PacketBatchFiller.Services.Interfaces;

namespace PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity
{
    [InterestedIn(typeof(ShareholderAccountSearchViewModel))]
    public class ShareholderDocumentPackageWindowModel : ViewModelBase
    {
        private readonly IDocumentService _documentService;

        public ShareholderDocumentPackageWindowModel(ShareholderDocumentPackage shareholderDocumentPackage, IUIVisualizerService uiVisualizerService, IUnitService unitService, IDocumentService documentService)
        {
            _documentService = documentService;

            ShareholderDocumentPackageModel = shareholderDocumentPackage;
            DocumentsCollection = shareholderDocumentPackage.DocumentsCollection ?? new ObservableCollection<Document>();
            MainAccount = shareholderDocumentPackage.MainAccount;

            

            ShareholderAccountSearchViewModel = new ShareholderAccountSearchViewModel(MainAccount, uiVisualizerService, unitService);
            ShareholderDocumentsListViewModel = new ShareholderDocumentsListViewModel(DocumentsCollection, MainAccount, documentService);
        }

        #region ShareholderDocumentPackage model & properties

        #region Name property

        [ViewModelToModel("ShareholderDocumentPackageModel")]
        public string Name
        {
            get { return GetValue<string>(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly PropertyData NameProperty = RegisterProperty("Name", typeof(string));

        #endregion

        #region DocumentsCollection property

        [ViewModelToModel("ShareholderDocumentPackageModel")]
        public ObservableCollection<Document> DocumentsCollection
        {
            get { return GetValue<ObservableCollection<Document>>(DocumensCollectionProperty); }
            set { SetValue(DocumensCollectionProperty, value); }
        }

        public static readonly PropertyData DocumensCollectionProperty = RegisterProperty("DocumentsCollection", typeof(ObservableCollection<Document>));

        #endregion

        #region MainAccount property

        [ViewModelToModel("ShareholderDocumentPackageModel")]
        public ShareholderAccount MainAccount
        {
            get { return GetValue<ShareholderAccount>(MainAccountProperty); }
            set { SetValue(MainAccountProperty, value); }
        }

        public static readonly PropertyData MainAccountProperty = RegisterProperty("MainAccount", typeof(ShareholderAccount));

        #endregion


        #region ShareholderDocumentPackageModel model property

        [Model]
        public ShareholderDocumentPackage ShareholderDocumentPackageModel
        {
            get { return GetValue<ShareholderDocumentPackage>(ShareholderDocumentPackageModelProperty); }
            private set { SetValue(ShareholderDocumentPackageModelProperty, value); }
        }

        public static readonly PropertyData ShareholderDocumentPackageModelProperty = RegisterProperty("ShareholderDocumentPackageModel", typeof (ShareholderDocumentPackage));

        #endregion

        #endregion

        #region UI properties

        #region ShareholderAccountSearchViewModel property

        public IViewModel ShareholderAccountSearchViewModel
        {
            get { return GetValue<IViewModel>(ShareholderAccountSearchViewModelProperty); }
            set { SetValue(ShareholderAccountSearchViewModelProperty, value); }
        }

        public static readonly PropertyData ShareholderAccountSearchViewModelProperty = RegisterProperty("ShareholderAccountSearchViewModel", typeof (IViewModel));

        #endregion

        #region ShareholderDocumentsListViewModel property

        public IViewModel ShareholderDocumentsListViewModel
        {
            get { return GetValue<IViewModel>(ShareholderDocumentsListViewModelProperty); }
            set { SetValue(ShareholderDocumentsListViewModelProperty, value); }
        }

        public static readonly PropertyData ShareholderDocumentsListViewModelProperty = RegisterProperty("ShareholderDocumentsListViewModel", typeof (IViewModel));

        #endregion

        

        #endregion

        #region Methods

        protected override void OnViewModelPropertyChanged(IViewModel viewModel, string propertyName)
        {
            if (propertyName == "TargetEntity")
            {
                if (viewModel is ShareholderAccountSearchViewModel)
                {
                    var vm = (ShareholderAccountSearchViewModel)viewModel;
                    MainAccount = vm.TargetEntity;
                    ShareholderDocumentsListViewModel = new ShareholderDocumentsListViewModel(DocumentsCollection, MainAccount, _documentService);

                }
            }
        }

        #endregion

        public override string Title => "Пакет документов акционера";
        protected override async Task InitializeAsync() { await base.InitializeAsync(); }
        protected override async Task CloseAsync() { await base.CloseAsync(); }
    }
}