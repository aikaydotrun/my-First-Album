using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
/*internal sealed class TokenProvider
{
    private readonly IConfiguration _configuration;

    public TokenProvider()
    {
    }

    public TokenProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Create(Employee employee)
    {
        string secretKey = _configuration["jwt:SecretKey"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)); // Fix: Correctly instantiate the SecurityKey object  
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                   new Claim(ClaimTypes.Name, employee.Name),
                   new Claim(ClaimTypes.Email, employee.Email),
                   new Claim("EmployeeId", employee.EmployeeId.ToString())
               }),
            Expires = DateTime.UtcNow.AddMinutes(ConfigurationBinder.GetValue<int>(_configuration, "jwt:ExpirationInMinutes")), // Fix: Correctly bind configuration value  
            SigningCredentials = signingCredentials,
            Issuer = _configuration["jwt:Issuer"],
            Audience = _configuration["jwt:Audience"]
        };
        var handler = new JsonWebTokenHandler();
        string token = handler.CreateToken(tokenDescriptor);
        return token;
    }
}


public class TokenProvider
{
    private const string SecretKey = "your_super_key_12345";

    public string Create(Employee user)
    {
        try
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
             new  Claim(ClaimTypes.Name, user.Email ?? user.Phone ?? "unknown"),
            };
            var token = new JwtSecurityToken(
                issuer: "zibabeachresort",
                audience: "zibabeachresort",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        catch (Exception ex)
        {
            return $"Token generation failed:{ex.Message}";
        }

    }

}
*/

using global::zibabeachresortAPI.DATA;

namespace zibabeachresortAPI.AuthService
{
    namespace zibabeachresortAPI.AuthService
    {
        public class TokenProvider
        {
            private readonly IConfiguration _configuration;
            private readonly ApplicationDbContext _dbContext;

            public TokenProvider(IConfiguration configuration, ApplicationDbContext dbContext)
            {
                _configuration = configuration;
                _dbContext = dbContext;
            }

            public string GenerateToken(string email)
            {
                var user = _dbContext.employees.FirstOrDefault(e => e.Email.ToLower() == email.ToLower());
                if (user == null)
                {
                    throw new Exception("Invalid email.");
                }
                var jwtSecret = _configuration["Jwt:SecretKey"];
                var issuer = _configuration["Jwt:Issuer"];
                var audience = _configuration["Jwt:Audience"];

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Role, user.Role ?? "User"),

            };

                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
    }

}
