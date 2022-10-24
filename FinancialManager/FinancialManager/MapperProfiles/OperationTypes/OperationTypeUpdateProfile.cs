using FinancialManager.Models;
using FinancialManager.DTOs.OperationTypes;
using AutoMapper;
namespace FinancialManager.MapperProfiles.OperationTypes
{
    public class OperationTypeUpdateProfile : Profile
    {
        public OperationTypeUpdateProfile() 
        {
            CreateMap<OperationTypeUpdateProfile, OperationType>();
        }
    }
}
