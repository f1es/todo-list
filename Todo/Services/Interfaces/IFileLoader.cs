using Todo.Models;

namespace Todo.Services.Interfaces;

public interface IFileLoader
{
	Task SaveTodoTaskAsync(TodoTask todoTask);
	Task<IEnumerable<TodoTask>> LoadTodoTasksAsync();
	Task DeleteTodoTaskAsync(Guid id);
	Task UpdateTodo(Guid id, TodoTask todoTask);
}
