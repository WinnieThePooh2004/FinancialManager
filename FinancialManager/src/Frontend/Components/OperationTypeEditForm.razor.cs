using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using FinancialManager.Shared.DTOs;

namespace Frontend.Components
{
    public partial class OperationTypeEditForm
    {
        [Parameter] public OperationTypeDTO Model { get; set; } = new();
        [Parameter] public EventCallback<OperationTypeDTO> OnSubmited { get; set; }
        [Inject] private NavigationManager Navigation { get; set; } = default!;
        private EditForm? _form;
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
            Navigation.NavigateTo("/OperationTypes");
        }

        private bool IsValid()
        {
            return true;
        }
    }
}
