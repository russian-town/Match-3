using UnityEngine;

namespace Sourse.Background
{
    public class BackgroundView : MonoBehaviour
    {
        public void Construct(Vector2 size)
        {
            transform.localScale = size;
        }
    }
}
