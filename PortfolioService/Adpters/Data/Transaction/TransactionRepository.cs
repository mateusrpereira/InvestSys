using Domain.Transaction.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Transaction
{
    public class TransactionRepository : ITransactionRepository
    {
        private PortfolioDbContext _portfolioDbContext;
        public TransactionRepository(PortfolioDbContext portfolioDbContext)
        {
            _portfolioDbContext = portfolioDbContext;
        }

        public async Task<int> Create(Domain.Entities.Transaction transaction)
        {
            _portfolioDbContext.Transaction.Add(transaction);
            await _portfolioDbContext.SaveChangesAsync();
            return transaction.Id;
        }

        public async Task<Domain.Entities.Transaction> Get(int id)
        {
            return await _portfolioDbContext.Transaction
                .Include(p => p.Portfolio)
                .Include(a => a.Active)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<int> Update(Domain.Entities.Transaction transaction)
        {
            _portfolioDbContext.Transaction.Update(transaction);
            await _portfolioDbContext.SaveChangesAsync();
            return transaction.Id;
        }

        public async Task Delete(int id)
        {
            var transactionId = await Get(id);
            _portfolioDbContext.Transaction.Remove(transactionId);
            await _portfolioDbContext.SaveChangesAsync();
        }
    }
}
