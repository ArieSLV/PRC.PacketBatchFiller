using PRC.PacketBatchFiller.ViewModels.PersonEntity.CardIDIssuer;

namespace PRC.PacketBatchFiller.Views.Person.CardIDIssuer
{
    public partial class CardIDIssuerEditWindow
    {
        public CardIDIssuerEditWindow()
            : this(null)
        {
        }

        public CardIDIssuerEditWindow(CardIDIssuerEditWindowModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
