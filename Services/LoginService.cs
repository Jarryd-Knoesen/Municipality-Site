using Microsoft.EntityFrameworkCore;
using PROG7312_P1_V1.Database;
using PROG7312_P1_V1.DataModels;

namespace PROG7312_P1_V1.Services
{
    public class LoginService
    {
        private readonly AppDbContext _context;

        public LoginService(AppDbContext context)
        {
            _context = context;
        }

        // checks if the email exists in the database
        public async Task<bool> ProfileExists (string email)
        {
            return await _context.Profiles.AnyAsync(p => p.Email == email);
        }

        // checks if email and password match a profile in the database
        public async Task<Profile?> AuthenticateUser(string email, string password)
        {
            return await _context.Profiles
                .FirstOrDefaultAsync(p => p.Email == email && p.Password == password);
        }

        // Add a new user to the database
        //public async Task RegisterUser(Profile profile)
        //{
        //    profile.ProfileID = Guid.NewGuid().ToString(); // Generate a new unique ID
        //    _context.Profiles.Add(profile);
        //    await _context.SaveChangesAsync();
        //}

        public async Task<bool> RegisterUser(Profile profile)
        {
            // Check if email already exists
            if (await ProfileExists (profile.Email))
                return false;

            profile.ProfileID = Guid.NewGuid().ToString();
            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
