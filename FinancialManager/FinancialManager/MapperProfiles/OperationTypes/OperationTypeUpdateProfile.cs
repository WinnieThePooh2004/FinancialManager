using FinancialManager.Models;
using Shared.DTOs.OperationTypes;
using AutoMapper;
namespace FinancialManager.MapperProfiles.OperationTypes
{
    public class OperationTypeUpdateProfile : Profile
    {
        public OperationTypeUpdateProfile()
        {
            CreateMap<OperationTypeUpdateDto, OperationType>();
        }
    }
}
