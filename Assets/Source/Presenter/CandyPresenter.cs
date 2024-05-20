using System.Collections;
using Source.Candies;
using Source.Factories;
using Source.Services;
using UnityEngine;

namespace Source.Presenter
{
    public class CandyPresenter : IPresenter
    {
        private readonly Candy _candy;
        private readonly CandyView _candyView;
        private readonly float _speed;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly MonoBehaviour _context;
        private readonly CandyFactory _factory;

        private Coroutine _move;

        public CandyPresenter(
            Candy candy,
            CandyView candyView,
            float speed,
            ICoroutineRunner coroutineRunner)
        {
            _candy = candy;
            _candyView = candyView;
            _speed = speed;
            _coroutineRunner = coroutineRunner;
            Index = candy.Index;
        }

        public int Index { get; private set; }

        public void Enable()
        {
        }

        public void Disable()
        {
            if (_move != null)
                _context.StopCoroutine(_move);

            _move = null;
        }

        public void Swap(Vector2 targetPosition)
            => _move = _context.StartCoroutine(StartMove(targetPosition));

        public void Remove()
        {
            _candyView.Destroy();
            _candy.Remove();
        }

        private IEnumerator StartMove(Vector2 targetPosition)
        {
            if (_move != null)
                yield return _move;

            Vector2 currentPosition;

            while (Mathf.Approximately(Vector2.Distance(_candy.Position, targetPosition), 0) == false)
            {
                currentPosition = Vector2.MoveTowards(_candy.Position, targetPosition, _speed * Time.deltaTime);
                _candy.ChangePosition(currentPosition);
                _candyView.ChangePosition(currentPosition);
                yield return null;
            }

            _move = null;
        }
    }
}
