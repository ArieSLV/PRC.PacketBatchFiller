using Catel.Data;
using Catel.MVVM;
using PRC.PacketBatchFiller.Models.LegalEntityEntity;

namespace PRC.PacketBatchFiller.ViewModels
{
    public class IssueOfSecuritiesViewModel : ViewModelBase
    {
        public IssueOfSecuritiesViewModel(IssueOfSecurities issueOfSecurities)
        {
            IssueOfSecuritiesModel = issueOfSecurities ?? new IssueOfSecurities();

            ChooseIssuerOfSecuritiesCommand = new Command(ChooseIssuerOfSecurities);
        }

        #region IssueOfSecuritiesId property

        public long IssueOfSecuritiesId
        {
            get { return GetValue<long>(IssueOfSecuritiesIdProperty); }
            set { SetValue(IssueOfSecuritiesIdProperty, value); }
        }

        public static readonly PropertyData IssueOfSecuritiesIdProperty = RegisterProperty("IssueOfSecuritiesId", typeof(long));

        #endregion

        #region Type property

        public SecuritiesTypes Type
        {
            get { return GetValue<SecuritiesTypes>(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly PropertyData TypeProperty = RegisterProperty("Type", typeof(SecuritiesTypes));

        #endregion

        #region Number property

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
        



        #region IssueOfSecuritiesNeedSelect property

        public bool IssueOfSecuritiesNeedSelect
        {
            get { return GetValue<bool>(IssueOfSecuritiesNeedSelectProperty); }
            set { SetValue(IssueOfSecuritiesNeedSelectProperty, value); }
        }

        public static readonly PropertyData IssueOfSecuritiesNeedSelectProperty = RegisterProperty("IssueOfSecuritiesNeedSelect", typeof (bool));

        #endregion

        #region ChooseIssuerOfSecurities command

        public Command ChooseIssuerOfSecuritiesCommand { get; set; }
        

        private void ChooseIssuerOfSecurities()
        {
            IssueOfSecuritiesNeedSelect = !IssueOfSecuritiesNeedSelect;
        }

        #endregion

       
    }
}