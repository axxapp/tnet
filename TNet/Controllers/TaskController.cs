﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TNet.Controllers
{
    public class TaskController : Controller
    {
        // GET: Task
        public ActionResult MyTask()
        {
            return View();
        }

        public ActionResult Finish()
        {
            return View();
        }


        public ActionResult Pause()
        {
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }


        public ActionResult DisList()
        {
            return View();
        }
    }
}