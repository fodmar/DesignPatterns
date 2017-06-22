using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatterns.ChainOfResponsibility;
using NUnit.Framework;
using Rhino.Mocks;

namespace DesignPatterns.Tests.ChainOfResponsibility
{
    [TestFixture]
    class Tests
    {
        [Test]
        public void FirstElementCanDecide_OnlyFirstCalled()
        {
            // Arrange
            int someVal = 10;
            ChainDecision expected = ChainDecision.NotPassed;

            IChainOfResponsibility<int> chain = new ChainOfResponsibility<int>();

            var element1 = MockRepository.GenerateStub<IChainElement<int>>();
            var element2 = MockRepository.GenerateStub<IChainElement<int>>();
            var element3 = MockRepository.GenerateStub<IChainElement<int>>();

            element1.Stub(c => c.Process(someVal)).Return(expected);

            chain.Add(element1);
            chain.Add(element2);
            chain.Add(element3);

            // Act
            ChainDecision result = chain.Process(someVal);

            // Assert
            element1.AssertWasCalled(s => s.Process(someVal));
            element2.AssertWasNotCalled(s => s.Process(Arg<int>.Is.Anything));
            element3.AssertWasNotCalled(s => s.Process(Arg<int>.Is.Anything));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SecondElementCanDecide_FirstAndSecondCalled()
        {
            // Arrange
            int someVal = 10;
            ChainDecision expected = ChainDecision.Passed;

            IChainOfResponsibility<int> chain = new ChainOfResponsibility<int>();

            var element1 = MockRepository.GenerateStub<IChainElement<int>>();
            var element2 = MockRepository.GenerateStub<IChainElement<int>>();
            var element3 = MockRepository.GenerateStub<IChainElement<int>>();

            element1.Stub(c => c.Process(someVal)).Return(ChainDecision.CantSay);
            element2.Stub(c => c.Process(someVal)).Return(expected);

            chain.Add(element1);
            chain.Add(element2);
            chain.Add(element3);

            // Act
            ChainDecision result = chain.Process(someVal);

            // Assert
            element1.AssertWasCalled(s => s.Process(someVal));
            element2.AssertWasCalled(s => s.Process(someVal));
            element3.AssertWasNotCalled(s => s.Process(Arg<int>.Is.Anything));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AllElementsCantDecide_AllCalled()
        {
            // Arrange
            int someVal = 10;
            ChainDecision expected = ChainDecision.CantSay;

            var element1 = MockRepository.GenerateStub<IChainElement<int>>();
            var element2 = MockRepository.GenerateStub<IChainElement<int>>();
            var element3 = MockRepository.GenerateStub<IChainElement<int>>();

            element1.Stub(c => c.Process(someVal)).Return(ChainDecision.CantSay);
            element2.Stub(c => c.Process(someVal)).Return(ChainDecision.CantSay);
            element3.Stub(c => c.Process(someVal)).Return(ChainDecision.CantSay);

            var provider = MockRepository.GenerateStub<IChainElementsProvider<int>>();
            provider.Stub(c => c.GetElements()).Return(new[] { element1, element2, element3 });

            IChainOfResponsibility<int> chain = new ChainOfResponsibility<int>(provider);

            // Act
            ChainDecision result = chain.Process(someVal);

            // Assert
            element1.AssertWasCalled(s => s.Process(someVal));
            element2.AssertWasCalled(s => s.Process(someVal));
            element3.AssertWasCalled(s => s.Process(someVal));
            Assert.AreEqual(expected, result);
        }
    }
}
