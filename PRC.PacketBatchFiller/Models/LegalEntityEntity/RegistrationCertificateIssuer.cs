using System.ComponentModel.DataAnnotations.Schema;
using Catel.Data;

namespace PRC.PacketBatchFiller.Models.LegalEntityEntity
{
    [Table("RegistrationCertificateIssuers")]
    public class RegistrationCertificateIssuer : ModelBase
    {
        public const string DefaultValue = "[орган не выбран]";
        public const string MainField = "Value";

        #region RegistrationCertificateIssuerId property

        public long RegistrationCertificateIssuerId
        {
            get { return GetValue<long>(RegistrationCertificateIssuerIdProperty); }
            set { SetValue(RegistrationCertificateIssuerIdProperty, value); }
        }

        public static readonly PropertyData RegistrationCertificateIssuerIdProperty = RegisterProperty("RegistrationCertificateIssuerId", typeof (long));

        #endregion
        
        #region Value property

        public string Value
        {
            get { return GetValue<string>(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly PropertyData ValueProperty = RegisterProperty("Name", typeof(string));

        #endregion

        public override string ToString()
        {
            return Value;
        }

        public override bool Equals(object obj)
        {
            var item = obj as RegistrationCertificateIssuer;

            if (item == null) return false;
            return Value == item.Value && RegistrationCertificateIssuerId == item.RegistrationCertificateIssuerId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}