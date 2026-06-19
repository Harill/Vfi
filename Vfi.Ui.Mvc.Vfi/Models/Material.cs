using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public partial class Material
    {
        public Material()
        {
            this.MaterialLimitPlans = new List<MaterialLimitPlan>();
            this.ProductionMaterials = new List<ProductionMaterial>();
            this.TrackUpMachines = new List<TrackUpMachine>();
            this.ExportMaterialDetails = new List<ExportMaterialDetail>();
            this.MaterialInventories = new List<MaterialInventory>();
            this.MaterialInventoryPeriods = new List<MaterialInventoryPeriod>();
            this.StockOrderDetails = new List<StockOrderDetail>();
            this.TransactionDetails = new List<TransactionDetail>();
            this.ImportPurchaseOrderDetails = new List<ImportPurchaseOrderDetail>();
            this.PriceListMaterials = new List<PriceListMaterial>();
            this.Products = new List<Product>();
            this.MaterialImgs = new List<MaterialImg>();
        }

        public int MaterialId { get; set; }
        public int MaterialTypeId { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public string DiameterType { get; set; }
        public Nullable<double> Length { get; set; }
        public double Weight { get; set; }
        public bool Active { get; set; }
        public string ModifiedUser { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public double UnitPrice { get; set; }
        public string Shape { get; set; }
        public double OutDiameter { get; set; }
        public double InDiameter { get; set; }
        public bool IsExpensive { get; set; }
        public virtual ICollection<MaterialLimitPlan> MaterialLimitPlans { get; set; }
        public virtual ICollection<ProductionMaterial> ProductionMaterials { get; set; }
        public virtual ICollection<TrackUpMachine> TrackUpMachines { get; set; }
        public virtual ICollection<ExportMaterialDetail> ExportMaterialDetails { get; set; }
        public virtual ICollection<MaterialInventory> MaterialInventories { get; set; }
        public virtual ICollection<MaterialInventoryPeriod> MaterialInventoryPeriods { get; set; }
        public virtual ICollection<StockOrderDetail> StockOrderDetails { get; set; }
        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }
        public virtual ICollection<ImportPurchaseOrderDetail> ImportPurchaseOrderDetails { get; set; }
        public virtual MaterialType MaterialType { get; set; }
        public virtual ICollection<PriceListMaterial> PriceListMaterials { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<MaterialImg> MaterialImgs { get; set; }
    }
}
