using PRC.PacketBatchFiller.ViewModels.LegalEntityEntity.FormOfIncorporationEntity;

namespace PRC.PacketBatchFiller.Views.LegalEntity.FormOfIncorporation
{
    public partial class FormOfIncorporationEditWindow
    {
        public FormOfIncorporationEditWindow()
            : this(null)
        { }

        public FormOfIncorporationEditWindow(FormOfIncorporationEditWindowModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
