using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Catel.Data;
using Catel.MVVM;
using Catel.Services;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.BaseClasses;

//namespace PRC.PacketBatchFiller.ViewModels.Suggest.Person
//{
//    public class PersonSearchViewModel : SuggestFromLocalDataViewModelBase<Models.Person, PersonWindowModel, Views.PersonWindow>
//    {
//        private readonly IUnitService _unitService;
//        private readonly ICommandManager _commandManager;

//        public PersonSearchViewModel(
//            string labelText,
//            Models.Person tEntity, 
//            IUIVisualizerService uiVisualizerService, 
//            IUnitService unitService, ICommandManager commandManager) 
//            : base(
//                  tEntity, 
//                  uiVisualizerService, 
//                  unitService)
//        {
//            _unitService = unitService;
//            _commandManager = commandManager;

//            LabelText = labelText;
//        }

//        #region LabelText property

//        public string LabelText
//        {
//            get { return GetValue<string>(LabelTextProperty); }
//            set { SetValue(LabelTextProperty, value); }
//        }

//        public static readonly PropertyData LabelTextProperty = RegisterProperty("LabelText", typeof(string));

//        #endregion

//        protected override void LoadReferenceBookFromContext()
//        {
//            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
//            {
//                ItemsCollection = new ObservableCollection<Models.Person>(dbContextManager.Context.Persons.OrderBy(o => o.FullName));
//            }
//        }


//        protected override async Task<Models.Person> AddItemToCollection()
//        {
//            if (this.GetRootIViewModel() is LegalEntityWindowModel) _commandManager.GetCommand("SaveAndCloseLegalEntityWindowCommand").Execute(null);

//            Models.Person person;
//            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
//            {
//                Models.Person newPerson;
//                char[] splitArray = {' ', '.', ','};
//                var nameWordsArray = SearchText.Split(splitArray, StringSplitOptions.RemoveEmptyEntries);

//                if (nameWordsArray.Length == 3)
//                {
//                    newPerson = new Models.Person
//                    {
//                        LastName = nameWordsArray[0],
//                        FirstName = nameWordsArray[1],
//                        MiddleName = nameWordsArray[2]
//                    };
//                }
//                else { newPerson = new Models.Person(); }

                
//                person = (Models.Person) await _unitService.OpenUnitWindow(newPerson);

//                LoadReferenceBookFromContext();

//                _unitService.SaveUnitForRestoreInSuggestList(person);

//                await _unitService.OpenUnitWindow(_unitService.GetLastUnitFromHistory());
//            }

//            return person;
//        }
//    }
//}