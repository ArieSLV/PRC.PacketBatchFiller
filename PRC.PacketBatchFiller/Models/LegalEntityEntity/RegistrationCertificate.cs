using System;
using System.ComponentModel.DataAnnotations.Schema;
using Catel.Data;

namespace PRC.PacketBatchFiller.Models.LegalEntityEntity
{
    [Table("RegistrationCertificates")]
    public class RegistrationCertificate : ModelBase
    {
        #region RegistrationCertificateId property

        public long RegistrationCertificateId
        {
            get { return GetValue<long>(RegistrationCertificateIdProperty); }
            set { SetValue(RegistrationCertificateIdProperty, value); }
        }
        
        public static readonly PropertyData RegistrationCertificateIdProperty = RegisterProperty("RegistrationCertificateId", typeof (long));

        #endregion

        #region Number property
        
        public string Number
        {
            get { return GetValue<string>(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }
        
        public static readonly PropertyData NumberProperty = RegisterProperty("Number", typeof(string));

        #endregion

        #region IssueDate property

        public DateTime? IssueDate
        {
            get { return GetValue<DateTime>(IssueDateProperty); }
            set { SetValue(IssueDateProperty, value); }
        }

        public static readonly PropertyData IssueDateProperty = RegisterProperty("IssueDate", typeof(DateTime));

        #endregion

        #region RegistrationCertificateIssuer property

        public RegistrationCertificateIssuer RegistrationCertificateIssuer
        {
            get { return GetValue<RegistrationCertificateIssuer>(RegistrationCertificateIssuerProperty); }
            set { SetValue(RegistrationCertificateIssuerProperty, value); }
        }

        public static readonly PropertyData RegistrationCertificateIssuerProperty = RegisterProperty("RegistrationCertificateIssuer", typeof (RegistrationCertificateIssuer));

        #endregion

    }
}