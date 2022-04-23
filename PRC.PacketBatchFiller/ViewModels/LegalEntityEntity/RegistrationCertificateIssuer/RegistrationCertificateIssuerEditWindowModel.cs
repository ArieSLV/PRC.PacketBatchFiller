using System.Threading.Tasks;
using Catel.Data;
using Catel.MVVM;

namespace PRC.PacketBatchFiller.ViewModels.LegalEntityEntity.RegistrationCertificateIssuer
{
    public class RegistrationCertificateIssuerEditWindowModel : ViewModelBase
    {
        public RegistrationCertificateIssuerEditWindowModel(Models.LegalEntityEntity.RegistrationCertificateIssuer registrationCertificateIssuer)
        {
            RegistrationCertificateIssuerModel = registrationCertificateIssuer ?? new Models.LegalEntityEntity.RegistrationCertificateIssuer();
        }

        #region Value property

        [ViewModelToModel("RegistrationCertificateIssuerModel")]
        public string Value
        {
            get { return GetValue<string>(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly PropertyData ValueProperty = RegisterProperty("Value", typeof(string));

        #endregion

        #region RegistrationCertificateIssuerModel model property

        [Model]
        public Models.LegalEntityEntity.RegistrationCertificateIssuer RegistrationCertificateIssuerModel
        {
            get { return GetValue<Models.LegalEntityEntity.RegistrationCertificateIssuer>(RegistrationCertificateIssuerModelProperty); }
            private set { SetValue(RegistrationCertificateIssuerModelProperty, value); }
        }

        public static readonly PropertyData RegistrationCertificateIssuerModelProperty = RegisterProperty("RegistrationCertificateIssuerModel", typeof (Models.LegalEntityEntity.RegistrationCertificateIssuer));

        #endregion

        public override string Title => "Орган выдачи ОГРН (свидетельства о регистрации)";

        protected override async Task InitializeAsync() { await base.InitializeAsync(); }
        protected override async Task CloseAsync() { await base.CloseAsync(); }
    }
}