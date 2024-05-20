using Sourse.Candies;
using Unity.VisualScripting;
using UnityEngine;

namespace Sourse.GameboardContent.CellContent
{
    public class Cell
    {
        private Candy _candy;

        public Cell(Vector2 worldPosition, int index)
        {
            WorldPosition = worldPosition;
            Index = index;
        }

        public Vector2 WorldPosition { get; private set; }

        public int Index { get; private set; }

        public Candy Candy => _candy;

        public CandyType CandyType => Candy.Type;

        public bool IsEmpty => _candy == null;

        public void SetCandy(Candy candy)
        {
            if(candy == null)
            {
                if (_candy != null)
                    _candy.Destroyed -= OnDestroyed;

                _candy = null;
                return;
            }

            if (_candy != null && candy.Index != _candy.Index)
                _candy.Destroyed -= OnDestroyed;

            _candy = candy;
            _candy.Destroyed += OnDestroyed;
        }

        private void OnDestroyed(Candy candy)
        {
            candy.Destroyed -= OnDestroyed;

            if (_candy == null)
                return;

            if (candy.Index == _candy.Index)
                _candy = null;
        }
    }
}
