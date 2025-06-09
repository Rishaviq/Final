using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Final.Services.DTOs.User;
using Final.Services.Interfaces.User;
using Final.Services.DTOs.User.Requests;


namespace Final.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

       

        // POST: LoginController/Create
        [HttpPost]
        
        public async Task<ActionResult> Login(string username,string password)
        {
            LoginRequest request = new LoginRequest { Password = password, Username = username };
            var response = await _userService.Login(request);
            if (response.IsSuccesful ) {
            HttpContext.Session.SetInt32("UserId", response.UserId);
            HttpContext.Session.SetString("Name", username);
            }

           return RedirectToAction("Index","Home");
        }

        [HttpPost]

        public async Task<ActionResult> LogOut()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }


    }
}
