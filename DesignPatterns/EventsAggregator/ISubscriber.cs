using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.EventsAggregator
{
    public interface ISubscriber<T>
    {
        void Receive(T eventObject);
    }
}
