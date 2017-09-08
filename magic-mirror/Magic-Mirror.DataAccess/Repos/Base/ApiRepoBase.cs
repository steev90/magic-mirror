﻿using MagicMirror.DataAccess.Entities;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace MagicMirror.DataAccess.Repos
{
    public abstract class ApiRepoBase<T> : IApiRepo<T> where T : IEntity
    {
        protected virtual string _apiUrl { get; set; }
        protected virtual string _apiId { get; set; }
        protected virtual string _url { get; set; }

        public virtual Task<T> GetEntityAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetJsonAsync()
        {
            try
            {
                var client = new HttpClient();
                string result = await client.GetStringAsync(_url);

                return result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Unable to retrieve JSON from server", ex);
            }
        }

        public virtual async Task<HttpResponseMessage> GetHttpResponseFromApiAsync()
        {
            var client = new HttpClient();
            HttpResponseMessage _response = await client.GetAsync(_url);

            if (_response.IsSuccessStatusCode)
            {
                return _response;
            }
            else
            {
                throw new HttpRequestException($"{_response.StatusCode}: {_response.ReasonPhrase}");
            }
        }

        /// <summary>
        /// Convert a string of Json into a C# Magic-Mirror Data Entity class
        /// </summary>
        /// <param name="json">Json string to convert to C# POCO</param>
        /// <returns></returns>
        protected T ConvertJsonToEntity(string json)
        {
            try
            {
                T entity = JsonConvert.DeserializeObject<T>(json);
                return entity;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Cannot convert Json to Entity", e);
            }
        }
    }
}