using System;
using System.Collections.Generic;
using System.Linq;

public class DummyTransactionSource : ITransactionSource
{
    private int _count;

    public DummyTransactionSource(int count)
    {
        _count = count;
    }

    public IEnumerable<Transaction> GetTransactions(DateTime startDate, DateTime endDate)
    {
        Random random = new Random();
        string[] currencies = { "CAD", "USD", "EUR" };

        for (int i = 0; i < _count; i++)
        {
            var transactionDate = startDate + TimeSpan.FromSeconds(random.Next((int)(endDate - startDate).TotalSeconds));
            var amount = (decimal)random.NextDouble() * 100;
            var currency = currencies[random.Next(currencies.Length)];
            var cardNumber = "4111111111111111"; // Dummy card number for illustration

            yield return new Transaction(
                TransactionId: Guid.NewGuid(),
                TransactionDate: transactionDate,
                CardNumber: cardNumber,
                Amount: amount,
                Currency: currency
            );
        }
    }
}

public class TransactionBatchSource
{
    private ITransactionSource _transactionSource;
    private int _batchSize;

    public TransactionBatchSource(ITransactionSource transactionSource, int batchSize)
    {
        _transactionSource = transactionSource;
        _batchSize = batchSize;
    }

    public IEnumerable<Batch> GetBatches(DateTime startDate, DateTime endDate)
    {
        List<Transaction> transactions = _transactionSource.GetTransactions(startDate, endDate).ToList();
        int batchId = 1;

        var batchesByCurrency = transactions
            .GroupBy(t => t.Currency)
            .Select(group => new
            {
                Currency = group.Key,
                Batches = group
                    .Select((transaction, index) => new { Transaction = transaction, Index = index })
                    .GroupBy(pair => pair.Index / _batchSize, pair => pair.Transaction)
            });

        foreach (var currencyGroup in batchesByCurrency)
        {
            foreach (var batchTransactions in currencyGroup.Batches)
            {
                yield return new Batch
                {
                    BatchId = batchId++,
                    Transactions = batchTransactions.ToList()
                };
            }
        }
    }
}