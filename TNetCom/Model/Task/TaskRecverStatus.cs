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
        /// 正常
        /// </summary>
        public readonly static int Normal = 1;


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


        /// <summary>
        /// 工作中
        /// </summary>
        public readonly static int Doing = 400;



        public static Dictionary<int, TaskRecverStatusItem> s = new Dictionary<int, TaskRecverStatusItem>()
        {
            {
                Normal,
                new TaskRecverStatusItem()
                {

                    text = "等待开工",
                    ops = "srartwork"
                }
            },
            {
                Pause,
                new TaskRecverStatusItem()
                {

                    text = "暂结",
                    ops = "srartwork"
                }
            },
            {
                Finish,
                new TaskRecverStatusItem()
                {

                    text = "完工",
                    ops = ""
                }
            },
            {
                Cancel,
                new TaskRecverStatusItem()
                {

                    text = "取消",
                    ops = ""
                }
            },
            {
                Doing,
                new TaskRecverStatusItem()
                {

                    text = "工作中",
                    ops = "pause|finish"
                }
            },
            {
                0,
                new TaskRecverStatusItem()
                {

                    text = "未知",
                    ops = ""
                }
            },
        };

        public static TaskRecverStatusItem get(int? sv)
        {
            return sv != null && s.ContainsKey(sv.Value) ? s[sv.Value] : s[0];
        }
    }
}
