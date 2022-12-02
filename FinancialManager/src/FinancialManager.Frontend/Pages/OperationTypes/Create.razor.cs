using FinancialManager.Frontend.Requests;
using FinancialManager.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace FinancialManager.Frontend.Pages.OperationTypes
{
    public partial class Create
    {
        [Inject] private IOperationTypesRequests OperationTypesRequests { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;

        private async Task Save(OperationTypeDTO operationType)
        {
            await OperationTypesRequests.CreateAsync(operationType);
            Navigation.NavigateTo("/OperationTypes");
        }
    }
}
