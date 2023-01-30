namespace BLL.Models
{
    public class NewsDto
    {
        public string Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? PublishingDate { get; set; }
        public int FeedId { get; set; }
    }
}
