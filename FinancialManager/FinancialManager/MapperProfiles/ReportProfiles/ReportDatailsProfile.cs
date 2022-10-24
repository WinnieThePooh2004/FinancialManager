using AutoMapper;
using FinancialManager.DTOs.Reports;
using FinancialManager.Models;
using Microsoft.CodeAnalysis.FlowAnalysis;
using FinancialManager.MapperProfiles.FinancialOperations;
using FinancialManager.DTOs.FinancialOperations;

namespace FinancialManager.MapperProfiles.ReportProfiles
{
    public class ReportDatailsProfile : Profile
    {
        public ReportDatailsProfile() 
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new FinancialOperationDetailsProfile()));
            CreateMap<Report, ReportDetailsDto>()
                .ForMember(
                    dest => dest.Operations,
                    opt => opt.MapFrom(c => new Mapper(config).Map<List<FinancialOperationDetailsDto>>(c.Operations))
                );
        }
    }
}
