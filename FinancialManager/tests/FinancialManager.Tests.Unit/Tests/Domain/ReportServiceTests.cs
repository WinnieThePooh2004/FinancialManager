using AutoMapper;
using FinancialManager.Domain.MapperProfiles;
using FinancialManager.Domain.Services;
using FinancialManager.Shared.DTOs;
using FinancialManager.Shared.Interfaces.Repositiories;
using Microsoft.Extensions.Logging;
using FinancialManager.Shared.Models;
using FinancialManager.Shared.Exceptions.DomainExceptions;

namespace FinancialManager.Tests.Unit.Tests.Domain
{
    public class ReportServiceTests
    {
        private readonly IReportRepository _repository;
        private readonly ReportService _service;
        private readonly ILogger<ReportService> _logger;
        private readonly IMapper _mapper;

        public ReportServiceTests()
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<ReportProfile>()).CreateMapper();
            _repository = Substitute.For<IReportRepository>();
            _logger = Substitute.For<ILogger<ReportService>>();
            _service = new(_repository, _mapper, _logger);
            _expectedReport = _mapper.Map<ReportDTO>(_report);
        }

        [Fact]
        public async Task DailyPeriod_ShouldReturnFromRepository()
        {
            _repository.DailyReport(DateTime.MinValue).Returns(_report);
            var actual = await _service.DailyReportAsync(DateTime.MinValue);
            actual.Should().BeEquivalentTo(_expectedReport, options => options.ComparingByMembers<ReportDTO>());
            actual.Operations.Should().BeEquivalentTo(_expectedReport.Operations, options => options.ComparingByMembers<FinancialOperationDTO>());
        }

        [Fact]
        public async Task PeriodReport_ShouldReturnFromRepository()
        {
            _repository.PeriodReport(DateTime.MinValue, DateTime.MaxValue).Returns(_report);
            var actual = await _service.PeriodReportAsync(DateTime.MinValue, DateTime.MaxValue);
            actual.Should().BeEquivalentTo(_expectedReport, options => options.ComparingByMembers<ReportDTO>());
            actual.Operations.Should().BeEquivalentTo(_expectedReport.Operations, options => options.ComparingByMembers<FinancialOperationDTO>());
        }

        [Fact]
        public async Task PerionReport_InvalidDateRangePasses_ThrowsException()
        {
            var action = async () => await _service.PeriodReportAsync(DateTime.MaxValue, DateTime.MinValue);
            await action.Should().ThrowAsync<DateRangeExpeption>().WithMessage($"date perion begin({DateTime.MaxValue}) must " +
                  $"be less or equal than date perion end({DateTime.MinValue})\nError code: 400");
        }

        Report _report = new Report()
        {
            TotalExprenses = 0,
            TotalIncome = 0,
            Operations = new()
            {
                new FinancialOperation{ Id = 1, Amount = 100, DateTime = DateTime.Now, Description = "abc", OperationTypeId = 11 },
                new FinancialOperation{ Id = 2, Amount = 100, DateTime = DateTime.Now, Description = "bbc", OperationTypeId = 11 },
                new FinancialOperation{ Id = 3, Amount = 100, DateTime = DateTime.Now, Description = "cbc", OperationTypeId = 11 },
                new FinancialOperation{ Id = 4, Amount = 100, DateTime = DateTime.Now, Description = "dbc", OperationTypeId = 11 },
            }
        };

        ReportDTO _expectedReport;
    }
}
