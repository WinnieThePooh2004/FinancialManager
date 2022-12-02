using FinancialManager.Frontend.Requests;
using FinancialManager.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using FinancialManager.Frontend.Components;

namespace FinancialManager.Frontend.Pages.FinancialOperations
{
    public partial class Index
    {
        [Inject] private IFinancialOperationsRequests FinancialOperationsRequests { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;
        [Inject] private ISnackbar Snackbar { get; set; } = default!;

        private MudTable<FinancialOperationDTO>? _table;

        private async Task<TableData<FinancialOperationDTO>> LoadData(TableState state)
        {
            var operations = await FinancialOperationsRequests.GetAllAsync();
            return new TableData<FinancialOperationDTO>()
            {
                Items = operations,
                TotalItems = operations.Count,
            };
        }

        private async Task Delete(int id)
        {
            var parameters = new DialogParameters
            {
                { "ContentText", "Do you really want to delete this operation? This process cannot be undone." },
                { "ButtonText", "Delete" },
                { "Color", Color.Error }
            };

            var options = new DialogOptions() { MaxWidth = MaxWidth.ExtraSmall };
            var dialogResult = await DialogService.Show<DialogConfirm>("Delete", parameters, options).Result;

            if (dialogResult.Cancelled)
            {
                return;
            }
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomLeft;
            await FinancialOperationsRequests.DeleteAsync(id);
            Snackbar.Add("Deleted!", Severity.Error);
            await _table.ReloadServerData();
        }
    }
}
