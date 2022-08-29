using Microsoft.AspNetCore.Identity;

namespace Abhiroop.Domain.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
    }
}
