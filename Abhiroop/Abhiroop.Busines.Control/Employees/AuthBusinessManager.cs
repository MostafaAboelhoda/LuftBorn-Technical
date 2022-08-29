using Abhiroop.Busines.Auth;
using Abhiroop.Domain.AuthDto;

namespace Abhiroop.Busines.Control.Employees
{
    public class AuthBusinessManager
    {
        private readonly IAuthService _authService;

        public AuthBusinessManager(IAuthService authService)
        {
            this._authService = authService;
        }
        public async Task<AuthDto> LoginAysnc(TokenRequestDto tokenRequestDto)
        {
            return await _authService.GetTokenAsync(tokenRequestDto);
        }
    }
}
