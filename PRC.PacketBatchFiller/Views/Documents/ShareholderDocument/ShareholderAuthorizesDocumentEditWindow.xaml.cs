using Catel.Windows;
using PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity.ShareholderAuthorizesDocumentEntity;

namespace PRC.PacketBatchFiller.Views.Documents.ShareholderDocument
{
    public partial class ShareholderAuthorizesDocumentEditWindow
    {
        public ShareholderAuthorizesDocumentEditWindow()
            : this(null)
        { }

        public ShareholderAuthorizesDocumentEditWindow(ShareholderAuthorizesDocumentEditWindowModel viewModel)
            : base(viewModel, DataWindowMode.OkCancel, null, DataWindowDefaultButton.None)
        {
            InitializeComponent();
        }
    }
}
