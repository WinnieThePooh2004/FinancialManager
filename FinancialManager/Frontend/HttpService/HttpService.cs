using Newtonsoft.Json;
using System.Collections;
using System.Security.Policy;

namespace Frontend.HttpService
{
    public class HttpService : IHttpService
    {
        public string FinancialOperationUri
            => $"{_client.BaseAddress}{System.Configuration.ConfigurationManager.AppSettings.Get("FinancialOperationsUri")}";
        public string OperationTypesUri
            => $"{_client.BaseAddress}{System.Configuration.ConfigurationManager.AppSettings.Get("OperationTypesUri")}";
        public string ReportUri
            => $"{_client.BaseAddress}{System.Configuration.ConfigurationManager.AppSettings.Get("ReportsUri")}";

        private static readonly HttpClient _client;
        static HttpService()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings.Get("BaseUri") ?? throw new Exception("Config is lost"))
            };
        }

        public HttpService()
        {
        }

        public async Task<T> GetObjectAsync<T>(string uri) where T : class
        {
            Console.WriteLine($"\nSending http get request to {uri}\n");
            using var response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"\nGet request\n{uri}\nnot successful\n");
            }
            using var streamReader = new StreamReader(await response.Content.ReadAsStreamAsync());
            using var reader = new JsonTextReader(streamReader);
            var list = new JsonSerializer().Deserialize<T>(reader);
            if (list is null)
            {
                throw new Exception(response.StatusCode.ToString());
            }
            Console.WriteLine($"\nGet request\n{uri}\nsuccessful\n");
            return list;
        }

        public async Task<T> GetObjectByIdAsync<T>(string uri, int id) where T : class
        {
            Console.WriteLine($"\nSending http get request to {uri}\n");
            using var response = await _client.GetAsync($"{uri}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"\nGet request\n{uri}\nnot successful\n");
            }
            using var streamReader = new StreamReader(await response.Content.ReadAsStreamAsync());
            using var reader = new JsonTextReader(streamReader);
            var list = new JsonSerializer().Deserialize<T>(reader);
            if (list is null)
            {
                throw new Exception(response.StatusCode.ToString());
            }
            Console.WriteLine($"\nGet request\n{uri}\nsuccessful\n");
            return list;

        }

        public async Task DeleteObject(string uri, int id)
        {
            var requestString = uri + "/" + id;
            Console.WriteLine($"Sending http delete request\n{requestString}");
            HttpResponseMessage response = await _client.DeleteAsync(requestString); ;
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Delete request \n{requestString}\nnot successful");
                throw new Exception(response.StatusCode.ToString());
            }
            Console.WriteLine($"\nDelete requst \n{requestString}\nsuccessful\n");
        }

        public async Task PostObject<T>(string uri, T @object) where T : class
        {
            Console.WriteLine($"\nSending http post request\n{uri}\n");
            using var response = await _client.PostAsJsonAsync(uri, @object);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"\nPost request\n{uri}\nnot successful\n");
                throw new Exception(response.StatusCode.ToString());
            }
            Console.WriteLine($"\nPost request\n{uri}\nsuccessful\n");
        }

        public async Task PutObject<T>(string uri, T @object) where T : class
        {
            Console.WriteLine($"\nSending http put request\n{uri}\n");
            var response = await _client.PutAsJsonAsync(uri, @object);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"\nPut request\n{uri}\nnot successful\n");
                throw new Exception(response.StatusCode.ToString());
            }
            Console.WriteLine($"\nPut request\n{uri}\nsuccessful\n");
        }
    }
}
