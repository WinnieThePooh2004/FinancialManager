using AutoMapper;
using FinancialManager.Shared.Models;
using FinancialManager.Shared.DTOs;

namespace FinancialManager.Domain.MapperProfiles
{
    public class OperationTypeProfile : Profile
    {
        public OperationTypeProfile() 
        {
            CreateMap<OperationType, OperationTypeDTO>().ReverseMap();
        }
    }
}
