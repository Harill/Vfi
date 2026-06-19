using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public partial class ProductInventoryPeriod
    {
        public long ProductInventoryPeriodId { get; set; }
        public long TransactionId { get; set; }
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }
        public int PeriodDay { get; set; }
        public int PeriodMonth { get; set; }
        public int PeriodYear { get; set; }
        public System.DateTime PeriodDate { get; set; }
        public double EarlyPeriodQuantity { get; set; }
        public Nullable<double> EarlyPeriodPrice { get; set; }
        public double Quantity { get; set; }
        public Nullable<double> UnitPrice { get; set; }
        public Nullable<double> Price { get; set; }
        public string UnitMeasure { get; set; }
        public double LastPeriodQuantity { get; set; }
        public Nullable<double> LastPeriodPrice { get; set; }
        public string ModifiedUser { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public int ProductInvId { get; set; }
        public string LotNumber { get; set; }


        public virtual ProductInventory ProductInventory { get; set; }
        public virtual Product Product { get; set; }
        public virtual Transaction Transaction { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
