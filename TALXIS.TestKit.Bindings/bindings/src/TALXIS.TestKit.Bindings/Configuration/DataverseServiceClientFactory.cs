using Microsoft.PowerPlatform.Dataverse.Client;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TALXIS.TestKit.Bindings.Configuration
{
    public static class DataverseServiceClientFactory
    {
        public static ServiceClient CreateWithToken(string dataverseUrl,string accessToken)
        {
            return new ServiceClient(
                new Uri(dataverseUrl),
                (authority) => Task.FromResult(accessToken),
                true,
                null);
        }

        public static ServiceClient CreateWithClientCredentials(string dataverseUrl, ClientCredentials credentials)
        {
            string connectionString = $"AuthType=ClientSecret;Url={ExtractBaseUrl(dataverseUrl)};ClientId={credentials.ClientId};ClientSecret={credentials.ClientSecret};TenantId={credentials.TenantId};";

            return new ServiceClient(
                dataverseConnectionString: connectionString);
        }

        public static string ExtractBaseUrl(string fullUrl)
        {
            if (string.IsNullOrWhiteSpace(fullUrl))
                throw new ArgumentException("URL is empty", nameof(fullUrl));

            var uri = new Uri(fullUrl);

            // Собираем базовый URL (scheme + host)
            return $"{uri.Scheme}://{uri.Host}";
        }
    }
}
