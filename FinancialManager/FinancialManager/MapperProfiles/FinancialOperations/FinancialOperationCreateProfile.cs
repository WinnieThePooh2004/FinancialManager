using AutoMapper;
using FinancialManager.DTOs.FinancialOperations;
using FinancialManager.Models;

namespace FinancialManager.MapperProfiles.FinancialOperations
{
    public class FinancialOperationCreateProfile : Profile
    {
        public FinancialOperationCreateProfile()
        {
            CreateMap<FinancialOperationCreateDto, FinancialOperation>()
                .ForMember(
                    dest => dest.Amount,
                    opt => opt.MapFrom(c => (int)(c.Amount * 100))
                );

        }
    }
}
