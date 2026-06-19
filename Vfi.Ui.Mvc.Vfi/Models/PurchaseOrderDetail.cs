using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public partial class PurchaseOrderDetail
    {
        public PurchaseOrderDetail()
        {
            this.InquiryPoes = new List<InquiryPo>();
        }

        public long PurchaseOrderDetailId { get; set; }
        public long PurchaseOrderId { get; set; }
        public int MaterialClassifiedId { get; set; }
        public int ReferenceId { get; set; }
        public double OrderQty { get; set; }
        public double UnitPrice { get; set; }
        public double ReceivedQty { get; set; }
        public double RejectedQty { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public bool Active { get; set; }
        public string ModifiedUser { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<bool> IsComplete { get; set; }
        public string Unit { get; set; }
        public Nullable<double> Met { get; set; }

        public string Standard { get; set; }
        public string Note { get; set; }
        public string ManagerNote { get; set; }

        public string ApprovedPo { get; set; }
        public DateTime? DateApproved { get; set; }

        public virtual MaterialClassified MaterialClassified { get; set; }
        public virtual ICollection<InquiryPo> InquiryPoes { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}
