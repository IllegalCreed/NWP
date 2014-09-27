using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWPCore
{
    public class GameManager
    {
        #region 构造
        public GameManager()
        {
            StatesDictionary = new Dictionary<string, GameState>();
            MapDictionary = new Dictionary<string, Scene>();
            ItemDictionary = new Dictionary<string, Item>();
            ItemNameToIDDictionary = new Dictionary<string, string>();
            Player = new Player(this);
        }
        #endregion

        #region 属性
        public Dictionary<string, GameState> StatesDictionary;
        private string m_CurrentState;
        public string CurrentState
        {
            get
            {
                return m_CurrentState;
            }
            set
            {
                if (value != m_CurrentState)
                {
                    if (m_CurrentState != null && StatesDictionary.ContainsKey(m_CurrentState))
                    {
                        WorkCompleted result;
                        if (StatesDictionary[m_CurrentState].AfterWork != null)
                        {
                            result = StatesDictionary[m_CurrentState].AfterWork(StatesDictionary[m_CurrentState]);
                            if (WorkCompleted != null)
                            {
                                WorkCompleted(result);
                            }
                        }
                    }

                    m_CurrentState = value;

                    if (m_CurrentState != null && StatesDictionary.ContainsKey(m_CurrentState))
                    {
                        WorkCompleted result;
                        if (StatesDictionary[m_CurrentState].BeforeWork != null)
                        {
                            result = StatesDictionary[m_CurrentState].BeforeWork(StatesDictionary[m_CurrentState]);
                            if (WorkCompleted != null)
                            {
                                WorkCompleted(result);
                            }
                        }
                    }
                }
            }
        }

        public Dictionary<string, Scene> MapDictionary;
        private string m_CurrentMap;
        public string CurrentMap
        {
            get
            {
                return m_CurrentMap;
            }
            set
            {
                if (value != m_CurrentMap)
                {
                    if (m_CurrentMap != null && MapDictionary.ContainsKey(m_CurrentMap))
                    {
                        WorkCompleted result;
                        if (MapDictionary[m_CurrentMap].AfterWork != null)
                        {
                            result = MapDictionary[m_CurrentMap].AfterWork(MapDictionary[m_CurrentMap]);
                            if (WorkCompleted != null)
                            {
                                WorkCompleted(result);
                            }
                        }
                    }

                    m_CurrentMap = value;

                    if (m_CurrentMap != null && MapDictionary.ContainsKey(m_CurrentMap))
                    {
                        WorkCompleted result;
                        if (MapDictionary[m_CurrentMap].BeforeWork != null)
                        {
                            result = MapDictionary[m_CurrentMap].BeforeWork(MapDictionary[m_CurrentMap]);
                            if (WorkCompleted != null)
                            {
                                WorkCompleted(result);
                            }
                        }
                    }
                }
            }
        }

        public Dictionary<string, Item> ItemDictionary;
        public Dictionary<string, string> ItemNameToIDDictionary;

        public Player Player;
        public event Action<WorkCompleted> WorkCompleted;
        #endregion

        #region 方法
        public void Excute(string command)
        {
            if (m_CurrentState != null && StatesDictionary.ContainsKey(m_CurrentState))
            {
                WorkCompleted result;
                if (StatesDictionary[m_CurrentState].DoWork != null)
                {
                    result = StatesDictionary[m_CurrentState].DoWork(command, StatesDictionary[m_CurrentState]);
                    if (WorkCompleted != null)
                    {
                        WorkCompleted(result);
                    }
                }
            }
        }
        #endregion
    }
}
