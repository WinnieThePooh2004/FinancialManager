using Frontend.Requests;
using Microsoft.AspNetCore.Components;
using FinancialManager.Shared.DTOs;

namespace Frontend.Pages.FinancialOperations
{
    public partial class Details
    {
        [Parameter] public int Id { get; set; }
        [Inject] private IFinancialOperationRequests FinancialOperationRequests { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;
        public FinancialOperationDTO? _operation;

        protected override async Task OnInitializedAsync()
        {
            _operation = await FinancialOperationRequests.GetByIdAsync(Id);
        }

        void Back()
        {
            Navigation.NavigateTo("/FinancialOperations");
        }

    }
}
