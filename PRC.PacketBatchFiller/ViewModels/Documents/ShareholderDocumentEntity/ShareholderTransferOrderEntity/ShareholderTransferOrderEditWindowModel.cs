using System.Collections.ObjectModel;
using Catel.Data;
using Catel.MVVM;
using Catel.Services;
using PRC.PacketBatchFiller.Models.BaseClasses;
using PRC.PacketBatchFiller.Models.Documents;
using PRC.PacketBatchFiller.Models.Documents.ShareholderDocuments;
using PRC.PacketBatchFiller.Models.LegalEntityEntity;
using PRC.PacketBatchFiller.Services.Interfaces;

namespace PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity.ShareholderTransferOrderEntity
{
    [InterestedIn(typeof(DebitingAccountSearchViewModel))]
    [InterestedIn(typeof(CreditingAccountSearchViewModel))]
    public class ShareholderTransferOrderEditWindowModel : ShareholderTransferOrderViewModel
    {
        public ShareholderTransferOrderEditWindowModel(ShareholderTransferOrder shareholderTransferOrder, IDocumentService documentService, IUIVisualizerService uiVisualizerService) 
            : base(shareholderTransferOrder, documentService)
        {
            TransferReasonDocuments = ShareholderTransferOrderModel.TransferReasonDocuments ?? new ObservableCollection<TransferReasonDocument>();

            TransferReasonDocumentsViewModel = new TransferReasonDocumentsViewModel(TransferReasonDocuments, uiVisualizerService);
            DebitingAccountSearchViewModel = new DebitingAccountSearchViewModel(DebitingAccount, uiVisualizerService, null);
            CreditingAccountSearchViewModel = new CreditingAccountSearchViewModel(CreditingAccount, uiVisualizerService, null);
        }

        #region Entity properies

        #region IssueOfSecurities property

        [ViewModelToModel("ShareholderTransferOrderModel")]
        public IssueOfSecurities IssueOfSecurities
        {
            get { return GetValue<IssueOfSecurities>(IssueOfSecuritiesProperty); }
            set { SetValue(IssueOfSecuritiesProperty, value); }
        }

        public static readonly PropertyData IssueOfSecuritiesProperty = RegisterProperty("IssueOfSecurities", typeof(IssueOfSecurities));

        #endregion

        #region QuantityOfTransferedSecurities property

        [ViewModelToModel("ShareholderTransferOrderModel")]
        public string QuantityOfTransferedSecurities
        {
            get { return GetValue<string>(QuantityOfTransferedSecuritiesProperty); }
            set { SetValue(QuantityOfTransferedSecuritiesProperty, value); }
        }

        public static readonly PropertyData QuantityOfTransferedSecuritiesProperty = RegisterProperty("QuantityOfTransferedSecurities", typeof(string));

        #endregion

        #region TransferReasonDocuments property

        [ViewModelToModel("ShareholderTransferOrderModel")]
        public ObservableCollection<TransferReasonDocument> TransferReasonDocuments
        {
            get { return GetValue<ObservableCollection<TransferReasonDocument>>(TransferReasonDocumentsProperty); }
            set { SetValue(TransferReasonDocumentsProperty, value); }
        }

        public static readonly PropertyData TransferReasonDocumentsProperty = RegisterProperty("TransferReasonDocuments", typeof(ObservableCollection<TransferReasonDocument>));

        #endregion

        #region AmountOfTransaction property

        [ViewModelToModel("ShareholderTransferOrderModel")]
        public string AmountOfTransaction
        {
            get { return GetValue<string>(AmountOfTransactionProperty); }
            set { SetValue(AmountOfTransactionProperty, value); }
        }

        public static readonly PropertyData AmountOfTransactionProperty = RegisterProperty("AmountOfTransaction", typeof(string));

        #endregion

        #region CashPaymentFlag property

        [ViewModelToModel("ShareholderTransferOrderModel")]
        public bool CashPaymentFlag
        {
            get { return GetValue<bool>(CashPaymentFlagProperty); }
            set { SetValue(CashPaymentFlagProperty, value); }
        }

        public static readonly PropertyData CashPaymentFlagProperty = RegisterProperty("CashPaymentFlag", typeof(bool));

        #endregion

        #region DebitingAccount property

        [ViewModelToModel("ShareholderTransferOrderModel")]
        public virtual ShareholderAccount DebitingAccount
        {
            get { return GetValue<ShareholderAccount>(DebitingAccountProperty); }
            set { SetValue(DebitingAccountProperty, value); }
        }

        public static readonly PropertyData DebitingAccountProperty = RegisterProperty("DebitingAccount", typeof(ShareholderAccount));

        #endregion

        #region CreditingAccount property

        [ViewModelToModel("ShareholderTransferOrderModel")]
        public virtual ShareholderAccount CreditingAccount
        {
            get { return GetValue<ShareholderAccount>(CreditingAccountProperty); }
            set { SetValue(CreditingAccountProperty, value); }
        }

        public static readonly PropertyData CreditingAccountProperty = RegisterProperty("CreditingAccount", typeof(ShareholderAccount));

        #endregion

        #endregion

        
        #region UI properies
        
        #region TransferReasonDocumentsViewModel property

        public IViewModel TransferReasonDocumentsViewModel
        {
            get { return GetValue<IViewModel>(TransferReasonDocumentsViewModelProperty); }
            set { SetValue(TransferReasonDocumentsViewModelProperty, value); }
        }

        public static readonly PropertyData TransferReasonDocumentsViewModelProperty = RegisterProperty("TransferReasonDocumentsViewModel", typeof (IViewModel));

        #endregion

        #region DebitingAccountSearchViewModel property

        public IViewModel DebitingAccountSearchViewModel
        {
            get { return GetValue<IViewModel>(DebitingAccountSearchViewModelProperty); }
            set { SetValue(DebitingAccountSearchViewModelProperty, value); }
        }

        public static readonly PropertyData DebitingAccountSearchViewModelProperty = RegisterProperty("DebitingAccountSearchViewModel", typeof (IViewModel));

        #endregion

        #region CreditingAccountSearchViewModel property

        public IViewModel CreditingAccountSearchViewModel
        {
            get { return GetValue<IViewModel>(CreditingAccountSearchViewModelProperty); }
            set { SetValue(CreditingAccountSearchViewModelProperty, value); }
        }

        public static readonly PropertyData CreditingAccountSearchViewModelProperty = RegisterProperty("CreditingAccountSearchViewModel", typeof (IViewModel));

        #endregion

        #endregion

        #region Methods
        protected override void OnViewModelPropertyChanged(IViewModel viewModel, string propertyName)
        {
            if (propertyName == "TargetEntity")
            {
                var debitingAccountSearchViewModel = viewModel as DebitingAccountSearchViewModel;
                var creditingAccountSearchViewModel = viewModel as CreditingAccountSearchViewModel;

                if (debitingAccountSearchViewModel != null) { DebitingAccount = debitingAccountSearchViewModel.TargetEntity; }
                if (creditingAccountSearchViewModel != null) { CreditingAccount = creditingAccountSearchViewModel.TargetEntity; }
            }
        }
        #endregion
    }
}
