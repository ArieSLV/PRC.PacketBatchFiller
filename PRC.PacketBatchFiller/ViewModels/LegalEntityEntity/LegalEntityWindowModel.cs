using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Catel.Data;
using Catel.MVVM;
using Catel.Services;
using PRC.PacketBatchFiller.Models;
using PRC.PacketBatchFiller.Models.BaseClasses;
using PRC.PacketBatchFiller.Models.BaseClasses.Documents;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.Models.Documents;
using PRC.PacketBatchFiller.Models.LegalEntityEntity;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.LegalEntityEntity.FormOfIncorporationEntity;
using PRC.PacketBatchFiller.ViewModels.LegalEntityEntity.IssuesOfSecurities;
using PRC.PacketBatchFiller.ViewModels.UnitEntity;
using PRC.PacketBatchFiller.ViewModels.UnitEntity.CitizenshipEntity;
using PRC.PacketBatchFiller.ViewModels.UnitEntity.Emails;
using PRC.PacketBatchFiller.ViewModels.UnitEntity.PhoneNumbers;
using PRC.PacketBatchFiller.ViewModels.UnitEntity.ShareholderAccounts;

namespace PRC.PacketBatchFiller.ViewModels.LegalEntityEntity
{
    [InterestedIn(typeof(CitizenshipViewModel))]
    [InterestedIn(typeof(FormOfIncorporationViewModel))]
    [InterestedIn(typeof(FirstPersonOfCompanyViewModel))]
    public class LegalEntityWindowModel : ViewModelBase
    {
        #region Fields

        private readonly IUIVisualizerService _uiVisualizerService;
        private readonly ICommandManager _commandManager;
        private readonly IUnitService _unitService;
        
        private string _fullSubname;
        private string _shortSubname;

        #endregion


