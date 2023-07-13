using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace YAPW.Service.Broker.Extentions
{
    public static class HostingEnvironmentExtention
    {
        public const string LocalToDevEnvironment = "LocalToDev";
        public const string LocalToProdEnvironment = "LocalToProd";
        public const string LocaEnvironment = "Local";

        public static bool IsLocalToDev(this IWebHostEnvironment hostingEnvironment)
        {
            return hostingEnvironment.IsEnvironment(LocalToDevEnvironment);
        }

        public static bool IsLocal(this IWebHostEnvironment hostingEnvironment)
        {
            return hostingEnvironment.IsEnvironment(LocaEnvironment);
        }

        public static bool IsLocalToProd(this IWebHostEnvironment hostingEnvironment)
        {
            return hostingEnvironment.IsEnvironment(LocalToProdEnvironment);
        }
    }
}
