using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public partial class MaterialClassified
    {
        public MaterialClassified()
        {
            this.InventoryShelves = new List<InventoryShelf>();
            this.InquiryPoes = new List<InquiryPo>();
            this.MaterialTypes = new List<MaterialType>();
            this.PoTaxInvoices = new List<PoTaxInvoice>();
            this.PurchaseOrders = new List<PurchaseOrder>();
            this.PurchaseOrderDetails = new List<PurchaseOrderDetail>();
            this.Vendors = new List<Vendor>();
            this.EvaluationForms = new List<EvaluationForm>();
        }

        public int MaterialClassifiedId { get; set; }
        public string MaterialClassifiedName { get; set; }
        public bool Active { get; set; }
        public string ModifiedUser { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual ICollection<InventoryShelf> InventoryShelves { get; set; }
        public virtual ICollection<InquiryPo> InquiryPoes { get; set; }
        public virtual ICollection<MaterialType> MaterialTypes { get; set; }
        public virtual ICollection<PoTaxInvoice> PoTaxInvoices { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public virtual ICollection<Vendor> Vendors { get; set; }
        public virtual ICollection<EvaluationForm> EvaluationForms { get; set; }
    }
}
