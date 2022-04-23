using Catel.Data;
using Catel.Services;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.BaseClasses;
using PRC.PacketBatchFiller.Views.LegalEntity.RegistrationCertificateIssuer;

namespace PRC.PacketBatchFiller.ViewModels.LegalEntityEntity.RegistrationCertificateIssuer
{
    public class RegistrationCertificateIssuerViewModel : SuggestFromLocalDataViewModelBase<Models.LegalEntityEntity.RegistrationCertificateIssuer, RegistrationCertificateIssuerEditWindowModel, RegistrationCertificateIssuerEditWindow>
    {
        public RegistrationCertificateIssuerViewModel(
            string labelText,
            Models.LegalEntityEntity.RegistrationCertificateIssuer registrationCertificateIssuer, 
            IUIVisualizerService uiVisualizerService,
            IUnitService unitService
            )
            : base (
                  registrationCertificateIssuer, 
                  uiVisualizerService, unitService
                  )
        {
            LabelText = labelText;
        }

        #region LabelText property

        public string LabelText
        {
            get { return GetValue<string>(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        public static readonly PropertyData LabelTextProperty = RegisterProperty("LabelText", typeof(string));

        #endregion
    }


}