        public LegalEntityWindowModel(
            LegalEntity legalEntity, 
            IUIVisualizerService uiVisualizerService, 
            IPleaseWaitService pleaseWaitService, 
            ICommandManager commandManager, 
            IUnitService unitService,
            IDocumentService documentService
            
)
        {
            #region Initialization parameters 

            _uiVisualizerService = uiVisualizerService;
            _commandManager = commandManager;
            _unitService = unitService;
            

            if (legalEntity.FormOfIncorporation?.FullForm != null && legalEntity.FormOfIncorporation.FullForm != FormOfIncorporation.DefaultValue)
                _fullSubname = legalEntity.FullName.Substring(legalEntity.FormOfIncorporation.FullForm.Length + 2, legalEntity.FullName.Length - 1 - (legalEntity.FormOfIncorporation.FullForm.Length + 2));
            
            if (legalEntity.FormOfIncorporation?.ShortForm != null && legalEntity.FormOfIncorporation.ShortForm != FormOfIncorporation.DefaultValue)
                _shortSubname = legalEntity.ShortName.Substring(legalEntity.FormOfIncorporation.ShortForm.Length + 2, legalEntity.ShortName.Length - 1 - (legalEntity.FormOfIncorporation.ShortForm.Length + 2));

            LegalEntityModel = legalEntity;
            FormOfIncorporation = LegalEntityModel.FormOfIncorporation ?? new FormOfIncorporation();
            RegistrationCertificate = LegalEntityModel.RegistrationCertificate ?? new RegistrationCertificate();
            Citizenship = LegalEntityModel.Citizenship ?? new Citizenship();
            AddressRegistration = LegalEntityModel.AddressRegistration ?? new Address();
            MailingAddress = LegalEntityModel.MailingAddress ?? new Address();
            BankDetails = LegalEntityModel.BankDetails ?? new BankDetails();
            FirstPersonOfCompany = LegalEntityModel.FirstPersonOfCompany;

            PhoneNumbers = LegalEntityModel.PhoneNumbers ?? new ObservableCollection<PhoneNumber>();
            Emails = LegalEntityModel.Emails ?? new ObservableCollection<Email>();
            ShareholderAccounts = LegalEntityModel.ShareholderAccounts ?? new ObservableCollection<ShareholderAccount>();
            IssuesOfSecurities = LegalEntityModel.IssuesOfSecurities ?? new ObservableCollection<IssueOfSecurities>();
            

            #endregion

           
            #region Setting nested ViewModels

            CitizenshipViewModel = new CitizenshipViewModel("Страна государственной регистрации", Citizenship, uiVisualizerService,  unitService);
            AddressRegistrationViewModel = new AddressRegistrationViewModel("Адрес места регистрации", AddressRegistration, pleaseWaitService, commandManager, "MailingAddressEqualRegistrationAddressCheckBox");
            MailingAddressViewModel = new MailingAddressViewModel("Адрес фактического места жительства", MailingAddress, pleaseWaitService, commandManager, "DividentsPaymentWayByMailRadioButton");
            BankDetailsViewModel = new BankDetailsViewModel(BankDetails, pleaseWaitService);
            RegistrationCertificateViewModel = new RegistrationCertificateViewModel(RegistrationCertificate, Citizenship.Value, uiVisualizerService, unitService);
            PhoneNumbersViewModel = new PhoneNumbersViewModel(PhoneNumbers, commandManager);
            EmailsViewModel = new EmailsViewModel(Emails);
            FormOfIncorporationViewModel = new FormOfIncorporationViewModel(FormOfIncorporation, uiVisualizerService, unitService);
            FirstPersonOfCompanyViewModel = new FirstPersonOfCompanyViewModel(FirstPersonOfCompany, uiVisualizerService, unitService, commandManager);
            ShareholderAccountsViewModel = new ShareholderAccountsViewModel(LegalEntityModel.UnitId, ShareholderAccounts, _uiVisualizerService, _commandManager, _unitService);
            IssuesOfSecuritiesViewModel = new IssuesOfSecuritiesViewModel(IssuesOfSecurities, _unitService);

            #endregion

            #region Commands

            SaveAndCloseLegalEntityWindowCommand = new Command<Type>(SaveAndCloseLegalEntityWindow);
            commandManager.RegisterCommand("SaveAndCloseLegalEntityWindowCommand", SaveAndCloseLegalEntityWindowCommand, this);

            #endregion
        }

        #region LegalEntity model & properties


        #region FormOfIncorporation property

        [ViewModelToModel("LegalEntityModel")]
        public FormOfIncorporation FormOfIncorporation
        {
            get { return GetValue<FormOfIncorporation>(FormOfIncorporationProperty); }
            set { SetValue(FormOfIncorporationProperty, value); }
        }

        public static readonly PropertyData FormOfIncorporationProperty = RegisterProperty("FormOfIncorporation", typeof (FormOfIncorporation), null,
            (sender, e) => ((LegalEntityWindowModel)sender).OnFormOfIncorporationChanged());

        private void OnFormOfIncorporationChanged()
        {
            NameFormation();
        }

        #endregion

        #region INN property

        [ViewModelToModel("LegalEntityModel")]
        public string INN
        {
            get { return GetValue<string>(INNProperty); }
            set { SetValue(INNProperty, value); }
        }

        public static readonly PropertyData INNProperty = RegisterProperty("INN", typeof(string));

        #endregion

        #region KPP property

        [ViewModelToModel("LegalEntityModel")]
        public string KPP
        {
            get { return GetValue<string>(KPPProperty); }
            set { SetValue(KPPProperty, value); }
        }

        public static readonly PropertyData KPPProperty = RegisterProperty("KPP", typeof (string));

        #endregion

        #region OKPO property

        [ViewModelToModel("LegalEntityModel")]
        public string OKPO
        {
            get { return GetValue<string>(OKPOProperty); }
            set { SetValue(OKPOProperty, value); }
        }

        public static readonly PropertyData OKPOProperty = RegisterProperty("OKPO", typeof (string));

        #endregion

