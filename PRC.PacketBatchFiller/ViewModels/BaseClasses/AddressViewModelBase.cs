using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Catel.Data;
using Catel.MVVM;
using Catel.Services;
using PRC.PacketBatchFiller.DataAccess.Clients;
using PRC.PacketBatchFiller.DataAccess.Models;

namespace PRC.PacketBatchFiller.ViewModels.BaseClasses
{
    public class AddressViewModelBase : ViewModelBase
    {
        private readonly IPleaseWaitService _pleaseWaitService;
        private readonly ICommandManager _commandManager;
        private readonly string _nextControlNameToFocus;
        private readonly SuggestClient _api = new SuggestClient("cc45a1775877d4e29e1652c226fcb36c76b573e6", "dadata.ru", "https");
        private readonly string _expanderHeaderText;
        private bool _canExpand;
        
        public AddressViewModelBase(string expanderHeaderText, IPleaseWaitService pleaseWaitService, ICommandManager commandManager, string nextControlNameToFocus)
        {
            _pleaseWaitService = pleaseWaitService;
            _commandManager = commandManager;
            _nextControlNameToFocus = nextControlNameToFocus;

            _expanderHeaderText = ExpanderHeaderText = expanderHeaderText;

            SelectedItemToItemCommand = new Command(OnSelectedItemToItemExecute);
        }

        #region Address propertyes

        #region Index property

        [ViewModelToModel("Address")]
        public string Index
        {
            get { return GetValue<string>(IndexProperty); }
            set { SetValue(IndexProperty, value); }
        }

        public static readonly PropertyData IndexProperty = RegisterProperty("Index", typeof (string), null, (sender, e) => ((AddressViewModelBase) sender).MakeAddressInOneString());


        #endregion

        #region RegionType property

        [ViewModelToModel("Address")]
        public string RegionType
        {
            get { return GetValue<string>(RegionTypeProperty); }
            set { SetValue(RegionTypeProperty, value); }
        }

        public static readonly PropertyData RegionTypeProperty = RegisterProperty("RegionType", typeof(string), null, (sender, e) => ((AddressViewModelBase)sender).MakeAddressInOneString());

        #endregion

        #region RegionName property

        [ViewModelToModel("Address")]
        public string RegionName
        {
            get { return GetValue<string>(RegionNameProperty); }
            set { SetValue(RegionNameProperty, value); }
        }

        public static readonly PropertyData RegionNameProperty = RegisterProperty("RegionName", typeof (string), null,
            (sender, e) => ((AddressViewModelBase) sender).MakeAddressInOneString());

        #endregion

        #region DistrictType property

        [ViewModelToModel("Address")]
        public string DistrictType
        {
            get { return GetValue<string>(DistrictTypeProperty); }
            set { SetValue(DistrictTypeProperty, value); }
        }
        
        public static readonly PropertyData DistrictTypeProperty = RegisterProperty("DistrictType", typeof (string), null, (sender, e) => ((AddressViewModelBase) sender).MakeAddressInOneString());

        #endregion

        #region DistrictName property

        [ViewModelToModel("Address")]
        public string DistrictName
        {
            get { return GetValue<string>(DistrictNameProperty); }
            set { SetValue(DistrictNameProperty, value); }
        }

        public static readonly PropertyData DistrictNameProperty = RegisterProperty("DistrictName", typeof (string), null, (sender, e) => ((AddressViewModelBase) sender).MakeAddressInOneString());

        #endregion

        #region CityType property

        [ViewModelToModel("Address")]
        public string CityType
        {
            get { return GetValue<string>(CityTypeProperty); }
            set { SetValue(CityTypeProperty, value); }
        }

        public static readonly PropertyData CityTypeProperty = RegisterProperty("CityType", typeof (string), null,
            (sender, e) => ((AddressViewModelBase) sender).MakeAddressInOneString());

        #endregion

        #region CityName property

        [ViewModelToModel("Address")]
        public string CityName
        {
            get { return GetValue<string>(CityNameProperty); }
            set { SetValue(CityNameProperty, value); }
        }

       
        public static readonly PropertyData CityNameProperty = RegisterProperty("CityName", typeof (string), null, (sender, e) => ((AddressViewModelBase) sender).MakeAddressInOneString());

        #endregion

        #region LocalityType property

        [ViewModelToModel("Address")]
        public string LocalityType
        {
            get { return GetValue<string>(LocalityTypeProperty); }
            set { SetValue(LocalityTypeProperty, value); }
        }
        
        public static readonly PropertyData LocalityTypeProperty = RegisterProperty("LocalityType", typeof (string), null, (sender, e) => ((AddressViewModelBase) sender).MakeAddressInOneString());

        #endregion

        #region LocalityName property

        [ViewModelToModel("Address")]
        public string LocalityName
        {
            get { return GetValue<string>(LocalityNameProperty); }
            set { SetValue(LocalityNameProperty, value); }
        }

