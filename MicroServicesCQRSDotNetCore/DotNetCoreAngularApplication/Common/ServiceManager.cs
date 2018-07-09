using Dot.Net.Core.Common.Settings;
using Dot.Net.Core.Interfaces.Service;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DotNetCoreAngularApplication.Common
{
    public class ServiceManager : IManageApi
    {
        IOptions<AppSettingsConfig> _appSettings;

        public ServiceManager(IOptions<AppSettingsConfig> appSettings)
        {
            _appSettings = appSettings;
        }

        public T GetSynch<T>(string controller, string action = null, Dictionary<string, string> data = null)
        {
            string apiParameters = controller + (!string.IsNullOrEmpty(action) ? "/" + action : "");
            if (data != null)
            {
                foreach (KeyValuePair<string, string> keyValPair in data)
                {
                    apiParameters = apiParameters + "/" + keyValPair.Value;
                }
            }
            HttpClientHandler handler = new HttpClientHandler()
            {
                UseDefaultCredentials = true
            };
            using (HttpClient httpClient = new HttpClient(handler))
            {
                string serviceUrl = _appSettings.Value.ServiceURL + apiParameters;
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = httpClient.GetAsync(serviceUrl).Result;
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(response.ReasonPhrase, new Exception(response.Content.ReadAsStringAsync().Result));
                }

                //deserialise to the requested type
                return Deserialize<T>(response);
            }
        }

        public T PostSynch<T>(string controller, string action, object data)
        {

            HttpClientHandler handler = new HttpClientHandler()
            {
                UseDefaultCredentials = true
            };

            using (HttpClient httpClient = new HttpClient(handler))
            {

                string serviceUrl = _appSettings.Value.ServiceURL + controller + "/" + action;
                httpClient.DefaultRequestHeaders.Accept.Clear();
                HttpResponseMessage response = httpClient.PostAsJsonAsync(serviceUrl, data).Result;

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(response.ReasonPhrase, new Exception(response.Content.ReadAsStringAsync().Result));
                }

                //deserialise to the requested type                
                return Deserialize<T>(response);

            }
        }


        /// <summary>
        /// method to deserialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        private T Deserialize<T>(HttpResponseMessage response)
        {
            var deserialized = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result, SerializerSettings);
            return deserialized;
        }

        private static JsonSerializerSettings SerializerSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    TypeNameHandling = TypeNameHandling.Objects
                };
            }
        }
    }
}

