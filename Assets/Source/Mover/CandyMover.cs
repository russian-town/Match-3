using Source.Candies;
using UnityEngine;

namespace Source.Mover
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

        
    }
}
