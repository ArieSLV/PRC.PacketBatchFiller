using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Catel.Data;
using Catel.MVVM;
using Catel.Services;
using PRC.PacketBatchFiller.Models;
using PRC.PacketBatchFiller.Models.LegalEntityEntity;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.BaseClasses;
using PRC.PacketBatchFiller.ViewModels.LegalEntityEntity;
using PRC.PacketBatchFiller.ViewModels.PersonEntity;

namespace PRC.PacketBatchFiller.ViewModels.Suggest
{
    public class SecuritiesIssuerSearchViewModel :
        SuggestFromLocalDataViewModelBase<LegalEntity, LegalEntityWindowModel, Views.LegalEntityWindow>
    {
        private readonly ICommandManager _commandManager;
        private readonly IUnitService _unitService;

        public SecuritiesIssuerSearchViewModel(
            LegalEntity tEntity,
            IUIVisualizerService uiVisualizerService,
            IUnitService unitService, ICommandManager commandManager
            )
            : base(
                tEntity,
                uiVisualizerService, unitService
                )
        {
            _commandManager = commandManager;
            _unitService = unitService;
        }

        protected override void LoadReferenceBookFromContext()
        {
            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
            {
                ItemsCollection = new ObservableCollection<LegalEntity>(dbContextManager.Context.LegalEntities
                    .Where(o => o.RoleIsSecuritiesIssuerFlag)
                    .OrderBy(o => o.FullName));
            }
        }

        protected override async Task<LegalEntity> AddItemToCollection()
        {
            _unitService.SetSearchText(SearchText);

            if (this.GetRootIViewModel() is LegalEntityWindowModel) _commandManager.GetCommand("SaveAndCloseLegalEntityWindowCommand").Execute(GetType());
            else if (this.GetRootIViewModel() is PersonWindowModel) _commandManager.GetCommand("SaveAndClosePersonWindowCommand").Execute(GetType());

            

            //LegalEntity legalEntity;
            //using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
            //{
               

            //    legalEntity = (LegalEntity) await _unitService.OpenUnitWindow(newLegalEntity);

            //    LoadReferenceBookFromContext();

            //    _unitService.CreateNewItemInItemOfUnitForRestoreList(legalEntity.GetType(), legalEntity.UnitId);

            //    await _unitService.OpenUnitWindow(_unitService.GetLastUnitFromHistory());
            //}

            return new LegalEntity();
        }
    }
}