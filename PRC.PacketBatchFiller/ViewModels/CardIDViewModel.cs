using System;
using System.Collections.ObjectModel;
using System.Linq;
using Catel.Data;
using Catel.MVVM;
using Catel.MVVM.Views;
using Catel.Services;
using PRC.PacketBatchFiller.Models.PersonsEntity;
using PRC.PacketBatchFiller.Services.Interfaces;
using PRC.PacketBatchFiller.ViewModels.PersonEntity.CardIDIssuer;

namespace PRC.PacketBatchFiller.ViewModels
{

    [InterestedIn(typeof(CardIDIssuerSearchViewModel))]
    public class CardIDViewModel : ViewModelBase
    {
        
        public CardIDViewModel(CardID cardID, IUIVisualizerService uiVisualizerService, IUnitService unitService)
        {

            #region Initialization parameters 
            
            CardID = cardID ?? new CardID();

            #endregion

            #region Loading reference books from context

            using (var dbContextManager = DbContextManager<PBFContext>.GetManager())
            {
                CardIDTypeCollection = new ObservableCollection<CardIDType>(dbContextManager.Context.CardIDTypes);
            }

            #endregion

            #region Setting inner ViewModels

            CardIDIssuerSearchViewModel = new CardIDIssuerSearchViewModel(CardIDIssuer, uiVisualizerService, unitService);

            #endregion
            
            #region Setting defaults

            //Тип документа: "Паспорт гражданина РФ"
            if (CardIDTypeCollection.Count > 0 && CardIDType == null)
            {
                CardIDType = CardIDTypeCollection.Single(x => x.Value == "Паспорт гражданина РФ");
            }
            else
            {
                CardIDType = CardID.CardIDType;

                switch (CardID.CardIDType.Value)
                {
                    case "Паспорт гражданина РФ":
                    case "Загранпаспорт гражданина РФ":
                    case null:
                        break;
                    default:
                        SeriesMask = new string('A', CardID.Series.Length);
                        NumberMask = new string('A', CardID.Number.Length);
                        break;
                }
            }

            #endregion

        }

        #region CardID model & properties

        #region CardIDType property

        /// <summary>
        /// Gets or sets the CardIDType value.
        /// </summary>
        [ViewModelToModel("CardID")]
        public CardIDType CardIDType
        {
            get { return GetValue<CardIDType>(CardIDTypeProperty); }
            set { SetValue(CardIDTypeProperty, value); }
        }

        /// <summary>
        /// CardIDType property data.
        /// </summary>
        public static readonly PropertyData CardIDTypeProperty = RegisterProperty("CardIDType", typeof (CardIDType), null,
            (sender, e) => ((CardIDViewModel) sender).OnCardIDTypeChanged());

        /// <summary>
        /// Called when the CardIDType property has changed.
        /// </summary>
        private void OnCardIDTypeChanged()
        {
            if (CardIDType == null) return;

            switch (CardIDType.Value)
            {
                case "Паспорт гражданина РФ":
                    try { SeriesMask = "00 00"; } catch (Exception) { Series = ""; SeriesMask = ""; SeriesMask = "00 00"; }
                    try { NumberMask = "000000"; } catch (Exception) { Number = ""; NumberMask = ""; NumberMask = "000000"; }
                    break;
                case "Загранпаспорт гражданина РФ":
                    try { SeriesMask = "00"; } catch (Exception) { Series = ""; SeriesMask = ""; SeriesMask = "00"; }
                    try { NumberMask = "0000000"; } catch (Exception) { Number = ""; NumberMask = ""; NumberMask = "0000000"; }
                    break;
                default:
                    try { SeriesMask = "A"; } catch (Exception) { Series = ""; SeriesMask = ""; SeriesMask = "A"; }
                    try { NumberMask = "A"; } catch (Exception) { Number = ""; NumberMask = ""; NumberMask = "A"; }
                    break;
            }


        }

        #endregion

        #region Series property

        [ViewModelToModel("CardID")]
        public string Series
        {
            get { return GetValue<string>(SeriesProperty); }
            set { SetValue(SeriesProperty, value); }
        }

        public static readonly PropertyData SeriesProperty = RegisterProperty("Series", typeof (string));

        #endregion

        #region Number property

