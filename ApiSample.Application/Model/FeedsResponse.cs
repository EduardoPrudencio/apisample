using ApiSample.Domain;

namespace ApiSample.Application.Model
{
    public class FeedsResponse
    {
        public FeedsResponse(long totalFeeds , int totalPages, int feedsPerPages, int page, IEnumerable<Feed> feeds)
        {
            TotalFeeds = totalFeeds;
            TotalPages = totalPages;
            FeedsPerPages = feedsPerPages;
            Page = page;
            Feeds = feeds;
            CurrentCount = feeds.Count();
        }

        public long TotalFeeds { get; set; }
        public int TotalPages { get; set; }
        public int FeedsPerPages { get; set; }
        public int CurrentCount { get; set; }
        public int Page { get; set; }

        public IEnumerable<Feed> Feeds { get; set; }
    }
}
