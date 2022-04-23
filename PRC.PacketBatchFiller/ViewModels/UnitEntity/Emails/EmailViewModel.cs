using System.Windows;
using Catel.Data;
using Catel.MVVM;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;

namespace PRC.PacketBatchFiller.ViewModels.UnitEntity.Emails
{
    public class EmailViewModel : ViewModelBase
    {
        public EmailViewModel(Email email)
        {
            Email = email ?? new Email();

            RemoveEmailCommand = new Command(RemoveEmail);
            CopyToClipboardCommand = new Command(CopyToClipboard);
        }


        #region Value property

        [ViewModelToModel("Email")]
        public string Value
        {
            get { return GetValue<string>(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly PropertyData ValueProperty = RegisterProperty("Value", typeof(string));

        #endregion

        #region Type property

        [ViewModelToModel("Email")]
        public ContactType Type
        {
            get { return GetValue<ContactType>(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly PropertyData TypeProperty = RegisterProperty("Type", typeof(ContactType));

        #endregion

        #region Comment property

        [ViewModelToModel("Email")]
        public string Comment
        {
            get { return GetValue<string>(CommentProperty); }
            set { SetValue(CommentProperty, value); }
        }

        public static readonly PropertyData CommentProperty = RegisterProperty("Comment", typeof(string));

        #endregion
        

        #region Email model property

        [Model]
        public Email Email
        {
            get { return GetValue<Email>(EmailProperty); }
            private set { SetValue(EmailProperty, value); }
        }

        public static readonly PropertyData EmailProperty = RegisterProperty("Email", typeof(Email));

        #endregion

        #region EntryNeedDelete property

        public bool EntryNeedDelete
        {
            get { return GetValue<bool>(EntryNeedDeleteProperty); }
            set { SetValue(EntryNeedDeleteProperty, value); }
        }

        public static readonly PropertyData EntryNeedDeleteProperty = RegisterProperty("EntryNeedDelete", typeof(bool));

        #endregion

        #region RemoveEmail command

        public Command RemoveEmailCommand { get; set; }

        private void RemoveEmail()
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