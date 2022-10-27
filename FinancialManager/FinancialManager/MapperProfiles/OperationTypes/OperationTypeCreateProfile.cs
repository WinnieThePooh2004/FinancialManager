using Microsoft.AspNetCore.Mvc.RazorPages;
using AutoMapper;
using Shared.DTOs.OperationTypes;
using FinancialManager.Models;

namespace FinancialManager.MapperProfiles.OperationTypes
{
    public class OperationTypeCreateProfile : Profile
    {
        public OperationTypeCreateProfile()
        {
            CreateMap<OperationTypeCreateDto, OperationType>()
                    .ForMember(
                        dest => dest.IsIncome,
                        opt => opt.MapFrom(c => bool.Parse(c.IsIncome))
                    );
        }
    }
}
