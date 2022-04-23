using Catel.Data;
using Catel.MVVM;
using PRC.PacketBatchFiller.Models.LegalEntityEntity;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.BaseClasses;

namespace PRC.PacketBatchFiller.ViewModels.LegalEntityEntity
{
    public class ForItemCollectionLegalEntityViewModel : UnitAsItemForViewModelCollection
    {
        private readonly IUnitService _unitService;

        public ForItemCollectionLegalEntityViewModel(LegalEntity legalEntity, IUnitService unitService)
        {
            _unitService = unitService;

            LegalEntityModel = legalEntity;
         

            UnitName = LegalEntityModel.ToString();

            OpenUnitEditWindowCommand = new Command(OpenUnitEditWindowExecute);
        }


        #region LegalEntity model property

        public LegalEntity LegalEntityModel
        {
            get { return GetValue<LegalEntity>(LegalEntityModelProperty); }
            private set { SetValue(LegalEntityModelProperty, value); }
        }

        public static readonly PropertyData LegalEntityModelProperty = RegisterProperty("LegalEntityModel", typeof(LegalEntity));

        #endregion

        #region OpenUnitEditWindow command

        public Command OpenUnitEditWindowCommand { get; set; }

        private void OpenUnitEditWindowExecute()
        {
            _unitService.OpenUnitWindow(LegalEntityModel);
        }

        #endregion


    }
}