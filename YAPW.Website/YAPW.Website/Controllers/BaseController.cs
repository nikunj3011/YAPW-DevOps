using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace YAPW.Website.Controllers
{
    public class BaseController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        protected HttpClient Client { get; set; }

        protected BaseController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            Client = clientFactory.CreateClient("api");
        }

        protected async Task<T> ExecuteServiceRequest<T>(HttpMethod method, string endpoint) where T : class
        {
            var request = new HttpRequestMessage(method, endpoint);
            var response = await Client.SendAsync(request).ConfigureAwait(false);
            var returnedContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(JsonConvert.SerializeObject(new { StatusCode = (int)response.StatusCode, content = returnedContent }));
            }

            return JsonConvert.DeserializeObject<T>(returnedContent);
        }

        protected async Task<T> ExecutePostServiceRequest<T, TDataModel>(string endpoint, TDataModel dataModel) where T : class where TDataModel : class
        {
            var response = await Client.PostAsJsonAsync(endpoint, dataModel).ConfigureAwait(false);
            var returnedContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(JsonConvert.SerializeObject(new { StatusCode = (int)response.StatusCode, content = returnedContent }));
            }

            return JsonConvert.DeserializeObject<T>(returnedContent);
        }

        protected async Task<T> ExecutePutServiceRequest<T, TDataModel>(string endpoint, TDataModel dataModel) where T : class where TDataModel : class
        {
            var response = await Client.PutAsJsonAsync(endpoint, dataModel).ConfigureAwait(false);
            var returnedContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(JsonConvert.SerializeObject(new { StatusCode = (int)response.StatusCode, content = returnedContent }));
            }

            return JsonConvert.DeserializeObject<T>(returnedContent);
        }
    }
}
