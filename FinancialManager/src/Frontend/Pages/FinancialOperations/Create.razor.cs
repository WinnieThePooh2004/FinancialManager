using Frontend.Requests;
using Microsoft.AspNetCore.Components;
using FinancialManager.Shared.DTOs;

namespace Frontend.Pages.FinancialOperations
{
    public partial class Create
    {
        [Inject] NavigationManager Navigation { get; set; } = default!;
        [Inject] IFinancialOperationRequests FinancialOperationRequests { get; set; } = default!;

        private async Task Save(FinancialOperationDTO financialOperation)
        {
            await FinancialOperationRequests.CreateAsync(financialOperation);
            Navigation.NavigateTo("/FinancialOperations");
        }
    }
}
