using Catel.Data;
using Catel.MVVM;

namespace PRC.PacketBatchFiller.ViewModels.BaseClasses
{
    public class UnitAsItemForViewModelCollection : ViewModelBase
    {
        public UnitAsItemForViewModelCollection()
        {
            RemoveUnitCommand = new Command(RemoveUnit);
        }

        #region UnitName property

        public string UnitName
        {
            get { return GetValue<string>(UnitNameProperty); }
            set { SetValue(UnitNameProperty, value); }
        }

        public static readonly PropertyData UnitNameProperty = RegisterProperty("UnitName", typeof(string));

        #endregion



        #region EntryNeedDelete property

        public bool EntryNeedDelete
        {
            get { return GetValue<bool>(EntryNeedDeleteProperty); }
            set { SetValue(EntryNeedDeleteProperty, value); }
        }

        public static readonly PropertyData EntryNeedDeleteProperty = RegisterProperty("EntryNeedDelete", typeof(bool));

        #endregion

        #region RemoveUnit command

        public Command RemoveUnitCommand { get; set; }

        private void RemoveUnit()
        {
            EntryNeedDelete = !EntryNeedDelete;
        }

        #endregion




    }
}