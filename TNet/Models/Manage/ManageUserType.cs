﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TNet.Models
{
    /// <summary>
    /// 管理员类型
    /// </summary>
    public enum ManageUserType
    {
        /// <summary>
        /// 管理员
        /// </summary>
        [Display(Name ="管理员")]
        ManageUser=0,

        /// <summary>
        /// 职员
        /// </summary>
        [Display(Name = "职员")]
        Worker =1

    }
}