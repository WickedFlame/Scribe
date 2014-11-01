using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribe
{
    public interface IListner
    {
        void Initialize(ILoggerFactory loggerFactory);
    }
}
