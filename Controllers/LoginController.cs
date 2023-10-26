using Instagram.Models;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginCredentialDBDataContext dataContext;

        public LoginController(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("ConStr");
             dataContext= new LoginCredentialDBDataContext(connectionString);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string txtUserName,string txtPassword)
        {
           var list= dataContext.CheckCrential();

            foreach (var item in list)
            {
                if (txtUserName == item.UserName && txtPassword == item.Password)
                {
                    ViewBag.msg = "Login Successfull";
                    return View("Sucess");
                }
                else
                {
                    ViewBag.msg = "Login Failed";
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(LoginCredentials data)
        {
           bool Isinserted= dataContext.CreateAccount(data);

            if (Isinserted)
            {
                ViewBag.msg = "Account Created Successfully";
            }
            else
            {
                ViewBag.msg = "Account not Created";
            }
            return View();

        }




    }
}
