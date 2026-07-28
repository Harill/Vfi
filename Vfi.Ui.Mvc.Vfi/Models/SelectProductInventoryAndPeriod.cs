using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public partial class SelectProductInventoryAndPeriod
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public Nullable<double> TotalInv { get; set; }
        public Nullable<double> TotalPeriod { get; set; }

        public string LotNumber { get; set; }
        public int ProductInventoryId { get; set; }
    }
}
