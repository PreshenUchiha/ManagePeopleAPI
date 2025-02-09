using HttpClientLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientLibrary.HttpClientService
{
    public class HttpClientHelper(HttpClient _httpClient) : IHttpClientHelper
    {
        public async Task<List<T>> HttpRetrieveAllAsync<T>(string RequestUri)
        {
            var httpResponse = await _httpClient.GetAsync(RequestUri);
            httpResponse.EnsureSuccessStatusCode();

            var result = await httpResponse.Content.ReadFromJsonAsync<List<T>>();

            return result!;
        }

        public async Task<T> HttpRetrieveByIdAsync<T>(string RequestUri)
        {
            var httpResponse = await _httpClient.GetAsync(RequestUri);
            httpResponse.EnsureSuccessStatusCode();

            var result = await httpResponse.Content.ReadFromJsonAsync<T>();

            return result!;
        }

        public async Task<HttpResponseMessage> HttpPostAsync<T>(string RequestUri, T model)
        {
            var httpResponse = await _httpClient.PostAsJsonAsync(RequestUri, model);
            httpResponse.EnsureSuccessStatusCode();

            return httpResponse;
        }

        public async Task<HttpResponseMessage> HttpPutAsync<T>(string RequestUri, T model)
        {
            var httpResponse = await _httpClient.PutAsJsonAsync(RequestUri, model);
            httpResponse.EnsureSuccessStatusCode();

            return httpResponse;
        }

        public async Task<HttpResponseMessage> HttpDeleteAsync(string RequestUri)
        {
            var httpResponse = await _httpClient.DeleteAsync(RequestUri);
            httpResponse.EnsureSuccessStatusCode();

            return httpResponse;
        }

       
    }
}
