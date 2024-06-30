using Domain.Active.Enums;
using Domain.Active.Ports;
using Domain.Ports;

namespace Domain.Entities
{
    public class Active
    {
        public int Id { get; set; }
        public ActiveTypes ActiveType { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public async Task Save(IActiveRepository activeRepository)
        {
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
