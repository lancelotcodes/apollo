using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Drawing;
using System.IO;
using System.Net;

namespace apollo.Presentation.Utils
{
    public static class UrlImageUtils
    {

        public static Stream GetStreamFromUrl(string url)
        {
            byte[] imageData = null;

            using (var wc = new WebClient())
            {
                imageData = wc.DownloadData(url);
            }


            return new MemoryStream(imageData);
        }

        public static Stream AccessFileUsingBasicAuthentication()
        {
            byte[] imageData = null;
            var client = new RestClient(new RestClientOptions
            {
                BaseUrl = new Uri("https://www.filestackapi.com/api/store/S3?key=" + "ApCcw2CNPQqK8qHL8YFaEz"),
                Authenticator = new HttpBasicAuthenticator("app", "2IRSYXZYYRAMPE4JGFR2J5C34E")
            });

            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("url", "https://cdn.filestackcontent.com/cache=false/B6jg7HjARlOmoFhPOLUR");

            imageData = client.DownloadData(request);
            return new MemoryStream(imageData);
        }

        public static bool IsValidImage(byte[] bytes)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(bytes))
                    Image.FromStream(ms);
            }
            catch (ArgumentException)
            {
                return false;
            }
            return true;
        }
    }
}
