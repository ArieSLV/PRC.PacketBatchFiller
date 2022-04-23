using System.Windows;
using Catel.Data;
using Catel.MVVM;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;

namespace PRC.PacketBatchFiller.ViewModels.UnitEntity.PhoneNumbers
{
    public class PhoneNumberViewModel : ViewModelBase
    {
        public PhoneNumberViewModel(PhoneNumber phoneNumber)
        {
            PhoneNumberModel = phoneNumber ?? new PhoneNumber();

            RemovePhoneCommand = new Command(RemovePhone);
            CopyToClipboardCommand = new Command(CopyToClipboard);
        }


        #region Value property
        
        [ViewModelToModel("PhoneNumberModel")]
        public string Value
        {
            get { return GetValue<string>(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        
        public static readonly PropertyData ValueProperty = RegisterProperty("Value", typeof (string));

        #endregion

        #region Type property
        
        [ViewModelToModel("PhoneNumberModel")]
        public ContactType Type
        {
            get { return GetValue<ContactType>(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        
        public static readonly PropertyData TypeProperty = RegisterProperty("Type", typeof (ContactType));

        #endregion

        #region Comment property

        [ViewModelToModel("PhoneNumberModel")]
        public string Comment
        {
            get { return GetValue<string>(CommentProperty); }
            set { SetValue(CommentProperty, value); }
        }

        public static readonly PropertyData CommentProperty = RegisterProperty("Comment", typeof (string));

        #endregion




        #region PhoneNumberModel model property

        [Model]
        public PhoneNumber PhoneNumberModel
        {
            get { return GetValue<PhoneNumber>(PhoneNumberModelProperty); }
            private set { SetValue(PhoneNumberModelProperty, value); }
        }

        public static readonly PropertyData PhoneNumberModelProperty = RegisterProperty("PhoneNumberModel", typeof (PhoneNumber));

        #endregion

        #region EntryNeedDelete property

        public bool EntryNeedDelete
        {
            get { return GetValue<bool>(EntryNeedDeleteProperty); }
            set { SetValue(EntryNeedDeleteProperty, value); }
        }

        public static readonly PropertyData EntryNeedDeleteProperty = RegisterProperty("EntryNeedDelete", typeof (bool));

        #endregion

        #region RemovePhone command

        public Command RemovePhoneCommand { get; set; }
        
        private void RemovePhone()
        {
            EntryNeedDelete = !EntryNeedDelete;
        }

        #endregion

        #region CopyToClipboard command

        public Command CopyToClipboardCommand { get; set; }
        
        private void CopyToClipboard()
        {
            Clipboard.SetText(Comment != null ? $"{Value} ({Comment})" : Value);
        }

        #endregion
    }
}