using System.ComponentModel.DataAnnotations.Schema;
using Catel.Data;
using PRC.PacketBatchFiller.Extensions;

namespace PRC.PacketBatchFiller.Models.LegalEntityEntity
{
    public enum SecuritiesTypes
    {
        [StringValue("Акция обыкновенная именная")]
        SimpleShare,

        [StringValue("Акция привилегированная именная типа А")]
        PreferredTypaAShare,

        [StringValue("Акция привилегированная именная")]
        PreferredShare,

        Unknown,
    }

    [Table("IssuesOfSecurities")]
    public class IssueOfSecurities : ModelBase
    {
        #region IssueOfSecuritiesId property

        public long IssueOfSecuritiesId
        {
            get { return GetValue<long>(IssueOfSecuritiesIdProperty); }
            set { SetValue(IssueOfSecuritiesIdProperty, value); }
        }
        
        public static readonly PropertyData IssueOfSecuritiesIdProperty = RegisterProperty("IssueOfSecuritiesId", typeof (long));

        #endregion

        #region Type property

        public SecuritiesTypes Type
        {
            get { return GetValue<SecuritiesTypes>(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly PropertyData TypeProperty = RegisterProperty("Type", typeof (SecuritiesTypes));

        #endregion

        #region Number property

        public string Number
        {
            get { return GetValue<string>(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly PropertyData NumberProperty = RegisterProperty("Number", typeof (string));

        #endregion
    }
}