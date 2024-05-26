namespace ApiSample.Domain
{
    public struct Feed
    {
        public string? Id { get; set; }
        public string? Author { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Link { get; set; }
        public string Image { get; set; }
        public DateTime PublishDate { get; set; }
    }
}