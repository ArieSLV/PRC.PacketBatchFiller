using Catel.Data;
using Catel.MVVM;
using PRC.PacketBatchFiller.Models;
using PRC.PacketBatchFiller.Models.BaseClasses;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.Models.LegalEntityEntity;

namespace PRC.PacketBatchFiller.ViewModels.UnitEntity.ShareholderAccounts
{
    
    public class ShareholderAccountViewModel : ViewModelBase
    {
        public ShareholderAccountViewModel(ShareholderAccount shareholderAccount)
        {
            ShareholderAccountModel = shareholderAccount ?? new ShareholderAccount();

            RemoveShareholderAccountCommand = new Command(RemoveShareholderAccount);
        }

        
        #region ShareholderAccountType property

        [ViewModelToModel("ShareholderAccountModel")]
        public ShareholderAccountType ShareholderAccountType
        {
            get { return GetValue<ShareholderAccountType>(ShareholderAccountTypeProperty); }
            set { SetValue(ShareholderAccountTypeProperty, value); }
        }

        public static readonly PropertyData ShareholderAccountTypeProperty = RegisterProperty("ShareholderAccountType", typeof (ShareholderAccountType), ShareholderAccountType.Owner);

        #endregion

        #region SecuritiesIssuer property

        [ViewModelToModel("ShareholderAccountModel")]
        public LegalEntity SecuritiesIssuer
        {
            get { return GetValue<LegalEntity>(SecuritiesIssuerProperty); }
            set { SetValue(SecuritiesIssuerProperty, value); }
        }

        public static readonly PropertyData SecuritiesIssuerProperty = RegisterProperty("SecuritiesIssuer", typeof (LegalEntity));

        #endregion

        #region Number property

        [ViewModelToModel("ShareholderAccountModel")]
        public string Number
        {
            get { return GetValue<string>(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly PropertyData NumberProperty = RegisterProperty("Number", typeof (string));

        #endregion

        #region Unit property

        [ViewModelToModel("ShareholderAccountModel")]
        public Unit Unit
        {
            get { return GetValue<Unit>(UnitProperty); }
            set { SetValue(UnitProperty, value); }
        }

        public static readonly PropertyData UnitProperty = RegisterProperty("Unit", typeof(Unit));

        #endregion


        #region ShareholderAccountModel model property

        [Model]
        public ShareholderAccount ShareholderAccountModel
        {
            get { return GetValue<ShareholderAccount>(ShareholderAccountModelProperty); }
            private set { SetValue(ShareholderAccountModelProperty, value); }
        }

        public static readonly PropertyData ShareholderAccountModelProperty = RegisterProperty("ShareholderAccountModel", typeof (ShareholderAccount));

        #endregion


        #region EntryNeedDelete property

        public bool EntryNeedDelete
        {
            get { return GetValue<bool>(EntryNeedDeleteProperty); }
            set { SetValue(EntryNeedDeleteProperty, value); }
        }

        public static readonly PropertyData EntryNeedDeleteProperty = RegisterProperty("EntryNeedDelete", typeof(bool));

        #endregion

        #region RemoveShareholderAccount command

        public Command RemoveShareholderAccountCommand { get; set; }

        private void RemoveShareholderAccount()
        {
            EntryNeedDelete = !EntryNeedDelete;
        }

        #endregion








    }
}