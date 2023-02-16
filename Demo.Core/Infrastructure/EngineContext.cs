using System;
using System.Runtime.CompilerServices;

namespace Demo.Core.Infrastructure
{
    public class WebEngineContext
    {
        #region Methods

        /// <summary>
        /// Initializes a static instance of the Nop factory.
        /// </summary>
        /// <param name="forceRecreate">Creates a new factory instance even though the factory has been previously initialized.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IWebApplicationEngine Create(bool forceRecreate)
        {
            if (Singleton<IWebApplicationEngine>.Instance == null || forceRecreate)
            {
                Singleton<IWebApplicationEngine>.Instance = new WebApplicationEngine();
               
            }
            return Singleton<IWebApplicationEngine>.Instance;
        }

        /// <summary>
        /// Sets the static engine instance to the supplied engine. Use this method to supply your own engine implementation.
        /// </summary>
        /// <param name="engine">The engine to use.</param>
        /// <remarks>Only use this method if you know what you're doing.</remarks>
        public static void Replace(IWebApplicationEngine engine)
        {
            Singleton<IWebApplicationEngine>.Instance = engine;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton Nop engine used to access Nop services.
        /// </summary>
        public static IWebApplicationEngine Current
        {
            get
            {
                if (Singleton<IWebApplicationEngine>.Instance == null)
                {
                    throw new NullReferenceException("Engine not initialized!");
                }
                return Singleton<IWebApplicationEngine>.Instance;
            }
        }

        #endregion
    }
}
