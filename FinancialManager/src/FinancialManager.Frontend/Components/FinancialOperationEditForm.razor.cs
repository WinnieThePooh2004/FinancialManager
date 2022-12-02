using Microsoft.AspNetCore.Components;
using FinancialManager.Shared.DTOs;
using FinancialManager.Frontend.Requests;
using MudBlazor;
using FinancialManager.Domain.Validatiors;

namespace FinancialManager.Frontend.Components
{
    public partial class FinancialOperationEditForm
    {
        [Parameter] public FinancialOperationDTO Model { get; set; } = new();
        [Parameter] public EventCallback<FinancialOperationDTO> OnSubmited { get; set; }
        [Inject] private IOperationTypesRequests OperationTypesRequests { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;
        private List<OperationTypeDTO> _allOperations = new();

        private MudForm? form;
        private readonly FinancialOperationDTOValidator _validator = new();
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

        private void DateChanged(DateTime? date)
        {
            if(date is null)
            {
                return;
            }
            var newDate = (DateTime)date;
            var dateOnly = new DateOnly(newDate.Year, newDate.Month, newDate.Day);
            Model.DateTime = dateOnly.ToDateTime(new TimeOnly(Model.DateTime.Hour, Model.DateTime.Minute, Model.DateTime.Second));
        }

        private void TimeChanged(TimeSpan? time)
        {
            if(time is null)
            {
                return;
            }
            var newTime = (TimeSpan)time;
            var dateOnly = new DateOnly(Model.DateTime.Year, Model.DateTime.Month, Model.DateTime.Day);
            Model.DateTime = dateOnly.ToDateTime(new TimeOnly(newTime.Hours, newTime.Minutes, newTime.Seconds));
        }
    }
}