        public static readonly PropertyData LocalityNameProperty = RegisterProperty("LocalityName", typeof (string), null, (sender, e) => ((AddressViewModelBase) sender).MakeAddressInOneString());

        #endregion

        #region StreetType property

        [ViewModelToModel("Address")]
        public string StreetType
        {
            get { return GetValue<string>(StreetTypeProperty); }
            set { SetValue(StreetTypeProperty, value); }
        }
        
        public static readonly PropertyData StreetTypeProperty = RegisterProperty("StreetType", typeof (string), null, (sender, e) => ((AddressViewModelBase) sender).MakeAddressInOneString());


        #endregion

        #region StreetName property

        [ViewModelToModel("Address")]
        public string StreetName
        {
            get { return GetValue<string>(StreetNameProperty); }
            set { SetValue(StreetNameProperty, value); }
        }

        
        public static readonly PropertyData StreetNameProperty = RegisterProperty("StreetName", typeof (string), null, (sender, e) => ((AddressViewModelBase) sender).MakeAddressInOneString());

        #endregion

        #region BuildingType property

        [ViewModelToModel("Address")]
        public string BuildingType
        {
            get { return GetValue<string>(BuildingTypeProperty); }
            set { SetValue(BuildingTypeProperty, value); }
        }

        public static readonly PropertyData BuildingTypeProperty = RegisterProperty("BuildingType", typeof (string), null, (sender, e) => ((AddressViewModelBase) sender).MakeAddressInOneString());

        #endregion

        #region BuildingValue property

        [ViewModelToModel("Address")]
        public string BuildingValue
        {
            get { return GetValue<string>(BuildingValueProperty); }
            set { SetValue(BuildingValueProperty, value); }
        }

        public static readonly PropertyData BuildingValueProperty = RegisterProperty("BuildingValue", typeof (string), null, (sender, e) => ((AddressViewModelBase) sender).MakeAddressInOneString());

        #endregion

        #region SubBuildingType property

        [ViewModelToModel("Address")]
        public string SubBuildingType
        {
            get { return GetValue<string>(SubBuildingTypeProperty); }
            set { SetValue(SubBuildingTypeProperty, value); }
        }

        
        public static readonly PropertyData SubBuildingTypeProperty = RegisterProperty("SubBuildingType", typeof (string), null, (sender, e) => ((AddressViewModelBase) sender).MakeAddressInOneString());

        #endregion

        #region SubBuildingValue property

        [ViewModelToModel("Address")]
        public string SubBuildingValue
        {
            get { return GetValue<string>(SubBuildingValueProperty); }
            set { SetValue(SubBuildingValueProperty, value); }
        }
        
        public static readonly PropertyData SubBuildingValueProperty = RegisterProperty("SubBuildingValue", typeof (string), null, (sender, e) => ((AddressViewModelBase) sender).MakeAddressInOneString());

        #endregion

        #region FlatType property

        [ViewModelToModel("Address")]
        public string FlatType
        {
            get { return GetValue<string>(FlatTypeProperty); }
            set { SetValue(FlatTypeProperty, value); }
        }

        public static readonly PropertyData FlatTypeProperty = RegisterProperty("FlatType", typeof (string), null, (sender, e) => ((AddressViewModelBase) sender).MakeAddressInOneString());

        #endregion

        #region FlatValue property

        [ViewModelToModel("Address")]
        public string FlatValue
        {
            get { return GetValue<string>(FlatValueProperty); }
            set { SetValue(FlatValueProperty, value); }
        }

        public static readonly PropertyData FlatValueProperty = RegisterProperty("FlatValue", typeof (string), null, (sender, e) => ((AddressViewModelBase) sender).MakeAddressInOneString());

        #endregion

        #region AddressInOneString property

        [ViewModelToModel("Address")]
        public string AddressInOneString
        {
            get { return GetValue<string>(AddressInOneStringProperty); }
            set { SetValue(AddressInOneStringProperty, value); }
        }

        
        public static readonly PropertyData AddressInOneStringProperty = RegisterProperty("AddressInOneString", typeof (string));

        #endregion

        #endregion

        #region UI properties

        #region SearchText property

