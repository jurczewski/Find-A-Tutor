using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Find_A_Tutor.Core.Exceptions;

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
                throw new ValidationException($"User cannot have an empty first name.");
            }
            FirstName = name;
        }

        public void SetLastName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ValidationException($"User cannot have an empty last name.");
            }
            LastName = name;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ValidationException($"User cannot have an empty email.");
            }

            var isValidMail = new Regex(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");
            if (!isValidMail.IsMatch(email))
            {
                throw new ValidationException($"Invalid email.");
            }

            Email = email;
        }

        public void SetRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new ValidationException($"User cannot have an empty role.");
            }
            role = role.ToLowerInvariant();
            if (!_roles.Contains(role))
            {
                throw new ValidationException($"User cannot have an role: '{role}'.");
            }
            Role = role;
        }

        public void SetPassword(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasMiniMaxChars = new Regex(@".{8,32}");
            var hasLowerChar = new Regex(@"[a-z]+");

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ValidationException($"Password should not be empty.");
            }

            if (!hasMiniMaxChars.IsMatch(password))
            {
                throw new ValidationException("Password should be between 8 - 32 alphanumeric character.");
            }

            if (!hasLowerChar.IsMatch(password))
            {
                throw new ValidationException("Password should contain At least one lower case letter");
            }

            if (!hasNumber.IsMatch(password))
            {
                throw new ValidationException("Password should contain At least one numeric value");
            }

            Password = password;
        }
    }
}
