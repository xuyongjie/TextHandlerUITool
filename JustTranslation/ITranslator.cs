using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustTranslation
{
    interface ITranslator
    {
        string Name { get;}
        string Translate(string origin);
    }
}
