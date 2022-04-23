using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Text;
using Catel.Data;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;

namespace PRC.PacketBatchFiller.Models.LegalEntityEntity
{
    [Serializable]
    [Table("LegalEntities")]
    public class LegalEntity : Unit
    {
        public new const string DefaultValue = "[Лицо не выбрано]";
        public new const string MainField = "FullName";

        #region KPP property

        public string KPP
        {
            get { return GetValue<string>(KPPProperty); }
            set { SetValue(KPPProperty, value); }
        }

        public static readonly PropertyData KPPProperty = RegisterProperty("KPP", typeof (string));

        #endregion

        #region OKPO property

        public string OKPO
        {
            get { return GetValue<string>(OKPOProperty); }
            set { SetValue(OKPOProperty, value); }
        }

        public static readonly PropertyData OKPOProperty = RegisterProperty("OKPO", typeof (string));

        #endregion

        #region OKVED property

        public string OKVED
        {
            get { return GetValue<string>(OKVEDProperty); }
            set { SetValue(OKVEDProperty, value); }
        }

        public static readonly PropertyData OKVEDProperty = RegisterProperty("OKVED", typeof (string));

        #endregion

        #region RegistrationCertificate property

        public virtual RegistrationCertificate RegistrationCertificate
        {
            get { return GetValue<RegistrationCertificate>(RegistrationCertificateProperty); }
            set { SetValue(RegistrationCertificateProperty, value); }
        }
        
        public static readonly PropertyData RegistrationCertificateProperty = RegisterProperty("RegistrationCertificate", typeof (RegistrationCertificate));

        #endregion

        #region FirstPersonOfCompanyViewModel property

        public Unit FirstPersonOfCompany
        {
            get { return GetValue<Unit>(FirstPersonOfCompanyProperty); }
            set { SetValue(FirstPersonOfCompanyProperty, value); }
        }

        public static readonly PropertyData FirstPersonOfCompanyProperty = RegisterProperty("FirstPersonOfCompanyViewModel", typeof (Unit));

        #endregion

        #region RoleIsSecuritiesIssuerFlag property

        public bool RoleIsSecuritiesIssuerFlag
        {
            get { return GetValue<bool>(RoleIsSecuritiesIssuerFlagProperty); }
            set { SetValue(RoleIsSecuritiesIssuerFlagProperty, value); }
        }

        public static readonly PropertyData RoleIsSecuritiesIssuerFlagProperty = RegisterProperty("RoleIsSecuritiesIssuerFlag", typeof (bool));

        #endregion

        #region FormOfIncorporation property

        public FormOfIncorporation FormOfIncorporation
        {
            get { return GetValue<FormOfIncorporation>(FormOfIncorporationProperty); }
            set { SetValue(FormOfIncorporationProperty, value); }
        }

        public static readonly PropertyData FormOfIncorporationProperty = RegisterProperty("FormOfIncorporation", typeof (FormOfIncorporation));

        #endregion

        #region ShortName property

        public string ShortName
        {
            get { return GetValue<string>(ShortNameProperty); }
            set { SetValue(ShortNameProperty, value); }
        }

        public static readonly PropertyData ShortNameProperty = RegisterProperty("ShortName", typeof (string));

        #endregion

        #region IssuesOfSecurities property

        public ObservableCollection<IssueOfSecurities> IssuesOfSecurities
        {
            get { return GetValue<ObservableCollection<IssueOfSecurities>>(IssuesOfSecuritiesProperty); }
            set { SetValue(IssuesOfSecuritiesProperty, value); }
        }

        public static readonly PropertyData IssuesOfSecuritiesProperty = RegisterProperty("IssuesOfSecurities", typeof (ObservableCollection<IssueOfSecurities>));

        #endregion
        
        public override string ToString()
        {
            var stringToReturn = new StringBuilder();

            stringToReturn.Append(!string.IsNullOrWhiteSpace(FullName) ? FullName : "[Полное наименование не указано]");
            if (!string.IsNullOrWhiteSpace(ShortName)) stringToReturn.Append($" ({ShortName})");
            if (!string.IsNullOrWhiteSpace(RegistrationCertificate?.Number)) stringToReturn.Append($", {RegistrationCertificate.Number}");
            
            return stringToReturn.ToString();
        }
    }

    public class LegalEntityMap : EntityTypeConfiguration<LegalEntity>
    {
        public LegalEntityMap()
        {
            HasOptional(one => one.RegistrationCertificate);
            HasOptional(one => one.KPP);
            HasOptional(one => one.OKPO);
            HasOptional(one => one.OKVED);
            HasOptional(one => one.FirstPersonOfCompany);
        }
    }
}