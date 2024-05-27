namespace BankAccountAPI.Exceptions.Account
{
    public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException(string message) : base(message) { }
    }

    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException(string message) : base(message) { }
    }
}
