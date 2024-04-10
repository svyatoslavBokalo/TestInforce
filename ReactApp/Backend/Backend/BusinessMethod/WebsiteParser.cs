using Backend.Models;
using HtmlAgilityPack;
using System;
using System.Net;

namespace Backend.BusinessMethod
{
    public class WebsiteParser
    {
        public WebsiteInfo ParseWebsite(string url)
        {
            try
            {
                var web = new HtmlWeb();
                string decodedUrl = DecodedUrl(url);
                var doc = web.Load(decodedUrl);

                var titleNode = doc.DocumentNode.SelectSingleNode("//title");
                var descriptionNode = doc.DocumentNode.SelectSingleNode("//meta[@name='description']");
                string shortUrl = ShortingUrl(decodedUrl);

                var websiteInfo = new WebsiteInfo();
                if (titleNode != null)
                    websiteInfo.Title = titleNode.InnerText;
                if (descriptionNode != null)
                    websiteInfo.Description = descriptionNode.GetAttributeValue("content", "");
                if(shortUrl != null)
                    websiteInfo.ShortUrl = shortUrl;

                return websiteInfo;
            }
            catch (Exception ex)
            {
                throw new Exception($"Помилка під час парсингу веб-сторінки: {ex.Message}");
                return null;
            }
        }

        static public string ShortingUrl(string url)
        {
            url = url.Replace("http://", "").Replace("https://", "");
            int index = url.IndexOf("/");
            if (index != (url.Length - 1) && index != -1)
            {
                int nextIndex = url.IndexOf("/", index + 2);
                if (nextIndex != -1)
                {
                    url = url.Substring(0, nextIndex);
                }
                else
                {
                    url = url.Substring(0, index);
                }
            }

            return url;
        }

        static public string DecodedUrl(string encodedUrl)
        {
            return WebUtility.UrlDecode(encodedUrl);
        }
        //я відмовився від цієї функцію, тому що вона зазвичай має багато даних, яких не посплітих по якомусь точному критерію
        //тому вирішив її просто залишити
        public async Task GetRobotTXT(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(new Uri(new Uri(url), "/robots.txt"));
                    if (response.IsSuccessStatusCode)
                    {
                        string robotsTxtContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(robotsTxtContent);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Сталася помилка: {ex.Message}");
                }
            }
        }
    }
}
