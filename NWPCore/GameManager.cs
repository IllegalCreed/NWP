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
            Player = new Player();
        }
        #endregion

        #region 属性
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
                    m_CurrentState = value;
                    GameStateChanged(StatesDictionary[m_CurrentState]);
                }
            }
        }

        public Dictionary<string, GameState> StatesDictionary;

        public Player Player;

        public event Action<GameState> GameStateChanged;
        #endregion

        #region 方法
        public WorkCompleted Excute(string command)
        {
            WorkCompleted result = null;

            if(StatesDictionary.ContainsKey(CurrentState))
            {
                GameState currentstate = StatesDictionary[CurrentState];
                result = currentstate.Dowork(command);
            }

            return result;
        }
        #endregion
    }

    //public enum GameState
    //{
    //    Beginning,
    //    MainMenu,
    //    CreatPlayer,
    //    Intro
    //}
}
