namespace PRC.PacketBatchFiller.Views
{
    using Catel.Windows;
    using ViewModels;

    public partial class AddUnitWindow
    {
        public AddUnitWindow()
            : this(null)
        { }

        public AddUnitWindow(AddUnitWindowModel viewModel)
            : base(viewModel, DataWindowMode.Custom)
        {
            InitializeComponent();
        }
    }
}
