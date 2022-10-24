using FinancialManager.DTOs.FinancialOperations;
using FinancialManager.Models;

namespace FinancialManager.MapperProfiles.FinancialOperations
{
    public class FinancialOperationUpdateProfile : AutoMapper.Profile
    {
        public FinancialOperationUpdateProfile() 
        {
            CreateMap<FinancialOperationUpdateDto, FinancialOperation>()
                .ForMember(
                    dest => dest.Amount,
                    opt => opt.MapFrom(c => (int)(c.Amount * 100)));
        }
    }
}
