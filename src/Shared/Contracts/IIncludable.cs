namespace Shared.Contracts
{
    public interface IIncludable
    { }

    public interface IIncludable<out TEntity> : IIncludable
    { }

    public interface IIncludable<out TEntity, out TProperty> : IIncludable<TEntity>
    { }
}