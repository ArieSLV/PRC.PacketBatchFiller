using Catel.Data;
using Catel.MVVM;
using PRC.PacketBatchFiller.Models.BaseClasses;
using PRC.PacketBatchFiller.Models.BaseClasses.Documents;
using PRC.PacketBatchFiller.Models.Documents.ShareholderDocuments;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.BaseClasses;

namespace PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity.ShareholderAuthorizesDocumentEntity
{
    public class ShareholderAuthorizesDocumentViewModel : DocumentViewModelBase
    {
        private readonly IDocumentService _documentService;

        public ShareholderAuthorizesDocumentViewModel(ShareholderAuthorizesDocument shareholderAuthorizesDocument, IDocumentService documentService)
        {
            _documentService = documentService;

            ShareholderAuthorizesDocumentModel = shareholderAuthorizesDocument ?? new ShareholderAuthorizesDocument();

            FormName = ShareholderAuthorizesDocument.FormName;
        }

        #region WhoGivingAuthority property

        [ViewModelToModel("ShareholderAuthorizesDocumentModel")]
        public virtual ShareholderAccount WhoGivingAuthority
        {
            get { return GetValue<ShareholderAccount>(WhoGivingAuthorityProperty); }
            set { SetValue(WhoGivingAuthorityProperty, value); }
        }

        public static readonly PropertyData WhoGivingAuthorityProperty = RegisterProperty("WhoGivingAuthority", typeof(ShareholderAccount));

        #endregion

        #region AuthorizesDocumentType property

        [ViewModelToModel("ShareholderAuthorizesDocumentModel")]
        public virtual AuthorizesDocumentType AuthorizesDocumentType
        {
            get { return GetValue<AuthorizesDocumentType>(AuthorizesDocumentTypeProperty); }
            set { SetValue(AuthorizesDocumentTypeProperty, value); }
        }

        public static readonly PropertyData AuthorizesDocumentTypeProperty = RegisterProperty("AuthorizesDocumentType", typeof(AuthorizesDocumentType));

        #endregion

        


        #region ShareholderAuthorizesDocumentModel model property

        [Model]
        public ShareholderAuthorizesDocument ShareholderAuthorizesDocumentModel
        {
            get { return GetValue<ShareholderAuthorizesDocument>(ShareholderAuthorizesDocumentModelProperty); }
            private set { SetValue(ShareholderAuthorizesDocumentModelProperty, value); }
        }

        public static readonly PropertyData ShareholderAuthorizesDocumentModelProperty = RegisterProperty("ShareholderAuthorizesDocumentModel", typeof (ShareholderAuthorizesDocument));

        #endregion


       
        protected override async void EditShareholderDocumentCommandExecute()
        {
            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
            {
                var doc = await _documentService.OpenDocumentEditWindow(ShareholderAuthorizesDocumentModel, null);

                ShareholderAuthorizesDocumentModel = dbContextManager.Context.ShareholderAuthorizesDocuments.Find(doc.DocumentId);
            }
        }
    }
}