        #region OKVED property

        [ViewModelToModel("LegalEntityModel")]
        public string OKVED
        {
            get { return GetValue<string>(OKVEDProperty); }
            set { SetValue(OKVEDProperty, value); }
        }

        public static readonly PropertyData OKVEDProperty = RegisterProperty("OKVED", typeof (string));

        #endregion

        #region Citizenship property

        [ViewModelToModel("LegalEntityModel")]
        public Citizenship Citizenship
        {
            get { return GetValue<Citizenship>(CitizenshipProperty); }
            set { SetValue(CitizenshipProperty, value); }
        }
        
        public static readonly PropertyData CitizenshipProperty = RegisterProperty("Citizenship", typeof (Citizenship), null,
            (sender, e) => ((LegalEntityWindowModel) sender).OnCitizenshipChanged());

        private void OnCitizenshipChanged()
        {
            if (Citizenship != null && RegistrationCertificate !=null) RegistrationCertificateViewModel = new RegistrationCertificateViewModel(RegistrationCertificate, Citizenship.Value, _uiVisualizerService, _unitService);
        }

        #endregion
        
        #region RegistrationCertificate property
        
        [ViewModelToModel("LegalEntityModel")]
        public RegistrationCertificate RegistrationCertificate
        {
            get { return GetValue<RegistrationCertificate>(RegistrationCertificateProperty); }
            set { SetValue(RegistrationCertificateProperty, value); }
        }
        
        public static readonly PropertyData RegistrationCertificateProperty = RegisterProperty("RegistrationCertificate", typeof (RegistrationCertificate));

        #endregion

        #region AddressRegistration property

        [ViewModelToModel("LegalEntityModel")]
        public Address AddressRegistration
        {
            get { return GetValue<Address>(AddressRegistrationProperty); }
            set { SetValue(AddressRegistrationProperty, value); }
        }

        public static readonly PropertyData AddressRegistrationProperty = RegisterProperty("AddressRegistration", typeof(Address));

        #endregion

        #region MailingAddress property

        [ViewModelToModel("LegalEntityModel")]
        public Address MailingAddress
        {
            get { return GetValue<Address>(MailingAddressProperty); }
            set { SetValue(MailingAddressProperty, value); }
        }

        public static readonly PropertyData MailingAddressProperty = RegisterProperty("MailingAddress", typeof(Address));

        #endregion

        #region PhoneNumbers property

        [ViewModelToModel("LegalEntityModel")]
        public ObservableCollection<PhoneNumber> PhoneNumbers
        {
            get { return GetValue<ObservableCollection<PhoneNumber>>(PhoneNumbersProperty); }
            set { SetValue(PhoneNumbersProperty, value); }
        }

        public static readonly PropertyData PhoneNumbersProperty = RegisterProperty("PhoneNumbers", typeof(ObservableCollection<PhoneNumber>));

        #endregion

        #region Emails property

        [ViewModelToModel("LegalEntityModel")]
        public ObservableCollection<Email> Emails
        {
            get { return GetValue<ObservableCollection<Email>>(EmailsProperty); }
            set { SetValue(EmailsProperty, value); }
        }

        public static readonly PropertyData EmailsProperty = RegisterProperty("Emails", typeof(ObservableCollection<Email>));

        #endregion
        
        #region OnlyPersonalPresenceFlag property

        [ViewModelToModel("LegalEntityModel")]
        public bool OnlyPersonalPresenceFlag
        {
            get { return GetValue<bool>(OnlyPersonalPresenceFlagProperty); }
            set { SetValue(OnlyPersonalPresenceFlagProperty, value); }
        }

        public static readonly PropertyData OnlyPersonalPresenceFlagProperty = RegisterProperty("OnlyPersonalPresenceFlag", typeof(bool));

        #endregion

        #region DividentsPaymentWay property

