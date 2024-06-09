using Domain.Active.Enums;

namespace Domain.Entities
{
    public class Active
    {
        public int Id { get; set; }
        public ActiveTypes ActiveType { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }//Verificar
    }
}
