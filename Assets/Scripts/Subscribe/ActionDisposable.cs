using System;
using UnityEngine.Rendering.RenderGraphModule;

namespace Subscribe
{
    public class ActionDisposable : IDisposable
    {
        private Action _action;

        public ActionDisposable(Action action)
        {
            _action = action;
        }
        
        public void Dispose()
        {
            _action?.Invoke();      
        }
    }
}