namespace BankAccountAPI.DTOs.Transaction
{
    public class TransactionSuccessDTO
    {
        public double Amount { get; set; }
        public double BalanceBefore { get; set; }
        public double BalanceAfter { get; set; }
    }
}
