using LIS.Extensions;
using LIS.Interface;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;

namespace LIS.Services
{
    public class LoadingIndicatorService : ILoadingIndicatorService
    {
        private Loader _loader;

        public void Subscribe(Action<EventPattern<EventArgs>> onLoad, Action onComplete)
        {
            // Dispose of the old loader, releasing the old subscribers.
            _loader?.Dispose();
            // Create a new loader.
            _loader = new Loader(onLoad, onComplete);
        }

        public IDisposable IsLoading()
        {
            return new Loading(_loader);
        }

        /// <summary>
        /// Provides the management of the events, observables, subscribers for the loading process.     
        /// Creates the observable and subscribes the actions passed in by the subscriber. 
        /// This registers the actions with the <seealso cref="_startedLoad"/> event.
        /// </summary>
        private class Loader : IDisposable
        {
            public bool IsDisposed = false;
            private event EventHandler<EventArgs> _startedLoad;
            private IObservable<EventPattern<EventArgs>> _startedLoadObservable;
            private IDisposable _loadSubscriber;

            public Loader(Action<EventPattern<EventArgs>> onLoad, Action onComplete)
            {
                
                _startedLoadObservable = Observable.FromEventPattern<EventHandler<EventArgs>, EventArgs>(r => _startedLoad += r, u => _startedLoad -= u)
                    .TakeWhile(_ => !IsDisposed);
                _loadSubscriber = _startedLoadObservable.Subscribe(onLoad, onComplete);
            }

            /// <summary>
            /// Invokes the <seealso cref="_startedLoad"/> event. This informs the subscriber to start the onLoad action.
            /// </summary>
            public void OnLoad()
            {
                _startedLoad?.Invoke(this, new EventArgs());
            }

            /// <summary>
            /// Disposes of the <seealso cref="Loader"/>. This ensures that observable knows to stop taking events. 
            /// This informs the observable that it has completed it's job, starting the onComplete action for the subscriber. 
            /// This notifies the subscriber that loading has ended.
            /// </summary>
            public void OnFinish()
            {
                Dispose();
            }

            public void Dispose()
            {
                // Disposes of the subscribes, releasing the events.
                IsDisposed = true;
                // Invoke to ensure OnComplete is called for observable;
                OnLoad();
                // Dispose of the subscriber to unload from event.
                _loadSubscriber?.Dispose();
            }
        }

        /// <summary>
        /// <seealso cref="IsLoading"/> returns this class. Providing this object to a using() statement gives the <seealso cref="Loader"/> the scope for the loading action. 
        /// When created, <seealso cref="Loader.OnLoad"/> is called letting the <seealso cref="Loader"/> know it can start the loading process. 
        /// When disposed of <seealso cref="Loader.OnFinish"/> is called, letting the <seealso cref="Loader"/> know it can start the finish process (disposal of the subscriber).
        /// </summary>
        private class Loading : IDisposable
        {
            private Loader _loader;
            public Loading(Loader loader)
            {
                _loader = loader;
                _loader.OnLoad();
            }

            public void Dispose()
            {
                // finishes and dereference the loader.
                _loader.OnFinish();
                _loader = null;
            }
        }
    }
}
