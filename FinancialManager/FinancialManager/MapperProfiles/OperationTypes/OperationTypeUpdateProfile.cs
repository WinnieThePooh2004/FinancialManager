using FinancialManager.Models;
using FinancialManager.DTOs.OperationTypes;
using AutoMapper;
namespace FinancialManager.MapperProfiles.OperationTypes
{
    public class OperationTypeUpdateProfile : Profile
    {
        public OperationTypeUpdateProfile()
        {
            CreateMap<OperationTypeUpdateDto, OperationType>()
                .ForMember(
                    dest => dest.IsIncome,
                    opt => opt.MapFrom(c => bool.Parse(c.IsIncome))
                );
        }
    }
}
