using Catel.Windows;
using PRC.PacketBatchFiller.ViewModels;

namespace PRC.PacketBatchFiller.Views
{
    public partial class UnitListWindow
    {
        public UnitListWindow()
            : this(null)
        {
        }

        public UnitListWindow(UnitListViewModel viewModel)
            : base(viewModel, DataWindowMode.Close)
        {
            InitializeComponent();
        }
    }
}