using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Catel.Data;
using Catel.MVVM;
using PhoneNumbers;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PhoneNumber = PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity.PhoneNumber;

namespace PRC.PacketBatchFiller.ViewModels.UnitEntity.PhoneNumbers
{
    [InterestedIn(typeof (PhoneNumberViewModel))]
    public class PhoneNumbersViewModel : ViewModelBase
    {
        private static PhoneNumberUtil _phoneUtil;
        private static AsYouTypeFormatter _asYouTypeFormatter;

        public PhoneNumbersViewModel(ObservableCollection<PhoneNumber> phoneNumbers, ICommandManager commandManager)
        {
            PhoneNumbersCollection = phoneNumbers ?? new ObservableCollection<PhoneNumber>();
            PhoneNumbersViewModelsCollection = new ObservableCollection<PhoneNumberViewModel>();

            foreach (var phoneNumber in PhoneNumbersCollection)
            {
                PhoneNumbersViewModelsCollection.Add(new PhoneNumberViewModel(phoneNumber));
            }

            AddPhoneCommand = new Command(AddPhone);
            AddCharToPhoneCommand = new Command<Key>(AddCharToPhone);

            commandManager.RegisterCommand("AddCharToPhoneCommand", AddCharToPhoneCommand, this);
            
            CopyToClipboardCommand = new Command(CopyToClipboard);
            PasteFromClipboardCommand = new Command(PasteFromClipboard);
            
            _phoneUtil = PhoneNumberUtil.GetInstance();
            _asYouTypeFormatter = _phoneUtil.GetAsYouTypeFormatter("RU");
        }

        #region AddedContactType property

        public ContactType AddedContactType
        {
            get { return GetValue<ContactType>(AddedPhoneNumberTypeProperty); }
            set { SetValue(AddedPhoneNumberTypeProperty, value); }
        }

        public static readonly PropertyData AddedPhoneNumberTypeProperty = RegisterProperty("AddedContactType",
            typeof (ContactType), ContactType.Work);

        #endregion

        #region AddedPhoneNumberValue property

        public string AddedPhoneNumberValue
        {
            get { return GetValue<string>(AddedPhoneNumberValueProperty); }
            set { SetValue(AddedPhoneNumberValueProperty, value); }
        }

        public static readonly PropertyData AddedPhoneNumberValueProperty = RegisterProperty("AddedPhoneNumberValue",
            typeof (string));

        #endregion

        #region AddedPhoneNumberComment property

        public string AddedPhoneNumberComment
        {
            get { return GetValue<string>(AddedPhoneNumberCommentProperty); }
            set { SetValue(AddedPhoneNumberCommentProperty, value); }
        }

        public static readonly PropertyData AddedPhoneNumberCommentProperty = RegisterProperty(
            "AddedPhoneNumberComment", typeof (string));

        #endregion


        #region PhoneNumbersCollection property

        public ObservableCollection<PhoneNumber> PhoneNumbersCollection
        {
            get { return GetValue<ObservableCollection<PhoneNumber>>(PhoneNumbersCollectionProperty); }
            set { SetValue(PhoneNumbersCollectionProperty, value); }
        }

        public static readonly PropertyData PhoneNumbersCollectionProperty = RegisterProperty("PhoneNumbersCollection", typeof (ObservableCollection<PhoneNumber>));

        #endregion

        #region PhoneNumbersViewModelsCollection property

        public ObservableCollection<PhoneNumberViewModel> PhoneNumbersViewModelsCollection
        {
            get
            {
                return GetValue<ObservableCollection<PhoneNumberViewModel>>(PhoneNumbersViewModelsCollectionProperty);
            }
            set { SetValue(PhoneNumbersViewModelsCollectionProperty, value); }
        }

        public static readonly PropertyData PhoneNumbersViewModelsCollectionProperty =
            RegisterProperty("PhoneNumbersViewModelsCollection", typeof (ObservableCollection<PhoneNumberViewModel>));

        #endregion


        #region AddPhone command

        public Command AddPhoneCommand { get; set; }

