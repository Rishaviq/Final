using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Azure.Core;
using Final.Repositories.Interfaces.BankAccount;
using Final.Repositories.Interfaces.Transfer;
using Final.Repositories.Interfaces.UsersPerAcc;
using Final.Services.DTOs.Transfer;
using Final.Services.DTOs.Transfer.Requests;
using Final.Services.DTOs.Transfer.Responses;
using Final.Services.Interfaces.Transfer;
using Final.Services.Interfaces.UsersPerAcc;

namespace Final.Services.Implementations.Transfer
{
    public class TransferService : ITransferService
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IUsersPerAccRepository _usersPerAccRepository;

        public TransferService(ITransferRepository transferRepository, IBankAccountRepository bankAccountRepository, IUsersPerAccRepository usersPerAccRepository)
        {
            _usersPerAccRepository = usersPerAccRepository;
            _bankAccountRepository = bankAccountRepository;
            _transferRepository = transferRepository;
        }

        public async Task<TransferResponse> CancelTransfer(AuthRequest Request)
        {
            try
            {
                TransferResponse response = new TransferResponse();

                var transferDetails = await _transferRepository.RetrieveAsync(Request.TransferId);
                if (transferDetails.UserId == Request.UserId
                    && transferDetails.TransferStatus == "ИЗЧАКВА"
                    )
                {
                    TransferUpdate update = new TransferUpdate
                    {
                        TransferStatus = "ОТКАЗАН"
                    };
                    response.IsSuccesful = await _transferRepository.UpdateAsync(transferDetails.TransferId, update);
                }
                else
                {
                    response.IsSuccesful = false;
                    response.Message = "You are not authorized to cancel this transfer.";
                }
                return response;
            }
            catch (Exception ex)
            {
                return new TransferResponse
                {
                    IsSuccesful = false,
                    Message = ex.Message
                };
            }

        }

        public async Task<CreateTransferResponse> CreateTransfer(TransferRequest transferRequest)
        {
            try
            {
                CreateTransferResponse response = new CreateTransferResponse();



                if (Regex.IsMatch(transferRequest.GoingToAccNumber, @"^[A-Za-z0-9]{22}$")
                    && transferRequest.TransferReason.Length <= 32
                    && decimal.Round(transferRequest.TransferAmount, 2) == transferRequest.TransferAmount
                    )
                {
                    int goingToId = 0;
                    bool accExist = false;
                    await foreach (var acc in _bankAccountRepository.RetrieveCollectionAsync(new BankAccountFilter { AccNumber = transferRequest.GoingToAccNumber }))
                    {
                        goingToId = acc.AccId;
                        accExist = true;
                        break;
                    }
                    if (accExist)
                    {
                        response.CreatedTransferId = await _transferRepository.CreateAsync(new Models.Transfer
                        {

                            GoingToId = goingToId,
                            SenderId = transferRequest.SenderId,
                            TransferAmount = transferRequest.TransferAmount,
                            TransferReason = transferRequest.TransferReason,
                            UserId = transferRequest.UserId,
                            TransferStatus = "ИЗЧАКВА"
                        });
                    }
                    else
                    {
                        response.IsSuccesful = false;
                        response.Message = "The account you want to transfer money to doesn't exist";
                    }

                }
                else
                {
                    response.IsSuccesful = false;
                    response.Message = "Invalid input data such as account number or transfer reason";

                }
                return response;
            }
            catch (Exception ex)
            {
                return new CreateTransferResponse
                {
                    IsSuccesful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<TransferListResponse> GetTransfersByUser(int UserId)
        {
            try
            {
                TransferListResponse response = new TransferListResponse();
                await foreach (var transfer in _transferRepository.RetrieveCollectionAsync(new TransferFilter { Userid = UserId }))
                {
                    response.Transfers.Add(new TransferDTO
                    {
                        TransferId = transfer.TransferId,
                        GoingToId = transfer.GoingToId,
                        SenderId = transfer.SenderId,
                        TransferAmount = transfer.TransferAmount,
                        TransferReason = transfer.TransferReason,
                        TransferStatus = transfer.TransferStatus,
                        UserId = transfer.UserId,
                    });
                }
                response.Transfers = response.Transfers.OrderByDescending(t => t.TransferId).ToList();
                return response;
            }
            catch (Exception ex)
            {
                return new TransferListResponse
                {
                    IsSuccesful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<TransferListResponse> GetTransfersToUser(int UserId)
        {
            try
            {
                TransferListResponse response = new TransferListResponse();
                List<int> accounts = new List<int>();
                await foreach (var acc in _usersPerAccRepository.RetrieveCollectionAsync(new UsersPerAccFilter { UserId = UserId }))
                {
                    accounts.Add(acc.BankAccountId);
                }
                foreach (var acc in accounts)
                {
                    await foreach (var transfer in _transferRepository.RetrieveCollectionAsync(new TransferFilter { GoingToId = acc }))
                    {
                        response.Transfers.Add(new TransferDTO
                        {
                            TransferId = transfer.TransferId,
                            GoingToId = transfer.GoingToId,
                            SenderId = transfer.SenderId,
                            TransferAmount = transfer.TransferAmount,
                            TransferReason = transfer.TransferReason,
                            TransferStatus = transfer.TransferStatus,
                            UserId = transfer.UserId,
                        });
                    }                  
                }
                response.Transfers = response.Transfers.OrderByDescending(t => t.TransferId).ToList();
                return response;
            }
            catch (Exception ex)
            {
                return new TransferListResponse
                {
                    IsSuccesful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<TransferResponse> SendTransfer(AuthRequest Request)
        {
            try
            {
                TransferResponse response = new TransferResponse();

                var transferDetails = await _transferRepository.RetrieveAsync(Request.TransferId);
                var accountDetails = await _bankAccountRepository.RetrieveAsync(transferDetails.SenderId);
                var reciverDetails = await _bankAccountRepository.RetrieveAsync(transferDetails.GoingToId);
                if (transferDetails.UserId == Request.UserId
                    && (accountDetails.AccBalance - transferDetails.TransferAmount) > 0
                    && transferDetails.TransferStatus == "ИЗЧАКВА"
                    )
                {
                    TransferUpdate update = new TransferUpdate
                    {
                        TransferStatus = "ОБРАБОТЕН"
                    };
                    BankAccountUpdate balanceupdate = new BankAccountUpdate
                    {
                        AccBalance = accountDetails.AccBalance - transferDetails.TransferAmount
                    };


                    response.IsSuccesful = response.IsSuccesful && await _transferRepository.UpdateAsync(transferDetails.TransferId, update);
                    response.IsSuccesful = response.IsSuccesful && await _bankAccountRepository.UpdateAsync(transferDetails.SenderId, balanceupdate);
                    response.IsSuccesful = response.IsSuccesful && await _bankAccountRepository.UpdateAsync(transferDetails.GoingToId, new BankAccountUpdate
                    {
                        AccBalance = reciverDetails.AccBalance + transferDetails.TransferAmount
                    });
                }
                else
                {
                    response.IsSuccesful = false;
                    response.Message = "You are not authorized to send this transfer or dont have sufficient balance";
                }
                return response;
            }
            catch (Exception ex)
            {
                return new TransferResponse
                {
                    IsSuccesful = false,
                    Message = ex.Message
                };
            }
        }
    }
}
