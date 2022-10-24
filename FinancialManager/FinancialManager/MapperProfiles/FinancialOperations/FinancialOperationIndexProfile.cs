using FinancialManager.Models;
using FinancialManager.DTOs.FinancialOperations;
using AutoMapper;
namespace FinancialManager.MapperProfiles.FinancialOperations
{
    public class FinancialOperationIndexProfile : Profile
    {
        public FinancialOperationIndexProfile()
        {
            CreateMap<FinancialOperation, FinancialOperationIndexDto>()
                .ForMember(
                    dest => dest.Amount,
                    opt => opt.MapFrom(c => ((double)c.Amount / 100).ToString("0.00") + " UAH"))
                .ForMember(
                    dest => dest.DateTime,
                    opt => opt.MapFrom(c => c.DateTime.ToString("G"))
                );
        }
    }
}
