using Frontend.Requests;
using Microsoft.AspNetCore.Components;
using FinancialManager.Shared.DTOs;

namespace Frontend.Pages.OperationTypes
{
    public partial class Update
    {
        [Parameter] public int Id { get; set; }
        [Inject] private IOperationTypesRequests OperationTypesRequests { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;
        private OperationTypeDTO? _operationType;

        protected override async Task OnInitializedAsync()
        {
            _operationType = await OperationTypesRequests.GetByIdAsync(Id);
        }

        public async Task Save(OperationTypeDTO operationType)
        {
            await OperationTypesRequests.UpdateAsync(operationType);
            Navigation.NavigateTo("/OperationTypes");
        }
    }
}
