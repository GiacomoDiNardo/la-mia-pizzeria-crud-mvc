using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Forms;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Data
{
    public class PizzeriaDbContext : DbContext
    {
        public DbSet<Pizza> Pizze { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=pizzeria-1-db;Integrated Security=True;Encrypt=false;");

        }
    }
}
