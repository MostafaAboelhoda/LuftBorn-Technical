
using Abhiroop.Busines.Auth;
using Abhiroop.Busines.Control.Employees;
using Abhiroop.Domain.AuthDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CybralWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly AuthBusinessManager _authBusinessManager;

        public AuthController(IAuthService authService, AuthBusinessManager authBusinessManager)
        {
            _authService = authService;
            this._authBusinessManager = authBusinessManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthDto>> Login([FromBody] TokenRequestDto loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authBusinessManager.LoginAysnc(loginRequest);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

    }
}
