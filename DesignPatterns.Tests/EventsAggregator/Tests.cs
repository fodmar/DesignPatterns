using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatterns.EventsAggregator;
using NUnit.Framework;
using Rhino.Mocks;

namespace DesignPatterns.Tests.EventsAggregator
{
    [TestFixture]
    [Category("EventAggregator")]
    class Tests
    {
        [Test]
        public async void StringEvent_StringSubscribersCalled()
        {
            // Arrange
            string someString = "test";
            IEventAggregator eventAggregator = new EventAggregator();

            var subscriber1 = MockRepository.GenerateStub<ISubscriber<string>>();
            var subscriber2 = MockRepository.GenerateStub<ISubscriber<string>>();
            var subscriber3 = MockRepository.GenerateStub<ISubscriber<int>>();

            eventAggregator.Subscribe(subscriber1);
            eventAggregator.Subscribe(subscriber2);
            eventAggregator.Subscribe(subscriber3);

            // Act
            Feedback<string> feedback = await eventAggregator.Publish(someString);

            // Assert
            subscriber1.AssertWasCalled(s => s.Receive(someString));
            subscriber2.AssertWasCalled(s => s.Receive(someString));
            subscriber3.AssertWasNotCalled(s => s.Receive(Arg<int>.Is.Anything));
            Assert.That(feedback.Received);
            Assert.IsNull(feedback.Exception);
            Assert.AreEqual(feedback.EventObject, someString);
        }

        [Test]
        public async void AsyncPublish_StringSubscribersCalled()
        {
            // Arrange
            string someString = "test";
            IEventAggregator eventAggregator = new EventAggregator();

            var subscriber1 = MockRepository.GenerateStub<ISubscriber<string>>();
            var subscriber2 = MockRepository.GenerateStub<ISubscriber<string>>();
            var subscriber3 = MockRepository.GenerateStub<ISubscriber<int>>();

            eventAggregator.Subscribe(subscriber1);
            eventAggregator.Subscribe(subscriber2);
            eventAggregator.Subscribe(subscriber3);

            // Act
            Feedback<string> feedback = await eventAggregator.Publish(someString);

            // Assert
            subscriber1.AssertWasCalled(s => s.Receive(someString));
            subscriber2.AssertWasCalled(s => s.Receive(someString));
            subscriber3.AssertWasNotCalled(s => s.Receive(Arg<int>.Is.Anything));
            Assert.That(feedback.Received);
            Assert.IsNull(feedback.Exception);
            Assert.AreEqual(feedback.EventObject, someString);
        }

        [Test]
        public async void UnsubscribedFromEvent_NotCalled()
        {
            // Arrange
            string someString = "test";
            IEventAggregator eventAggregator = new EventAggregator();

            var subscriber1 = MockRepository.GenerateStub<ISubscriber<string>>();
            var subscriber2 = MockRepository.GenerateStub<ISubscriber<string>>();
            var subscriber3 = MockRepository.GenerateStub<ISubscriber<int>>();

            eventAggregator.Subscribe(subscriber1);
            eventAggregator.Subscribe(subscriber2);
            eventAggregator.Subscribe(subscriber3);

            // Act
            eventAggregator.Unsubscribe(subscriber2);
            eventAggregator.Unsubscribe(subscriber3);
            Feedback<string> feedback = await eventAggregator.Publish(someString);

            // Assert
            subscriber1.AssertWasCalled(s => s.Receive(someString));
            subscriber2.AssertWasNotCalled(s => s.Receive(Arg<string>.Is.Anything));
            subscriber3.AssertWasNotCalled(s => s.Receive(Arg<int>.Is.Anything));
            Assert.That(feedback.Received);
            Assert.IsNull(feedback.Exception);
            Assert.AreEqual(feedback.EventObject, someString);
        }

        [Test]
        public async void UnsubscriberThrownException_ExceptionIsHandled()
        {
            // Arrange
            string someString = "test";
            IEventAggregator eventAggregator = new EventAggregator();

            var subscriber1 = MockRepository.GenerateStub<ISubscriber<string>>();
            var subscriber2 = MockRepository.GenerateStub<ISubscriber<string>>();
            var subscriber3 = MockRepository.GenerateStub<ISubscriber<int>>();

            subscriber2.Stub(s => s.Receive(someString)).Throw(new Exception("test"));

            eventAggregator.Subscribe(subscriber1);
            eventAggregator.Subscribe(subscriber3);
            eventAggregator.Subscribe(subscriber2);

            // Act
            Feedback<string> feedback = await eventAggregator.Publish(someString);

            // Assert
            subscriber1.AssertWasCalled(s => s.Receive(someString));
            subscriber2.AssertWasCalled(s => s.Receive(someString));
            subscriber3.AssertWasNotCalled(s => s.Receive(Arg<int>.Is.Anything));
            Assert.That(feedback.Received);
            Assert.IsNotNull(feedback.Exception);
            Assert.AreEqual(feedback.EventObject, someString);
        }

        [Test]
        public async void SubscriberThrownExceptionAsync_ExceptionIsHandled()
        {
            // Arrange
            string someString = "test";
            IEventAggregator eventAggregator = new EventAggregator();

            var subscriber1 = MockRepository.GenerateStub<ISubscriber<string>>();
            var subscriber2 = MockRepository.GenerateStub<ISubscriber<string>>();
            var subscriber3 = MockRepository.GenerateStub<ISubscriber<int>>();

            subscriber2.Stub(s => s.Receive(someString)).Throw(new Exception("test"));

            eventAggregator.Subscribe(subscriber1);
            eventAggregator.Subscribe(subscriber3);
            eventAggregator.Subscribe(subscriber2);

            // Act
            Task<Feedback<string>> feedbackTask = eventAggregator.Publish(someString);

            feedbackTask.ContinueWith(t =>
            {
                Feedback<string> feedback = t.Result;

                // Assert
                subscriber1.AssertWasCalled(s => s.Receive(someString));
                subscriber2.AssertWasCalled(s => s.Receive(someString));
                subscriber3.AssertWasNotCalled(s => s.Receive(Arg<int>.Is.Anything));
                Assert.That(feedback.Received);
                Assert.IsNotNull(feedback.Exception);
                Assert.AreEqual(feedback.EventObject, someString);
            });
        }
    }
}
