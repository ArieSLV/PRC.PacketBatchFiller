using Catel.Data;
using Catel.MVVM;
using Catel.MVVM.Views;
using Catel.Services;
using PRC.PacketBatchFiller.Models;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.ViewModels.BaseClasses;

namespace PRC.PacketBatchFiller.ViewModels.UnitEntity
{
    public class AddressRegistrationViewModel : AddressViewModelBase
    {
        private readonly ICommandManager _commandManager;

        public AddressRegistrationViewModel(string expanderHeaderText, Address address, IPleaseWaitService pleaseWaitService, ICommandManager commandManager, string nextControlNameToFocus)
            : base(expanderHeaderText, pleaseWaitService, commandManager, nextControlNameToFocus)
        {
            _commandManager = commandManager;

            Address = address ?? new Address();

            //GetFocusOnSearchTextboxCommand = new Command(GetFocusOnSearchTextboxExecute);
            //_commandManager.RegisterCommand("GotFocusOnSearchTextbox", GetFocusOnSearchTextboxCommand, this);
        }

        [Model, ViewToViewModel("AddressRegistration")]
        public Address Address
        {
            get { return GetValue<Address>(AddressProperty); }
            private set { SetValue(AddressProperty, value); }
        }

        public static readonly PropertyData AddressProperty = RegisterProperty("Address", typeof(Address));
        
        #region UI properties
        
        #region GetFocusOnSearchTextBoxServiceProperty property

        public bool GetFocusOnSearchTextBoxServiceProperty
        {
            get { return GetValue<bool>(GetFocusOnSeatchTextBoxServicePropertyProperty); }
            set { SetValue(GetFocusOnSeatchTextBoxServicePropertyProperty, value); }
        }

        public static readonly PropertyData GetFocusOnSeatchTextBoxServicePropertyProperty = RegisterProperty("GetFocusOnSearchTextBoxServiceProperty", typeof(bool));

        #endregion

        #endregion

        #region Commands

        #region GetFocusOnSearchTextbox command

        public Command GetFocusOnSearchTextboxCommand { get; set; }

        private void GetFocusOnSearchTextboxExecute() { GetFocusOnSearchTextBoxServiceProperty = !GetFocusOnSearchTextBoxServiceProperty; }

        #endregion

        #endregion

        
    }
}