using CQRS.Design.Pattern.Entities;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Design.Pattern.Contexts
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
