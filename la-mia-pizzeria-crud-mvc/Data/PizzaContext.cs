using la_mia_pizzeria_crud_mvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace la_mia_pizzeria_crud_mvc.Data
{
    public class PizzaContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Pizza>Pizze { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Ingredients> ingredients { get; set; }

        public const string CONNECTION_STRING = "Data Source=localhost;Initial Catalog=miapizzeria;Integrated Security=True;TrustServerCertificate=True";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CONNECTION_STRING);
        }
    }
}
