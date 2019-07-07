using System;
using System.Collections.Generic;

namespace Find_A_Tutor.Core.Domain
{
    public class User : Entity
    {
        private readonly static List<string> _roles = new List<string>()
        {
            "student", "tutor", "admin"
        };
        public string Role { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected User() { }

        public User(Guid id, string role, string firstName, string lastName, string email, string password)
        {
            Id = id;
            SetRole(role);
            SetFirstName(firstName);
            SetLastName(lastName);
            SetEmail(email);
            SetPassword(password);
            CreatedAt = DateTime.UtcNow;
        }
        public void SetFirstName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception($"User cannot have an empty first name.");
            }
            FirstName = name;
        }

        public void SetLastName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception($"User cannot have an empty last name.");
            }
            LastName = name;
        }

        public void SetEmail(string email)
        {
            //todo: email validation "System.Net.Mail;"
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception($"User cannot have an empty email.");
            }

            Email = email;
        }

        public void SetRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new Exception($"User cannot have an empty role.");
            }
            role = role.ToLowerInvariant();
            if (!_roles.Contains(role))
            {
                throw new Exception($"User cannot have an role: '{role}'.");
            }
            Role = role;
        }

        public void SetPassword(string password)
        {
            //todo: regex
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception($"User cannot have an empty password.");
            }
            if (password.Length < 8 || password.Length > 254)
            {
                throw new Exception("Password should be between 8 - 254 alphanumeric character.");
            }
            Password = password;
        }
    }
}
