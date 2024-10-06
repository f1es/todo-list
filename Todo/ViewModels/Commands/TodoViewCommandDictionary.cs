using FlesLib.WPF.Commands;
using FlesLib.WPF.Commands.Implementations;
using FlesLib.WPF.Windows;
using Todo.Services.Interfaces;

namespace Todo.ViewModels.Commands;

public class TodoViewCommandDictionary : ContextCommandDictionary
{
    private IFileLoader _fileLoader;
    public TodoViewCommandDictionary(
        TodoViewModel dataContext,
        IFileLoader fileLoader)
        : base(dataContext)
    {
        _fileLoader = fileLoader;

        AddCommand("DeleteTodo", new RelayCommand(async c =>
        {
            await _fileLoader.DeleteTodoTaskAsync(dataContext.TodoTask.Id);

            var mainWindowViewModel = WindowsHandler.GetMainWindowContext() as MainWindowViewModel;
            await mainWindowViewModel.LoadTodosAsync();
        }));

        AddCommand("UpdateTodo", new RelayCommand(async c =>
        {
            await _fileLoader.UpdateTodo(dataContext.TodoTask.Id, dataContext.TodoTask);
        }));
    }
}
