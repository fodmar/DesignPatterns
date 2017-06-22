using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.ChainOfResponsibility
{
    public interface IChainElementsProvider<T>
    {
        IEnumerable<IChainElement<T>> GetElements();
    }
}
