using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCom.Model.Task
{
    /// <summary>
    /// 任务类型
    /// </summary>
    public class TaskType
    {
        /// <summary>
        /// 报装
        /// </summary>
        public readonly static int Setup = 10;

        /// <summary>
        /// 投诉报修
        /// </summary>
        public readonly static int Complaint = 20;



        public static Dictionary<int, string> s = new Dictionary<int, string>()
        {
            {
                Setup,"报装"
            },
            {
                Complaint,"投诉建议"
            },
            {
                0,"未知"
            }
        };

        public static string get(int? tasktype)
        {
            return tasktype != null && s.ContainsKey(tasktype.Value) ? s[tasktype.Value] : s[0];
        }
    }
}
