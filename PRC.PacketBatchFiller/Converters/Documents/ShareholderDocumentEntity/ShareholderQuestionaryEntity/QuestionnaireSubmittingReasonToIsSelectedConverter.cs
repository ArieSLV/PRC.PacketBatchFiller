using System;
using System.Globalization;
using System.Windows.Data;
using PRC.PacketBatchFiller.Models.Documents.ShareholderDocuments;

namespace PRC.PacketBatchFiller.Converters.Documents.ShareholderDocumentEntity.ShareholderQuestionaryEntity
{
    [ValueConversion(typeof(QuestionnaireSubmittingReason), typeof(bool), ParameterType = typeof(QuestionnaireSubmittingReason))]
    public class QuestionnaireSubmittingReasonToIsSelectedConverter : ConvertorBase<QuestionnaireSubmittingReasonToIsSelectedConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is QuestionnaireSubmittingReason)) return false;

            var reasonRepresented = (QuestionnaireSubmittingReason) parameter;
            var reason = (QuestionnaireSubmittingReason) value;

            return reasonRepresented == reason;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var reasonRepresented = (QuestionnaireSubmittingReason) parameter;
            var isChecked = false;
            if (value is bool) isChecked = (bool) value;
            else if (value is bool?) isChecked = ((bool?) value).HasValue ? ((bool?) value).Value : false;

            return isChecked ? reasonRepresented : Binding.DoNothing;
        }
    }
}