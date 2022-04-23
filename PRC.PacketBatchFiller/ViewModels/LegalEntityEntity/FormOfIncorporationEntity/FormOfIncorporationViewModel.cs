using Catel.Services;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.BaseClasses;
using PRC.PacketBatchFiller.Views.LegalEntity.FormOfIncorporation;

namespace PRC.PacketBatchFiller.ViewModels.LegalEntityEntity.FormOfIncorporationEntity
{
    public class FormOfIncorporationViewModel : SuggestFromLocalDataViewModelBase<Models.LegalEntityEntity.FormOfIncorporation, FormOfIncorporationEditWindowModel, FormOfIncorporationEditWindow>
    {
        public FormOfIncorporationViewModel(
            Models.LegalEntityEntity.FormOfIncorporation formOfIncorporation, 
            IUIVisualizerService uiVisualizerService,
             IUnitService unitService
)
            : base (
                  formOfIncorporation, 
                  uiVisualizerService, unitService
                  )
        {
            
        }
    }
}