using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Models {
    public partial class EvaluationForm 
    {
        public int FormId { get; set; }
        public int VendorId { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
        public int MaterialClassifiedId { set; get; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public string ModifiedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ApprovedUser { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }

        public Nullable<int> TechSpPoint{ get; set; }
        public Nullable<int> LogisticsPoint { get; set; }
        public Nullable<int> QuantityDeliveryPoint { get; set; }
        public Nullable<int> NGTimePoint { get; set; }
        public Nullable<int> NGNumberPoint { get; set; }
        public Nullable<int> PricePoint { get; set; }
        public Nullable<int> TotalPoint { get; set; }
        public Nullable<int> ZeroPointCount { get; set; }
        public string Grade { get; set; }
        public Nullable<int> NGQuantity { get; set; }
        public Nullable<int> OrderQuantity { get; set; }

        public virtual Vendor Vendor { get; set; }
        public virtual MaterialClassified MaterialClassified { get; set; }

    }
}
