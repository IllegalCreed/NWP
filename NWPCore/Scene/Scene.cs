using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWPCore
{
    /// <summary>
    /// 场景基类
    /// </summary>
    public class Scene : GameObject
    {
        public Scene()
        {
            Places = new List<Place>();
        }

        public Func<Scene, WorkCompleted> BeforeWork;

        public Func<String, Scene, WorkCompleted> DoWork;

        public Func<Scene, WorkCompleted> AfterWork;

        public List<Place> Places;
    }
}
