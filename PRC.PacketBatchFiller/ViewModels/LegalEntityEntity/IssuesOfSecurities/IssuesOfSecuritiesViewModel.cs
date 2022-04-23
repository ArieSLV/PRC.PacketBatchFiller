using System.Collections.ObjectModel;
using Catel.Data;
using Catel.MVVM;
using PRC.PacketBatchFiller.Models;
using PRC.PacketBatchFiller.Models.LegalEntityEntity;
using PRC.PacketBatchFiller.Services.Interfaces;


namespace PRC.PacketBatchFiller.ViewModels.LegalEntityEntity.IssuesOfSecurities
{
    [InterestedIn(typeof(IssueOfSecuritiesViewModel))]
    public class IssuesOfSecuritiesViewModel : ViewModelBase
    {
        private readonly IUnitService _unitService;

        public IssuesOfSecuritiesViewModel(ObservableCollection<IssueOfSecurities> issuesOfSecurities, IUnitService unitService)
        {
            _unitService = unitService;
            IssuesOfSecuritiesCollection = issuesOfSecurities ?? new ObservableCollection<IssueOfSecurities>();
            IssuesOfSecuritiesViewModelCollection = new ObservableCollection<IssueOfSecuritiesViewModel>();

            foreach (var issueOfSecuritiese in IssuesOfSecuritiesCollection)
            {
                IssuesOfSecuritiesViewModelCollection.Add(new IssueOfSecuritiesViewModel(issueOfSecuritiese));
            }

            AddIssueOfSecuritiesCommand = new Command(AddIssueOfSecurities);
        }

        #region AddedIssueOfSecuritiesType property

        public SecuritiesTypes AddedIssueOfSecuritiesType
        {
            get { return GetValue<SecuritiesTypes>(AddedIssueOfSecuritiesTypeProperty); }
            set { SetValue(AddedIssueOfSecuritiesTypeProperty, value); }
        }

        public static readonly PropertyData AddedIssueOfSecuritiesTypeProperty = RegisterProperty("AddedIssueOfSecuritiesType", typeof (SecuritiesTypes), SecuritiesTypes.Unknown);

        #endregion

        #region AddedNumber property

        public string AddedNumber
        {
            get { return GetValue<string>(AddedNumberProperty); }
            set { SetValue(AddedNumberProperty, value); }
        }

        public static readonly PropertyData AddedNumberProperty = RegisterProperty("AddedNumber", typeof (string));

        #endregion


        #region IssuesOfSecuritiesCollection property

        public ObservableCollection<IssueOfSecurities> IssuesOfSecuritiesCollection
        {
            get { return GetValue<ObservableCollection<IssueOfSecurities>>(IssuesOfSecuritiesCollectionProperty); }
            set { SetValue(IssuesOfSecuritiesCollectionProperty, value); }
        }

        public static readonly PropertyData IssuesOfSecuritiesCollectionProperty = RegisterProperty("IssuesOfSecuritiesCollection", typeof (ObservableCollection<IssueOfSecurities>));

        #endregion

        #region IssuesOfSecuritiesViewModelCollection property

        public ObservableCollection<IssueOfSecuritiesViewModel> IssuesOfSecuritiesViewModelCollection
        {
            get { return GetValue<ObservableCollection<IssueOfSecuritiesViewModel>>(IssuesOfSecuritiesViewModelCollectionProperty); }
            set { SetValue(IssuesOfSecuritiesViewModelCollectionProperty, value); }
        }

        public static readonly PropertyData IssuesOfSecuritiesViewModelCollectionProperty = RegisterProperty("IssuesOfSecuritiesViewModelCollection", typeof (ObservableCollection<IssueOfSecuritiesViewModel>));

        #endregion

        #region AddIssueOfSecurities command
        
        public Command AddIssueOfSecuritiesCommand { get; set; }
        
        private void AddIssueOfSecurities()
        {
            var ios = new IssueOfSecurities
            {
                Type = AddedIssueOfSecuritiesType,
                Number = AddedNumber
            };

            var iosvm = new IssueOfSecuritiesViewModel(ios);
            IssuesOfSecuritiesCollection.Add(ios);
            IssuesOfSecuritiesViewModelCollection.Add(iosvm);

            AddedNumber = string.Empty;
            AddedIssueOfSecuritiesType = SecuritiesTypes.Unknown;
            
        }

        #endregion

        #region Methods

        protected override void OnViewModelPropertyChanged(IViewModel viewModel, string propertyName)
        {
            if (propertyName != "EntryNeedDelete") return;

            var vm = (IssueOfSecuritiesViewModel)viewModel;

            IssuesOfSecuritiesCollection.Remove(vm.IssueOfSecuritiesModel);
            IssuesOfSecuritiesViewModelCollection.Remove(vm);
            _unitService.AddToIssuesOfSecuritiesListToRemove(vm.IssueOfSecuritiesModel.IssueOfSecuritiesId);
        }

        #endregion
    }
}