using System;

namespace Demo.Services
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }

    public class Argon2PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            // Your Argon2 hashing logic here
            byte[] hashedPassword = null; // Replace with actual hashing logic
            return Convert.ToBase64String(hashedPassword);
        }
    }
}
