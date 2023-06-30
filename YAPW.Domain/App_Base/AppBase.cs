using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace YAPW.Domain.App_Base
{
    public class AppBase
    {
        public async Task<T> Get<T>(string samsaraUrl, string endpoint, string id, string token, bool errorTpe = true)
        {
            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer" + token);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = string.IsNullOrWhiteSpace(id) ?
                await client.GetAsync($"{samsaraUrl}/{endpoint}") :
                await client.GetAsync($"{samsaraUrl}/{endpoint}/{id}");


            if (response.IsSuccessStatusCode)
            {
                var result1 = await response.Content.ReadAsStringAsync();
                var result = await response.Content.ReadFromJsonAsync<T>();
                return result;
            }
            else
            {
                return default;
                //throw new Exception( response.Content.ReadAsStringAsync().Result);
            }
        }
        //testlogistics
        public async Task<TReturnType> Post<TReturnType, TInput>(string samsaraUrl, string endpoint,
            TInput dataModel, string token, string id = null, bool errorTpe = true) where TReturnType : class
        {
            HttpResponseMessage response;
            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer" + token);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (id == null)
            {
                response = await client.PostAsJsonAsync($"{samsaraUrl}/{endpoint}", dataModel);
            }
            else
            {
                var content = dataModel == null
                        ? new StringContent(JsonConvert.SerializeObject(""), Encoding.UTF8, "application/json")
                        : new StringContent(JsonConvert.SerializeObject(dataModel), Encoding.UTF8, "application/json");
                response = await client.PatchAsync($"{samsaraUrl}/{endpoint}/{id}", content);
            }

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TReturnType>();
                return result;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException(error);
            }
        }

        public async Task<(string rawResponse, HttpStatusCode statusCode, T returnedElement)> GetElementFromApiAsync<T>
            (string requestUri, IHttpClientFactory httpClientFactory, string clientString)
        {
            try
            {
                (string, HttpStatusCode, T) result;

                var client = httpClientFactory.CreateClient(clientString);
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

        public async Task<(string rawResponse, HttpStatusCode statusCode, List<T> returnedElement)> GetListOfElementsFromApiAsync<T>
            (string requestUri, IHttpClientFactory httpClientFactory, string clientString)
        {
            try
            {
                (string, HttpStatusCode, List<T>) result;

                var client = httpClientFactory.CreateClient(clientString);
                var request = new HttpRequestMessage(HttpMethod.Get,
                    requestUri);
                var response = await client.SendAsync(request);
                var returnedContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    result = (returnedContent, response.StatusCode, default(List<T>));
                    return result;
                }

                var hack = returnedContent.Split("<div ").First();

                var element = JsonConvert.DeserializeObject<List<T>>(hack);
                result = (hack, response.StatusCode, element);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}