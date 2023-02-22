namespace Apicalypse.Core.Interfaces;
public interface IQueryBuilderCreator
{
	/// <summary>
	/// Creates <see cref="IQueryBuilder{TEntity}"/> for model <typeparamref name="TEntity"/>.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <returns></returns>
	IQueryBuilder<TEntity> CreateFor<TEntity>();
}
