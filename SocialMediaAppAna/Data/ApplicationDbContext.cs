/*using SocialMediaAppAna.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;*/

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMediaAppAna.Models;
using static SocialMediaAppAna.Models.UserGroup;
using static SocialMediaAppAna.Models.Follow;

namespace SocialMediaAppAna.Data
{
    // PASUL 3: useri si roluri
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        //aici se pun restul claselor

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Follow> Follows { get; set; }
        //fct pt many-to-many

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            //relatii many-to-many
            //user - grup
            //cheie primara compusa
            modelBuilder.Entity<UserGroup>().HasKey(a => new { a.Id, a.UserId, a.GroupId });
            //relatii cu modelele User si Group
            modelBuilder.Entity<UserGroup>()
                .HasOne(a => a.User)
                .WithMany(a => a.UserGroups)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<UserGroup>()
                .HasOne(a => a.Group)
                .WithMany(a => a.UserGroups)
                .HasForeignKey(a => a.GroupId);
            //user-user
            //cheie primara compusa 
            modelBuilder.Entity<Follow>().HasKey(a => new { a.Id, a.FollowerId, a.FollowedId });
            //relatii cu modelele User si User
            modelBuilder.Entity<Follow>()
                .HasOne(a => a.Follower)
                .WithMany(a => a.Following)
                .HasForeignKey(a => a.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Follow>()
                .HasOne(a => a.Followed)
                .WithMany(a => a.Followers)
                .HasForeignKey(a => a.FollowedId);
                //.OnDelete(DeleteBehavior.Restrict);

        }
    }
}