using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TheBank.Data;
using TheBank.Models.Entities;

namespace TheBank.AuthService
{
    public class TokenProvider
    {
        private readonly IConfiguration _configuration;
        private readonly StaffDbContext _stafftaffDb;
        public TokenProvider(IConfiguration configuration, StaffDbContext staffDb)
        {
            _configuration = configuration;
            _stafftaffDb = staffDb;
        }

        public string GenerateToken(string email)
        {
            var user = _stafftaffDb.Staff.FirstOrDefault(s => s.Email == email);
            if (user == null)
            {
                throw new Exception("Invalid Email");
            }
            var jwtSecret = _configuration["JwtSettings:SecretKey"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                   new Claim(ClaimTypes.Name, email),
                   new Claim(ClaimTypes.Role, user.Role) // Fixed: Added 'user.Role' to resolve the error  
               };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

        
}
