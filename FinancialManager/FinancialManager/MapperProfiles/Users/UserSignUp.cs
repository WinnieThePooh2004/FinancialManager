using AutoMapper;
using FinancialManager.Models;
using Shared.DTOs.Users;
namespace FinancialManager.MapperProfiles.Users
{
    public class UserSignUp : Profile
    {
        public UserSignUp() 
        {
            CreateMap<UserSignUpDto, User>();
        }
    }
}
