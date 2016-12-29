using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITextHandle
{
    public interface IHandler
    {
        string Name { get; }
        string Handle(string resource);
    }
}
