using System;
using System.Collections.Generic;
using System.Linq;
using Vfi.Ui.Mvc.Vfi.Utilities;

namespace Vfi.Ui.Mvc.Vfi.Areas.Inv.Models {

    public class ProductDailyReport {
        //public string FromDate { get; set; }
        //public string ToDate { get; set; }
        public int Index { get; set; }
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        //public string LotNumber { get; set; }
        //public double UnitPrice { get; set; }
        //public int ProductionPrice { get; set; }
        public double OkQuantity { get; set; }
        public double ProcessingQuantity { get; set; }
        public double NGQuantity { get; set; }

        public double TotalPrice { get; set; }
        public string Currency { get; set; }

        public string ProcessingTypeName { get; set; }


        public DateTime MaterialUsedDate { get; set; }
        public double ProductUnitPrice { get; set; }




    }
}