namespace BLL.Models
{
    public class FeedDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? LastUpdatedDate { get; set; }
        public string UserId { get; set; }

        public virtual ICollection<NewsDto> News { get; set; } = new HashSet<NewsDto>();
    }
}
