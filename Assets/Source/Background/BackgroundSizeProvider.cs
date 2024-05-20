using UnityEngine;

namespace Source.Background
{
    public class BackgroundSizeProvider
    {
        private readonly float _scaleFactor = 2f;

        public Vector2 GetSize(Camera camera)
        {
            float height = camera.orthographicSize * _scaleFactor;
            float width = height * Screen.width / Screen.height;

            return new Vector2(width, height);
        }
    }
}
