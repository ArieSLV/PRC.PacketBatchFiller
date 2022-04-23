using System.ComponentModel.DataAnnotations.Schema;
using Catel.Data;
using Catel.MVVM;

namespace PRC.PacketBatchFiller.Models.PersonsEntity
{
    [Table("CardIDTypes")]
    public class CardIDType : ModelBase
    {
        #region CardIDTypeId property

        /// <summary>
        /// Gets or sets the CardIDTypeId value.
        /// </summary>
        public long CardIDTypeId
        {
            get { return GetValue<long>(CardIDTypeIdProperty); }
            set { SetValue(CardIDTypeIdProperty, value); }
        }

        /// <summary>
        /// CardIDTypeId property data.
        /// </summary>
        public static readonly PropertyData CardIDTypeIdProperty = RegisterProperty("CardIDTypeId", typeof (long));

        #endregion

        #region Value property

        /// <summary>
        /// Gets or sets the Value value.
        /// </summary>
        public string Value
        {
            get { return GetValue<string>(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Value property data.
        /// </summary>
        public static readonly PropertyData ValueProperty = RegisterProperty("Value", typeof (string));

        #endregion

        public override string ToString()
        {
            return Value;
        }

        public override bool Equals(object obj)
        {
            return (obj as CardIDType)?.CardIDTypeId == CardIDTypeId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}