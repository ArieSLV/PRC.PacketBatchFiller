using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Catel.Data;
using Catel.IoC;
using Catel.MVVM;
using Catel.Services;
using PRC.PacketBatchFiller.DataAccess;
using PRC.PacketBatchFiller.Models;
using PRC.PacketBatchFiller.Models.Documents.ShareholderDocuments;
using PRC.PacketBatchFiller.Models.LegalEntityEntity;
using PRC.PacketBatchFiller.Models.PersonsEntity;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.Documents;
using PRC.PacketBatchFiller.ViewModels.LegalEntityEntity;
using PRC.PacketBatchFiller.Views.Documents;

namespace PRC.PacketBatchFiller.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IUIVisualizerService _uiVisualizerService;
        private readonly IPleaseWaitService _pleaseWaitService;
        private readonly IUnitService _unitService;

        public MainWindowViewModel(IUIVisualizerService uiVisualizerService, IPleaseWaitService pleaseWaitService, IUnitService unitService)
        {
            _uiVisualizerService = uiVisualizerService;
            _pleaseWaitService = pleaseWaitService;
            _unitService = unitService;

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PBFContext, Configuration>());
            
            OpenUnitListWindowCommand = new TaskCommand(OpenUnitListWindow);
            CreateNewPersonCommand = new Command(CreateNewPersonExecuted);
            CreateNewLegalEntityCommand = new TaskCommand(CreateNewLegalEntityExecuted);
            OpenDocumentCreationWindowCommand = new TaskCommand(OpenDocumentCreationWindowExecuted);
            
        }
        
        public override string Title => "Главное меню";
        protected override async Task InitializeAsync() { await base.InitializeAsync(); }
        protected override async Task CloseAsync() { await base.CloseAsync(); }

        #region OpenUnitListWindow command

        public TaskCommand OpenUnitListWindowCommand { get; private set; }

        private async Task OpenUnitListWindow()
        {
            _pleaseWaitService.Show("Открывается список клиентов");
            var unitListWindow = this.GetTypeFactory().CreateInstance<UnitListViewModel>();
            _pleaseWaitService.Hide();
            await _uiVisualizerService.ShowDialogAsync(unitListWindow);

        }

        #endregion

        #region CreateNewPersonCommand command

        public Command CreateNewPersonCommand { get; private set; }
        
        private void CreateNewPersonExecuted()
        {
            _unitService.OpenUnitWindow(new Person {RoleIsShareHolderFlag = true});
        }
        #endregion

        #region AddLegalEntityCommand command

        public TaskCommand CreateNewLegalEntityCommand { get; private set; }

        private async Task CreateNewLegalEntityExecuted()
        {
            await _unitService.OpenUnitWindow(new LegalEntity());
        }

        #endregion


        #region OpenDocumentCreationWindow command

        public TaskCommand OpenDocumentCreationWindowCommand { get; set; }

        private async Task OpenDocumentCreationWindowExecuted()
        {
            var typeFactory = TypeFactory.Default;

            _uiVisualizerService.Unregister(typeof(DocumentPackagesListWindowModel));
            _uiVisualizerService.Register(typeof(DocumentPackagesListWindowModel), typeof(DocumentPackagesListWindow));

            var viewModel = typeFactory.CreateInstanceWithParametersAndAutoCompletion<DocumentPackagesListWindowModel>();

            await _uiVisualizerService.ShowDialogAsync(viewModel);
        }

        #endregion
        
    }
}