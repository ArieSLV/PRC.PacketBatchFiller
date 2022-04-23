using PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity.ShareholderTransferOrderEntity;

namespace PRC.PacketBatchFiller.Views.Documents.ShareholderDocument
{
    using Catel.Windows;
    using ViewModels;

    public partial class ShareholderTransferOrderEditWindow
    {
        public ShareholderTransferOrderEditWindow()
            : this(null) { }

        public ShareholderTransferOrderEditWindow(ShareholderTransferOrderEditWindowModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
