using System;
using Source.Codebase.Domain;
using UnityEngine;

namespace Source.Codebase.Controllers.Inputs
{
    public interface ITouchpad
    {
        event Action<Vector2> TouchStarted;
        event Action<Vector2> TouchEnded;
        Direction LastTouchDirection { get; }
    }
}