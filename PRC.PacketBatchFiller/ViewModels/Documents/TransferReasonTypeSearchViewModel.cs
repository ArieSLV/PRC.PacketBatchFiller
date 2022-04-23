using Catel.Services;
using PRC.PacketBatchFiller.Models.Documents;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.BaseClasses;
using PRC.PacketBatchFiller.Views;

namespace PRC.PacketBatchFiller.ViewModels.Documents
{
    public class TransferReasonTypeSearchViewModel : SuggestFromLocalDataViewModelBase<TransferReasonType, TransferReasonTypeEditWindowModel, TransferReasonTypeEditWindow>
    {
        public TransferReasonTypeSearchViewModel(TransferReasonType tEntity, IUIVisualizerService uiVisualizerService, IUnitService unitService) 
            : base(tEntity, uiVisualizerService, unitService)
        {
        }
    }
}