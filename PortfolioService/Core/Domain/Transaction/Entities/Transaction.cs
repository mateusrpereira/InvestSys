using Domain.Transaction.Enums;
using Domain.Transaction.Ports;

namespace Domain.Entities
{
    public class Transaction
    {
        public Transaction()
        {
            Date = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public Portfolio Portfolio { get; set; }
        public Active Active { get; set; }
        public TransactionTypes TransactionType { get; set; }//Enum
        public int Quantity { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }//Ctor

        public async Task Save(ITransactionRepository transactionRepository)
        {
            if(Id == 0)
            {
                Id = await transactionRepository.Create(this);
            }
            else
            {
                await transactionRepository.Update(this);
            }
        }
    }
}
