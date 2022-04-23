using PRC.PacketBatchFiller.ViewModels.Documents;
using PRC.PacketBatchFiller.ViewModels.LegalEntityEntity;

namespace PRC.PacketBatchFiller.Views.Documents
{
    public partial class DocumentPackagesListWindow
    {
        public DocumentPackagesListWindow()
            : this(null)
        { }

        public DocumentPackagesListWindow(DocumentPackagesListWindowModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
