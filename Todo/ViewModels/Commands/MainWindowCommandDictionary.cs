using FlesLib.WPF.Commands;
using FlesLib.WPF.Commands.Implementations;
using FlesLib.WPF.Windows;
using Todo.Models;
using Todo.Services.Implementatioms;
using Todo.Services.Interfaces;
using Todo.Views;

namespace Todo.ViewModels.Commands;

public class MainWindowCommandDictionary : ContextCommandDictionary
{
    private IFileLoader _fileLoader;
    public MainWindowCommandDictionary(
        MainWindowViewModel dataContext,
        IFileLoader fileLoader)
        : base(dataContext)
    {
        _fileLoader = fileLoader;

        AddCommand("SaveTodo", new RelayCommand(async c =>
        {
            var todoTask = new TodoTask
            {
                Id = Guid.NewGuid(),
                Description = "test",
                IsAbadoned = true,
                IsCompleted = true,
                StartDate = DateTime.Now,
            };

            await _fileLoader.SaveTodoTaskAsync(todoTask);
            await dataContext.LoadTodosAsync();
        }));

        AddCommand("AddTodo", new RelayCommand(c =>
        {
            if (!WindowsHandler.IsExist<AddTodoWindow>())
            {
                new AddTodoWindow().Show();
            }
        }));

        AddCommand("NextPage", new RelayCommand(async c =>
        {
            if (!dataContext.HasNextPage)
                return;

            dataContext.Page += 1;
            await dataContext.LoadTodosAsync();
        }));

		AddCommand("PreviousPage", new RelayCommand(async c =>
		{
            if (!dataContext.HasPreviousPage)
                return;

			dataContext.Page -= 1;
            await dataContext.LoadTodosAsync();
		}));

        AddCommand("Close", new RelayCommand(c =>
        {
            WindowsHandler.Close(dataContext);
        }));

        AddCommand("Minimize", new RelayCommand(c =>
        {
            WindowsHandler.Minimize(dataContext);
        }));
	}
}
