using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.EventsAggregator
{
    public interface IEventAggregator
    {
        Feedback<T> Publish<T>(T eventObject);

        Task<Feedback<T>> PublishAsync<T>(T eventObject);
        
        void Subscribe<T>(ISubscriber<T> subscriber);

        void Unsubscribe<T>(ISubscriber<T> subscriber);
    }
}
