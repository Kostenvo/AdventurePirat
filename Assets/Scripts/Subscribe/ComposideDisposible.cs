using System;
using System.Collections.Generic;


namespace Subscribe
{
    public class ComposideDisposible : IDisposable
    {
        private List<IDisposable> disposers = new List<IDisposable>();

        public void Retain(IDisposable disposer)
        {
            disposers.Add(disposer);
        }


        public void Dispose()
        {
            foreach (var disposer in disposers)
            {
                disposer.Dispose();
            }

            disposers.Clear();
        }
    }
}