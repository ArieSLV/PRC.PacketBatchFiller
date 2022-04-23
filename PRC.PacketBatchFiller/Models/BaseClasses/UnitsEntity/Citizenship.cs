using System.ComponentModel.DataAnnotations.Schema;
using Catel.Data;

namespace PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity
{
    [Table("Citizenships")]
    public class Citizenship : ModelBase
    {
        public const string DefaultValue = "[Страна не выбрана]";
        public const string MainField = "Value";

        #region CitizenshipId property

        /// <summary>
        ///     Gets or sets the CitizenshipId value.
        /// </summary>
        public long CitizenshipId
        {
            get { return GetValue<long>(CitizenshipIdProperty); }
            set { SetValue(CitizenshipIdProperty, value); }
        }

        /// <summary>
        ///     CitizenshipId property data.
        /// </summary>
        public static readonly PropertyData CitizenshipIdProperty = RegisterProperty("CitizenshipId", typeof (long));

        #endregion

        #region CitizenshipValue property

        public string Value
        {
            get { return GetValue<string>(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        
        public static readonly PropertyData ValueProperty = RegisterProperty("Value",
            typeof (string));

        #endregion

        public override string ToString()
        {
            return Value;
        }
    }
}