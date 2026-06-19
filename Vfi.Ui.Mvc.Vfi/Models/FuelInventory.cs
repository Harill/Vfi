using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public partial class FuelInventory
    {
        public FuelInventory()
        {
            this.FuelInventoryPeriods = new List<FuelInventoryPeriod>();
        }

        public int FuelInvId { get; set; }
        public int FuelId { get; set; }
        public double TotalQuantity { get; set; }
        public string LotNumber { get; set; }
        public string UnitMeasure { get; set; }
        public double UnitPrice { get; set; }
        public int VendorId { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<System.DateTime> ImportDate { get; set; }

        public bool NG { set; get; }
        public bool Lock { set; get; }



        public virtual Fuel Fuel { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<FuelInventoryPeriod> FuelInventoryPeriods { get; set; }
    }
}
