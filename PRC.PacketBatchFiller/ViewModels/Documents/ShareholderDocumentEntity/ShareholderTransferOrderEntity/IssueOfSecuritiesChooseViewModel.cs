using System.Collections.ObjectModel;
using System.Windows;
using Catel.Data;
using Catel.MVVM;
using PRC.PacketBatchFiller.Models.LegalEntityEntity;

namespace PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity.ShareholderTransferOrderEntity
{
    [InterestedIn(typeof(IssueOfSecuritiesViewModel))]
    public class IssueOfSecuritiesChooseViewModel : ViewModelBase
    {
        public IssueOfSecuritiesChooseViewModel(LegalEntity securitiesIssuer)
        {
            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
            {
                dbContextManager.Context.Entry(securitiesIssuer).Reference(o =>o.IssuesOfSecurities).Load();
                IssueOfSecuritiesCollection = securitiesIssuer.IssuesOfSecurities;
            }

            foreach (var issueOfSecuritiese in IssueOfSecuritiesCollection)
            {
                IssueOfSecuritiesViewModelCollection.Add(new IssueOfSecuritiesViewModel(issueOfSecuritiese));
            }

            ResetSelectedIssueOfSecuritiesCommand = new Command(ResetSelectedIssueOfSecurities);
        }

        #region IssueOfSecuritiesCollection property

        public ObservableCollection<IssueOfSecurities> IssueOfSecuritiesCollection
        {
            get { return GetValue<ObservableCollection<IssueOfSecurities>>(IssueOfSecuritiesCollectionProperty); }
            set { SetValue(IssueOfSecuritiesCollectionProperty, value); }
        }

        public static readonly PropertyData IssueOfSecuritiesCollectionProperty = RegisterProperty("IssueOfSecuritiesCollection", typeof (ObservableCollection<IssueOfSecurities>));

        #endregion

        #region IssueOfSecuritiesViewModelCollection property

        public ObservableCollection<IssueOfSecuritiesViewModel> IssueOfSecuritiesViewModelCollection
        {
            get { return GetValue<ObservableCollection<IssueOfSecuritiesViewModel>>(IssueOfSecuritiesViewModelCollectionProperty); }
            set { SetValue(IssueOfSecuritiesViewModelCollectionProperty, value); }
        }

        public static readonly PropertyData IssueOfSecuritiesViewModelCollectionProperty = RegisterProperty("IssueOfSecuritiesViewModelCollection", typeof (ObservableCollection<IssueOfSecuritiesViewModel>));

        #endregion

        #region SelectedIssueOfSecurities property

        public IssueOfSecurities SelectedIssueOfSecurities
        {
            get { return GetValue<IssueOfSecurities>(SelectedIssueOfSecuritiesProperty); }
            set { SetValue(SelectedIssueOfSecuritiesProperty, value); }
        }

        public static readonly PropertyData SelectedIssueOfSecuritiesProperty = RegisterProperty("SelectedIssueOfSecurities", typeof (IssueOfSecurities));

        #endregion



        #region SelectedIssueOfSecuritiesVisibility property

        public Visibility SelectedIssueOfSecuritiesVisibility
        {
            get { return GetValue<Visibility>(SelectedIssueOfSecuritiesVisibilityProperty); }
            set { SetValue(SelectedIssueOfSecuritiesVisibilityProperty, value); }
        }

        public static readonly PropertyData SelectedIssueOfSecuritiesVisibilityProperty = RegisterProperty("SelectedIssueOfSecuritiesVisibility", typeof (Visibility));

        #endregion

        #region UnnecessaryIssueOfSecuritiesVisibility property

        public Visibility UnnecessaryIssueOfSecuritiesVisibility
        {
            get { return GetValue<Visibility>(UnnecessaryIssueOfSecuritiesVisibilityProperty); }
            set { SetValue(UnnecessaryIssueOfSecuritiesVisibilityProperty, value); }
        }

        public static readonly PropertyData UnnecessaryIssueOfSecuritiesVisibilityProperty = RegisterProperty("UnnecessaryIssueOfSecuritiesVisibility", typeof (Visibility));

        #endregion


        #region ResetSelectedIssueOfSecurities command
        
        public Command ResetSelectedIssueOfSecuritiesCommand {get; set;}

        private void ResetSelectedIssueOfSecurities()
        {
            SelectedIssueOfSecurities = null;
            SelectedIssueOfSecuritiesVisibility = Visibility.Collapsed;
            UnnecessaryIssueOfSecuritiesVisibility = Visibility.Visible;
        }

        #endregion
        
        
        #region Methods

        protected override void OnViewModelPropertyChanged(IViewModel viewModel, string propertyName)
        {
            if (propertyName != "IssueOfSecuritiesNeedSelect") return;

            var vm = (IssueOfSecuritiesViewModel)viewModel;

            SelectedIssueOfSecurities = vm.IssueOfSecuritiesModel;

            SelectedIssueOfSecuritiesVisibility = Visibility.Visible;
            UnnecessaryIssueOfSecuritiesVisibility = Visibility.Collapsed;
        }

        #endregion
    }
}