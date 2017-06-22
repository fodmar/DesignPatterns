using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.ChainOfResponsibility
{
    public class ChainOfResponsibility<T> : IChainOfResponsibility<T>
    {
        private List<IChainElement<T>> chain;

        public ChainOfResponsibility(IChainElementsProvider<T> provider)
        {
            this.chain = new List<IChainElement<T>>(provider.GetElements());
        }

        public ChainOfResponsibility()
        {
            this.chain = new List<IChainElement<T>>();
        }

        public ChainDecision Process(T item)
        {
            foreach (var chainElement in this.chain)
            {
                ChainDecision decision = chainElement.Process(item);

                if (decision != ChainDecision.CantSay)
                {
                    return decision;
                }
            }

            return ChainDecision.CantSay;
        }

        public void Add(IChainElement<T> chainPart)
        {
            this.chain.Add(chainPart);
        }
    }
}
