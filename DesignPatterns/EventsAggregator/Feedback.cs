using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.EventsAggregator
{
    public class Feedback<T>
    {
        public Feedback(T eventObject)
        {
            this.EventObject = eventObject;
        }

        public T EventObject { get; set; }

        public bool Received { get; set; }

        public AggregateException Exception { get; set; }
    }
}
