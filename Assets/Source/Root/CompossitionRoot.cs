using Sourse.Spawners;
using UnityEngine;

namespace Sourse.Root
{
    public class CompossitionRoot : MonoBehaviour
    {
        private readonly BackgroundSizeChanger _backgroundSizeChanger = new ();
        private readonly BackgroundViewSpawners _backgroundViewSpawners = new ();

        [SerializeField] private BackgroundView _backgroundViewTemplate;
        [SerializeField] private Camera _camera;

        public void Start()
            => Initialize();

        private void Initialize()
        {
            Vector2 backgroundSize = _backgroundSizeChanger.GetSize(_camera);
            BackgroundView backgroundView = _backgroundViewSpawners.Get(_backgroundViewTemplate);
            backgroundView.Construct(backgroundSize);
        }
    }
}
