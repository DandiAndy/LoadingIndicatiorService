using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;

namespace LIS.Interface
{
    public interface ILoadingIndicatorService
    {
        /// <summary>
        /// A single page can be subscribed to this service at a time. 
        /// Subscribing removes the old <seealso cref="Loader"/> and creates a new one. 
        /// The <seealso cref="Loader"/> will hold onto the actions for starting a load and completing a load.  
        /// </summary>
        /// <param name="onLoad">The action that should occur when the loading process begins. e.g. overlay with a activity indicator. </param>
        /// <param name="onComplete">The action that should occur when the loading process ends. e.g. remove the activity indicator overlay.</param>
        void Subscribe(Action<EventPattern<EventArgs>> onLoad, Action onComplete);

        /// <summary>
        /// Used in tandem with a using() statement to provide the subscriber with the scope of the loading process.
        /// When called, the loading process begins. The subscriber will begin the onLoad action.
        /// When the returned <seealso cref="IDisposable"/> is disposed of, the loading process is completed. The subscriber will begin the onComplete action.
        /// </summary>
        /// <returns>The IDisposable representative of the loading process scope. When created, loading starts. When disposed, loading completes.</returns>
        IDisposable IsLoading();
    }
}
