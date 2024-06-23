﻿namespace Domain.Transaction.Ports
{
    public interface ITransactionRepository
    {
        Task<Entities.Transaction> Get(int id);
        Task<int> Create(Entities.Transaction transaction);
    }
}
