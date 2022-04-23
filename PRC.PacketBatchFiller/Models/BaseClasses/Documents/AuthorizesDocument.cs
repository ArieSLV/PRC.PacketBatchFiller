using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Catel.Data;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;

namespace PRC.PacketBatchFiller.Models.BaseClasses.Documents
{
    [Serializable]
    [Table("AuthorizesDocuments")]
    public class AuthorizesDocument : Document
    {
        #region AuthorizedUnits property

        public ObservableCollection<Unit> AuthorizedUnits
        {
            get { return GetValue<ObservableCollection<Unit>>(AuthorizedUnitsProperty); }
            set { SetValue(AuthorizedUnitsProperty, value); }
        }

        public static readonly PropertyData AuthorizedUnitsProperty = RegisterProperty("AuthorizedUnits", typeof (ObservableCollection<Unit>));

        #endregion

        #region Number property

        public string Number
        {
            get { return GetValue<string>(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly PropertyData NumberProperty = RegisterProperty("Number", typeof (string));

        #endregion

        #region StartDate property

        public DateTime? StartDate
        {
            get { return GetValue<DateTime?>(StartDateProperty); }
            set { SetValue(StartDateProperty, value); }
        }

        public static readonly PropertyData StartDateProperty = RegisterProperty("StartDate", typeof (DateTime?));

        #endregion

        #region EndDate property

        public DateTime? EndDate
        {
            get { return GetValue<DateTime?>(EndDateProperty); }
            set { SetValue(EndDateProperty, value); }
        }

        public static readonly PropertyData EndDateProperty = RegisterProperty("EndDate", typeof (DateTime?));

        #endregion

        #region AuthorizesDocumentType property

        public virtual AuthorizesDocumentType AuthorizesDocumentType
        {
            get { return GetValue<AuthorizesDocumentType>(AuthorizesDocumentTypeProperty); }
            set { SetValue(AuthorizesDocumentTypeProperty, value); }
        }

        public static readonly PropertyData AuthorizesDocumentTypeProperty = RegisterProperty("AuthorizesDocumentType", typeof (AuthorizesDocumentType));

        #endregion
    }

    public class AuthorizesDocumentMap : EntityTypeConfiguration<AuthorizesDocument>
    {
        public AuthorizesDocumentMap()
        {
            HasOptional(one => one.AuthorizesDocumentType);
        }
    }
}