using UnityEngine;

namespace Sourse.Candies
{
    public class CandyView : MonoBehaviour
    {
        public void Construct(Vector2 position)
            => transform.position = position;
    }
}
