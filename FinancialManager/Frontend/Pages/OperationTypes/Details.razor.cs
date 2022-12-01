using Microsoft.AspNetCore.Components;
using FinancialManager.Shared.DTOs;
using Frontend.Requests;

namespace Frontend.Pages.OperationTypes
{
    public partial class Details
    {
        [Parameter] public int Id { get; set; }
        [Inject] private IOperationTypesRequests OperationTypesRequests { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;
        private OperationTypeDTO? _operationType;
        
        protected override async Task OnInitializedAsync()
        {
            _operationType = await OperationTypesRequests.GetByIdAsync(Id);
        }

        private async Task Delete()
        {
            await OperationTypesRequests.DeleteAsync(Id);
            Navigation.NavigateTo("/OperationTypes");
        }

        void Back()
        {
            Navigation.NavigateTo("/OperationTypes");

        }

    }
}
