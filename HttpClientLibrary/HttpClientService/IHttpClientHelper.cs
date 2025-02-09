using HttpClientLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientLibrary.HttpClientService
{
    public interface IHttpClientHelper
    {
        Task<HttpResponseMessage> HttpDeleteAsync(string RequestUri);
        Task<HttpResponseMessage> HttpRetrieveAccessTokenAsync(AccessTokenModel model);
        Task<List<T>> HttpRetrieveAllAsync<T>(string RequestUri);
        Task<T> HttpRetrieveByIdAsync<T>(string RequestUri);
        Task<HttpResponseMessage> HttpPostAsync<T>(string RequestUri, T model);
        Task<HttpResponseMessage> HttpPutAsync<T>(string RequestUri, T model);
    }

}