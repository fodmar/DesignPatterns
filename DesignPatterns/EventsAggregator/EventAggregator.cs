using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.EventsAggregator
{
    public class EventAggregator : IEventAggregator
    {
        private Dictionary<Type, ArrayList> subscribers;

        public EventAggregator()
        {
            this.subscribers = new Dictionary<Type, ArrayList>();
        }

        public Feedback<T> Publish<T>(T eventObject)
        {
            Feedback<T> feedback = new Feedback<T>(eventObject);
            ArrayList subscribers;

            if (this.subscribers.TryGetValue(typeof(T), out subscribers))
	        {
                feedback.Received = true;

                try
                {
                    Parallel.For(0, subscribers.Count, (i) =>
                    {
                        var current = subscribers[i] as ISubscriber<T>;
                        current.Receive(eventObject);
                    });

                }
                catch (AggregateException ex)
                {
                    feedback.Exception = ex;
                }
	        }

            return feedback;
        }

        public Task<Feedback<T>> PublishAsync<T>(T eventObject)
        {
            return Task.Factory.StartNew<Feedback<T>>(() => this.Publish<T>(eventObject));
        }

        public void Subscribe<T>(ISubscriber<T> subscriber)
        {
            Type currentType = typeof(T);
            ArrayList typeSubscribers;

            if (!this.subscribers.TryGetValue(currentType, out typeSubscribers))
            {
                typeSubscribers = new ArrayList();
                this.subscribers[currentType] = typeSubscribers;
            }

            typeSubscribers.Add(subscriber);
        }

        public void Unsubscribe<T>(ISubscriber<T> subscriber)
        {
            Type currentType = typeof(T);
            ArrayList typeSubscribers;

            if (this.subscribers.TryGetValue(currentType, out typeSubscribers))
            {
                typeSubscribers.Remove(subscriber);

                if (typeSubscribers.Count == 0)
                {
                    this.subscribers.Remove(currentType);
                }
            }
        }
    }
}
