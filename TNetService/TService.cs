using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using TCom.Msg;
using TCom.Util;
using TComCoreService;
using TNetService.BLL;
namespace TNetService
{
    partial class TService : ServiceBase
    {


        public TService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Service.init();
        }


        protected override void OnStop()
        {
            Service.stopt();
        }

    }
}
