using AutoMapper;
using FinancialManager.DTOs.FinancialOperations;
using FinancialManager.Models;

namespace FinancialManager.MapperProfiles.FinancialOperations
{
    public class FinancialOperationCreateProfile : Profile
    {
        public FinancialOperationCreateProfile()
        {
            CreateMap<FinancialOperationCreateDto, FinacialOperation>()
                .ForMember(
                    dest => dest.Amount,
                    opt => opt.MapFrom(c => (int)(double.Parse(c.Amount) * 100))
                );
        }
    }
}
