using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Catel.Data;
using Catel.MVVM;
using PRC.PacketBatchFiller.Models.BaseClasses.Documents;
using PRC.PacketBatchFiller.Models.Documents.ShareholderDocuments;
using PRC.PacketBatchFiller.Services.Interfaces;

namespace PRC.PacketBatchFiller.ViewModels.Documents
{
    public class DocumentPackagesListWindowModel : ViewModelBase
    {
        private readonly IDocumentService _documentService;

        public DocumentPackagesListWindowModel(IDocumentService documentService)
        {
        
            _documentService = documentService;

            CreateNewShareholderDocumentPackageCommand = new TaskCommand(CreateNewShareholderDocumentPackageExecuted);
            EditDocumentPackageCommand = new TaskCommand(EditDocumentPackageExecute, CanEditAndRemoveDocumentPackage);
            RemoveDocumentPackageFromDataContextCommand = new Command(RemoveDocumentPackageFromDataContextExecute, CanEditAndRemoveDocumentPackage);
                

            DocumentPackageCollection = GetDocumentPackageCollectionFromContext();
        }

        #region DocumentPackageCollection property

        public ObservableCollection<DocumentPackage> DocumentPackageCollection
        {
            get { return GetValue<ObservableCollection<DocumentPackage>>(DocumentPackageCollectionProperty); }
            set { SetValue(DocumentPackageCollectionProperty, value); }
        }

        public static readonly PropertyData DocumentPackageCollectionProperty = RegisterProperty("DocumentPackageCollection", typeof (ObservableCollection<DocumentPackage>));

        #endregion

        #region SelectedDocumentPackage property

        public DocumentPackage SelectedDocumentPackage
        {
            get { return GetValue<DocumentPackage>(SelectedDocumentPackageProperty); }
            set { SetValue(SelectedDocumentPackageProperty, value); }
        }

        public static readonly PropertyData SelectedDocumentPackageProperty = RegisterProperty("SelectedDocumentPackage", typeof (DocumentPackage));

        #endregion


        #region CreateNewShareholderDocumentPackage command

        public TaskCommand CreateNewShareholderDocumentPackageCommand { get; private set; }

        private async Task CreateNewShareholderDocumentPackageExecuted()
        {
            await _documentService.OpenDocumentPackageWindow(new ShareholderDocumentPackage());

            DocumentPackageCollection = GetDocumentPackageCollectionFromContext();
        }

        #endregion

        #region EditDocumentPackage command
        
        public TaskCommand EditDocumentPackageCommand { get; private set; }

        private async Task EditDocumentPackageExecute()
        {
            await _documentService.OpenDocumentPackageWindow(SelectedDocumentPackage);

            DocumentPackageCollection = GetDocumentPackageCollectionFromContext();
        }

        private bool CanEditAndRemoveDocumentPackage()
        {
            return SelectedDocumentPackage != null;
        }

        #endregion

        #region RemoveDocumentPackageFromDataContext command

        public Command RemoveDocumentPackageFromDataContextCommand { get; private set; }

        private void RemoveDocumentPackageFromDataContextExecute()
        {
            _documentService.RemoveDocumentPackageFromDataContext(SelectedDocumentPackage);

            DocumentPackageCollection = GetDocumentPackageCollectionFromContext();
        }

        #endregion


        private static ObservableCollection<DocumentPackage> GetDocumentPackageCollectionFromContext()
        {
            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
            {
                var tmpCollection = new ObservableCollection<DocumentPackage>();

                var shCollection = new ObservableCollection<ShareholderDocumentPackage>(dbContextManager.Context.ShareholderDocumentPackages
                    .Include(o => o.MainAccount.Unit)
                    .Include(o => o.MainAccount.SecuritiesIssuer)
                    .Include(o => o.DocumentsCollection));
                //var siCollection = new ObservableCollection<SecuritiesIssuerDocumentPackage>();

                foreach (var shareholderDocumentPackage in shCollection) {tmpCollection.Add(shareholderDocumentPackage);}

                return new ObservableCollection<DocumentPackage>(tmpCollection.OrderByDescending(o => o.TimeStamp));
            }
        }

        public override string Title => "Список пакетов документов";
        protected override async Task InitializeAsync() { await base.InitializeAsync(); }
        protected override async Task CloseAsync() { await base.CloseAsync(); }
    }
}