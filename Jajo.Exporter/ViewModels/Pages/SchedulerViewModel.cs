using Jajo.Ui.Common;
using Jajo.Ui.MVVM.Services;
using Jajo.Utils.ViewModels;

namespace Jajo.Exporter.ViewModels.Pages;

public class SchedulerViewModel : PageBaseViewModel, IViewModelBase
{
    public SnackbarService SnackbarService { get; set; }

    /// <summary>
    ///     Override method from abstract class
    /// </summary>
    protected override void Export()
    {
        if (SnackbarService is null) return;

        // Just an example how to use a snackbar
        if (IsExportToDwgSelected)
            SnackbarService.Show("Schedule was updated!", ControlAppearance.Success);
        // logic when the dwg export check box was not selected
        else
            SnackbarService.Show("Schedule update failed!", ControlAppearance.Failure);
    }
}