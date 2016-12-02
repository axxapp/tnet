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
        /// 派单
        /// </summary>
        public readonly static int DisTask = 100;


        /// <summary>
        /// 抢单
        /// </summary>
        public readonly static int Rob = 200;

        /// <summary>
        /// 开始
        /// </summary>
        public readonly static int Start = 300;


        /// <summary>
        /// 暂停
        /// </summary>
        public readonly static int Pause = 400;

        /// <summary>
        /// 完工
        /// </summary>
        public readonly static int Finish = 500;


        public static Dictionary<int, string> s = new Dictionary<int, string>()
        {
            {
                DisTask,"派单"
            },
            {
                Rob,"抢单"
            },
            {
                Start,"开工"
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

        public static string get(int? type)
        {
            return type != null && s.ContainsKey(type.Value) ? s[type.Value] : s[0];
        }
    }
}
