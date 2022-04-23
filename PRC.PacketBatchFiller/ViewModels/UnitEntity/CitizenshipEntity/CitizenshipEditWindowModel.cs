using System.Threading.Tasks;
using Catel.Data;
using Catel.MVVM;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;

namespace PRC.PacketBatchFiller.ViewModels.UnitEntity.CitizenshipEntity
{
    public class CitizenshipEditWindowModel : ViewModelBase
    {
        public CitizenshipEditWindowModel(Citizenship citizenship)
        {
            CitizenshipModel = citizenship ?? new Citizenship();
        }

        #region Value property

        [ViewModelToModel("CitizenshipModel")]
        public string Value
        {
            get { return GetValue<string>(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly PropertyData ValueProperty = RegisterProperty("Value", typeof(string));

        #endregion

        #region CitizenshipModel model property

        [Model]
        public Citizenship CitizenshipModel
        {
            get { return GetValue<Citizenship>(CitizenshipModelProperty); }
            private set { SetValue(CitizenshipModelProperty, value); }
        }

        public static readonly PropertyData CitizenshipModelProperty = RegisterProperty("CitizenshipModel", typeof (Citizenship));

        #endregion
        

        public override string Title => "Редактирование имени страны регистрации";
        
        protected override async Task InitializeAsync() { await base.InitializeAsync(); }
        protected override async Task CloseAsync() { await base.CloseAsync(); }
    }
}