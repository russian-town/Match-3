namespace Source.Codebase.Infrastructure.Pools
{
    public interface IPoolable
    {
        void SetPool(IPool pool);
        void BackToPool();
    }
}