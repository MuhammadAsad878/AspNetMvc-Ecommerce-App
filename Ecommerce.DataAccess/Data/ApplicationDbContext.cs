using Ecom.DataAccess;
using Microsoft.EntityFrameworkCore;
using Ecom.Models;

namespace FirstProject.Data
{
    // any class for EF must implement DbContext class
    // then add DbContextOptions<ApplicationDbcontext> options in constructor with : base (options) also 
    // then register this ApplicationDbContext in builder.builder.Services.AddDbContext<ApplicationDbContext>(options =>
   // options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
   // );  telling we are using SQlServer as options in our Application DBContext
   // before builder

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }
        // This line wil create a table category of type Category Class in Models in databaes
        public DbSet<Category> Categories { get; set; } // Table name is Categories
        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Category Data
            modelBuilder.Entity<Category>().HasData(
               new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
               new Category { Id = 2, Name = "Adventure", DisplayOrder = 2 },
               new Category { Id = 3, Name = "Comedy", DisplayOrder = 3 }
           );

            // Seed Product Data
            modelBuilder.Entity<Product>().HasData(
      new Product
      {
          Id = 1,
          Title = "Fortune of Time",
          Description = "Praesent vitae sodales libero. Curabitur at pulvinar elit.",
          ISBN = "SWD9999001",
          Author = "Billy Spark",
          Price = 90,
          Price50 = 85,
          Price100 = 80,
          CategoryId = 1,
          ImageUrl = "" // Add a default image URL or leave it empty if not applicable
      },
      new Product
      {
          Id = 2,
          Title = "The Silent Observer",
          Description = "Etiam auctor, magna vel malesuada tempor, augue velit aliquet sapien.",
          ISBN = "SWD9999002",
          Author = "Nancy Drew",
          Price = 120,
          Price50 = 110,
          Price100 = 100,
          CategoryId = 1,
          ImageUrl = ""
      },
      new Product
      {
          Id = 3,
          Title = "Mystic River Tales",
          Description = "Suspendisse potenti. Nullam ac nisi non sapien pulvinar faucibus.",
          ISBN = "SWD9999003",
          Author = "James Walker",
          Price = 75,
          Price50 = 70,
          Price100 = 65,
          CategoryId = 1,
          ImageUrl = ""
      },
      new Product
      {
          Id = 4,
          Title = "Digital Dreams",
          Description = "Integer vel arcu nec augue dapibus fermentum a a ligula.",
          ISBN = "SWD9999004",
          Author = "Sara Connor",
          Price = 150,
          Price50 = 140,
          Price100 = 130,
          CategoryId = 1,
          ImageUrl = ""
      },
      new Product
      {
          Id = 5,
          Title = "Echoes of Eternity",
          Description = "Morbi accumsan metus at ipsum euismod, ac porta velit volutpat.",
          ISBN = "SWD9999005",
          Author = "Tony Stark",
          Price = 200,
          Price50 = 190,
          Price100 = 180,
          CategoryId = 1,
          ImageUrl = ""
      },
      new Product
      {
          Id = 6,
          Title = "Code of Shadows",
          Description = "Ut vehicula risus in ex blandit, ut facilisis ex luctus.",
          ISBN = "SWD9999006",
          Author = "Bruce Wayne",
          Price = 110,
          Price50 = 100,
          Price100 = 95,
          CategoryId = 1,
          ImageUrl = ""
      },
      new Product
      {
          Id = 7,
          Title = "Journey to Nowhere",
          Description = "Phasellus at dignissim augue. In nec tellus magna.",
          ISBN = "SWD9999007",
          Author = "Lara Craft",
          Price = 85,
          Price50 = 80,
          Price100 = 75,
          CategoryId = 1,
          ImageUrl = ""
      }
  );






            //modelBuilder.Entity<Category>().HasKey(c => c.Id); // Setting Id as primary key
            //base.OnModelCreating(modelBuilder);
            //// Here we can add any custom configurations for our models if needed
            //// For example, we can set the table name explicitly or configure relationships
            //modelBuilder.Entity<Category>().ToTable("Categories");
        }




    }
}
