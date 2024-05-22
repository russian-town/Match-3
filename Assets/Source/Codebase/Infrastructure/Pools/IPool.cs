namespace Source.Codebase.Infrastructure.Pools
{
    public interface IPool
    {
        void Release(IPoolable poolable);
        void Clear();
        IPoolable Get();
    }
}