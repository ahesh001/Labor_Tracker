using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.Maui.Storage;

namespace Labor_Tracker.Services
{
    public class PasswordService
    {
        //Generates a new salt and computes the hash of the password.
        public (string Hash, string Salt) HashPassword(string password)
        {
            //Generate a random salt
            byte[] saltBytes = new byte[16];
            using (var provider = RandomNumberGenerator.Create())
            {
                provider.GetBytes(saltBytes);
            }
            string salt = Convert.ToBase64String(saltBytes);

            //Hash the password with salt
            string hash = ComputeHash(password, saltBytes);

            return (hash, salt);
        }

        //Checks if the entered password matches the stored hash.
        public bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            byte[] saltBytes = Convert.FromBase64String(storedSalt);
            string hash = ComputeHash(enteredPassword, saltBytes);

            return hash == storedHash;
        }

        //Uses PBKDF2 with SHA-256 to hash the password.
        private string ComputeHash(string password, byte[] saltBytes)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 100000, HashAlgorithmName.SHA256))
            {
                byte[] hashBytes = pbkdf2.GetBytes(32);
                return Convert.ToBase64String(hashBytes);
            }
            
        }
    }
}
