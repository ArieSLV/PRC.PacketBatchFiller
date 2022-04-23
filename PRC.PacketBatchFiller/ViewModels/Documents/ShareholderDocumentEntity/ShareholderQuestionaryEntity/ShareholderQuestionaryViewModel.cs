using System.Collections.ObjectModel;
using Catel.Data;
using Catel.MVVM;
using PRC.PacketBatchFiller.Models;
using PRC.PacketBatchFiller.Models.BaseClasses;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.Models.Documents.ShareholderDocuments;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.BaseClasses;

namespace PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity.ShareholderQuestionaryEntity
{
    public class ShareholderQuestionaryViewModel : DocumentViewModelBase
    {
        private readonly IDocumentService _documentService;

        public ShareholderQuestionaryViewModel(ShareholderQuestionary shareholderQuestionary, IDocumentService documentService)
        {
            _documentService = documentService;

            ShareholderQuestionaryModel = shareholderQuestionary ?? new ShareholderQuestionary();

            FormName = ShareholderQuestionary.FormName;
        }
        
        #region ShareholderAccount property

        [ViewModelToModel("ShareholderQuestionaryModel")]
        public virtual ShareholderAccount ShareholderAccount
        {
            get { return GetValue<ShareholderAccount>(ShareholderAccountProperty); }
            set { SetValue(ShareholderAccountProperty, value); }
        }

        public static readonly PropertyData ShareholderAccountProperty = RegisterProperty("ShareholderAccount", typeof(ShareholderAccount));

        #endregion

        #region ShareholderQuestionaryModel model property

        [Model]
        public ShareholderQuestionary ShareholderQuestionaryModel
        {
            get { return GetValue<ShareholderQuestionary>(ShareholderQuestionaryModelProperty); }
            private set { SetValue(ShareholderQuestionaryModelProperty, value); }
        }

        public static readonly PropertyData ShareholderQuestionaryModelProperty = RegisterProperty("ShareholderQuestionaryModel", typeof (ShareholderQuestionary));

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
                var doc = await _documentService.OpenDocumentEditWindow(ShareholderQuestionaryModel, AuthorizedUnitsCollection);

                ShareholderQuestionaryModel = dbContextManager.Context.ShareholderQuestionaries.Find(doc.DocumentId);
            }
        }
    }
}