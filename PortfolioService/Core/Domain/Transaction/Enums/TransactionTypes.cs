namespace Domain.Transaction.Enums
{
    public enum TransactionTypes
    {
        Buy = 0,// Compra
        Sell = 1,// Venda
        Transfer = 2,// Transferência
        Deposit = 3,// Depósito
        Withdrawal = 4,// Retirada
        Dividends = 5,// Dividendos
        Interest = 6,// Juros
        StockSplits = 7,// Splits de Ações
        MergersAcquisitions = 8,// Fusão/Aquisição
        Contribution = 9,// Contribuição
        Reinvestment = 10,// Reinvestimento
        Rebalancing = 11,// Rebalanceamento
    }
}
