using System.Net;

namespace ApiSample.Infraestrutura
{
    public class ImageDownloader
    {
        public ImageDownloader() { }

        public async Task SaveImageAsync(string url, string name)
        {
            string extensionImage = string.Empty;

            if (url != null)
            {
                extensionImage = url.Split('.').Last();
            }

            await Task.Run(async () =>
            {
                if (!string.IsNullOrWhiteSpace(url))
                {
                    try
                    {
                        using HttpClient client = new();

                        HttpResponseMessage response = client.GetAsync(url).Result;
                        response.EnsureSuccessStatusCode();

                        if (!Directory.Exists("images"))
                            Directory.CreateDirectory("images");

                        using (Stream responseStream = await response.Content.ReadAsStreamAsync())
                        {
                            using (FileStream fileStream = new FileStream($"images/{name}.{extensionImage}", FileMode.Create, FileAccess.Write))
                            {
                                await responseStream.CopyToAsync(fileStream);
                            }
                        }
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