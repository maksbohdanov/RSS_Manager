namespace DAL.Entities
{
    public class News: EntityInfo
    {
        public string Id { get; set; } 
        public DateTime? PublishingDate { get; set; }

        public virtual Feed? Feed { get; set; }
        public int FeedId { get; set; }
    }
}
