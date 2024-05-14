using Sourse.HUD.Input;
using UnityEngine;

public class TouchpadSpawner
{
    public Touchpad Get(Touchpad touchpadTemplate, RectTransform hud)
    {
        return Object.Instantiate(touchpadTemplate, Vector3.zero, Quaternion.identity, hud);
    }
}
