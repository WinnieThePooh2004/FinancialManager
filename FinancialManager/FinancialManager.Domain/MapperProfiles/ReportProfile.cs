using AutoMapper;
using FinancialManager.Shared.Models;
using FinancialManager.Shared.DTOs;

namespace FinancialManager.MapperProfiles
{
    public class ReportProfile : Profile
    {
        public ReportProfile() 
        {
            CreateMap<Report, ReportDTO>();
            CreateMap<FinancialOperation, FinancialOperationDTO>();
        }
    }
}
