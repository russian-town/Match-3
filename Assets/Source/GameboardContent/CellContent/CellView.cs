using Sourse.Presenter;
using UnityEngine;

namespace Sourse.GameboardContent.CellContent
{
    public class CellView : MonoBehaviour
    {
        private IPresenter _presenter;

        public void Enable()
            => _presenter.Enable();

        public void Disable()
            => _presenter.Disable();

        public void Constuct(IPresenter presenter, Vector2 position, Transform parent)
        {
            _presenter = presenter;
            transform.parent = parent;
            transform.localPosition = position;
            Enable();
        }
    }
}
