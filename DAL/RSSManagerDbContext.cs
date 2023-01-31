using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class RssManagerDbContext: IdentityDbContext<IdentityUser>
    {
        public DbSet<News> News { get; set; }
        public DbSet<Feed> Feeds { get; set; }

        public RssManagerDbContext(DbContextOptions<RssManagerDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<News>()
                .HasOne(n => n.Feed)
                .WithMany(f => f.News)
                .HasForeignKey(n => n.FeedId);

            builder.Entity<Feed>()
                .HasMany(f => f.Users)
                .WithMany()
                .UsingEntity(j => j.ToTable("Subscription"));

            builder.Entity<News>()
                .HasMany(n => n.Users)
                .WithMany()
                .UsingEntity<UserNews>(
                    j => j
                     .HasOne(un => un.User)
                     .WithMany()
                     .HasForeignKey(un => un.UserId),
                    j => j
                     .HasOne(un => un.News)
                     .WithMany(n => n.UserNews)
                     .HasForeignKey(un => un.NewsId),
                    j => 
                     {
                         j.HasKey(k => new { k.UserId, k.NewsId });
                         j.ToTable("UserNews");
                     });

            base.OnModelCreating(builder);
        }
    }
}
