using RateGames.Common.Contracts;

namespace RateGames.Common.Utils;
public class IdOr<TEntity> : IEntity
    where TEntity : IEntity
{
    public IdOr(int id) => Id = id;
    public IdOr(TEntity entity) => Value = entity;

    public static implicit operator IdOr<TEntity>(int id) => new(id);
    public static implicit operator IdOr<TEntity>(TEntity entity) => new(entity);
    public static explicit operator TEntity?(IdOr<TEntity> idOr) => idOr.Value;
    public static explicit operator int(IdOr<TEntity> idOr) => idOr.Id;

    public int Id { get; set; }
    public TEntity? Value { get; set; }
}