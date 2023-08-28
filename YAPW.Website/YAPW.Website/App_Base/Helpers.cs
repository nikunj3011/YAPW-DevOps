using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Ditech.Portal.NET.App_Base
{
    public static class Helpers
    {
        public static string ToQueryString(this Dictionary<string, string> dictQueryString)
        {
            var queryStringBuilder = new StringBuilder();
            var uppercent = "";
            var counter = 1;

            queryStringBuilder.Append("?");

            foreach (var kvp in dictQueryString)
            {
                uppercent = dictQueryString.Count() > counter ? "&" : "";
                queryStringBuilder.Append(kvp.Key + "=" + kvp.Value + uppercent);
                counter++;
            }

            return queryStringBuilder.ToString();
        }

        public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }
    }
}
