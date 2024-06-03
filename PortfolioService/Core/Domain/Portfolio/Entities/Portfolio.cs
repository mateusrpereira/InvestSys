namespace Domain.Entities
{
    public class Portfolio
    {
        public int Id { get; set; }
        public User UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
