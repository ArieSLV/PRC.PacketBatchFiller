using PRC.PacketBatchFiller.ViewModels.Documents;

namespace PRC.PacketBatchFiller.Views
{
    public partial class TransferReasonTypeEditWindow
    {
        public TransferReasonTypeEditWindow()
            : this(null) { }

        public TransferReasonTypeEditWindow(TransferReasonTypeEditWindowModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
