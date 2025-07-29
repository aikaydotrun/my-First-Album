using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BCrypt.Net;
using TheHealthCare.DATA;
using TheHealthCare.Models.DTOs;
using TheHealthCare.Models.Enitities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TheHealthCare.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
        Task<string> GetProfileAsync(string userId);
    }
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            if (await _dbContext.Users.AnyAsync(u => u.Email == dto.Email))
            {
                throw new Exception("User with this email already exists");
            }
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHasher = hashedPassword,
                Role = dto.Role,
            };
            _dbContext.Add(user);
            await _dbContext.SaveChangesAsync();
            return "Registration successful";
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null ||!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHasher))
            {
                throw new Exception("Invalid email or password");
            }

            return GenerateJwtToken(user);
        }

        public async Task<UserDto> GetUserAsync(string userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null) throw new Exception("user not found");

            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role
            };
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Task<string> GetProfileAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