        [ViewModelToModel("CardID")]
        public string Number
        {
            get { return GetValue<string>(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly PropertyData NumberProperty = RegisterProperty("Number", typeof (string));

        #endregion

        #region IssueDate property

        [ViewModelToModel("CardID")]
        public DateTime? IssueDate
        {
            get { return GetValue<DateTime?>(IssueDateProperty); }
            set { SetValue(IssueDateProperty, value); }
        }

        public static readonly PropertyData IssueDateProperty = RegisterProperty("IssueDate", typeof (DateTime?));

        #endregion

        #region CardIDIssuer model property

        /// <summary>
        /// Gets or sets the CardIDIssuer value.
        /// </summary>
        [Model, ViewModelToModel("CardID")]
        public CardIDIssuer CardIDIssuer
        {
            get { return GetValue<CardIDIssuer>(CardIDIssuerProperty); }
            private set { SetValue(CardIDIssuerProperty, value); }
        }

        /// <summary>
        /// CardIDIssuer property data.
        /// </summary>
        public static readonly PropertyData CardIDIssuerProperty = RegisterProperty("CardIDIssuer", typeof (CardIDIssuer));

        #endregion
       
        
        #region CardID model property
        
        [Model, ViewToViewModel("CardID")]
        public CardID CardID
        {
            get { return GetValue<CardID>(CardIDProperty); }
            private set { SetValue(CardIDProperty, value); }
        }

        public static readonly PropertyData CardIDProperty = RegisterProperty("CardID", typeof(CardID));

        #endregion

        #endregion
        
        #region UI properties
     
        #region CardIDTypeCollection property

        /// <summary>
        /// Gets or sets the CardIDTypeCollection value.
        /// </summary>
        public ObservableCollection<CardIDType> CardIDTypeCollection
        {
            get { return GetValue<ObservableCollection<CardIDType>>(CardIDTypeCollectionProperty); }
            set { SetValue(CardIDTypeCollectionProperty, value); }
        }

        /// <summary>
        /// CardIDTypeCollection property data.
        /// </summary>
        public static readonly PropertyData CardIDTypeCollectionProperty = RegisterProperty("CardIDTypeCollection", typeof (ObservableCollection<CardIDType>));

        #endregion

        #region SeriesMask property

        /// <summary>
        /// Gets or sets the SeriesMask value.
        /// </summary>
        public string SeriesMask
        {
            get { return GetValue<string>(SeriesMaskProperty); }
            set { SetValue(SeriesMaskProperty, value); }
        }

        /// <summary>
        /// SeriesMask property data.
        /// </summary>
        public static readonly PropertyData SeriesMaskProperty = RegisterProperty("SeriesMask", typeof (string), "00 00");

        #endregion

        #region NumberMask property

        /// <summary>
        /// Gets or sets the NumberMask value.
        /// </summary>
        public string NumberMask
        {
            get { return GetValue<string>(NumberMaskProperty); }
            set { SetValue(NumberMaskProperty, value); }
        }

        /// <summary>
        /// NumberMask property data.
        /// </summary>
        public static readonly PropertyData NumberMaskProperty = RegisterProperty("NumberMask", typeof (string), "000000");

        #endregion

        #region GetFocusOnSeriesTextBoxServiceProperty property

        public bool GetFocusOnSeriesTextBoxServiceProperty2055557116
        {
            get { return GetValue<bool>(GetFocusOnSeriesTextBoxServicePropertyProperty); }
            set { SetValue(GetFocusOnSeriesTextBoxServicePropertyProperty, value); }
        }

        public static readonly PropertyData GetFocusOnSeriesTextBoxServicePropertyProperty = RegisterProperty("GetFocusOnSeriesTextBoxServiceProperty", typeof(bool));

        #endregion

        #region CardIDIssuerSearchViewModel property

        public IViewModel CardIDIssuerSearchViewModel
        {
            get { return GetValue<IViewModel>(CardIDIssuerSearchViewModelProperty); }
            set { SetValue(CardIDIssuerSearchViewModelProperty, value); }
        }

        public static readonly PropertyData CardIDIssuerSearchViewModelProperty = RegisterProperty("SuggestFromLocalDataViewModel", typeof (IViewModel));

        #endregion

        #endregion
       
        #region Methods
        protected override void OnViewModelPropertyChanged(IViewModel viewModel, string propertyName)
        {
            if (propertyName == "TargetEntity" && viewModel is CardIDIssuerSearchViewModel)
            {
                var vm = (CardIDIssuerSearchViewModel) viewModel;
                CardIDIssuer = vm.TargetEntity;
            }
        }
        #endregion
   
    }
}