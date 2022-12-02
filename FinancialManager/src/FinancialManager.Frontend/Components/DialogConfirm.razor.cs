using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FinancialManager.Frontend.Components
{
    public partial class DialogConfirm : ComponentBase
    {
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; } = default!;
        [Parameter] public string ContentText { get; set; } = default!;
        [Parameter] public string ButtonText { get; set; } = default!;
        [Parameter] public Color Color { get; set; } = default!;

        void Submit() => MudDialog.Close(DialogResult.Ok(true));
        void Cancel() => MudDialog.Cancel();
    }
}
