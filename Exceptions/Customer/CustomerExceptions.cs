namespace BankAccountAPI.Exceptions.Customer
{
    public class CustomerExceptions
    {
        public class CustomerNotFoundException : Exception
        {
            public CustomerNotFoundException(string message) : base(message) { }
        }
    }
}
