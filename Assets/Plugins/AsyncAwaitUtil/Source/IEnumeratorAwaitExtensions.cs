using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace AsyncAwaitUtil
{
    public static class IEnumeratorAwaitExtensions
    {
        public static IEnumerator AwaitCoroutine(this IEnumerator enumerator)
        {
            while (enumerator.MoveNext())
            {
                if (enumerator.Current is UnityWebRequestAsyncOperation webOp)
                {
                    yield return webOp;

#if UNITY_2020_1_OR_NEWER
                    if (webOp.webRequest.result == UnityWebRequest.Result.ConnectionError || 
                        webOp.webRequest.result == UnityWebRequest.Result.ProtocolError)
#else
                    if (webOp.webRequest.isNetworkError || webOp.webRequest.isHttpError)
#endif
                    {
                        Debug.LogError("Web request failed: " + webOp.webRequest.error);
                    }
                }
                else if (enumerator.Current is AsyncOperation asyncOp)
                {
                    yield return asyncOp;
                }
                else
                {
                    yield return enumerator.Current;
                }
            }
        }

        public static SimpleCoroutineAwaiter<object> GetAwaiter(this IEnumerator enumerator)
        {
            var awaiter = new SimpleCoroutineAwaiter<object>();
            RunOnUnityScheduler(() => AsyncCoroutineRunner.Instance.StartCoroutine(
                AwaitCoroutine(enumerator, awaiter)));
            return awaiter;
        }

        private static IEnumerator AwaitCoroutine(
            IEnumerator enumerator,
            SimpleCoroutineAwaiter<object> awaiter)
        {
            yield return AwaitCoroutine(enumerator);
            awaiter.Complete(null, null);
        }

        public static SimpleCoroutineAwaiter<object> GetAwaiterWithReturnValue(
            this IEnumerator enumerator)
        {
            var awaiter = new SimpleCoroutineAwaiter<object>();
            RunOnUnityScheduler(() => AsyncCoroutineRunner.Instance.StartCoroutine(
                AwaitCoroutineWithReturnValue(enumerator, awaiter)));
            return awaiter;
        }

        private static IEnumerator AwaitCoroutineWithReturnValue(
            IEnumerator enumerator,
            SimpleCoroutineAwaiter<object> awaiter)
        {
            yield return AwaitCoroutine(enumerator);
            awaiter.Complete(enumerator.Current, null);
        }

        private static void RunOnUnityScheduler(Action action)
        {
            if (SynchronizationContextUtil.UnitySynchronizationContext == null)
                throw new Exception(
                    "UnitySynchronizationContext is not initialized. " +
                    "Please ensure you're calling this from the main Unity thread.");

            SynchronizationContextUtil.UnitySynchronizationContext.Post(_ => action(), null);
        }
    }
}