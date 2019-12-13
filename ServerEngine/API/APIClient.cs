// <copyright file="APIClient.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.ServerEngine.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using RestSharp;

    /// <summary>
    /// The API client in charge of getting data from the TThread API.
    /// </summary>
    public static class APIClient
    {
        /// <summary>
        /// The URL of the API root.
        /// </summary>
        private const string APIURL = "https://tthread.com/servermaker/get/v2/api/";

        /// <summary>
        /// The Rest client.
        /// </summary>
        private static RestClient client;

        static APIClient()
        {
            client = new RestClient(APIURL);
        }

        private static string PricingUrl => APIURL + "purchase/get-pricing.php";

        /// <summary>
        /// Loads the pricing from the website.
        /// </summary>
        public static void LoadPricing()
        {
            // TODO ez persze nem ide kell, csak próbálgatom
            RestRequest request = new RestRequest(PricingUrl);
            IRestResponse response = client.Post(request);
        }
    }
}