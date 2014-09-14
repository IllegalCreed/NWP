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
            GM.WorkCompleted += GM_WorkCompleted;
            GM.CurrentState = "MainMenu";
        }

        void GM_WorkCompleted(WorkCompleted result)
        {
            //根据返回值执行显示
            if (result.result == false && !string.IsNullOrEmpty(result.log))
            {
                PrintLog(result.log, LogType.ERROR);
            }
            else if (result.result == true && !string.IsNullOrEmpty(result.log))
            {
                PrintLog(result.log, LogType.SYSTEM);
            }
        }

        public void CommandExcute(string command)
        {
            //打印用户命令
            if (!String.IsNullOrEmpty(command))
            {
                PrintLog(command, LogType.PLAYER);
            }

            //调用GM处理命令
            GM.Excute(command);
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
