using FinancialManager.Frontend.Requests;
using Shared.DTOs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FinancialManager.Frontend.Pages.FinancialOperations
{
    public partial class Index
    {
        [Inject] private IFinancialOperationsRequests Requests { get; set; } = default!;
        private List<FinancialOperationDTO> _operation = new();
        private MudTable<FinancialOperationDTO>? _table;
        protected override async Task OnInitializedAsync()
        {
            _operation = await Requests.GetAllAsync();
        }


    }
}
