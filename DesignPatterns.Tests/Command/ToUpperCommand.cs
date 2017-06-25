using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatterns.Command;

namespace DesignPatterns.Tests.Command
{
    class ToUpperCommand : ICommand<string>
    {
        public void Do(ref string target)
        {
            target = target.ToUpper();
        }

        public void Undo(ref string target)
        {
            target = target.ToLower();
        }
    }
}
