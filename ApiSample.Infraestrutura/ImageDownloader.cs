using System.Net;

namespace ApiSample.Infraestrutura
{
    public class ImageDownloader
    {
        public async Task SaveImageAsync(string url, string name)
        {
            string extensionImage = url.Split('.').Last();
            await Task.Run(() =>
            {
                if (!string.IsNullOrWhiteSpace(url))
                {
                    WebClient webClient = new();

                    using WebClient client = webClient;

                    try
                    {
                        if (!Directory.Exists("images"))
                            Directory.CreateDirectory("images");

                        client.DownloadFile(new Uri(url), $"images/{name}.{extensionImage}");
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            });

        }
    }
}