using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Demo.Models;
using Microsoft.Extensions.Configuration;
using Konscious.Security.Cryptography;
using Demo.Services;



namespace Demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher _passwordHasher;

        public UserController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        public byte[] HashPassword(string password)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var salt = new byte[16]; // Generate a 16-byte salt
            new RNGCryptoServiceProvider().GetBytes(salt);

            var argon2 = new Argon2id(passwordBytes)
            {
                Salt = salt,
                DegreeOfParallelism = 8, // number of threads
                MemorySize = 65536, // 64 MB
                Iterations = 4
            };

            return argon2.GetBytes(16);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(User newUser)
        {
            if (newUser == null || string.IsNullOrEmpty(newUser.Username) || string.IsNullOrEmpty(newUser.PasswordHash))
            {
                return BadRequest("Invalid user data");
            }

            var hashedPassword = _passwordHasher.HashPassword(newUser.PasswordHash);  // Modify this line
            newUser.PasswordHash = Convert.ToBase64String(hashedPassword)
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User registered successfully!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Username and password are required");
            }

            var hashedPassword = Convert.ToBase64String(HashPassword(password));
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == hashedPassword);

            if (user == null)
            {
                return Unauthorized();
            }


                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { Token = tokenString });
            }
        }
    }
}
