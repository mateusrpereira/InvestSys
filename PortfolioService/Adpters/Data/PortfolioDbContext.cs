using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options) { }
        public virtual DbSet<Domain.Entities.User> User { get; set; }
        public virtual DbSet<Domain.Entities.Transaction> Transaction { get; set; }
        public virtual DbSet<Domain.Entities.Portfolio> Portfolio { get; set; }
        public virtual DbSet<Domain.Entities.Active> Active { get; set; }
    }
}
