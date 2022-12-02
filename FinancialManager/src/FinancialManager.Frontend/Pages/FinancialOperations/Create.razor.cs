using Microsoft.AspNetCore.Components;
using FinancialManager.Shared.DTOs;
using FinancialManager.Frontend.Requests;

namespace FinancialManager.Frontend.Pages.FinancialOperations
{
    public partial class Create
    {
        [Inject] NavigationManager Navigation { get; set; } = default!;
        [Inject] IFinancialOperationsRequests FinancialOperationRequests { get; set; } = default!;

        private async Task Save(FinancialOperationDTO financialOperation)
        {
            await FinancialOperationRequests.CreateAsync(financialOperation);
            Navigation.NavigateTo("/FinancialOperations");
        }
    }
}
