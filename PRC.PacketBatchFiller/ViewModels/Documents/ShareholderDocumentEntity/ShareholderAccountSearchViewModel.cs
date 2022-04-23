using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using Catel.Data;
using Catel.Services;
using PRC.PacketBatchFiller.Models.BaseClasses;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.BaseClasses;

namespace PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity
{
    public class ShareholderAccountSearchViewModel : SuggestFromLocalDataViewModelBase<ShareholderAccount, object, object>
    {
        public ShareholderAccountSearchViewModel(ShareholderAccount tEntity, IUIVisualizerService uiVisualizerService, IUnitService unitService) : base(tEntity, uiVisualizerService, unitService)
        {

        }

        protected override void LoadReferenceBookFromContext()
        {
            var dbContextManager = DbContextManager<PBFContext>.GetManager();
            
            ItemsCollection = new ObservableCollection<ShareholderAccount>(dbContextManager.Context.ShareholderAccounts
                .Include(o => o.Unit)
                .Include(o => o.SecuritiesIssuer)
                .OrderByDescending(o => o.Unit.TimeStamp));
        }
    }
}