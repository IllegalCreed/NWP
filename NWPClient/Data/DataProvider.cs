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
<<<<<<< HEAD
=======
            InitMap();
>>>>>>> origin/master
        }
        public static DataProvider Instence
        {
            get { return m_Instence; }
        }

        public GameManager GM;

<<<<<<< HEAD
=======
        private void InitMap()
        {
            #region 实验室
            {
                Map Lab = new Map();
                Lab.ID = "Lab";
                Lab.Name = "实验室";
                Lab.Description = "你现在处于怪博士的实验室，墙壁四周都是斑驳的血迹，手术台上还有刚刚用过的手术器械，看起开还没来得急清洗，实验室的大门微掩着！";
                Lab.OtherMap.Add("大门", "SittingRoom");
                Lab.OtherMap.Add("客厅", "SittingRoom");
                GM.MapDictionary.Add("Lab", Lab);
            }
            #endregion

            #region 客厅
            {
                Map SittingRoom = new Map();
                SittingRoom.ID = "SittingRoom";
                SittingRoom.Name = "客厅";
                SittingRoom.Description = "你现在处于怪博士的客厅，光线昏暗客厅里面挂着神秘的风景画，角落里散落着一件白大褂，左手边有一扇木门，右手边有一扇玻璃的推拉门！";
                SittingRoom.OtherMap.Add("木门", "BedRoom");
                SittingRoom.OtherMap.Add("卧室", "BedRoom");
                SittingRoom.OtherMap.Add("推拉门", "WashRoom");
                SittingRoom.OtherMap.Add("厕所", "WashRoom");
                SittingRoom.OtherMap.Add("实验室", "Lab");
                GM.MapDictionary.Add("SittingRoom", SittingRoom);
            }
            #endregion

            #region 卧室
            {
                Map BedRoom = new Map();
                BedRoom.ID = "SittingRoom";
                BedRoom.Name = "卧室";
                BedRoom.Description = "你现在处于怪博士的卧室，窗户完全封死了，床上爬过几只蟑螂，床单早就泛黄了，几件破衣服散落一地，看起来没有其他的路！";
                BedRoom.OtherMap.Add("木门", "SittingRoom");
                BedRoom.OtherMap.Add("客厅", "SittingRoom");
                GM.MapDictionary.Add("BedRoom", BedRoom);
            }
            #endregion

            #region 厕所
            {
                Map WashRoom = new Map();
                WashRoom.ID = "WashRoom";
                WashRoom.Name = "厕所";
                WashRoom.Description = "你现在处于怪博士的厕所，气味实在难闻，马桶的座圈碎了一地，洗手池里面是一些血迹和疑似器官的东西，看似也没有其他路了！";
                WashRoom.OtherMap.Add("马桶", "Out");
                WashRoom.OtherMap.Add("推拉门", "SittingRoom");
                WashRoom.OtherMap.Add("客厅", "SittingRoom");
                GM.MapDictionary.Add("WashRoom", WashRoom);
            }
            #endregion

            #region 外界
            {
                Map Out = new Map();
                Out.ID = "Out";
                Out.Name = "出口";
                Out.Description = "你爬进了马桶，意外的发现这里面其实是一个隧道，你顺着地道一直爬最后终于从一个井盖出来了，恭喜你！通关了！";
                GM.MapDictionary.Add("Out", Out);
            }
            #endregion
        }

>>>>>>> origin/master
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
<<<<<<< HEAD
=======

            #region 人物移动
            {
                GameState Moving = new GameState();
                Moving.ID = "Moving";
                Moving.Name = "人物移动";
                Moving.Description = "";
                Moving.Dowork += MovingExcute;
                GM.StatesDictionary.Add("Moving", Moving);
            }
            #endregion
>>>>>>> origin/master
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
<<<<<<< HEAD
=======
                case 2:
                    result.result = true;
                    result.nextState = "Moving";
                    GM.CurrentMap = "Lab";
                    break;
>>>>>>> origin/master
                default:
                    break;
            }
            introTick++;
            return result;
        }
<<<<<<< HEAD
=======

        private WorkCompleted MovingExcute(string command)
        {
            WorkCompleted result = new WorkCompleted();

            if (GM.CurrentMap == "Out")
            {
                result.result = true;
                return result;
            }

            string[] cmds = command.Split('/');
            string cmd = cmds[0];

            switch(cmd)
            {
                case "moveto":
                    if (cmds.Length > 1)
                    {
                        if (GM.MapDictionary[GM.CurrentMap].OtherMap.ContainsKey(cmds[1]))
                        {
                            GM.CurrentMap = GM.MapDictionary[GM.CurrentMap].OtherMap[cmds[1]];
                        }
                        else
                        {
                            result.result = false;
                            result.log = "指定地点无效";
                        }
                    }
                    break;
                default:
                    result.result = false;
                    result.log = "请输入“moveto/地点”来进行移动";
                    break;
            }

            return result;
        }
>>>>>>> origin/master
    }
}
