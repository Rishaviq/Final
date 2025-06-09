namespace Final.Web.Models.BankAccounts
{
    public class AccountModel
    {
        public int AccId { get; set; }

        public required string AccNumber { get; set; }

        public decimal AccBalance { get; set; }
       
    }
}
