using System;
using System.Collections;
using UnityEngine;

namespace _0_Game.Scripts.Tools.CoroutineHelper
{
    public class CoroutineHolder : MonoBehaviour, IDisposable
    {
        public static CoroutineHolder Make(bool dontDestroyOnLoad=true, string goName="[Coroutine]")
        {
            var go = new GameObject(goName);
            if(dontDestroyOnLoad)
                DontDestroyOnLoad(go);
            return go.AddComponent<CoroutineHolder>();
        }

        public IEnumerator WaitUntil(Func<bool> predicate, Action callback)
        {
            var coroutine = WaitUntilCoroutine(predicate, callback);
            StartCoroutine(coroutine);
            return coroutine;
        }

        private IEnumerator WaitUntilCoroutine(Func<bool> predicate, Action callback)
        {
            yield return new WaitUntil(predicate);
            callback();
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}