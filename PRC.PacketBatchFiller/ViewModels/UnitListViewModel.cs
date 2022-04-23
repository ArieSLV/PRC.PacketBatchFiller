using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Catel.Data;
using Catel.MVVM;
using PRC.PacketBatchFiller.Models;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.Models.LegalEntityEntity;
using PRC.PacketBatchFiller.Models.PersonsEntity;
using PRC.PacketBatchFiller.Services.Interfaces;


namespace PRC.PacketBatchFiller.ViewModels
{
    public class UnitListViewModel : ViewModelBase
    {
        private readonly IUnitService _unitService;

        public UnitListViewModel(IUnitService unitService)
        {
            _unitService = unitService;

            UnitsCollection = GetUnitCollectionFromContext();

            CreateNewPersonCommand = new TaskCommand(CreateNewPersonExecuted);
            EditUnitCommand = new TaskCommand(OnEditUnitCommandExecute, CanEditAndRemoveUnitCommand);
            RemoveUnitCommand = new Command(OnRemoveUnitCommandExecute, CanEditAndRemoveUnitCommand);
        }

        #region Properties


        #region UnitsCollection property

        public ObservableCollection<Unit> UnitsCollection
        {
            get { return GetValue<ObservableCollection<Unit>>(UnitsCollectionProperty); }
            set { SetValue(UnitsCollectionProperty, value); }
        }

        public static readonly PropertyData UnitsCollectionProperty = RegisterProperty("UnitsCollection", typeof (ObservableCollection<Unit>));

        #endregion

        #region SelectedUnit property

        public Unit SelectedUnit
        {
            get { return GetValue<Unit>(SelectedUnitProperty); }
            set { SetValue(SelectedUnitProperty, value); }
        }

        public static readonly PropertyData SelectedUnitProperty = RegisterProperty("SelectedUnit", typeof (Unit));

        #endregion

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();
        }

        protected override async Task CloseAsync()
        {
            await base.CloseAsync();
        }

        public override string Title => "Список физических и юридических лиц";

        #endregion


        #region Commands

        #region CreateNewPersonCommand command

        public TaskCommand CreateNewPersonCommand { get; private set; }

        private async Task CreateNewPersonExecuted()
        {
            await _unitService.OpenUnitWindow(new Person());

            UnitsCollection = GetUnitCollectionFromContext();
        }

        #endregion

        #region EditUnitCommand command

        public TaskCommand EditUnitCommand { get; private set; }

        private async Task OnEditUnitCommandExecute()
        {
            await _unitService.OpenUnitWindow(SelectedUnit);
            
            UnitsCollection = GetUnitCollectionFromContext();

        }

        private bool CanEditAndRemoveUnitCommand()
        {
            return SelectedUnit != null;
        }

        #endregion

        #region RemoveUnitCommand command

        public Command RemoveUnitCommand { get; private set; }

        private void OnRemoveUnitCommandExecute()
        {
            _unitService.RemoveUnitFromDataContext(SelectedUnit);

            UnitsCollection = GetUnitCollectionFromContext();

        }


        #endregion

        #endregion

        #region Methods

        private static ObservableCollection<Unit> GetUnitCollectionFromContext()
        {
            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
            {
                var tmpCollection = new ObservableCollection<Unit>();

                var leCollection = new ObservableCollection<LegalEntity>(dbContextManager.Context.LegalEntities.Include(o => o.RegistrationCertificate));
                var pCollection = new ObservableCollection<Person>(dbContextManager.Context.Persons.Include(o => o.CardID));

                foreach (var legalEntity in leCollection) { tmpCollection.Add(legalEntity); }
                foreach (var person in pCollection) { tmpCollection.Add(person); }

                return new ObservableCollection<Unit>(tmpCollection.OrderByDescending(o => o.TimeStamp));
            }
        }

        #endregion
    }
}