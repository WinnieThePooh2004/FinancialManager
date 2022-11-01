using AutoMapper;
using FinancialManager.Models;
using Shared.DTOs.Users;
namespace FinancialManager.MapperProfiles.Users
{
    public class UserSignUpProfile : Profile
    {
        public UserSignUpProfile() 
        {
            CreateMap<UserSignUpDto, User>();
        }
    }
}
