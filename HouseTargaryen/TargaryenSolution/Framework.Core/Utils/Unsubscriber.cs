namespace Framework.Core.Utils
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The class to provide the observable with un-subscription logic.
    /// </summary>
    /// <typeparam name="T">The type of the data exchange object.
    /// </typeparam>
    internal class Unsubscriber<T> : IDisposable
    {
        private readonly IObserver<T> observer;

        private readonly List<IObserver<T>> observers;

        public Unsubscriber(IList<IObserver<T>> observers, IObserver<T> observer)
        {
            this.observers = observers == null ? new List<IObserver<T>>() : new List<IObserver<T>>(observers);
            this.observer = observer;
        }

        /// <summary>
        /// Pops up the subscriber to unsubscribe it.
        /// </summary>
        public void Dispose()
        {
            if (this.observer != null && this.observers.Contains(this.observer))
            {
                this.observers.Remove(this.observer);
            }
        }
    }
}