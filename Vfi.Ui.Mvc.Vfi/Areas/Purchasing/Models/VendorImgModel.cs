using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public class VendorObjectiveModel
    {
        public int ObjectiveId { get; set; }
        public int VendorId { get; set; }
        public int Year { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public string Note { get; set; }

        public int PricePoint { get; set; }
        public int TechSpPoint { get; set; }

        public int LateTimes { get; set; }
        public int LessTimes { get; set; }
        public int NGTimes { get; set; }
        public double PercentNGNumber { get; set; }

        public int LogisticsPoint { get; set; }
        public int QuantityDeliveryPoint { get; set; }
        public int NGTimesPoint { get; set; }
        public int NGNumberPoint { get; set; }

        public int TotalPoint { get; set; }

    }
}