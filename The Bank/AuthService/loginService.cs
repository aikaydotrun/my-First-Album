using Microsoft.EntityFrameworkCore;
using TheBank.Data;

namespace TheBank.AuthService
{
    public class loginService
    {
        private readonly CustomerDbContext customerDb;
        private readonly StaffDbContext staffDb;
        private readonly TokenProvider _tokenProvider;

        public loginService(CustomerDbContext customerDb, StaffDbContext staffDb, TokenProvider tokenProvider)
        {
            this.customerDb = customerDb;
            this.staffDb = staffDb;
            _tokenProvider = tokenProvider;
        }

        public async Task<string> HandleAsync(string email)
        {
            var staff = await staffDb.Staff
                .FirstOrDefaultAsync(s => s.Email == email);
            if (staff == null)
            {
                throw new UnauthorizedAccessException("invalid email");
            }

            var token = _tokenProvider.GenerateToken(staff.Email);
            if (token == null)
            {
                throw new InvalidOperationException("Token generation failed.");
            }

            return token;
        }
    }
}
