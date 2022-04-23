using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Catel.Data;
using PRC.PacketBatchFiller.Extensions;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.Models.LegalEntityEntity;

namespace PRC.PacketBatchFiller.Models.BaseClasses
{
    public enum ShareholderAccountType
    {
        [StringValue("Владелец")]
        Owner,

        [StringValue("Номинальный держатель")]
        Nominee,

        [StringValue("Доверительный управляющий")]
        Trustee,

        [StringValue("Депозитный")]
        Deposit,

        [StringValue("Казначейский")]
        Treasury,

        [StringValue("Центральный депозитарий")]
        CentralDepository
    }

    [Serializable]
    [Table("ShareholderAccount")]
    public class ShareholderAccount : ModelBase
    {
        public const string DefaultValue = "[лицевой счет не выбран]";
        public const string MainField = "Number";


        #region ShareholderAccountId property

        public long ShareholderAccountId
        {
            get { return GetValue<long>(ShareholderAccountIdProperty); }
            set { SetValue(ShareholderAccountIdProperty, value); }
        }

        public static readonly PropertyData ShareholderAccountIdProperty = RegisterProperty("ShareholderAccountId", typeof (long));

        #endregion

        #region Number property

        public string Number
        {
            get { return GetValue<string>(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly PropertyData NumberProperty = RegisterProperty("Number", typeof (string), "");

        #endregion

        #region SecuritiesIssuer property

        public virtual LegalEntity SecuritiesIssuer
        {
            get { return GetValue<LegalEntity>(SecuritiesIssuerProperty); }
            set { SetValue(SecuritiesIssuerProperty, value); }
        }

        public static readonly PropertyData SecuritiesIssuerProperty = RegisterProperty("SecuritiesIssuer", typeof (LegalEntity));

        #endregion

        #region ShareholderAccountType property

        public ShareholderAccountType ShareholderAccountType
        {
            get { return GetValue<ShareholderAccountType>(ShareholderAccountTypeProperty); }
            set { SetValue(ShareholderAccountTypeProperty, value); }
        }

        public static readonly PropertyData ShareholderAccountTypeProperty = RegisterProperty("ShareholderAccountType", typeof (ShareholderAccountType));

        #endregion


        #region Unit property

        public virtual Unit Unit
        {
            get { return GetValue<Unit>(UnitProperty); }
            set { SetValue(UnitProperty, value); }
        }

        public static readonly PropertyData UnitProperty = RegisterProperty("Unit", typeof (Unit));

        #endregion


        public override string ToString()
        {
            var stringToReturn = new StringBuilder();

            var unitToReturn = Unit?.ToString() ?? string.Empty;
            var numberToReturn = Number ?? string.Empty;
            var siToReturn = SecuritiesIssuer != null ? SecuritiesIssuer.ShortName : string.Empty;

            if (!string.IsNullOrWhiteSpace(unitToReturn))       stringToReturn.Append($"{unitToReturn}");
            if (!string.IsNullOrWhiteSpace(numberToReturn)  &&
                !string.IsNullOrWhiteSpace(siToReturn)      &&
                !string.IsNullOrWhiteSpace(unitToReturn))       stringToReturn.Append(" (");
            if (!string.IsNullOrWhiteSpace(numberToReturn))     stringToReturn.Append(numberToReturn);
            if (numberToReturn != "[лицевой счет не выбран]")   stringToReturn.Append($", {StringEnum.GetStringValue(ShareholderAccountType)}");
            if (!string.IsNullOrWhiteSpace(siToReturn))         stringToReturn.Append($", {siToReturn}");
            if (!string.IsNullOrWhiteSpace(numberToReturn)  && 
                !string.IsNullOrWhiteSpace(siToReturn)      &&
                !string.IsNullOrWhiteSpace(unitToReturn))       stringToReturn.Append(")");


            return stringToReturn.ToString();
        }
    }
}