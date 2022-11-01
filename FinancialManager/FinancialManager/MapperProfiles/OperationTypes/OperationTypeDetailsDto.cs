using AutoMapper;
using FinancialManager.Models;
using Shared.DTOs.OperationTypes;
namespace FinancialManager.MapperProfiles.OperationTypes
{
    public class OperationTypeDetailsProfile : Profile
    {
        public OperationTypeDetailsProfile()
        {
            CreateMap<OperationType, OperationTypeDetailsDto>();
        }
    }
}
