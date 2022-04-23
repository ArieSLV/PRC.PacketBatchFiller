using PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity;

namespace PRC.PacketBatchFiller.Views.Documents.ShareholderDocument
{
    using Catel.Windows;
    using ViewModels;

    public partial class ShareholderDocumentPackageWindow
    {
        public ShareholderDocumentPackageWindow()
            : this(null)
        { }

        public ShareholderDocumentPackageWindow(ShareholderDocumentPackageWindowModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
