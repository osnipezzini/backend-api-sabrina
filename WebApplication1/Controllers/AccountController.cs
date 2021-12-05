using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebApplication1.DataModels;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpPost("/login")]
        public IActionResult Login(LoginModel loginModel)
        {
            var user = Constants.GetUsers()
                .Where(x => x.Document == loginModel.Username || x.Username == loginModel.Username || x.Email == loginModel.Username)
                .Where(x => x.Password == loginModel.Password)
                .FirstOrDefault();

            if (user == null)
                return Unauthorized();

            var token = user.GenerateToken();
            return Ok(token);
        }

        [HttpPost("/register")]
        public IActionResult Register(RegisterModel loginModel)
        {
            if (loginModel.Password != loginModel.ConfirmPassword)
                return BadRequest();

            var user = Constants.GetUsers()
                .Where(x => x.Username == loginModel.Username)
                .Where(x => x.Email == loginModel.Email)
                .Where(x => x.Document == loginModel.Document)
                .FirstOrDefault();

            if (user != null)
                return Unauthorized();

            int id = 0;
            if (Constants.GetUsers().Any())
                id = Constants.GetUsers().Max(x => x.Id);

            user = new Models.User
            {
                Id = id + 1,
                Name = loginModel.Name,
                Document = loginModel.Document,
                Username = loginModel.Username,
                Password = loginModel.Password,
                Email = loginModel.Email
            };
            Constants.AddUser(user);
            return Ok(user);
        }

        [HttpPost("/post")]
        [Authorize]
        public IActionResult Register(PostModel postModel)
        {            
            return Ok(postModel);
        }
    }
}
