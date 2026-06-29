using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public partial class Employee
    {
        public Employee()
        {
            this.MachineRepairForms = new List<MachineRepairForm>();
            this.MachineRepairForms1 = new List<MachineRepairForm>();
            this.Production2TransactionDetail = new List<Production2TransactionDetail>();
            this.RealTestings = new List<RealTesting>();
            this.RepairFormDetails = new List<RepairFormDetail>();
            this.SmartProduction2 = new List<SmartProduction2>();
            this.TrackingRepairEmployees = new List<TrackingRepairEmployee>();
            this.TrackingRepairEmployees1 = new List<TrackingRepairEmployee>();
            this.WorkOrderProcesses = new List<WorkOrderProcess>();
            this.Customers = new List<Customer>();
            this.QuoteForms = new List<QuoteForm>();
            this.Orders = new List<Order>();
        }

        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public bool Active { get; set; }
        public string ModifiedUser { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public bool Production2 { get; set; }
        public bool Production2B { get; set; }
        public bool Repair { get; set; }
        public bool QcLine { get; set; }
        public string GroupName { get; set; }
        public Nullable<int> UserId { get; set; }

        public virtual ICollection<MachineRepairForm> MachineRepairForms { get; set; }
        public virtual ICollection<MachineRepairForm> MachineRepairForms1 { get; set; }
        public virtual ICollection<Production2TransactionDetail> Production2TransactionDetail { get; set; }
        public virtual ICollection<RealTesting> RealTestings { get; set; }
        public virtual ICollection<RepairFormDetail> RepairFormDetails { get; set; }
        public virtual ICollection<SmartProduction2> SmartProduction2 { get; set; }
        public virtual ICollection<TrackingRepairEmployee> TrackingRepairEmployees { get; set; }
        public virtual ICollection<TrackingRepairEmployee> TrackingRepairEmployees1 { get; set; }
        public virtual ICollection<WorkOrderProcess> WorkOrderProcesses { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<QuoteForm> QuoteForms { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
