using Catel.MVVM;
using PRC.PacketBatchFiller.ViewModels.LegalEntityEntity;

namespace PRC.PacketBatchFiller.Views
{
    using Catel.Windows;
    using ViewModels;

    public partial class LegalEntityWindow
    {
        private readonly CommandManagerWrapper _commandManagerWrapper;

        public LegalEntityWindow()
            : this(null)
        { }

        public LegalEntityWindow(LegalEntityWindowModel viewModel)
            : base(viewModel, DataWindowMode.OkCancel, null, DataWindowDefaultButton.None)
        {
            InitializeComponent();

            _commandManagerWrapper = new CommandManagerWrapper(this);
        }
    }
}
