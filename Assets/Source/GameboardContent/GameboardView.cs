using Source.Presenter;
using UnityEngine;

namespace Source.GameboardContent
{
    public class GameboardView : MonoBehaviour
    {
        private IPresenter _presenter;

        public void Construct(Vector2 position, IPresenter presenter)
        {
            transform.position = position;
            _presenter = presenter;
        }

        public void Enable()
            => _presenter.Enable();

        public void Disable()
            => _presenter.Disable();
    }
}
