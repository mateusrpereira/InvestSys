using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options) { }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }
        public virtual DbSet<Portfolio> Portfolio { get; set; }
        public virtual DbSet<Active> Active { get; set; }
    }
}
