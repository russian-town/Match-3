using Sourse.Configs;
using Sourse.Factories;
using Sourse.Presenter;
using UnityEngine;

namespace Sourse.Candies
{
    public class CandyView : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private IPresenter _presenter;

        public void Construct(
            Vector2 position,
            Transform parent,
            CandyConfig candyConfig,
            IPresenter presenter)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            transform.parent = parent;
            transform.localPosition = position;
            _spriteRenderer.sprite = candyConfig.Texture;
            _presenter = presenter;
        }

        public void ChangePosition(Vector2 position)
            => transform.localPosition = position;

        public void Destroy()
        {
            _presenter.Disable();
            gameObject.SetActive(false);
        }
    }
}
