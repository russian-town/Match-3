using System.Collections;
using UnityEngine;

namespace Source.Codebase.Services.Abstract
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator routine);
        void StopCoroutine(Coroutine coroutine);
    }
}