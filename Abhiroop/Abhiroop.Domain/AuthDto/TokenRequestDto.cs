using System.ComponentModel.DataAnnotations;

namespace Abhiroop.Domain.AuthDto
{
    public class TokenRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
