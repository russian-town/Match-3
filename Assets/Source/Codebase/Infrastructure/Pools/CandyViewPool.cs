using System.Collections.Generic;

namespace Source.Codebase.Infrastructure.Pools
{
    public class Pool : IPool
    {
        private readonly Queue<object> _poolables = new();

        public IPoolable Get()
        {
            if (_poolables.Count == 0)
                return null;

            return _poolables.Dequeue() as IPoolable;
        }

        public void Release(IPoolable poolable) =>
            _poolables.Enqueue(poolable);

        public void Clear() =>
            _poolables.Clear();
    }
}