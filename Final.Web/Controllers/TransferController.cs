using System.Threading.Tasks;
using Azure;
using Final.Services.DTOs.Transfer.Requests;
using Final.Services.DTOs.Transfer.Responses;
using Final.Services.Interfaces.BankAccount;
using Final.Services.Interfaces.Transfer;
using Final.Web.Models;
using Final.Web.Models.BankAccounts;
using Final.Web.Models.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Office.Web.Attributes;

namespace Final.Web.Controllers
{
    [RequireLogin]
    public class TransferController : Controller
    {
        private readonly IBankAccountService _bankAccountService;
        private readonly ITransferService _transferService;
        public TransferController(IBankAccountService bankAccountService, ITransferService transferService)
        {
            _transferService = transferService;
            _bankAccountService = bankAccountService;
        }
        // GET: TransferController
        public async Task<ActionResult> Index()
        {
            TransferListModel model = new TransferListModel();
            var response = await _transferService.GetTransfersByUser(Convert.ToInt32(HttpContext.Session.GetInt32("UserId")));
            if (response.IsSuccesful)
            {
                foreach (var transfer in response.Transfers)
                {
                    model.Transfers.Add(new TransferModel
                    {
                        TransferId = transfer.TransferId,
                        GoingToNumber = _bankAccountService.GetAccountById(transfer.GoingToId).Result.Account.AccNumber,
                        SenderNumber = _bankAccountService.GetAccountById(transfer.SenderId).Result.Account.AccNumber,
                        TransferAmount = transfer.TransferAmount,
                        TransferReason = transfer.TransferReason,
                        TransferStatus = transfer.TransferStatus,
                        UserId = transfer.UserId
                    });
                }

                return View(model);
            }


            return View("RequestAnswer", new RequestAnswerModel { isSuccess = response.IsSuccesful, message = response.Message });
        }



        // GET: TransferController/Create
        public async Task<IActionResult> Create()
        {
            AccountsListModel model = new AccountsListModel();
            var response = await _bankAccountService.GetAccountsOfUser(Convert.ToInt32(HttpContext.Session.GetInt32("UserId")));
            if (response.IsSuccesful)
            {
                foreach (var acc in response.Accounts)
                {
                    model.Accounts.Add(new AccountModel
                    {
                        AccId = acc.AccId,
                        AccNumber = acc.AccNumber,
                        AccBalance = acc.AccBalance
                    });
                }

                return View(model);
            }


            return View("RequestAnswer", new RequestAnswerModel { isSuccess = response.IsSuccesful, message = response.Message });
        }

        // POST: TransferController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateTransferModel request)
        {
            TransferRequest transferRequest = new TransferRequest
            {
                GoingToAccNumber = request.GoingToAccNumber,
                SenderId = request.SenderId,
                TransferAmount = request.TransferAmount,
                TransferReason = request.TransferReason,
                UserId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"))

            };

            var response = await _transferService.CreateTransfer(transferRequest);
            return View("RequestAnswer", new RequestAnswerModel { isSuccess = response.IsSuccesful, message = response.Message });
        }

        public async Task<ActionResult> Send(int id)
        {
            AuthRequest request = new AuthRequest
            {
                UserId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId")),
                TransferId = id
            };
            var response = await _transferService.SendTransfer(request);
            return View("RequestAnswer", new RequestAnswerModel { isSuccess = response.IsSuccesful, message = response.Message });
        }

        public async Task<ActionResult> Cancel(int id)
        {
            AuthRequest request = new AuthRequest
            {
                UserId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId")),
                TransferId = id
            };
            var response = await _transferService.CancelTransfer(request);
            return View("RequestAnswer", new RequestAnswerModel { isSuccess = response.IsSuccesful, message = response.Message });
        }

        public async Task<ActionResult> Reorder()
        {

            TransferListModel model = new TransferListModel();
            var response = await _transferService.GetTransfersByUser(Convert.ToInt32(HttpContext.Session.GetInt32("UserId")));
            if (response.IsSuccesful)
            {
                foreach (var transfer in response.Transfers)
                {
                    model.Transfers.Add(new TransferModel
                    {
                        TransferId = transfer.TransferId,
                        GoingToNumber = _bankAccountService.GetAccountById(transfer.GoingToId).Result.Account.AccNumber,
                        SenderNumber = _bankAccountService.GetAccountById(transfer.SenderId).Result.Account.AccNumber,
                        TransferAmount = transfer.TransferAmount,
                        TransferReason = transfer.TransferReason,
                        TransferStatus = transfer.TransferStatus,
                        UserId = transfer.UserId
                    });
                }
                model.Transfers = model.Transfers.OrderByDescending(t => t.TransferStatus == "ИЗЧАКВА").ToList();

                return View("Index",model);
            }


            return View("RequestAnswer", new RequestAnswerModel { isSuccess = response.IsSuccesful, message = response.Message });
        }


        public async Task<ActionResult> Incoming() {
            TransferListModel model = new TransferListModel();
            var response = await _transferService.GetTransfersToUser(Convert.ToInt32(HttpContext.Session.GetInt32("UserId")));
            if (response.IsSuccesful)
            {
                foreach (var transfer in response.Transfers)
                {
                    model.Transfers.Add(new TransferModel
                    {
                        TransferId = transfer.TransferId,
                        GoingToNumber = _bankAccountService.GetAccountById(transfer.GoingToId).Result.Account.AccNumber,
                        SenderNumber = _bankAccountService.GetAccountById(transfer.SenderId).Result.Account.AccNumber,
                        TransferAmount = transfer.TransferAmount,
                        TransferReason = transfer.TransferReason,
                        TransferStatus = transfer.TransferStatus,
                        UserId = transfer.UserId
                    });
                }

                return View(model);
            }


            return View("RequestAnswer", new RequestAnswerModel { isSuccess = response.IsSuccesful, message = response.Message });

            
        }
    }
}
