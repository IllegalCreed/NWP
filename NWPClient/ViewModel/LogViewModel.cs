using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWPClient.ViewModel
{
    public class LogViewModel : ViewModelBase
    {
        #region 属性
        private string m_Text;
        /// <summary>
        /// 日志
        /// </summary>
        public string Text
        {
            get
            {
                return m_Text;
            }
            set
            {
                if (value != m_Text)
                {
                    m_Text = value;
                    RaisePropertyChanged(() => Text);
                }
            }
        }

        private LogType m_Type;
        /// <summary>
        /// 日志类型
        /// </summary>
        public LogType Type
        {
            get
            {
                return m_Type;
            }
            set
            {
                if (value != m_Type)
                {
                    m_Type = value;
                    RaisePropertyChanged(() => Type);
                }
            }
        }
        #endregion

        #region 构造
        public LogViewModel()
        {
            Text = "";
            Type = LogType.SYSTEM;
        }
        #endregion
    }

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        SYSTEM,
        PLAYER,
        ERROR
    }
}
