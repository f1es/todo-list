using Todo.Models;
using Todo.ViewModels;
using Todo.Views;

namespace Todo.Services.Extensions;

public static class TodoMapper
{
	public static TodoView ToView(this TodoTask todoTask)
	{
		var viewModel = new TodoViewModel(todoTask);
		var todoView = new TodoView()
		{
			DataContext = viewModel,
		};
		return todoView;
	}

	public static TodoTask ToTask(this TodoView todoView)
	{
		var viewModel = todoView.DataContext as TodoViewModel;
		var todoTask = viewModel.TodoTask;
		return todoTask;
	} 
}
