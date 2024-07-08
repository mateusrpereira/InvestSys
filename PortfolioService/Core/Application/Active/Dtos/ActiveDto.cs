using Domain.Active.Enums;
using Domain.Entities;
using Entities = Domain.Entities;

namespace Application.Active.Dtos
{
    public class ActiveDto
    {
        public int Id { get; set; }
        public ActiveTypes ActiveType { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public static Entities.Active MapToEntity(ActiveDto activeDto)
        {
            return new Entities.Active
            {
                Id = activeDto.Id,
                ActiveType = activeDto.ActiveType,
                Name = activeDto.Name,
                Code = activeDto.Code,
            };
        }
        public static ActiveDto MapToDto(Entities.Active active)
        {
            return new ActiveDto 
            {
                Id = active.Id,
                ActiveType = active.ActiveType,
                Name = active.Name,
                Code = active.Code,
            };
        }
    }
}
