using Frontend.Requests;
using Microsoft.AspNetCore.Components;
using FinancialManager.Shared.DTOs;

namespace Frontend.Pages.OperationTypes
{
    public partial class Index
    {
        [Inject] IOperationTypesRequests OperationTypesRequests { get; set; } = default!;
        private List<OperationTypeDTO>? _operationTypes { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            _operationTypes = await OperationTypesRequests.GetAllAsync();
        }

        private async Task Delete(OperationTypeDTO operationType)
        {
            await OperationTypesRequests.DeleteAsync(operationType.Id);
            _operationTypes = await OperationTypesRequests.GetAllAsync();
            StateHasChanged();
        }
    }
}
