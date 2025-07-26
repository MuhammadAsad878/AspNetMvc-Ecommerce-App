using EcommerceRazor.Model;
using Microsoft.EntityFrameworkCore;

namespace EcommerceRazor.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; } // Table name is Categories
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasKey(c => c.Id); // Setting Id as primary key
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Adventure", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Comedy", DisplayOrder = 3 }
            );

            //base.OnModelCreating(modelBuilder);
            //Here we can add any custom configurations for our models if needed
            // For example, we can set the table name explicitly or configure relationships
            //modelBuilder.Entity<Category>().ToTable("Categories");
        }
    }
}
