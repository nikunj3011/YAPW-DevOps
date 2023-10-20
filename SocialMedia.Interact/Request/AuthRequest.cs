using Microsoft.AspNetCore.Mvc;

namespace SocialMedia.Interact.Controllers
{
    public class AuthRequest
    {
        public string IdentityServer { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Client { get; set; }
    }
}