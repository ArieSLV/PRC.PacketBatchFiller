using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Catel.Data;
using PRC.PacketBatchFiller.Models.BaseClasses.Documents;

namespace PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity
{

    #region Enums

    public enum DividentsPaymentWays
    {
        ByMail,
        ByBank,
        Unknow
    }

    #endregion

    [Table("Units")]
    public class Unit : ModelBase
    {
        public const string DefaultValue = "[Лицо не выбрано]";
        public const string MainField = "FullName";

        #region UnitId property

        public long UnitId
        {
            get { return GetValue<long>(UnitIdProperty); }
            set { SetValue(UnitIdProperty, value); }
        }

        public static readonly PropertyData UnitIdProperty = RegisterProperty("UnitId", typeof (long));

        #endregion

        #region DividentsPaymentWay property

        public DividentsPaymentWays DividentsPaymentWay
        {
            get { return GetValue<DividentsPaymentWays>(DividentsPaymentWayProperty); }
            set { SetValue(DividentsPaymentWayProperty, value); }
        }
        
        public static readonly PropertyData DividentsPaymentWayProperty = RegisterProperty("DividentsPaymentWay",
            typeof (DividentsPaymentWays), DividentsPaymentWays.ByMail);

        #endregion

        #region OnlyPersonalPresenceFlag property

        public bool OnlyPersonalPresenceFlag
        {
            get { return GetValue<bool>(OnlyPersonalPresenceFlagProperty); }
            set { SetValue(OnlyPersonalPresenceFlagProperty, value); }
        }

        public static readonly PropertyData OnlyPersonalPresenceFlagProperty =
            RegisterProperty("OnlyPersonalPresenceFlag", typeof (bool), true);

        #endregion

        #region INN property

        public string INN
        {
            get { return GetValue<string>(INNProperty); }
            set { SetValue(INNProperty, value); }
        }

        public static readonly PropertyData INNProperty = RegisterProperty("INN", typeof (string));

        #endregion

        #region MailingAddressEqualRegistrationAddressFlag property
        
        public bool MailingAddressEqualRegistrationAddressFlag
        {
            get { return GetValue<bool>(MailingAddressEqualRegistrationAddressFlagProperty); }
            set { SetValue(MailingAddressEqualRegistrationAddressFlagProperty, value); }
        }
        
        public static readonly PropertyData MailingAddressEqualRegistrationAddressFlagProperty =
            RegisterProperty("MailingAddressEqualRegistrationAddressFlag", typeof (bool), true);

        #endregion

        #region Citizenship property
        
        public Citizenship Citizenship
        {
            get { return GetValue<Citizenship>(CitizenshipProperty); }
            set { SetValue(CitizenshipProperty, value); }
        }

        public static readonly PropertyData CitizenshipProperty = RegisterProperty("Citizenship", typeof (Citizenship));

        #endregion

        #region AddressRegistration property
        
        public Address AddressRegistration
        {
            get { return GetValue<Address>(AddressRegistrationProperty); }
            set { SetValue(AddressRegistrationProperty, value); }
        }

        public static readonly PropertyData AddressRegistrationProperty = RegisterProperty("AddressRegistration",
            typeof (Address));

        #endregion

        #region MailingAddress property
        
        public Address MailingAddress
        {
            get { return GetValue<Address>(MailingAddressProperty); }
            set { SetValue(MailingAddressProperty, value); }
        }
        
        public static readonly PropertyData MailingAddressProperty = RegisterProperty("MailingAddress", typeof (Address));

        #endregion

        #region PhoneNumbers property
        
        public virtual ObservableCollection<PhoneNumber> PhoneNumbers
        {
            get { return GetValue<ObservableCollection<PhoneNumber>>(PhoneNumbersProperty); }
            set { SetValue(PhoneNumbersProperty, value); }
        }

        public static readonly PropertyData PhoneNumbersProperty = RegisterProperty("PhoneNumbers",
            typeof (ObservableCollection<PhoneNumber>));

        #endregion
        
        #region Emails property
        
        public virtual ObservableCollection<Email> Emails
        {
            get { return GetValue<ObservableCollection<Email>>(EmailsProperty); }
            set { SetValue(EmailsProperty, value); }
        }
        
        public static readonly PropertyData EmailsProperty = RegisterProperty("Emails",
            typeof (ObservableCollection<Email>));

        #endregion

        #region BankDetails property
        
        public BankDetails BankDetails
        {
            get { return GetValue<BankDetails>(BankDetailsProperty); }
            set { SetValue(BankDetailsProperty, value); }
        }
        
        public static readonly PropertyData BankDetailsProperty = RegisterProperty("BankDetails", typeof (BankDetails));

        #endregion

        #region ShareholderAccounts property

        public ObservableCollection<ShareholderAccount> ShareholderAccounts
        {
            get { return GetValue<ObservableCollection<ShareholderAccount>>(ShareholderAccountsProperty); }
            set { SetValue(ShareholderAccountsProperty, value); }
        }

        public static readonly PropertyData ShareholderAccountsProperty = RegisterProperty("ShareholderAccounts", typeof (ObservableCollection<ShareholderAccount>));

        #endregion

        #region RoleIsShareHolderFlag property

        public bool RoleIsShareHolderFlag
        {
            get { return GetValue<bool>(RoleIsShareHolderFlagProperty); }
            set { SetValue(RoleIsShareHolderFlagProperty, value); }
        }

        public static readonly PropertyData RoleIsShareHolderFlagProperty = RegisterProperty("RoleIsShareHolderFlag", typeof(bool));

        #endregion

        #region RoleIsFirstPersonOfTheCompany property

        public bool RoleIsFirstPersonOfTheCompany
        {
            get { return GetValue<bool>(RoleIsFirstPersonOfTheCompanyProperty); }
            set { SetValue(RoleIsFirstPersonOfTheCompanyProperty, value); }
        }

        public static readonly PropertyData RoleIsFirstPersonOfTheCompanyProperty = RegisterProperty("RoleIsFirstPersonOfTheCompany", typeof (bool));

        #endregion

        
        #region TimeStamp property

        public DateTime? TimeStamp
        {
            get { return GetValue<DateTime>(TimeStampProperty); }
            set { SetValue(TimeStampProperty, value); }
        }
        
        public static readonly PropertyData TimeStampProperty = RegisterProperty("TimeStamp", typeof (DateTime),
            DateTime.Now);

        #endregion

        #region FullName property

        public string FullName
        {
            get { return GetValue<string>(FullNameProperty); }
            set { SetValue(FullNameProperty, value); }
        }

        public static readonly PropertyData FullNameProperty = RegisterProperty("FullName", typeof (string));

        #endregion

        //#region AuthorizedDocumentsCollection property

        //public ObservableCollection<AuthorizesDocument> AuthorizedDocumentsCollection
        //{
        //    get { return GetValue<ObservableCollection<AuthorizesDocument>>(AuthorizedDocumentsCollectionProperty); }
        //    set { SetValue(AuthorizedDocumentsCollectionProperty, value); }
        //}

        //public static readonly PropertyData AuthorizedDocumentsCollectionProperty = RegisterProperty("AuthorizedDocumentsCollection", typeof(ObservableCollection<AuthorizesDocument>));

        //#endregion
    }

    public class UnitMap : EntityTypeConfiguration<Unit>
    {
        public UnitMap()
        {
            HasOptional(o => o.Citizenship);
        }
    }
}
