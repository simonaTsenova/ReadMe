using Microsoft.AspNet.Identity.EntityFramework;

namespace ReadMe.Models
{
    public class User : IdentityUser
    {
        public User()
        {

        }

        public User(string username, string email)
            : this()
        {
            this.UserName = username;
            this.Email = email;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
