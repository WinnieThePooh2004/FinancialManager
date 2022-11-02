using AutoMapper;
using FinancialManager.Controllers;
using FinancialManager.MapperProfiles.OperationTypes;
using FinancialManager.Services.CRUDServices;
using FinancialManagerTest.Mocks.Data;
using Frontend.HttpService;
using Shared.DTOs.OperationTypes;

namespace FinancialManagerTest.Mocks.HttpServices
{
    internal class MockOperationTypesHttpService : IHttpService
    {
        public string FinancialOperationUri => "";

        public string OperationTypesUri => "";

        public string ReportUri => "";

        public MockFinancialManagerContext Context { get; }

        public MockOperationTypesHttpService()
        {
            Context = new();
        }

        public MockOperationTypesHttpService(MockFinancialManagerContext context)
        {
            Context = context;
        }

        public async Task DeleteObject(string uri, int id)
        {
            var controller = CreateController(new OperationTypeDetailsProfile());
            await controller.DeleteOperationType(id);
        }

        public async Task<T> GetObjectAsync<T>(string uri) where T : class
        {
            try
            {
                var result = (await CreateController(new OperationTypeIndexProfile()).GetOperationType()).Value;
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
            var result = (await CreateController(new OperationTypeDetailsProfile()).GetOperationType(id)).Value;
            if (result is null)
            {
                throw new Exception("object is null");
            }
            return result as T ?? throw new Exception();
        }

        public async Task PostObject<T>(string uri, T? @object) where T : class
        {
            await CreateController(new OperationTypeCreateProfile())
                .PostOperationType(@object as OperationTypeCreateDto ?? throw new Exception());
        }

        public async Task PutObject<T>(string uri, int id, T? @object) where T : class
        {
            await CreateController(new OperationTypeUpdateProfile())
                .PutOperationType(id, @object as OperationTypeUpdateDto ?? throw new Exception());
        }

        private OperationTypesController CreateController(params Profile[] profiles)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfiles(profiles));
            return new OperationTypesController(new OperationTypeService(Context), new Mapper(config));
        }

    }
}
