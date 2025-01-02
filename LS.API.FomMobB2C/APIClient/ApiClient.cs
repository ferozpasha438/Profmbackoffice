﻿
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CIN.Application;
using LS.API.FomMobB2C.HttpContext;

namespace LS.API.FomMobB2C
{
    public partial class ApiClient
    {
        private readonly HttpClient _HttpClient;
        private Uri BaseEndpoint { get; set; }
        string ApiContent = string.Empty;

        public ApiClient(Uri baseEndpoint)
        {
            if (baseEndpoint == null)
            {
                throw new ArgumentNullException("baseEndpoint");
            }
            BaseEndpoint = baseEndpoint;
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            _HttpClient = new HttpClient(clientHandler);
        }
        private void AddHeaders()
        {
           

            _HttpClient.DefaultRequestHeaders.Remove("ApiEndPoint");
            _HttpClient.DefaultRequestHeaders.Remove("Accept-Language");
            _HttpClient.DefaultRequestHeaders.Remove("ConnectionString");




            _HttpClient.DefaultRequestHeaders.Add("ApiEndPoint", Convert.ToString(ClaimSettings.WebAPIURL));
            _HttpClient.DefaultRequestHeaders.Add("Accept-Language", ClaimSettings.Language);
            _HttpClient.DefaultRequestHeaders.Add("ConnectionString", ClaimSettings.DbConnectionString);
          
        }

        private static NameValueCollection CreateNvc(string[] columns, params object[] inputparams)
        {
            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            for (int i = 0; i < inputparams.Length; i++)
            {
                queryString[columns[i]] = Convert.ToString(inputparams[i]);
            }
            return queryString;
        }

