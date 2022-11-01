using AutoMapper;
using Shared.DTOs.Reports;
using FinancialManager.Models;
using Microsoft.CodeAnalysis.FlowAnalysis;
using FinancialManager.MapperProfiles.FinancialOperations;
using Shared.DTOs.FinancialOperations;

namespace FinancialManager.MapperProfiles.ReportProfiles
{
    public class ReportDatailsProfile : Profile
    {
        public ReportDatailsProfile() 
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new FinancialOperationIndexProfile()));
            CreateMap<Report, ReportDetailsDto>()
                .ForMember(
                    dest => dest.Operations,
                    opt => opt.MapFrom(c => new Mapper(config).Map<List<FinancialOperationIndexDto>>(c.Operations))
                )
                .ForMember(
                    dest => dest.TotalExprenses,
                    opt => opt.MapFrom(c => (((double)c.TotalExprenses) / 100).ToString("0.00") + " UAH")
                )
                .ForMember(
                    dest => dest.TotalIncome,
                    opt => opt.MapFrom(c => (((double)c.TotalIncome) / 100).ToString("0.00") + " UAH")
                );
        }
    }
}
