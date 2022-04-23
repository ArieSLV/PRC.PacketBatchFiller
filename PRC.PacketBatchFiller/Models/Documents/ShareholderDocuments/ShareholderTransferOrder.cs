using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using Catel.Data;
using PRC.PacketBatchFiller.Models.BaseClasses;
using PRC.PacketBatchFiller.Models.LegalEntityEntity;

namespace PRC.PacketBatchFiller.Models.Documents.ShareholderDocuments
{
    [Serializable]
    [Table("ShareholderTransferOrders")]
    public class ShareholderTransferOrder : Document
    {
        public const string FormName = "Списание ЦБ";

        #region IssueOfSecurities property

        public IssueOfSecurities IssueOfSecurities
        {
            get { return GetValue<IssueOfSecurities>(IssueOfSecuritiesProperty); }
            set { SetValue(IssueOfSecuritiesProperty, value); }
        }

        public static readonly PropertyData IssueOfSecuritiesProperty = RegisterProperty("IssueOfSecurities", typeof (IssueOfSecurities));

        #endregion

        #region QuantityOfTransferedSecurities property

        public string QuantityOfTransferedSecurities
        {
            get { return GetValue<string>(QuantityOfTransferedSecuritiesProperty); }
            set { SetValue(QuantityOfTransferedSecuritiesProperty, value); }
        }

        public static readonly PropertyData QuantityOfTransferedSecuritiesProperty =
            RegisterProperty("QuantityOfTransferedSecurities", typeof (string));

        #endregion

        #region TransferReasonDocuments property

        public ObservableCollection<TransferReasonDocument> TransferReasonDocuments
        {
            get { return GetValue<ObservableCollection<TransferReasonDocument>>(TransferReasonDocumentsProperty); }
            set { SetValue(TransferReasonDocumentsProperty, value); }
        }

        public static readonly PropertyData TransferReasonDocumentsProperty = RegisterProperty(
            "TransferReasonDocuments", typeof (ObservableCollection<TransferReasonDocument>));

        #endregion

        #region AmountOfTransaction property

        public string AmountOfTransaction
        {
            get { return GetValue<string>(AmountOfTransactionProperty); }
            set { SetValue(AmountOfTransactionProperty, value); }
        }

        public static readonly PropertyData AmountOfTransactionProperty = RegisterProperty("AmountOfTransaction",
            typeof (string));

        #endregion

        #region CashPaymentFlag property

        public bool CashPaymentFlag
        {
            get { return GetValue<bool>(CashPaymentFlagProperty); }
            set { SetValue(CashPaymentFlagProperty, value); }
        }

        public static readonly PropertyData CashPaymentFlagProperty = RegisterProperty("CashPaymentFlag", typeof (bool));

        #endregion

        #region DebitingAccount property

        public virtual ShareholderAccount DebitingAccount
        {
            get { return GetValue<ShareholderAccount>(DebitingAccountProperty); }
            set { SetValue(DebitingAccountProperty, value); }
        }

        public static readonly PropertyData DebitingAccountProperty = RegisterProperty("DebitingAccount",
            typeof (ShareholderAccount));

        #endregion

        #region CreditingAccount property

        public virtual ShareholderAccount CreditingAccount
        {
            get { return GetValue<ShareholderAccount>(CreditingAccountProperty); }
            set { SetValue(CreditingAccountProperty, value); }
        }

        public static readonly PropertyData CreditingAccountProperty = RegisterProperty("CreditingAccount",
            typeof (ShareholderAccount));

        #endregion
    }
}