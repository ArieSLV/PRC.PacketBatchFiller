using System;
using System.ComponentModel.DataAnnotations.Schema;
using Catel.Data;

namespace PRC.PacketBatchFiller.Models.Documents
{
    [Serializable]
    [Table("TransferReasonDocuments")]
    public class TransferReasonDocument : ModelBase
    {
        #region TransferReasonDocumentId property

        public long TransferReasonDocumentId
        {
            get { return GetValue<long>(TransferReasonDocumentIdProperty); }
            set { SetValue(TransferReasonDocumentIdProperty, value); }
        }

        public static readonly PropertyData TransferReasonDocumentIdProperty = RegisterProperty("TransferReasonDocumentId", typeof (long));

        #endregion

        #region TransferReasonType property

        public virtual TransferReasonType TransferReasonType
        {
            get { return GetValue<TransferReasonType>(TransferReasonTypeProperty); }
            set { SetValue(TransferReasonTypeProperty, value); }
        }

        public static readonly PropertyData TransferReasonTypeProperty = RegisterProperty("TransferReasonType", typeof (TransferReasonType));

        #endregion

        #region Number property

        public string Number
        {
            get { return GetValue<string>(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly PropertyData NumberProperty = RegisterProperty("Number", typeof (string));

        #endregion

        #region Date property

        public DateTime? Date
        {
            get { return GetValue<DateTime?>(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public static readonly PropertyData DateProperty = RegisterProperty("Date", typeof (DateTime?));

        #endregion
    }
}