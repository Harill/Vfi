using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public partial class InquiryPo
    {
        public long InquiryId { get; set; }
        public int ReferenceId { get; set; }
        public int ClasstifiedId { get; set; }
        public Nullable<long> PoDetailId { get; set; }
        public string Unit { get; set; }
        public double OrderQty { get; set; }
        public double UnitPrice { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public byte Status { get; set; }
        public string ModifiedUser { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> VendorId { get; set; }
        public string Note { get; set; }
        public string Currency { get; set; }
        public string InquiryNumber { get; set; }

        public byte ManagementSignature { get; set; }
        public string Standard { get; set; }

        public virtual MaterialClassified MaterialClassified { get; set; }
        public virtual PurchaseOrderDetail PurchaseOrderDetail { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
