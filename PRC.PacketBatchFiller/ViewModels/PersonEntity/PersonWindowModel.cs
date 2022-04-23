using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Catel.Data;
using Catel.MVVM;
using Catel.Services;
using PRC.PacketBatchFiller.Models;
using PRC.PacketBatchFiller.Models.BaseClasses;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.Models.PersonsEntity;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.PersonEntity.PlaceOfBirth;
using PRC.PacketBatchFiller.ViewModels.UnitEntity;
using PRC.PacketBatchFiller.ViewModels.UnitEntity.CitizenshipEntity;
using PRC.PacketBatchFiller.ViewModels.UnitEntity.Emails;
using PRC.PacketBatchFiller.ViewModels.UnitEntity.PhoneNumbers;
using PRC.PacketBatchFiller.ViewModels.UnitEntity.ShareholderAccounts;

namespace PRC.PacketBatchFiller.ViewModels.PersonEntity
{

    [InterestedIn(typeof(PlaceOfBirthViewModel))]
    [InterestedIn(typeof(CitizenshipViewModel))]
    public class PersonWindowModel : ViewModelBase
    {
        #region Fields

        private readonly IUnitService _unitService;

        #endregion

        public PersonWindowModel(   
                                    Person person, 
                                    IUIVisualizerService uiVisualizerService, 
                                    IPleaseWaitService pleaseWaitService, 
                                    ICommandManager commandManager, 
                                    IUnitService unitService)
        {

            #region Initialization parameters 

            _unitService = unitService;
            
            PersonModel = person;
            AddressRegistration = PersonModel.AddressRegistration ?? new Address();
            MailingAddress = PersonModel.MailingAddress ?? new Address();
            PlaceOfBirth = PersonModel.PlaceOfBirth ?? new Models.PersonsEntity.PlaceOfBirth();
            BankDetails = PersonModel.BankDetails ?? new BankDetails();
            CardID = PersonModel.CardID ?? new CardID();
            Citizenship = PersonModel.Citizenship ?? new Citizenship();

            PhoneNumbers = PersonModel.PhoneNumbers ?? new ObservableCollection<PhoneNumber>();
            Emails = PersonModel.Emails ?? new ObservableCollection<Email>();
            ShareholderAccounts = PersonModel.ShareholderAccounts ?? new ObservableCollection<ShareholderAccount>();
            
            #endregion

            #region Setting commands

            MakeFullNameCommand = new Command(MakeFullNameExecute);

            SaveAndClosePersonWindowCommand = new Command<Type>(SaveAndClosePersonWindow);
            commandManager.RegisterCommand("SaveAndClosePersonWindowCommand", SaveAndClosePersonWindowCommand, this);

            #endregion

            #region Setting nested ViewModels

            PlaceOfBirthViewModel = new PlaceOfBirthViewModel(PlaceOfBirth, uiVisualizerService, unitService);
            AddressRegistrationViewModel = new AddressRegistrationViewModel("Адрес места регистрации", PersonModel.AddressRegistration, pleaseWaitService, commandManager, "MailingAddressEqualRegistrationAddressCheckBox");
            MailingAddressViewModel = new MailingAddressViewModel("Адрес фактического места жительства", PersonModel.MailingAddress, pleaseWaitService, commandManager, "DividentsPaymentWayByMailRadioButton");
            BankDetailsViewModel = new BankDetailsViewModel(PersonModel.BankDetails, pleaseWaitService);
            CardIDViewModel = new CardIDViewModel(CardID, uiVisualizerService, unitService);
            PhoneNumbersViewModel = new PhoneNumbersViewModel(PhoneNumbers, commandManager);
            EmailsViewModel = new EmailsViewModel(Emails);
            CitizenshipViewModel = new CitizenshipViewModel("Гражданство", Citizenship, uiVisualizerService, unitService);
            ShareholderAccountsViewModel = new ShareholderAccountsViewModel(PersonModel.UnitId, ShareholderAccounts, uiVisualizerService, commandManager, unitService);

            #endregion


        }

        #region Person model & properties

        #region LastName property

        [ViewModelToModel("PersonModel")]
        public string LastName
        {
            get { return GetValue<string>(LastNameProperty); }
            set { SetValue(LastNameProperty, value); }
        }

        public static readonly PropertyData LastNameProperty = RegisterProperty("LastName", typeof(string));

        #endregion

        #region FirstName property

        [ViewModelToModel("PersonModel")]
        public string FirstName
        {
            get { return GetValue<string>(FirstNameProperty); }
            set { SetValue(FirstNameProperty, value); }
        }

