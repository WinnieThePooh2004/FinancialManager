using FinancialManager.Shared.DTOs;
using AutoMapper;
using FinancialManager.Shared.Interfaces.Repositiories;
using FinancialManager.Shared.Interfaces.Services;

namespace FinancialManager.Domain.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _repository;
        private readonly IMapper _mapper;

        public ReportService(IReportRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ReportDTO> DailyReportAsync(DateTime date)
        {
            return _mapper.Map<ReportDTO>(await _repository.DailyReport(date));
        }

        public async Task<ReportDTO> PeriodReportAsync(DateTime start, DateTime finish)
        {
            return _mapper.Map<ReportDTO>(await _repository.PeriodReport(start, finish));
        }
    }
}
