using System.ComponentModel.DataAnnotations.Schema;
using Catel.Data;

namespace PRC.PacketBatchFiller.Models.Documents
{
    [Table("TransferReasonTypes")]
    public class TransferReasonType : ModelBase
    {
        public const string DefaultValue = "[Тип документа-основания не выбран]";
        public const string MainField = "Value";

        #region TransferReasonTypeId property

        public long TransferReasonTypeId
        {
            get { return GetValue<long>(TransferReasonTypeIdProperty); }
            set { SetValue(TransferReasonTypeIdProperty, value); }
        }

        public static readonly PropertyData TransferReasonTypeIdProperty = RegisterProperty("TransferReasonTypeId", typeof (long));

        #endregion

        #region Value property

        public string Value
        {
            get { return GetValue<string>(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly PropertyData ValueProperty = RegisterProperty("Value", typeof(string));

        #endregion

        public override string ToString()
        {
            return Value;
        }
    }
}