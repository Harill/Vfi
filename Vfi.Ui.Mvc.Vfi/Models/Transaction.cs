using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public partial class Transaction
    {
        public Transaction()
        {
            this.ExportChangeProducts = new List<ExportChangeProduct>();
            this.ExportGCN_NCU = new List<ExportGCN_NCU>();
            this.ExportMaterials = new List<ExportMaterial>();
            this.ImportNCU_QCB = new List<ImportNCU_QCB>();
            this.ImportWorkpieceMaterials = new List<ImportWorkpieceMaterial>();
            this.MaterialInventoryPeriods = new List<MaterialInventoryPeriod>();
            this.ProductInventoryPeriods = new List<ProductInventoryPeriod>();
            this.ImportPurchaseOrders = new List<ImportPurchaseOrder>();
            this.OrderNotes = new List<OrderNote>();
            this.TransactionDetails = new List<TransactionDetail>();
            this.TransactionProducts = new List<TransactionProduct>();
            this.TransactionWeighings = new List<TransactionWeighing>();
        }

        public long TransactionId { get; set; }
        public Nullable<long> StockOrderId { get; set; }
        public Nullable<int> WarehouseIssueId { get; set; }
        public Nullable<int> WarehouseReceiptId { get; set; }
        public string TransactionCode { get; set; }
        public string EoI { get; set; }
        public bool MoP { get; set; }
        public string CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public byte Status { get; set; }
        public bool IsApprove { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public string ModifiedUser { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public bool FinishDesign { get; set; }

        public Nullable<long> PoId { get; set; }
        public Nullable<long> ReferenceId { get; set; }
        public Nullable<bool> IsInternal { get; set; }
        public Nullable<bool> IsPacking { get; set; }
        public virtual ICollection<ExportChangeProduct> ExportChangeProducts { get; set; }
        public virtual ICollection<ExportGCN_NCU> ExportGCN_NCU { get; set; }
        public virtual ICollection<ExportMaterial> ExportMaterials { get; set; }
        public virtual ICollection<ImportNCU_QCB> ImportNCU_QCB { get; set; }
        public virtual ICollection<ImportWorkpieceMaterial> ImportWorkpieceMaterials { get; set; }
        public virtual ICollection<MaterialInventoryPeriod> MaterialInventoryPeriods { get; set; }
        public virtual ICollection<ProductInventoryPeriod> ProductInventoryPeriods { get; set; }
        public virtual StockOrder StockOrder { get; set; }
        public virtual ICollection<ImportPurchaseOrder> ImportPurchaseOrders { get; set; }
        public virtual ICollection<OrderNote> OrderNotes { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual Warehouse Warehouse1 { get; set; }
        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }
        public virtual ICollection<TransactionProduct> TransactionProducts { get; set; }
        public virtual ICollection<TransactionWeighing> TransactionWeighings { get; set; }
        public virtual ICollection<TransactionImg> TransactionImg { get; set; }
    }
}
