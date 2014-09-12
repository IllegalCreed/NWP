using NWPCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NWPClient.Data
{
    public class DataProvider
    {
        public static readonly DataProvider m_Instence = new DataProvider();
        private DataProvider()
        {
            GM = new GameManager();
            InitGameState();
        }
        public static DataProvider Instence
        {
            get { return m_Instence; }
        }

        public GameManager GM;

        private void InitGameState()
        {
            #region 主菜单
            {
                GameState MainMenu = new GameState();
                MainMenu.ID = "MainMenu";
                MainMenu.Name = "主菜单";
                MainMenu.Description = "科科历险记\r\n\r\n开始游戏(COMMAND=start)\r\n退出游戏(COMMAND=exit)";
                MainMenu.Dowork += MainMenuExcute;
                GM.StatesDictionary.Add("MainMenu", MainMenu);
            }
            #endregion

            #region 创建角色
            {
                GameState CreatPlayer = new GameState();
                CreatPlayer.ID = "CreatePlayer";
                CreatPlayer.Name = "创建角色";
                CreatPlayer.Description = "欢迎来到科科的世界\r\n首先你要为你的角色起一个名字";
                CreatPlayer.Dowork += CreatPlayerExcute;
                GM.StatesDictionary.Add("CreatePlayer", CreatPlayer);
            }
            #endregion

            #region 开场白
            {
                GameState Intro = new GameState();
                Intro.ID = "Intro";
                Intro.Name = "开场白";
                Intro.Description = "你好" + GM.Player.Name + "\r\n我是游戏引导员，这里是怪博士的实验室，你需要想办法逃出去";
                Intro.Dowork += IntroExcute;
                GM.StatesDictionary.Add("Intro", Intro);
            }
            #endregion
        }

        private WorkCompleted MainMenuExcute(string command)
        {
            WorkCompleted result = new WorkCompleted();
            switch (command)
            {
                case "start":
                    result.result = true;
                    result.nextState = "CreatePlayer";
                    break;
                case "exit":
                    Application.Current.Shutdown();
                    break;
                default:
                    result.result = false;
                    result.log = "指令错误";
                    break;
            }
            return result;
        }

        private WorkCompleted CreatPlayerExcute(string command)
        {
            WorkCompleted result = new WorkCompleted();
            switch (command)
            {
                case "":
                    result.result = false;
                    result.log = "姓名不能为空";
                    break;
                default:
                    GM.Player.Name = command;
                    result.result = true;
                    result.log = "角色" + GM.Player.Name + "创建成功!";
                    result.nextState = "Intro";
                    break;
            }
            return result;
        }

        private int introTick = 0;
        private WorkCompleted IntroExcute(string command)
        {
            WorkCompleted result = new WorkCompleted();
            switch (introTick)
            {
                case 0:
                    result.result = true;
                    result.log = "这个实验室由若干个房间组成，如果你能顺利的找到出去的路则可以活下来";
                    break;
                case 1:
                    result.result = true;
                    result.log = "如果你在怪博士回来之前还没能逃出去则游戏结束，现在，游戏开始！";
                    break;
                default:
                    break;
            }
            introTick++;
            return result;
        }
    }
}
