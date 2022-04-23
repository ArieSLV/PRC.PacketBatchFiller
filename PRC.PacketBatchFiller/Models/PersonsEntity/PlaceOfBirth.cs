using System.ComponentModel.DataAnnotations.Schema;
using Catel.Data;

namespace PRC.PacketBatchFiller.Models.PersonsEntity
{
    [Table("PlaceOfBirths")]
    public class PlaceOfBirth : ModelBase
    {
        public const string DefaultValue = "[место рождения не выбрано]";
        public const string MainField = "Value";

        #region PlaceOfBirthId property

        /// <summary>
        ///     Gets or sets the PlaceOfBirthId value.
        /// </summary>
        public long PlaceOfBirthId
        {
            get { return GetValue<long>(PlaceOfBirthIdProperty); }
            set { SetValue(PlaceOfBirthIdProperty, value); }
        }

        /// <summary>
        ///     PlaceOfBirthId property data.
        /// </summary>
        public static readonly PropertyData PlaceOfBirthIdProperty = RegisterProperty("PlaceOfBirthId", typeof (long));

        #endregion

        #region Value property

        /// <summary>
        ///     Gets or sets the Value value.
        /// </summary>
        public string Value
        {
            get { return GetValue<string>(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        ///     Value property data.
        /// </summary>
        public static readonly PropertyData ValueProperty = RegisterProperty("Value", typeof (string));

        #endregion

        public override string ToString()
        {
            return Value;
        }
    }
}