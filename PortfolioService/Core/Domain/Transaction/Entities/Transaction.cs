using Domain.Transaction.Enums;

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
    }
}
