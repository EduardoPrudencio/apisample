using System.Xml.Linq;

namespace RssReader
{
    public class Reader
    {
        public IList<Feed> Parse(string url)
        {
            try
            {
                XDocument feed = XDocument.Load(url);

                var entries = from item in feed.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")

                              select new Feed
                              {
                                  Title = item.Elements().FirstOrDefault(i => i.Name.LocalName == "title") != null
                                    ? item.Elements().First(i => i.Name.LocalName == "title").Value
                                    : "",
                                  Author = item.Elements().FirstOrDefault(i => i.Name.LocalName == "author") != null
                                    ? item.Elements().First(i => i.Name.LocalName == "author").Value
                                    : "",
                                  Id = item.Elements().FirstOrDefault(i => i.Name.LocalName == "guid") != null ?
                                    item.Elements().First(i => i.Name.LocalName == "guid").Value
                                    : "",
                                  Content = item.Elements().FirstOrDefault(i => i.Name.LocalName == "description") != null
                                    ? item.Elements().First(i => i.Name.LocalName == "description").Value
                                    : "",

                                  Link = item.Elements().FirstOrDefault(i => i.Name.LocalName == "link") != null
                                    ? item.Elements().First(i => i.Name.LocalName == "link").Value
                                    : "",
                                  Image = (item.Elements().FirstOrDefault(i => i.Name.LocalName == "image") != null)
                                    ? item.Elements().First(i => i.Name.LocalName == "image").FirstAttribute.Value
                                    : "",
                                  PublishDate = item.Elements().FirstOrDefault(i => i.Name.LocalName == "pubDate") != null
                                    ? Convert.ToDateTime(item.Elements().First(i => i.Name.LocalName == "pubDate").Value)
                                    : Convert.ToDateTime("Fri, 19 Sep 2014 00:50:00 +0000")
                              };

                return entries.ToList();
            }
            catch (Exception exp)
            {
                return new List<Feed>();
            }
        }
    }
}
