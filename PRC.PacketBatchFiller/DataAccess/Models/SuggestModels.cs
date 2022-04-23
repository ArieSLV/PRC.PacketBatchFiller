using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using PRC.PacketBatchFiller.Models;
using RestSharp;

namespace PRC.PacketBatchFiller.DataAccess.Models
{

    public sealed class ContentType
    {
        readonly string name;
        readonly DataFormat format;

        public string Name
        {
            get { return this.name; }
        }

        public DataFormat Format
        {
            get { return this.format; }
        }

        public static readonly ContentType JSON = new ContentType("application/json", DataFormat.Json);
        public static readonly ContentType XML = new ContentType("application/xml", DataFormat.Xml);

        private ContentType(string name, DataFormat format)
        {
            this.name = name;
            this.format = format;
        }

        public override string ToString()
        {
            return name;
        }
    }

    public class SuggestQuery
    {
        public string query { get; set; }
        public SuggestQuery(string query)
        {
            this.query = query;
        }
    }

    public class FioData
    {
        public string surname { get; set; }
        public string name { get; set; }
        public string patronymic { get; set; }
        public string gender { get; set; }
    }

    public class AddressData
    {
        public string value { get; set; }
        public string postal_code { get; set; }
        public string country { get; set; }
        public string region_type { get; set; }
        public string region_type_full { get; set; }
        public string region { get; set; }
        public string area_type { get; set; }
        public string area_type_full { get; set; }
        public string area { get; set; }
        public string city_type { get; set; }
        public string city_type_full { get; set; }
        public string city { get; set; }
        public string settlement_type { get; set; }
        public string settlement_type_full { get; set; }
        public string settlement { get; set; }
        public string street_type { get; set; }
        public string street_type_full { get; set; }
        public string street { get; set; }
        public string house_type { get; set; }
        public string house_type_full { get; set; }
        public string house { get; set; }
        public string block_type { get; set; }
        public string block { get; set; }
        public string flat_type { get; set; }
        public string flat { get; set; }
        public string postal_box { get; set; }
        public string kladr_id { get; set; }
        public string okato { get; set; }
        public string oktmo { get; set; }
        public string tax_office { get; set; }
    }

    public class BankData
    {
        public string value { get; set; }
        public string unrestricted_value { get; set; }
        public BankOpfData opf { get; set; }
        public BankNameData name { get; set; }
        public string bic { get; set; }
        public string swift { get; set; }
        public string okpo { get; set; }
        public string correspondent_account { get; set; }
        public string registration_number { get; set; }
        public BankData rkc { get; set; }
        public AddressData address { get; set; }
        public string phone { get; set; }
        public BankStateData state { get; set; }
    }

    
    public class BankOpfData
    {
        public string type { get; set; }
        public string full { get; set; }
        public string @short { get; set; }
    }

    public class BankNameData
    {
        public string full { get; set; }
        public string payment { get; set; }
        public string @short { get; set; }
    }

    public class BankStateData
    {
        public string status { get; set; }
        public string actuality_date { get; set; }
        public string registration_date { get; set; }
        public string liquidation_date { get; set; }
    }

    public class PartyData
    {
        public string value { get; set; }
        public AddressData address { get; set; }
        public string branch_count { get; set; }
        public PartyBranchType branch_type { get; set; }
        public string inn { get; set; }
        public string kpp { get; set; }
        public PartyManagementData management { get; set; }
        public PartyNameData name { get; set; }
        public string ogrn { get; set; }
        public string okpo { get; set; }
        public string okved { get; set; }
        public PartyOpfData opf { get; set; }
        public PartyStateData state { get; set; }
        public PartyType type { get; set; }
    }

    public enum PartyBranchType
    {
        MAIN,
        BRANCH
    }

    public class PartyManagementData
    {
        public string name { get; set; }
        public string post { get; set; }
    }

    public class PartyNameData
    {
        public string full { get; set; }
        public string latin { get; set; }
        public string @short { get; set; }
    }

    public class PartyOpfData
    {
        public string code { get; set; }
        public string full { get; set; }
        public string @short { get; set; }
    }

    public class PartyStateData
    {
        public string registration_date { get; set; }
        public string liquidation_date { get; set; }
        public PartyStatus status { get; set; }
    }

    public enum PartyStatus
    {
        ACTIVE,
        LIQUIDATING,
        LIQUIDATED
    }

    public enum PartyType
    {
        LEGAL,
        INDIVIDUAL
    }

    public abstract class Suggestion
    {
        public string value { get; set; }
        public override string ToString()
        {
            return value;
        }
    }

    public class SuggestBankResponse
    {
        public class Suggestions : Suggestion
        {
            public BankData data { get; set; }
            public override string ToString()
            {
                return data.name.payment;
            }
        }
        public List<Suggestions> suggestionss { get; set; }


    }



    public class SuggestFioResponse
    {
        public class Suggestions : Suggestion
        {
            public FioData data { get; set; }
        }
        public List<Suggestions> suggestionss { get; set; }
    }

    public class SuggestAddressResponse
    {
        public class Suggestions : Suggestion
        {
            public AddressData data { get; set; }
        }
        public List<Suggestions> suggestionss { get; set; }
    }

    public class SuggestPartyResponse
    {
        public class Suggestions : Suggestion
        {
            public PartyData data { get; set; }
        }
        public List<Suggestions> suggestionss { get; set; }
    }
}