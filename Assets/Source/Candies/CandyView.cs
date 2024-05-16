using Sourse.Configs;
using UnityEngine;

namespace Sourse.Candies
{
    public class CandyView : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        public void Construct(Vector2 position, Transform parent, CandyConfig candyConfig)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            transform.parent = parent;
            transform.localPosition = position;
            _spriteRenderer.sprite = candyConfig.Texture;
        }

        public void ChangePosition(Vector2 position)
            => transform.localPosition = position;
    }
}
