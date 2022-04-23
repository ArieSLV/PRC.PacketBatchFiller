using System;
using System.ComponentModel.DataAnnotations.Schema;
using Catel.Data;

namespace PRC.PacketBatchFiller.Models.PersonsEntity
{
    [Table("CardID")]
    public class CardID : ModelBase
    {
        #region CardIDId property

        /// <summary>
        ///     Gets or sets the CardIDId value.
        /// </summary>
        public long CardIDId
        {
            get { return GetValue<long>(CardIDIdProperty); }
            set { SetValue(CardIDIdProperty, value); }
        }

        /// <summary>
        ///     CardIDId property data.
        /// </summary>
        public static readonly PropertyData CardIDIdProperty = RegisterProperty("CardIDId", typeof (long));

        #endregion

        #region CardIDType property

        /// <summary>
        /// Gets or sets the CardIDType value.
        /// </summary>
        public CardIDType CardIDType
        {
            get { return GetValue<CardIDType>(CardIDTypeProperty); }
            set { SetValue(CardIDTypeProperty, value); }
        }

        /// <summary>
        /// CardIDType property data.
        /// </summary>
        public static readonly PropertyData CardIDTypeProperty = RegisterProperty("CardIDType", typeof (CardIDType));

        #endregion

        #region Series property

        /// <summary>
        ///     Gets or sets the Series value.
        /// </summary>
        public string Series
        {
            get { return GetValue<string>(SeriesProperty); }
            set { SetValue(SeriesProperty, value); }
        }

        /// <summary>
        ///     Series property data.
        /// </summary>
        public static readonly PropertyData SeriesProperty = RegisterProperty("Series", typeof (string));

        #endregion

        #region Number property

        public string Number
        {
            get { return GetValue<string>(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly PropertyData NumberProperty = RegisterProperty("Number", typeof (string));

        #endregion

        #region IssueDate property

        public DateTime? IssueDate
        {
            get { return GetValue<DateTime>(IssueDateProperty); }
            set { SetValue(IssueDateProperty, value); }
        }

        public static readonly PropertyData IssueDateProperty = RegisterProperty("IssueDate", typeof (DateTime));

        #endregion

        #region CardIDIssuer property

        public CardIDIssuer CardIDIssuer
        {
            get { return GetValue<CardIDIssuer>(CardIDIssuerProperty); }
            set { SetValue(CardIDIssuerProperty, value); }
        }

        public static readonly PropertyData CardIDIssuerProperty = RegisterProperty("CardIDIssuer",
            typeof (CardIDIssuer));

        #endregion
    }
}