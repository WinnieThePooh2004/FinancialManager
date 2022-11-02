using AutoMapper;
using FinancialManager.Controllers;
using FinancialManager.Services.ReportService;
using FinancialManagerTest.Mocks.Data;
using Frontend.HttpService;
using FinancialManager.MapperProfiles.ReportProfiles;
using Shared.DTOs.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task<T> GetObjectAsync<T>(string uri) where T : class
        {
            throw new NotImplementedException();
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
    }
}
