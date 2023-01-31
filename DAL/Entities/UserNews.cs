using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
    public  class UserNews
    {
        public string UserId { get; set; }
        public virtual IdentityUser? User { get; set; }

        public string NewsId { get; set; }
        public virtual News? News { get; set; }

        public bool IsRead { get; set; }
    }
}
