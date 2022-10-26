using FinancialManager.MapperProfiles.FinancialOperations;
using FinancialManager.Controllers;
using FinancialManager.Models;
using FinancialManager.Data;
using AutoMapper;
using Moq;
using FinancialManager.DTOs.FinancialOperations;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagetTest
{
    public class FinancialOperationsControllerTest
    {
        [Fact]
        public async Task TestFinancialOperationIndex()
        {
            var context = new Mock<FinancialManagerContext>();
            context.Setup<List<FinacialOperation>>(repo => repo.FinacialOperations.ToList())
                .Returns(await GetTestSessionAsync());
            var config = new MapperConfiguration(cfg => cfg.CreateMap<FinacialOperation, FinancialOperationIndexDto>()
                .ForMember(
                    dest => dest.Amount,
                    opt => opt.MapFrom(c => ((double)c.Amount / 100).ToString("0.00") + " UAH"))
                .ForMember(
                    dest => dest.DateTime,
                    opt => opt.MapFrom(c => c.DateTime.ToString("G"))
                ));
            var controller = new FinacialOperationsController(context.Object, new Mapper(config));
            await Task.Run(() => { });
            //var mapper = new Mapper(new FinancialOperationIndexProfile());
        }

        private async Task<List<FinacialOperation>> GetTestSessionAsync()
        {
            var testSession = new List<FinacialOperation>();
            await Task.Run(() => { });
            return testSession;
        }
    }
}