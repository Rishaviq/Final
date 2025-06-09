using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final.Services.DTOs.BankAccount.Requests;
using Final.Services.DTOs.Transfer.Responses;

namespace Final.Services.Interfaces.BankAccount
{
    public interface IBankAccountService
    {
        public Task<AccountListResponse> GetAccountsOfUser(int UserId);
        public Task<GetAccountResponse> GetAccountById(int accountId);
    }
}
