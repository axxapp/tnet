using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TNet.Models.Task;

namespace TNet.Models.Order
{
    //[NotMapped]
    public class MyOrderDetail
    {
        public TCom.EF.MyOrder Order { get; set; }


        public List<TCom.EF.MyOrderPress> Presses { get; set; }


        public OrderStatusItem Status
        {
            get
            {
                return OrderStatus.get(Order != null && Order.status != null ? Order.status.Value : 0);
            }
            set
            {

            }
        }


        public TaskDetailItem task { get; set; }

        public List<TaskPressItem> press { get; set; }

        public List<TaskRecverItem> recver { get; set; }

        public List<TaskImg> imgs { get; set; }

    }
}