using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.ChainOfResponsibility
{
    public interface IChainOfResponsibility<T> : IChainElement<T>
    {
        void Add(IChainElement<T> chainPart);
    }
}