        [ViewModelToModel("LegalEntityModel")]
        public DividentsPaymentWays DividentsPaymentWay
        {
            get { return GetValue<DividentsPaymentWays>(DividentsPaymentWayProperty); }
            set { SetValue(DividentsPaymentWayProperty, value); }
        }

        public static readonly PropertyData DividentsPaymentWayProperty = RegisterProperty("DividentsPaymentWay", typeof(DividentsPaymentWays), DividentsPaymentWays.ByBank);

        #endregion

        #region BankDetails property

        [ViewModelToModel("LegalEntityModel")]
        public BankDetails BankDetails
        {
            get { return GetValue<BankDetails>(BankDetailsProperty); }
            set { SetValue(BankDetailsProperty, value); }
        }

        public static readonly PropertyData BankDetailsProperty = RegisterProperty("BankDetails", typeof(BankDetails));

        #endregion

        #region FirstPersonOfCompany property

        [ViewModelToModel("LegalEntityModel")]
        public Unit FirstPersonOfCompany
        {
            get { return GetValue<Unit>(FirstPersonOfCompanyProperty); }
            set { SetValue(FirstPersonOfCompanyProperty, value); }
        }

        public static readonly PropertyData FirstPersonOfCompanyProperty = RegisterProperty("FirstPersonOfCompany", typeof (Unit));

        #endregion

        #region MailingAddressEqualRegistrationAddressFlag property

        [ViewModelToModel("LegalEntityModel")]
        public bool MailingAddressEqualRegistrationAddressFlag
        {
            get { return GetValue<bool>(MailingAddressEqualRegistrationAddressFlagProperty); }
            set { SetValue(MailingAddressEqualRegistrationAddressFlagProperty, value); }
        }

        public static readonly PropertyData MailingAddressEqualRegistrationAddressFlagProperty = RegisterProperty("MailingAddressEqualRegistrationAddressFlag", typeof(bool));
         
        #endregion

        #region ShareholderAccounts property

        [ViewModelToModel("LegalEntityModel")]
        public ObservableCollection<ShareholderAccount> ShareholderAccounts
        {
            get { return GetValue<ObservableCollection<ShareholderAccount>>(ShareholderAccountsProperty); }
            set { SetValue(ShareholderAccountsProperty, value); }
        }

        public static readonly PropertyData ShareholderAccountsProperty = RegisterProperty("ShareholderAccounts", typeof(ObservableCollection<ShareholderAccount>));

        #endregion

        #region IssuesOfSecurities property

        [ViewModelToModel("LegalEntityModel")]
        public ObservableCollection<IssueOfSecurities> IssuesOfSecurities
        {
            get { return GetValue<ObservableCollection<IssueOfSecurities>>(IssuesOfSecuritiesProperty); }
            set { SetValue(IssuesOfSecuritiesProperty, value); }
        }

        public static readonly PropertyData IssuesOfSecuritiesProperty = RegisterProperty("IssuesOfSecurities", typeof(ObservableCollection<IssueOfSecurities>));

        #endregion

        #region RoleIsShareHolderFlag property

        [ViewModelToModel("LegalEntityModel")]
        public bool RoleIsShareHolderFlag
        {
            get { return GetValue<bool>(RoleIsShareHolderFlagProperty); }
            set { SetValue(RoleIsShareHolderFlagProperty, value); }
        }

        public static readonly PropertyData RoleIsShareHolderFlagProperty = RegisterProperty("RoleIsShareHolderFlag", typeof(bool));

        #endregion
        
        #region RoleIsSecuritiesIssuerFlag property

        [ViewModelToModel("LegalEntityModel")]
        public bool RoleIsSecuritiesIssuerFlag
        {
            get { return GetValue<bool>(RoleIsSecuritiesIssuerFlagProperty); }
            set { SetValue(RoleIsSecuritiesIssuerFlagProperty, value); }
        }

        public static readonly PropertyData RoleIsSecuritiesIssuerFlagProperty = RegisterProperty("RoleIsSecuritiesIssuerFlag", typeof (bool));
        
