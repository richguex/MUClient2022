using System.Threading;
using UnityEngine;

namespace AsyncAwaitUtil
{
    public static class SynchronizationContextUtil
    {
        public static SynchronizationContext UnitySynchronizationContext;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Init()
        {
            UnitySynchronizationContext = SynchronizationContext.Current;
        }
    }
}