using System;
using Source.Codebase.Domain;
using UnityEngine;

namespace Source.Codebase.Controllers.Inputs
{
    public class Touchpad : MonoBehaviour, ITouchpad
    {
        private Camera _camera;
        private Vector2 _startTouchPosition;
        private Vector2 _endTouchPosition;
        private float _minSqrTouchDistance = 0.3f;

        public event Action<Vector2> TouchStarted;

        public event Action<Vector2> TouchEnded;

        public Direction LastTouchDirection { get; private set; }

        public void Construct(Camera mainCamera)
            => _camera = mainCamera;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startTouchPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                TouchStarted?.Invoke(_startTouchPosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                _endTouchPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                LastTouchDirection = CalculateTouchDirection(_endTouchPosition - _startTouchPosition);

                TouchEnded?.Invoke(_endTouchPosition);
            }
        }

        private Direction CalculateTouchDirection(Vector2 touchDirection)
        {
            if (touchDirection.sqrMagnitude < _minSqrTouchDistance)
                return Direction.Unknown;

            if (Math.Abs(touchDirection.x) > Math.Abs(touchDirection.y))
                return touchDirection.x > 0 ? Direction.Right : Direction.Left;

            return touchDirection.y > 0 ? Direction.Up : Direction.Down;
        }
    }
}