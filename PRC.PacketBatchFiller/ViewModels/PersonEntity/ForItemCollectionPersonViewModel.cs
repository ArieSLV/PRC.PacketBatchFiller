using Catel.Data;
using Catel.MVVM;
using PRC.PacketBatchFiller.Models.PersonsEntity;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.BaseClasses;

namespace PRC.PacketBatchFiller.ViewModels.PersonEntity
{
    public class ForItemCollectionPersonViewModel : UnitAsItemForViewModelCollection
    {
        private readonly IUnitService _unitService;

        public ForItemCollectionPersonViewModel(Person person, IUnitService unitService)
        {
            _unitService = unitService;
            PersonModel = person;
            UnitName = PersonModel.ToString();

            OpenUnitEditWindowCommand = new Command(OpenUnitEditWindowExecute);
        }

        #region PersonModel model property

        public Person PersonModel
        {
            get { return GetValue<Person>(PersonModelProperty); }
            private set { SetValue(PersonModelProperty, value); }
        }

        public static readonly PropertyData PersonModelProperty = RegisterProperty("PersonModel", typeof(Person));

        #endregion

        #region OpenUnitEditWindow command

        public Command OpenUnitEditWindowCommand { get; set; }

        private void OpenUnitEditWindowExecute()
        {
            _unitService.OpenUnitWindow(PersonModel);
        }

        #endregion
    }
}