using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AddNamesToDatabase.Repositories.RestClient
{
    public class RacingRestClient : IDisposable
    {
        private readonly HttpClient _instance;

        public RacingRestClient(string baseAdress)
        {
            _instance = new HttpClient { BaseAddress = new Uri(baseAdress) };
        }

        public async Task<IList<T>> GetData<T>(string endpoint)
        {
            var result = await _instance.GetAsync(endpoint);

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"error: {result.StatusCode}");
            }

            var json = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IList<T>>(json);
        }

        public bool CreateData<T>(string endpoint, T data)
        {
            var json = JsonConvert.SerializeObject(data).ToString();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = _instance.PostAsync(endpoint, content);
            response.Wait();

            return response.Result.IsSuccessStatusCode;
        }

        public bool UpdateData<T>(string endpoint, T data)
        {
            var json = JsonConvert.SerializeObject(data).ToString();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = _instance.PutAsync(endpoint, content);
            response.Wait();

            return response.Result.IsSuccessStatusCode;
        }

        public bool DeleteData(string endpoint, int id)
        {
            var response = _instance.DeleteAsync(endpoint + $"/{id}");
            response.Wait();

            return response.Result.IsSuccessStatusCode;
        }

        public void Dispose()
        {
            _instance?.Dispose();
        }
    }
}
