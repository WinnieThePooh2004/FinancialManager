using Frontend.Requests;
using Microsoft.AspNetCore.Components;
using FinancialManager.Shared.DTOs;

namespace Frontend.Pages.FinancialOperations
{
    public partial class Update
    {
        [Parameter] public int Id { get; set; }
        [Inject] NavigationManager Navigation { get; set; } = default!;
        [Inject] IFinancialOperationRequests FinancialOperationRequests { get; set; } = default!;
        private FinancialOperationDTO? _financialOperation;

        protected override async Task OnInitializedAsync()
        {
            _financialOperation = await FinancialOperationRequests.GetByIdAsync(Id);
        }

        private async Task Save(FinancialOperationDTO financialOperation)
        {
            await FinancialOperationRequests.UpdateAsync(financialOperation);
            Navigation.NavigateTo("/FinancialOperations");
        }

    }
}
