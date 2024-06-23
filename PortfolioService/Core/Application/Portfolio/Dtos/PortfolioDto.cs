using Application.User.Dtos;
using Domain.Entities;
using Entities = Domain.Entities;

namespace Application.Portfolio.Dtos
{
    public class PortfolioDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static Entities.Portfolio MapToEntity(PortfolioDto portfolioDto)
        {
            return new Entities.Portfolio
            {
                Id = portfolioDto.Id,
                User = new Entities.User { Id = portfolioDto.UserId },//bo aqui
                Name = portfolioDto.Name,
                Description = portfolioDto.Description
            };
        }

        public static PortfolioDto MapToDto(Entities.Portfolio portfolio)
        {
            return new PortfolioDto
            { 
                Id = portfolio.Id,
                UserId = portfolio.User.Id,
                Name = portfolio.Name,
                Description = portfolio.Description, 
            };
        }
    }
}
