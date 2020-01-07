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
    }
}
