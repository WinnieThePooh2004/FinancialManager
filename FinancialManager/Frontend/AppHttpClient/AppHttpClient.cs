using FinancialManager.DTOs.FinancialOperations;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace Frontend.AppHttpClient
{
    public class AppHttpClient
    {
        public static string FinancialOperationUrl => "https://localhost:7054/api/FinancialOperations";
        public static string OperationTypesUrl => "https://localhost:7054/api/OperationTypes";
        public static string ReportUrl => "https://localhost:7054/api/Reports";

        private HttpClient _client = new();
        public AppHttpClient()
        {

        }

        public T GetObjects<T>(string url) where T : class
        {
            Console.WriteLine($"\nSending http get request to {url}\n");
            using var response = _client.GetAsync(url).Result;
            if(!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"\nGet request\n{url}\nnot successful\n");
            }
            using var streamReader = new StreamReader(response.Content.ReadAsStream());
            using var reader = new JsonTextReader(streamReader);
            var list = new JsonSerializer().Deserialize<T>(reader);
            if(list is null)
            {
                throw new Exception(response.StatusCode.ToString());
            }
            Console.WriteLine($"\nGet request\n{url}\nsuccessful\n");
            return list;
        }

        public async Task DeleteObject(string url, int id)
        {
            var requestString = url + "/" + id;
            Console.WriteLine($"Sending http delete request\n{requestString}");
            HttpResponseMessage response = await _client.DeleteAsync(requestString); ;
            if(!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Delete request \n{requestString}\nnot successful");
                throw new Exception(response.StatusCode.ToString());
            }
            Console.WriteLine($"\nDelete requst \n{requestString}\nsuccessful\n");
        }

        public async Task PostObject<T>(string url, T @object)
        {
            Console.WriteLine($"\nSending http post request\n{url}\n");
            using var response = await _client.PostAsJsonAsync(url, @object);
            if(!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"\nPost request\n{url}\nnot successful\n");
                throw new Exception(response.StatusCode.ToString());
            }
            Console.WriteLine($"\nPost request\n{url}\nsuccessful\n");
        }

        public async Task PutObject<T>(string url, T @object)
        {
            Console.WriteLine($"\nSending http put request\n{url}\n");
            var response = await _client.PutAsJsonAsync(url, @object);
            if(!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"\nPut request\n{url}\nnot successful\n");
                throw new Exception(response.StatusCode.ToString());
            }
            Console.WriteLine($"\nPut request\n{url}\nsuccessful\n");
        }
    }
}
