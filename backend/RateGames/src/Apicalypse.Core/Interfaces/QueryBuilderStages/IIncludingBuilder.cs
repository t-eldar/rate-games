namespace Apicalypse.Core.Interfaces.QueryBuilderStages;
/// <summary>
/// Including builder, returns <see cref="IFilterBuilder{TEntity}"/>
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IIncludingBuilder<TEntity> : IFilterBuilder<TEntity>
{
	IFilterBuilder<TEntity> Include<TProp>(Expression<Func<TEntity, TProp>> selector);
}