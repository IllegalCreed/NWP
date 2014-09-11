using NWPCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NWPClient.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region 属性
        private ObservableCollection<LogViewModel> m_Logs;
        /// <summary>
        /// 日志列表
        /// </summary>
        public ObservableCollection<LogViewModel> Logs
        {
            get
            {
                return m_Logs;
            }
            set
            {
                if (value != m_Logs)
                {
                    m_Logs = value;
                    RaisePropertyChanged(() => Logs);
                }
            }
        }

        private GameManager m_GM;
        public GameManager GM
        {
            get
            {
                return m_GM;
            }
            set
            {
                if (value != m_GM)
                {
                    m_GM = value;
                    RaisePropertyChanged(() => GM);
                }
            }
        }

        public MainWindow UI { get; set; }
        #endregion

        #region 构造
        public MainWindowViewModel(MainWindow ui)
        {
            UI = ui;
            Logs = new ObservableCollection<LogViewModel>();
            InitGM();
        }
        #endregion

        #region 方法
        public void InitGM()
        {
            GM = new GameManager();
            GM.State = GameState.Beginning;

            string command = "科科历险记\r\n\r\n开始游戏(COMMAND=start)\r\n退出游戏(COMMAND=exit)";
            PrintLog(command, LogType.SYSTEM);

            GM.State = GameState.MainMenu;
        }

        public void PrintLog(string command, LogType type)
        {
            LogViewModel LVM = new LogViewModel();
            LVM.Text = command;
            LVM.Type = type;
            Logs.Add(LVM);
            UI.scrollViewer.ScrollToBottom();
        }

        public void CommandExcute(string command, LogType type)
        {
            if (!String.IsNullOrEmpty(command))
            {
                PrintLog(command, type);
            }
            switch (GM.State)
            {
                case GameState.MainMenu:
                    MainMenuExcute(command);
                    break;
                case GameState.CreatPlayer:
                    CreatPlayer(command);
                    break;
                case GameState.Intro:
                    Intro(command);
                    break;
            }
        }

        public void MainMenuExcute(string command)
        {
            switch (command)
            {
                case "start":
                    string log = "欢迎来到科科的世界\r\n首先你要为你的角色起一个名字";
                    PrintLog(log, LogType.SYSTEM);
                    GM.State = GameState.CreatPlayer;
                    break;
                case "exit":
                    Application.Current.Shutdown();
                    break;
                default:
                    PrintLog("指令错误", LogType.ERROR);
                    break;
            }
        }

        public void CreatPlayer(string command)
        {
            switch (command)
            {
                case "":
                    PrintLog("姓名不能为空", LogType.ERROR);
                    break;
                default:
                    GM.Player = new Player();
                    GM.Player.Name = command;
                    string log = "你好" + GM.Player.Name + "\r\n我是游戏引导员，此刻我本该说一些游戏背景介绍啊，新手引导啊之类的话";
                    PrintLog(log, LogType.SYSTEM);
                    GM.State = GameState.Intro;
                    break;
            }
        }

        private int introTick = 0;
        public void Intro(string command)
        {
            string log = "";
            switch (introTick)
            {
                case 0:
                    log = "但是游戏开发着只做到这里，后面他还在想怎么做";
                    PrintLog(log, LogType.SYSTEM);
                    break;
                case 1:
                    log = "所以就没有然后了，你可以退出游戏了\r\n恭喜你" + GM.Player.Name + "，你通关了！";
                    PrintLog(log, LogType.SYSTEM);
                    break;
                default:
                    break;
            }
            introTick++;

        }
        #endregion
    }
}
