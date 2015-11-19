using System.Data.Entity.Core.Objects;
using System.Linq;

namespace 作業客戶管理.Models
{
	public static class RepositoryIQueryableExtensions
	{
		public static IQueryable<T> Include<T>
			(this IQueryable<T> source, string path)
		{
			var objectQuery = source as ObjectQuery<T>;
			if (objectQuery != null)
			{
				return objectQuery.Include(path);
			}
			return source;
		}
	}
}