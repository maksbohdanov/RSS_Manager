using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
    public class News: EntityInfo
    {
        public string Id { get; set; } 
        public DateTime? PublishingDate { get; set; }

        public virtual Feed? Feed { get; set; }
        public int FeedId { get; set; }

        public virtual ICollection<IdentityUser> Users { get; set; } = new HashSet<IdentityUser>();
        public virtual ICollection<UserNews> UserNews { get; set; } = new HashSet<UserNews>();

    }
}
