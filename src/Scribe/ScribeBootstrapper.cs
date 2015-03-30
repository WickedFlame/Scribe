
namespace Scribe
{
    public class ScribeBootstrapper
    {
        public void Initialize()
        {
            BeforeInitialize();

            // creating an instance automaticaly initializes Scribe
            var manager = new LogManager();
            manager.Initialize();

            OnStartup();
        }

        /// <summary>
        /// Virtual method that gets called befor the initialization is started
        /// </summary>
        protected virtual void BeforeInitialize() { }

        /// <summary>
        /// Virtual method that gets called when Scribe is started
        /// </summary>
        protected virtual void OnStartup() { }
    }
}
