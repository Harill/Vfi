using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public partial class TransactionDetail
    {
        public TransactionDetail()
        {
            this.ExportFormTP_KDDetail = new List<ExportFormTP_KDDetail>();
            this.ExportGCN_NCUDetail = new List<ExportGCN_NCUDetail>();
            this.ExportMaterialDetails = new List<ExportMaterialDetail>();
            this.ImportNCU_QCBDetail = new List<ImportNCU_QCBDetail>();
        }

        public long TransactionDetailId { get; set; }
        public Nullable<long> TransactionId { get; set; }
        public Nullable<int> ReferenceId { get; set; }
        public bool MoP { get; set; }
        public double Quantity { get; set; }
        public Nullable<double> QuantityKg { get; set; }
        public Nullable<double> Price { get; set; }
        public string UnitMeasure { get; set; }
        public bool Active { get; set; }
        public string ModifiedUser { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string Note { get; set; }
        public Nullable<int> VendorId { get; set; }
        public string LotNumber { get; set; }
        public Nullable<int> ErrorId { get; set; }
        public Nullable<int> ProductInvId { get; set; }
        public Nullable<int> NextProcessId { get; set; }
        public Nullable<long> TransactionProductId { get; set; }
        public Nullable<int> MachineId { get; set; }
        public Nullable<long> PoDetailId { get; set; }
        public Nullable<bool> IsInternal { get; set; }
        public string StoreCode { get; set; }
        public Nullable<int> DefectId { get; set; }
        public Nullable<int> DrawerId { get; set; }
        public bool NG { set; get; }


        public virtual Machine Machine { get; set; }
        public virtual ProductionDefect ProductionDefect { get; set; }
        public virtual ProductionProcessByMachine ProductionProcessByMachine { get; set; }
        public virtual ICollection<ExportFormTP_KDDetail> ExportFormTP_KDDetail { get; set; }
        public virtual ICollection<ExportGCN_NCUDetail> ExportGCN_NCUDetail { get; set; }
        public virtual ICollection<ExportMaterialDetail> ExportMaterialDetails { get; set; }
        public virtual ICollection<ImportNCU_QCBDetail> ImportNCU_QCBDetail { get; set; }
        public virtual InventoryDrawer InventoryDrawer { get; set; }
        public virtual ProcessError ProcessError { get; set; }
        public virtual ProductInventory ProductInventory { get; set; }
        public virtual Transaction Transaction { get; set; }
        public virtual Material Material { get; set; }
        public virtual Product Product { get; set; }
        public virtual TransactionProduct TransactionProduct { get; set; }
    }
}
