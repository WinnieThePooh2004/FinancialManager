using FinancialManager.Models;

namespace FinancialManager.Services.LoginServices
{
    public interface ILoginService
    {
        Task SignUp(User user);
        Task SignIn(User user);
    }
}
