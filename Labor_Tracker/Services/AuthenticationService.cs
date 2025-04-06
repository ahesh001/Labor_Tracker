using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labor_Tracker.Models;

//This service provides methods for registering new users and authenticating existing users.
namespace Labor_Tracker.Services
{
    public class AuthenticationService
    {
        private readonly DatabaseService _databaseService;
        private readonly PasswordService _passwordService;

        public AuthenticationService()
        {
            _databaseService = new DatabaseService();
            _passwordService = new PasswordService();
        }

        public async Task<(bool IsSuccess, string Message)> RegisterAsync(string username, string password)
        {
            //Check if the user already exists
            var existingUser = await _databaseService.GetUserByUsernameAsync(username);
            if (existingUser != null)
            {
                // User already exists
                return (false, "Username already exists.");
            }

            //Hash the password
            var (hash, salt) = _passwordService.HashPassword(password);

            //Create a new user
            var user = new User
            {
                Username = username,
                PasswordHash = hash,
                Salt = salt
            };

            //Add the user to the database
            await _databaseService.AddUserAsync(user);
            return (true, "Registration successful.");
        }

        public async Task<(bool IsSuccess, string Message, User user)> LoginAsync(string username, string password)
        {
            //Retrieve the user from the database
            var user = await _databaseService.GetUserByUsernameAsync(username);
            if (user == null)
            {
                // User does not exist
                return (false, "Invalid username or password.", null);
            }

            //Verify the Password
            bool isValid = _passwordService.VerifyPassword(password, user.PasswordHash, user.Salt);
            if (!isValid)
            {
                return (false, "Invalid username or password.", null);
            }

            return (true, "Login successful.", user);
        }
    }
}
