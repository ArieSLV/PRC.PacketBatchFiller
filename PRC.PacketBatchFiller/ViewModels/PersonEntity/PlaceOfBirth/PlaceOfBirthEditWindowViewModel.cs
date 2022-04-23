using System.Threading.Tasks;
using Catel.Data;
using Catel.MVVM;
using Catel.MVVM.Views;

namespace PRC.PacketBatchFiller.ViewModels.PersonEntity.PlaceOfBirth
{
    public class PlaceOfBirthEditWindowModel : ViewModelBase
    {
        public PlaceOfBirthEditWindowModel(Models.PersonsEntity.PlaceOfBirth placeOfBirth)
        {
            PlaceOfBirthModel = placeOfBirth ?? new Models.PersonsEntity.PlaceOfBirth();
        }

        #region Value property

        [ViewModelToModel("PlaceOfBirthModel")]
        public string Value
        {
            get { return GetValue<string>(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly PropertyData ValueProperty = RegisterProperty("Value", typeof (string));

        #endregion

        #region PlaceOfBirth model property

        /// <summary>
        /// Gets or sets the PlaceOfBirth value.
        /// </summary>
        [Model, ViewToViewModel("PlaceOfBirth")]
        public Models.PersonsEntity.PlaceOfBirth PlaceOfBirthModel
        {
            get { return GetValue<Models.PersonsEntity.PlaceOfBirth>(PlaceOfBirthModelProperty); }
            private set { SetValue(PlaceOfBirthModelProperty, value); }
        }

        /// <summary>
        /// PlaceOfBirth property data.
        /// </summary>
        public static readonly PropertyData PlaceOfBirthModelProperty = RegisterProperty("PlaceOfBirthModel", typeof (Models.PersonsEntity.PlaceOfBirth));

        #endregion

        public override string Title => "Редактирование наименования места рождения";

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();
        }

        protected override async Task CloseAsync()
        {
            await base.CloseAsync();
        }
    }
}