        public static readonly PropertyData FirstNameProperty = RegisterProperty("FirstName", typeof(string));

        #endregion

        #region MiddleName property

        [ViewModelToModel("PersonModel")]
        public string MiddleName
        {
            get { return GetValue<string>(MiddleNameProperty); }
            set { SetValue(MiddleNameProperty, value); }
        }

        public static readonly PropertyData MiddleNameProperty = RegisterProperty("MiddleName", typeof(string));

        #endregion

        #region PlaceOfBirth property

        [ViewModelToModel("PersonModel")]
        public Models.PersonsEntity.PlaceOfBirth PlaceOfBirth
        {
            get { return GetValue<Models.PersonsEntity.PlaceOfBirth>(PlaceOfBirthProperty); }
            set { SetValue(PlaceOfBirthProperty, value); }
        }

        public static readonly PropertyData PlaceOfBirthProperty = RegisterProperty("PlaceOfBirth", typeof (Models.PersonsEntity.PlaceOfBirth));

        #endregion

        #region AddressRegistration property

        [ViewModelToModel("PersonModel")]
        public Address AddressRegistration
        {
            get { return GetValue<Address>(AddressRegistrationProperty); }
            set { SetValue(AddressRegistrationProperty, value); }
        }

        public static readonly PropertyData AddressRegistrationProperty = RegisterProperty("AddressRegistration", typeof(Address));

        #endregion
        
        #region MailingAddress property

        [ViewModelToModel("PersonModel")]
        public Address MailingAddress
        {
            get { return GetValue<Address>(MailingAddressProperty); }
            set { SetValue(MailingAddressProperty, value); }
        }

        public static readonly PropertyData MailingAddressProperty = RegisterProperty("MailingAddress", typeof (Address));

        #endregion

        #region CardID property

        [ViewModelToModel("PersonModel")]
        public CardID CardID
        {
            get { return GetValue<CardID>(CardIDProperty); }
            set { SetValue(CardIDProperty, value); }
        }

        public static readonly PropertyData CardIDProperty = RegisterProperty("CardID", typeof (CardID));

        #endregion

        #region MailingAddressEqualRegistrationAddressFlag property

        [ViewModelToModel("PersonModel")]
        public bool MailingAddressEqualRegistrationAddressFlag
        {
            get { return GetValue<bool>(MailingAddressEqualRegistrationAddressFlagProperty); }
            set { SetValue(MailingAddressEqualRegistrationAddressFlagProperty, value); }
        }
        
        public static readonly PropertyData MailingAddressEqualRegistrationAddressFlagProperty = RegisterProperty("MailingAddressEqualRegistrationAddressFlag", typeof (bool));

        #endregion

        #region DateOfBirth property

        [ViewModelToModel("PersonModel")]
        public DateTime? DateOfBirth
        {
            get { return GetValue<DateTime?>(DateOfBirthProperty); }
            set { SetValue(DateOfBirthProperty, value); }
        }

        public static readonly PropertyData DateOfBirthProperty = RegisterProperty("DateOfBirth", typeof (DateTime?));

        #endregion

        #region DividentsPaymentWay property

        [ViewModelToModel("PersonModel")]
        public DividentsPaymentWays DividentsPaymentWay
        {
            get { return GetValue<DividentsPaymentWays>(DividentsPaymentWayProperty); }
            set { SetValue(DividentsPaymentWayProperty, value); }
        }

        public static readonly PropertyData DividentsPaymentWayProperty = RegisterProperty("DividentsPaymentWay", typeof (DividentsPaymentWays), DividentsPaymentWays.Unknow);

        #endregion

        #region BankDetails property

        [ViewModelToModel("PersonModel")]
        public BankDetails BankDetails
        {
            get { return GetValue<BankDetails>(BankDetailsProperty); }
            set { SetValue(BankDetailsProperty, value); }
        }

        public static readonly PropertyData BankDetailsProperty = RegisterProperty("BankDetails", typeof (BankDetails));

        #endregion

        #region OnlyPersonalPresenceFlag property
        
        [ViewModelToModel("PersonModel")]
        public bool OnlyPersonalPresenceFlag
        {
            get { return GetValue<bool>(OnlyPersonalPresenceFlagProperty); }
            set { SetValue(OnlyPersonalPresenceFlagProperty, value); }
        }

        public static readonly PropertyData OnlyPersonalPresenceFlagProperty = RegisterProperty("OnlyPersonalPresenceFlag", typeof (bool));

        #endregion

        #region IsOneOfPODFTFlag property

