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

        /// <summary>
        /// 主窗口UI根节点
        /// </summary>
        public MainWindow UI { get; set; }

        /// <summary>
        /// 游戏管理器
        /// </summary>
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
        /// <summary>
        /// 初始化游戏管理器
        /// </summary>
        public void InitGM()
        {
            GM = DataProvider.Instence.GM;
            GM.WorkCompleted += GM_WorkCompleted;
            GM.CurrentState = "MainMenu";
        }

        /// <summary>
        /// 打印游戏管理器返回的日志
        /// </summary>
        /// <param name="result">日志</param>
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

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="command">命令</param>
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

        /// <summary>
        /// 打印日志
        /// </summary>
        /// <param name="command">日志内容</param>
        /// <param name="type">日志类型</param>
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
