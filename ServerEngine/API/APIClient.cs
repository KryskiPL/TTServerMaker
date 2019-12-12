using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using RestSharp;

namespace ServerEngine.API
{
    public static class APIClient
    {
        private const string API_URL = "https://tthread.com/servermaker/get/v2/api";
        private static RestClient client;

        static APIClient()
        {
            client = new RestClient(API_URL);
        }

        public static void LoadPricing()
        {
            // TODO ez persze nem ide kell, csak próbálgatom

            RestRequest request = new RestRequest("purchase/get-pricing.php");
            IRestResponse response = client.Post(request);

            //MessageBox.Show(response.Content);
        }
    }
}