        [ViewModelToModel("PersonModel")]
        public bool IsOneOfPODFTFlag
        {
            get { return GetValue<bool>(IsOneOfPODFTFlagProperty); }
            set { SetValue(IsOneOfPODFTFlagProperty, value); }
        }
        
        public static readonly PropertyData IsOneOfPODFTFlagProperty = RegisterProperty("IsOneOfPODFTFlag", typeof (bool));

        #endregion

        #region GotBeneficialOwnerFlag property

        [ViewModelToModel("PersonModel")]
        public bool GotBeneficialOwnerFlag
        {
            get { return GetValue<bool>(GotBeneficialOwnerFlagProperty); }
            set { SetValue(GotBeneficialOwnerFlagProperty, value); }
        }
        
        public static readonly PropertyData GotBeneficialOwnerFlagProperty = RegisterProperty("GotBeneficialOwnerFlag", typeof (bool));

        #endregion

        #region IsHeadNonComercialCompanyFlag property

        [ViewModelToModel("PersonModel")]
        public bool IsHeadNonComercialCompanyFlag
        {
            get { return GetValue<bool>(IsHeadNonComercialCompanyFlagProperty); }
            set { SetValue(IsHeadNonComercialCompanyFlagProperty, value); }
        }
        
        public static readonly PropertyData IsHeadNonComercialCompanyFlagProperty = RegisterProperty("IsHeadNonComercialCompanyFlag", typeof (bool));

        #endregion

        #region PhoneNumbers property

        [ViewModelToModel("PersonModel")]
        public ObservableCollection<PhoneNumber> PhoneNumbers
        {
            get { return GetValue<ObservableCollection<PhoneNumber>>(PhoneNumbersProperty); }
            set { SetValue(PhoneNumbersProperty, value); }
        }

        public static readonly PropertyData PhoneNumbersProperty = RegisterProperty("PhoneNumbers", typeof(ObservableCollection<PhoneNumber>));

        #endregion

        #region Emails property

        [ViewModelToModel("PersonModel")]
        public ObservableCollection<Email> Emails
        {
            get { return GetValue<ObservableCollection<Email>>(EmailsProperty); }
            set { SetValue(EmailsProperty, value); }
        }

        public static readonly PropertyData EmailsProperty = RegisterProperty("Emails", typeof (ObservableCollection<Email>));

        #endregion

        #region INN property

        [ViewModelToModel("PersonModel")]
        public string INN
        {
            get { return GetValue<string>(INNProperty); }
            set { SetValue(INNProperty, value); }
        }

        public static readonly PropertyData INNProperty = RegisterProperty("INN", typeof(string));

        #endregion

        #region Citizenship property

        [ViewModelToModel("PersonModel")]
        public Citizenship Citizenship
        {
            get { return GetValue<Citizenship>(CitizenshipProperty); }
            set { SetValue(CitizenshipProperty, value); }
        }

        public static readonly PropertyData CitizenshipProperty = RegisterProperty("Citizenship", typeof (Citizenship));

        #endregion

        #region RoleIsShareHolderFlag property

        [ViewModelToModel("PersonModel")]
        public bool RoleIsShareHolderFlag
        {
            get { return GetValue<bool>(RoleIsShareHolderFlagProperty); }
            set { SetValue(RoleIsShareHolderFlagProperty, value); }
        }

        public static readonly PropertyData RoleIsShareHolderFlagProperty = RegisterProperty("RoleIsShareHolderFlag", typeof(bool));

        #endregion

        #region ShareholderAccounts property

        [ViewModelToModel("PersonModel")]
        public ObservableCollection<ShareholderAccount> ShareholderAccounts
        {
            get { return GetValue<ObservableCollection<ShareholderAccount>>(ShareholderAccountsProperty); }
            set { SetValue(ShareholderAccountsProperty, value); }
        }

        public static readonly PropertyData ShareholderAccountsProperty = RegisterProperty("ShareholderAccounts", typeof (ObservableCollection<ShareholderAccount>), null);

        #endregion


        #region PersonModel model property

        [Model]
        public Person PersonModel
        {
            get { return GetValue<Person>(PersonModelProperty); }
            private set { SetValue(PersonModelProperty, value); }
        }

        public static readonly PropertyData PersonModelProperty = RegisterProperty("PersonModel", typeof(Person));

        #endregion

        #endregion

        #region UI properties

        #region Window Title

        public override string Title => "Физическое лицо";

        #endregion


        #region ViewModels properties

        #region PlaceOfBirthViewModel property

