using System.ComponentModel.DataAnnotations.Schema;
using Catel.Data;

namespace PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity
{
    
    [Table("Emails")]
    public class Email : ModelBase
    {
        #region EmailId property

        /// <summary>
        ///     Gets or sets the EmailId value.
        /// </summary>
        public long EmailId
        {
            get { return GetValue<long>(EmailIdProperty); }
            set { SetValue(EmailIdProperty, value); }
        }

        /// <summary>
        ///     EmailId property data.
        /// </summary>
        public static readonly PropertyData EmailIdProperty = RegisterProperty("EmailId", typeof (long));

        #endregion

        #region Value property

        public string Value
        {
            get { return GetValue<string>(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        
        public static readonly PropertyData ValueProperty = RegisterProperty("Value", typeof (string));

        #endregion

        #region Type property

        public ContactType Type
        {
            get { return GetValue<ContactType>(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        
        public static readonly PropertyData TypeProperty = RegisterProperty("Type", typeof (ContactType), ContactType.Work);

        #endregion

        #region Comment property

        /// <summary>
        /// Gets or sets the Comment value.
        /// </summary>
        public string Comment
        {
            get { return GetValue<string>(CommentProperty); }
            set { SetValue(CommentProperty, value); }
        }

        /// <summary>
        /// Comment property data.
        /// </summary>
        public static readonly PropertyData CommentProperty = RegisterProperty("Comment", typeof (string));

        #endregion
    }
}