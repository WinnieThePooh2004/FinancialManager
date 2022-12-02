using Frontend.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using FinancialManager.Shared.DTOs;

namespace Frontend.Components
{
    public partial class FinancialOperationEditForm
    {
        [Parameter] public FinancialOperationDTO? Model { get; set; }
        [Parameter] public EventCallback<FinancialOperationDTO> OnSubmited { get; set; }
        [Inject] private IOperationTypesRequests OperationTypesRequests { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;
        private List<OperationTypeDTO> _allOperations = new();
        private EditForm? form;

        protected override async Task OnInitializedAsync()
        {
            _allOperations = await OperationTypesRequests.GetAllAsync();
        }

        private async Task Submit()
        {
            if(!IsValid())
            {
                return;
            }
            await OnSubmited.InvokeAsync(Model);
        }

        private void Back()
        {
            Navigation.NavigateTo("/FinancialOperations");
        }

        private bool IsValid()
        {
            return true;
        }
    }
}
