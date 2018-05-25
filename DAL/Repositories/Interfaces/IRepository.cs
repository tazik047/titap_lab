using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAL.Repositories.Interfaces
{
	public interface IRepository<T> where T : class, new()
	{
		/// <summary>
		/// Create entry in database.
		/// </summary>
		/// <param name="entity">Entry to add.</param>
		void Create(T entity);

		/// <summary>
		/// Update entry in database.
		/// </summary>
		/// <param name="entity">Entry to update.</param>
		void Update(T entity);

		/// <summary>
		/// Get's entity by keys
		/// </summary>
		/// <param name="keyValues">Key values</param>
		/// <returns>returns found objects</returns>
		T Find(params object[] keyValues);

		/// <summary>
		/// Get all entries from database.
		/// </summary>
		/// <returns>List of entries.</returns>
		IEnumerable<T> Get();

		/// <summary>
		/// Get single entry from database.
		/// </summary>
		/// <param name="predicate">Filtering expression</param>
		/// <returns>Entry with specified id.</returns> 
		T Single(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Get single entry from database.
		/// </summary>
		/// <param name="predicate">Filtering expression</param>
		/// <returns>Entry with specified id.</returns> 
		T SingleOrDefault(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Get first entry from database.
		/// </summary>
		/// <returns>Entry with specified id.</returns> 
		T First();

		/// <summary>
		/// Get first entry from database.
		/// </summary>
		/// <param name="predicate">Filtering expression</param>
		/// <returns>Entry with specified id.</returns> 
		T First(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Get count of entries from database.
		/// </summary>
		/// <returns>Count of entries.</returns>
		int Count();

		/// <summary>
		/// Get count of entries from database by predicate.
		/// </summary>
		/// <param name="predicate">Predicate to filter entities.</param>
		/// <returns>Count of entries.</returns>
		int Count(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Get all entries from database by expression.
		/// </summary>
		/// <param name="predicate">Expression to find.</param>
		/// <returns>List of entries.</returns>
		IEnumerable<T> Get(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Check existence of entity by predicate.
		/// </summary>
		/// <param name="predicate">Expression to find.</param>
		/// <returns>Boolean variable that indicate if such entity exist.</returns>
		bool Any(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Gets filtered entries list by predicate.
		/// </summary>
		/// <param name="predicate">Filter predicate.</param>
		/// <param name="skip">Number of rows to skip.</param>
		/// <param name="take">Number of rows to take.</param>
		/// <returns>List of filtered entries.</returns>
		IEnumerable<T> Get(Expression<Func<T, bool>> predicate, int skip, int take);

		void Drop(T item);
	}
}