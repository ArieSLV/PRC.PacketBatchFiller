using PRC.PacketBatchFiller.ViewModels.Documents;

namespace PRC.PacketBatchFiller.Views
{
    using ViewModels;

    public partial class AuthorizesDocumentTypeEditWindow
    {
        public AuthorizesDocumentTypeEditWindow()
            : this(null)
        { }

        public AuthorizesDocumentTypeEditWindow(AuthorizesDocumentTypeEditWindowModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
