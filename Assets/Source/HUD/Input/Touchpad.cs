using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Source.HUD.Input
{
    public class Touchpad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Camera _camera;
        private Vector2 _firstTouchPosition;
        private Vector2 _finalTouchPosition;

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
        }
    }
}
