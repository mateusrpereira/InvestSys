﻿using Application.Portfolio.Requests;
using Application.Portfolio.Responses;

namespace Application.Portfolio.Ports
{
    public interface IPortfolioManager
    {
        Task<PortfolioResponse> CreatePortfolio(CreatePortfolioRequest request);
        Task<PortfolioResponse> GetPortfolio(int  portfolioId);
    }
}
