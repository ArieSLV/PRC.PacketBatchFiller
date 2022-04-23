using System.ComponentModel.DataAnnotations.Schema;
using Catel.Data;

namespace PRC.PacketBatchFiller.Models.BaseClasses.Documents
{
    [Table("AuthorizesDocumentTypes")]
    public class AuthorizesDocumentType : ModelBase
    {
        public const string DefaultValue = "[Тип документа не выбран]";
        public const string MainField = "Value";

        #region AuthorizesDocumentTypeId property

        public long AuthorizesDocumentTypeId
        {
            get { return GetValue<long>(AuthorizesDocumentTypeIdProperty); }
            set { SetValue(AuthorizesDocumentTypeIdProperty, value); }
        }

        public static readonly PropertyData AuthorizesDocumentTypeIdProperty = RegisterProperty("AuthorizesDocumentTypeId", typeof (long));

        #endregion

        #region Value property

        public string Value
        {
            get { return GetValue<string>(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly PropertyData ValueProperty = RegisterProperty("Value", typeof (string));

        #endregion

        public override string ToString()
        {
            return Value;
        }
    }
}