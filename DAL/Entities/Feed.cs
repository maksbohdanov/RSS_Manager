using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
    public class Feed: EntityInfo
    {
        public int Id { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        public virtual IdentityUser? User { get; set; }
        public string UserId { get; set; } 

        public virtual ICollection<News> News { get; set; } = new HashSet<News>();
    }
}
