using Domain.Transaction.Enums;
using Entities = Domain.Entities;

namespace Application.Transaction.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public int ActiveId { get; set; }
        public TransactionTypes TransactionType { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }//verificar se é necessário, pois será gerado automaticamente

        public static Entities.Transaction MapToEntity(TransactionDto transactionDto)
        {
            return new Entities.Transaction
            {
                Id = transactionDto.Id,
                Portfolio = new Entities.Portfolio { Id = transactionDto.PortfolioId },
                Active = new Entities.Active { Id = transactionDto.ActiveId },
                TransactionType = transactionDto.TransactionType,
                Quantity = transactionDto.Quantity,
                Price = transactionDto.Price,
                Date = transactionDto.Date,//verificar se é necessário, pois será gerado automaticamente
            };
        }
        public static TransactionDto MapToDto(Entities.Transaction transaction)
        {
            return new TransactionDto
            {
                Id = transaction.Id,
                PortfolioId = transaction.Portfolio.Id,
                ActiveId = transaction.Active.Id,
                TransactionType = transaction.TransactionType,
                Quantity = transaction.Quantity,
                Price = transaction.Price,
                Date = transaction.Date,
            };
        }

    }
}
