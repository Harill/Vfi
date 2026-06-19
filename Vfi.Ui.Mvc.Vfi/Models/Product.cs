using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public partial class Product
    {
        public Product()
        {
            this.ProductionPlans = new List<ProductionPlan>();
            this.DefectTransactionDetails = new List<DefectTransactionDetail>();
            this.MachineAppraisals = new List<MachineAppraisal>();
            this.MachineRepairForms = new List<MachineRepairForm>();
            this.MaterialLimitPlans = new List<MaterialLimitPlan>();
            this.ProductImgs = new List<ProductImg>();
            this.QcImgs = new List<QcImg>();
            this.Production2TransactionDetail = new List<Production2TransactionDetail>();
            this.ProductionDefects = new List<ProductionDefect>();
            this.ProductionFuels = new List<ProductionFuel>();
            this.ProductionHeatTreatments = new List<ProductionHeatTreatment>();
            this.ProductionMaterials = new List<ProductionMaterial>();
            this.ProductionPlatings = new List<ProductionPlating>();
            this.ProductionPolishes = new List<ProductionPolish>();
            this.ProductionPricings = new List<ProductionPricing>();
            this.ProductionProcesses = new List<ProductionProcess>();
            this.ProductionProcessByMachines = new List<ProductionProcessByMachine>();
            this.ProductionProductivityQuoteBases = new List<ProductionProductivityQuoteBase>();
            this.ProductionSections = new List<ProductionSection>();
            this.ProductionTestings = new List<ProductionTesting>();
            this.ProductionTestingNotes = new List<ProductionTestingNote>();
            this.ProductionTools = new List<ProductionTool>();
            this.RealProductions = new List<RealProduction>();
            this.RealTestings = new List<RealTesting>();
            this.SectionProcessDetails = new List<SectionProcessDetail>();
            this.SectionProcessInventories = new List<SectionProcessInventory>();
            this.SmartProductions = new List<SmartProduction>();
            this.SmartProduction2 = new List<SmartProduction2>();
            this.TrackUpMachines = new List<TrackUpMachine>();
            this.WorkOrders = new List<WorkOrder>();
            this.WorkOrderRoutings = new List<WorkOrderRouting>();
            this.ExportFormQC_TPDetail = new List<ExportFormQC_TPDetail>();
            this.ExportFormTP_KDDetail = new List<ExportFormTP_KDDetail>();
            this.ExportGCN_NCUDetail = new List<ExportGCN_NCUDetail>();
            this.ExportToolDetails = new List<ExportToolDetail>();
            this.ImportFormCncDetails = new List<ImportFormCncDetail>();
            this.ImportFormSX1Detail = new List<ImportFormSX1Detail>();
            this.ImportNCU_QCBDetail = new List<ImportNCU_QCBDetail>();
            this.ProductInventories = new List<ProductInventory>();
            this.ProductInventoryPeriods = new List<ProductInventoryPeriod>();
            this.ProductTotalByMonths = new List<ProductTotalByMonth>();
            this.StockOrderDetails = new List<StockOrderDetail>();
            this.TransactionDetails = new List<TransactionDetail>();
            this.TransactionProducts = new List<TransactionProduct>();
            this.TransactionWeighings = new List<TransactionWeighing>();
            this.OrderProgresses = new List<OrderProgress>();
            this.ForecastOrders = new List<ForecastOrder>();
            this.InvoiceDetails = new List<InvoiceDetail>();
            this.PlatingFormDetails = new List<PlatingFormDetail>();
            this.ProductAdditionFees = new List<ProductAdditionFee>();
            this.ProductCombinationRecipes = new List<ProductCombinationRecipe>();
            this.ProductCombinationRecipeDetails = new List<ProductCombinationRecipeDetail>();
            this.ProductChanges = new List<ProductChange>();
            this.ProductHistories = new List<ProductHistory>();
            this.QuoteDetails = new List<QuoteDetail>();
            this.OrderDetails = new List<OrderDetail>();
            this.TaxInvoiceProducts = new List<TaxInvoiceProduct>();
        }

        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public Nullable<int> MaterialId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string DesignNo { get; set; }
        public Nullable<double> Diameter { get; set; }
        public Nullable<double> Length { get; set; }
        public Nullable<double> Weight { get; set; }
        public bool Active { get; set; }
        public string ModifiedUser { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<double> ForecastsQuality { get; set; }
        public Nullable<double> UnitPrice { get; set; }
        public Nullable<double> ProductionWeight { get; set; }
        public Nullable<int> ProductionRate { get; set; }
        public Nullable<double> Productivity { get; set; }
        public Nullable<double> SaleFactor { get; set; }
        public Nullable<double> ProductionFactor { get; set; }
        public Nullable<double> CncWeight { get; set; }
        public Nullable<double> Production2Weight { get; set; }
        public Nullable<double> WaitingPlatingWeight { get; set; }
        public Nullable<double> PlatingWeight { get; set; }
        public Nullable<double> QcWeight { get; set; }
        public Nullable<double> FinishWeight { get; set; }
        public Nullable<double> OutsideProcessWeight { get; set; }
        public Nullable<double> HeatTreatmentWeight { get; set; }
        public Nullable<double> SurfaceTreatmentWeight { get; set; }
        public Nullable<bool> IsSelling { get; set; }
        public string MaterialNameDesign { get; set; }
        public Nullable<double> OutDiameterDesign { get; set; }
        public string OutDiameterTolerance { get; set; }
        public Nullable<double> InDiameterDesign { get; set; }
        public string InDiameterTolerance { get; set; }
        public string ShapeDesign { get; set; }
        public Nullable<System.DateTime> NewUpdateDate { get; set; }
        public Nullable<double> LastSaleFactor { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public Nullable<double> LastMonthSaleFactor { get; set; }
        public Nullable<System.DateTime> LastMonthUpdateDate { get; set; }
        public Nullable<System.DateTime> FinishProcessingDate { get; set; }
        public string Drawing2D { get; set; }
        public Nullable<byte> Status { get; set; }
        public Nullable<double> Diff { get; set; }
        public Nullable<int> ProcessingDesign { get; set; }
        public Nullable<double> ProcessingCost { get; set; }
        public string DiameterTypeDesign { get; set; }
        public string DrawingFinish { get; set; }
        public Nullable<double> KnifeCut { get; set; }
        public string UploadUser { get; set; }
        public Nullable<System.DateTime> UploadDate { get; set; }
        public Nullable<double> MillProductivity { get; set; }
        public Nullable<double> MaterialCost { get; set; }
        public double DiffRequestInv { get; set; }
        public double QcProductivity { get; set; }
        public string ProductShape { get; set; }
        public Nullable<double> CncProductivity { get; set; }
        public string TaxCode { get; set; }
        public bool FinishDesign { get; set; }
        public int MachineFunction { get; set; }
        public string Note { get; set; }
        public string Currency { get; set; }
        public Nullable<int> BaseProductId { get; set; }
        public int MaxQuantityInTray { get; set; }
        public int MaxQuantityInTrayRunTime { get; set; }
        public string IdentityCode { get; set; }
        public Nullable<int> ProductionLevel { get; set; }
        public Nullable<double> PackingFee { get; set; }
        public Nullable<int> ProcessClassifiedId { get; set; }
        public Nullable<bool> IsCalculateLock { get; set; }
        public Nullable<int> ProductionLossRate { get; set; }
        public virtual ICollection<ProductionPlan> ProductionPlans { get; set; }
        public virtual ICollection<DefectTransactionDetail> DefectTransactionDetails { get; set; }
        public virtual ICollection<MachineAppraisal> MachineAppraisals { get; set; }
        public virtual ICollection<MachineRepairForm> MachineRepairForms { get; set; }
        public virtual ICollection<MaterialLimitPlan> MaterialLimitPlans { get; set; }
        public virtual ProcessClassified ProcessClassified { get; set; }
        public virtual ProcessingType ProcessingType { get; set; }
        public virtual ICollection<ProductImg> ProductImgs { get; set; }
        public virtual ICollection<QcImg> QcImgs { get; set; }
        public virtual ICollection<Production2TransactionDetail> Production2TransactionDetail { get; set; }
        public virtual ICollection<ProductionDefect> ProductionDefects { get; set; }
        public virtual ICollection<ProductionFuel> ProductionFuels { get; set; }
        public virtual ICollection<ProductionHeatTreatment> ProductionHeatTreatments { get; set; }
        public virtual ICollection<ProductionMaterial> ProductionMaterials { get; set; }
        public virtual ICollection<ProductionPlating> ProductionPlatings { get; set; }
        public virtual ICollection<ProductionPolish> ProductionPolishes { get; set; }
        public virtual ICollection<ProductionPricing> ProductionPricings { get; set; }
        public virtual ICollection<ProductionProcess> ProductionProcesses { get; set; }
        public virtual ICollection<ProductionProcessByMachine> ProductionProcessByMachines { get; set; }
        public virtual ICollection<ProductionProductivityQuoteBase> ProductionProductivityQuoteBases { get; set; }
        public virtual ProductionProductLevel ProductionProductLevel { get; set; }
        public virtual ICollection<ProductionSection> ProductionSections { get; set; }
        public virtual ICollection<ProductionTesting> ProductionTestings { get; set; }
        public virtual ICollection<ProductionTestingNote> ProductionTestingNotes { get; set; }
        public virtual ICollection<ProductionTool> ProductionTools { get; set; }
        public virtual ICollection<RealProduction> RealProductions { get; set; }
        public virtual ICollection<RealTesting> RealTestings { get; set; }
        public virtual ICollection<SectionProcessDetail> SectionProcessDetails { get; set; }
        public virtual ICollection<SectionProcessInventory> SectionProcessInventories { get; set; }
        public virtual ICollection<SmartProduction> SmartProductions { get; set; }
        public virtual ICollection<SmartProduction2> SmartProduction2 { get; set; }
        public virtual ICollection<TrackUpMachine> TrackUpMachines { get; set; }
        public virtual ICollection<WorkOrder> WorkOrders { get; set; }
        public virtual ICollection<WorkOrderRouting> WorkOrderRoutings { get; set; }
        public virtual ICollection<ExportFormQC_TPDetail> ExportFormQC_TPDetail { get; set; }
        public virtual ICollection<ExportFormTP_KDDetail> ExportFormTP_KDDetail { get; set; }
        public virtual ICollection<ExportGCN_NCUDetail> ExportGCN_NCUDetail { get; set; }
        public virtual ICollection<ExportToolDetail> ExportToolDetails { get; set; }
        public virtual ICollection<ImportFormCncDetail> ImportFormCncDetails { get; set; }
        public virtual ICollection<ImportFormSX1Detail> ImportFormSX1Detail { get; set; }
        public virtual ICollection<ImportNCU_QCBDetail> ImportNCU_QCBDetail { get; set; }
        public virtual ICollection<ProductInventory> ProductInventories { get; set; }
        public virtual ICollection<ProductInventoryPeriod> ProductInventoryPeriods { get; set; }
        public virtual ICollection<ProductTotalByMonth> ProductTotalByMonths { get; set; }
        public virtual ICollection<StockOrderDetail> StockOrderDetails { get; set; }
        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }
        public virtual ICollection<TransactionProduct> TransactionProducts { get; set; }
        public virtual ICollection<TransactionWeighing> TransactionWeighings { get; set; }
        public virtual Material Material { get; set; }
        public virtual ICollection<OrderProgress> OrderProgresses { get; set; }
        public virtual ICollection<ForecastOrder> ForecastOrders { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual ICollection<PlatingFormDetail> PlatingFormDetails { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<ProductAdditionFee> ProductAdditionFees { get; set; }
        public virtual ICollection<ProductCombinationRecipe> ProductCombinationRecipes { get; set; }
        public virtual ICollection<ProductCombinationRecipeDetail> ProductCombinationRecipeDetails { get; set; }
        public virtual ICollection<ProductChange> ProductChanges { get; set; }
        public virtual ICollection<ProductHistory> ProductHistories { get; set; }
        public virtual ICollection<QuoteDetail> QuoteDetails { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<TaxInvoiceProduct> TaxInvoiceProducts { get; set; }
    }
}
