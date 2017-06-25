using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Command
{
    public class Executor<T> : IExecutor<T>
    {
        private Dictionary<Type, Stack<ICommand<T>>> undo;
        private Dictionary<Type, Stack<ICommand<T>>> redo;

        public T Target { get; private set; }

        public Executor(T target)
        {
            this.Target = target;
            this.undo = new Dictionary<Type, Stack<ICommand<T>>>();
            this.redo = new Dictionary<Type, Stack<ICommand<T>>>();
        }

        public void Do(ICommand<T> command)
        {
            T temp = this.Target;
            command.Do(ref temp);
            this.Target = temp;

            this.AddToStack(command, this.undo);
        }

        public void Undo(Type commandType)
        {
            ICommand<T> command = this.MoveFromStackToStack(commandType, this.undo, this.redo);

            if (command != null)
            {
                T temp = this.Target;
                command.Undo(ref temp);
                this.Target = temp;
            }
        }

        public void Redo(Type commandType)
        {
            ICommand<T> command = this.MoveFromStackToStack(commandType, this.redo, this.undo);

            if (command != null)
            {
                T temp = this.Target;
                command.Do(ref temp);
                this.Target = temp;
            }
        }

        private void AddToStack(ICommand<T> command, Dictionary<Type, Stack<ICommand<T>>> stacks)
        {
            Type commandType = command.GetType();
            Stack<ICommand<T>> stack;

            if (!stacks.TryGetValue(commandType, out stack))
            {
                stack = new Stack<ICommand<T>>();
                stacks[commandType] = stack;
            }

            stack.Push(command);
        }

        private ICommand<T> MoveFromStackToStack(
            Type commandType,
            Dictionary<Type, Stack<ICommand<T>>> from,
            Dictionary<Type, Stack<ICommand<T>>> to)
        {
            Stack<ICommand<T>> commandStack;
            ICommand<T> command = null;

            if (from.TryGetValue(commandType, out commandStack))
            {
                command = commandStack.Pop();

                if (commandStack.Count == 0)
                {
                    from.Remove(commandType);
                }

                this.AddToStack(command, to);
            }

            return command;
        }
    }
}
