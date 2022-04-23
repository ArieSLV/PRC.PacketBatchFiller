using PRC.PacketBatchFiller.ViewModels.PersonEntity.PlaceOfBirth;

namespace PRC.PacketBatchFiller.Views.Person.PlaceOfBirth
{
    public partial class PlaceOfBirthEditWindow
    {
        public PlaceOfBirthEditWindow()
            : this(null)
        {
        }

        public PlaceOfBirthEditWindow(PlaceOfBirthEditWindowModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
