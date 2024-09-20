using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {

        }


        // Configure o contexto para usar o SQLite
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }

        public DbSet<Produto> Produtos { get; set; }

    }
}
