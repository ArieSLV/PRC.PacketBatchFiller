using Catel.MVVM;
using PRC.PacketBatchFiller.ViewModels.PersonEntity;

namespace PRC.PacketBatchFiller.Views
{
    using Catel.Windows;
    using ViewModels;

    public partial class PersonWindow
    {
        private readonly CommandManagerWrapper _commandManagerWrapper;

        public PersonWindow()
            : this(null)
        { }

        public PersonWindow(PersonWindowModel model)
            : base(model, DataWindowMode.OkCancel, null, DataWindowDefaultButton.None)
        {
           InitializeComponent();

            _commandManagerWrapper = new CommandManagerWrapper(this);
        }

        
    }
}
