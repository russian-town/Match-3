using System;
using System.Threading;
using System.Threading.Tasks;
using Source.Codebase.Presentation.Abstract;
using UnityEngine;

namespace Source.Codebase.Presentation
{
    public class CandyView : PoolableViewBase
    {
        private static readonly int Explode = Animator.StringToHash("Explode");

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _explosionAnimator;
        [SerializeField] private AnimationClip _explosionClip;

        public void SetSprite(Sprite sprite) =>
            _spriteRenderer.sprite = sprite;

        public async Task PlayExplosion(CancellationToken cancellationToken)
        {
            _spriteRenderer.enabled = false;
            _explosionAnimator.transform.gameObject.SetActive(true);
            _explosionAnimator.SetTrigger(Explode);
            await Task.Delay(TimeSpan.FromSeconds(_explosionClip.length), cancellationToken);
            _explosionAnimator.transform.gameObject.SetActive(false);
            _spriteRenderer.enabled = true;
        }
    }
}