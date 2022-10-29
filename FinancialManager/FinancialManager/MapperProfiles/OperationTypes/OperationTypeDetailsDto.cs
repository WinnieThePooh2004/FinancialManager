using AutoMapper;
using FinancialManager.Models;
using Shared.DTOs.OperationTypes;
namespace FinancialManager.MapperProfiles.OperationTypes
{
    public class OperationTypeDetilsProfile : Profile
    {
        public OperationTypeDetilsProfile()
        {
            CreateMap<OperationType, OperationTypeDetailsDto>();
        }
    }
}
