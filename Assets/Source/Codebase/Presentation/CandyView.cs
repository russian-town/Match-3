using Source.Codebase.Infrastructure.Pools;
using UnityEngine;

namespace Source.Codebase.Presentation
{
    public class CandyView : PoolableViewBase
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void SetSprite(Sprite sprite) =>
            _spriteRenderer.sprite = sprite;
    }
}