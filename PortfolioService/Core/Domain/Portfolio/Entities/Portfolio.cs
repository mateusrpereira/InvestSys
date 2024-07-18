using Domain.Portfolio.Exceptions;
using Domain.Portfolio.Ports;
using Domain.User;

namespace Domain.Entities
{
    public class Portfolio
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        private void ValidateState()
        {
            if (User == null)
            {
                throw new UserIsRequiredInformation();
            }

            if (string.IsNullOrEmpty(Name) ||
                string.IsNullOrEmpty(Description))
            {
                throw new MissingRequiredInformation();
            }
        }

        public async Task Save(IPortfolioRepository portfolioRepository)
        {
            this.ValidateState();

            if (Id == 0)
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
