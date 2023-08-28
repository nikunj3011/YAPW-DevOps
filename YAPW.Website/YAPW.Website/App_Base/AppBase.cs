using Newtonsoft.Json;
using NuGet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ditech.Portal.NET.App_Base
{
    public class AppBase
    {
        public async Task<(string rawResponse, HttpStatusCode statusCode, List<T> returnedElements)> GetListOfElementsFromApiAsync<T>
            (string requestUri, IHttpClientFactory httpClientFactory)
        {
            try
            {
                (string, HttpStatusCode, List<T>) result;

                var client = httpClientFactory.CreateClient("api");
                var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
                var response = await client.SendAsync(request);
                var returnedContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var statusCode = (int)response.StatusCode;
                    result = (returnedContent, response.StatusCode, default(List<T>));
                    return result;
                } 
                var elements = JsonConvert.DeserializeObject<List<T>>(returnedContent).ToList();
                result = (returnedContent, response.StatusCode, elements);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(string rawResponse, HttpStatusCode statusCode, T returnedElement)> GetElementFromApiAsync<T>
            (string requestUri, IHttpClientFactory httpClientFactory)
        {
            try
            {
                (string, HttpStatusCode, T) result;

                var client = httpClientFactory.CreateClient("api");
                var request = new HttpRequestMessage(HttpMethod.Get,
                    requestUri);
                var response = await client.SendAsync(request);
                var returnedContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    result = (returnedContent, response.StatusCode, default(T));
                    return result;
                }
                var element = JsonConvert.DeserializeObject<T>(returnedContent);
                result = (returnedContent, response.StatusCode, element);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public async Task<(string rawResponse, HttpStatusCode statusCode, T2 returnedElement)> PostFromApiAsync<T1, T2>
            (string endpoint, IHttpClientFactory httpClientFactory, T1 element, bool isPut = false)
        {
            try
            {
                (string, HttpStatusCode, T2) result;
                var client = httpClientFactory.CreateClient("api");
                var returnedElement = (T2)Activator.CreateInstance(typeof(T2));

                //client.BaseAddress = new Uri(requestUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(element), Encoding.UTF8, "application/json");
                var x = content.ReadAsStringAsync();
                HttpResponseMessage response;
                if (!isPut)
                {
                    response = await client.PostAsync(endpoint, content);
                }
                else
                {
                    response = await client.PutAsync(endpoint, content);
                }
                var data = await response.Content.ReadAsStringAsync();
                result = (data, response.StatusCode, returnedElement);

                if (!response.IsSuccessStatusCode)
                {
                    result = (data, response.StatusCode, default(T2));
                    return result;
                }
                result = (data, response.StatusCode, JsonConvert.DeserializeObject<T2>(data));
                return result;      
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(string rawResponse, HttpStatusCode statusCode)> PostToApiAsync
            (string endpoint, IHttpClientFactory httpClientFactory, bool isPut = false)
        {
            (string, HttpStatusCode) result;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            try
            {

                var client = httpClientFactory.CreateClient("api");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                StringContent content = new StringContent("", Encoding.UTF8, "application/json");
                var x = content.ReadAsStringAsync();
                HttpResponseMessage response;
                if (!isPut)
                {
                    response = await client.PostAsync(endpoint, content);
                }
                else
                {
                    response = await client.PutAsync(endpoint, content);
                }
                var data = await response.Content.ReadAsStringAsync();
                result = (data, response.StatusCode);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(message: await response.Content.ReadAsStringAsync());
                }
                result = (data, response.StatusCode);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<(string rawResponse, HttpResponseMessage message, T returnedElement)> DeleteFromApiAsync<T>
            (string requestUri, IHttpClientFactory httpClientFactory)
        {
            try
            {
                (string, HttpResponseMessage, T) result;
                var returnedElement = (T)Activator.CreateInstance(typeof(T));

                var client = httpClientFactory.CreateClient("api");
                var request = new HttpRequestMessage(HttpMethod.Delete,
                    requestUri);
                var response = await client.SendAsync(request);
                var returnedContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var statusCode = (int)response.StatusCode;
                    result = (returnedContent, response, default(T));
                    return result;
                }

                result = (returnedContent, response, default(T));
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}