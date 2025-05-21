using System;

namespace AsyncAwaitUtil
{
    public class SimpleCoroutineAwaiter<T> : IAwaitInstruction
    {
        bool _isDone;
        Exception _exception;
        T _result;

        public bool IsCompleted => _isDone;

        public T GetResult()
        {
            if (_exception != null) throw _exception;
            return _result;
        }

        public void Complete(T result, Exception e = null)
        {
            _isDone = true;
            _result = result;
            _exception = e;
        }
    }

    public interface IAwaitInstruction
    {
        bool IsCompleted { get; }
    }
}