        private void AddPhone()
        {
            try
            {
                if (AddedPhoneNumberValue.Length == 9) AddedPhoneNumberValue = $"+7 343 {AddedPhoneNumberValue}";

                var numberProto = _phoneUtil.Parse(AddedPhoneNumberValue, "RU");

                var pn = new PhoneNumber
                {
                    Value = _phoneUtil.Format(numberProto, PhoneNumberFormat.INTERNATIONAL),
                    Type = AddedContactType,
                    Comment = AddedPhoneNumberComment
                };

                
                    var pnvm = new PhoneNumberViewModel(pn);
                    PhoneNumbersCollection.Add(pn);
                    PhoneNumbersViewModelsCollection.Add(pnvm);

               
                _asYouTypeFormatter.Clear();
                AddedPhoneNumberValue = string.Empty;
                AddedPhoneNumberComment = string.Empty;
                AddedContactType = ContactType.Work;
            }
            catch (NumberParseException e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        #endregion

        #region AddCharToPhone command

        public Command<Key> AddCharToPhoneCommand { get; set; }

        private void AddCharToPhone(Key key)
        {
            switch (key)
            {
                case Key.Back:

                    _asYouTypeFormatter.Clear();

                    if (AddedPhoneNumberValue.Length > 1)
                    {
                        foreach (var character in AddedPhoneNumberValue.Substring(0, AddedPhoneNumberValue.Length - 1))
                        {
                            if (character != ' ' && character != '-')
                                AddedPhoneNumberValue = _asYouTypeFormatter.InputDigit(character);
                        }
                    }
                    else
                    {
                        AddedPhoneNumberValue = string.Empty;
                    }

                    break;
                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                   
                        AddedPhoneNumberValue = _asYouTypeFormatter.InputDigit(key.ToString()[1]);
                    
                    break;
                case Key.NumPad0:
                case Key.NumPad1:
                case Key.NumPad2:
                case Key.NumPad3:
                case Key.NumPad4:
                case Key.NumPad5:
                case Key.NumPad6:
                case Key.NumPad7:
                case Key.NumPad8:
                case Key.NumPad9:
                    
                        AddedPhoneNumberValue = _asYouTypeFormatter.InputDigit(key.ToString()[6]);
                    
                    break;
                case Key.Add:
                case Key.OemPlus:
                    AddedPhoneNumberValue = _asYouTypeFormatter.InputDigit('+');
                    break;
            }

            try
            {
                var numberProto = _phoneUtil.Parse(AddedPhoneNumberValue, "RU");

                if (_phoneUtil.IsPossibleNumberWithReason(numberProto) == PhoneNumberUtil.ValidationResult.TOO_LONG)
                {
                }

                var pnType = _phoneUtil.GetNumberType(numberProto);

                switch (pnType)
                {
                    case PhoneNumberType.FIXED_LINE:
                        AddedContactType = ContactType.Work;
                        break;
                    case PhoneNumberType.MOBILE:
                        AddedContactType = ContactType.CellPhone;
                        break;
                    case PhoneNumberType.UNKNOWN:
                        if (AddedPhoneNumberValue.Length == 9 && AddedPhoneNumberValue.Substring(3, 1) == "-" &&
                            AddedPhoneNumberValue.Substring(6, 1) == "-") AddedContactType = ContactType.Work;
                        else AddedContactType = ContactType.Unknown;
                        break;
                }
            }
            catch (NumberParseException e)
            {
                if (e.ErrorType != ErrorType.NOT_A_NUMBER && e.ErrorType != ErrorType.TOO_SHORT_NSN)
                    MessageBox.Show(e.ToString());
            }
        }

        #endregion

        #region CopyToClipboard command

        public Command CopyToClipboardCommand { get; set ; }
        
        private void CopyToClipboard()
        {
            try
            {
                var stringToClipboard = "";

                if (AddedPhoneNumberValue.Length == 9) stringToClipboard = $"+7 343 {AddedPhoneNumberValue}";

                var numberProto = _phoneUtil.Parse(stringToClipboard, "RU");
                
                Clipboard.SetText(_phoneUtil.Format(numberProto, PhoneNumberFormat.INTERNATIONAL));

            }
            catch (NumberParseException e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        #endregion

        #region PasteFromClipboard command

        
        public Command PasteFromClipboardCommand { get; set; }

        private void PasteFromClipboard()
        {
            _asYouTypeFormatter.Clear();

            foreach (var character in Clipboard.GetText().Where(character => char.IsDigit(character) || character == '+'))
            {
                AddedPhoneNumberValue = _asYouTypeFormatter.InputDigit(character);
            }

            if (AddedPhoneNumberValue.Length == 9) AddedPhoneNumberValue = $"+7 343 {AddedPhoneNumberValue}";
        }

        #endregion

        #region Methods

        protected override void OnViewModelPropertyChanged(IViewModel viewModel, string propertyName)
        {
            if (propertyName != "EntryNeedDelete") return;
            
                var vm = (PhoneNumberViewModel) viewModel;

                PhoneNumbersViewModelsCollection.Remove(vm);
                PhoneNumbersCollection.Remove(vm.PhoneNumberModel);
        }

        #endregion
    }
}