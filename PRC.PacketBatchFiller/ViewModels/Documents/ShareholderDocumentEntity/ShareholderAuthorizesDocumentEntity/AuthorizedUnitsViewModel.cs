using System.Collections.ObjectModel;
using Catel.Data;
using Catel.MVVM;
using Catel.Services;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.Models.LegalEntityEntity;
using PRC.PacketBatchFiller.Models.PersonsEntity;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.LegalEntityEntity;
using PRC.PacketBatchFiller.ViewModels.PersonEntity;
using PRC.PacketBatchFiller.ViewModels.UnitEntity;

namespace PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity.ShareholderAuthorizesDocumentEntity
{
    [InterestedIn(typeof(UnitSearchViewModel))]
    [InterestedIn(typeof(ForItemCollectionPersonViewModel))]
    [InterestedIn(typeof(ForItemCollectionLegalEntityViewModel))]
    public class AuthorizedUnitsViewModel : ViewModelBase
    {
        private readonly IUnitService _unitService;

        public AuthorizedUnitsViewModel(ObservableCollection<Unit> units, IUIVisualizerService uiVisualizerService, IUnitService unitService, ICommandManager commandManager)
        {
            _unitService = unitService;
            UnitCollection = units ?? new ObservableCollection<Unit>();
            UnitViewModelsCollection = new ObservableCollection<IViewModel>();

            foreach (var unit in UnitCollection)
            {
                UnitViewModelsCollection.Add(GetUnitViewModel(unit));
            }

            UnitSearchViewModel = new UnitSearchViewModel(AddedUnitToCollection, uiVisualizerService, unitService, commandManager);

            AddUnitCommand = new Command(AddUnitCommandExecute, CanAddUnit);
            
        }


        #region UnitCollection property

        public ObservableCollection<Unit> UnitCollection
        {
            get { return GetValue<ObservableCollection<Unit>>(UnitCollectionProperty); }
            set { SetValue(UnitCollectionProperty, value); }
        }

        public static readonly PropertyData UnitCollectionProperty = RegisterProperty("UnitCollection", typeof (ObservableCollection<Unit>));

        #endregion

        #region UnitViewModelsCollection property

        public ObservableCollection<IViewModel> UnitViewModelsCollection
        {
            get { return GetValue<ObservableCollection<IViewModel>>(UnitViewModelsCollectionProperty); }
            set { SetValue(UnitViewModelsCollectionProperty, value); }
        }

        public static readonly PropertyData UnitViewModelsCollectionProperty = RegisterProperty("UnitViewModelsCollection", typeof (ObservableCollection<IViewModel>));

        #endregion

        #region AddedUnitToCollection property

        public Unit AddedUnitToCollection
        {
            get { return GetValue<Unit>(AddedUnitToCollectionProperty); }
            set { SetValue(AddedUnitToCollectionProperty, value); }
        }

        public static readonly PropertyData AddedUnitToCollectionProperty = RegisterProperty("AddedUnitToCollection", typeof (Unit));

        #endregion
        

        #region UnitSearchViewModel property

        public IViewModel UnitSearchViewModel
        {
            get { return GetValue<IViewModel>(UnitSearchViewModelProperty); }
            set { SetValue(UnitSearchViewModelProperty, value); }
        }

        public static readonly PropertyData UnitSearchViewModelProperty = RegisterProperty("UnitSearchViewModel", typeof (IViewModel));

        #endregion

        
        #region Commands

        #region AddUnitCommand command

        public Command AddUnitCommand { get; set; }

        private void AddUnitCommandExecute()
        {
            UnitCollection.Add(AddedUnitToCollection);
            UnitViewModelsCollection.Add(GetUnitViewModel(AddedUnitToCollection));

            AddedUnitToCollection = null;

            ((UnitSearchViewModel) UnitSearchViewModel).RemoveSelectedItemCommand.Execute();
            
        }

        private bool CanAddUnit()
        {
            return AddedUnitToCollection != null;
        }

        #endregion

        #endregion


        #region Methods

        private IViewModel GetUnitViewModel(Unit unit)
        {
            var person = unit as Person;
            var legalEntity = unit as LegalEntity;

            if (person != null)      return new ForItemCollectionPersonViewModel(person, _unitService);
            if (legalEntity != null) return new ForItemCollectionLegalEntityViewModel(legalEntity, _unitService);

            return null;
        }

        protected override void OnViewModelPropertyChanged(IViewModel viewModel, string propertyName)
        {
            if (propertyName == "TargetEntity")
            {
                var unitSearchViewModel = viewModel as UnitSearchViewModel;
                if (unitSearchViewModel != null)
                {
                    var vm = unitSearchViewModel;
                    AddedUnitToCollection = vm.TargetEntity;
                }
            }
            else if (propertyName == "EntryNeedDelete")
            {
                var pvm = viewModel as ForItemCollectionPersonViewModel;
                var levm = viewModel as ForItemCollectionLegalEntityViewModel;

                if (pvm != null)
                {
                    UnitCollection.Remove(pvm.PersonModel);
                    UnitViewModelsCollection.Remove(pvm);
                }
                else if (levm != null)
                {
                    UnitCollection.Remove(levm.LegalEntityModel);
                    UnitViewModelsCollection.Remove(levm);
                }
            }
        }

        #endregion

    }
}