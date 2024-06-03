namespace Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public Portfolio PortfolioId { get; set; }
        public Active ActiveId { get; set; }
        public string TransactionType { get; set; }//Verificar
        public int Quantity { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }//Definir Data Atual
    }
}
