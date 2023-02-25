namespace Apicalypse.Core.Extensions;

internal static class ObjectExtensions
{
	/// <summary>
	/// Use for including all properties in query.
	/// </summary>
	/// <typeparam name="T">Entity type</typeparam>
	/// <param name="entity"></param>
	/// <returns>Unchanged entity</returns>
	public static T IncludeProperties<T>(this T entity) => entity;
}