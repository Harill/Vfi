using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public partial class RepairFormDetail
    {
        public int DetailId { get; set; }
        public int FormId { get; set; }
        public int EmployeeId { get; set; }
        public System.DateTime StartDate { get; set; }
        public string StartUser { get; set; }
        public int Status { get; set; }
        public Nullable<System.DateTime> FinishDate { get; set; }
        public string FinishUser { get; set; }
        public Nullable<int> FixId { get; set; }
        public string Note { get; set; }
        public int Shift { get; set; }
        public int MoreTime { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public Nullable<int> FixQuantity { get; set; }
        public virtual MachineRepairForm MachineRepairForm { get; set; }
        public virtual MachineStateDetail MachineStateDetail { get; set; }
        public virtual Employee Employee { get; set; }

    }
}
