using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final.Repositories.Implementations.Transfer;
using Final.Services.DTOs.Transfer.Requests;
using Final.Services.DTOs.Transfer.Responses;

namespace Final.Services.Interfaces.Transfer
{
    public interface ITransferService
    {
        public Task<CreateTransferResponse> CreateTransfer(TransferRequest transferRequest);
        public Task<TransferListResponse> GetTransfersByUser(int UserId);
        public Task<TransferResponse> SendTransfer(AuthRequest authorizedRequest);
        public Task<TransferResponse> CancelTransfer(AuthRequest authorizedRequest);
        public Task<TransferListResponse> GetTransfersToUser(int UserId);
    }
}
