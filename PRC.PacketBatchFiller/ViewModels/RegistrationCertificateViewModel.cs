using System;
using Catel.Data;
using Catel.MVVM;
using Catel.MVVM.Views;
using Catel.Services;
using PRC.PacketBatchFiller.Models.LegalEntityEntity;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.LegalEntityEntity.RegistrationCertificateIssuer;

namespace PRC.PacketBatchFiller.ViewModels
{
    [InterestedIn(typeof(RegistrationCertificateIssuerViewModel))]
    public class RegistrationCertificateViewModel : ViewModelBase
    {
        public RegistrationCertificateViewModel(
            RegistrationCertificate registrationCertificate, 
            string citizenship, 
            IUIVisualizerService uiVisualizerService, 
            IUnitService unitService
)
        {
     
        

            #region Initialization parameters 

            Citizenship = citizenship;

            RegistrationCertificateModel = registrationCertificate ?? new RegistrationCertificate();

            #endregion
            
            #region Setting defaults

            if (Citizenship != "Российская Федерация")
            {
                NumberMask = RegistrationCertificateModel.Number != null ? new string('A', RegistrationCertificateModel.Number.Length) : "A";
                NumberWatermark = "Номер государственной регистрации";
                DateWatermark = "Дата государственной регистрации";
                IssuerWatermark = "Орган, осуществивший регистрацию";
            }
            else
            {
                NumberMask = new string('A', 13);
                NumberWatermark = "ОГРН";
                DateWatermark = "Дата присвоения";
                IssuerWatermark = "Орган, присвоивший ОГРН";
            }

            #endregion

            #region Setting inner ViewModels

            RegistrationCertificateIssuerViewModel = new RegistrationCertificateIssuerViewModel(IssuerWatermark, RegistrationCertificateIssuer, uiVisualizerService, unitService);

            #endregion
        }

        public string IssuerWatermark { get; set; }

        #region RegistrationCertificate model & properties

        #region Number property

        [ViewModelToModel("RegistrationCertificateModel")]
        public string Number
        {
            get { return GetValue<string>(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly PropertyData NumberProperty = RegisterProperty("Number", typeof(string));

        #endregion

        #region IssueDate property

        [ViewModelToModel("RegistrationCertificateModel")]
        public DateTime? IssueDate
        {
            get { return GetValue<DateTime>(IssueDateProperty); }
            set { SetValue(IssueDateProperty, value); }
        }

        public static readonly PropertyData IssueDateProperty = RegisterProperty("IssueDate", typeof(DateTime));

        #endregion

        #region RegistrationCertificateIssuer property

        [ViewModelToModel("RegistrationCertificateModel")]
        public RegistrationCertificateIssuer RegistrationCertificateIssuer
        {
            get { return GetValue<RegistrationCertificateIssuer>(RegistrationCertificateIssuerProperty); }
            set { SetValue(RegistrationCertificateIssuerProperty, value); }
        }

        public static readonly PropertyData RegistrationCertificateIssuerProperty = RegisterProperty("RegistrationCertificateIssuer", typeof(RegistrationCertificateIssuer));

        #endregion


        #region RegistrationCertificateModel model property

        [Model, ViewToViewModel("RegistrationCertificate")]
        public RegistrationCertificate RegistrationCertificateModel
        {
            get { return GetValue<RegistrationCertificate>(RegistrationCertificateModelProperty); }
            private set { SetValue(RegistrationCertificateModelProperty, value); }
        }

        public static readonly PropertyData RegistrationCertificateModelProperty = RegisterProperty("RegistrationCertificateModel", typeof (RegistrationCertificate));

        #endregion

        #endregion

        #region UI properties

        #region DateWatermark property

        public string DateWatermark
        {
            get { return GetValue<string>(DateWatermarkProperty); }
            set { SetValue(DateWatermarkProperty, value); }
        }

        public static readonly PropertyData DateWatermarkProperty = RegisterProperty("DateWatermark", typeof (string));

        #endregion
        
        #region NumberMask property

        public string NumberMask
        {
            get { return GetValue<string>(NumberMaskProperty); }
            set { SetValue(NumberMaskProperty, value); }
        }

        public static readonly PropertyData NumberMaskProperty = RegisterProperty("NumberMask", typeof(string), new string('0', 13));

        #endregion

        #region RegistrationCertificateIssuerViewModel property

        public IViewModel RegistrationCertificateIssuerViewModel
        {
            get { return GetValue<IViewModel>(RegistrationCertificateIssuerViewModelProperty); }
            set { SetValue(RegistrationCertificateIssuerViewModelProperty, value); }
        }

        public static readonly PropertyData RegistrationCertificateIssuerViewModelProperty = RegisterProperty("RegistrationCertificateIssuerViewModel", typeof(IViewModel));

        #endregion

        #region NumberWatermark property

        public string NumberWatermark
        {
            get { return GetValue<string>(NumberWatermarkProperty); }
            set { SetValue(NumberWatermarkProperty, value); }
        }

        public static readonly PropertyData NumberWatermarkProperty = RegisterProperty("NumberWatermark", typeof (string));

        #endregion

        #region Citizenship property

        public string Citizenship
        {
            get { return GetValue<string>(CitizenshipProperty); }
            set { SetValue(CitizenshipProperty, value); }
        }

        public static readonly PropertyData CitizenshipProperty = RegisterProperty("Citizenship", typeof (string));

        #endregion

        #endregion

        #region Methods
        protected override void OnViewModelPropertyChanged(IViewModel viewModel, string propertyName)
        {
            if (propertyName == "TargetEntity" && viewModel is RegistrationCertificateIssuerViewModel)
            {
                var vm = (RegistrationCertificateIssuerViewModel)viewModel;
                RegistrationCertificateIssuer = vm.TargetEntity;
            }
            //    else if (propertyName == "TargetEntity" && viewModel is CitizenshipViewModel)
            //{
            //    var vm = (CitizenshipViewModel)viewModel;
            //    Citizenship = vm.TargetEntity;
            //}
        }
        #endregion


    }
}