using Catel.Services;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.BaseClasses;
using PRC.PacketBatchFiller.Views.Person.CardIDIssuer;

namespace PRC.PacketBatchFiller.ViewModels.PersonEntity.CardIDIssuer
{
    public class CardIDIssuerSearchViewModel : SuggestFromLocalDataViewModelBase<Models.PersonsEntity.CardIDIssuer, CardIDIssuerEditWindowModel, CardIDIssuerEditWindow>
    {
        public CardIDIssuerSearchViewModel
            (
            Models.PersonsEntity.CardIDIssuer cardIDIssuer,
            IUIVisualizerService uiVisualizerService,
              IUnitService unitService
            )

            : base(
                    cardIDIssuer,
                    uiVisualizerService,  unitService
                  )
        {
           
        }

        


    }
}