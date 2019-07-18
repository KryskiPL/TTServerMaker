using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace ServerEngine.API.Purchase
{
    class CurrencyExchange
    {
        private const string ExchangeApi = "https://api.exchangeratesapi.io/latest";


        public Dictionary<string, double> ExchangeRates;

        public CurrencyExchange()
        {
            LoadExchangeRates();
        }

        private Dictionary<string, double> LoadExchangeRates()
        {
            RestClient client = new RestClient(ExchangeApi);
            RestRequest request = new RestRequest();

            // Setting base currency to USD
            request.AddParameter("base", "USD");

            // Getting the response, and getting the needed part from the json
            IRestResponse response = client.Get(request);
            var rates = JObject.Parse(response.Content).GetValue("rates");

            // Serializing response
            ExchangeRates =  JsonConvert.DeserializeObject<Dictionary<string, double>>(rates.ToString());

            return ExchangeRates;
        }
    

    }
}
