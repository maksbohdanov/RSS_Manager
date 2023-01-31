using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
    public class Feed: EntityInfo
    {
        public int Id { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        public virtual ICollection<IdentityUser> Users { get; set; } = new HashSet<IdentityUser>();

        public virtual ICollection<News> News { get; set; } = new HashSet<News>();
    }
}
