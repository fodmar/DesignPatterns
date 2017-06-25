using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatterns.Command;

namespace DesignPatterns.Tests.Command
{
    class AppendTextCommand : ICommand<string>
    {
        private string toAppend;

        public AppendTextCommand(string toAppend)
        {
            this.toAppend = toAppend;
        }

        public void Do(ref string target)
        {
            target += this.toAppend;
        }

        public void Undo(ref string target)
        {
            target = target.Substring(0, target.Length - this.toAppend.Length);
        }
    }
}
