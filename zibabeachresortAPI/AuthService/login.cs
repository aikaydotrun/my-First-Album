using Microsoft.EntityFrameworkCore;
using zibabeachresortAPI.AuthService;
using zibabeachresortAPI.AuthService.zibabeachresortAPI.AuthService;
using zibabeachresortAPI.DATA;
using zibabeachresortAPI.Models.Entities;

namespace zibabeachresortAPI.Services
{
    public sealed class LoginService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly TokenProvider _tokenProvider;
        

        public LoginService(ApplicationDbContext dbContext, TokenProvider tokenProvider)
        {
            _dbContext = dbContext;
            _tokenProvider = tokenProvider;
            
        }

        public async Task<string> HandleAsync(string email)
        {
            var employee = await _dbContext.employees
                .FirstOrDefaultAsync(e => e.Email.ToLower().Trim() == email.ToLower().Trim());

            if (employee == null)
            {
                throw new UnauthorizedAccessException("Invalid email.");
            }

            // Here you'd normally also validate password.
            var token = _tokenProvider.GenerateToken(employee.Email);
            return token;
        }
    }
}
