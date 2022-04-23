using Catel.Windows;
using PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity.ShareholderQuestionaryEntity;

namespace PRC.PacketBatchFiller.Views.Documents.ShareholderDocument
{
    public partial class ShareholderQuestionaryEditWindow
    {
        public ShareholderQuestionaryEditWindow()
            : this(null)
        { }

        public ShareholderQuestionaryEditWindow(ShareholderQuestionaryEditWindowModel viewModel)
         : base(viewModel, DataWindowMode.OkCancel, null, DataWindowDefaultButton.None)
        {
            InitializeComponent();
        }
    }
}
