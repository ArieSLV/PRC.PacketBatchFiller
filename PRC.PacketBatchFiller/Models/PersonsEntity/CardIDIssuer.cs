using System.ComponentModel.DataAnnotations.Schema;
using Catel.Data;

namespace PRC.PacketBatchFiller.Models.PersonsEntity
{
   
    [Table("CardIDIssuers")]
    public class CardIDIssuer : ModelBase
    {
        public const string DefaultValue = "[орган выдачи паспорта не выбран]";
        public const string MainField = "Name";

        #region CardIDIssuerNameId property

        public long CardIDIssuerId
        {
            get { return GetValue<long>(CardIDIssuerIdProperty); }
            set { SetValue(CardIDIssuerIdProperty, value); }
        }

        public static readonly PropertyData CardIDIssuerIdProperty = RegisterProperty("CardIDIssuerId", typeof (long));

        #endregion

        #region Name property

        /// <summary>
        ///     Gets or sets the Value value.
        /// </summary>
        public string Name
        {
            get { return GetValue<string>(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        /// <summary>
        ///     Value property data.
        /// </summary>
        public static readonly PropertyData NameProperty = RegisterProperty("Name", typeof (string));

        #endregion

        #region Code property

        /// <summary>
        ///     Gets or sets the CardIDIssuerCode value.
        /// </summary>
        public string Code
        {
            get { return GetValue<string>(CodeProperty); }
            set { SetValue(CodeProperty, value); }
        }

        /// <summary>
        ///     CardIDIssuerCode property data.
        /// </summary>
        public static readonly PropertyData CodeProperty = RegisterProperty("Code", typeof (string));

        #endregion

        public override string ToString()
        {
            return string.IsNullOrEmpty(Code) ? Name : $"{Name}, {Code}";
        }
    }
}