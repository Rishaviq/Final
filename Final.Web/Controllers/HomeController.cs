using System.Diagnostics;
using System.Threading.Tasks;
using Final.Models;
using Final.Services.Interfaces.BankAccount;
using Final.Web.Models;
using Final.Web.Models.BankAccounts;
using Microsoft.AspNetCore.Mvc;
using Office.Web.Attributes;

namespace Final.Web.Controllers
{
    [RequireLogin]
    public class HomeController : Controller
    {
        private readonly IBankAccountService _bankAccountService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
