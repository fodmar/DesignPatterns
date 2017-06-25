using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Command
{
    public interface ICommand<T>
    {
        void Do(ref T target);
        void Undo(ref T target);
    }
}
