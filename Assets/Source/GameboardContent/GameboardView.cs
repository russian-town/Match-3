using UnityEngine;

namespace Sourse.GameboardContent
{
    public class GameboardView : MonoBehaviour
    {
        public void Construct(Vector2 position)
            => transform.position = position;
    }
}
