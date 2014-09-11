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
        #endregion

        #region 构造
        public MainWindowViewModel()
        {
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
        }

        public void CommandExcute(string command,LogType type)
        {
            PrintLog(command, type);

            switch (GM.State)
            {
                case GameState.MainMenu:
                    MainMenuExcute(command);
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
                    break;
                case "exit":
                    Application.Current.Shutdown();
                    break;
                default:
                    PrintLog("指令错误", LogType.ERROR);
                    break;
            }
        }
        #endregion
    }
}
