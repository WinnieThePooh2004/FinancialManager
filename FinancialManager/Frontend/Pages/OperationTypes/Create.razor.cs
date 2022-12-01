using Frontend.Requests;
using Microsoft.AspNetCore.Components;
using FinancialManager.Shared.DTOs;

namespace Frontend.Pages.OperationTypes
{
    public partial class Create
    {
        [Inject] IOperationTypesRequests OperationTypesRequests { get; set; } = default!;
        [Inject] NavigationManager Navigation { get; set; } = default!;
        private async Task Save(OperationTypeDTO operationType)
        {
            await OperationTypesRequests.CreateAsync(operationType);
            Navigation.NavigateTo("/OperationTypes");
        }
    }
}
