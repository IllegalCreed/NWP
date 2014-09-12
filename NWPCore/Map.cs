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

        public Dictionary<string, string> OtherMap;
    }
}
