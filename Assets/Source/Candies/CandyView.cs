using Source.Presenter;
using UnityEngine;

namespace Source.Candies
{
    public class CandyView : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private IPresenter _presenter;

        public void Construct(
            Transform parent,
            IPresenter presenter)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            transform.parent = parent;
            _presenter = presenter;
        }

        public void SetSprite(Sprite sprite) =>
            _spriteRenderer.sprite = sprite;

        public void ChangePosition(Vector2 position)
            => transform.localPosition = position;

        public void Destroy()
        {
            _presenter.Disable();
            gameObject.SetActive(false);
        }
    }
}
