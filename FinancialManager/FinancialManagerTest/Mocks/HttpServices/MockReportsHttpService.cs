using AutoMapper;
using FinancialManager.Controllers;
using FinancialManager.Services.ReportService;
using FinancialManagerTest.Mocks.Data;
using Frontend.HttpService;
using FinancialManager.MapperProfiles.ReportProfiles;
using Shared.DTOs.Reports;

namespace FinancialManagerTest.Mocks.HttpServices
{
    internal class MockReportsHttpService : IHttpService
    {
        public string FinancialOperationUri => "";

        public string OperationTypesUri => "";

        public string ReportUri => "";

        public Task DeleteObject(string uri, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetObjectAsync<T>(string uri) where T : class
        {
            var requestQuery = uri.Split('?')[1];
            return await GetReport(requestQuery) as T ?? throw new Exception();
        }

        public Task<T> GetObjectByIdAsync<T>(string uri, int id) where T : class
        {
            throw new NotImplementedException();
        }

        public Task PostObject<T>(string uri, T? @object) where T : class
        {
            throw new NotImplementedException();
        }

        public Task PutObject<T>(string uri, int id, T? @object) where T : class
        {
            throw new NotImplementedException();
        }

        private ReportsController CreateController()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new ReportDatailsProfile()));
            return new ReportsController(new ReportService(new MockFinancialManagerContext()), new Mapper(config));
        }

        private (DateTime, DateTime)GetTwoDatesFromRequestQuery(string requestQuery)
        {
            var dates = requestQuery.Split('&');
            return (GetSingleDateFromRequestQuery(dates[0]), GetSingleDateFromRequestQuery(dates[1]));
        }

        private DateTime GetSingleDateFromRequestQuery(string requestQuery) 
        {
            return DateTime.Parse(requestQuery.Split('=')[1]);
        }

        private async Task<ReportDetailsDto> GetReport(string requestQuery)
        {
            if(requestQuery.Contains('&'))
            {
                var parsedValues = GetTwoDatesFromRequestQuery(requestQuery);
                var begin = parsedValues.Item1;
                var end = parsedValues.Item2;
                return (await CreateController().GetReportByPeriod(begin, end)).Value ?? throw new Exception();
            }
            return (await CreateController().GetDailyReport(GetSingleDateFromRequestQuery(requestQuery))).Value ?? throw new Exception();
        }
    }
}
