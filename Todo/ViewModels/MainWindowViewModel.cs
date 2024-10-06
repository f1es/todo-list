using FlesLib.WPF;
using System.Collections.ObjectModel;
using Todo.Services.Extensions;
using Todo.Services.Implementatioms;
using Todo.Services.Interfaces;
using Todo.ViewModels.Commands;
using Todo.Views;

namespace Todo.ViewModels;

public class MainWindowViewModel : ObservableObject
{
    private readonly IFileLoader _fileLoader;
	private readonly MainWindowCommandDictionary _commandDictionary;

	private int _page;
	private int _pageCount;
	private const int _pageSize = 10;

    public ObservableCollection<TodoView> Todos { get; set; }
	public MainWindowCommandDictionary CommandDictionary =>
	_commandDictionary;
	public int Page
	{
		get => _page;
		set
		{
			if (value < 0)
				return;

			_page = value;
			OnPropertyChanged();
		}
	}
	public int PageCount
	{
		get => _pageCount;
		set
		{
			_pageCount = value;
			OnPropertyChanged();
		}
	}
	public bool HasNextPage {  get; private set; }
	public bool HasPreviousPage { get; private set; }
	public MainWindowViewModel()
    {
		_fileLoader = new FileLoader();
        _commandDictionary = new MainWindowCommandDictionary(this, _fileLoader);
		_page = 1;

        Todos = new ObservableCollection<TodoView>();
        LoadTodosAsync();
    }

	public async Task LoadTodosAsync()
    {
		var todos = await _fileLoader.LoadTodoTasksAsync();
		var todosCount = todos.Count();

		todos = todos
			.OrderBy(td => td.IsAbadoned)
			.ThenByDescending(td => !td.IsCompleted)
			.ThenByDescending(td => td.StartDate)
			.Paginate(_pageSize, Page);

		var pageCount = todos.Count();

		UpdatePageData(todosCount, _pageSize);

		var isPageEmpty = pageCount == 0;
		if (isPageEmpty && HasPreviousPage)
		{
			Page -= 1;
			await LoadTodosAsync();
			return;
		}

		Todos.Clear();
		foreach (var todo in todos)
			Todos.Add(todo.ToView());
	}

	private void UpdatePageData(int todosCount, int pageSize)
	{
		if (todosCount == 0)
			Page = 0;

		if (todosCount > 0 && Page == 0)
			Page = 1;

		HasNextPage = Page < (double)todosCount / pageSize;
		HasPreviousPage = Page > 1;
		PageCount = (int)Math.Ceiling((double)todosCount / pageSize);
	}
}
