using System;
using System.Net;
using PRC.PacketBatchFiller.DataAccess.Models;
using RestSharp;

namespace PRC.PacketBatchFiller.DataAccess.Clients {

    public class SuggestClient
    {
        const string SUGGESTIONS_URL_SHORT = "{0}://{1}/api/v2/suggest";
        const string SUGGESTIONS_URL_FULL = "{0}://{1}:{2}/api/v2/suggest";
        const string FIO_RESOURCE = "fio";
        const string ADDRESS_RESOURCE = "address";
        const string BANK_RESOURCE = "bank";
        const string PARTY_RESOURCE = "party";

        RestClient client;
        string token;

        public IWebProxy Proxy
        {
            get { return client.Proxy; }
            set { client.Proxy = value; }
        }

        static SuggestClient()
        {
            // use SSL v3
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
        }

        public SuggestClient(string token, string hostname, string protocol = "http")
        {
            this.token = token;
            this.client = new RestClient(String.Format(SUGGESTIONS_URL_SHORT, protocol, hostname));
        }

        public SuggestClient(string token, string hostname, int port, string protocol = "http")
        {
            this.token = token;
            this.client = new RestClient(String.Format(SUGGESTIONS_URL_FULL, protocol, hostname, port));
        }

        public SuggestFioResponse QueryFio(string fio)
        {
            var request = new RestRequest(FIO_RESOURCE, Method.POST);
            var query = new SuggestQuery(fio);
            return Execute<SuggestFioResponse>(request, query, ContentType.XML);
        }

        public SuggestAddressResponse QueryAddress(string address)
        {
            var request = new RestRequest(ADDRESS_RESOURCE, Method.POST);
            var query = new SuggestQuery(address);
            return Execute<SuggestAddressResponse>(request, query, ContentType.XML);
        }

        public SuggestPartyResponse QueryParty(string party)
        {
            var request = new RestRequest(PARTY_RESOURCE, Method.POST);
            var query = new SuggestQuery(party);
            return Execute<SuggestPartyResponse>(request, query, ContentType.XML);
        }

        public SuggestBankResponse QueryBank(string bank)
        {
            var request = new RestRequest(BANK_RESOURCE, Method.POST);
            var query = new SuggestQuery(bank);
            return Execute<SuggestBankResponse>(request, query, ContentType.XML);
        }

        private T Execute<T>(RestRequest request, SuggestQuery query, ContentType contentType) where T : new()
        {
            request.AddHeader("Authorization", "Token " + this.token);
            request.AddHeader("Content-Type", contentType.Name);
            request.AddHeader("Accept", contentType.Name);
            request.RequestFormat = contentType.Format;
            request.XmlSerializer.ContentType = contentType.Name;
            request.AddBody(query);
            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }
            return response.Data;
        }
    }
}