        public IViewModel PlaceOfBirthViewModel
        {
            get { return GetValue<IViewModel>(PlaceOfBirthViewModelProperty); }
            set { SetValue(PlaceOfBirthViewModelProperty, value); }
        }

        public static readonly PropertyData PlaceOfBirthViewModelProperty = RegisterProperty("PlaceOfBirthViewModel", typeof(IViewModel));

        #endregion

        #region AddressRegistrationViewModel property

        public IViewModel AddressRegistrationViewModel
        {
            get { return GetValue<IViewModel>(AddressRegistrationViewModelProperty); }
            set { SetValue(AddressRegistrationViewModelProperty, value); }
        }

        public static readonly PropertyData AddressRegistrationViewModelProperty = RegisterProperty("AddressRegistrationViewModel", typeof(IViewModel));

        #endregion

        #region MailingAddressViewModel property

        public IViewModel MailingAddressViewModel
        {
            get { return GetValue<IViewModel>(MailingAddressViewModelProperty); }
            set { SetValue(MailingAddressViewModelProperty, value); }
        }

        public static readonly PropertyData MailingAddressViewModelProperty = RegisterProperty("MailingAddressViewModel", typeof(IViewModel));

        #endregion

        #region CardIDViewModel property

        public IViewModel CardIDViewModel
        {
            get { return GetValue<IViewModel>(CardIDViewModelProperty); }
            set { SetValue(CardIDViewModelProperty, value); }
        }

        public static readonly PropertyData CardIDViewModelProperty = RegisterProperty("CardIDViewModel", typeof(IViewModel));

        #endregion

        #region BankDetailsViewModel property

        public IViewModel BankDetailsViewModel
        {
            get { return GetValue<IViewModel>(BankDetailsViewModelProperty); }
            set { SetValue(BankDetailsViewModelProperty, value); }
        }

        public static readonly PropertyData BankDetailsViewModelProperty = RegisterProperty("BankDetailsViewModel", typeof(IViewModel));

        #endregion

        #region PhoneNumbersViewModel property
        
        public IViewModel PhoneNumbersViewModel
        {
            get { return GetValue<IViewModel>(PhoneNumbersViewModelProperty); }
            set { SetValue(PhoneNumbersViewModelProperty, value); }
        }
        
        public static readonly PropertyData PhoneNumbersViewModelProperty = RegisterProperty("PhoneNumbersViewModel", typeof (IViewModel));

        #endregion

        #region EmailsViewModel property

        public IViewModel EmailsViewModel
        {
            get { return GetValue<IViewModel>(EmailsViewModelProperty); }
            set { SetValue(EmailsViewModelProperty, value); }
        }

        public static readonly PropertyData EmailsViewModelProperty = RegisterProperty("EmailsViewModel", typeof (IViewModel));

        #endregion

        #region ShareholderAccountsViewModel property

        public IViewModel ShareholderAccountsViewModel
        {
            get { return GetValue<IViewModel>(ShareholderAccountsViewModelProperty); }
            set { SetValue(ShareholderAccountsViewModelProperty, value); }
        }

        public static readonly PropertyData ShareholderAccountsViewModelProperty = RegisterProperty("ShareholderAccountsViewModel", typeof(IViewModel));

        #endregion


        #region CitizenshipViewModel property

        public IViewModel CitizenshipViewModel
        {
            get { return GetValue<IViewModel>(CitizenshipViewModelProperty); }
            set { SetValue(CitizenshipViewModelProperty, value); }
        }

        public static readonly PropertyData CitizenshipViewModelProperty = RegisterProperty("CitizenshipViewModel", typeof (IViewModel));

        #endregion

        

        #endregion

         
        #region GetFocusOnMailingAddressEqualRegistrationAddressServiceProperty property

        public bool GetFocusOnMailingAddressEqualRegistrationAddressServiceProperty
        {
            get { return GetValue<bool>(GetFocusOnMailingAddressEqualRegistrationAddressServicePropertyProperty); }
            set { SetValue(GetFocusOnMailingAddressEqualRegistrationAddressServicePropertyProperty, value); }
        }

        public static readonly PropertyData GetFocusOnMailingAddressEqualRegistrationAddressServicePropertyProperty = RegisterProperty("GetFocusOnMailingAddressEqualRegistrationAddressServiceProperty", typeof (bool));

        #endregion

        #region GetFocusOnDividentsPaymentWayByMailServiceProperty property

        public bool GetFocusOnDividentsPaymentWayByMailServiceProperty
        {
            get { return GetValue<bool>(GetFocusOnDividentsPaymentWayByMailServicePropertyProperty); }
            set { SetValue(GetFocusOnDividentsPaymentWayByMailServicePropertyProperty, value); }
        }

