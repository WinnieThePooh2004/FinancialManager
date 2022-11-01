using FinancialManager.Data;
using FinancialManager.Models;
using Shared.ValueValidators.UserValuesValidator;
using System.ComponentModel.DataAnnotations;

namespace FinancialManager.Services.LoginServices
{
    public class LoginService : ILoginService
    {
        IFinancialManagerContext _context;
        public LoginService(IFinancialManagerContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            if(!UserDetailsValidator.PasswordIsValid(user.Password)) 
            {
                throw new Exception("Passwors is invalid");
            }
            if(!UserDetailsValidator.EmailValid(user.Email))
            {
                throw new Exception("Email is not valid");
            }
            if (_context.Users.Any(entity => entity.Name == user.Name))
            {
                throw new Exception("this name already exists");
            }
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = _context.Users.FirstOrDefault(x => x.Id == id);
            if (entity is null)
            {
                throw new Exception("Not found");
            }
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
