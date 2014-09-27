using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWPCore
{
    public class Item : GameObject
    {
        public Func<String, Item, WorkCompleted> DoWork;

        public Func<String, Item, Place, WorkCompleted> DoWorkWithPlace;
    }
}
