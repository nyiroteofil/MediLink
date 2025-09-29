using CommunityToolkit.Mvvm.Input;
using MediLine_FrontEnd.Models;

namespace MediLine_FrontEnd.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}