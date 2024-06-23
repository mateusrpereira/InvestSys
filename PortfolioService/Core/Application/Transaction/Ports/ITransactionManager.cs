using Application.Transaction.Requests;
using Application.Transaction.Responses;

namespace Application.Transaction.Ports
{
    public interface ITransactionManager
    {
        Task<TransactionResponse> CreateTransaction(CreateTransactionRequest request);
        Task<TransactionResponse> GetTransaction(int transactionId);
    }
}
