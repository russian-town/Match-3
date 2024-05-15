using Sourse.HUD.Input;
using UnityEngine;

namespace Sourse.Factories
{
    public class TouchpadFactory
    {
        public Touchpad Get(Touchpad touchpadTemplate, RectTransform hud)
        {
            return Object.Instantiate(touchpadTemplate, Vector3.zero, Quaternion.identity, hud);
        }
    }
}
