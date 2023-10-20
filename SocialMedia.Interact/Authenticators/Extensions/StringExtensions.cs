using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SocialMedia.Interact.Controllers;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SocialMedia.Interact.Authenticators
{
    public static class StringExtensions
    {
        public static string URLEncodeAndToBase64(this AuthRequest authRequest)
        {
            string bearerRequest =
                    HttpUtility.UrlEncode(authRequest.ClientId) + ":" + HttpUtility.UrlEncode(authRequest.ClientSecret);

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(bearerRequest));
        }
    }
}