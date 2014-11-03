using System.Configuration;

namespace Scribe.Configuration
{
    public class ScribeSection : ConfigurationSection
    {
        [ConfigurationProperty("writers", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(LoggerElement), AddItemName = "add")]
        public LoggerElementCollection Writers
        {
            get
            {
                return (LoggerElementCollection)this["writers"];
            }
        }

        [ConfigurationProperty("listeners", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(ListenerElement), AddItemName = "add")]
        public ListenerElementCollection Listeners
        {
            get
            {
                return (ListenerElementCollection)this["listeners"];
            }
        }
    }
}
