using PRC.PacketBatchFiller.ViewModels.LegalEntityEntity.RegistrationCertificateIssuer;

namespace PRC.PacketBatchFiller.Views.LegalEntity.RegistrationCertificateIssuer
{
    public partial class RegistrationCertificateIssuerEditWindow
    {
        public RegistrationCertificateIssuerEditWindow()
            : this(null)
        { }

        public RegistrationCertificateIssuerEditWindow(RegistrationCertificateIssuerEditWindowModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
