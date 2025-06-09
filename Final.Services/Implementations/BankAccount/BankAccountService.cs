using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final.Repositories.Interfaces.BankAccount;
using Final.Repositories.Interfaces.UsersPerAcc;
using Final.Services.DTOs.BankAccount.Requests;
using Final.Services.Interfaces.BankAccount;
using Final.Services.DTOs.BankAccount;
using Final.Services.DTOs.Transfer.Responses;

namespace Final.Services.Implementations.BankAccount
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IUsersPerAccRepository _usersPerAccRepository;
        public BankAccountService(IBankAccountRepository bankAccountRepository, IUsersPerAccRepository usersPerAccRepository)
        {
            _usersPerAccRepository = usersPerAccRepository;
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<GetAccountResponse> GetAccountById(int accountId)
        {
            try
            {
                GetAccountResponse response = new GetAccountResponse();
                var account = await _bankAccountRepository.RetrieveAsync(accountId);
                response.Account = new AccDTO
                {
                    AccId = account.AccId,
                    AccNumber = account.AccNumber,
                    AccBalance = account.AccBalance
                };
                return response;
            }
            catch (Exception ex)
            {
                return new GetAccountResponse
                {
                    Message = ex.Message,
                    IsSuccesful = false
                };
            }
        }

        public async Task<AccountListResponse> GetAccountsOfUser(int UserId)
        {
            try
            {
                AccountListResponse response = new AccountListResponse();
                await foreach (var acc in _usersPerAccRepository.RetrieveCollectionAsync(new UsersPerAccFilter { UserId = UserId }))
                {
                    var accountdetail = await _bankAccountRepository.RetrieveAsync(acc.BankAccountId);
                    response.Accounts.Add(new AccDTO
                    {
                        AccId = accountdetail.AccId,
                        AccNumber = accountdetail.AccNumber,
                        AccBalance = accountdetail.AccBalance
                    });
                }
                return response;
            }
            catch (Exception ex)
            {
                return new AccountListResponse
                {
                    Message = ex.Message,
                    IsSuccesful = false
                };

            }
        }
    }
}
