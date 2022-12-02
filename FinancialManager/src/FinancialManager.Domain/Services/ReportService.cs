using FinancialManager.Shared.DTOs;
using AutoMapper;
using FinancialManager.Shared.Interfaces.Repositiories;
using FinancialManager.Shared.Interfaces.Services;
using FinancialManager.Shared.Exceptions.DomainExceptions;
using Microsoft.Extensions.Logging;
using FinancialManager.Shared.Extentions;

namespace FinancialManager.Domain.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ReportService(IReportRepository repository, IMapper mapper, ILogger<ReportService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ReportDTO> DailyReportAsync(DateTime date)
        {
            _logger.LogInformation("Getting report by pertion by {date}", date);
            return _mapper.Map<ReportDTO>(await _repository.DailyReport(date));
        }

        public async Task<ReportDTO> PeriodReportAsync(DateTime start, DateTime finish)
        {
            _logger.LogInformation("Getting report by pertion from {start} to {end}", start, finish);
            if(start > finish) 
            {
                _logger.LogAndThrow(new DateRangeExpeption(start, finish));
            }
            return _mapper.Map<ReportDTO>(await _repository.PeriodReport(start, finish));
        }
    }
}
