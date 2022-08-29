using Abhiroop.Domain.AuthDto;

namespace Abhiroop.Busines.Auth
{
    public interface IAuthService
    {
        Task<AuthDto> GetTokenAsync(TokenRequestDto model);
    }
}
