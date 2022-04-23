using System;
using System.ComponentModel.DataAnnotations.Schema;
using Catel.Data;
using PRC.PacketBatchFiller.Models.BaseClasses;
using PRC.PacketBatchFiller.Models.BaseClasses.Documents;

namespace PRC.PacketBatchFiller.Models.Documents.ShareholderDocuments
{
    [Serializable]
    [Table("ShareholderAuthorizesDocuments")]
    public class ShareholderAuthorizesDocument : AuthorizesDocument
    {
        public const string FormName = "Документ, устанавливающий полномочия";

        #region WhoGivingAuthority property

        public virtual ShareholderAccount WhoGivingAuthority
        {
            get { return GetValue<ShareholderAccount>(WhoGivingAuthorityProperty); }
            set { SetValue(WhoGivingAuthorityProperty, value); }
        }

        public static readonly PropertyData WhoGivingAuthorityProperty = RegisterProperty("WhoGivingAuthority", typeof (ShareholderAccount));

        #endregion
    }
}