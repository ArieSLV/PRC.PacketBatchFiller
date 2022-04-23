using System;
using System.Collections.ObjectModel;
using Catel.Data;
using Catel.Services;
using PRC.PacketBatchFiller.Annotations;
using PRC.PacketBatchFiller.Models;
using PRC.PacketBatchFiller.Models.BaseClasses.Documents;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.Models.Documents;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.Documents;

namespace PRC.PacketBatchFiller.ViewModels
{
    using Catel.MVVM;
    using System.Threading.Tasks;

    [InterestedIn(typeof(AuthorizesDocumentTypeViewModel))]
    public class AuthorizesDocumentWindowModel : ViewModelBase
    {
        public AuthorizesDocumentWindowModel(AuthorizesDocument authorizesDocument, IUIVisualizerService uiVisualizerService, IUnitService unitService)
        {
            AuthorizesDocumentModel = authorizesDocument ?? new AuthorizesDocument();

            AuthorizesDocumentTypeEditWindowModel = new AuthorizesDocumentTypeViewModel(AuthorizesDocumentModel.AuthorizesDocumentType, uiVisualizerService, unitService);
        }

        #region AuthorizesDocument properties

        #region WhoGivingAuthority property

        [ViewModelToModel("AuthorizesDocumentModel")]
        public Unit WhoGivingAuthority
        {
            get { return GetValue<Unit>(WhoGivingAuthorityProperty); }
            set { SetValue(WhoGivingAuthorityProperty, value); }
        }

        public static readonly PropertyData WhoGivingAuthorityProperty = RegisterProperty("WhoGivingAuthority", typeof(Unit));

        #endregion

        #region AuthorizedUnits property

        [ViewModelToModel("AuthorizesDocumentModel")]
        [CanBeNull]
        public ObservableCollection<Unit> AuthorizedUnits
        {
            get { return GetValue<ObservableCollection<Unit>>(AuthorizedUnitsProperty); }
            set { SetValue(AuthorizedUnitsProperty, value); }
        }

        public static readonly PropertyData AuthorizedUnitsProperty = RegisterProperty("AuthorizedUnits", typeof(ObservableCollection<Unit>));

        #endregion

        #region Number property

        [ViewModelToModel("AuthorizesDocumentModel")]
        public string Number
        {
            get { return GetValue<string>(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly PropertyData NumberProperty = RegisterProperty("Number", typeof(string));

        #endregion

        #region StartDate property

        [ViewModelToModel("AuthorizesDocumentModel")]
        public DateTime? StartDate
        {
            get { return GetValue<DateTime?>(StartDateProperty); }
            set { SetValue(StartDateProperty, value); }
        }

        public static readonly PropertyData StartDateProperty = RegisterProperty("StartDate", typeof(DateTime?));

        #endregion

        #region EndDate property

        [ViewModelToModel("AuthorizesDocumentModel")]
        public DateTime? EndDate
        {
            get { return GetValue<DateTime?>(EndDateProperty); }
            set { SetValue(EndDateProperty, value); }
        }

        public static readonly PropertyData EndDateProperty = RegisterProperty("EndDate", typeof(DateTime?));

        #endregion

        #region AuthorizesDocumentType property

        [ViewModelToModel("AuthorizesDocumentModel")]
        public AuthorizesDocumentType AuthorizesDocumentType
        {
            get { return GetValue<AuthorizesDocumentType>(AuthorizesDocumentTypeProperty); }
            set { SetValue(AuthorizesDocumentTypeProperty, value); }
        }

        public static readonly PropertyData AuthorizesDocumentTypeProperty = RegisterProperty("AuthorizesDocumentType", typeof(AuthorizesDocumentType));

        #endregion


        #region AuthorizesDocumentModel model property

        [Model]
        public AuthorizesDocument AuthorizesDocumentModel
        {
            get { return GetValue<AuthorizesDocument>(AuthorizesDocumentModelProperty); }
            private set { SetValue(AuthorizesDocumentModelProperty, value); }
        }

        public static readonly PropertyData AuthorizesDocumentModelProperty = RegisterProperty("AuthorizesDocumentModel", typeof (AuthorizesDocument));

        #endregion

        #endregion
        
        #region Nested ViewModel properties

        #region AuthorizesDocumentTypeEditWindowModel property

        public IViewModel AuthorizesDocumentTypeEditWindowModel
        {
            get { return GetValue<IViewModel>(AuthorizesDocumentTypeEditWindowModelProperty); }
            set { SetValue(AuthorizesDocumentTypeEditWindowModelProperty, value); }
        }

        public static readonly PropertyData AuthorizesDocumentTypeEditWindowModelProperty = RegisterProperty("AuthorizesDocumentTypeEditWindowModel", typeof (IViewModel));

        #endregion

        #endregion



        public override string Title => "Ввод уполномоченного лица";
        protected override async Task InitializeAsync() { await base.InitializeAsync(); }
        protected override async Task CloseAsync() { await base.CloseAsync(); }


        #region Methods
        protected override void OnViewModelPropertyChanged(IViewModel viewModel, string propertyName)
        {
            if (propertyName == "TargetEntity" && viewModel is AuthorizesDocumentTypeViewModel)
            {
                var vm = (AuthorizesDocumentTypeViewModel)viewModel;
                AuthorizesDocumentType = vm.TargetEntity;
            }
        }
        #endregion

    }
}
