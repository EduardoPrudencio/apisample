using System.Net;

namespace RssReader
{
    internal class ImageDownloader
    {
        async Task SaveImageAsync(string url, string name)
        {
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

                        client.DownloadFile(new Uri(url), $"images/{name}");
                    }
                    catch (Exception exp)
                    {
                        // WriteError($"Ocorreu o seguinte erro: {exp.Message} - {exp.StackTrace}");
                    }
                }
            });

        }
    }
}
