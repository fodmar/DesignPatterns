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
        public void StringEvent_StringSubscribersCalled()
        {
            // Arrange
            string someString = "test";
            IEventAggregator eventAggregator = new EventAggregator();

            var subscriber1 = MockRepository.GenerateStub<ISubscriber<string>>();
            var subscriber2 = MockRepository.GenerateStub<ISubscriber<string>>();
            var subscriber3 = MockRepository.GenerateStub<ISubscriber<int>>();

            eventAggregator.Subscribe<string>(subscriber1);
            eventAggregator.Subscribe<string>(subscriber2);
            eventAggregator.Subscribe<int>(subscriber3);

            // Act
            Feedback<string> feedback = eventAggregator.Publish<string>(someString);

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

            eventAggregator.Subscribe<string>(subscriber1);
            eventAggregator.Subscribe<string>(subscriber2);
            eventAggregator.Subscribe<int>(subscriber3);

            // Act
            Feedback<string> feedback = await eventAggregator.PublishAsync<string>(someString);

            // Assert
            subscriber1.AssertWasCalled(s => s.Receive(someString));
            subscriber2.AssertWasCalled(s => s.Receive(someString));
            subscriber3.AssertWasNotCalled(s => s.Receive(Arg<int>.Is.Anything));
            Assert.That(feedback.Received);
            Assert.IsNull(feedback.Exception);
            Assert.AreEqual(feedback.EventObject, someString);
        }

        [Test]
        public void TestEvent_SubscriberCanChangeEvent()
        {
            // Arrange
            TestEvent testEvent = new TestEvent();
            IEventAggregator eventAggregator = new EventAggregator();

            var subscriber = MockRepository.GenerateStub<ISubscriber<TestEvent>>();

            subscriber.Stub(s => s.Receive(testEvent)).Do(new Action<TestEvent>((e) => e.Passed = true));

            eventAggregator.Subscribe<TestEvent>(subscriber);

            // Act
            Feedback<TestEvent> feedback = eventAggregator.Publish<TestEvent>(testEvent);

            // Assert
            subscriber.AssertWasCalled(s => s.Receive(testEvent));
            Assert.That(feedback.Received);
            Assert.IsNull(feedback.Exception);
            Assert.IsTrue(testEvent.Passed);
            Assert.IsTrue(feedback.EventObject.Passed);
            Assert.AreSame(testEvent, feedback.EventObject);
        }

        [Test]
        public void UnsubscribedFromEvent_NotCalled()
        {
            // Arrange
            string someString = "test";
            IEventAggregator eventAggregator = new EventAggregator();

            var subscriber1 = MockRepository.GenerateStub<ISubscriber<string>>();
            var subscriber2 = MockRepository.GenerateStub<ISubscriber<string>>();
            var subscriber3 = MockRepository.GenerateStub<ISubscriber<int>>();

            eventAggregator.Subscribe<string>(subscriber1);
            eventAggregator.Subscribe<string>(subscriber2);
            eventAggregator.Subscribe<int>(subscriber3);

            // Act
            eventAggregator.Unsubscribe<string>(subscriber2);
            eventAggregator.Unsubscribe<int>(subscriber3);
            Feedback<string> feedback = eventAggregator.Publish<string>(someString);

            // Assert
            subscriber1.AssertWasCalled(s => s.Receive(someString));
            subscriber2.AssertWasNotCalled(s => s.Receive(Arg<string>.Is.Anything));
            subscriber3.AssertWasNotCalled(s => s.Receive(Arg<int>.Is.Anything));
            Assert.That(feedback.Received);
            Assert.IsNull(feedback.Exception);
            Assert.AreEqual(feedback.EventObject, someString);
        }

        [Test]
        public void UnsubscriberThrownException_ExceptionIsHandled()
        {
            // Arrange
            string someString = "test";
            IEventAggregator eventAggregator = new EventAggregator();

            var subscriber1 = MockRepository.GenerateStub<ISubscriber<string>>();
            var subscriber2 = MockRepository.GenerateStub<ISubscriber<string>>();
            var subscriber3 = MockRepository.GenerateStub<ISubscriber<int>>();

            subscriber2.Stub(s => s.Receive(someString)).Do(new Action<string>((s) => { throw new IndexOutOfRangeException(); }));

            eventAggregator.Subscribe<string>(subscriber1);
            eventAggregator.Subscribe<int>(subscriber3);
            eventAggregator.Subscribe<string>(subscriber2);

            // Act
            Feedback<string> feedback = eventAggregator.Publish<string>(someString);

            // Assert
            subscriber1.AssertWasCalled(s => s.Receive(someString));
            subscriber2.AssertWasCalled(s => s.Receive(someString));
            subscriber3.AssertWasNotCalled(s => s.Receive(Arg<int>.Is.Anything));
            Assert.That(feedback.Received);
            Assert.IsNotNull(feedback.Exception);
            Assert.AreEqual(feedback.EventObject, someString);
        }

        [Test]
        public void UnsubscriberThrownExceptionAsync_ExceptionIsHandled()
        {
            // Arrange
            string someString = "test";
            IEventAggregator eventAggregator = new EventAggregator();

            var subscriber1 = MockRepository.GenerateStub<ISubscriber<string>>();
            var subscriber2 = MockRepository.GenerateStub<ISubscriber<string>>();
            var subscriber3 = MockRepository.GenerateStub<ISubscriber<int>>();

            subscriber2.Stub(s => s.Receive(someString)).Do(new Action<string>((s) => { throw new IndexOutOfRangeException(); }));

            eventAggregator.Subscribe<string>(subscriber1);
            eventAggregator.Subscribe<int>(subscriber3);
            eventAggregator.Subscribe<string>(subscriber2);

            // Act
            Task<Feedback<string>> feedbackTask = eventAggregator.PublishAsync<string>(someString);

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
