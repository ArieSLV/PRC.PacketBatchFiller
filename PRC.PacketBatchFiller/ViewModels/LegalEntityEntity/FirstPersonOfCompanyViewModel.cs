using Catel.MVVM;
using Catel.Services;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.Services.Interfaces;

namespace PRC.PacketBatchFiller.ViewModels.LegalEntityEntity
{
    public class FirstPersonOfCompanyViewModel : BaseClasses.UnitSearchBaseViewModel
    {
        public FirstPersonOfCompanyViewModel(Unit tEntity, IUIVisualizerService uiVisualizerService, IUnitService unitService, ICommandManager commandManager) 
            : base(tEntity, uiVisualizerService, unitService, commandManager)
        {
        }
    }
}