namespace Todo.Services.Extensions;

public static class PagingExtension
{
	public static IEnumerable<T> Paginate<T>(this IEnumerable<T> values, int pageSize, int pageNumber) =>
		values
		.Skip((pageNumber - 1) * pageSize)
		.Take(pageSize);
}
