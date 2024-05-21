using System;
using Source.Codebase.Controllers.Presenters;
using UnityEngine;

namespace Source.Codebase.Presentation.Abstract
{
    public class ViewBase : MonoBehaviour, IViewBase
    {
        private IPresenter _presenter;

        public void Construct(IPresenter presenter)
        {
            if (presenter == null)
                throw new ArgumentNullException(nameof(presenter));

            gameObject.SetActive(false);
            _presenter = presenter;
            gameObject.SetActive(true);
        }

        public void Destroy() =>
            Destroy(gameObject);

        private void OnEnable()
        {
            _presenter?.Enable();
        }

        private void OnDisable()
        {
            _presenter?.Disable();
        }
    }
}