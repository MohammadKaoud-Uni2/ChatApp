using ChatApp.Api.Data.Identity;
using ChatApp.Api.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Api.Data
{
    public class ApplicationDbContext:IdentityDbContext<CustomUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CustomUser>().Property(x=>x.City).IsRequired(required:false);
            builder.Entity<Message>().HasOne(x=>x.Sender).WithMany(x=>x.MessagesSent).HasForeignKey(x=>x.SenderId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Message>().HasOne(x=>x.Reciver).WithMany(x=>x.MessageRecived).HasForeignKey(x=>x.ReciverId).OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Photo> photos { get; set; }
        public DbSet<Message> messages { get; set; }

    }
}
