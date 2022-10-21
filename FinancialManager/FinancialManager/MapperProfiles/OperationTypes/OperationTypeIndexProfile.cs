using AutoMapper;
using FinancialManager.Models;
using FinancialManager.DTOs.OperationTypes;
namespace FinancialManager.MapperProfiles.OperationTypes
{
    public class OperationTypeIndexProfile : Profile
    {
        public OperationTypeIndexProfile() 
        {
            CreateMap<OperationType, OperationTypeIndexDto>()
                .ForMember(
                    dest => dest.IsIncome,
                    opt => opt.MapFrom(c => c.IsIncome.ToString())
                );
        }
    }
}
