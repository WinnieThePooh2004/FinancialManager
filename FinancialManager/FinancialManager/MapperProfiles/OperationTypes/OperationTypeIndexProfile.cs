using AutoMapper;
using FinancialManager.Models;
using FinancialManager.DTOs.OperationTypes;
namespace FinancialManager.MapperProfiles.OperationTypes
{
    public class OperationTypeIndexProfile : Profile
    {
        public OperationTypeIndexProfile() 
        {
            CreateMap<OperationType, OperationTypeIndexDto>();
        }
    }
}
