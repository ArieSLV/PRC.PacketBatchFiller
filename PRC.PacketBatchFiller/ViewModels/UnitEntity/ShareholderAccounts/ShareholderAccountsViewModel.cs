using System.Collections.ObjectModel;
using Catel.Data;
using Catel.MVVM;
using Catel.Services;
using PRC.PacketBatchFiller.Models;
using PRC.PacketBatchFiller.Models.BaseClasses;
using PRC.PacketBatchFiller.Models.LegalEntityEntity;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.Suggest;

namespace PRC.PacketBatchFiller.ViewModels.UnitEntity.ShareholderAccounts
{
    [InterestedIn(typeof(SecuritiesIssuerSearchViewModel))]
    [InterestedIn(typeof(ShareholderAccountViewModel))]
    public class ShareholderAccountsViewModel : ViewModelBase
    {
        private readonly IUnitService _unitService;

        public ShareholderAccountsViewModel(long unitId, ObservableCollection<ShareholderAccount> shareholderAccounts, IUIVisualizerService uiVisualizerService, ICommandManager commandManager, IUnitService unitService)
        {
            _unitService = unitService;
            ShareholderAccountsCollection = shareholderAccounts ?? new ObservableCollection<ShareholderAccount>();
            ShareholderAccountViewModelCollection = new ObservableCollection<ShareholderAccountViewModel>();

            foreach (var shareholderAccount in ShareholderAccountsCollection)
            {
                ShareholderAccountViewModelCollection.Add(new ShareholderAccountViewModel(shareholderAccount));
            }


            var addedSecuritiesIssuer = unitService.GetUnitFromRestoreList(typeof(SecuritiesIssuerSearchViewModel), unitId);
            if (addedSecuritiesIssuer != null)
            {
                AddedSecuritiesIssuer = addedSecuritiesIssuer;
            }

            SecuritiesIssuerSearchViewModel = new SecuritiesIssuerSearchViewModel(AddedSecuritiesIssuer, uiVisualizerService, unitService, commandManager);

            AddShareholderAccountCommand = new Command(AddShareholderAccount);
        }


        #region AddedNumber property

        public string AddedNumber
        {
            get { return GetValue<string>(AddedNumberProperty); }
            set { SetValue(AddedNumberProperty, value); }
        }

        public static readonly PropertyData AddedNumberProperty = RegisterProperty("AddedNumber", typeof (string));

        #endregion

        #region AddedSecuritiesIssuer property

        public LegalEntity AddedSecuritiesIssuer
        {
            get { return GetValue<LegalEntity>(AddedSecuritiesIssuerProperty); }
            set { SetValue(AddedSecuritiesIssuerProperty, value); }
        }

        public static readonly PropertyData AddedSecuritiesIssuerProperty = RegisterProperty("AddedSecuritiesIssuer", typeof (LegalEntity));

        #endregion

        #region AddedShareholderAccountType property

        public ShareholderAccountType AddedShareholderAccountType
        {
            get { return GetValue<ShareholderAccountType>(AddedShareholderAccountTypeProperty); }
            set { SetValue(AddedShareholderAccountTypeProperty, value); }
        }

        public static readonly PropertyData AddedShareholderAccountTypeProperty = RegisterProperty("AddedShareholderAccountType", typeof (ShareholderAccountType));

        #endregion

        
        #region ShareholderAccountsCollection property

        public ObservableCollection<ShareholderAccount> ShareholderAccountsCollection
        {
            get { return GetValue<ObservableCollection<ShareholderAccount>>(ShareholderAccountsCollectionProperty); }
            set { SetValue(ShareholderAccountsCollectionProperty, value); }
        }

        public static readonly PropertyData ShareholderAccountsCollectionProperty = RegisterProperty("ShareholderAccountsCollection", typeof(ObservableCollection<ShareholderAccount>));

        #endregion

        #region ShareholderAccountViewModelCollection property

        public ObservableCollection<ShareholderAccountViewModel> ShareholderAccountViewModelCollection
        {
            get { return GetValue<ObservableCollection<ShareholderAccountViewModel>>(ShareholderAccountViewModelCollectionProperty); }
            set { SetValue(ShareholderAccountViewModelCollectionProperty, value); }
        }

        public static readonly PropertyData ShareholderAccountViewModelCollectionProperty = RegisterProperty("ShareholderAccountViewModelCollection", typeof(ObservableCollection<ShareholderAccountViewModel>));

        #endregion



        #region SecuritiesIssuerSearchViewModel property

        public IViewModel SecuritiesIssuerSearchViewModel
        {
            get { return GetValue<IViewModel>(SecuritiesIssuerSearchViewModelProperty); }
            set { SetValue(SecuritiesIssuerSearchViewModelProperty, value); }
        }

        public static readonly PropertyData SecuritiesIssuerSearchViewModelProperty = RegisterProperty("SecuritiesIssuerSearchViewModel", typeof(IViewModel));

        #endregion


        


        

        #region AddShareholderAccount command

        public Command AddShareholderAccountCommand { get; set; }
        
        private void AddShareholderAccount()
        {
            if (AddedNumber == null) AddedNumber = string.Empty;
            var sa = new ShareholderAccount
            {
                Number = AddedNumber,
                SecuritiesIssuer = AddedSecuritiesIssuer,
                ShareholderAccountType = AddedShareholderAccountType
            };

            
            var savm = new ShareholderAccountViewModel(sa);
            ShareholderAccountsCollection.Add(sa);
            ShareholderAccountViewModelCollection.Add(savm);
            

            AddedNumber = string.Empty;
            AddedSecuritiesIssuer = null;
            ((SecuritiesIssuerSearchViewModel) SecuritiesIssuerSearchViewModel).RemoveSelectedItemCommand.Execute();
            
        }

        #endregion
     

        #region Methods
        protected override void OnViewModelPropertyChanged(IViewModel viewModel, string propertyName)
        {
            if (propertyName == "TargetEntity" && viewModel is SecuritiesIssuerSearchViewModel)
            {
                var vm = (SecuritiesIssuerSearchViewModel)viewModel;
                AddedSecuritiesIssuer = vm.TargetEntity;
            }
            else if (propertyName == "EntryNeedDelete" && viewModel is ShareholderAccountViewModel)
            {
                var vm = (ShareholderAccountViewModel) viewModel;

                ShareholderAccountsCollection.Remove(vm.ShareholderAccountModel);
                ShareholderAccountViewModelCollection.Remove(vm);
                _unitService.AddToShareholderAccountsListToRemove(vm.ShareholderAccountModel.ShareholderAccountId);
            }


        }
        #endregion
    }
}