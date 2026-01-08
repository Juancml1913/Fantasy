using Fantasy.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fantasy.Fronted.Layout
{
    public partial class MainLayout
    {
        [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
    }
}