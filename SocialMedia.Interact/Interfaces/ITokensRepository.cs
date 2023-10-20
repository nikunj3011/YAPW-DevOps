using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Web;

namespace SocialMedia.Interact.Controllers
{
    public interface ITokensRepository
    {
        Task<T> ExecutePostServiceRequest<T, TDataModel>(string endpoint, TDataModel dataModel)
            where T : class
            where TDataModel : class;
        Task<T> ExecutePutServiceRequest<T, TDataModel>(string endpoint, TDataModel dataModel)
            where T : class
            where TDataModel : class;
        Task<T> ExecuteServiceRequest<T>(HttpMethod method, string endpoint) where T : class;
        string GetBearerToken();
    }
}