using System.Collections.ObjectModel;
using Catel.Data;
using Catel.MVVM;
using PRC.PacketBatchFiller.Models;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;

namespace PRC.PacketBatchFiller.ViewModels.UnitEntity.Emails
{
    [InterestedIn(typeof(EmailViewModel))]
    public class EmailsViewModel : ViewModelBase
    {
        public EmailsViewModel(ObservableCollection<Email> emails)
        {
            EmailsCollection = emails ?? new ObservableCollection<Email>();
            EmailsViewModelsCollection = new ObservableCollection<EmailViewModel>();

            foreach (var email in EmailsCollection)
            {
                EmailsViewModelsCollection.Add(new EmailViewModel(email));
            }

            AddEmailCommand = new Command(AddEmail);
            
        }

        #region AddedContactType property

        public ContactType AddedContactType
        {
            get { return GetValue<ContactType>(AddedEmailTypeProperty); }
            set { SetValue(AddedEmailTypeProperty, value); }
        }

        public static readonly PropertyData AddedEmailTypeProperty = RegisterProperty("AddedContactType",
            typeof(ContactType), ContactType.Work);

        #endregion

        #region AddedEmailValue property

        public string AddedEmailValue
        {
            get { return GetValue<string>(AddedEmailValueProperty); }
            set { SetValue(AddedEmailValueProperty, value); }
        }

        public static readonly PropertyData AddedEmailValueProperty = RegisterProperty("AddedEmailValue", typeof(string));

        #endregion

        #region AddedEmailComment property

        public string AddedEmailComment
        {
            get { return GetValue<string>(AddedEmailCommentProperty); }
            set { SetValue(AddedEmailCommentProperty, value); }
        }

        public static readonly PropertyData AddedEmailCommentProperty = RegisterProperty(
            "AddedEmailComment", typeof(string));

        #endregion

        #region EmailsCollection property

        public ObservableCollection<Email> EmailsCollection
        {
            get { return GetValue<ObservableCollection<Email>>(EmailsCollectionProperty); }
            set { SetValue(EmailsCollectionProperty, value); }
        }

        public static readonly PropertyData EmailsCollectionProperty = RegisterProperty("EmailsCollection", typeof(ObservableCollection<Email>));

        #endregion

        #region EmailsViewModelsCollection property

        public ObservableCollection<EmailViewModel> EmailsViewModelsCollection
        {
            get
            {
                return GetValue<ObservableCollection<EmailViewModel>>(EmailsViewModelsCollectionProperty);
            }
            set { SetValue(EmailsViewModelsCollectionProperty, value); }
        }

        public static readonly PropertyData EmailsViewModelsCollectionProperty = RegisterProperty("EmailsViewModelsCollection", typeof(ObservableCollection<EmailViewModel>));

        #endregion

        #region AddEmail command

        public Command AddEmailCommand { get; set; }

        private void AddEmail()
        {
            var e = new Email
                {
                    Value = AddedEmailValue,
                    Type = AddedContactType,
                    Comment = AddedEmailComment
                };

            var evm = new EmailViewModel(e);
            EmailsCollection.Add(e);
            EmailsViewModelsCollection.Add(evm);
            

            AddedEmailValue = string.Empty;
            AddedEmailComment = string.Empty;
            AddedContactType = ContactType.Work;

        }

        #endregion

        
        
        #region Methods

        protected override void OnViewModelPropertyChanged(IViewModel viewModel, string propertyName)
        {
            if (propertyName != "EntryNeedDelete") return;

            var vm = (EmailViewModel) viewModel;

            EmailsCollection.Remove(vm.Email);
            EmailsViewModelsCollection.Remove(vm);
        }

        #endregion
    }
}
