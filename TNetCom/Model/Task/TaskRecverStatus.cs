using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCom.Model.Task
{
    public class TaskRecverStatus
    {
        /// <summary>
        /// 暂结
        /// </summary>
        public readonly static int Pause = 100;

        /// <summary>
        /// 完工
        /// </summary>
        public readonly static int Finish = 200;

        /// <summary>
        /// 取消
        /// </summary>
        public readonly static int Cancel = 300;


        public static Dictionary<int, string> s = new Dictionary<int, string>()
        {
            {
                Pause,"暂结"
            },
            {
                Finish,"完工"
            },
            {
                Cancel,"取消"
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
