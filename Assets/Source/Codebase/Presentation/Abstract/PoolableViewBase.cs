using System;
using Source.Codebase.Controllers.Presenters;
using Source.Codebase.Infrastructure.Pools;
using UnityEngine;

namespace Source.Codebase.Presentation.Abstract
{
    public abstract class PoolableViewBase : MonoBehaviour, IPoolable
    {
        private IPresenter _presenter;
        private IPool _pool;

        public void Construct(IPresenter presenter)
        {
            if (presenter == null)
                throw new ArgumentNullException(nameof(presenter));

            gameObject.SetActive(false);
            _presenter = presenter;
            gameObject.SetActive(true);
        }

        public void Destroy()
        {
            if (_pool != null)
            {
                BackToPool();

                return;
            }

            Destroy(gameObject);
        }

        private void OnEnable()
        {
            _presenter?.Enable();
        }

        private void OnDisable()
        {
            _presenter?.Disable();
        }

        public void SetPool(IPool pool) =>
            _pool = pool;

        public void BackToPool()
        {
            if (_pool == null)
                throw new Exception("You trying to release poolable, but poolable without pool!");

            gameObject.SetActive(false);
            _pool.Release(this);
        }
    }
}