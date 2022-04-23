using Catel.Data;
using Catel.MVVM;

using PRC.PacketBatchFiller.Models.LegalEntityEntity;

namespace PRC.PacketBatchFiller.ViewModels.LegalEntityEntity.IssuesOfSecurities
{
    public class IssueOfSecuritiesViewModel : ViewModelBase
    {
        public IssueOfSecuritiesViewModel(IssueOfSecurities issueOfSecurities)
        {
            IssueOfSecuritiesModel = issueOfSecurities ?? new IssueOfSecurities();

            RemoveIssueOfSecuritiesCommand = new Command(RemoveIssueOfSecurities);
        }

        #region Type property

        [ViewModelToModel("IssueOfSecuritiesModel")]
        public SecuritiesTypes Type
        {
            get { return GetValue<SecuritiesTypes>(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly PropertyData TypeProperty = RegisterProperty("Type", typeof(SecuritiesTypes));

        #endregion

        #region Number property

        [ViewModelToModel("IssueOfSecuritiesModel")]
        public string Number
        {
            get { return GetValue<string>(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly PropertyData NumberProperty = RegisterProperty("Number", typeof(string));

        #endregion
        

        #region IssueOfSecuritiesModel model property

        [Model]
        public IssueOfSecurities IssueOfSecuritiesModel
        {
            get { return GetValue<IssueOfSecurities>(IssueOfSecuritiesModelProperty); }
            private set { SetValue(IssueOfSecuritiesModelProperty, value); }
        }

        public static readonly PropertyData IssueOfSecuritiesModelProperty = RegisterProperty("IssueOfSecuritiesModel", typeof (IssueOfSecurities));

        #endregion

        
        #region EntryNeedDelete property

        public bool EntryNeedDelete
        {
            get { return GetValue<bool>(EntryNeedDeleteProperty); }
            set { SetValue(EntryNeedDeleteProperty, value); }
        }

        public static readonly PropertyData EntryNeedDeleteProperty = RegisterProperty("EntryNeedDelete", typeof(bool));

        #endregion



        #region RemoveIssueOfSecurities command

        public Command RemoveIssueOfSecuritiesCommand { get; set; }
        
        private void RemoveIssueOfSecurities()
        {
            EntryNeedDelete = !EntryNeedDelete;
        }

        #endregion
    }
}