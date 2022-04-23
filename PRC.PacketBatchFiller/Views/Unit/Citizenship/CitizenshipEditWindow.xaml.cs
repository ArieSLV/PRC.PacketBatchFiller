using PRC.PacketBatchFiller.ViewModels.UnitEntity.CitizenshipEntity;

namespace PRC.PacketBatchFiller.Views.Unit.Citizenship
{
    public partial class CitizenshipEditWindow
    {
        public CitizenshipEditWindow()
            : this(null)
        {
        }

        public CitizenshipEditWindow(CitizenshipEditWindowModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
