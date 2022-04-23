using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Text;
using Catel.Data;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;

namespace PRC.PacketBatchFiller.Models.PersonsEntity
{
    [Serializable]
    [Table("Persons")]
    public class Person : Unit
    {
        public new const string DefaultValue = "[Лицо не выбрано]";
        public new const string MainField = "FullName";

        #region LastName property

        public string LastName
        {
            get { return GetValue<string>(LastNameProperty); }
            set { SetValue(LastNameProperty, value); }
        }

        public static readonly PropertyData LastNameProperty = RegisterProperty("LastName", typeof (string));

        #endregion

        #region FirstName property

        public string FirstName
        {
            get { return GetValue<string>(FirstNameProperty); }
            set { SetValue(FirstNameProperty, value); }
        }

        public static readonly PropertyData FirstNameProperty = RegisterProperty("FirstName", typeof (string));

        #endregion

        #region MiddleName property

        public string MiddleName
        {
            get { return GetValue<string>(MiddleNameProperty); }
            set { SetValue(MiddleNameProperty, value); }
        }

        public static readonly PropertyData MiddleNameProperty = RegisterProperty("MiddleName", typeof (string));

        #endregion

        #region DateOfBirth property

        public DateTime? DateOfBirth
        {
            get { return GetValue<DateTime?>(DateOfBirthProperty); }
            set { SetValue(DateOfBirthProperty, value); }
        }

        public static readonly PropertyData DateOfBirthProperty = RegisterProperty("DateOfBirth", typeof (DateTime?));

        #endregion

        #region IsOneOfPODFTFlag property

        public bool IsOneOfPODFTFlag
        {
            get { return GetValue<bool>(IsOneOfPODFTFlagProperty); }
            set { SetValue(IsOneOfPODFTFlagProperty, value); }
        }
        
        public static readonly PropertyData IsOneOfPODFTFlagProperty = RegisterProperty("IsOneOfPODFTFlag",
            typeof (bool));

        #endregion

        #region GotBeneficialOwnerFlag property
        
        public bool GotBeneficialOwnerFlag
        {
            get { return GetValue<bool>(GotBeneficialOwnerFlagProperty); }
            set { SetValue(GotBeneficialOwnerFlagProperty, value); }
        }
        
        public static readonly PropertyData GotBeneficialOwnerFlagProperty = RegisterProperty("GotBeneficialOwnerFlag",
            typeof (bool));

        #endregion

        #region IsHeadNonComercialCompanyFlag property
        
        public bool IsHeadNonComercialCompanyFlag
        {
            get { return GetValue<bool>(IsHeadNonComercialCompanyFlagProperty); }
            set { SetValue(IsHeadNonComercialCompanyFlagProperty, value); }
        }
        
        public static readonly PropertyData IsHeadNonComercialCompanyFlagProperty = RegisterProperty("IsHeadNonComercialCompanyFlag", typeof (bool));

        #endregion

        #region CardID property
        
        public CardID CardID
        {
            get { return GetValue<CardID>(CardIDProperty); }
            set { SetValue(CardIDProperty, value); }
        }

        public static readonly PropertyData CardIDProperty = RegisterProperty("CardID", typeof (CardID));

        #endregion

        #region PlaceOfBirth property
        
        public PlaceOfBirth PlaceOfBirth
        {
            get { return GetValue<PlaceOfBirth>(PlaceOfBirthProperty); }
            set { SetValue(PlaceOfBirthProperty, value); }
        }
        
        public static readonly PropertyData PlaceOfBirthProperty = RegisterProperty("PlaceOfBirth", typeof (PlaceOfBirth));

        #endregion

        public override string ToString()
        {
            var stringToReturn = new StringBuilder();

            stringToReturn.Append(!string.IsNullOrWhiteSpace(FullName) ? FullName : "[Имя не указано]");
            if (DateOfBirth != null) stringToReturn.Append($", {DateOfBirth:dd.MM.yyyy}г.р.");
            if (!string.IsNullOrWhiteSpace(CardID?.Series) || !string.IsNullOrWhiteSpace(CardID?.Number)) stringToReturn.Append(",");
            if (!string.IsNullOrWhiteSpace(CardID?.Series)) stringToReturn.Append($" {CardID.Series}");
            if (!string.IsNullOrWhiteSpace(CardID?.Number)) stringToReturn.Append($" {CardID.Number}");

            return stringToReturn.ToString();
        }


    }

    public class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            HasOptional(one => one.LastName);
            HasOptional(one => one.FirstName);
            HasOptional(one => one.MiddleName);
            HasOptional(one => one.CardID);
            HasOptional(one => one.PlaceOfBirth);
        }
    }
}