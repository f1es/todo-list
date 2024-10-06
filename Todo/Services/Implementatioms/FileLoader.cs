using System.IO;
using System.Text.Json;
using Todo.Models;
using Todo.Services.Interfaces;

namespace Todo.Services.Implementatioms;

public class FileLoader : IFileLoader
{
	private readonly string _todosFolder = "Todos";
	public async Task<IEnumerable<TodoTask>> LoadTodoTasksAsync()
	{
		var files = Directory.GetFiles(_todosFolder);
		var todos = new List<TodoTask>();
		foreach (var file in files)
		{
			try
			{
				var fileText = await File.ReadAllTextAsync(file);
				var todoObject = JsonSerializer.Deserialize<TodoTask>(fileText);
				todos.Add(todoObject);
			}
			catch 
			{
			
			}
		}

		return todos;
	}

	public async Task SaveTodoTaskAsync(TodoTask todoTask)
	{
		Directory.CreateDirectory(_todosFolder);
		var path = Path.Combine(_todosFolder, Guid.NewGuid().ToString() + ".json");

		using(var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
			await JsonSerializer.SerializeAsync(stream, todoTask);
	}

	public async Task DeleteTodoTaskAsync(Guid id)
	{
		var files = Directory.GetFiles(_todosFolder);
		foreach (var file in files)
		{
			var fileText = await File.ReadAllTextAsync(file);
			var todoObject = JsonSerializer.Deserialize<TodoTask>(fileText);
			if (todoObject.Id.Equals(id))
			{
				File.Delete(file);
				return;
			}
		}
	}

	public async Task UpdateTodo(Guid id, TodoTask todoTask)
	{
		var files = Directory.GetFiles(_todosFolder);
		foreach (var file in files)
		{
			var fileText = await File.ReadAllTextAsync(file);
			var todoObject = JsonSerializer.Deserialize<TodoTask>(fileText);
			if (todoObject.Id.Equals(id))
			{
				var newTodo = JsonSerializer.Serialize(todoTask);
				await File.WriteAllTextAsync(file, newTodo);
				return;
			}
		}
	}
}
