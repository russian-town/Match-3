using System;
using Sourse.GameboardContent;
using Sourse.GameboardContent.CellContent;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sourse.HUD.Input
{
    public class Touchpad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Camera _camera;
        private Vector2 _firstTouchPosition;
        private Vector2 _finalTouchPosition;
        private float _swipeAngle;

        public event Action<Vector2> TouchStarted;

        public event Action<Vector2> TouchEnded;

        public void Construct(Camera camera)
            => _camera = camera;

        public void OnPointerDown(PointerEventData eventData)
        {
            _firstTouchPosition = _camera.ScreenToWorldPoint(eventData.position);
            TouchStarted?.Invoke(_firstTouchPosition);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _finalTouchPosition = _camera.ScreenToWorldPoint(eventData.position);
            TouchEnded?.Invoke(_finalTouchPosition);
            CalculateAngle();
        }

        private void CalculateAngle()
        {
            _swipeAngle = Mathf.Atan2(_finalTouchPosition.y - _firstTouchPosition.y, _finalTouchPosition.x);
        }
    }
}
