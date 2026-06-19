using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Vfi.Ui.Mvc.Vfi.Models.Mapping;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public partial class vfiContext : DbContext
    {
        static vfiContext()
        {
            Database.SetInitializer<vfiContext>(null);
        }

        public vfiContext()
            : base("Name=vfiContext")
        {
        }

        public DbSet<ProductionPlan> ProductionPlans { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<DefectInventory> DefectInventories { get; set; }
        public DbSet<DefectInventoryPeriod> DefectInventoryPeriods { get; set; }
        public DbSet<DefectTransaction> DefectTransactions { get; set; }
        public DbSet<DefectTransactionDetail> DefectTransactionDetails { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ErrorCauseForm> ErrorCauseForms { get; set; }
        public DbSet<Fuel> Fuels { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<MachineAppraisal> MachineAppraisals { get; set; }
        public DbSet<MachineLog> MachineLogs { get; set; }
        public DbSet<MachineRepairForm> MachineRepairForms { get; set; }
        public DbSet<MachineState> MachineStates { get; set; }
        public DbSet<MachineStateDetail> MachineStateDetails { get; set; }
        public DbSet<MaterialLimitPlan> MaterialLimitPlans { get; set; }
        public DbSet<OutsideProcess> OutsideProcesses { get; set; }
        public DbSet<ProcessClassified> ProcessClassifieds { get; set; }
        public DbSet<ProcessingType> ProcessingTypes { get; set; }
        public DbSet<ProductImg> ProductImgs { get; set; }
        public DbSet<QcImg> QcImgs { get; set; }
        public DbSet<Production2Inventory> Production2Inventory { get; set; }
        public DbSet<Production2InventoryPeriod> Production2InventoryPeriod { get; set; }
        public DbSet<Production2Transaction> Production2Transaction { get; set; }
        public DbSet<Production2TransactionDetail> Production2TransactionDetail { get; set; }
        public DbSet<ProductionDefect> ProductionDefects { get; set; }
        public DbSet<ProductionDefectRemedy> ProductionDefectRemedies { get; set; }
        public DbSet<ProductionDefectType> ProductionDefectTypes { get; set; }
        public DbSet<ProductionFuel> ProductionFuels { get; set; }
        public DbSet<ProductionHeatTreatment> ProductionHeatTreatments { get; set; }
        public DbSet<ProductionLock> ProductionLocks { get; set; }
        public DbSet<ProductionMaterial> ProductionMaterials { get; set; }
        public DbSet<ProductionPlating> ProductionPlatings { get; set; }
        public DbSet<ProductionPolish> ProductionPolishes { get; set; }
        public DbSet<ProductionPricing> ProductionPricings { get; set; }
        public DbSet<ProductionProcess> ProductionProcesses { get; set; }
        public DbSet<ProductionProcessByMachine> ProductionProcessByMachines { get; set; }
        public DbSet<ProductionProductivityQuoteBase> ProductionProductivityQuoteBases { get; set; }
        public DbSet<ProductionProductLevel> ProductionProductLevels { get; set; }
        public DbSet<ProductionSection> ProductionSections { get; set; }
        public DbSet<ProductionSectionProcess> ProductionSectionProcesses { get; set; }
        public DbSet<ProductionTesting> ProductionTestings { get; set; }
        public DbSet<ProductionTestingDetail> ProductionTestingDetails { get; set; }
        public DbSet<ProductionTestingMachine> ProductionTestingMachines { get; set; }
        public DbSet<ProductionTestingNote> ProductionTestingNotes { get; set; }
        public DbSet<ProductionTool> ProductionTools { get; set; }
        public DbSet<ProductionToolReplacement> ProductionToolReplacements { get; set; }
        public DbSet<RealProduction> RealProductions { get; set; }
        public DbSet<RealTesting> RealTestings { get; set; }
        public DbSet<RepairFormDetail> RepairFormDetails { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<SectionLog> SectionLogs { get; set; }
        public DbSet<SectionProcess> SectionProcesses { get; set; }
        public DbSet<SectionProcessDetail> SectionProcessDetails { get; set; }
        public DbSet<SectionProcessInventory> SectionProcessInventories { get; set; }
        public DbSet<SectionProcessPeriod> SectionProcessPeriods { get; set; }
        public DbSet<SectionProcessTransaction> SectionProcessTransactions { get; set; }
        public DbSet<SectionProcessTransactionDetail> SectionProcessTransactionDetails { get; set; }
        public DbSet<SmartProduction> SmartProductions { get; set; }
        public DbSet<SmartProduction2> SmartProduction2 { get; set; }
        public DbSet<Tool> Tools { get; set; }
        public DbSet<ToolDetail> ToolDetails { get; set; }
        public DbSet<ToolInventoryRequirement> ToolInventoryRequirements { get; set; }
        public DbSet<ToolOrder> ToolOrders { get; set; }
        public DbSet<ToolPeriod> ToolPeriods { get; set; }
        public DbSet<TrackingRepairEmployee> TrackingRepairEmployees { get; set; }
        public DbSet<TrackUpMachine> TrackUpMachines { get; set; }
        public DbSet<TrackUpMaterial> TrackUpMaterials { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<WorkOrderProcess> WorkOrderProcesses { get; set; }
        public DbSet<WorkOrderRouting> WorkOrderRoutings { get; set; }
        public DbSet<WorkpieceMaterialPeriod> WorkpieceMaterialPeriods { get; set; }
        public DbSet<ExportChangeProduct> ExportChangeProducts { get; set; }
        public DbSet<ExportFormQC_TP> ExportFormQC_TP { get; set; }
        public DbSet<ExportFormQC_TPDetail> ExportFormQC_TPDetail { get; set; }
        public DbSet<ExportFormTP_KD> ExportFormTP_KD { get; set; }
        public DbSet<ExportFormTP_KDDetail> ExportFormTP_KDDetail { get; set; }
        public DbSet<ExportGCN_NCU> ExportGCN_NCU { get; set; }
        public DbSet<ExportGCN_NCUDetail> ExportGCN_NCUDetail { get; set; }
        public DbSet<ExportMaterial> ExportMaterials { get; set; }
        public DbSet<ExportMaterialDetail> ExportMaterialDetails { get; set; }
        public DbSet<ExportTool> ExportTools { get; set; }
        public DbSet<ExportToolDetail> ExportToolDetails { get; set; }
        public DbSet<ExportWorkpieceMaterial> ExportWorkpieceMaterials { get; set; }
        public DbSet<ExportWorkpieceMaterialDetail> ExportWorkpieceMaterialDetails { get; set; }
        public DbSet<FuelInventory> FuelInventories { get; set; }
        public DbSet<FuelInventoryPeriod> FuelInventoryPeriods { get; set; }
        public DbSet<ImportFormCnc> ImportFormCncs { get; set; }
        public DbSet<ImportFormCncDetail> ImportFormCncDetails { get; set; }
        public DbSet<ImportFormSX1> ImportFormSX1 { get; set; }
        public DbSet<ImportFormSX1Detail> ImportFormSX1Detail { get; set; }
        public DbSet<ImportNCU_QCB> ImportNCU_QCB { get; set; }
        public DbSet<ImportNCU_QCBDetail> ImportNCU_QCBDetail { get; set; }
        public DbSet<ImportWorkpieceMaterial> ImportWorkpieceMaterials { get; set; }
        public DbSet<ImportWorkpieceMaterialDetail> ImportWorkpieceMaterialDetails { get; set; }
        public DbSet<InventoryDrawer> InventoryDrawers { get; set; }
        public DbSet<InventoryShelf> InventoryShelves { get; set; }
        public DbSet<MaterialInventory> MaterialInventories { get; set; }
        public DbSet<MaterialInventoryPeriod> MaterialInventoryPeriods { get; set; }
        public DbSet<MaterialInvOnMachine> MaterialInvOnMachines { get; set; }
        public DbSet<MaterialInvOnMachinePeriod> MaterialInvOnMachinePeriods { get; set; }
        public DbSet<MaterialUseDetail> MaterialUseDetails { get; set; }
        public DbSet<MaterialUseInShift> MaterialUseInShifts { get; set; }
        public DbSet<OnShelf> OnShelves { get; set; }
        public DbSet<ProcessError> ProcessErrors { get; set; }
        public DbSet<ProductInventory> ProductInventories { get; set; }
        public DbSet<ProductInventoryPeriod> ProductInventoryPeriods { get; set; }
        public DbSet<ProductTotalByMonth> ProductTotalByMonths { get; set; }
        public DbSet<StockOrder> StockOrders { get; set; }
        public DbSet<StockOrderDetail> StockOrderDetails { get; set; }
        public DbSet<StockOrderType> StockOrderTypes { get; set; }
        public DbSet<ToolInventory> ToolInventories { get; set; }
        public DbSet<ToolInventoryPeriod> ToolInventoryPeriods { get; set; }
        public DbSet<ToolInvOnMachine> ToolInvOnMachines { get; set; }
        public DbSet<ToolInvOnMachinePeriod> ToolInvOnMachinePeriods { get; set; }
        public DbSet<ToolUse> ToolUses { get; set; }
        public DbSet<ToolUseDetail> ToolUseDetails { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }
        public DbSet<TransactionProduct> TransactionProducts { get; set; }
        public DbSet<TransactionWeighing> TransactionWeighings { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<WarehouseType> WarehouseTypes { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialClassified> MaterialClassifieds { get; set; }
        public DbSet<MaterialQuoteBase> MaterialQuoteBases { get; set; }
        public DbSet<MaterialQuoteBaseDetail> MaterialQuoteBaseDetails { get; set; }
        public DbSet<MaterialType> MaterialTypes { get; set; }
        public DbSet<OrderProgress> OrderProgresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAdditionFee> ProductAdditionFees { get; set; }
        public DbSet<ProductCombinationRecipe> ProductCombinationRecipes { get; set; }
        public DbSet<ProductCombinationRecipeDetail> ProductCombinationRecipeDetails { get; set; }
        public DbSet<ProductChange> ProductChanges { get; set; }
        public DbSet<ProductHistory> ProductHistories { get; set; }
        public DbSet<UnitMeasure> UnitMeasures { get; set; }
        public DbSet<WeighingMachine> WeighingMachines { get; set; }
        public DbSet<ImportPurchaseOrder> ImportPurchaseOrders { get; set; }
        public DbSet<ImportPurchaseOrderDetail> ImportPurchaseOrderDetails { get; set; }
        public DbSet<InquiryPo> InquiryPoes { get; set; }
        public DbSet<Method> Methods { get; set; }
        public DbSet<MethodType> MethodTypes { get; set; }
        public DbSet<PlatingForm> PlatingForms { get; set; }
        public DbSet<PlatingFormDetail> PlatingFormDetails { get; set; }
        public DbSet<PoTaxInvoice> PoTaxInvoices { get; set; }
        public DbSet<PoTaxInvoiceMoney> PoTaxInvoiceMoneys { get; set; }
        public DbSet<PoTaxInvoiceReference> PoTaxInvoiceReferences { get; set; }
        public DbSet<PoTaxInvoiceReferenceDetail> PoTaxInvoiceReferenceDetails { get; set; }
        public DbSet<PriceListMaterial> PriceListMaterials { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public DbSet<ScrapReason> ScrapReasons { get; set; }
        public DbSet<ShipMethod> ShipMethods { get; set; }
        public DbSet<TransactionFpt> TransactionFpts { get; set; }
        public DbSet<TransactionFptDetail> TransactionFptDetails { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<WorkOrder1> WorkOrder1 { get; set; }
        public DbSet<WorkOrderRouting1> WorkOrderRouting1 { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAccessPermission> CustomerAccessPermissions { get; set; }
        public DbSet<CustomerClassified> CustomerClassifieds { get; set; }
        public DbSet<CustomerPayType> CustomerPayTypes { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ForecastOrder> ForecastOrders { get; set; }
        public DbSet<ForecastOrderDetail> ForecastOrderDetails { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<MOQTemplate> MOQTemplates { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderNote> OrderNotes { get; set; }
        public DbSet<OrderNoteDetail> OrderNoteDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<PaymentTerm> PaymentTerms { get; set; }
        public DbSet<QuoteDetail> QuoteDetails { get; set; }
        public DbSet<QuoteForm> QuoteForms { get; set; }
        public DbSet<TaxInvoice> TaxInvoices { get; set; }
        public DbSet<TaxInvoiceDetail> TaxInvoiceDetails { get; set; }
        public DbSet<TaxInvoiceProduct> TaxInvoiceProducts { get; set; }
        public DbSet<TaxInvoiceProductDetail> TaxInvoiceProductDetails { get; set; }
        public DbSet<TimeLine> TimeLines { get; set; }
        public DbSet<ContextLog> ContextLogs { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserWorkGroup> UserWorkGroups { get; set; }
        public DbSet<WarehousePermission> WarehousePermissions { get; set; }
        public DbSet<WarehouseRotate> WarehouseRotates { get; set; }
        public DbSet<WorkGroup> WorkGroups { get; set; }
        public DbSet<SelectApprovedProduction> SelectApprovedProductions { get; set; }
        public DbSet<SelectCurrentProductionTool> SelectCurrentProductionTools { get; set; }
        public DbSet<SelectCurrentTrackUpMachine> SelectCurrentTrackUpMachines { get; set; }
        public DbSet<SelectDiagram1> SelectDiagram1 { get; set; }
        public DbSet<SelectDiagram2> SelectDiagram2 { get; set; }
        public DbSet<SelectDiagram3> SelectDiagram3 { get; set; }
        public DbSet<SelectFuelInvAndPeriod> SelectFuelInvAndPeriods { get; set; }
        public DbSet<SelectMaterialInvAndPeriod> SelectMaterialInvAndPeriods { get; set; }
        public DbSet<SelectMaterialInvOnMachineAndPeriod> SelectMaterialInvOnMachineAndPeriods { get; set; }
        public DbSet<SelectProductInventoryAndPeriod> SelectProductInventoryAndPeriods { get; set; }
        public DbSet<SelectToolInvAndPeriod> SelectToolInvAndPeriods { get; set; }
        public DbSet<TransactionImg> TransactionImgs { get; set; }
        public DbSet<DeliveryAddress> DeliveryAddresses { get; set; }
        public DbSet<MaterialImg> MaterialImgs { get; set; }
        public DbSet<EvaluationForm> EvaluationForms { get; set; }
        public DbSet<VendorImg> VendorImgs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductionPlanMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new DefectInventoryMap());
            modelBuilder.Configurations.Add(new DefectInventoryPeriodMap());
            modelBuilder.Configurations.Add(new DefectTransactionMap());
            modelBuilder.Configurations.Add(new DefectTransactionDetailMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new DeliveryAddressMap());
            modelBuilder.Configurations.Add(new ErrorCauseFormMap());
            modelBuilder.Configurations.Add(new FuelMap());
            modelBuilder.Configurations.Add(new MachineMap());
            modelBuilder.Configurations.Add(new MachineAppraisalMap());
            modelBuilder.Configurations.Add(new MachineLogMap());
            modelBuilder.Configurations.Add(new MachineRepairFormMap());
            modelBuilder.Configurations.Add(new MachineStateMap());
            modelBuilder.Configurations.Add(new MachineStateDetailMap());
            modelBuilder.Configurations.Add(new MaterialLimitPlanMap());
            modelBuilder.Configurations.Add(new OutsideProcessMap());
            modelBuilder.Configurations.Add(new ProcessClassifiedMap());
            modelBuilder.Configurations.Add(new ProcessingTypeMap());
            modelBuilder.Configurations.Add(new ProductImgMap());
            modelBuilder.Configurations.Add(new QcImgMap());
            modelBuilder.Configurations.Add(new MaterialImgMap());
            modelBuilder.Configurations.Add(new Production2InventoryMap());
            modelBuilder.Configurations.Add(new Production2InventoryPeriodMap());
            modelBuilder.Configurations.Add(new Production2TransactionMap());
            modelBuilder.Configurations.Add(new Production2TransactionDetailMap());
            modelBuilder.Configurations.Add(new ProductionDefectMap());
            modelBuilder.Configurations.Add(new ProductionDefectRemedyMap());
            modelBuilder.Configurations.Add(new ProductionDefectTypeMap());
            modelBuilder.Configurations.Add(new ProductionFuelMap());
            modelBuilder.Configurations.Add(new ProductionHeatTreatmentMap());
            modelBuilder.Configurations.Add(new ProductionLockMap());
            modelBuilder.Configurations.Add(new ProductionMaterialMap());
            modelBuilder.Configurations.Add(new ProductionPlatingMap());
            modelBuilder.Configurations.Add(new ProductionPolishMap());
            modelBuilder.Configurations.Add(new ProductionPricingMap());
            modelBuilder.Configurations.Add(new ProductionProcessMap());
            modelBuilder.Configurations.Add(new ProductionProcessByMachineMap());
            modelBuilder.Configurations.Add(new ProductionProductivityQuoteBaseMap());
            modelBuilder.Configurations.Add(new ProductionProductLevelMap());
            modelBuilder.Configurations.Add(new ProductionSectionMap());
            modelBuilder.Configurations.Add(new ProductionSectionProcessMap());
            modelBuilder.Configurations.Add(new ProductionTestingMap());
            modelBuilder.Configurations.Add(new ProductionTestingDetailMap());
            modelBuilder.Configurations.Add(new ProductionTestingMachineMap());
            modelBuilder.Configurations.Add(new ProductionTestingNoteMap());
            modelBuilder.Configurations.Add(new ProductionToolMap());
            modelBuilder.Configurations.Add(new ProductionToolReplacementMap());
            modelBuilder.Configurations.Add(new RealProductionMap());
            modelBuilder.Configurations.Add(new RealTestingMap());
            modelBuilder.Configurations.Add(new RepairFormDetailMap());
            modelBuilder.Configurations.Add(new SectionMap());
            modelBuilder.Configurations.Add(new SectionLogMap());
            modelBuilder.Configurations.Add(new SectionProcessMap());
            modelBuilder.Configurations.Add(new SectionProcessDetailMap());
            modelBuilder.Configurations.Add(new SectionProcessInventoryMap());
            modelBuilder.Configurations.Add(new SectionProcessPeriodMap());
            modelBuilder.Configurations.Add(new SectionProcessTransactionMap());
            modelBuilder.Configurations.Add(new SectionProcessTransactionDetailMap());
            modelBuilder.Configurations.Add(new SmartProductionMap());
            modelBuilder.Configurations.Add(new SmartProduction2Map());
            modelBuilder.Configurations.Add(new ToolMap());
            modelBuilder.Configurations.Add(new ToolDetailMap());
            modelBuilder.Configurations.Add(new ToolInventoryRequirementMap());
            modelBuilder.Configurations.Add(new ToolOrderMap());
            modelBuilder.Configurations.Add(new ToolPeriodMap());
            modelBuilder.Configurations.Add(new TrackingRepairEmployeeMap());
            modelBuilder.Configurations.Add(new TrackUpMachineMap());
            modelBuilder.Configurations.Add(new TrackUpMaterialMap());
            modelBuilder.Configurations.Add(new WorkOrderMap());
            modelBuilder.Configurations.Add(new WorkOrderProcessMap());
            modelBuilder.Configurations.Add(new WorkOrderRoutingMap());
            modelBuilder.Configurations.Add(new WorkpieceMaterialPeriodMap());
            modelBuilder.Configurations.Add(new EvaluationFormMap());
            modelBuilder.Configurations.Add(new ExportChangeProductMap());
            modelBuilder.Configurations.Add(new ExportFormQC_TPMap());
            modelBuilder.Configurations.Add(new ExportFormQC_TPDetailMap());
            modelBuilder.Configurations.Add(new ExportFormTP_KDMap());
            modelBuilder.Configurations.Add(new ExportFormTP_KDDetailMap());
            modelBuilder.Configurations.Add(new ExportGCN_NCUMap());
            modelBuilder.Configurations.Add(new ExportGCN_NCUDetailMap());
            modelBuilder.Configurations.Add(new ExportMaterialMap());
            modelBuilder.Configurations.Add(new ExportMaterialDetailMap());
            modelBuilder.Configurations.Add(new ExportToolMap());
            modelBuilder.Configurations.Add(new ExportToolDetailMap());
            modelBuilder.Configurations.Add(new ExportWorkpieceMaterialMap());
            modelBuilder.Configurations.Add(new ExportWorkpieceMaterialDetailMap());
            modelBuilder.Configurations.Add(new FuelInventoryMap());
            modelBuilder.Configurations.Add(new FuelInventoryPeriodMap());
            modelBuilder.Configurations.Add(new ImportFormCncMap());
            modelBuilder.Configurations.Add(new ImportFormCncDetailMap());
            modelBuilder.Configurations.Add(new ImportFormSX1Map());
            modelBuilder.Configurations.Add(new ImportFormSX1DetailMap());
            modelBuilder.Configurations.Add(new ImportNCU_QCBMap());
            modelBuilder.Configurations.Add(new ImportNCU_QCBDetailMap());
            modelBuilder.Configurations.Add(new ImportWorkpieceMaterialMap());
            modelBuilder.Configurations.Add(new ImportWorkpieceMaterialDetailMap());
            modelBuilder.Configurations.Add(new InventoryDrawerMap());
            modelBuilder.Configurations.Add(new InventoryShelfMap());
            modelBuilder.Configurations.Add(new MaterialInventoryMap());
            modelBuilder.Configurations.Add(new MaterialInventoryPeriodMap());
            modelBuilder.Configurations.Add(new MaterialInvOnMachineMap());
            modelBuilder.Configurations.Add(new MaterialInvOnMachinePeriodMap());
            modelBuilder.Configurations.Add(new MaterialUseDetailMap());
            modelBuilder.Configurations.Add(new MaterialUseInShiftMap());
            modelBuilder.Configurations.Add(new OnShelfMap());
            modelBuilder.Configurations.Add(new ProcessErrorMap());
            modelBuilder.Configurations.Add(new ProductInventoryMap());
            modelBuilder.Configurations.Add(new ProductInventoryPeriodMap());
            modelBuilder.Configurations.Add(new ProductTotalByMonthMap());
            modelBuilder.Configurations.Add(new StockOrderMap());
            modelBuilder.Configurations.Add(new StockOrderDetailMap());
            modelBuilder.Configurations.Add(new StockOrderTypeMap());
            modelBuilder.Configurations.Add(new ToolInventoryMap());
            modelBuilder.Configurations.Add(new ToolInventoryPeriodMap());
            modelBuilder.Configurations.Add(new ToolInvOnMachineMap());
            modelBuilder.Configurations.Add(new ToolInvOnMachinePeriodMap());
            modelBuilder.Configurations.Add(new ToolUseMap());
            modelBuilder.Configurations.Add(new ToolUseDetailMap());
            modelBuilder.Configurations.Add(new TransactionMap());
            modelBuilder.Configurations.Add(new TransactionDetailMap());
            modelBuilder.Configurations.Add(new TransactionProductMap());
            modelBuilder.Configurations.Add(new TransactionWeighingMap());
            modelBuilder.Configurations.Add(new WarehouseMap());
            modelBuilder.Configurations.Add(new WarehouseTypeMap());
            modelBuilder.Configurations.Add(new CurrencyMap());
            modelBuilder.Configurations.Add(new MaterialMap());
            modelBuilder.Configurations.Add(new MaterialClassifiedMap());
            modelBuilder.Configurations.Add(new MaterialQuoteBaseMap());
            modelBuilder.Configurations.Add(new MaterialQuoteBaseDetailMap());
            modelBuilder.Configurations.Add(new MaterialTypeMap());
            modelBuilder.Configurations.Add(new OrderProgressMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ProductAdditionFeeMap());
            modelBuilder.Configurations.Add(new ProductCombinationRecipeMap());
            modelBuilder.Configurations.Add(new ProductCombinationRecipeDetailMap());
            modelBuilder.Configurations.Add(new ProductChangeMap());
            modelBuilder.Configurations.Add(new ProductHistoryMap());
            modelBuilder.Configurations.Add(new UnitMeasureMap());
            modelBuilder.Configurations.Add(new WeighingMachineMap());
            modelBuilder.Configurations.Add(new ImportPurchaseOrderMap());
            modelBuilder.Configurations.Add(new ImportPurchaseOrderDetailMap());
            modelBuilder.Configurations.Add(new InquiryPoMap());
            modelBuilder.Configurations.Add(new TransactionImgMap());
            modelBuilder.Configurations.Add(new MethodMap());
            modelBuilder.Configurations.Add(new MethodTypeMap());
            modelBuilder.Configurations.Add(new PlatingFormMap());
            modelBuilder.Configurations.Add(new PlatingFormDetailMap());
            modelBuilder.Configurations.Add(new PoTaxInvoiceMap());
            modelBuilder.Configurations.Add(new PoTaxInvoiceMoneyMap());
            modelBuilder.Configurations.Add(new PoTaxInvoiceReferenceMap());
            modelBuilder.Configurations.Add(new PoTaxInvoiceReferenceDetailMap());
            modelBuilder.Configurations.Add(new PriceListMaterialMap());
            modelBuilder.Configurations.Add(new PurchaseOrderMap());
            modelBuilder.Configurations.Add(new PurchaseOrderDetailMap());
            modelBuilder.Configurations.Add(new ScrapReasonMap());
            modelBuilder.Configurations.Add(new ShipMethodMap());
            modelBuilder.Configurations.Add(new TransactionFptMap());
            modelBuilder.Configurations.Add(new TransactionFptDetailMap());
            modelBuilder.Configurations.Add(new VendorMap());
            modelBuilder.Configurations.Add(new WorkOrder1Map());
            modelBuilder.Configurations.Add(new WorkOrderRouting1Map());
            modelBuilder.Configurations.Add(new AreaMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new CustomerAccessPermissionMap());
            modelBuilder.Configurations.Add(new CustomerClassifiedMap());
            modelBuilder.Configurations.Add(new CustomerPayTypeMap());
            modelBuilder.Configurations.Add(new CustomerTypeMap());
            modelBuilder.Configurations.Add(new EmployeeMap());
            modelBuilder.Configurations.Add(new ForecastOrderMap());
            modelBuilder.Configurations.Add(new ForecastOrderDetailMap());
            modelBuilder.Configurations.Add(new InvoiceMap());
            modelBuilder.Configurations.Add(new InvoiceDetailMap());
            modelBuilder.Configurations.Add(new MOQTemplateMap());
            modelBuilder.Configurations.Add(new OrderDetailMap());
            modelBuilder.Configurations.Add(new OrderNoteMap());
            modelBuilder.Configurations.Add(new OrderNoteDetailMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new OrderStatusMap());
            modelBuilder.Configurations.Add(new PaymentTermMap());
            modelBuilder.Configurations.Add(new QuoteDetailMap());
            modelBuilder.Configurations.Add(new QuoteFormMap());
            modelBuilder.Configurations.Add(new TaxInvoiceMap());
            modelBuilder.Configurations.Add(new TaxInvoiceDetailMap());
            modelBuilder.Configurations.Add(new TaxInvoiceProductMap());
            modelBuilder.Configurations.Add(new TaxInvoiceProductDetailMap());
            modelBuilder.Configurations.Add(new TimeLineMap());
            modelBuilder.Configurations.Add(new VendorImgMap());
            modelBuilder.Configurations.Add(new ContextLogMap());
            modelBuilder.Configurations.Add(new FunctionMap());
            modelBuilder.Configurations.Add(new MenuMap());
            modelBuilder.Configurations.Add(new ParameterMap());
            modelBuilder.Configurations.Add(new PermissionMap());
            modelBuilder.Configurations.Add(new StatusMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserWorkGroupMap());
            modelBuilder.Configurations.Add(new WarehousePermissionMap());
            modelBuilder.Configurations.Add(new WarehouseRotateMap());
            modelBuilder.Configurations.Add(new WorkGroupMap());
            modelBuilder.Configurations.Add(new SelectApprovedProductionMap());
            modelBuilder.Configurations.Add(new SelectCurrentProductionToolMap());
            modelBuilder.Configurations.Add(new SelectCurrentTrackUpMachineMap());
            modelBuilder.Configurations.Add(new SelectDiagram1Map());
            modelBuilder.Configurations.Add(new SelectDiagram2Map());
            modelBuilder.Configurations.Add(new SelectDiagram3Map());
            modelBuilder.Configurations.Add(new SelectFuelInvAndPeriodMap());
            modelBuilder.Configurations.Add(new SelectMaterialInvAndPeriodMap());
            modelBuilder.Configurations.Add(new SelectMaterialInvOnMachineAndPeriodMap());
            modelBuilder.Configurations.Add(new SelectProductInventoryAndPeriodMap());
            modelBuilder.Configurations.Add(new SelectToolInvAndPeriodMap());
        }
    }
}
