using System;
using System.ComponentModel.DataAnnotations.Schema;
using Catel.Data;
using PRC.PacketBatchFiller.Models.BaseClasses.Documents;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.Models.PersonsEntity;

namespace PRC.PacketBatchFiller.Models.BaseClasses
{
    [Serializable]
    [Table("Documents")]
    public class Document : ModelBase
    {
        #region DocumentId property

        public long DocumentId
        {
            get { return GetValue<long>(DocumentIdProperty); }
            set { SetValue(DocumentIdProperty, value); }
        }

        public static readonly PropertyData DocumentIdProperty = RegisterProperty("DocumentId", typeof (long));

        #endregion

        #region TimeStamp property

        public DateTime? TimeStamp
        {
            get { return GetValue<DateTime>(TimeStampProperty); }
            set { SetValue(TimeStampProperty, value); }
        }

        public static readonly PropertyData TimeStampProperty = RegisterProperty("TimeStamp", typeof(DateTime),
            DateTime.Now);

        #endregion

        #region Signatory property

        public virtual Unit Signatory
        {
            get { return GetValue<Unit>(SignatoryProperty); }
            set { SetValue(SignatoryProperty, value); }
        }

        public static readonly PropertyData SignatoryProperty = RegisterProperty("Signatory", typeof(Unit));

        #endregion

        #region SignedByAuthorizesDocument property

        public virtual AuthorizesDocument SignedByAuthorizesDocument
        {
            get { return GetValue<AuthorizesDocument>(SignedByAuthorizesDocumentProperty); }
            set { SetValue(SignedByAuthorizesDocumentProperty, value); }
        }

        public static readonly PropertyData SignedByAuthorizesDocumentProperty = RegisterProperty("SignedByAuthorizesDocument", typeof (AuthorizesDocument));

        #endregion
    }
}