using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWPCore
{
    public class GameState : GameObject
    {
        public GameState()
        {

        }

        public Func<GameState, WorkCompleted> BeforeWork;

        public Func<String, GameState, WorkCompleted> DoWork;

        public Func<GameState, WorkCompleted> AfterWork;
    }

    public class WorkCompleted
    {
        public bool result;
        public string log;
    }

}
