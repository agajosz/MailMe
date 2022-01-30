using System;
using System.Collections.Generic;
using System.Web;

namespace MailMe.Jobs.Helpers
{
    public static class UriHelper
    {
        public static UriBuilder BuildUri(string basicUrl, string endpoint, Dictionary<string, string> queryParams)
        {
            var uri = BuildBaseAddress(basicUrl, endpoint);
            foreach (var (key, value) in queryParams)
            {
                uri.AppendQuery(key, value);
            }

            return uri;
        }

        public static UriBuilder BuildBaseAddress(string basicUrl, string endpoint)
        {
            var baseAddressString = string.Concat(basicUrl, endpoint);
            return new UriBuilder(baseAddressString);
        }

        public static UriBuilder AppendQuery(this UriBuilder uri, string key, string value)
        {
            var query = HttpUtility.ParseQueryString(uri.Query);
            query.Add(key, value);
            uri.Query = query.ToString() ?? string.Empty;
            
            return uri;
        }
    }
}