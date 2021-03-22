namespace Reloadly.Airtime.Operation
{
    public interface IReportOperations
    {
        TransactionHistoryOperations TransactionsHistory { get; }
    }
}