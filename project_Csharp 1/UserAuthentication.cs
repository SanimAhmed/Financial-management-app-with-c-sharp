using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity;


namespace project_Csharp_1
{
    public class UserAccount
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        private string PasswordHash { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }

        // Constructor
        public UserAccount() { }

        // Hash Password
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        // Register User
        public void Register(string username, string email, string password)
        {
            using (var context = new FinancialManagerContext())
            {
                if (context.UserAccounts.Any(u => u.Username == username))
                {
                    throw new Exception("User already exists.");
                }

                var user = new UserAccount
                {
                    Username = username,
                    PasswordHash = HashPassword(password),
                    Email = email,
                    RegistrationDate = DateTime.Now
                };

                context.UserAccounts.Add(user);
                context.SaveChanges();
            }
        }

        // Login User
        public bool Login(string username, string password)
        {
            using (var context = new FinancialManagerContext())
            {
                var user = context.UserAccounts.FirstOrDefault(u => u.Username == username);

                if (user == null)
                {
                    throw new UserNotFoundException("User not found.");
                }

                return user.PasswordHash == HashPassword(password);
            }
        }
    }
}
