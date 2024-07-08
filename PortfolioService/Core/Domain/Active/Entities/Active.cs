using Domain.Active.Enums;
using Domain.Active.Exceptions;
using Domain.Active.Ports;
using Domain.Ports;

namespace Domain.Entities
{
    public class Active
    {
        public int Id { get; set; }
        public ActiveTypes ActiveType { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        private void ValidateState()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new MissingRequiredInformation();
            }

            if (!Enum.IsDefined(typeof(ActiveTypes), ActiveType))
            {
                throw new InvalidActiveTypeException();
            }

            if (Code <= 0)//B.O no merge...
            {
                throw new InvalidCodeException();
            }
        }
        public async Task Save(IActiveRepository activeRepository)
        {
            this.ValidateState();

            if (Id == 0)
            {
                Id = await activeRepository.Create(this);
            }
            else
            {
                await activeRepository.Update(this);
            }
        }
    }
}
