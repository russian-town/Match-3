using System.Collections.Generic;

namespace Source.Codebase.Infrastructure.Pools
{
    public class Pool: IPool
    {
        private List<object> _poolables = new();

        public IPoolable Get() =>
            _poolables.Count == 0 ? null : _poolables[0] as IPoolable;

        public void Release(IPoolable poolable) =>
            _poolables.Add(poolable);

        public void Clear() =>
            _poolables.Clear();
    }
}