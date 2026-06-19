using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public partial class ToolInventory
    {
        public ToolInventory()
        {
            this.ExportToolDetails = new List<ExportToolDetail>();
            this.ToolInventoryPeriods = new List<ToolInventoryPeriod>();
        }

        public int ToolInvId { get; set; }
        public int ToolId { get; set; }
        public double TotalQuantity { get; set; }
        public string LotNumber { get; set; }
        public string UnitMeasure { get; set; }
        public double UnitPrice { get; set; }
        public int VendorId { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> ImportDate { get; set; }
        public Nullable<double> ImportQuantity { get; set; }
        public Nullable<System.DateTime> FirstUseDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string StoreCode { get; set; }

        public bool NG { set; get; }
        public bool Lock { set; get; }

        public virtual Tool Tool { get; set; }
        public virtual ICollection<ExportToolDetail> ExportToolDetails { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<ToolInventoryPeriod> ToolInventoryPeriods { get; set; }
    }
}