        #endregion

        #region FullName property

        [ViewModelToModel("LegalEntityModel")]
        public string FullName
        {
            get { return GetValue<string>(FullNameProperty); }
            set { SetValue(FullNameProperty, value); }
        }

        public static readonly PropertyData FullNameProperty = RegisterProperty("FullName", typeof (string), string.Empty);

        #endregion

        #region ShortName property

        [ViewModelToModel("LegalEntityModel")]
        public string ShortName
        {
            get { return GetValue<string>(ShortNameProperty); }
            set { SetValue(ShortNameProperty, value); }
        }

        public static readonly PropertyData ShortNameProperty = RegisterProperty("ShortName", typeof (string), string.Empty);

        #endregion
       

        #region LegalEntity model property

        [Model]
        public LegalEntity LegalEntityModel
        {
            get { return GetValue<LegalEntity>(LegalEntityModelProperty); }
            private set { SetValue(LegalEntityModelProperty, value); }
        }

        public static readonly PropertyData LegalEntityModelProperty = RegisterProperty("LegalEntityModel", typeof (LegalEntity));

        #endregion

        #endregion

        #region UI properties

        #region Window Title

        public override string Title => "Юридическое лицо";

        #endregion



        #region Nested ViewModels properties

        #region CitizenshipViewModel property

        public IViewModel CitizenshipViewModel
        {
            get { return GetValue<IViewModel>(CitizenshipViewModelProperty); }
            set { SetValue(CitizenshipViewModelProperty, value); }
        }

        public static readonly PropertyData CitizenshipViewModelProperty = RegisterProperty("CitizenshipViewModel", typeof(IViewModel));

        #endregion

        #region RegistrationCertificateViewModel property

        public IViewModel RegistrationCertificateViewModel
        {
            get { return GetValue<IViewModel>(RegistrationCertificateViewModelProperty); }
            set { SetValue(RegistrationCertificateViewModelProperty, value); }
        }

        public static readonly PropertyData RegistrationCertificateViewModelProperty = RegisterProperty("RegistrationCertificateViewModel", typeof (IViewModel), null,
            (sender, e) => ((LegalEntityWindowModel) sender).OnRegistrationCertificateViewModelChanged());

        private void OnRegistrationCertificateViewModelChanged()
        {
            // TODO: Implement logic
        }

        #endregion

        #region FormOfIncorporationViewModel property

        public IViewModel FormOfIncorporationViewModel
        {
            get { return GetValue<IViewModel>(FormOfIncorporationViewModelProperty); }
            set { SetValue(FormOfIncorporationViewModelProperty, value); }
        }

        public static readonly PropertyData FormOfIncorporationViewModelProperty = RegisterProperty("FormOfIncorporationViewModel", typeof (IViewModel));

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

        public static readonly PropertyData PhoneNumbersViewModelProperty = RegisterProperty("PhoneNumbersViewModel", typeof(IViewModel));

        #endregion

        #region EmailsViewModel property

        public IViewModel EmailsViewModel
        {
            get { return GetValue<IViewModel>(EmailsViewModelProperty); }
            set { SetValue(EmailsViewModelProperty, value); }
        }

        public static readonly PropertyData EmailsViewModelProperty = RegisterProperty("EmailsViewModel", typeof(IViewModel));

        #endregion

        #region AuthorizesDocumentsViewModel property

        public IViewModel AuthorizesDocumentsViewModel
        {
            get { return GetValue<IViewModel>(AuthorizesDocumentsViewModelProperty); }
            set { SetValue(AuthorizesDocumentsViewModelProperty, value); }
        }

        public static readonly PropertyData AuthorizesDocumentsViewModelProperty = RegisterProperty("AuthorizesDocumentsViewModel", typeof (IViewModel));

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

        #region ShareholderAccountsViewModel property

        public IViewModel ShareholderAccountsViewModel
        {
            get { return GetValue<IViewModel>(ShareholderAccountsViewModelProperty); }
            set { SetValue(ShareholderAccountsViewModelProperty, value); }
        }

