using AutoMapper;
using FinancialManager.Models;
using FinancialManager.DTOs.OperationTypes;
namespace FinancialManager.MapperProfiles.OperationTypes
{
    public class OperationTypeDetilsProfile : Profile
    {
        public OperationTypeDetilsProfile()
        {
            CreateMap<OperationType, OperationTypeDetailsDto>()
                .ForMember(
                    dest => dest.IsIncome,
                    opt => opt.MapFrom(c => c.IsIncome.ToString())
                );
        }
    }
}
