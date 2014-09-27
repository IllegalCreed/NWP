using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWPCore
{
    public class Place : GameObject
    {
        public Place()
        {
            IsHidden = false;
        }

        public string Scene;

        public bool IsHidden;

        public Func<String, Place, WorkCompleted> DoWork;
    }
}
