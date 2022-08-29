using Microsoft.AspNetCore.Identity;

namespace Abhiroop.Domain.Entities
{
    public class Role : IdentityRole
    {
        public string RoleDescription { get; set; }

        public Role(string name, string roleDescription)
        {
            this.Name = name;
            this.RoleDescription = roleDescription;
        }
    }
}
