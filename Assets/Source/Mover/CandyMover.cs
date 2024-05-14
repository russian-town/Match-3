using System.Collections;
using Sourse.Candies;
using UnityEngine;

namespace Sourse.Mover
{
    public class CandyMover
    {
        private Candy _candy;
        private float _speed;
        private Coroutine _move;

        public CandyMover(Candy candy, float speed)
        {
            _candy = candy;
            _speed = speed;
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
