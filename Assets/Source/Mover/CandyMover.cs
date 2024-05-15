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

        
    }
}
