using System;

namespace Find_A_Tutor.Core.DTO
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