        public string SearchText
        {
            get { return GetValue<string>(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        public static readonly PropertyData SearchTextProperty = RegisterProperty("SearchText", typeof(string), null, (sender, e) => ((AddressViewModelBase)sender).SuggestAddress());

        #endregion

        #region SuggestCollection property

        public List<SuggestAddressResponse.Suggestions> SuggestCollection
        {
            get { return GetValue<List<SuggestAddressResponse.Suggestions>>(SuggestCollectionProperty); }
            set { SetValue(SuggestCollectionProperty, value); }
        }

        public static readonly PropertyData SuggestCollectionProperty = RegisterProperty("SuggestCollection", typeof(List<SuggestAddressResponse.Suggestions>));
        
        #endregion

        #region SuggestListBoxHeight property

        public string SuggestListBoxHeight
        {
            get { return GetValue<string>(SuggestListBoxHeightProperty); }
            set { SetValue(SuggestListBoxHeightProperty, value); }
        }

        public static readonly PropertyData SuggestListBoxHeightProperty = RegisterProperty("SuggestListBoxHeight", typeof(string), "0");

        #endregion

        #region SelectedItem property
        
        public string SelectedItem
        {
            get { return GetValue<string>(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly PropertyData SelectedItemProperty = RegisterProperty("SelectedItem", typeof(string));

        #endregion

        #region IsExpanded property

        public bool IsExpanded
        {
            get { return GetValue<bool>(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }
        
        public static readonly PropertyData IsExpandedProperty = RegisterProperty("IsExpanded", typeof (bool), true);

        #endregion

        #region ExpanderHeaderText property

        /// <summary>
        /// Gets or sets the ExpanderHeaderText value.
        /// </summary>
        public string ExpanderHeaderText
        {
            get { return GetValue<string>(ExpanderHeaderTextProperty); }
            set { SetValue(ExpanderHeaderTextProperty, value); }
        }

        /// <summary>
        /// ExpanderHeaderText property data.
        /// </summary>
        public static readonly PropertyData ExpanderHeaderTextProperty = RegisterProperty("ExpanderHeaderText", typeof (string));

        #endregion

        #endregion


        #region Methods

        private void MakeAddressInOneString()
        {
            AddressInOneString = string.Empty;

            if (!string.IsNullOrWhiteSpace(Index))
            {
                AddressInOneString += Index;
            }

            if (!string.IsNullOrWhiteSpace(RegionName))
            {
                AddressInOneString += $", {RegionName} {RegionType}";
            }

            if (!string.IsNullOrWhiteSpace(DistrictName))
            {
                AddressInOneString += $", {DistrictName} {DistrictType}";
            }

            if (!string.IsNullOrWhiteSpace(CityName))
            {
                AddressInOneString += $", {CityType} {CityName}";
            }

            if (!string.IsNullOrWhiteSpace(LocalityName))
            {
                AddressInOneString += $", {LocalityType} {LocalityName}";
            }

            if (!string.IsNullOrWhiteSpace(StreetName))
            {
                AddressInOneString += $", {StreetType} {StreetName}";
            }

            if (!string.IsNullOrWhiteSpace(BuildingValue))
            {
                AddressInOneString += $", {BuildingType} {BuildingValue}";
            }

            if (!string.IsNullOrWhiteSpace(SubBuildingValue))
            {
                AddressInOneString += $", {SubBuildingType} {SubBuildingValue}";
            }

            if (!string.IsNullOrWhiteSpace(FlatValue))
            {
                AddressInOneString += $", {FlatType} {FlatValue}";
            }


        }
        
        #region SuggestAddress method

        private void SuggestAddress()
        {
            _pleaseWaitService.Show("Поиск адреса");
            var query = SearchText;
            var response = _api.QueryAddress(query);
            SuggestCollection = response.suggestionss;
            if (SuggestCollection.Count > 0)
            {
                Index = SuggestCollection[0].data.postal_code;
                RegionType = SuggestCollection[0].data.region_type_full;
                RegionName = SuggestCollection[0].data.region;
                DistrictType = SuggestCollection[0].data.area_type_full;
                DistrictName = SuggestCollection[0].data.area;
                CityType = SuggestCollection[0].data.city_type_full;
                CityName = SuggestCollection[0].data.city;
                LocalityType = SuggestCollection[0].data.settlement_type_full;
                LocalityName = SuggestCollection[0].data.settlement;
                StreetType = SuggestCollection[0].data.street_type_full;
                StreetName = SuggestCollection[0].data.street;
                BuildingType = !string.IsNullOrWhiteSpace(SuggestCollection[0].data.house_type_full)
                    ? SuggestCollection[0].data.house_type_full
                    : "дом";
                BuildingValue = SuggestCollection[0].data.house;
                SubBuildingType = !string.IsNullOrWhiteSpace(SuggestCollection[0].data.block_type)
                    ? SuggestCollection[0].data.block_type
                    : "корпус";
                SubBuildingValue = SuggestCollection[0].data.block;
                FlatType = !string.IsNullOrWhiteSpace(SuggestCollection[0].data.flat_type)
                    ? SuggestCollection[0].data.flat_type
                    : "квартира";
                FlatValue = SuggestCollection[0].data.flat;
            }
            _pleaseWaitService.Hide();
            _canExpand = false;
        }

        #endregion

        #region SelectedItemToItem command

        public Command SelectedItemToItemCommand { get; private set; }

        private void OnSelectedItemToItemExecute()
        {
            
            if (!_canExpand)
            {
                _canExpand = !_canExpand;
            }
            else
            {
                ExpanderHeaderText = $"{_expanderHeaderText}: {AddressInOneString}";
                IsExpanded = false;
                if(!string.IsNullOrEmpty(_nextControlNameToFocus)) _commandManager.ExecuteCommand($"GetFocusOn{_nextControlNameToFocus}Command");

            }
        }

        #endregion

        

        #endregion

    }



}