using Sourse.GameboardContent;
using Sourse.GameboardContent.CellContent;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sourse.HUD.Input
{
    public class Touchpad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Camera _camera;
        private Gameboard _gameboard;
        private Vector2 _firstTouchPosition;
        private Vector2 _finalTouchPosition;
        private float _swipeAngle;

        public void Construct(Camera camera, Gameboard gameboard)
        {
            _camera = camera;
            _gameboard = gameboard;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _firstTouchPosition = _camera.ScreenToWorldPoint(eventData.position);
            Cell cell = _gameboard.GetTouchCell(_firstTouchPosition);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _finalTouchPosition = _camera.ScreenToWorldPoint(eventData.position);
            Cell cell = _gameboard.GetTargetCell(_finalTouchPosition);
            CalculateAngle();
        }

        private void CalculateAngle()
        {
            _swipeAngle = Mathf.Atan2(_finalTouchPosition.y - _firstTouchPosition.y, _finalTouchPosition.x);
        }
    }
}
