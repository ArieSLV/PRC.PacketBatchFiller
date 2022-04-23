using System.Linq;
using System.Threading.Tasks;
using Catel.Data;
using Catel.MVVM;
using Catel.Services;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.LegalEntityEntity;
using PRC.PacketBatchFiller.ViewModels.PersonEntity;

namespace PRC.PacketBatchFiller.ViewModels.BaseClasses
{
    public class UnitSearchBaseViewModel : SuggestFromLocalDataViewModelBase<Unit, AddUnitWindowModel, object>
    {
    
        private readonly IUnitService _unitService;
        private readonly ICommandManager _commandManager;

        public UnitSearchBaseViewModel(
            Unit tEntity, 
            IUIVisualizerService uiVisualizerService,
            IUnitService unitService, 
            ICommandManager commandManager) 
            : base(
                  tEntity, 
                  uiVisualizerService, 
                  unitService)
        {
            _unitService = unitService;
            _commandManager = commandManager;
        }

        //protected async Task<Models.Unit> AddLegalEntityTask()
        //{
        //    if (this.GetRootIViewModel() is LegalEntityWindowModel) _commandManager.GetCommand("SaveAndCloseLegalEntityWindowCommand").Execute(null);
        //    else if (this.GetRootIViewModel() is PersonWindowModel) _commandManager.GetCommand("SaveAndClosePersonWindowCommand").Execute(null);

        //    Models.LegalEntity legalEntity;
        //    using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
        //    {
        //        var isRealName = false;

        //        foreach (var entity in dbContextManager.Context.FormsOfIncorporation.ToList())
        //        {
        //            if (SearchText.ToLower().Contains(entity.FullForm.ToLower())) isRealName = true;
        //        }

        //        var asFullName = isRealName ? SearchText : string.Empty;

        //        var newLegalEntity = new Models.LegalEntity
        //        {
        //            FullName = asFullName
        //        };

        //        legalEntity = (Models.LegalEntity) await _unitService.OpenUnitWindow(newLegalEntity);

        //        LoadReferenceBookFromContext();

        //        _unitService.SaveUnitForRestoreInSuggestList(legalEntity);

        //        await _unitService.OpenUnitWindow(_unitService.GetLastUnitFromHistory());
        //    }

        //    return legalEntity;
        //}



        protected override async Task<Unit> AddItemToCollection()
        {
            if (this.GetRootIViewModel() is LegalEntityWindowModel) _commandManager.GetCommand("SaveAndCloseLegalEntityWindowCommand").Execute(GetType());
            else if (this.GetRootIViewModel() is PersonWindowModel) _commandManager.GetCommand("SaveAndClosePersonWindowCommand").Execute(GetType());

            LoadReferenceBookFromContext();

            await _unitService.OpenUnitWindow(_unitService.GetLastUnitFromHistory());

            return null;
        }
    }
}
