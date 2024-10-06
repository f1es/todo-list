using FlesLib.WPF;
using Todo.Models;
using Todo.Services.Implementatioms;
using Todo.ViewModels.Commands;

namespace Todo.ViewModels;

public class TodoViewModel : ObservableObject
{
	private TodoTask _todoTask;
	private TodoViewCommandDictionary _commandDictionary;
	public TodoViewCommandDictionary CommandDictionary =>
		_commandDictionary;

	public TodoTask TodoTask
	{
		get => _todoTask;
		set
		{
			_todoTask = value;
			OnPropertyChanged();
		}
	}

    public TodoViewModel(TodoTask todoTask)
    {
        _todoTask = todoTask;
		_commandDictionary = new TodoViewCommandDictionary(this, new FileLoader());
    }

    public TodoViewModel()
    { }

	public bool IsCheckBoxesValid(TodoTask todo) =>
		!(todo.IsAbadoned && todo.IsCompleted);
}
