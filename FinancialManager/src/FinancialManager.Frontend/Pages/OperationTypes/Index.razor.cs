using FinancialManager.Frontend.Components;
using FinancialManager.Frontend.Requests;
using FinancialManager.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FinancialManager.Frontend.Pages.OperationTypes
{
    public partial class Index
    {
        [Inject] private IOperationTypesRequests OperationTypesRequests { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;
        [Inject] private ISnackbar Snackbar { get; set; } = default!;

        private MudTable<OperationTypeDTO>? _table = null;

        private async Task<TableData<OperationTypeDTO>> LoadData(TableState state)
        {
            var operationTypes = await OperationTypesRequests.GetAllAsync();
            return new()
            {
                Items = operationTypes,
                TotalItems = operationTypes.Count,
            };
        }

        private async Task Delete(int id)
        {
            var parameters = new DialogParameters
            {
                ["ContentText"] = "Do you really want to delete this operation type? This process cannot be undone.",
                ["ButtonText"] = "Delete",
                ["Color"] = Color.Error
            };

            var options = new DialogOptions() { MaxWidth = MaxWidth.ExtraSmall };
            var dialogResult = await DialogService.Show<DialogConfirm>("Delete", parameters, options).Result;

            if (dialogResult.Cancelled)
            {
                return;
            }
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomLeft;
            await OperationTypesRequests.DeleteAsync(id);
            Snackbar.Add("Deleted!", Severity.Error);
            await _table.ReloadServerData();
        }
    }
}