        private string ToQueryString(NameValueCollection nvc)
        {
            var array = (from key in nvc.AllKeys
                         from value in nvc.GetValues(key)
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)))
                .ToArray();
            return "?" + string.Join("&", array);
        }

        /// <summary>  
        /// Common method for making GET calls  
        /// </summary>  
        private async Task<T> GetAsync<T>(Uri requestUrl)
        {
            AddHeaders();
            var response = await _HttpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
            }
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        /// <summary>
        /// Get data with generic type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUrl"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<APIMessageDto<T>> GetAsync<T>(Uri requestUrl, object param = null)
        {
            try
            {

                AddHeaders();
                var response = await _HttpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
                ApiContent = await GetApiContentAsync(response);
                if (response.IsSuccessStatusCode && ApiContent is not null)
                {
                    response.EnsureSuccessStatusCode();
                    return GetAPIResponse<T>();
                }
                return GetAPIErrorResponse<T>();
            }
            catch (Exception ex)
            {
                return GetAPIErrorResponse<T>();
            }
        }

        /// <summary>
        /// Get data with generic type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUrl"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<APIMessageDto<T>> GetWithQueryParamsAsync<T>(Uri requestUrl, string queryParams)
        {
            try
            {
                AddHeaders();
                var response = await _HttpClient.GetAsync($"{requestUrl}?{queryParams}", HttpCompletionOption.ResponseHeadersRead);
                ApiContent = await GetApiContentAsync(response);
                if (response.IsSuccessStatusCode && ApiContent is not null)
                {
                    response.EnsureSuccessStatusCode();
                    return GetAPIResponse<T>();
                }
                return GetAPIErrorResponse<T>();
            }
            catch (Exception ex)
            {
                return GetAPIErrorResponse<T>();
            }
        }

        /// <summary>
        /// Get data with generic type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUrl"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<APIListMessageDto<PaginatedList<T>>> GetPaginatedListAsync<T>(Uri requestUrl, string queryParams)
        {
            try
            {
                AddHeaders();
                var response = await _HttpClient.GetAsync($"{requestUrl}?{queryParams}", HttpCompletionOption.ResponseHeadersRead);
                ApiContent = await GetApiContentAsync(response);
                if (response.IsSuccessStatusCode && ApiContent is not null)
                {
                    response.EnsureSuccessStatusCode();
                    return GetAPIPagedListResponse<PaginatedList<T>>();
                }
                return GetAPIPagedListResponse<PaginatedList<T>>();
            }
            catch (Exception ex)
            {
                return GetAPIPagedListResponse<PaginatedList<T>>();
            }
        }


        /// <summary>
        /// Get data with generic type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUrl"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<APIListMessageDto<T>> GetPaginatedListOneAsync<T>(Uri requestUrl, string queryParams)
        {
            try
            {
                AddHeaders();
                var response = await _HttpClient.GetAsync($"{requestUrl}", HttpCompletionOption.ResponseHeadersRead);
                ApiContent = await GetApiContentAsync(response);
                if (response.IsSuccessStatusCode && ApiContent is not null)
                {
                    response.EnsureSuccessStatusCode();
                    return GetAPIPagedListResponse<T>();
                }
                return GetAPIErrorPagedListResponse<T>();
            }
            catch (Exception ex)
            {
                return GetAPIErrorPagedListResponse<T>();
            }
        }

        private async Task<APIMessageDto<T>> PostAsync<T>(Uri requestUrl, T content)
        {
            try
            {
                AddHeaders();
                var response = await _HttpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
                ApiContent = await GetApiContentAsync(response);
                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    var data = await response.Content.ReadAsStringAsync();
                    return GetAPIResponse<T>();
                }
                return GetAPIErrorResponse<T>();
            }
            catch (Exception)
            {
                return GetAPIErrorResponse<T>();
            }
        }
        private async Task<APIMessageDto<T1>> PostAsync<T1, T2>(Uri requestUrl, T2 content)
        {
            try
            {
                AddHeaders();
                var response = await _HttpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T2>(content));
                ApiContent = await GetApiContentAsync(response);
                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    return GetAPIResponse<T1>(); // (JsonConvert.DeserializeObject<APIMessage<T1>>(ApiContent), string.Empty);
                }
                return GetAPIErrorResponse<T1>();
            }
            catch (Exception)
            {
                return GetAPIErrorResponse<T1>();
            }
        }

        private APIMessageDto<T> GetAPIResponse<T>() => new APIMessageDto<T> { Data = JsonConvert.DeserializeObject<T>(ApiContent) };
        private APIMessageDto<T> GetAPIErrorResponse<T>() => new APIMessageDto<T> { Message = ApiMessageInfo.Failed };
        private APIListMessageDto<T> GetAPIPagedListResponse<T>() => new APIListMessageDto<T> { Data = JsonConvert.DeserializeObject<T>(ApiContent) };
        private APIListMessageDto<T> GetAPIErrorPagedListResponse<T>() => new APIListMessageDto<T> { Message = ApiMessageInfo.Failed };



        private async Task<T1> PostAsyncSpl<T1, T2>(Uri requestUrl, T2 content)
        {
            AddHeaders();
            var response = await _HttpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T2>(content));
            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
            }
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T1>(data);
        }
        private async Task<string> GetApiContentAsync(HttpResponseMessage response) => await response.Content.ReadAsStringAsync();
        private async Task<T> GetAsyncSpl<T>(Uri requestUrl, object param = null)
        {
            AddHeaders();
            string data = null;
            var response = await _HttpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
            }
            data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

      
        private async Task<APIMessage<T1>> GetDataPostAsync<T1, T2>(Uri requestUrl, T2 content)
        {
            AddHeaders();
            var response = await _HttpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T2>(content));
            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
            }
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<APIMessage<T1>>(data);
        }

        private Uri CreateRequestUri(string relativePath, string queryString = "")
        {
            var endpoint = new Uri(BaseEndpoint, relativePath);
            var uriBuilder = new UriBuilder(endpoint);
            uriBuilder.Query = queryString;
            return uriBuilder.Uri;
        }

        private HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content, MicrosoftDateFormatSettings);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private static JsonSerializerSettings MicrosoftDateFormatSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
            }
        }

    }
}
