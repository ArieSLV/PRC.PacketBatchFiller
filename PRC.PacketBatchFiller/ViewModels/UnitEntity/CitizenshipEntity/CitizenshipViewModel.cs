using System.Linq;
using Catel.Data;
using Catel.Services;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.BaseClasses;
using PRC.PacketBatchFiller.Views.Unit.Citizenship;

namespace PRC.PacketBatchFiller.ViewModels.UnitEntity.CitizenshipEntity
{
    public class CitizenshipViewModel : SuggestFromLocalDataViewModelBase<Citizenship, CitizenshipEditWindowModel, CitizenshipEditWindow>
    {
        public CitizenshipViewModel(
            string labelText, 
            Citizenship citizenship, 
            IUIVisualizerService uiVisualizerService,
             IUnitService unitService)
            : base (
                  citizenship, 
                  uiVisualizerService, unitService
                  )
        {
            LabelText = labelText;
        }

        #region LabelText property

        public string LabelText
        {
            get { return GetValue<string>(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        public static readonly PropertyData LabelTextProperty = RegisterProperty("LabelText", typeof(string));

        #endregion

        protected override void SetTargetEntityToDefault()
        {
            if (TargetEntity.Value != null) return;

            TargetEntity = ItemsCollection.FirstOrDefault(citizenship => citizenship.Value == "Российская Федерация");
        }
    }
}