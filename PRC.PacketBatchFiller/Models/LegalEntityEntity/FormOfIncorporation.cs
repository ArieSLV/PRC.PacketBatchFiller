using System.ComponentModel.DataAnnotations.Schema;
using Catel.Data;


namespace PRC.PacketBatchFiller.Models.LegalEntityEntity
{
    [Table("FormOfIncorporations")]
    public class FormOfIncorporation : ModelBase
    {
        public const string DefaultValue = "[ОПФ не выбрана]";
        public const string MainField = "FullForm";

        #region FormOfIncorporationId property

        public long FormOfIncorporationId
        {
            get { return GetValue<long>(FormOfIncorporationIdProperty); }
            set { SetValue(FormOfIncorporationIdProperty, value); }
        }

        public static readonly PropertyData FormOfIncorporationIdProperty = RegisterProperty("FormOfIncorporationId", typeof (long));

        #endregion

        #region ShortForm property
        
        public string ShortForm
        {
            get { return GetValue<string>(ShortFormProperty); }
            set { SetValue(ShortFormProperty, value); }
        }

        public static readonly PropertyData ShortFormProperty = RegisterProperty("ShortForm", typeof (string));

        #endregion

        #region FullForm property

        public string FullForm
        {
            get { return GetValue<string>(FullFormProperty); }
            set { SetValue(FullFormProperty, value); }
        }

        public static readonly PropertyData FullFormProperty = RegisterProperty("FullForm", typeof (string));

        #endregion

        public override string ToString()
        {
            return $"{FullForm} ({ShortForm})";
        }
    }


}