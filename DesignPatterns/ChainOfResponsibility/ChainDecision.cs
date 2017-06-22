using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.ChainOfResponsibility
{
    public enum ChainDecision
    {
        Passed = 0,
        NotPassed = 1,
        CantSay = 2
    }
}
