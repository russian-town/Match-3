using UnityEngine;

namespace Sourse.Candies
{
    public class CandyView : MonoBehaviour
    {
        public void Construct(Candy candy, Transform parent)
        {
            transform.position = candy.Position;
            transform.parent = parent;
        }
    }
}
