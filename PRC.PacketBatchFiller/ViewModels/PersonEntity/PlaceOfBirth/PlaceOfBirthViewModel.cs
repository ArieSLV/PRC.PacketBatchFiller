using Catel.Services;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.BaseClasses;
using PRC.PacketBatchFiller.Views.Person.PlaceOfBirth;

namespace PRC.PacketBatchFiller.ViewModels.PersonEntity.PlaceOfBirth
{
    public class PlaceOfBirthViewModel :
        SuggestFromLocalDataViewModelBase<Models.PersonsEntity.PlaceOfBirth, PlaceOfBirthEditWindowModel, PlaceOfBirthEditWindow>
    {
        public PlaceOfBirthViewModel
            (
            Models.PersonsEntity.PlaceOfBirth placeOfBirth,
            IUIVisualizerService uiVisualizerService,
             IUnitService unitService

            )
            : base(
                placeOfBirth,
                uiVisualizerService,  unitService
                )
        {

        }
    }
}