using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCom.Model.Task
{
    public class TaskPressType
    {
        /// <summary>
        /// 开始
        /// </summary>
        public readonly static int Start = 100;


        /// <summary>
        /// 暂停
        /// </summary>
        public readonly static int Pause = 200;

        /// <summary>
        /// 完工
        /// </summary>
        public readonly static int Finish = 300;


        public static Dictionary<int, string> s = new Dictionary<int, string>()
        {
            {
                Start,"开始"
            },
            {
                Pause,"暂停"
            },
            {
                Finish,"完工"
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