        public static readonly PropertyData GetFocusOnDividentsPaymentWayByMailServicePropertyProperty = RegisterProperty("GetFocusOnDividentsPaymentWayByMailServiceProperty", typeof (bool));

        #endregion

        #endregion

        #region Commands

        #region MakeFullName command
        
        public Command MakeFullNameCommand { get; private set; }
        
        private void MakeFullNameExecute()
        {
            PersonModel.FullName = string.Empty;
            if (!string.IsNullOrWhiteSpace(LastName))
            {
                PersonModel.FullName += $"{LastName}";
            }
            if (!string.IsNullOrWhiteSpace(FirstName))
            {
                PersonModel.FullName += $" {FirstName}";
            }
            if (!string.IsNullOrWhiteSpace(MiddleName))
            {
                PersonModel.FullName += $" {MiddleName}";
            }

        }

        #endregion

        #region MakeMailingAddressIsDifferentThanAddressRegistrationCommandExecute command
        private Command _makeMailingAddressIsDifferentThanAddressRegistrationCommand;
        public Command MakeMailingAddressIsDifferentThanAddressRegistrationCommand { get { return _makeMailingAddressIsDifferentThanAddressRegistrationCommand ?? (_makeMailingAddressIsDifferentThanAddressRegistrationCommand = new Command(MakeMailingAddressIsDifferentThanAddressRegistrationCommandExecute)); } }
        private void MakeMailingAddressIsDifferentThanAddressRegistrationCommandExecute()
        {
            if (MailingAddress == AddressRegistration)
            {
                MailingAddress = new Address();
            } 
        }
        #endregion
        
        #region GetFocusOnMailingAddressEqualRegistrationAddressCheckBox command

        public Command GetFocusOnMailingAddressEqualRegistrationAddressCheckBoxCommand { get; }
        
        private void GetFocusOnMailingAddressEqualRegistrationAddressCheckBox()
        {
            GetFocusOnMailingAddressEqualRegistrationAddressServiceProperty = !GetFocusOnMailingAddressEqualRegistrationAddressServiceProperty;
        }

        #endregion

        #region GetFocusOnDividentsPaymentWayByMailRadioButton command

        public Command GetFocusOnDividentsPaymentWayByMailRadioButtonCommand { get; }
        
        private void GetFocusOnDividentsPaymentWayByMailRadioButton()
        {
            GetFocusOnDividentsPaymentWayByMailServiceProperty = !GetFocusOnDividentsPaymentWayByMailServiceProperty;
        }

        #endregion

        #region GetFocusOnINNTextBox command

        public Command GetFocusOnINNTextBoxCommand { get; }

        public bool GetFocusOnINNTextBoxServiceProperty
        {
            get { return GetValue<bool>(GetFocusOnINNTextBoxServicePropertyProperty); }
            set { SetValue(GetFocusOnINNTextBoxServicePropertyProperty, value); }
        }

        public static readonly PropertyData GetFocusOnINNTextBoxServicePropertyProperty = RegisterProperty("GetFocusOnINNTextBoxServiceProperty", typeof (bool));

        private void GetFocusOnINNTextBox()
        {
            GetFocusOnINNTextBoxServiceProperty = !GetFocusOnINNTextBoxServiceProperty;
        }

        #endregion

        #region SaveAndClosePersonWindow command

        public Command<Type> SaveAndClosePersonWindowCommand { get; set; }

        private void SaveAndClosePersonWindow(Type type)
        {
            var personId = _unitService.SaveUnitToContext(PersonModel);
            _unitService.AddUnitToHistory(PersonModel);

            var task = this.SaveAndCloseViewModelAsync();
            task.Wait();

            _unitService.CreateNewItemInItemOfUnitForRestoreList(type, personId);
        }

        #endregion


        protected override async Task InitializeAsync() { await base.InitializeAsync(); }
        protected override async Task CloseAsync() { await base.CloseAsync(); }

        #endregion
        
        #region Methods

        protected override void OnViewModelPropertyChanged(IViewModel viewModel, string propertyName)
        {
            if (propertyName == "TargetEntity" && viewModel is PlaceOfBirthViewModel)
            {
                var vm = (PlaceOfBirthViewModel) viewModel;
                PlaceOfBirth = vm.TargetEntity;
            }
            else if (propertyName == "TargetEntity" && viewModel is CitizenshipViewModel)
            {
                var vm = (CitizenshipViewModel) viewModel;
                Citizenship = vm.TargetEntity;
            }
        }

        #endregion
    }
}