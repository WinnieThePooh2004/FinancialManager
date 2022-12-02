using FinancialManager.Frontend.Requests;
using FinancialManager.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace FinancialManager.Frontend.Pages.OperationTypes
{
    public partial class Update
    {
        [Parameter] public int Id { get; set; }
        [Inject] private IOperationTypesRequests OperationTypesRequests { get; set; } = default!;
        private OperationTypeDTO? _operationType;

        protected override async Task OnInitializedAsync()
        {
            _operationType = await OperationTypesRequests.GetByIdAsync(Id);
        }

        private async Task Save(OperationTypeDTO operationType)
        {
            await OperationTypesRequests.UpdateAsync(operationType);
        }
    }
}
