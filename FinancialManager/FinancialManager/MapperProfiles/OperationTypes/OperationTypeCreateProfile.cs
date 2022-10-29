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
            CreateMap<OperationTypeCreateDto, OperationType>();
        }
    }
}
