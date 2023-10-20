using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SocialMedia.Interact.Controllers;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Web;

namespace SocialMedia.Interact.Authenticators
{
    public class OAuthRequestHandler : ITokensRepository
    {
        private string identityServer;// = "https://api.twitter.com/oauth/request_token";//https://graph.facebook.com/v18.0/ //https://www.reddit.com/api/v1/access_token
        private string clientId;//t = "JbAMjFl4UdSpqpZ28hQFurQUx";
        private string clientSecret;//t = "kETOWLPVZKiLaaNVuHUASHHqqJqLrYKqE677cXgdksdmQHd7Ky";
        //fb token EAA0oJdcAlJwBO2khBECP3RN0ctnzQjP6pTHosT9ZAyT9F7CKvupZCvzPXzzw1uSKDQ4iZBMBZBSaTfYvBsj0cdmkuKhZCIIPJpk9OzCbpfJyO5yRt7KBIHXmoXzTUwbuWeC8HL5aTavFRvs8pdTZCnWZALDfZAyJUCy5mBiTTgCzQ37plLSyaqp37nisQPKoQ7pc

        //reddit client WUgF7JJKLd5r5qTqtfybKg
        //reddit secret UvfjEYl7MOuTmQYwBDV9LMMAwAsnxA
        private string token;
        private AuthRequest _authRequest;
        private readonly IHttpClientFactory _clientFactory;

        protected HttpClient Client { get; set; }

        public OAuthRequestHandler(IHttpClientFactory clientFactory, AuthRequest authRequest)
        {
            _clientFactory = clientFactory;
            _authRequest = authRequest;
            Client = clientFactory.CreateClient(client);
            identityServer = i;
            clientId = ci;
            clientSecret = cs;
            token = GetBearerToken();
        }

        public string GetBearerToken()
        {
            try
            {
                string bearerRequest =
                HttpUtility.UrlEncode(clientId) + ":" + HttpUtility.UrlEncode(clientSecret);

                bearerRequest =
                    Convert.ToBase64String(Encoding.UTF8.GetBytes(bearerRequest));

                // step 2: setup the request to obtain the Bearer Token from
                //         the Twitter API using the key and secret
                WebRequest request =
                    WebRequest.Create("https://api.twitter.com/oauth2/token");

                request.Headers.Add("Authorization", "Basic " + bearerRequest);
                request.Method = "POST";
                request.ContentType =
                    "application/x-www-form-urlencoded;charset=UTF-8";

                // step 3: set the OAuth Grant Type.
                // Using this Grant Type, we get a Bearer Token from Twitter if our
                // Consumer Key and Secret are valid.
                // (Twitter current only support "grant_type=Client_Credential)
                string grantType =
                    "grant_type=client_credentials";
                byte[] requestContent = Encoding.UTF8.GetBytes(grantType);

                // fetch the stream
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(requestContent, 0, requestContent.Length);
                requestStream.Close();

                string jsonResponse = string.Empty;

                // get the response
                HttpWebResponse response =
                    (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    jsonResponse = new StreamReader(responseStream).ReadToEnd();
                }

                JObject jObject = JObject.Parse(jsonResponse);
                return jObject["access_token"].ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> ExecuteServiceRequest<T>(HttpMethod method, string endpoint) where T : class
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

        public async Task<T> ExecutePostServiceRequest<T, TDataModel>(string endpoint, TDataModel dataModel) where T : class where TDataModel : class
        {
            var response = await Client.PostAsJsonAsync(endpoint, dataModel).ConfigureAwait(false);
            var returnedContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(JsonConvert.SerializeObject(new { StatusCode = (int)response.StatusCode, content = returnedContent }));
            }

            return JsonConvert.DeserializeObject<T>(returnedContent);
        }

        public async Task<T> ExecutePutServiceRequest<T, TDataModel>(string endpoint, TDataModel dataModel) where T : class where TDataModel : class
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