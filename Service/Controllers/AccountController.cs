using Microsoft.AspNetCore.Mvc;
using Sepidar.BaseApi;
using Sepidar.BaseApi.Filters;
using Sepidar.Business.Interfaces;
using Sepidar.Service.Models;

namespace Sepidar.Service.Controllers
{
    [IsPublic]
    public class AccountController : GeneralController
    {
        private IUserBusiness userBusiness;

        public AccountController(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }

        [HttpPost]
        public dynamic SignIn([FromBody] SignInModel signInModel)
        {
            var userData = userBusiness.SignIn(signInModel.Email, signInModel.Password);
            long userId = userData.UserId;
            Sepidar.BaseApi.Security.SetAuthenticationCookie(userId, userData);
            return userData;
        }

        [HttpPost]
        public dynamic Register([FromBody] RegisterModel registerModel)
        {
            var userData = userBusiness.Register(registerModel.Email, registerModel.Password, registerModel.RePassword);
            long userId = userData.UserId;
            Sepidar.BaseApi.Security.SetAuthenticationCookie(userId, userData);
            return userData;
        }

        [HttpPost]
        public dynamic Login([FromBody] LoginModel loginModel)
        {
            var userData = userBusiness.Login(loginModel.Username, loginModel.Password);
            long userId = userData.UserId;
            Sepidar.BaseApi.Security.SetAuthenticationCookie(userId, userData);
            return userData;
        }

        [HttpPost]
        public IActionResult Logout()
        {
            ServiceSecurity.DeleteAuthenticationCookie();
            return Ok("logged out");
        }

        [HttpGet]
        public dynamic Test()
        {
            return "ali fallah";
        }
    }
}
