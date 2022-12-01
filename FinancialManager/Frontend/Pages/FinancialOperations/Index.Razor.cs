using Frontend.Requests;
using Microsoft.AspNetCore.Components;
using FinancialManager.Shared.DTOs;

namespace Frontend.Pages.FinancialOperations
{
    public partial class Index
    {
        [Inject] IFinancialOperationRequests FinancialOperationRequests { get; set; } = default!;
        private List<FinancialOperationDTO>? _operations;
        protected override async Task OnInitializedAsync()
        {
            _operations = await FinancialOperationRequests.GetAllAsync();
        }

        private async Task Delete(FinancialOperationDTO financialOperation)
        {
            await FinancialOperationRequests.DeleteAsync(financialOperation.Id);
            _operations = await FinancialOperationRequests.GetAllAsync();
            StateHasChanged();
        }
    }
}
