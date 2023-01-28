using Microsoft.AspNetCore.Mvc;
using TecAllianceWebPortal.Communication.Requests;
using TecAllianceWebPortal.Services.Interfaces;

namespace TecAllianceWebPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            GenerateTestUsers().Wait();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = await _userService.GetUserByEmail(request.Email);

                if (user != null)
                {
                    Response.Cookies.Append("email", user.Email, new CookieOptions()
                    {
                        Expires = DateTimeOffset.Now.AddDays(1),
                        HttpOnly = true,
                        Secure = true

                    });
                    return Ok();
                }
                throw new Exception($"Email {request.Email} not found");
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                Response.Cookies.Delete("email");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // Just for demo :)
        private async Task<IActionResult> GenerateTestUsers()
        {
            await _userService.GenerateTestUsers();
            return Ok();
        }
    }
}
