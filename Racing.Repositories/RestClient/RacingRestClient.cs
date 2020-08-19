using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Racing.Repositories.RestClient
{
    public class RacingRestClient : IDisposable
    {
        private readonly HttpClient _instance;

        public RacingRestClient(string baseAdress)
        {
            _instance = new HttpClient { BaseAddress = new Uri(baseAdress) };
        }

        public async Task<IList<T>> GenerateNames<T>(string endpoint, int numberOfNames)
        {
            var result = await _instance.GetAsync(endpoint + $"?numberOfNames={numberOfNames}");

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"error: {result.StatusCode}");
            }

            var json = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IList<T>>(json);
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

        public async Task<T> GetDataByString<T>(string endpoint, string value)
        {
            var result = await _instance.GetAsync(endpoint + $"/{value}");

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"error: {result.StatusCode}");
            }

            var json = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json);
        }

        public void Dispose()
        {
            _instance?.Dispose();
        }
    }
}
