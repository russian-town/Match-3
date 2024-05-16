using System.Collections;
using Sourse.Candies;
using UnityEngine;

namespace Sourse.Presenter
{
    public class CandyPresenter : IPresenter
    {
        private readonly Candy _candy;
        private readonly CandyView _candyView;
        private readonly float _speed;
        private readonly MonoBehaviour _context;

        private Coroutine _move;

        public CandyPresenter(Candy candy,
            CandyView candyView,
            float speed,
            MonoBehaviour context)
        {
            _candy = candy;
            _candyView = candyView;
            _speed = speed;
            Index = candy.Index;
            _context = context;
        }

        public int Index { get; private set; }

        public Vector2 CandyPosition => _candy.Position;

        public void Enable()
        {
        }

        public void Disable()
        {
            if (_move != null)
                _context.StopCoroutine(_move);

            _move = null;
        }

        public void Swape(Vector2 targetPosition)
        {
            _candy.ChangePosition(targetPosition);
            _candyView.ChangePosition(targetPosition);
        }

        public void RemoveCandy()
        {
            _candy.Destroy();
            _candyView.Disable();
        }

        public void Move(Vector2 targetPosition, MonoBehaviour context)
        {
            if (_move != null)
                return;

            _move = _context.StartCoroutine(StartMove(targetPosition));
        }

        private IEnumerator StartMove(Vector2 targetPosition)
        {
            Vector2 currentPosition = Vector2.zero;

            while (Vector2.Distance(_candy.Position, targetPosition) > 0)
            {
                currentPosition = Vector2.Lerp(currentPosition, targetPosition, _speed);
                _candy.ChangePosition(currentPosition);
                yield return null;
            }

            _move = null;
        }
    }
}
