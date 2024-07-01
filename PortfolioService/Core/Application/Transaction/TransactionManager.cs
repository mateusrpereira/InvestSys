using Application.Transaction.Dtos;
using Application.Transaction.Ports;
using Application.Transaction.Requests;
using Application.Transaction.Responses;
using Domain.Active.Ports;
using Domain.Portfolio.Ports;
using Domain.Transaction.Ports;

namespace Application.Transaction
{
    public class TransactionManager : ITransactionManager
    {
        private ITransactionRepository _transactionRepository;
        private IPortfolioRepository _portfolioRepository;
        private IActiveRepository _activeRepository;

        public TransactionManager(
            ITransactionRepository transactionRepository, 
            IPortfolioRepository portfolioRepository, 
            IActiveRepository activeRepository)
        {
            _transactionRepository = transactionRepository;
            _portfolioRepository = portfolioRepository;
            _activeRepository = activeRepository;
        }

        public async Task<TransactionResponse> CreateTransaction(CreateTransactionRequest request)
        {
            try
            {
                var transaction = TransactionDto.MapToEntity(request.Data);
                transaction.Portfolio = await _portfolioRepository.Get(request.Data.PortfolioId);
                transaction.Active = await _activeRepository.Get(request.Data.ActiveId);

                //request.Data.Id = await _transactionRepository.Create(transaction);
                await transaction.Save(_transactionRepository);
                request.Data.Id = transaction.Id;

                return new TransactionResponse
                {
                    Data = request.Data,
                    Success = true,
                };
            }
            catch (Exception)
            {
                return new TransactionResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.COULD_NOT_STORE_DATA,
                    Message = "There was an error when saving to DB"
                };
            }
        }

        public async Task<TransactionResponse> GetTransaction(int transactionId)
        {
            var transaction = await _transactionRepository.Get(transactionId);

            if (transaction == null)
            {
                return new TransactionResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.TRANSACTION_NOT_FOUND,
                    Message = "No Transaction record was found with the given Id"
                };
            }

            return new TransactionResponse
            {
                Data = TransactionDto.MapToDto(transaction),
                Success = true,
            };
        }

        public async Task<TransactionResponse> UpdateTransaction(UpdateTransactionRequest request)
        {
            try
            {
                var transaction = TransactionDto.MapToEntity(request.Data);
                transaction.Portfolio = await _portfolioRepository.Get(request.Data.PortfolioId);//Recupera Portfolio
                transaction.Active = await _activeRepository.Get(request.Data.ActiveId);//Recupera Active

                await transaction.Save(_transactionRepository);
                request.Data.Id = transaction.Id;

                return new TransactionResponse
                {
                    Data = request.Data,
                    Success = true,
                };
            }
            catch (Exception)
            {
                return new TransactionResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.PORTFOLIO_UPDATE_FAILED,
                    Message = "There was an error when updating to DB"
                };
            }
        }

        public async Task<TransactionResponse> DeleteTransaction(int transactionId)
        {
            try
            {
                await _transactionRepository.Delete(transactionId);

                return new TransactionResponse
                {
                    Success = true
                };
            }
            catch (Exception)
            {
                return new TransactionResponse()
                {
                    Success = false,
                    ErrorCode = ErrorCodes.PORTFOLIO_DELETE_FAILED,
                    Message = "There was an error when deleting to DB"
                };
            }
        }
    }
}
