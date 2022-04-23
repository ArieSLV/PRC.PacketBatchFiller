using System;
using System.Collections.ObjectModel;
using Catel.Data;
using Catel.MVVM;
using Catel.Services;
using PRC.PacketBatchFiller.Models.BaseClasses.Documents;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.Models.Documents.ShareholderDocuments;
using PRC.PacketBatchFiller.Services.Interfaces;

namespace PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity.ShareholderAuthorizesDocumentEntity
{
    [InterestedIn(typeof(AuthorizesDocumentTypeViewModel))]
    public class ShareholderAuthorizesDocumentEditWindowModel : ShareholderAuthorizesDocumentViewModel
    {
        public ShareholderAuthorizesDocumentEditWindowModel(ShareholderAuthorizesDocument shareholderAuthorizesDocument, IDocumentService documentService, IUIVisualizerService uiVisualizerService, IUnitService unitService, ICommandManager commandManager) : base(shareholderAuthorizesDocument, documentService)
        {
            AuthorizesDocumentType = shareholderAuthorizesDocument.AuthorizesDocumentType ?? new AuthorizesDocumentType();
            AuthorizedUnits = shareholderAuthorizesDocument.AuthorizedUnits ?? new ObservableCollection<Unit>();

            AuthorizesDocumentTypeViewModel = new AuthorizesDocumentTypeViewModel(AuthorizesDocumentType, uiVisualizerService, unitService);
            AuthorizedUnitsViewModel = new AuthorizedUnitsViewModel(AuthorizedUnits, uiVisualizerService, unitService, commandManager);
        }



        #region Number property

        [ViewModelToModel("ShareholderAuthorizesDocumentModel")]
        public string Number
        {
            get { return GetValue<string>(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly PropertyData NumberProperty = RegisterProperty("Number", typeof(string));

        #endregion

        #region StartDate property

        [ViewModelToModel("ShareholderAuthorizesDocumentModel")]
        public DateTime? StartDate
        {
            get { return GetValue<DateTime?>(StartDateProperty); }
            set { SetValue(StartDateProperty, value); }
        }

        public static readonly PropertyData StartDateProperty = RegisterProperty("StartDate", typeof(DateTime?));

        #endregion

        #region EndDate property

        [ViewModelToModel("ShareholderAuthorizesDocumentModel")]
        public DateTime? EndDate
        {
            get { return GetValue<DateTime?>(EndDateProperty); }
            set { SetValue(EndDateProperty, value); }
        }

        public static readonly PropertyData EndDateProperty = RegisterProperty("EndDate", typeof(DateTime?));

        #endregion

        #region AuthorizedUnits property

        [ViewModelToModel("ShareholderAuthorizesDocumentModel")]
        public ObservableCollection<Unit> AuthorizedUnits
        {
            get { return GetValue<ObservableCollection<Unit>>(AuthorizedUnitsProperty); }
            set { SetValue(AuthorizedUnitsProperty, value); }
        }

        public static readonly PropertyData AuthorizedUnitsProperty = RegisterProperty("AuthorizedUnits", typeof(ObservableCollection<Unit>));

        #endregion



        #region UI properies

        #region AuthorizesDocumentTypeViewModel property

        public IViewModel AuthorizesDocumentTypeViewModel
        {
            get { return GetValue<IViewModel>(AuthorizesDocumentTypeViewModelProperty); }
            set { SetValue(AuthorizesDocumentTypeViewModelProperty, value); }
        }

        public static readonly PropertyData AuthorizesDocumentTypeViewModelProperty = RegisterProperty("AuthorizesDocumentTypeViewModel", typeof (IViewModel));

        #endregion

        #region AuthorizedUnitsViewModel property

        public IViewModel AuthorizedUnitsViewModel
        {
            get { return GetValue<IViewModel>(AuthorizedUnitsViewModelProperty); }
            set { SetValue(AuthorizedUnitsViewModelProperty, value); }
        }

        public static readonly PropertyData AuthorizedUnitsViewModelProperty = RegisterProperty("AuthorizedUnitsViewModel", typeof (IViewModel));

        #endregion

        #endregion

        #region Methods
        protected override void OnViewModelPropertyChanged(IViewModel viewModel, string propertyName)
        {
            if (propertyName == "TargetEntity")
            {
                var authorizesDocumentTypeViewModel = viewModel as AuthorizesDocumentTypeViewModel;
                if (authorizesDocumentTypeViewModel != null) { AuthorizesDocumentType = authorizesDocumentTypeViewModel.TargetEntity; }
            }
        }
        #endregion
    }
}