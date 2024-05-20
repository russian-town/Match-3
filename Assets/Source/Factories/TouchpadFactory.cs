using Source.HUD.Input;
using UnityEngine;

namespace Source.Factories
{
    public class TouchpadFactory
    {
        public Touchpad Get(Touchpad touchpadTemplate, RectTransform hud)
        {
            return Object.Instantiate(touchpadTemplate, Vector3.zero, Quaternion.identity, hud);
        }
    }
}
