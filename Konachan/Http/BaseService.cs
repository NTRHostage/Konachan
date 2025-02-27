﻿using System;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Web.Http;

namespace Konachan.Http
{
    /// <summary>
    /// 实现HTTP请求的基础类
    /// </summary>
    class BaseService
    {
        ///<summary>
        ///发送Get请求
        /// </summary>
        public async static Task<string> SentGetAsync(string url)
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri(url);
            HttpResponseMessage msg = await client.GetAsync(uri);
            string msgContent = await msg.Content.ReadAsStringAsync();
            //System.Diagnostics.Debug.WriteLine(msgContent);
            return msgContent;
        }
        /// <summary>
        /// 获得Json数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<JsonObject> GetJson(string url)
        {
            string json = await BaseService.SentGetAsync(url);
            if (json != null)
                return JsonObject.Parse(json);
            else
                return null;
        }
        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static async Task<string> SendPostAsync(string url, string message, string refer = "http://konachan.net")
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Referer = new Uri(refer);
                    var response = await client.PostAsync(new Uri(url), new HttpStringContent(message, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/x-www-form-urlencoded"));
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
