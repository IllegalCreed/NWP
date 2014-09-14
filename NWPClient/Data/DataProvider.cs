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
            InitMap();
        }
        public static DataProvider Instence
        {
            get { return m_Instence; }
        }

        public GameManager GM;

        private void InitMap()
        {
            #region 实验室
            {
                Map Lab = new Map();
                Lab.ID = "Lab";
                Lab.Name = "实验室";
                Lab.Description = "你现在处于怪博士的实验室，墙壁四周都是斑驳的血迹，手术台上还有刚刚用过的手术器械，看起开还没来得急清洗，实验室的大门微掩着！";
                Lab.BeforeWork += LabBefore;
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
                SittingRoom.BeforeWork += SittingRoomBefore;
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
                BedRoom.BeforeWork += BedRoomBefore;
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
                WashRoom.BeforeWork += WashRoomBefore;
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
                Out.BeforeWork += OutBefore;
                Out.DoWork += OutDoing;
                GM.MapDictionary.Add("Out", Out);
            }
            #endregion
        }

        private void InitGameState()
        {
            #region 主菜单
            {
                GameState MainMenu = new GameState();
                MainMenu.ID = "MainMenu";
                MainMenu.Name = "主菜单";
                MainMenu.Description = "科科历险记\r\n\r\n开始游戏(COMMAND=START)\r\n退出游戏(COMMAND=EXIT)";
                MainMenu.BeforeWork += MainMenuBefore;
                MainMenu.DoWork += MainMenuDoing;
                GM.StatesDictionary.Add("MainMenu", MainMenu);
            }
            #endregion

            #region 创建角色
            {
                GameState CreatPlayer = new GameState();
                CreatPlayer.ID = "CreatePlayer";
                CreatPlayer.Name = "创建角色";
                CreatPlayer.Description = "欢迎来到科科的世界\r\n首先你要为你的角色起一个名字";
                CreatPlayer.BeforeWork += CreatPlayerBefore;
                CreatPlayer.DoWork += CreatPlayerDoing;
                CreatPlayer.AfterWork += CreatPlayerAfter;
                GM.StatesDictionary.Add("CreatePlayer", CreatPlayer);
            }
            #endregion

            #region 开场白
            {
                GameState Intro = new GameState();
                Intro.ID = "Intro";
                Intro.Name = "开场白";
                Intro.Description = "你好" + GM.Player.Name + "\r\n我是游戏引导员，这里是怪博士的实验室，你需要想办法逃出去";
                Intro.BeforeWork += IntroBefore;
                Intro.DoWork += IntroDoing;
                Intro.AfterWork += IntroAfter;
                GM.StatesDictionary.Add("Intro", Intro);
            }
            #endregion

            #region 移动状态
            {
                GameState Moving = new GameState();
                Moving.ID = "Moving";
                Moving.Name = "移动状态";
                Moving.Description = "你可以控制你的角色了";
                Moving.DoWork += MovingDoing;
                GM.StatesDictionary.Add("Moving", Moving);
            }
            #endregion

            #region 游戏结束
            {
                GameState GameOver = new GameState();
                GameOver.ID = "GameOver";
                GameOver.Name = "游戏结束";
                GameOver.Description = "游戏结束，是否返回主菜单\r\nYES/NO";
                GameOver.BeforeWork += GameOverBefore;
                GameOver.DoWork += GameOverDoing;
                GM.StatesDictionary.Add("GameOver", GameOver);
            }
            #endregion
        }

        #region 游戏状态脚本
        #region 主菜单脚本
        private WorkCompleted MainMenuBefore(GameState GS)
        {
            WorkCompleted result = new WorkCompleted();

            result.result = true;
            result.log = GS.Description;

            return result;
        }

        private WorkCompleted MainMenuDoing(string command, GameState GS)
        {
            WorkCompleted result = new WorkCompleted();
            switch (command)
            {
                case "START":
                    GM.CurrentState = "CreatePlayer";
                    break;
                case "EXIT":
                    Application.Current.Shutdown();
                    break;
                default:
                    result.result = false;
                    result.log = "指令错误";
                    break;
            }
            return result;
        }
        #endregion

        #region 创建角色脚本
        private WorkCompleted CreatPlayerBefore(GameState GS)
        {
            WorkCompleted result = new WorkCompleted();

            result.result = true;
            result.log = GS.Description;

            return result;
        }

        private WorkCompleted CreatPlayerDoing(string command, GameState GS)
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
                    GM.CurrentState = "Intro";
                    break;
            }
            return result;
        }

        private WorkCompleted CreatPlayerAfter(GameState GS)
        {
            WorkCompleted result = new WorkCompleted();

            result.result = true;
            result.log = "角色" + GM.Player.Name + "创建成功!";

            return result;
        }
        #endregion

        #region 开场白脚本
        private WorkCompleted IntroBefore(GameState GS)
        {
            WorkCompleted result = new WorkCompleted();

            result.result = true;
            result.log = GS.Description;

            GS.Data = 0;

            return result;
        }

        private WorkCompleted IntroDoing(string command, GameState GS)
        {
            WorkCompleted result = new WorkCompleted();

            int introTick = (int)GS.Data;

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
                case 2:
                    GM.CurrentState = "Moving";
                    break;
                default:
                    break;
            }

            GS.Data = introTick+1;
            return result;
        }

        private WorkCompleted IntroAfter(GameState GS)
        {
            WorkCompleted result = new WorkCompleted();

            GM.CurrentMap = "Lab";
            GM.MapDictionary[GM.CurrentMap].IsFound = true;

            return result;
        }
        #endregion

        #region 移动状态脚本
        private WorkCompleted MovingDoing(string command, GameState GS)
        {
            WorkCompleted result = new WorkCompleted();

            string[] cmds = command.Split('/');
            string cmd = cmds[0];

            switch (cmd)
            {
                case "MOVE":
                    if (cmds.Length > 1)
                    {
                        if (GM.MapDictionary[GM.CurrentMap].OtherMap.ContainsKey(cmds[1]))
                        {
                            GM.CurrentMap = GM.MapDictionary[GM.CurrentMap].OtherMap[cmds[1]];
                            GM.MapDictionary[GM.CurrentMap].IsFound = true;
                        }
                        else
                        {
                            result.result = false;
                            result.log = "指定地点无效";
                        }
                    }
                    break;
                case "MAP":
                    bool hasFound = false;
                    foreach (KeyValuePair<string, string> other in GM.MapDictionary[GM.CurrentMap].OtherMap)
                    {
                        if (GM.MapDictionary[other.Value].IsFound == true)
                        {
                            hasFound = true;
                            result.log += ("\r\n" + other.Key);
                        }
                    }

                    if (hasFound)
                    {
                        result.result = true;
                        result.log = "已探索的地点:" + result.log;
                    }
                    else
                    {
                        result.result = false;
                        result.log = "您还未探索任何地点";
                    }
                    break;
                default:
                    if (GM.MapDictionary[GM.CurrentMap].DoWork != null)
                    {
                        result = GM.MapDictionary[GM.CurrentMap].DoWork(command, GM.MapDictionary[GM.CurrentMap]);
                    }
                    else
                    {
                        result.result = false;
                        result.log = "请输入“MOVE/地点”来进行移动\r\n输入MAP查看已探索的地点";
                    }
                    break;
            }

            return result;
        }
        #endregion

        #region 游戏结束脚本
        private WorkCompleted GameOverBefore(GameState GS)
        {
            WorkCompleted result = new WorkCompleted();

            result.result = true;
            result.log = GS.Description;

            return result;
        }

        private WorkCompleted GameOverDoing(string command, GameState GS)
        {
            WorkCompleted result = new WorkCompleted();
            switch (command)
            {
                case "YES":
                    GM.CurrentState = "MainMenu";
                    break;
                case "NO":
                    Application.Current.Shutdown();
                    break;
                default:
                    result.result = false;
                    result.log = "指令错误";
                    break;
            }
            return result;
        }
        
        #endregion
        #endregion

        #region 地图脚本
        private WorkCompleted LabBefore(Map M)
        {
            WorkCompleted result = new WorkCompleted();

            result.result = true;
            result.log = M.Description;

            return result;
        }

        private WorkCompleted SittingRoomBefore(Map M)
        {
            WorkCompleted result = new WorkCompleted();

            result.result = true;
            result.log = M.Description;

            return result;
        }

        private WorkCompleted BedRoomBefore(Map M)
        {
            WorkCompleted result = new WorkCompleted();

            result.result = true;
            result.log = M.Description;

            return result;
        }

        private WorkCompleted WashRoomBefore(Map M)
        {
            WorkCompleted result = new WorkCompleted();

            result.result = true;
            result.log = M.Description;

            return result;
        }

        private WorkCompleted OutBefore(Map M)
        {
            WorkCompleted result = new WorkCompleted();

            result.result = true;
            result.log = M.Description;

            return result;
        }

        private WorkCompleted OutDoing(string command, Map M)
        {
            WorkCompleted result = new WorkCompleted();
            GM.CurrentState = "GameOver";
            return result;
        }
        
        #endregion
    }
}
