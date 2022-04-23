using System.Collections.ObjectModel;
using Catel.Data;
using Catel.MVVM;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.Models.Documents.ShareholderDocuments;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.BaseClasses;

namespace PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity.ShareholderTransferOrderEntity
{
    public class ShareholderTransferOrderViewModel : DocumentViewModelBase
    {
        private readonly IDocumentService _documentService;

        public ShareholderTransferOrderViewModel(ShareholderTransferOrder shareholderTransferOrder, IDocumentService documentService)
        {
            _documentService = documentService;

            ShareholderTransferOrderModel = shareholderTransferOrder ?? new ShareholderTransferOrder();

            FormName = ShareholderTransferOrder.FormName;
        }

        #region ShareholderTransferOrderModel model property

        [Model]
        public ShareholderTransferOrder ShareholderTransferOrderModel
        {
            get { return GetValue<ShareholderTransferOrder>(ShareholderTransferOrderModelProperty); }
            private set { SetValue(ShareholderTransferOrderModelProperty, value); }
        }

        public static readonly PropertyData ShareholderTransferOrderModelProperty = RegisterProperty("ShareholderTransferOrderModel", typeof (ShareholderTransferOrder));

        #endregion

        #region AuthorizedUnitsCollection property

        public ObservableCollection<Unit> AuthorizedUnitsCollection
        {
            get { return GetValue<ObservableCollection<Unit>>(AuthorizedUnitsCollectionProperty); }
            set { SetValue(AuthorizedUnitsCollectionProperty, value); }
        }

        public static readonly PropertyData AuthorizedUnitsCollectionProperty = RegisterProperty("AuthorizedUnitsCollection", typeof(ObservableCollection<Unit>));

        #endregion

        protected override async void EditShareholderDocumentCommandExecute()
        {
            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
            {
                var doc = await _documentService.OpenDocumentEditWindow(ShareholderTransferOrderModel, AuthorizedUnitsCollection);

                ShareholderTransferOrderModel = dbContextManager.Context.ShareholderTransferOrders.Find(doc.DocumentId);
            }
        }
    }
}