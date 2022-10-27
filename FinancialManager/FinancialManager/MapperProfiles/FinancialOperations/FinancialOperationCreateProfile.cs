using AutoMapper;
using Shared.DTOs.FinancialOperations;
using FinancialManager.Models;

namespace FinancialManager.MapperProfiles.FinancialOperations
{
    public class FinancialOperationCreateProfile : Profile
    {
        public FinancialOperationCreateProfile()
        {
            CreateMap<FinancialOperationCreateDto, FinancialOperation>()
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
