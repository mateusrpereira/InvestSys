using Domain.Portfolio.Ports;

namespace Domain.Entities
{
    public class Portfolio
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public async Task Save(IPortfolioRepository portfolioRepository)
        {
            if(Id == 0)
            {
                Id = await portfolioRepository.Create(this);
            }
            else
            {
                await portfolioRepository.Update(this);
            }
        }
    }
}
