using NWPClient.Data;
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

        public MainWindow UI { get; set; }

        private GameManager GM{ get; set; }
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
            GM = DataProvider.Instence.GM;
            GM.GameStateChanged += GM_GameStateChanged;
            GM.MapChanged += GM_MapChanged;
            GM.CurrentState = "MainMenu";
        }

        void GM_MapChanged(Map Map)
        {
            if (!string.IsNullOrEmpty(Map.Description))
            {
                PrintLog(Map.Description, LogType.SYSTEM);
            }
        }

        private void GM_GameStateChanged(GameState GS)
        {
            if (!string.IsNullOrEmpty(GS.Description))
            {
                PrintLog(GS.Description, LogType.SYSTEM);
            }
        }

        public void CommandExcute(string command, LogType type)
        {
            if (!String.IsNullOrEmpty(command))
            {
                PrintLog(command, type);
            }

            WorkCompleted result = GM.Excute(command);
            if (result.result == false && !string.IsNullOrEmpty(result.log))
            {
                PrintLog(result.log, LogType.ERROR);
            }
            else if (result.result == true && !string.IsNullOrEmpty(result.log))
            {
                PrintLog(result.log, LogType.SYSTEM);
            }

            if (!string.IsNullOrEmpty(result.nextState))
            {
                GM.CurrentState = result.nextState;
            }

        }

        public void PrintLog(string command, LogType type)
        {
            LogViewModel LVM = new LogViewModel();
            LVM.Text = command;
            LVM.Type = type;
            Logs.Add(LVM);
            UI.scrollViewer.ScrollToBottom();
        }
        #endregion
    }
}
