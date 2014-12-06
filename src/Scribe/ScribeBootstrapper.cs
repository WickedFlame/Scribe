
namespace Scribe
{
    public class ScribeBootstrapper
    {
        public void Initialize()
        {
            // creating an instance automaticaly initializes Scribe
            var manager = new LogManager();
            manager.Initialize();
        }
    }
}
