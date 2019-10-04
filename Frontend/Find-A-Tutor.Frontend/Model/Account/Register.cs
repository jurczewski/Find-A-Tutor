using System;

namespace Find_A_Tutor.Frontend.Model.Account
{
    public class Register
    {
        public Guid UserId { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
