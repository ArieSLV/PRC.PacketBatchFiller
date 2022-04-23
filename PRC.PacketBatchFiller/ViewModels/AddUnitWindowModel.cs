using System;
using Catel.Data;
using Catel.MVVM;
using PRC.PacketBatchFiller.Models;
using PRC.PacketBatchFiller.Models.LegalEntityEntity;
using PRC.PacketBatchFiller.Models.PersonsEntity;
using PRC.PacketBatchFiller.Services.Interfaces;

namespace PRC.PacketBatchFiller.ViewModels
{
    public class AddUnitWindowModel : ViewModelBase
    {
        private readonly Type _type;
        private readonly long _targetUnitId;
        private readonly IUnitService _unitService;

        public AddUnitWindowModel(Type type, long targetUnitId, IUnitService unitService)
        {
            _type = type;
            _targetUnitId = targetUnitId;
            _unitService = unitService;

            AddLegalEntityCommand = new Command(AddLegalEntity);
            AddPersonCommand = new Command(AddPerson);
        }

        public override string Title => "Выбор типа лица";

        #region AddLegalEntity command

        public Command AddLegalEntityCommand { get; set; }

        private async void AddLegalEntity()
        {
            var closeViewModelTask = CloseViewModelAsync(await SaveAsync());
            closeViewModelTask.Wait();

            var legalEntity = (LegalEntity)await _unitService.OpenUnitWindow(new LegalEntity());

            _unitService.AddUnitIdToItemInItemOfUnitForRestoreList(_type, _targetUnitId, legalEntity.UnitId);
        }

        #endregion

        #region AddPerson command

        public Command AddPersonCommand { get; set; }

        private async void AddPerson()
        {
            var closeViewModelTask = CloseViewModelAsync(await SaveAsync());
            closeViewModelTask.Wait();

            var person = (Person) await _unitService.OpenUnitWindow(new Person());

            _unitService.AddUnitIdToItemInItemOfUnitForRestoreList(_type, _targetUnitId, person.UnitId);
        }

        #endregion
     
    }
}