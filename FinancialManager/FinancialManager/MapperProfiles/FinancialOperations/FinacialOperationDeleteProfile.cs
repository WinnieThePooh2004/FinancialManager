using AutoMapper;
using FinancialManager.Models;
using FinancialManager.DTOs.FinancialOperations;

namespace FinancialManager.MapperProfiles.FinancialOperations
{
    public class FinacialOperationDeleteProfile : Profile
    {
        public FinacialOperationDeleteProfile() 
        {
            CreateMap<FinancialOperation, FinancialOperationDeleteDto>()
                .ForMember(
                    dest => dest.Amount,
                    opt => opt.MapFrom(c => ((double)c.Amount / 100).ToString("0.00") + " UAH")
                )
                .ForMember(
                    dest => dest.DateTime,
                    opt => opt.MapFrom(c => c.DateTime.ToString("G"))
                );
        }
    }
}
