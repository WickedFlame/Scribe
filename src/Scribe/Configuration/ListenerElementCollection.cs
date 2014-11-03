using System.Collections.Generic;
using System.Configuration;

namespace Scribe.Configuration
{
    [ConfigurationCollection(typeof(ListenerElement))]
    public class ListenerElementCollection : ConfigurationElementCollection, IEnumerable<ListenerElement>
    {
        private readonly List<ListenerElement> _elements;

        public ListenerElementCollection()
        {
            _elements = new List<ListenerElement>();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            var element = new ListenerElement();
            _elements.Add(element);

            return element;
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ListenerElement)element).Type;
        }

        public new IEnumerator<ListenerElement> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }
    }
}
