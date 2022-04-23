using System.ComponentModel.DataAnnotations.Schema;
using Catel.Data;

namespace PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity
{
    [Table("PhoneNumbers")]
    public class PhoneNumber : ModelBase
    {
        #region PhoneNumberId property
        
        public long PhoneNumberId
        {
            get { return GetValue<long>(PhoneNumberIdProperty); }
            set { SetValue(PhoneNumberIdProperty, value); }
        }

        public static readonly PropertyData PhoneNumberIdProperty = RegisterProperty("PhoneNumberId", typeof (long));

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

        public string Comment
        {
            get { return GetValue<string>(CommentProperty); }
            set { SetValue(CommentProperty, value); }
        }
        
        public static readonly PropertyData CommentProperty = RegisterProperty("Comment", typeof(string));

        #endregion

    }
}