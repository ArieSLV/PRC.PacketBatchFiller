using System.ComponentModel.DataAnnotations.Schema;
using Catel.Data;

namespace PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity
{
    [Table("Addresses")]
    public class Address : ModelBase
    {
        #region AddressId property

        public long AddressId
        {
            get { return GetValue<long>(AddressIdProperty); }
            set { SetValue(AddressIdProperty, value); }
        }

        public static readonly PropertyData AddressIdProperty = RegisterProperty("AddressId", typeof (long));

        #endregion

        #region Index property

        public string Index
        {
            get { return GetValue<string>(IndexProperty); }
            set { SetValue(IndexProperty, value); }
        }

        public static readonly PropertyData IndexProperty = RegisterProperty("Index", typeof (string));

        #endregion

        #region RegionType property

        public string RegionType
        {
            get { return GetValue<string>(RegionTypeProperty); }
            set { SetValue(RegionTypeProperty, value); }
        }

        public static readonly PropertyData RegionTypeProperty = RegisterProperty("RegionType", typeof (string), "область");

        #endregion

        #region RegionName property

        public string RegionName
        {
            get { return GetValue<string>(RegionNameProperty); }
            set { SetValue(RegionNameProperty, value); }
        }

        public static readonly PropertyData RegionNameProperty = RegisterProperty("RegionName", typeof (string));

        #endregion

        #region DistrictType property

        public string DistrictType
        {
            get { return GetValue<string>(DistrictTypeProperty); }
            set { SetValue(DistrictTypeProperty, value); }
        }

        public static readonly PropertyData DistrictTypeProperty = RegisterProperty("DistrictType", typeof (string), "район");

        #endregion

        #region DistrictName property

        public string DistrictName
        {
            get { return GetValue<string>(DistrictNameProperty); }
            set { SetValue(DistrictNameProperty, value); }
        }

        public static readonly PropertyData DistrictNameProperty = RegisterProperty("DistrictName", typeof (string));

        #endregion

        #region CityType property

        public string CityType
        {
            get { return GetValue<string>(CityTypeProperty); }
            set { SetValue(CityTypeProperty, value); }
        }

        public static readonly PropertyData CityTypeProperty = RegisterProperty("CityType", typeof (string), "город");

        #endregion

        #region CityName property

        public string CityName
        {
            get { return GetValue<string>(CityNameProperty); }
            set { SetValue(CityNameProperty, value); }
        }

        public static readonly PropertyData CityNameProperty = RegisterProperty("CityName", typeof (string));

        #endregion

        #region LocalityType property

        public string LocalityType
        {
            get { return GetValue<string>(LocalityTypeProperty); }
            set { SetValue(LocalityTypeProperty, value); }
        }

        public static readonly PropertyData LocalityTypeProperty = RegisterProperty("LocalityType", typeof (string), "иной населенный пункт");

        #endregion

        #region LocalityName property

        public string LocalityName
        {
            get { return GetValue<string>(LocalityNameProperty); }
            set { SetValue(LocalityNameProperty, value); }
        }

        public static readonly PropertyData LocalityNameProperty = RegisterProperty("LocalityName", typeof (string));

        #endregion

        #region StreetType property

        public string StreetType
        {
            get { return GetValue<string>(StreetTypeProperty); }
            set { SetValue(StreetTypeProperty, value); }
        }

        public static readonly PropertyData StreetTypeProperty = RegisterProperty("StreetType", typeof (string), "улица");

        #endregion

        #region StreetName property

        public string StreetName
        {
            get { return GetValue<string>(StreetNameProperty); }
            set { SetValue(StreetNameProperty, value); }
        }

        public static readonly PropertyData StreetNameProperty = RegisterProperty("StreetName", typeof (string));

        #endregion

        #region BuildingType property

        public string BuildingType
        {
            get { return GetValue<string>(BuildingTypeProperty); }
            set { SetValue(BuildingTypeProperty, value); }
        }

        public static readonly PropertyData BuildingTypeProperty = RegisterProperty("BuildingType", typeof (string), "дом");

        #endregion

        #region BuildingValue property

        public string BuildingValue
        {
            get { return GetValue<string>(BuildingValueProperty); }
            set { SetValue(BuildingValueProperty, value); }
        }

        public static readonly PropertyData BuildingValueProperty = RegisterProperty("BuildingValue", typeof (string));

        #endregion

        #region SubBuildingType property

        public string SubBuildingType
        {
            get { return GetValue<string>(SubBuildingProperty); }
            set { SetValue(SubBuildingProperty, value); }
        }

        public static readonly PropertyData SubBuildingProperty = RegisterProperty("SubBuildingType", typeof (string), "корпус");

        #endregion

        #region SubBuildingValue property

        public string SubBuildingValue
        {
            get { return GetValue<string>(SubBuildingValueProperty); }
            set { SetValue(SubBuildingValueProperty, value); }
        }

        public static readonly PropertyData SubBuildingValueProperty = RegisterProperty("SubBuildingValue", typeof (string));

        #endregion

        #region FlatType property

        public string FlatType
        {
            get { return GetValue<string>(FlatTypeProperty); }
            set { SetValue(FlatTypeProperty, value); }
        }

        public static readonly PropertyData FlatTypeProperty = RegisterProperty("FlatType", typeof (string), "квартира");

        #endregion

        #region FlatValue property

        public string FlatValue
        {
            get { return GetValue<string>(FlatValueProperty); }
            set { SetValue(FlatValueProperty, value); }
        }

        public static readonly PropertyData FlatValueProperty = RegisterProperty("FlatValue", typeof (string));

        #endregion

        #region AddressInOneString property

        public string AddressInOneString
        {
            get { return GetValue<string>(AddressInOneStringProperty); }
            set { SetValue(AddressInOneStringProperty, value); }
        }

        public static readonly PropertyData AddressInOneStringProperty = RegisterProperty("AddressInOneString", typeof (string));

        #endregion

    }
}