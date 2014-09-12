using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWPCore
{
    public class GameState : TaskBase
    {
        public GameState()
        {

        }

        public Func<String, WorkCompleted> Dowork;
    }

    public class WorkCompleted
    {
        public bool result;
        public string log;
        public string nextState;
    }

}
