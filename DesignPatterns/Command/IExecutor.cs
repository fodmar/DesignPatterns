using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Command
{
    public interface IExecutor<T>
    {
        T Target { get; }

        void Do(ICommand<T> command);
        void Undo(Type commandType);
        void Redo(Type commandType);
    }
}
