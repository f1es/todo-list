using FlesLib.WPF;
using FlesLib.WPF.Commands;
using FlesLib.WPF.Windows;
using System.Windows;
using System.Windows.Input;
using Todo.Models;
using Todo.Services.Implementatioms;
using Todo.Services.Interfaces;

namespace Todo.ViewModels;

public class AddTodoWindowViewModel : ObservableObject
{
	private IFileLoader _fileLoader;
	private string _description = string.Empty;
	public string Description
	{
		get => _description;
		set
		{
			_description = value;
			OnPropertyChanged();
		}
	}

	public ICommand AcceptCommand =>
		new RelayCommand(async c =>
		{
			if (!IsLengthValid(Description, 175))
			{
				MessageBox.Show("Description is too long, it dosn't fit in todo :(");
				return;
			}

			var todo = new TodoTask()
			{
				Id = Guid.NewGuid(),
				Description = _description,
				IsAbadoned = false,
				IsCompleted = false,
				StartDate = DateTime.Now,
			};

			await _fileLoader.SaveTodoTaskAsync(todo);

			var mainWindowViewModel = WindowsHandler.GetMainWindowContext() as MainWindowViewModel;
			await mainWindowViewModel.LoadTodosAsync();

			WindowsHandler.Close(this);
		});

	public ICommand CancelCommand =>
		new RelayCommand(c =>
		{
			WindowsHandler.Close(this);
		});


    public AddTodoWindowViewModel()
    {
		_fileLoader = new FileLoader();
    }

	public bool IsLengthValid(string description, int maxLength) =>
		description.Length < maxLength;
}
