using FinancialManager.Domain.Validatiors;
using FinancialManager.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FinancialManager.Frontend.Components
{
    public partial class OperationTypeEditForm
    {
        [Parameter] public OperationTypeDTO Model { get; set; } = new();
        [Parameter] public EventCallback<OperationTypeDTO> OnSubmited { get; set; }
        [Inject] private NavigationManager Navigation { get; set; } = default!;

        private MudForm? _form;
        private OperationTypeDTOValidator _validator = new();

        private async Task Submit()
        {
            if (_form is null)
            {
                return;
            }
            await _form.Validate();
            if (!_form.IsValid)
            {
                return;
            }
            await OnSubmited.InvokeAsync(Model);
        }

        private void Back()
        {
            Navigation.NavigateTo("/OperationTypes");
        }
    }
}
