using Catel.Data;
using Catel.MVVM;
using Catel.MVVM.Views;
using Catel.Services;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.ViewModels.BaseClasses;

namespace PRC.PacketBatchFiller.ViewModels.UnitEntity
{
    public class MailingAddressViewModel : AddressViewModelBase
    {
        public MailingAddressViewModel(
            string expanderHeaderText, 
            Address address, 
            IPleaseWaitService pleaseWaitService,
            ICommandManager commandManager, 
            string nextControlNameToFocus)

            : base(expanderHeaderText, pleaseWaitService, commandManager, nextControlNameToFocus)
        {
            Address = address ?? new Address();
        }

        [Model, ViewToViewModel("MailingAddress")]
        public Address Address
        {
            get { return GetValue<Address>(AddressProperty); }
            private set { SetValue(AddressProperty, value); }
        }
        
        public static readonly PropertyData AddressProperty = RegisterProperty("Address", typeof(Address));
        
    }
}