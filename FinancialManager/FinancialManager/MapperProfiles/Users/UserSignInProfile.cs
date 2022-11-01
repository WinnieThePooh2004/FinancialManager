using AutoMapper;
using FinancialManager.Models;
using Shared.DTOs.Users;

namespace FinancialManager.MapperProfiles.Users
{
    public class UserSignInProfile : Profile
    {
        public UserSignInProfile() 
        {
            CreateMap<UserSignInDto, User>();
        }
    }
}
