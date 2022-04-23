using Catel.Services;
using PRC.PacketBatchFiller.Models.BaseClasses.Documents;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.BaseClasses;
using PRC.PacketBatchFiller.Views;

namespace PRC.PacketBatchFiller.ViewModels.Documents
{
    public class AuthorizesDocumentTypeViewModel : SuggestFromLocalDataViewModelBase<AuthorizesDocumentType, AuthorizesDocumentTypeEditWindowModel, AuthorizesDocumentTypeEditWindow>
    {
        public AuthorizesDocumentTypeViewModel(
            AuthorizesDocumentType tEntity, 
            IUIVisualizerService uiVisualizerService, 
            IUnitService unitService) 
            
            : base (
                  tEntity, 
                  uiVisualizerService, 
                  unitService
                  )
        {

        }
    }
}