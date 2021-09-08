using Microsoft.EntityFrameworkCore;

namespace Sepidar.Api.DataAccess.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Sepidar.Api.DataAccess.Models.Module.Calendar> Calendars{ get; set; }

        public DbSet<Sepidar.Api.DataAccess.Models.Module.Content> Contents{ get; set; }

        public DbSet<Sepidar.Api.DataAccess.Models.Module.Gallery> Galleries{ get; set; }

        public DbSet<Sepidar.Api.DataAccess.Models.Module.Setting> Settings{ get; set; }

        public DbSet<Sepidar.Api.DataAccess.Models.Module.Slider> Sliders{ get; set; }

        public DbSet<Sepidar.Api.DataAccess.Models.Security.Admin> Admins{ get; set; }

        public DbSet<Sepidar.Api.DataAccess.Models.Security.EndUser> EndUsers{ get; set; }

        public DbSet<Sepidar.Api.DataAccess.Models.Security.User> Users{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sepidar.Api.DataAccess.Models.Module.Calendar>().ToTable("Calendars","Module");

            modelBuilder.Entity<Sepidar.Api.DataAccess.Models.Module.Content>().ToTable("Contents","Module");

            modelBuilder.Entity<Sepidar.Api.DataAccess.Models.Module.Gallery>().ToTable("Galleries","Module");

            modelBuilder.Entity<Sepidar.Api.DataAccess.Models.Module.Setting>().ToTable("Settings","Module");

            modelBuilder.Entity<Sepidar.Api.DataAccess.Models.Module.Slider>().ToTable("Sliders","Module");

            modelBuilder.Entity<Sepidar.Api.DataAccess.Models.Security.Admin>().ToTable("Admins","Security");
			modelBuilder.Entity<Sepidar.Api.DataAccess.Models.Security.Admin>().Property(i => i.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Sepidar.Api.DataAccess.Models.Security.EndUser>().ToTable("EndUsers","Security");
			modelBuilder.Entity<Sepidar.Api.DataAccess.Models.Security.EndUser>().Property(i => i.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Sepidar.Api.DataAccess.Models.Security.User>().ToTable("Users","Security");

            base.OnModelCreating(modelBuilder);
        }
    }
}
