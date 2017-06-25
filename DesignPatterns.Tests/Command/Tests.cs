using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using DesignPatterns.Command;
using Rhino.Mocks;

namespace DesignPatterns.Tests.Command
{
    [TestFixture]
    [Category("Command")]
    class Tests
    {
        [Test]
        public void ExecuteCommands()
        {
            // Arrange
            string test = string.Empty;
            string expected = "TSET";
            IExecutor<string> executor = new Executor<string>(test);

            ICommand<string> append = new AppendTextCommand("test");
            ICommand<string> toUpper = new ToUpperCommand();
            ICommand<string> reverse = new ReverseCommand();

            // Act
            executor.Do(append);
            executor.Do(toUpper);
            executor.Do(reverse);

            // Assert
            Assert.AreEqual(expected, executor.Target);
        }

        [Test]
        public void UndoCommand()
        {
            // Arrange
            string test = string.Empty;
            string expected = "TEST";
            IExecutor<string> executor = new Executor<string>(test);

            ICommand<string> append = new AppendTextCommand("test");
            ICommand<string> toUpper = new ToUpperCommand();
            ICommand<string> reverse = new ReverseCommand();

            // Act
            executor.Do(append);
            executor.Do(toUpper);
            executor.Do(reverse);
            executor.Do(append);
            executor.Undo(typeof(AppendTextCommand));
            executor.Undo(typeof(ReverseCommand));

            // Assert
            Assert.AreEqual(expected, executor.Target);
        }

        [Test]
        public void UndoCommand_DontThrowException()
        {
            // Arrange
            string test = string.Empty;
            string expected = "test";
            IExecutor<string> executor = new Executor<string>(test);

            ICommand<string> append = new AppendTextCommand("test");

            // Act
            executor.Do(append);
            executor.Undo(typeof(ToUpperCommand));
            executor.Undo(typeof(ReverseCommand));

            // Assert
            Assert.AreEqual(expected, executor.Target);
        }

        [Test]
        public void RedoCommand()
        {
            // Arrange
            string test = string.Empty;
            string expected = "tset";
            IExecutor<string> executor = new Executor<string>(test);

            ICommand<string> append = new AppendTextCommand("test");
            ICommand<string> reverse = new ReverseCommand();

            // Act
            executor.Do(append);
            executor.Redo(typeof(AppendTextCommand));
            executor.Undo(typeof(AppendTextCommand));
            executor.Undo(typeof(ReverseCommand));
            executor.Undo(typeof(AppendTextCommand));
            executor.Redo(typeof(AppendTextCommand));
            executor.Do(reverse);

            // Assert
            Assert.AreEqual(expected, executor.Target);
        }
    }
}
