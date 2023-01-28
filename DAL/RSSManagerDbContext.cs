using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class RSSManagerDbContext: IdentityDbContext<IdentityUser>
    {
        public DbSet<News> News { get; set; }
        public DbSet<Feed> Feeds { get; set; }

        public RSSManagerDbContext(DbContextOptions<RSSManagerDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<News>()
                .HasOne(n => n.Feed)
                .WithMany(f => f.News)
                .HasForeignKey(n => n.FeedId);

            builder.Entity<Feed>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId);

            base.OnModelCreating(builder);
        }
    }
}
