using AutoMapper;
using FinancialManager.Shared.Models;
using FinancialManager.Shared.DTOs;

namespace FinancialManager.Domain.MapperProfiles
{
    public class FinactialOperationProfile : Profile
    {
        public FinactialOperationProfile()
        {
            CreateMap<FinancialOperation, FinancialOperationDTO>().ReverseMap();             
        }
    }
}
