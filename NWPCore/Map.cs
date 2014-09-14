using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWPCore
{
    public class Map : SceneBase
    {
        public Map()
        {
            OtherMap = new Dictionary<string, string>();
        }

        public Func<Map, WorkCompleted> BeforeWork;

        public Func<String, Map, WorkCompleted> DoWork;

        public Func<Map, WorkCompleted> AfterWork;

        public bool IsFound;

        public Dictionary<string, string> OtherMap;
    }
}
