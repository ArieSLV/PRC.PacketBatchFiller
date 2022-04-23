using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Catel.Data;
using Catel.IoC;
using Catel.MVVM;
using Catel.Services;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;
using PRC.PacketBatchFiller.Models.LegalEntityEntity;
using PRC.PacketBatchFiller.Services.Interfaces;


namespace PRC.PacketBatchFiller.ViewModels.BaseClasses
{
    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    public class SuggestFromLocalDataViewModelBase<TEntity, TEditWindowViewModelEntity, TEditWindowEntity> : ViewModelBase where TEntity : class, new()
    {
        private readonly IUIVisualizerService _uiVisualizerService;
        private readonly IUnitService _unitService;
        private readonly char[] _delimiterChars = { ' ', ',', '.', ':', ';' };

        public int FiltredItemsCounter { get; set; }

        public SuggestFromLocalDataViewModelBase(TEntity tEntity, IUIVisualizerService uiVisualizerService, IUnitService unitService)
        {
            _uiVisualizerService = uiVisualizerService;
            _unitService = unitService;

            TargetEntity = tEntity;

            #region Loading reference books from context

            LoadReferenceBookFromContext();

            #endregion


            #region Setting commands

            AddItemToCollectionCommand = new Command(OnAddItemToCollectionCommandExecute);
            RemoveItemFromCollectionCommand = new TaskCommand(OnRemoveItemFromCollectionCommandExecute, CanEditAndRemoveItemInCollectionAndSelectedItemToItemCommand);
            EditItemInCollectionCommand = new TaskCommand(OnEditItemInCollectionCommandExecute, CanEditAndRemoveItemInCollectionAndSelectedItemToItemCommand);

            SelectedItemToItemCommand = new Command(OnSelectedItemToItemExecute, CanEditAndRemoveItemInCollectionAndSelectedItemToItemCommand);
            RemoveSelectedItemCommand = new Command(OnRemoveSelectedItemExecute);

            #endregion

            SetToDefault();
        }

        #region Properties

        #region TargetEntity property

        public TEntity TargetEntity
        {
            get { return GetValue<TEntity>(TargetEntityProperty); }
            set { SetValue(TargetEntityProperty, value); }
        }
        
        public static readonly PropertyData TargetEntityProperty = RegisterProperty("TargetEntity", typeof (TEntity));
        
        #endregion

        #region ItemsCollection property

        public ObservableCollection<TEntity> ItemsCollection
        {
            get { return GetValue<ObservableCollection<TEntity>>(ItemsCollectionProperty); }
            set { SetValue(ItemsCollectionProperty, value); }
        }
        
        public static readonly PropertyData ItemsCollectionProperty = RegisterProperty("ItemsCollection", typeof(ObservableCollection<TEntity>));

        #endregion

        #region SelectedItem property

        public TEntity SelectedItem
        {
            get { return GetValue<TEntity>(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly PropertyData SelectedItemProperty = RegisterProperty("SelectedItem", typeof(TEntity));

        #endregion

        #region SearchText property

        public string SearchText
        {
            get { return GetValue<string>(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }
        
        public static readonly PropertyData SearchTextProperty = RegisterProperty("SearchText", typeof(string), null, 
            (sender, e) => ((SuggestFromLocalDataViewModelBase<TEntity, TEditWindowViewModelEntity, TEditWindowEntity>)sender).OnSearchTextChanged());
        
        private void OnSearchTextChanged()
        {
            var tmpSelectedItem = SelectedItem;
            FiltredItemsCounter = 0;
            
            CollectionViewSource.GetDefaultView(ItemsCollection).Filter = item =>
            {
                var tEntity = item as TEntity;

                if (tEntity?.GetEntityMainPropertyValue() != null)
                {
                    foreach (var filterWord in SearchText.ToLower().Split(_delimiterChars))
                    {
                        if (!tEntity.ToString().ToLower().Contains(filterWord.ToLower()))
                        {
                            return false;
                        }
                    }
                }
                FiltredItemsCounter += 1;

                if (FiltredItemsCounter == 1) SelectedItem = tEntity;

                return true;
            };

            RemoveSelectedButtonVisibility = SetVisibilityVisibleBasedOnSuggestListBox(FiltredItemsCounter);

            if (FiltredItemsCounter == 1)
            {
                TargetEntity = SelectedItem;
            }

            else if (FiltredItemsCounter > 1)
            {
                SelectedItem = ItemsCollection.FirstOrDefault(item => item == tmpSelectedItem);
            }
            else
            {
                SelectedItem = null;
            }


            if (SearchText.Length > 0)
            {
                SuggestListBoxAndControlButtonsVisibility = Visibility.Visible;

            }
            else
            {
                SuggestListBoxAndControlButtonsVisibility = Visibility.Collapsed;
                SelectedItem = null;
            }
        }

        #endregion

        #region FiltredItemsCounter property

       
        
        #endregion

        #region SelectedItemTextBlockVisibility property

        public Visibility SelectedItemTextBlockVisibility
        {
            get { return GetValue<Visibility>(SelectedItemTextBlockVisibilityProperty); }
            set { SetValue(SelectedItemTextBlockVisibilityProperty, value); }
        }

        public static readonly PropertyData SelectedItemTextBlockVisibilityProperty = RegisterProperty("SelectedItemTextBlockVisibility", typeof (Visibility));

        #endregion

        #region SuggestListBoxAndControlButtonsVisibility property

        public Visibility SuggestListBoxAndControlButtonsVisibility
        {
            get { return GetValue<Visibility>(SuggestListBoxAndControlButtonsVisibilityProperty); }
            set { SetValue(SuggestListBoxAndControlButtonsVisibilityProperty, value); }
        }

        public static readonly PropertyData SuggestListBoxAndControlButtonsVisibilityProperty = RegisterProperty("SuggestListBoxAndControlButtonsVisibility", typeof(Visibility));

        #endregion

        #region RemoveSelectedButtonVisibility property

        public Visibility RemoveSelectedButtonVisibility
        {
            get { return GetValue<Visibility>(RemoveSelectedButtonVisibilityProperty); }
            set { SetValue(RemoveSelectedButtonVisibilityProperty, value); }
        }

        public static readonly PropertyData RemoveSelectedButtonVisibilityProperty = RegisterProperty("RemoveSelectedButtonVisibility", typeof(Visibility));

        #endregion

        #region SearchTextBoxVisibility property

        public Visibility SearchTextBoxVisibility
        {
            get { return GetValue<Visibility>(SearchTextBoxVisibilityProperty); }
            set { SetValue(SearchTextBoxVisibilityProperty, value); }
        }

        public static readonly PropertyData SearchTextBoxVisibilityProperty = RegisterProperty("SearchTextBoxVisibility", typeof(Visibility));

        #endregion

        #endregion




        #region Commands

        #region AddItemToCollection command

        public Command AddItemToCollectionCommand { get; private set; }

        private async void OnAddItemToCollectionCommandExecute()
        {
            var tEntity = await AddItemToCollection();
            
            LoadReferenceBookFromContext();
            SearchText = string.Empty;
            SelectedItem = ItemsCollection.FirstOrDefault(item => item == tEntity);
            if (SelectedItem != null) SearchText = SelectedItem?.ToString();
            SelectedItemToItemCommand.Execute();
        }



        #endregion

        #region RemoveItemFromCollection command

        public TaskCommand RemoveItemFromCollectionCommand { get; private set; }

        private async Task OnRemoveItemFromCollectionCommandExecute()
        {
            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
            {
                var tEntity = await dbContextManager.Context.GetDbSet<TEntity>().FindAsync(SelectedItem.GetIDPropertyValue());

                dbContextManager.Context.GetDbSet<TEntity>().Remove(tEntity);
                dbContextManager.Context.SaveChanges();
                LoadReferenceBookFromContext();
            }
        }

        private bool CanEditAndRemoveItemInCollectionAndSelectedItemToItemCommand()
        {
            return SelectedItem != null && SelectedItem.ToString() != SelectedItem.GetEntityDefaultValue().ToString();
        }

        #endregion

        #region EditItemInCollection command

        public TaskCommand EditItemInCollectionCommand { get; private set; }

        private async Task OnEditItemInCollectionCommandExecute()
        {
            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
            {
                object tEntity;

                if (typeof(TEntity) == typeof(LegalEntity))
                {
                    tEntity = await dbContextManager.Context.GetDbSet<LegalEntity>().FindAsync(SelectedItem.GetIDPropertyValue());

                    await _unitService.OpenUnitWindow((Unit) tEntity);
                }
                else
                {
                    tEntity = await dbContextManager.Context.GetDbSet<TEntity>().FindAsync(SelectedItem.GetIDPropertyValue());

                    _uiVisualizerService.Unregister(typeof(TEditWindowEntity));
                    _uiVisualizerService.Register(typeof(TEditWindowViewModelEntity), typeof(TEditWindowEntity));

                    var typeFactory = TypeFactory.Default;
                    var editWindowViewModel = (IViewModel)typeFactory.CreateInstanceWithParametersAndAutoCompletion<TEditWindowViewModelEntity>(tEntity);

                    if (await _uiVisualizerService.ShowDialogAsync(editWindowViewModel) != true) return;

                    dbContextManager.Context.SaveChanges();
                }

                LoadReferenceBookFromContext();
                SearchText = string.Empty;
                SelectedItem = ItemsCollection.FirstOrDefault(item => item == tEntity);
                if (SelectedItem != null) SearchText = SelectedItem?.ToString();
                SelectedItemToItemCommand.Execute();
            }
        }

        #endregion


        #region SelectedItemToItem command

        public Command SelectedItemToItemCommand { get; private set; }

        private void OnSelectedItemToItemExecute()
        {
            if (TargetEntity != SelectedItem)
            {
                TargetEntity = SelectedItem;
                SearchTextBoxVisibility = Visibility.Visible;
                RemoveSelectedButtonVisibility=Visibility.Visible;
                SelectedItemTextBlockVisibility = Visibility.Collapsed;
            }
            else
            {
                RemoveSelectedButtonVisibility = Visibility.Visible;
                SearchTextBoxVisibility = Visibility.Collapsed;
                SuggestListBoxAndControlButtonsVisibility = Visibility.Collapsed;
                SelectedItemTextBlockVisibility = Visibility.Visible;
                
                //ExecuteFocusCommand();
            }
        }
        
        #endregion

        #region RemoveSelectedItem command

        public Command RemoveSelectedItemCommand { get; set; }

        private void OnRemoveSelectedItemExecute()
        {
            TargetEntity = ItemsCollection.GetDefaultCollectionItem();
            SearchText = "";
            SelectedItemTextBlockVisibility = Visibility.Collapsed;
            SuggestListBoxAndControlButtonsVisibility = Visibility.Collapsed;
            SearchTextBoxVisibility = Visibility.Visible;
        }

        #endregion

        #endregion


        #region Methods

        public Visibility SetVisibilityCollapsedBasedOnSuggestListBox(int itemsCount)
        {
            return itemsCount == 1 ? Visibility.Collapsed : Visibility.Visible;
        }

        public Visibility SetVisibilityVisibleBasedOnSuggestListBox(int itemsCount)
        {
            return itemsCount == 1 ? Visibility.Visible : Visibility.Collapsed;
        }

        public void SetToDefault()
        {
            SetTargetEntityToDefault();
            
            if (TargetEntity.GetEntityMainPropertyValue() == null)
            {
                TargetEntity = ItemsCollection.GetDefaultCollectionItem();
                SelectedItemTextBlockVisibility = Visibility.Collapsed;
            }
            
            if (ItemsCollection.Count > 0 && EqualityComparer<TEntity>.Default.Equals(TargetEntity, ItemsCollection.GetDefaultCollectionItem()))
            {
                TargetEntity = ItemsCollection.GetDefaultCollectionItem();
                SelectedItemTextBlockVisibility = Visibility.Collapsed;
                SearchTextBoxVisibility = Visibility.Visible;
                SuggestListBoxAndControlButtonsVisibility = Visibility.Collapsed;
            }
            else
            {
                if (TargetEntity != null) SearchText = TargetEntity.ToString();
                SearchTextBoxVisibility = Visibility.Collapsed;
                RemoveSelectedButtonVisibility = Visibility.Visible;
                SelectedItemTextBlockVisibility = Visibility.Visible;
                SuggestListBoxAndControlButtonsVisibility = Visibility.Collapsed;
            }

            SelectedItem = default(TEntity);
            
        }

        protected virtual void SetTargetEntityToDefault() {  }

        protected virtual void LoadReferenceBookFromContext()
        {
            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
            {
                ItemsCollection = new ObservableCollection<TEntity>(dbContextManager.Context.GetEntityObservableCollection<TEntity>().OrderBy(item => item.GetMainPropertyName()));
            }
        }

        protected virtual async Task<TEntity> AddItemToCollection()
        {
            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
            {
                var tEntity = new TEntity();
                dbContextManager.Context.GetDbSet<TEntity>().Add(tEntity);
                tEntity.SetMainPropertyValue(SearchText);

                _uiVisualizerService.Unregister(typeof (TEditWindowViewModelEntity));
                _uiVisualizerService.Register(typeof (TEditWindowViewModelEntity), typeof (TEditWindowEntity));

                var typeFactory = TypeFactory.Default;
                var editWindowViewModel = (IViewModel) typeFactory.CreateInstanceWithParametersAndAutoCompletion<TEditWindowViewModelEntity>(tEntity);

                if (await _uiVisualizerService.ShowDialogAsync(editWindowViewModel) == false) return null;

                dbContextManager.Context.SaveChanges();
                return tEntity;
            }
        }



        #endregion
    }
}