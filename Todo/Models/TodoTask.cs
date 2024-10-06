namespace Todo.Models;

public class TodoTask
{
	private bool _isCompleted;
	private bool _isAbadoned;

	public Guid Id { get; set; }
	public string? Description { get; set; }
	public bool IsCompleted
	{
		get => _isCompleted;
		set
		{
			if (IsAbadoned)
				return;

			_isCompleted = value;
		}
	}
	public bool IsAbadoned
	{
		get => _isAbadoned;
		set
		{
			if (IsCompleted) 
				return;

			_isAbadoned = value;
		}
	}
	public DateTime StartDate { get; set; }
}
