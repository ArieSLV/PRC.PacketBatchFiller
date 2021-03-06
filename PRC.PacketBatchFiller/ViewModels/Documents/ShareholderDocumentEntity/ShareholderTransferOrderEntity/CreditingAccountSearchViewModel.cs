using Catel.Services;
using PRC.PacketBatchFiller.Models.BaseClasses;
using PRC.PacketBatchFiller.Services.Interfaces;

namespace PRC.PacketBatchFiller.ViewModels.Documents.ShareholderDocumentEntity.ShareholderTransferOrderEntity
{
    public class CreditingAccountSearchViewModel : ShareholderAccountSearchViewModel
    {
        public CreditingAccountSearchViewModel(ShareholderAccount tEntity, IUIVisualizerService uiVisualizerService, IUnitService unitService) : 
            base(tEntity, uiVisualizerService, unitService)
        {
        }
    }
}