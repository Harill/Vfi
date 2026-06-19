using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public partial class MaterialInventory
    {
        public MaterialInventory()
        {
            this.SmartProductions = new List<SmartProduction>();
            this.TrackUpMaterials = new List<TrackUpMaterial>();
            this.WorkOrderRoutings = new List<WorkOrderRouting>();
            this.ExportMaterialDetails = new List<ExportMaterialDetail>();
            this.ImportFormSX1Detail = new List<ImportFormSX1Detail>();
            this.MaterialInventoryPeriods = new List<MaterialInventoryPeriod>();
            this.MaterialInvOnMachines = new List<MaterialInvOnMachine>();
            this.MaterialInvOnMachinePeriods = new List<MaterialInvOnMachinePeriod>();
            this.MaterialUseDetails = new List<MaterialUseDetail>();
            this.ProductInventories = new List<ProductInventory>();
        }

        public int MaterialInventoryId { get; set; }
        public int MaterialId { get; set; }
        public double TotalQty { get; set; }
        public Nullable<double> TotalQtyKg { get; set; }
        public Nullable<double> AvailableQty { get; set; }
        public Nullable<double> UnavailableQty { get; set; }
        public string UnitMeasure { get; set; }
        public byte Status { get; set; }
        public bool Active { get; set; }
        public string ModifiedUser { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string LotNumber { get; set; }
        public double UnitPrice { get; set; }
        public double UnitWeight { get; set; }
        public Nullable<int> VendorId { get; set; }
        public System.DateTime ImportDate { get; set; }
        public double ImportQuantity { get; set; }
        public double ImportQuantityKg { get; set; }
        public Nullable<System.DateTime> FirstUseDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string StoreCode { get; set; }
        public double Length { get; set; }
        public string InfoImg { get; set; }
        public string InfoImg2 { get; set; }

        public bool NG { get; set; }
        public bool Lock { get; set; }




        public virtual ICollection<SmartProduction> SmartProductions { get; set; }
        public virtual ICollection<TrackUpMaterial> TrackUpMaterials { get; set; }
        public virtual ICollection<WorkOrderRouting> WorkOrderRoutings { get; set; }
        public virtual ICollection<ExportMaterialDetail> ExportMaterialDetails { get; set; }
        public virtual ICollection<ImportFormSX1Detail> ImportFormSX1Detail { get; set; }
        public virtual Material Material { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<MaterialInventoryPeriod> MaterialInventoryPeriods { get; set; }
        public virtual ICollection<MaterialInvOnMachine> MaterialInvOnMachines { get; set; }
        public virtual ICollection<MaterialInvOnMachinePeriod> MaterialInvOnMachinePeriods { get; set; }
        public virtual ICollection<MaterialUseDetail> MaterialUseDetails { get; set; }
        public virtual ICollection<ProductInventory> ProductInventories { get; set; }
    }
}
