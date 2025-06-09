using System.ComponentModel.DataAnnotations;

namespace Final.Web.Models.BankAccounts
{
    public class AccountsListModel
    {
        public List<AccountModel> Accounts { get; set; } = new List<AccountModel>();
    }
}
