using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribble
{
    public interface ILogProvider
    {
        void Initialize(Logger logger);
    }
}
