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
                    dest => dest.DateTime,
                    opt => opt.MapFrom(c => DateTime.Parse(c.DateTime))
                )
                .ForMember(
                    dest => dest.OperationTypeId,
                    opt => opt.MapFrom(c => int.Parse(c.OperationTypeId))
                )
                .ForMember(
                    dest => dest.Amount,
                    opt => opt.MapFrom(c => (int)(double.Parse(c.Amount) * 100))
                );
        }
    }
}
