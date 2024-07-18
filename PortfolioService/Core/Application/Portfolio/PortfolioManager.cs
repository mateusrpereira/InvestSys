using Application.Portfolio.Dtos;
using Application.Portfolio.Ports;
using Application.Portfolio.Requests;
using Application.Portfolio.Responses;
using Domain.Portfolio.Exceptions;
using Domain.Portfolio.Ports;
using Domain.Ports;

namespace Application.Portfolio
{
    public class PortfolioManager : IPortfolioManager
    {
        private IPortfolioRepository _portfolioRepository;
        private IUserRepository _userRepository;//User existe em Portfolio
        public PortfolioManager(
            IPortfolioRepository portfolioRepository, 
            IUserRepository userRepository)
        {
            _portfolioRepository = portfolioRepository;
            _userRepository = userRepository;
        }

        public async Task<PortfolioResponse> CreatePortfolio(CreatePortfolioRequest request)
        {
            try
            {
                var portfolio = PortfolioDto.MapToEntity(request.Data);

                portfolio.User = await _userRepository.Get(request.Data.UserId);//Recuperar User

                //request.Data.Id = await _portfolioRepository.Create(portfolio);
                await portfolio.Save(_portfolioRepository);
                request.Data.Id = portfolio.Id;

                return new PortfolioResponse
                {
                    Data = request.Data,
                    Success = true,
                };
            }
            catch (MissingRequiredInformation)
            {
                return new PortfolioResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.PORTFOLIO_MISSING_REQUIRED_INFORMATION,
                    Message = "Missing required information passed"
                };
            }
            catch (UserIsRequiredInformation)
            {
                return new PortfolioResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.PORTFOLIO_MISSING_REQUIRED_INFORMATION,
                    Message = "The user id provided was not found"
                };
            }
            catch (Exception)
            {
                return new PortfolioResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.PORTFOLIO_COULD_NOT_STORE_DATA,
                    Message = "There was an error when saving to DB"
                };
            }
        }

        public async Task<PortfolioResponse> GetPortfolio(int portfolioId)
        {
            var portfolio = await _portfolioRepository.Get(portfolioId);

            if (portfolio == null) 
            {
                return new PortfolioResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.PORTFOLIO_NOT_FOUND,
                    Message = "No Portfolio record was found with the given Id"
                };
            }

            return new PortfolioResponse
            {
                Data = PortfolioDto.MapToDto(portfolio),
                Success = true,
            };
        }

        public async Task<PortfolioResponse> UpdatePortfolio(UpdatePortfolioRequest request)
        {
            try
            {
                var portfolio = PortfolioDto.MapToEntity(request.Data);

                portfolio.User = await _userRepository.Get(request.Data.UserId);//Recuperar UserId

                await portfolio.Save(_portfolioRepository);
                request.Data.Id = portfolio.Id;

                return new PortfolioResponse
                {
                    Data = request.Data,
                    Success = true,
                };
            }
            catch (Exception)
            {
                return new PortfolioResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.PORTFOLIO_UPDATE_FAILED,
                    Message = "There was an error when updating to DB"
                };
            }
        }

        public async Task<PortfolioResponse> DeletePortfolio(int portfolioId)
        {
            try
            {
                await _portfolioRepository.Delete(portfolioId);

                return new PortfolioResponse
                {
                    Success = true,
                };
            }
            catch (Exception)
            {
                return new PortfolioResponse()
                {
                    Success = false,
                    ErrorCode = ErrorCodes.PORTFOLIO_DELETE_FAILED,
                    Message = "There was an error when deleting to DB"
                };
            }
        }
    }
}
