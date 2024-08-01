using ApiSample.Domain;

namespace Tests
{
    internal static class Configuration
    {
        static IEnumerable<Feed> _feeds;

        public static IEnumerable<Feed> GetFeeds()
        {
            if (_feeds != null) return _feeds;

            _feeds = new List<Feed>
            {
                new Feed { Id = "1", Title = "Feed 1", Content = "Content 1" },
                new Feed { Id = "2", Title = "Feed 2", Content = "Content 2" },
                new Feed { Id = "3", Title = "Feed 3", Content = "Content 3" },
                new Feed { Id = "4", Title = "Feed 4", Content = "Content 4" },
                new Feed { Id = "5", Title = "Feed 5", Content = "Content 5" },
                new Feed { Id = "6", Title = "Feed 6", Content = "Content 6" },
                new Feed { Id = "7", Title = "Feed 7", Content = "Content 7" },
                new Feed { Id = "8", Title = "Feed 8", Content = "Content 8" },
                new Feed { Id = "9", Title = "Feed 9", Content = "Content 9" },
                new Feed { Id = "10", Title = "Feed 10", Content = "Content 10" },
                new Feed { Id = "11", Title = "Feed 11", Content = "Content 11" },
                new Feed { Id = "12", Title = "Feed 12", Content = "Content 12" },
                new Feed { Id = "13", Title = "Feed 13", Content = "Content 13" },
                new Feed { Id = "14", Title = "Feed 14", Content = "Content 14" },
                new Feed { Id = "15", Title = "Feed 15", Content = "Content 15" },
                new Feed { Id = "16", Title = "Feed 16", Content = "Content 16" },
                new Feed { Id = "17", Title = "Feed 17", Content = "Content 17" },
                new Feed { Id = "18", Title = "Feed 18", Content = "Content 18" },
                new Feed { Id = "19", Title = "Feed 19", Content = "Content 19" },
                new Feed { Id = "20", Title = "Feed 20", Content = "Content 20" },
                new Feed { Id = "21", Title = "Feed 21", Content = "Content 21" },
                new Feed { Id = "22", Title = "Feed 22", Content = "Content 22" },
                new Feed { Id = "23", Title = "Feed 23", Content = "Content 23" },
                new Feed { Id = "24", Title = "Feed 24", Content = "Content 24" },
                new Feed { Id = "25", Title = "Feed 25", Content = "Content 25" },
                new Feed { Id = "26", Title = "Feed 26", Content = "Content 26" },
                new Feed { Id = "27", Title = "Feed 27", Content = "Content 27" },
                new Feed { Id = "28", Title = "Feed 28", Content = "Content 28" },
                new Feed { Id = "29", Title = "Feed 29", Content = "Content 29" },
                new Feed { Id = "30", Title = "Feed 30", Content = "Content 30" },
                new Feed { Id = "31", Title = "Feed 31", Content = "Content 31" },
                new Feed { Id = "32", Title = "Feed 32", Content = "Content 32" },
                new Feed { Id = "33", Title = "Feed 33", Content = "Content 33" },
                new Feed { Id = "34", Title = "Feed 34", Content = "Content 34" },
                new Feed { Id = "35", Title = "Feed 35", Content = "Content 35" },
                new Feed { Id = "36", Title = "Feed 36", Content = "Content 36" },
                new Feed { Id = "37", Title = "Feed 37", Content = "Content 37" },
                new Feed { Id = "38", Title = "Feed 38", Content = "Content 38" },
                new Feed { Id = "39", Title = "Feed 39", Content = "Content 39" },
                new Feed { Id = "40", Title = "Feed 40", Content = "Content 40" },
                new Feed { Id = "41", Title = "Feed 41", Content = "Content 41" },
                new Feed { Id = "42", Title = "Feed 42", Content = "Content 42" },
                new Feed { Id = "43", Title = "Feed 43", Content = "Content 43" },
                new Feed { Id = "44", Title = "Feed 44", Content = "Content 44" },
                new Feed { Id = "45", Title = "Feed 45", Content = "Content 45" },
                new Feed { Id = "46", Title = "Feed 46", Content = "Content 46" },
                new Feed { Id = "47", Title = "Feed 47", Content = "Content 47" },
                new Feed { Id = "48", Title = "Feed 48", Content = "Content 48" },
                new Feed { Id = "49", Title = "Feed 49", Content = "Content 49" },
                new Feed { Id = "50", Title = "Feed 50", Content = "Content 50" },
                new Feed { Id = "51", Title = "Feed 51", Content = "Content 51" },
                new Feed { Id = "52", Title = "Feed 52", Content = "Content 52" },
                new Feed { Id = "53", Title = "Feed 53", Content = "Content 53" },
                new Feed { Id = "54", Title = "Feed 54", Content = "Content 54" },
                new Feed { Id = "55", Title = "Feed 55", Content = "Content 55" },
                new Feed { Id = "56", Title = "Feed 56", Content = "Content 56" },
                new Feed { Id = "57", Title = "Feed 57", Content = "Content 57" },
                new Feed { Id = "58", Title = "Feed 58", Content = "Content 58" },
                new Feed { Id = "59", Title = "Feed 59", Content = "Content 59" },
                new Feed { Id = "60", Title = "Feed 60", Content = "Content 60" }
            };

            return _feeds;
        }
    }
}
