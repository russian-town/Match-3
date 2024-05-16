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

        private Coroutine _move;

        public CandyPresenter(Candy candy, CandyView candyView, float speed)
        {
            _candy = candy;
            _candyView = candyView;
            _speed = speed;
            Index = candy.Index;
        }

        public int Index { get; private set; }

        public Vector2 CandyPosition => _candy.Position;

        public void Enable()
        {
        }

        public void Disable()
        {
        }

        public void Swape(Vector2 targetPosition)
        {
            _candy.ChangePosition(targetPosition);
            _candyView.ChangePosition(targetPosition);
        }

        public void Move(Vector2 targetPosition, MonoBehaviour context)
        {
            if (_move != null)
                return;

            _move = context.StartCoroutine(StartMove(targetPosition));
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
