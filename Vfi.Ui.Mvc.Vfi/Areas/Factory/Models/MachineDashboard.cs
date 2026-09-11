using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vfi.Ui.Mvc.Vfi.Areas.Factory.Models{
    public class MachineDashboard {
        public int RunningMachine { get; set; }
        public int NotRunningMachine { get; set; }

        public int PendingWorkOrder { get; set; }
        public int ActiveWorkOrder { get; set; }

        public int FinishedWorkOrder_InMonth { get; set; }
        public double ProductionGood_InMonth { get; set; }
        public double ProductionNotGood_InMonth { get; set; }
        public double QCRate_InMonth { get; set; }
    }
}