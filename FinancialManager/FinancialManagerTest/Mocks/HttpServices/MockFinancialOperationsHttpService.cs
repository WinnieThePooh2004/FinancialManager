using Frontend.HttpService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManager.Controllers;
using AutoMapper;
using FinancialManager.Services.CRUDServices;
using FinancialManagerTest.Mocks.Data;
using FinancialManager.MapperProfiles.FinancialOperations;
using Moq;

namespace FinancialManagerTest.Mocks.HttpServices
{
    internal class MockFinancialOperationsHttpService : IHttpService
    {
        public string FinancialOperationUri => "";

        public string OperationTypesUri => "";

        public string ReportUri => "";

        public MockFinancialManagerContext Context { get; }

        public MockFinancialOperationsHttpService() 
        {
            Context = new();
        }

        public MockFinancialOperationsHttpService(MockFinancialManagerContext context)
        {
            Context = context;
        }

        public async Task DeleteObject(string uri, int id)
        {
            var controller = CreateController(new FinancialOperationDetailsProfile());
            await controller.DeleteFinacialOperation(id);
        }

        public async Task<T> GetObjectAsync<T>(string uri) where T : class
        {
            try
            {
                var result = (await CreateController(new FinancialOperationIndexProfile()).GetFinacialOperation()).Value;
                if (result is null)
                {
                    throw new Exception();
                }
                return (T)result;
            }
            catch (InvalidCastException)
            {
                return default!;
            }
        }

        public async Task<T> GetObjectByIdAsync<T>(string uri, int id) where T : class
        {
            var result = (await CreateController(new FinancialOperationDetailsProfile()).GetFinacialOperation(id)).Value;
            if (result is null)
            {
                throw new Exception();
            }
            return result as T ?? throw new Exception();
        }

        public Task PostObject<T>(string uri, T? @object) where T : class
        {
            throw new NotImplementedException();
        }

        public Task PutObject<T>(string uri, T? @object) where T : class
        {
            throw new NotImplementedException();
        }

        private FinancialOperationsController CreateController(params Profile[] profiles)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfiles(profiles));
            return new FinancialOperationsController(new FinancialOperationService(Context), new Mapper(config));
        }

    }
}
