using Catel.MVVM;
using Catel.Services;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.BaseClasses;

namespace PRC.PacketBatchFiller.ViewModels.UnitEntity
{
    public class UnitSearchViewModel : UnitSearchBaseViewModel
    {
        public UnitSearchViewModel(Unit tEntity, IUIVisualizerService uiVisualizerService, IUnitService unitService, ICommandManager commandManager) : base(tEntity, uiVisualizerService, unitService, commandManager)
        {
        }
    }
}