using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public partial class ProductInventory
    {
        public ProductInventory()
        {
            this.SmartProductions = new List<SmartProduction>();
            this.ExportGCN_NCUDetail = new List<ExportGCN_NCUDetail>();
            this.ImportFormCncDetails = new List<ImportFormCncDetail>();
            this.ImportNCU_QCBDetail = new List<ImportNCU_QCBDetail>();
            this.ProductInventoryPeriods = new List<ProductInventoryPeriod>();
            this.TransactionDetails = new List<TransactionDetail>();
        }

        public int ProductInventoryId { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public double TotalQty { get; set; }
        public Nullable<double> AvailableQty { get; set; }
        public Nullable<double> UnavailableQty { get; set; }
        public string UnitMeasure { get; set; }
        public byte Status { get; set; }
        public bool Active { get; set; }
        public string ModifiedUser { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string LotNumber { get; set; }
        public Nullable<int> VendorId { get; set; }
        public Nullable<System.DateTime> ImportDate { get; set; }
        public Nullable<System.DateTime> ExportDate { get; set; }
        public Nullable<int> ErrorId { get; set; }
        public Nullable<int> MachineId { get; set; }
        public Nullable<int> MaterialInvId { get; set; }
        public Nullable<int> ByProcessMachineId { get; set; }
        public string StoreCode { get; set; }
        public Nullable<int> DefectId { get; set; }
        public bool NG { set; get; }



        public virtual Machine Machine { get; set; }
        public virtual ProductionDefect ProductionDefect { get; set; }
        public virtual ICollection<SmartProduction> SmartProductions { get; set; }
        public virtual ICollection<ExportGCN_NCUDetail> ExportGCN_NCUDetail { get; set; }
        public virtual ICollection<ImportFormCncDetail> ImportFormCncDetails { get; set; }
        public virtual ICollection<ImportNCU_QCBDetail> ImportNCU_QCBDetail { get; set; }
        public virtual MaterialInventory MaterialInventory { get; set; }
        public virtual ProcessError ProcessError { get; set; }
        public virtual Product Product { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<ProductInventoryPeriod> ProductInventoryPeriods { get; set; }
        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}