        public static readonly PropertyData ShareholderAccountsViewModelProperty = RegisterProperty("ShareholderAccountsViewModel", typeof(IViewModel));

        #endregion

        #region IssuesOfSecuritiesViewModel property

        public IViewModel IssuesOfSecuritiesViewModel
        {
            get { return GetValue<IViewModel>(IssuesOfSecuritiesViewModelProperty); }
            set { SetValue(IssuesOfSecuritiesViewModelProperty, value); }
        }

        public static readonly PropertyData IssuesOfSecuritiesViewModelProperty = RegisterProperty("IssuesOfSecuritiesViewModel", typeof (IViewModel));

        #endregion

        #region FirstPersonOfCompanyViewModel property

        public IViewModel FirstPersonOfCompanyViewModel
        {
            get { return GetValue<IViewModel>(FirstPersonOfCompanyViewModelProperty); }
            set { SetValue(FirstPersonOfCompanyViewModelProperty, value); }
        }

        public static readonly PropertyData FirstPersonOfCompanyViewModelProperty = RegisterProperty("FirstPersonOfCompanyViewModel", typeof (IViewModel));

        #endregion
        
        #endregion

        protected override async Task InitializeAsync() { await base.InitializeAsync(); }
        protected override async Task CloseAsync() { await base.CloseAsync(); }
        

        #endregion

        #region Commands

        #region SaveAndCloseLegalEntityWindow command
        
        public Command<Type> SaveAndCloseLegalEntityWindowCommand { get; set ; }
        
        private void SaveAndCloseLegalEntityWindow(Type type)
        {
            var unitId = _unitService.SaveUnitToContext(LegalEntityModel);
            _unitService.AddUnitToHistory(LegalEntityModel);
            


            var task = this.SaveAndCloseViewModelAsync();
            task.Wait();

            _unitService.CreateNewItemInItemOfUnitForRestoreList(type, unitId);
        }

        #endregion
        
        #endregion

        #region Methods
        protected override void OnViewModelPropertyChanged(IViewModel viewModel, string propertyName)
        {
            if (propertyName == "TargetEntity")
            {
                if (viewModel is CitizenshipViewModel)
                {
                    var vm = (CitizenshipViewModel)viewModel;
                    Citizenship = vm.TargetEntity;
                }
                else if (viewModel is FormOfIncorporationViewModel)
                {
                    if (FormOfIncorporation.FullForm != null && FormOfIncorporation.FullForm != FormOfIncorporation.DefaultValue)
                    {
                        if (!string.IsNullOrEmpty(FullName))
                            _fullSubname = FullName.Substring(FormOfIncorporation.FullForm.Length + 2, FullName.Length - 1 - (FormOfIncorporation.FullForm.Length + 2));
                        if (!string.IsNullOrEmpty(ShortName))
                            _shortSubname = ShortName.Substring(FormOfIncorporation.ShortForm.Length + 2, ShortName.Length - 1 - (FormOfIncorporation.ShortForm.Length + 2));
                    }

                    var vm = (FormOfIncorporationViewModel)viewModel;
                    FormOfIncorporation = vm.TargetEntity;
                }
                else if (viewModel is FirstPersonOfCompanyViewModel)
                {
                    var vm = (FirstPersonOfCompanyViewModel) viewModel;
                    FirstPersonOfCompany = vm.TargetEntity;
                }
            }
        }

        protected void NameFormation()
        {
            if (FormOfIncorporation?.FullForm == null || FormOfIncorporation.FullForm == FormOfIncorporation.DefaultValue) return;
            
            if (_fullSubname != null) FullName = $"{FormOfIncorporation.FullForm} \"{_fullSubname}\"";
            if (_shortSubname != null) ShortName = $"{FormOfIncorporation.ShortForm} \"{_shortSubname}\"";
        }
        #endregion

    }
}