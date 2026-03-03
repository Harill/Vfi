using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Telerik.Web.Mvc;
using Telerik.Web.Mvc.Extensions;
using Vfi.Server.Core.CrossCutting.UnitOfWork;
using Vfi.Ui.Mvc.Vfi.Models.Production;
using Vfi.Ui.Mvc.Vfi.Areas.Factory.Models;
using Vfi.Ui.Mvc.Vfi.Areas.Inv.Models;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Utilities;
using System.IO.Ports;
using System.Threading;

namespace Vfi.Ui.Mvc.Vfi.Areas.Inv.Controllers {
    public class TransactionController : Controller {
        private readonly IUnitOfWork _unitOfWork;

        [InjectionConstructor]
        public TransactionController(IUnitOfWork unitOfWork  ) {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");

            _unitOfWork = unitOfWork;
        }

        #region View
        ViewDataDictionary GetPageConfigData() {
            var viewModel = MyUtilities.MySystem.GetPageConfig(HttpContext.User.Identity.Name);
            foreach (var property in viewModel.GetType().GetProperties()) {
                ViewData[property.Name] = property.GetValue(viewModel, null);
            }

            return ViewData;
        }
        // View
        public ActionResult TransactionMaterialByStockOrder() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult TransactionProductByStockOrder() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ExportTransactionMaterial() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            Session["SessionExportTransactionMaterialIds"] = new List<int>();
            return View();
        }

        public ActionResult ExportInternalTransactionMaterial() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            Session["SessionExportInternalTransactionMaterialIds"] = new List<int>();
            return View();
        }

        public ActionResult ImportTransactionMaterial() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            Session["SessionImportTransactionMaterialIds"] = new List<int>();
            return View();
        }
        public ActionResult ImportInternalTransactionMaterial() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            Session["SessionImportTransactionMaterialIds"] = new List<int>();
            return View();
        }

        public ActionResult TransactionPacking() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            Session["ListTransactionPackings"] = new List<int>();
            return View();
        }
        public ActionResult RotateTransactionMaterial() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ImportInternalTransactionProduct() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            Session["SessionImportInternalTransactionProduct"] = new List<ProductInventoryRotateModel>();
            return View();
        }
        public ActionResult ExportInternalTransactionProduct() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            Session["SessionExportInternalTransactionProduct"] = new List<ProductInventoryRotateModel>();
            return View();
        }
        public ActionResult ImportTransactionProduct() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            Session["SessionRotateTransactionProduct"] = new List<ProductInventoryRotateModel>();
            return View();
        }

        public ActionResult RotateTransactionProduct() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            Session["SessionRotateTransactionProduct"] = new List<ProductInventoryRotateModel>();
            return View();
        }

        public ActionResult ApproveTransactionMaterial() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ApproveTransactionProduct() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult CancelTransactionProduct() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult TransactionMaterialManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult TransactionProductManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ExportTransactionMaterialTool() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ImportTransactionMaterialTool() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ChangeOrderDetailForm() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult CreateCncForm() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult AssignMaterials() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult MaterialUseByShift() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ApproveMaterialUse() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult MaterialUseManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ImportMaterialInventory() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            Session["SessionImportTransactionMaterialIds"] = new List<int>();
            return View();
        }
        public ActionResult ExportMaterialInventory() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            Session["ListImportTransactionMaterialIds"] = new List<int>();
            return View();
        }
        public ActionResult RetrieveMaterial() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ImportWorkpieceMaterial() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ExportWorkpieceMaterial() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult RecipeTransactionProduct() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        // export TP
        public ActionResult CreateExportTP() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            //Session["SessionProductExportTerm"] = new List<Vfi.Models.ExportFormTP_KD>();
            Session["SessionRotateTransactionProduct"] = new List<ProductInventoryRotateModel>();
            return View();
        }

        // import NCU - QCB
        public ActionResult CreateImportNCU_QCB() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            Session["SessionImportDetail"] = new List<Vfi.Models.ImportNCU_QCBDetailModel>();
            return View();
        }
        //Import SX1
        public ActionResult CreateImportSX1() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            Session["SessionImportSX1Detail"] = new List<ImportSX1DetailModel>();
            return View();
        }
        public ActionResult UpdateImportSX1() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            // Session["SessionImportSX1Detail"] = new List<ImportSX1DetailModel>();
            return View();
        }
        // GetData

        //manage form
        public ActionResult ManageExportForm() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        // export GCN - NCU
        public ActionResult CreateExportGCN_NCU() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            Session["SessionExportGCN_NCUDetail"] = new List<Vfi.Models.ExportGCN_NCUDetail>();
            return View();
        }
        // export GCN - NCU
        public ActionResult ImportWorkpieceMaterialAdd() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ExportProcessing() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult TrackUpMaterialInv() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult AssignMaterials_Admin() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult AddTransactionDetail() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            Session["SessionProductLot"] = new List<ProductInventoryRotateModel>();
            return View();
        }

        public ActionResult TransactionWeighingAddDetail() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult TransactionWeighingManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        #endregion

        #region print device
        //private string GetDefaultPrinter() {
        //    PrinterSettings settings = new PrinterSettings();
        //    foreach (string printer in PrinterSettings.InstalledPrinters) {
        //        settings.PrinterName = printer;
        //        if (settings.IsDefaultPrinter)
        //            return printer;
        //    }
        //    return string.Empty;
        //}
        public void Print() { 
        }
        //public void Print(int transactionId) {
        //    PrinterSettings 
        //}
        //protected void Print(object sender, EventArgs e) {
        //    Process printjob = new Process();
        //    printjob.StartInfo.FileName = @"D:\File\Test.pdf"; //path of your file;
        //    printjob.StartInfo.Verb = "Print";
        //    printjob.StartInfo.CreateNoWindow = true;
        //    printjob.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        //    GetDefaultPrinter();
        //    printjob.Start();
        //}
        #endregion

        #region weigh device

        //ObjectCache cache = MemoryCache.Default;
        string cacheName = "weighValue";
        //MyCacheProvider cacheProvider = new MyCacheProvider();
        public ActionResult TransactionWeighing() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            DisConnectWeighingDevice();
            MyCacheProvider.Instance.AddItem(cacheName, 0);
            var model = (List<ProductInventoryRotateModel>)Session["SessionTransactionWeighingProduct"];
            if (model == null) {
                Session["SessionTransactionWeighingProduct"] = new List<ProductInventoryRotateModel>();
            }
            ViewData = GetPageConfigData();
            return View();
        }

        static SerialPort _portCOM = null;
        static MyWeighingService.MyWeighingModel _config = null;
        public ActionResult ConnectWeighingDevice() {
            try {
                MyUtilities.Function.SaveLog("ConnectWeighingDevice", "try");
                //string[] ports = SerialPort.GetPortNames();
                DisConnectWeighingDevice();
                _config = MyWeighingService.GetConfig();
                //if (ports.Length > 0) {
                if (_config != null && !string.IsNullOrWhiteSpace(_config.PortName)) {
                    _portCOM = new SerialPort(_config.PortName, _config.BaudRate, Parity.None, 8, StopBits.One);
                    _portCOM.DtrEnable = true;
                    _portCOM.RtsEnable = true;
                    _portCOM.ReadTimeout = SerialPort.InfiniteTimeout;
                    _portCOM.WriteTimeout = SerialPort.InfiniteTimeout;
                    _portCOM.DataReceived += HandleDataReceive;
                    _portCOM.ErrorReceived += PortCOM_ErrorReceived;
                    _portCOM.Disposed += PortCOM_Disposed;
                    try {
                        //if (_portCOM.IsOpen) { DisConnectWeighingDevice(); }
                        //if (!_portCOM.IsOpen)
                        _portCOM.Open();
                    }
                    catch (Exception ex) {
                        MyUtilities.Function.SaveLog("ConnectWeighingDevice", "failed-connect: not open " + _config.PortName);
                        return Json((int)MyUtilities.Monitor.ErrorCode.NotFound);
                    }
                    MyUtilities.Function.SaveLog("ConnectWeighingDevice", "success");
                }
                else {
                    MyUtilities.Function.SaveLog("ConnectWeighingDevice", "failed: not found ");
                    return Json((int)MyUtilities.Monitor.ErrorCode.NotFound);
                }
            }
            catch (Exception ex) {
                MyUtilities.Function.SaveLog("DisConnectWeighingDevice", "failed-error: " + ex.Message);
                return Json((int)MyUtilities.Monitor.ErrorCode.ReferenceError);
            }
            return Json((int)MyUtilities.Monitor.ErrorCode.NoError);
        }
        void PortCOM_ErrorReceived(object sender, SerialErrorReceivedEventArgs e) {
            var signal = ((SerialPort)sender).ReadExisting();
            MyUtilities.Function.SaveLog("PortCOM_ErrorReceived", "signal: " + signal);
        }
        void PortCOM_Disposed(object sender, EventArgs e) {
            var signal = ((SerialPort)sender).ReadExisting();
            MyUtilities.Function.SaveLog("PortCOM_Disposed", "signal: " + signal);
        }
        public ActionResult DisConnectWeighingDevice() {
            try {
                MyUtilities.Function.SaveLog("DisConnectWeighingDevice", "try:" + _portCOM.ToString());
                if (_portCOM != null) {
                    _portCOM.DataReceived -= HandleDataReceive;
                    if (_portCOM.IsOpen) {
                        _portCOM.Close();
                        _portCOM.DiscardInBuffer();
                        _portCOM.DiscardOutBuffer();
                    }
                    _portCOM.Dispose();
                    _portCOM = null;
                    _config = null;
                    MyUtilities.Function.SaveLog("DisConnectWeighingDevice", "success");
                }
            }
            catch (Exception ex) {
                MyUtilities.Function.SaveLog("DisConnectWeighingDevice", "error: " + ex.Message);
            }
            return Json((int)MyUtilities.Monitor.ErrorCode.NoError);
        }
        TransactionDetail _detail = new TransactionDetail() { QuantityKg = 0 };

        private readonly object ThisLock = new object();
        private void HandleDataReceive(object sender, SerialDataReceivedEventArgs e) {
            try {
                //var signal = _portCOM.ReadExisting();
                // 2.713 kg
                var signal = ((SerialPort)sender).ReadExisting();
                MyUtilities.Function.SaveLog("HandleDataReceive", "signal: " + signal);
                MyCacheProvider.Instance.SetItemValue(cacheName, signal);
                Thread.Sleep(800);
            }
            catch (Exception ex) {
                MyUtilities.Function.SaveLog("HandleDataReceive", "error: " + ex.Message);
            }
        }

        List<string> errorStrs = new List<string> { ".", "=", "-" };
        public string ReverseString(string s) {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        
        public ActionResult GetWeighingValue() {
            var value = 0.0;
            try {
                var signal = MyCacheProvider.Instance.GetItem(cacheName) as string;
                MyUtilities.Function.SaveLog( "GetCacheItem", "Value: " + signal);
                if (signal == null) { return Json(-1); }
                if (_config == null) return Json(0);
                switch (_config.SignalType) {
                    case 1:
                        value = ConvertSignal(signal);
                        break;
                    case 2:
                        // 2.713 kg
                        value = ConvertSignal2(signal);
                        break;
                    default:
                        value = ConvertSignal(signal);
                        break;
                }
            }
            catch (Exception ex) {
                MyUtilities.Function.SaveLog("GetCacheItem", "Exception: " + ex.Message);
            }
            MyUtilities.Function.SaveLog("GetWeighingValue", "weight: " + value);
            return Json(value);
        }
        double ConvertSignal2(string signal) {
            // 2.713 kg
            var time = DateTime.Now;
            var value = 0.0;
            if (!string.IsNullOrWhiteSpace(signal)) {
                if (signal.Contains("kg")) {
                    var data = signal.Split('k').ToList();
                    if (data.Any()) {
                        var valueStr = data[0];
                        try {
                            if (!string.IsNullOrWhiteSpace(valueStr) && !errorStrs.Contains(valueStr)) {
                                value = Convert.ToDouble(valueStr);
                            }
                        }
                        catch (FormatException ex) {
                            MyUtilities.Function.SaveLog("ConvertSignal", "signal/error: " + ex.Message);
                        }
                    }
                }
                else if (signal.Contains('g')) {
                    var data = signal.Split('g').ToList();
                    if (data.Any()) {
                        var valueStr = data[0];
                        try {
                            if (!string.IsNullOrWhiteSpace(valueStr) && !errorStrs.Contains(valueStr)) {
                                value = Convert.ToDouble(valueStr) / 1000;
                            }
                        }
                        catch (FormatException ex) {
                            MyUtilities.Function.SaveLog("ConvertSignal", "signal/error: " + ex.Message);
                        }
                    }

                }
                else {
                    MyUtilities.Function.SaveLog("ConvertSignal", "signal/no match: " + signal);
                }
            }
            return value;
        }

        double ConvertSignal(string signal) {
            var time = DateTime.Now;
            var value = 0.0;
            if (!string.IsNullOrWhiteSpace(signal)) {
                signal = signal.Replace('-', '0');
                var data = signal.Split('=').ToList();
                if (data.Any()) {
                    var valueStr = "";
                    if (data.Count > 1) {
                        valueStr = data[1];
                    }
                    else {
                        valueStr = data[0];
                    }
                    try {
                        //MyUtilities.Function.SaveLog(contentPath(), "ConvertSignal", "signal/convert: " + valueStr);
                        if (!string.IsNullOrWhiteSpace(valueStr) && !errorStrs.Contains(valueStr)) {
                            value = Convert.ToDouble(ReverseString(valueStr));
                        }
                        //MyUtilities.Function.SaveLog(contentPath(), "ConvertSignal", "signal/converted: " + value);

                    }
                    catch (FormatException ex) {
                        MyUtilities.Function.SaveLog("ConvertSignal", "signal/error: " + ex.Message);
                    }
                }
            }
            return value;
        } 

        [GridAction]
        public ActionResult AddTransactionWeighingProduct(int productId, int warehouseId, double productWeight, double packageWeight, double quantity) {
            var model = (List<ProductInventoryRotateModel>)Session["SessionTransactionWeighingProduct"];
            if (model == null || !model.Any()) model = new List<ProductInventoryRotateModel>();
            if (!(productId > 0 && warehouseId > 0 && quantity > 0))
                return View(new GridModel(model));
            try {
                using (var vfi = new tammaContext()) {
                    var entity = (from x in vfi.Products
                                  where x.Active && x.ProductId == productId
                                  select new ProductInventoryRotateModel {
                                      WarehouseId = warehouseId,
                                      ProductId = productId,
                                      ProductCode = x.ProductCode,
                                      CustomerCode = x.Customer.CustomerCode,
                                      Quantity = quantity,
                                      QuantityKg = quantity * productWeight / 1000,
                                      ProductWeight = productWeight,
                                      Weight = packageWeight,
                                  }).FirstOrDefault();
                    if (entity != null) {
                        entity.TransactionProductId = Convert.ToInt64(MyUtilities.Function.GetTimeStamp());
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("AddTransactionWeighingProduct", ex.Message);
            }
            Session["SessionTransactionWeighingProduct"] = model;
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult DeleteTransactionWeighingProduct(long transactionProductId) {
            var model = (List<ProductInventoryRotateModel>)Session["SessionTransactionWeighingProduct"];
            if (model == null || !model.Any()) { model = new List<ProductInventoryRotateModel>(); }
            else {
                try {
                    var index = model.FindIndex(x => x.TransactionProductId == transactionProductId);
                    if (index < 0) { throw new AggregateException("Lỗi! Không tìm thấy cái cần xoá " + transactionProductId); }
                    model.RemoveAt(index);

                }
                catch (Exception ex) {
                    ModelState.AddModelError("DeleteTransactionWeighingProduct", ex.Message);
                }
            }
            Session["SessionTransactionWeighingProduct"] = model;
            return View(new GridModel(model));
        }


        [HttpPost]
        public ActionResult SaveTransactionWeighingProduct() {
            if (!Request.IsAuthenticated)
                return Json((int)MyUtilities.Monitor.ErrorCode.ReferenceError, JsonRequestBehavior.AllowGet);

            var modifiedUser = HttpContext.User.Identity.Name;
            if (string.IsNullOrWhiteSpace(modifiedUser))
                return Json((int)MyUtilities.Monitor.ErrorCode.StatusChanged, JsonRequestBehavior.AllowGet);
            var model = (List<ProductInventoryRotateModel>)Session["SessionTransactionWeighingProduct"];
            if (model == null || !model.Any()) {
                return Json((int)MyUtilities.Monitor.ErrorCode.NoThing, JsonRequestBehavior.AllowGet);
            }

            //return View(new GridModel(new List<OrderDetailModel>()));
            try {
                var list = model.Select(x => new TransactionWeighing {
                    ProductId = x.ProductId,
                    WarehouseId = x.WarehouseId.Value,
                    Quantity = x.Quantity,
                    Weight = x.QuantityKg,
                    UnitWeight = x.ProductWeight,
                    Status = (byte)MyUtilities.Transaction.Status.Open,
                    PackageWeight = x.Weight,
                    ModifiedDate = DateTime.Now,
                    ModifiedUser = HttpContext.User.Identity.Name,
                }).ToList();
                if (list.Any()) {
                    using (var vfi = new tammaContext()) {
                        vfi.TransactionWeighings.AddRange(list);
                        vfi.SaveChanges();
                    }
                    Session["SessionTransactionWeighingProduct"] = new List<ProductInventoryRotateModel>();
                    return Json((int)MyUtilities.Monitor.ErrorCode.NoError, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception) {
                //ModelState.AddModelError("TransactionProductSaveDefault", "" + exception.Message);
                return Json((int)MyUtilities.Monitor.ErrorCode.Exception, JsonRequestBehavior.AllowGet);
            }
            return Json((int)MyUtilities.Monitor.ErrorCode.NotImplement, JsonRequestBehavior.AllowGet);
        }

        [GridAction]
        public ActionResult SelectTransactionWeighingManagement(int warehouseId, int status, string fromDate, string toDate) {
            var model = new List<TransactionWeighingModel>();
            try {
                model = GetTransactionWeighingManagement(warehouseId, status, fromDate, toDate);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectTransactionWeighingManagement", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<TransactionWeighingModel> GetTransactionWeighingManagement(int warehouseId, int status, string fromDate, string toDate) {
            var model = new List<TransactionWeighingModel>();
            if (status == 0) return model;
            using (var vfi = new tammaContext()) {
                var transactionWeighings = (from x in vfi.TransactionWeighings
                                            where (warehouseId == 0 || x.WarehouseId == warehouseId) &&
                                                x.Status == status
                                            select new TransactionWeighingModel {
                                                WeighingId = x.WeighingId,
                                                ProductId = x.ProductId,
                                                ProductCode = x.Product.ProductCode,
                                                CustomerCode = x.Product.Customer.CustomerCode,
                                                Quantity = x.Quantity,
                                                Weight = x.Weight,
                                                UnitWeight = x.UnitWeight,
                                                PackageWeight = x.PackageWeight,
                                                Status = x.Status,
                                                TransactionId = x.TransactionId ?? 0,
                                                ModifiedDate = x.ModifiedDate,
                                                ModifiedUser = x.ModifiedUser,
                                            }).ToList();
                if (status != (byte)MyUtilities.Transaction.Status.Open) {
                    var fDate = MyUtilities.Function.ParseDate(fromDate);
                    var tDate = MyUtilities.Function.ParseDate(toDate);
                    transactionWeighings = transactionWeighings.Where(x => x.ModifiedDate >= fDate && x.ModifiedDate <= tDate).ToList();
                }
                foreach (var transactionWeighing in transactionWeighings) {
                    transactionWeighing.StatusName = MyUtilities.Transaction.CastText.GetTextStatus((int)transactionWeighing.Status);
                    model.Add(transactionWeighing);
                }
            }
            return model;
        }

        [GridAction]
        public ActionResult DeleteTransactionWeighingManagement(int weighingId, int warehouseId, int status, string fromDate, string toDate) {
            try {
                using (var vfi = new tammaContext()) {
                    var transactionWeighing = vfi.TransactionWeighings.FirstOrDefault(x => x.WeighingId == weighingId);
                    if (transactionWeighing == null) {
                        throw new AggregateException("Lỗi! Không tìm thấy cân hàng");
                    }
                    if (transactionWeighing.Status != (byte)MyUtilities.Transaction.Status.Open) {
                        throw new AggregateException("Lỗi! Tình trạng cân hàng không thể hủy " 
                            + MyUtilities.Transaction.CastText.GetTextStatus(transactionWeighing.Status));
                    }
                    transactionWeighing.Status = (byte)MyUtilities.Transaction.Status.Cancel;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("DeleteTransactionWeighingManagement", ex.Message);
            }

            return View(new GridModel(GetTransactionWeighingManagement(warehouseId, status, fromDate, toDate)));
        }

        [GridAction]
        public ActionResult SelectTransactionWeighingProduct(int warehouseId, int status, string fromDate, string toDate) {
            var model = new List<TransactionWeighingModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var transactionWeighings = (from x in vfi.TransactionWeighings
                             where x.WarehouseId == warehouseId &&
                             x.Status == status
                             select new TransactionWeighingModel {
                                 WeighingId = x.WeighingId,
                                 ProductId = x.ProductId,
                                 ProductCode = x.Product.ProductCode,
                                 CustomerCode = x.Product.Customer.CustomerCode,
                                 Quantity = x.Quantity,
                                 Weight = x.Weight,
                                 UnitWeight = x.UnitWeight,
                                 PackageWeight = x.PackageWeight,
                                 Status = x.Status,
                                 TransactionId = x.TransactionId ?? 0,
                                 ModifiedDate = x.ModifiedDate,
                                 ModifiedUser = x.ModifiedUser,
                             }).ToList();
                    if (status != (byte)MyUtilities.Transaction.Status.Open) {
                        var fDate = MyUtilities.Function.ParseDate(fromDate);
                        var tDate = MyUtilities.Function.ParseDate(toDate);
                        transactionWeighings = transactionWeighings.Where(x => x.ModifiedDate >= fDate && x.ModifiedDate <= tDate).ToList();
                    }
                    var productIds = transactionWeighings.Select(x => x.ProductId).Distinct().ToList();
                    var productInvs = (from x in vfi.ProductInventories
                                       where x.WarehouseId == warehouseId &&
                                            productIds.Contains(x.ProductId) &&
                                            x.TotalQty > 0
                                       select new ProductInventoryModel {
                                           ProductId = x.ProductId,
                                           ProductInventoryId = x.ProductInventoryId,
                                           LotNumber = x.LotNumber,
                                           WarehouseId = x.WarehouseId,
                                           TotalQty = x.TotalQty,
                                       }).ToList();
                    if (productInvs.Any()) {
                        var productInvIds = productInvs.Select(x => x.ProductInventoryId).ToList();
                        var openTransactionDetails = (from x in vfi.TransactionDetails
                                                      where x.ProductInvId != null
                                                       && productInvIds.Contains(x.ProductInvId.Value)
                                                      && x.Transaction.Status == (byte)MyUtilities.Transaction.Status.Open
                                                      && x.Transaction.WarehouseIssueId == warehouseId
                                                      select new {
                                                          x.ProductInvId,
                                                          x.Quantity
                                                      }).ToList();
                        foreach (var productInv in productInvs) {
                            var transactionDetailsById = openTransactionDetails.Where(x => x.ProductInvId == productInv.ProductInventoryId).ToList();
                            if (transactionDetailsById.Any()) {
                                productInv.TotalQty -= transactionDetailsById.Sum(x => x.Quantity);
                            }
                        }
                    }
                    foreach (var transactionWeighing in transactionWeighings) {
                        var productInvsById = productInvs.Where(x => x.ProductId == transactionWeighing.ProductId && x.TotalQty > 0)
                                                        .ToList();
                        if (!productInvsById.Any()) {
                            model.Add(transactionWeighing);
                        }
                        else {
                            var quantity = transactionWeighing.Quantity;
                            var canCreate = productInvsById.Sum(x => x.TotalQty) > quantity;
                            foreach (var productInv in productInvsById) {
                                var entity = (TransactionWeighingModel)transactionWeighing.Clone();
                                model.Add(entity);
                                entity.CanCreate = canCreate;
                                entity.ProductInvId = productInv.ProductInventoryId;
                                entity.LotNumber = productInv.LotNumber;
                                if (quantity <= productInv.TotalQty) {
                                    entity.InvQuantity = quantity;
                                    productInv.TotalQty -= quantity;
                                    quantity = 0;
                                    break;
                                }
                                else {
                                    entity.InvQuantity = productInv.TotalQty;
                                    quantity -= productInv.TotalQty;
                                    productInv.TotalQty = 0;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("AddTransactionWeighingProduct", ex.Message);
            }
            return View(new GridModel(model));
        }

        [HttpPost]
        public ActionResult CreateTransactionWeighingProduct(string ids, 
            int warehouseIssueId, int warehouseReceiptId, string date) {
                try {
                    var transactionDate = MyUtilities.Function.ParseDate(date);
                    if (transactionDate > DateTime.Now.AddDays(1)) {
                        throw new AggregateException("Lỗi! Xem lại ngày ( lớn hơn hiện tại) !");
                    }
                    using (var vfi = new tammaContext()) {
                        var transaction = new Vfi.Models.Transaction {
                            WarehouseIssueId = warehouseIssueId,
                            WarehouseReceiptId = warehouseReceiptId,
                            TransactionCode =
                                MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                            EoI = Convert.ToChar(MyUtilities.Transaction.EoIEnum.Rotate) + "",
                            MoP = MyUtilities.Transaction.MoP.Product,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = transactionDate,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                        };
                        try {
                            var checkedRecords = ids.Split(':');
                            foreach (var checkedRecord in checkedRecords) {
                                var idsSplit = MyUtilities.Function.StringsSplit(checkedRecord);
                                var weighingId = Convert.ToInt32(idsSplit[0]);
                                var productInvId = Convert.ToInt32(idsSplit[1]);
                                var quantity = Convert.ToInt32(idsSplit[2]);
                                transaction.TransactionDetails.Add(new TransactionDetail {
                                    DrawerId = weighingId,
                                    ProductInvId = productInvId,
                                    Quantity = quantity,
                                    TransactionId = transaction.TransactionId,
                                    MoP = MyUtilities.Transaction.MoP.Product,
                                    ModifiedUser = transaction.ModifiedUser,
                                    ModifiedDate = transaction.ModifiedDate,
                                    Active = true
                                });
                            }
                        }
                        catch (FormatException ex) {
                            return Json(
                                new MyUtilities.Monitor.MyJsonResult(
                                    (int)MyUtilities.Monitor.ErrorCode.ReferenceError,
                                    "Lỗi data id", 
                                    null)); 
                        }

                        var weighingIds = transaction.TransactionDetails.Select(x => x.DrawerId).Distinct().ToList();
                        var productInvIds = transaction.TransactionDetails.Select(x => x.ProductInvId).Distinct().ToList();
                        var transactionWeighings = vfi.TransactionWeighings.Where(x => weighingIds.Contains(x.WeighingId));
                        var productInvs = vfi.ProductInventories.Where(x => productInvIds.Contains(x.ProductInventoryId))
                                                                .Select(x => new ProductInventoryModel {
                                                                    ProductInventoryId = x.ProductInventoryId,
                                                                    ProductCode = x.Product.ProductCode,
                                                                    LotNumber = x.LotNumber,
                                                                    TotalQty = x.TotalQty
                                                                }).ToList();
                        foreach (var transactionWeighing in transactionWeighings) {
                            var transactionDetailsById = transaction.TransactionDetails.Where(x => x.DrawerId == transactionWeighing.WeighingId);
                            foreach (var transactionDetail in transactionDetailsById) {
                                transactionDetail.DrawerId = null;
                                var productInv = productInvs.FirstOrDefault(x => x.ProductInventoryId == transactionDetail.ProductInvId);
                                productInv.TotalQty -= transactionDetail.Quantity;
                                if (productInv.TotalQty < 0) {
                                    throw new AggregateException("Lỗi! Không đủ số lượng xuất kho: " + productInv.ProductCode + "-" + productInv.LotNumber);
                                }
                                transactionDetail.ReferenceId = transactionWeighing.ProductId;
                                transactionDetail.LotNumber = productInv.LotNumber;
                            }
                            transactionWeighing.Transaction = transaction;
                            transactionWeighing.Status = (byte)MyUtilities.Transaction.Status.Approved;
                        }
                        if (transaction.TransactionDetails.Any()) {
                            vfi.Transactions.Add(transaction);
                            vfi.SaveChanges();
                            return Json(
                                new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.NoError, "", null),
                                JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (Exception exception) {
                    return Json(
                          new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.Exception, exception.Message, null),
                         JsonRequestBehavior.AllowGet);
                }
            return Json(
                new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.NotImplement, "Không có gì xảy ra", null), 
                JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Transaction

        public ActionResult SelectComboboxTransactionProcessing() {
            var model = new List<TransactionModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var transactions =
                        vfi.Transactions.Where(t => t.Status == (byte)MyUtilities.Transaction.Status.Processing);
                    foreach (var transaction in transactions) {
                        var entity = new TransactionModel {
                            TransactionId = transaction.TransactionId,
                            TransactionCode =
                                transaction.TransactionCode + "-" +
                                transaction.CreatedDate.ToString("dd/MM/yyyy")
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectComboboxTransactionProcessing", ex.Message);
            }
            return new JsonResult {
                Data = new SelectList(model, "TransactionId", "TransactionCode")
            };
        }

        [GridAction]
        public ActionResult SelectProductLot(long transactionId) {
            var model = new List<TransactionProductModel>();
            if (transactionId == 0)
                return View(new GridModel(model));
            try {
                Session["SessionProductLot"] = new List<TransactionProductModel>();
                using (var vfi = new tammaContext()) {
                    var transaction = vfi.Transactions
                        .FirstOrDefault(t => t.TransactionId == transactionId &&
                                             t.Status == (byte)MyUtilities.Transaction.Status.Processing);
                    if (transaction == null)
                        throw new AggregateException("Lỗi! Không tìm thấy phiếu xuất kho cần phân lô");

                    var transactionProducts = transaction.TransactionProducts;
                    var productIds = transactionProducts.Select(tp => tp.ProductId).ToList();
                    var productInvs =
                        vfi.ProductInventories.Where(
                            pi =>
                                productIds.Contains(pi.ProductId) &&
                                transaction.WarehouseIssueId == pi.WarehouseId &&
                                pi.TotalQty > 0);
                    foreach (var transactionProduct in transactionProducts) {
                        var quantity = transactionProduct.Quantity;
                        var productInvsById = productInvs.Where(pi => pi.ProductId == transactionProduct.ProductId);
                        var productWeight =
                             MyUtilities.Product.GetProductInvWeight(transactionProduct.ProductId,
                                 transactionProduct.Transaction.WarehouseIssueId ?? 0);
                        var transactionProductQuantity = transactionProduct.Quantity;
                        foreach (var productInv in productInvsById) {
                            var entity = new TransactionProductModel {
                                ProductId = productInv.ProductId,
                                ProductCode = productInv.Product.ProductCode,
                                ProductInvId = productInv.ProductInventoryId,
                                ProductInvLot = productInv.LotNumber,
                                ProductInvQuantity = productInv.TotalQty,
                                ProductInvAvailable = 0,
                                WarehouseName = productInv.Warehouse.WarehouseName,
                                WarehouseId = productInv.WarehouseId,
                                ProductWeight = productWeight,
                                Quantity = 0,
                                TransactionId = transaction.TransactionId,
                                TransactionProductId = transactionProduct.DetailId,
                                TransactionProductQuantity = transactionProductQuantity,
                                Status = 0,
                            };
                            entity.ProductInvAvailable = GetWarehouseInvPeriod(productInv.ProductInventoryId);

                            if (entity.ProductInvAvailable == 0 || quantity == 0)
                                entity.Quantity = 0;
                            else if (entity.ProductInvAvailable > quantity) {
                                entity.Quantity = quantity;
                                quantity = 0;
                            }
                            else {
                                entity.Quantity = entity.ProductInvAvailable;
                                quantity -= entity.ProductInvAvailable;
                                if (quantity < 0)
                                    quantity = 0;
                            }
                            entity.QuantityKg = entity.Quantity * entity.ProductWeight;
                            transactionProductQuantity = 0;
                            model.Add(entity);
                        }
                        if (quantity > 0) {
                            var entities = model.Where(m => m.TransactionProductId == transactionProduct.DetailId);
                            foreach (var entity in entities) {
                                entity.Status = 1;
                            }
                        }
                        if (!productInvsById.Any()) {
                            var entity = new TransactionProductModel {
                                ProductId = transactionProduct.ProductId,
                                ProductCode = transactionProduct.Product.ProductCode,
                                ProductInvId = 0,
                                ProductInvLot = "",
                                ProductInvQuantity = 0,
                                ProductInvAvailable = 0,
                                WarehouseName = transaction.Warehouse.WarehouseName,
                                WarehouseId = transaction.WarehouseIssueId.Value,
                                ProductWeight = productWeight,
                                Quantity = 0,
                                TransactionId = transaction.TransactionId,
                                TransactionProductId = transactionProduct.DetailId,
                                Status = 1,
                                TransactionProductQuantity = quantity,
                            };
                            model.Add(entity);
                        }
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductByLot", ex.Message);
            }
            Session["SessionProductLot"] = model;
            return View(new GridModel(model));
        }


        [GridAction]
        public ActionResult SelectProductLotById(string detailId) {
            if (string.IsNullOrWhiteSpace(detailId)) {
                Session["SessionProductLot"] = new List<TransactionProductModel>();
                return View(new GridModel(new List<TransactionProductModel>()));
            }
            // material
            var model = new List<TransactionProductModel>();
            try {

                using (var vfi = new tammaContext()) {
                    var transactionProductId = Convert.ToInt32(detailId);
                    var transactionProduct =
                        vfi.TransactionProducts.FirstOrDefault(tp => tp.DetailId == transactionProductId);
                    if (transactionProduct == null)
                        return View(new GridModel(new List<TransactionProductModel>()));
                    if (transactionProduct.TransactionDetails.Any())
                        throw new AggregateException("Sản phẩm đã được phân lô!");
                    var productInvs =
                        vfi.ProductInventories.Where(
                            pi =>
                                pi.ProductId == transactionProduct.ProductId &&
                                transactionProduct.Transaction.WarehouseIssueId == pi.WarehouseId &&
                                pi.TotalQty > 0);
                    var productWeight =
                         MyUtilities.Product.GetProductInvWeight(transactionProduct.ProductId,
                             transactionProduct.Transaction.WarehouseIssueId ?? 0);
                    //
                    var quantity = transactionProduct.Quantity;
                    foreach (var productInv in productInvs) {
                        var entity = new TransactionProductModel {
                            ProductId = productInv.ProductId,
                            ProductCode = productInv.Product.ProductCode,
                            ProductInvId = productInv.ProductInventoryId,
                            ProductInvLot = productInv.LotNumber,
                            ProductInvQuantity = productInv.TotalQty,
                            ProductInvAvailable = 0,
                            WarehouseName = productInv.Warehouse.WarehouseName,
                            WarehouseId = productInv.WarehouseId,
                            ProductWeight = productWeight,
                            Quantity = 0,
                            TransactionId = transactionProduct.TransactionId,
                            TransactionProductId = transactionProduct.DetailId,
                        };
                        entity.ProductInvAvailable = GetWarehouseInvPeriod(productInv.ProductInventoryId);

                        if (entity.ProductInvAvailable == 0 || quantity == 0)
                            entity.Quantity = 0;
                        else if (entity.ProductInvAvailable > quantity) {
                            entity.Quantity = quantity;
                            quantity = 0;
                        }
                        else {
                            entity.Quantity = entity.ProductInvAvailable;
                            quantity -= entity.ProductInvAvailable;
                            if (quantity < 0)
                                quantity = 0;
                        }
                        entity.QuantityKg = entity.Quantity * entity.ProductWeight;
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InputProductOrderDetail", ex.Message);
            }
            Session["SessionProductLot"] = model;
            return View(new GridModel(model));
        }


        [HttpPost]
        public ActionResult TransactionProductSaveDefault() {
            if (!Request.IsAuthenticated)
                return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

            var modifiedUser = HttpContext.User.Identity.Name;
            if (string.IsNullOrWhiteSpace(modifiedUser))
                return Json(@"Vui lòng đăng nhập hệ thống. (user null). ");
            var listDetails = (List<TransactionProductModel>)Session["SessionProductLot"];
            if (!listDetails.Any())
                return Json("Error! List null", JsonRequestBehavior.AllowGet);
            //return View(new GridModel(new List<OrderDetailModel>()));
            try {
                listDetails = listDetails.Where(l => l.Quantity > 0).ToList();
                UpdateAddTransactionDetail(listDetails);
                return Json("Hoàn tất lưu !", JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception) {
                //ModelState.AddModelError("TransactionProductSaveDefault", "" + exception.Message);
                return Json(exception.Message, JsonRequestBehavior.AllowGet);
            }
            return Json("Error! UnKnow", JsonRequestBehavior.AllowGet);
        }

        [GridAction]
        public ActionResult UpdateAddTransactionDetail(
            [Bind(Prefix = "inserted")] IEnumerable<TransactionProductModel> inserts,
            [Bind(Prefix = "updated")] IEnumerable<TransactionProductModel> updates,
            [Bind(Prefix = "deleted")] IEnumerable<TransactionProductModel> deletes) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode",
                                         "Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<TransactionProductModel>()));
            }
            var listDetails = (List<TransactionProductModel>)Session["SessionProductLot"];
            if (!listDetails.Any())
                return View(new GridModel(new List<TransactionProductModel>()));
            try {
                if (updates != null) {
                    foreach (var updateDetail in updates) {
                        var detail = listDetails.FirstOrDefault(od => od.ProductInvId == updateDetail.ProductInvId);
                        detail.Quantity = updateDetail.Quantity;
                    }
                    listDetails = listDetails.Where(l => l.Quantity > 0).ToList();
                    UpdateAddTransactionDetail(listDetails);
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("AddTransactionDetail", exception.Message);
            }
            Session["SessionProductLot"] = new List<TransactionProductModel>();
            return View(new GridModel(new List<TransactionProductModel>()));
        }

        private int UpdateAddTransactionDetail(List<TransactionProductModel> list) {
            if (!list.Any())
                throw new AggregateException("Lỗi! Danh sách trống!");
            using (var vfi = new tammaContext()) {
                var transactionProductFirst = list.FirstOrDefault();
                var transaction =
                    vfi.Transactions.FirstOrDefault(
                        t =>
                            t.TransactionId == transactionProductFirst.TransactionId &&
                            t.Status == (byte)MyUtilities.Transaction.Status.Processing);
                if (transaction == null)
                    throw new AggregateException("Lỗi! Không tìm thấy phiếu cần phân lô!");
                var transactionProducts = transaction.TransactionProducts;
                foreach (var transactionProduct in transactionProducts) {
                    var listById = list.Where(l => l.TransactionProductId == transactionProduct.DetailId);
                    if (!listById.Any())
                        throw new AggregateException("Lỗi! Không tìm thấy lô của SP " +
                                                     transactionProduct.Product.ProductCode);
                    if (listById.Sum(l => l.Quantity) != transactionProduct.Quantity)
                        throw new AggregateException("Lỗi! Phân lô sai số lượng xuất kho ! " +
                                                     transactionProduct.Product.ProductCode);
                    foreach (var entity in listById) {
                        var transactionDetail = new TransactionDetail {
                            ReferenceId = entity.ProductId,
                            MoP = MyUtilities.Transaction.MoP.Product,
                            Quantity = entity.Quantity,
                            UnitMeasure = "pcs",
                            Active = true,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            Note = entity.Note,
                            TransactionId = entity.TransactionId,
                            LotNumber = (entity.ProductInvLot + "").Trim(),
                            ProductInvId = entity.ProductInvId,
                            TransactionProductId = entity.TransactionProductId,
                        };
                        transactionProduct.TransactionDetails.Add(transactionDetail);
                    }
                }
                //if (transactionProduct == null)
                //    throw new AggregateException("Lỗi! Không tìm thấy phiếu sản phẩm!");
                //foreach (var entity in list)
                //{
                //}
                //var a = vfi.SaveChanges();
                //if (!transaction.TransactionProducts.Any(tp => !tp.TransactionDetails.Any()))
                //{
                transaction.Status = (byte)MyUtilities.Transaction.Status.Open;
                var a = vfi.SaveChanges();
                //}

                Session["SessionProductLot"] = new List<TransactionProductModel>();
                return a;
            }
        }

        public double GetWarehouseInvPeriod(int productInvId) {
            var inv = 0.0;
            using (var vfi = new tammaContext()) {

                var ci = new CultureInfo("vi-VN");
                DateTime from;
                DateTime to;
                var warehouseInv =
                    vfi.ProductInventories.FirstOrDefault(
                        pi => pi.ProductInventoryId == productInvId);
                if (warehouseInv != null)
                    inv = warehouseInv.TotalQty;
                var transactionDetails =
                    vfi.TransactionDetails.Where(
                        td =>
                        td.Transaction.Status == (byte)MyUtilities.Transaction.Status.Open &&
                        td.ProductInvId == productInvId).ToList();
                inv -= transactionDetails.Sum(td => td.Quantity);
            }
            return inv;
        }

        [HttpPost]
        public ActionResult ApproveTransactionForMaterial(long[] checkedRecords) {
            try {
                if (!Request.IsAuthenticated)
                    return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

                var modifiedUser = HttpContext.User.Identity.Name;
                if (string.IsNullOrWhiteSpace(modifiedUser))
                    return Json(@"Vui lòng đăng nhập hệ thống. (user null). ");
                if (checkedRecords.Count() <= 0)
                    return Json(@"Vui lòng chọn ít nhất 1 lệnh. (checkedRecords <= 0). ");

                // material inventory - material inventory period
                using (var vfi = new tammaContext()) {
                    var transactions =
                        vfi.Transactions.Where(f => checkedRecords.Contains(f.TransactionId));
                    var transaction = transactions.FirstOrDefault();
                    //tra.CreatedUser = HttpContext.User.Identity.Name;
                    //vfi.SaveChanges();
                    //return Json("okie");

                    //foreach (var transaction in transactions) {
                    if (transaction.Status != (byte)MyUtilities.Transaction.Status.Open) {
                        //continue;
                        return Json("okie");
                    }
                    //transaction.ModifiedDate = DateTime.Now;
                    //    vfi.SaveChanges();
                    //    return Json("okie");
                    //continue;
                    // nhap nguyen lieu
                    if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, transaction.CreatedDate)) {
                        throw new AggregateException(
                            @"Không có quyền duyệt phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                    }
                    if (transaction.EoI.Equals("0")) { // nhập kho nguyên liệu
                        var importPO =
                            vfi.ImportPurchaseOrders.FirstOrDefault(
                                i => i.TransactionId == transaction.TransactionId);
                        if (importPO == null)
                            throw new AggregateException("Lỗi phiếu nhập");
                        PurchaseOrder purchaseOrder = null;
                        if (importPO.PurchaseOrderId != null) {
                            purchaseOrder =
                                vfi.PurchaseOrders.FirstOrDefault(
                                    po => po.PurchaseOrderId == importPO.PurchaseOrderId);
                        }
                        foreach (var detail in transaction.TransactionDetails) {
                            PurchaseOrderDetail poDetail = null;
                            ImportPurchaseOrderDetail importDetail = null;
                            if (purchaseOrder != null && detail.PoDetailId != null) {
                                poDetail =
                                    purchaseOrder.PurchaseOrderDetails.FirstOrDefault(
                                        pod => pod.PurchaseOrderDetailId == detail.PoDetailId);
                                if (detail.PoDetailId != null) {
                                    importDetail =
                                        importPO.ImportPurchaseOrderDetails.FirstOrDefault(
                                            id => id.PoDetailId == detail.PoDetailId);
                                }
                            }
                            else {
                                importDetail =
                                    importPO.ImportPurchaseOrderDetails.FirstOrDefault(
                                    id => id.MaterialId == detail.ReferenceId
                                    && (detail.LotNumber == null
                                        || detail.LotNumber.Equals(id.LotNumber))
                                            );
                            }
                            if (string.IsNullOrWhiteSpace(importDetail.LotNumber))
                                importDetail.LotNumber = MyUtilities.Material.GetMaterialLot(importDetail.VendorId,
                                    detail.ReferenceId.Value, importDetail.Length, transaction.CreatedDate, 0);
                            var materialByLot =
                                vfi.MaterialInventories.FirstOrDefault(
                                    mi =>
                                    mi.LotNumber.Equals(importDetail.LotNumber) &&
                                    mi.MaterialId == detail.ReferenceId &&
                                    mi.VendorId == importDetail.VendorId &&
                                    mi.Length == importDetail.Length);
                            //var isNewInv = false;
                            // nhap moi nguyen lieu
                            if (materialByLot == null) {
                                materialByLot = new MaterialInventory {
                                    TotalQty = 0,
                                    TotalQtyKg = 0,
                                    LotNumber = importDetail.LotNumber,
                                    MaterialId = importDetail.MaterialId.Value,
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    Active = true,
                                    UnitPrice = importDetail.UnitPrice,
                                    UnitWeight = importDetail.UnitWeight,
                                    VendorId = importDetail.VendorId,
                                    ImportDate = transaction.CreatedDate,
                                    ImportQuantity = Math.Round((importDetail.Quantity), 2),
                                    ImportQuantityKg = Math.Round((importDetail.QuantityKg), 3),
                                    StoreCode = importDetail.StoreCode,
                                    Length = importDetail.Length,
                                };
                                //isNewInv = true;
                                vfi.MaterialInventories.Add(materialByLot);
                                vfi.SaveChanges();
                            }
                            var materialInvPeriod = new Vfi.Models.MaterialInventoryPeriod {
                                MaterialId = materialByLot.MaterialId,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                PeriodDate = transaction.CreatedDate,
                                PeriodDay = transaction.CreatedDate.Day,
                                PeriodMonth = transaction.CreatedDate.Month,
                                PeriodYear = transaction.CreatedDate.Year,
                                Quantity = Math.Round(detail.Quantity, 2),
                                QuantityKg = Math.Round(detail.QuantityKg.Value, 3),
                                EarlyPeriodQuantity = materialByLot.TotalQty,
                                EarlyPeriodQuantityKg = materialByLot.TotalQtyKg,
                                TransactionId = transaction.TransactionId,
                                UnitPrice = detail.Price,
                                MaterialInventoryId = materialByLot.MaterialInventoryId,
                            };
                            materialByLot.TotalQty += Math.Round(detail.Quantity, 2);
                            materialByLot.TotalQtyKg += Math.Round(detail.QuantityKg.Value, 3);
                            materialByLot.ModifiedDate = DateTime.Now;
                            materialByLot.ModifiedUser = HttpContext.User.Identity.Name;
                            materialByLot.Active = true;

                            materialInvPeriod.LastPeriodQuantity = materialByLot.TotalQty;
                            materialInvPeriod.LastPeriodQuantityKg = materialByLot.TotalQtyKg;

                            materialByLot.EndDate = null;
                            vfi.MaterialInventoryPeriods.Add(materialInvPeriod);
                            if (detail.DrawerId > 0) {
                                var onShelf = new OnShelf {
                                    ReferenceInvId = materialByLot.MaterialInventoryId,
                                    ReferenceId = materialByLot.MaterialId,
                                    Active = true,
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = transaction.ModifiedUser,
                                    OnDate = DateTime.Today,
                                    DrawerId = detail.DrawerId.Value,
                                };
                                vfi.OnShelves.Add(onShelf);
                            }
                            if (poDetail != null) {
                                if (poDetail.Unit.Contains("Kg")) {
                                    poDetail.ReceivedQty += Math.Round(detail.QuantityKg.Value, 3);
                                    if (poDetail.OrderQty - poDetail.ReceivedQty - poDetail.RejectedQty <= 0)
                                        poDetail.IsComplete = true;
                                }
                                else {
                                    poDetail.ReceivedQty += detail.Quantity;
                                    materialByLot.UnitPrice = materialByLot.UnitPrice / materialByLot.UnitWeight;
                                }
                                if (poDetail.OrderQty - poDetail.ReceivedQty - poDetail.RejectedQty <= 0)
                                    poDetail.IsComplete = true;
                                var material = vfi.Materials.FirstOrDefault(m => m.MaterialId == materialByLot.MaterialId);
                                if (material.UnitPrice < materialByLot.UnitPrice)
                                    material.UnitPrice = materialByLot.UnitPrice;
                            }

                            if (purchaseOrder != null && !purchaseOrder.PurchaseOrderDetails.Any(pod => !(pod.IsComplete ?? false)))
                                purchaseOrder.Status = (byte)MyUtilities.Sales.Status.Completed;
                            transaction.Status = (byte)MyUtilities.Transaction.Status.Approved;
                            //if (isNewInv) {
                            //    vfi.MaterialInventories.Add(materialByLot);
                            //}
                            vfi.SaveChanges();
                        }
                    }
                    else if (transaction.EoI.Equals("1")) { // xuất kho nguyên liệu
                        var exportMaterial =
                            vfi.ExportMaterials.FirstOrDefault(em => em.TransactionId == transaction.TransactionId);
                        var isDestroy = exportMaterial.ShiftType == null;
                        var invIdsEmpty = new List<int>();
                        // kiem tra ton kho nguyen lieu theo lo
                        var materialInvIds =
                            exportMaterial.ExportMaterialDetails.Select(u => u.MaterialInvId).Distinct().ToList();
                        foreach (var materialInvId in materialInvIds) {
                            var materialInvs =
                                exportMaterial.ExportMaterialDetails.Where(u => u.MaterialInvId == materialInvId);
                            var materialInventory =
                                vfi.MaterialInventories.FirstOrDefault(mi => mi.MaterialInventoryId == materialInvId);

                            var exportMaterialQuantity = Math.Round(materialInvs.Sum(u => u.Quantity), 2);
                            if (exportMaterialQuantity > Math.Round(materialInventory.TotalQty, 2)) {
                                throw new AggregateException("Nguyên liệu cây" +
                                                             materialInvs.FirstOrDefault().Material.MaterialCode +
                                                             " không đủ!\n" +
                                                             (materialInventory.TotalQty - exportMaterialQuantity));
                            }
                            var materialInvPeriod = new MaterialInventoryPeriod {
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                MaterialId = materialInventory.MaterialId,
                                MaterialInventoryId = materialInventory.MaterialInventoryId,
                                Quantity = Math.Round(exportMaterialQuantity, 2),
                                //QuantityKg = Math.Round(exportMaterialQuantity, 2),
                                PeriodDate = transaction.CreatedDate,
                                PeriodDay = transaction.CreatedDate.Day,
                                PeriodMonth = transaction.CreatedDate.Month,
                                PeriodYear = transaction.CreatedDate.Year,
                                EarlyPeriodQuantity = Math.Round(materialInventory.TotalQty, 2),
                                EarlyPeriodQuantityKg = Math.Round(materialInventory.TotalQtyKg ?? 0, 3),
                                TransactionId = transaction.TransactionId,
                            };
                            if (materialInventory.FirstUseDate == null) {
                                materialInventory.FirstUseDate = DateTime.Now;
                            }
                            materialInventory.TotalQty -= Math.Round(exportMaterialQuantity, 2);
                            materialInventory.TotalQtyKg -=
                                Math.Round(exportMaterialQuantity * materialInventory.UnitWeight, 3);
                            materialInvPeriod.LastPeriodQuantity = materialInventory.TotalQty;
                            materialInvPeriod.LastPeriodQuantityKg = materialInventory.TotalQtyKg;

                            if (Math.Round(materialInventory.TotalQty, 2) == 0) {
                                materialInventory.TotalQty = 0;
                                materialInventory.TotalQtyKg = 0;
                                invIdsEmpty.Add(materialInventory.MaterialInventoryId);
                                //if (isDestroy) {
                                //    if (!materialInventory.MaterialInvOnMachines.Any(x => x.TotalQuantity > 0)) {
                                //        materialInventory.EndDate = DateTime.Now;
                                //    }
                                //}
                            }
                            vfi.MaterialInventoryPeriods.Add(materialInvPeriod);
                        }
                        //
                        foreach (var transactionDetail in transaction.TransactionDetails) {
                            var exportDetail =
                                exportMaterial.ExportMaterialDetails.FirstOrDefault(
                                    emd => emd.TransactionDetailId == transactionDetail.TransactionDetailId);
                            if (exportDetail.MachineId == null) continue;
                            var materialOnMachine =
                                vfi.MaterialInvOnMachines.FirstOrDefault(
                                    mim =>
                                    mim.MachineId == exportDetail.MachineId &&
                                    mim.MaterialInvId == exportDetail.MaterialInvId);
                            var materialOnMachinePeriod = new MaterialInvOnMachinePeriod {
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                MachineId = exportDetail.MachineId,
                                MaterialInvId = exportDetail.MaterialInvId,
                                Quantity = Math.Round(transactionDetail.Quantity, 2),
                                PeriodDate = transaction.CreatedDate,
                            };
                            if (materialOnMachine == null) {
                                materialOnMachinePeriod.EarlyQuantity = 0;
                                materialOnMachinePeriod.LastQuantity = transactionDetail.Quantity;
                                materialOnMachine = new MaterialInvOnMachine {
                                    MachineId = exportDetail.MachineId,
                                    MaterialInvId = exportDetail.MaterialInvId,
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    TotalQuantity = Math.Round(transactionDetail.Quantity, 2),
                                };
                                vfi.MaterialInvOnMachines.Add(materialOnMachine);
                                vfi.SaveChanges();
                            }
                            else {
                                materialOnMachinePeriod.EarlyQuantity =
                                    Math.Round(materialOnMachine.TotalQuantity, 2);
                                materialOnMachinePeriod.LastQuantity = materialOnMachinePeriod.EarlyQuantity +
                                                                       Math.Round(transactionDetail.Quantity, 2);
                                materialOnMachine.ModifiedDate = DateTime.Now;
                                materialOnMachine.ModifiedUser = HttpContext.User.Identity.Name;
                                materialOnMachine.TotalQuantity += Math.Round(transactionDetail.Quantity, 2);
                            }
                            vfi.MaterialInvOnMachinePeriods.Add(materialOnMachinePeriod);
                        }
                        transaction.Status = (byte)MyUtilities.Transaction.Status.Approved;

                        vfi.SaveChanges();
                        DeActiveInventoryOnShelf(invIdsEmpty);
                        UpdateMaterialInventoryEndState(invIdsEmpty);
                        UpdateStatusWorkOrderRoutingMaterial(exportMaterial.ExportId);
                    }
                    //}
                }

            }
            catch (Exception exception) {
                return
                    Json(@"Lỗi giá trị nhập. (try-catch). " + exception.Message + "\n");
            }
            return Json("okie");
        }

        public void UpdateStatusWorkOrderRoutingMaterial(long exportId) {
            try {
                using (var vfi = new tammaContext()) {
                    var export = vfi.ExportMaterials.FirstOrDefault(x=> x.ExportId == exportId);
                    var exportDetailIds = export.ExportMaterialDetails.Select(x=> x.ExportDetailId).ToList();
                    var processes = vfi.WorkOrderProcesses.Where(x => exportDetailIds.Contains(x.ReferenceDetailId));
                    foreach (var process in processes) {
                        process.Status = (byte)MyUtilities.WorkOrder.Status.Finish;
                        process.WorkOrderRouting.Status = (byte)MyUtilities.WorkOrder.Status.Finish;
                        process.WorkOrderRouting.ActualEndDate = export.ExportDate;
                        if (process.WorkOrderRouting.NextRouteId != null) {
                            process.WorkOrderRouting.WorkOrderRouting2.Status = (byte)MyUtilities.WorkOrder.Status.Actived;
                            process.WorkOrderRouting.WorkOrderRouting2.ActualStartDate = export.ExportDate;
                        }
                        process.WorkOrderRouting.ActualCost += process.GoodQuantity;
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public void DeActiveInventoryOnShelf(List<int> materialInvIds) {
            try {
                using (var vfi = new tammaContext()) {
                    var onShelfs = vfi.OnShelves.Where(x => materialInvIds.Contains(x.ReferenceInvId)
                        && x.Active
                        && x.InventoryDrawer.InventoryShelf.ClassifiedId == 1);
                    if (onShelfs.Any()) { onShelfs.Each(x => x.Active = false); }
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("DeActiveInventoryOnShelf", ex.Message);
            }
        }

        [HttpPost]
        public ActionResult ApproveTransactionForProduct(long[] checkedRecords) {
            try {

                if (!Request.IsAuthenticated)
                    return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

                var modifiedUser = HttpContext.User.Identity.Name;
                if (string.IsNullOrWhiteSpace(modifiedUser))
                    return Json(@"Vui lòng đăng nhập hệ thống. (user null). ");
                if (checkedRecords.Count() <= 0)
                    return Json(@"Vui lòng chọn ít nhất 1 lệnh. (checkedRecords <= 0). ");

                //tam ma
                using (var vfi = new tammaContext()) {
                    var transactions =
                        vfi.Transactions.Where(
                            f =>
                            checkedRecords.Contains(f.TransactionId) &&
                            f.Status != (byte)MyUtilities.Transaction.Status.Approved &&
                            f.Status != (byte)MyUtilities.Transaction.Status.Cancel).ToList();
                    bool isExportTP = false;
                    bool isSendQuantityBack = false;
                    bool isChangeQuantityBack = false;
                    bool isChangeQuantityTo = false;
                    bool isImportPlating = false;
                    bool isImportPurchase = false;
                    bool isExportProduction2 = false;
                    //Vfi.Models.Transaction transactionExportChange = null;
                    ExportFormTP_KD exportTP = null;
                    OrderNote orderNote = null;
                    //var order = new Order();
                    //TaxInvoice taxInvoice = null;
                    Invoice invoice = null;
                    ExportChangeProduct exportChange = null;
                    PurchaseOrder purchaseOrder = null;
                    //double totalMoney = 0.0;
                    foreach (var transaction in transactions) {
                        if (transaction.Status != (byte)MyUtilities.Transaction.Status.Open) continue;
                        if (transaction.WarehouseReceiptId != (byte)MyUtilities.Warehouse.Business && transaction.WarehouseIssueId != null) {
                            var productInvIds =
                                transaction.TransactionDetails.Select(td => td.ProductInvId.Value).Distinct().ToList();
                            var productInvs =
                                vfi.ProductInventories.Where(
                                    pi => productInvIds.Contains(pi.ProductInventoryId));
                            var msg = "";
                            foreach (var productInvId in productInvIds) {
                                var details =
                                    transaction.TransactionDetails.Where(l => l.ProductInvId == productInvId).ToList();
                                var inv = productInvs.FirstOrDefault(pi => pi.ProductInventoryId == productInvId);
                                if (inv == null || details.Sum(d => d.Quantity) > inv.TotalQty)
                                    msg += "Số lượng xuất kho lớn hơn tồn ! " + details.FirstOrDefault().Product.ProductCode + "\n";
                            }
                            if (!string.IsNullOrWhiteSpace(msg)) {
                                throw new AggregateException(msg);
                            }
                        }
                        if (transaction.WarehouseReceiptId == (byte)MyUtilities.Warehouse.Defect) {
                            transaction.CreatedDate = DateTime.Today;
                        }
                        else if (transaction.WarehouseIssueId == (byte)MyUtilities.Warehouse.Finish &&
                           transaction.WarehouseReceiptId == (byte)MyUtilities.Warehouse.Business) {
                            exportTP =
                                vfi.ExportFormTP_KD.FirstOrDefault(
                                    e => e.TransactionCode.Equals(transaction.TransactionCode));
                            if (exportTP != null) {
                                isExportTP = true;
                                //order = vfi.Orders.FirstOrDefault(o => o.OrderId == exportTP.OrderId);
                            }
                            else {
                                exportChange =
                                    vfi.ExportChangeProducts.FirstOrDefault(
                                        on => on.TransactionId == transaction.TransactionId);
                                if (exportChange != null) {
                                    isChangeQuantityTo = true;
                                }
                            }
                        }
                        else if (transaction.WarehouseIssueId == null &&
                                 transaction.WarehouseReceiptId == (byte)MyUtilities.Warehouse.QcA) {
                            var importPo =
                                vfi.ImportPurchaseOrders.FirstOrDefault(
                                    i => i.TransactionId == transaction.TransactionId);
                            if (importPo != null)
                                if (importPo.PurchaseOrderId != null)
                                    purchaseOrder =
                                        vfi.PurchaseOrders.FirstOrDefault(
                                            po => po.PurchaseOrderId == importPo.PurchaseOrderId);
                            if (purchaseOrder != null)
                                isImportPurchase = true;
                        }
                        else if (transaction.WarehouseIssueId == (byte)MyUtilities.Warehouse.Business &&
                                 transaction.WarehouseReceiptId == (byte)MyUtilities.Warehouse.Finish) {
                            orderNote =
                                vfi.OrderNotes.FirstOrDefault(
                                    on => on.TransactionId == transaction.TransactionId);
                            if (orderNote != null) {
                                if (orderNote.NoteType == 1) {
                                    invoice = vfi.Invoices.FirstOrDefault(i => i.InvoiceId == orderNote.InvoiceId);
                                    exportTP = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == invoice.ExportId);
                                    if (exportTP != null) {
                                        isSendQuantityBack = true;
                                    }
                                }
                                else if (orderNote.NoteType == 2) {
                                    isChangeQuantityBack = true;
                                }
                            }
                        }
                        else if ((transaction.WarehouseIssueId == (byte)MyUtilities.Warehouse.Plating ||
                                  transaction.WarehouseIssueId == (byte)MyUtilities.Warehouse.PlatingTest)
                                 && transaction.WarehouseReceiptId == (byte)MyUtilities.Warehouse.QcB) {
                            var importPlating =
                                vfi.ImportNCU_QCB.FirstOrDefault(i => i.TransactionId == transaction.TransactionId);
                            if (importPlating != null) {
                                isImportPlating = true;
                            }
                        }
                        else if (transaction.WarehouseIssueId == (byte)MyUtilities.Warehouse.Production2
                                 && (transaction.WarehouseReceiptId == (byte)MyUtilities.Warehouse.Production2B
                                     || transaction.WarehouseReceiptId == (byte)MyUtilities.Warehouse.Production2C)) {
                            var message = "";
                            var transactionDetails =
                                transaction.TransactionDetails.Where(
                                    td => !td.Product.ProductionSections.Any(pm => pm.Active == true));
                            foreach (var transactionDetail in transactionDetails) {
                                message += transactionDetail.Product.ProductCode + " cần cập nhật công đoạn sản phẩm \n";
                            }
                            transactionDetails =
                                transaction.TransactionDetails.Where(
                                    td => td.Product.Production2Weight == null || td.Product.Production2Weight == 0);
                            foreach (var transactionDetail in transactionDetails) {
                                message += transactionDetail.Product.ProductCode +
                                           " cần cập nhật trọng lượng sản xuất 2 \n";
                            }
                            if (!string.IsNullOrWhiteSpace(message))
                                throw new AggregateException("Lỗi! \n" + message);
                            isExportProduction2 = true;
                        }
                        else if (transaction.WarehouseReceiptId == MyUtilities.Warehouse.Defect) {
                            var importSx1 =
                                vfi.ImportFormSX1.FirstOrDefault(
                                    i => i.TransactionCode.Equals(transaction.TransactionCode));
                            if (importSx1 == null) {
                                var message = "";
                                var transactionDetails =
                                    transaction.TransactionDetails.Where(
                                        td => !td.Product.ProductionMaterials.Any(pm => pm.Active));
                                foreach (var transactionDetail in transactionDetails) {
                                    message += transactionDetail.Product.ProductCode +
                                               " cần cập nhật nguyên liệu sản phẩm \n";
                                }
                                transactionDetails =
                                    transaction.TransactionDetails.Where(
                                        td => td.Product.QcWeight == null || td.Product.QcWeight == 0);
                                foreach (var transactionDetail in transactionDetails) {
                                    message += transactionDetail.Product.ProductCode + " cần cập nhật trọng lượng QC \n";
                                }
                                if (!string.IsNullOrWhiteSpace(message))
                                    throw new AggregateException("Lỗi! \n" + message);
                            }
                        }
                        if (isExportTP || isSendQuantityBack || isChangeQuantityTo || isImportPlating ||
                            isImportPurchase) {
                            var transactionDetails = transaction.TransactionDetails;
                            foreach (var transactionDetail in transactionDetails) {
                                if (isSendQuantityBack) {
                                    var exportDetail =
                                        vfi.ExportFormTP_KDDetail.FirstOrDefault(
                                            ed =>
                                            ed.ProductId == transactionDetail.ReferenceId &&
                                            ed.ExportId == exportTP.ExportId);
                                    if (exportDetail.IsInvoiced == true)
                                        throw new ArgumentException(
                                            "Chi tiết xuất đã xuất hóa đơn không thể nhập trả " +
                                            transactionDetail.Product.ProductCode);
                                    if (exportDetail.InvoiceDetails.Where(id => id.Active).Sum(id => id.Piece) <
                                        transactionDetail.Quantity)
                                        throw new ArgumentException("Số lượng trả vượt quá số lượng giao !" +
                                                                    transactionDetail.Product.ProductCode);
                                    orderNote = vfi.OrderNotes.FirstOrDefault(
                                            on => on.TransactionId == transaction.TransactionId);
                                    if (orderNote.NoteType == 1)
                                        exportDetail.Note += "\nTrả hàng :" + transactionDetail.Quantity;
                                    else if (orderNote.NoteType == 2)
                                        exportDetail.Note += "\nĐổi hàng :" + transactionDetail.Quantity;
                                    var orderNodeDetail =
                                        orderNote.OrderNoteDetails.FirstOrDefault(
                                            ond =>
                                            ond.OrderNote.InvoiceId ==
                                            exportDetail.ExportFormTP_KD.Invoices.FirstOrDefault().InvoiceId
                                            && ond.ProductId == exportDetail.ProductId);
                                    var entity = new InvoiceDetail {
                                        Piece = Convert.ToInt32(transactionDetail.Quantity * -1),
                                        NoteDetailId = orderNodeDetail.NoteDetailId,
                                        Price = exportDetail.InvoiceDetails.FirstOrDefault(id => id.Active).Price,
                                        ProductId = exportDetail.ProductId,
                                        Active = true,
                                        ExportDetailId = exportDetail.DetailId,
                                        InvoiceId = orderNote.InvoiceId,
                                        ModifiedDate = DateTime.Now,
                                        ModifiedUser = HttpContext.User.Identity.Name,
                                        Note = "Trả hàng " + orderNote.NoteNumber,
                                    };
                                    exportDetail.TransactionDetail.Quantity += entity.Piece;
                                    exportDetail.Quality += entity.Piece;
                                    var totalSendBack = transactionDetail.Quantity;
                                    var invoiceDetails =
                                        exportDetail.InvoiceDetails.Where(id => id.Active && id.Piece > 0);
                                    foreach (var invoiceDetail in invoiceDetails) {
                                        var orderDetail = invoiceDetail.OrderDetail;
                                        if (totalSendBack > invoiceDetail.Piece) {
                                            totalSendBack -= invoiceDetail.Piece;
                                            orderDetail.OrderQty -= invoiceDetail.Piece;
                                            orderDetail.Note += " !Trả hàng:" + invoiceDetail.Piece;
                                        }
                                        else {
                                            orderDetail.OrderQty = orderDetail.OrderQty -
                                                                   Convert.ToInt32(totalSendBack);
                                            orderDetail.Note += " !Trả hàng:" + totalSendBack;
                                            totalSendBack = 0;
                                        }
                                        if (totalSendBack == 0) break;
                                    }
                                    vfi.InvoiceDetails.Add(entity);
                                }
                                else if (isChangeQuantityTo) {
                                    var orderNoteDetail =
                                        vfi.OrderNoteDetails.FirstOrDefault(
                                            ond =>
                                            ond.NoteId == exportChange.NoteId &&
                                            ond.ProductId == transactionDetail.ReferenceId);
                                    var exportChanges =
                                        vfi.ExportChangeProducts.Where(
                                            ec =>
                                            ec.NoteId == exportChange.NoteId &&
                                            ec.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved);
                                    var alreadyChangeQuantity =
                                        exportChanges.Sum(
                                            ec =>
                                            ec.Transaction.TransactionDetails.FirstOrDefault(
                                                td => td.ReferenceId == transactionDetail.ReferenceId).Quantity);
                                    if (orderNoteDetail.Quantity - transactionDetail.Quantity - alreadyChangeQuantity <
                                        0)
                                        throw new AggregateException("Không thể đổi quá số lượng cần thiết " +
                                                                     transactionDetail.Product.ProductCode);
                                }
                                else if (isImportPlating) {
                                    var importPlating =
                                        vfi.ImportNCU_QCBDetail.FirstOrDefault(
                                            id =>
                                            id.ImportNCU_QCB.TransactionId == transaction.TransactionId &&
                                            id.ProductId == transactionDetail.ReferenceId);
                                    var exportPlating =
                                        vfi.ExportGCN_NCUDetail.FirstOrDefault(
                                            ed => ed.DetailId == importPlating.ExportDetailId);
                                    var importPlatingElses =
                                        vfi.ImportNCU_QCBDetail.Where(
                                            id =>
                                            id.ExportDetailId == exportPlating.DetailId &&
                                            id.ImportNCU_QCB.Transaction.Status ==
                                            (byte)MyUtilities.Transaction.Status.Approved).ToList();

                                    var importElse = 0.0;
                                    if (importPlatingElses.Any())
                                        importElse = importPlatingElses.Sum(id => id.RealNumber);
                                    if (importPlating.RealNumber > exportPlating.RealNumber - importElse)
                                        throw new AggregateException("Không nhập quá số lượng cần thiết " +
                                                                     transactionDetail.Product.ProductCode +
                                                                     " thiếu nhập " +
                                                                     (exportPlating.RealNumber - importElse));
                                    //}
                                }
                                else if (isImportPurchase) {
                                    var poDetail =
                                        purchaseOrder.PurchaseOrderDetails.FirstOrDefault(
                                            pod =>
                                            pod.PurchaseOrderId == purchaseOrder.PurchaseOrderId &&
                                            pod.ReferenceId == transactionDetail.ReferenceId);
                                    if (poDetail != null) {
                                        poDetail.ReceivedQty += transactionDetail.Quantity;
                                        if (poDetail.OrderQty - poDetail.ReceivedQty - poDetail.RejectedQty <= 0)
                                            poDetail.IsComplete = true;
                                    }
                                }
                            }
                        }
                        //bool checkInventory = isExportTP || isChangeQuantity;
                        transaction.Status = (byte)MyUtilities.Transaction.Status.Processing;
                        vfi.SaveChanges();
                        var approve = UpdateApproveTransactionProduct(transaction.TransactionId,
                                                                !(isExportTP || isChangeQuantityTo));
                        if (!approve) {
                            return Json(@"Không thể cập nhật trạng thái lệnh. Xin vui lòng thử lại. (transaction).");
                        }
                        vfi.SaveChanges();
                        if (isExportTP) {
                            // tao invoice
                            invoice = new Vfi.Models.Invoice();
                            invoice.InvoiceNumber = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Invoice, 1);
                            invoice.CustomerId = exportTP.CustomerId.Value;
                            //invoice.OrderId = Convert.ToInt32(order.OrderId); -- tamma
                            invoice.ExportId = exportTP.ExportId;
                            invoice.Active = true;
                            invoice.ShipmentDate = exportTP.DateTransporter;
                            invoice.ModifiedDate = DateTime.Now;
                            invoice.ModifiedUser = HttpContext.User.Identity.Name;
                            invoice.ExchangeRate = 1;
                            invoice.TaxPercent = 0;
                            invoice.Status = (byte)MyUtilities.Sales.Status.Waiting;
                            vfi.Invoices.Add(invoice);
                            vfi.SaveChanges();
                        }
                        else if (isChangeQuantityBack) {
                            var transactionReturn = new Vfi.Models.Transaction {
                                WarehouseIssueId = MyUtilities.Warehouse.Business,
                                WarehouseReceiptId = MyUtilities.Warehouse.Return,
                                TransactionCode = transaction.TransactionCode,
                                EoI = "2",
                                MoP = false,
                                CreatedUser = HttpContext.User.Identity.Name,
                                CreatedDate = transaction.CreatedDate,
                                Status = (byte)MyUtilities.Transaction.Status.Open,
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now
                            };
                            transactionReturn.TransactionDetails.AddRange(transaction.TransactionDetails);
                            vfi.Transactions.Add(transactionReturn);
                            vfi.SaveChanges();
                            approve = UpdateApproveTransactionProduct(transactionReturn.TransactionId, false);
                            if (!approve) {
                                return
                                    Json(
                                        @"Không thể cập nhật trạng thái lệnh. Xin vui lòng thử lại. (transactionReturn).");
                            }
                        }
                        else if (isSendQuantityBack) {
                            var exportDetails = vfi.ExportFormTP_KDDetail.Where(ed => ed.ExportId == exportTP.ExportId);
                            var isComplete = true;
                            foreach (var exportDetail in exportDetails) {
                                if (exportDetail.InvoiceDetails.Where(id => id.Active).Sum(id => id.Piece) != 0
                                    && exportDetail.IsInvoiced == false) {
                                    isComplete = false;
                                    //break;
                                }
                                else {
                                    exportDetail.IsInvoiced = true;
                                }
                            }
                            if (isComplete)
                                invoice.Status = (byte)MyUtilities.Sales.Status.Completed;
                        }
                        else if (isChangeQuantityTo) {
                            var transactionReturn = new Vfi.Models.Transaction {
                                WarehouseIssueId = MyUtilities.Warehouse.Return,
                                WarehouseReceiptId = MyUtilities.Warehouse.Business,
                                TransactionCode = transaction.TransactionCode,
                                EoI = "2",
                                MoP = false,
                                CreatedUser = HttpContext.User.Identity.Name,
                                CreatedDate = transaction.CreatedDate,
                                Status = (byte)MyUtilities.Transaction.Status.Open,
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now
                            };
                            foreach (var detail in transaction.TransactionDetails) {
                                var newDetail = new Vfi.Models.TransactionDetail {
                                    Transaction = transactionReturn,
                                    TransactionId = transactionReturn.TransactionId,
                                    ReferenceId = detail.ReferenceId,
                                    MoP = false,
                                    Quantity = detail.Quantity,
                                    Active = true,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    ModifiedDate = DateTime.Now,
                                    QuantityKg = detail.QuantityKg,
                                    // Note = detail.DetailId + "",
                                };
                                transactionReturn.TransactionDetails.Add(newDetail);
                            }
                            vfi.Transactions.Add(transactionReturn);
                            vfi.SaveChanges();
                            approve = UpdateApproveTransactionProduct(transactionReturn.TransactionId, false);
                            if (!approve) {
                                return
                                    Json(
                                        @"Không thể cập nhật trạng thái lệnh. Xin vui lòng thử lại. (transactionReturn).");
                            }
                        }
                        else if (isImportPurchase) {
                            if (!purchaseOrder.PurchaseOrderDetails.Any(pod => !(pod.IsComplete ?? false))) {
                                purchaseOrder.Status = (byte)MyUtilities.Sales.Status.Completed;
                                vfi.SaveChanges();
                            }
                        }
                        else if (isExportProduction2) {
                            var production2Periods = new List<Production2InventoryPeriod>();
                            var transaction2 = new Production2Transaction {
                                TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Production2, 1),
                                EoI = Convert.ToChar(MyUtilities.Transaction.EoIEnum.Import).ToString(),
                                CreateDate = transaction.CreatedDate,
                                Status = (byte)MyUtilities.Transaction.Status.Approved,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                Production2TransactionDetail = new List<Production2TransactionDetail>(),
                            };
                            foreach (var detail in transaction.TransactionDetails) {
                                var product =
                                    vfi.Products.FirstOrDefault(p => p.ProductId == detail.ReferenceId);
                                if (product == null)
                                    throw new AggregateException("Lỗi! Không tìm thấy SP");
                                if (product.Production2Weight == 0)
                                    throw new AggregateException("Lỗi! Vui lòng cập nhật trọng lượng SX 2 - " +
                                                                 product.ProductCode);
                                if (!product.ProductionSections.Any(ps => ps.Active))
                                    throw new AggregateException("Lỗi! Liên hệ kỹ thuật cập nhật công đoạn SX 2 - " +
                                                                 product.ProductCode);
                                var firstSection =
                                    product.ProductionSections.Where(ps => ps.Active)
                                           .OrderBy(ps => ps.SectionIndex)
                                           .FirstOrDefault();

                                var entity = new Production2TransactionDetail {
                                    Production2Transaction = transaction2,
                                    TransactionId = transaction2.TransactionId,
                                    ProductId = detail.ReferenceId.Value,
                                    SectionIssueId = null,
                                    SectionReceiptId = firstSection.ProductionSectionId,
                                    UnitMeasure = "Kg",
                                    QuantityKg =
                                        Math.Round((product.ProductionWeight ?? 0) * (detail.Quantity) / 1000, 3),
                                    Quantity = detail.Quantity,
                                    Note = detail.Note + "",
                                    QuantityDefect = 0,
                                    QuantityLost = 0,
                                    Time = 0,
                                    OverTime = 0,
                                    SectionIndex = firstSection.SectionIndex,
                                };
                                transaction2.Production2TransactionDetail.Add(entity);
                                var production2Inv = vfi.Production2Inventory.FirstOrDefault(
                                    pi =>
                                    pi.ProductionSectionId == firstSection.ProductionSectionId);
                                if (production2Inv == null) {
                                    production2Inv = new Production2Inventory {
                                        ProductionSectionId = firstSection.ProductionSectionId,
                                        TotalQuantity = 0,
                                        Weight = product.ProductionWeight ?? 0,
                                    };
                                    vfi.Production2Inventory.Add(production2Inv);
                                }
                                var period = new Production2InventoryPeriod {
                                    EarlyQuantity = production2Inv.TotalQuantity,
                                    Quantity = entity.QuantityKg,
                                    LastQuantity = production2Inv.TotalQuantity + entity.QuantityKg,
                                    PeriodDate = transaction2.CreateDate,
                                    TransactionId = transaction2.TransactionId,
                                    Production2Inventory = production2Inv,
                                    Weight = production2Inv.Weight,
                                };
                                production2Inv.TotalQuantity = period.LastQuantity;
                                production2Periods.Add(period);
                            }
                            if (transaction2.Production2TransactionDetail.Any()) {
                                vfi.Production2Transaction.Add(transaction2);
                                vfi.Production2InventoryPeriod.AddRange(production2Periods);
                                vfi.SaveChanges();
                            }
                        }
                        else if (transaction.WarehouseReceiptId == MyUtilities.Warehouse.Defect) {
                            var importSx1 =
                                vfi.ImportFormSX1.FirstOrDefault(
                                    i => i.TransactionCode.Equals(transaction.TransactionCode));

                            var importWorkpiece = new ImportWorkpieceMaterial {
                                ImportDate = transaction.CreatedDate,
                                TransactionId = transaction.TransactionId,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = HttpContext.User.Identity.Name,
                            };
                            if (importSx1 != null) {
                                importWorkpiece = importSx1.ImportWorkpieceMaterials.FirstOrDefault();
                            }
                            var list = new List<WorkpieceMaterialPeriod>();
                            foreach (var identity in MaterialIdentityCode.GetMaterialIdentityCodes(0)) {
                                var totalQuantity =
                                    vfi.WorkpieceMaterialPeriods.Where(
                                        p => p.IdentityCode.Equals(identity.IdentityCode) && p.Type == 3)
                                        .ToList()
                                       .Sum(p => p.LastQuantity - p.EarlyQuantity);
                                var period = new WorkpieceMaterialPeriod {
                                    IdentityCode = identity.IdentityCode,
                                    Type = 3,
                                    EarlyQuantity = totalQuantity,
                                    EoIId = importWorkpiece.ImportId,
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    PeriodDate = importWorkpiece.ImportDate.Value,
                                    IsDestroy = false,
                                    EoI = (int)MyUtilities.Transaction.EoIEnum.Import,
                                };
                                if (importSx1 != null) {
                                    var importDetails =
                                        importSx1.ImportFormSX1Detail.Where(
                                            id =>
                                            id.DefectProduct1 + id.DefectProduct2 > 0 &&
                                            id.MaterialInventory.Material.MaterialType.IdentityCode.Equals(
                                                identity.IdentityCode));
                                    if (importDetails.Any()) {
                                        period.Weight =
                                            Math.Round(importDetails.Sum(
                                                id => (id.DefectProduct1 + id.DefectProduct2) * id.ProductWeight / 1000), 2);
                                    }
                                    period.LastQuantity = Math.Round(period.EarlyQuantity + period.Weight, 2);
                                    list.Add(period);
                                }
                                else {
                                    var transactionDetails =
                                        transaction.TransactionDetails.Where(
                                            td =>
                                            td.Product.ProductionMaterials.Any() &&
                                            td.Product.ProductionMaterials.FirstOrDefault()
                                              .Material.MaterialType.IdentityCode.Equals(identity.IdentityCode))
                                                   .ToList();
                                    if (transactionDetails.Any()) {
                                        period.Weight =
                                            Math.Round(
                                                transactionDetails.Sum(td => td.Quantity * td.Product.QcWeight / 1000).Value, 2);
                                        period.LastQuantity = Math.Round(period.EarlyQuantity + period.Weight, 2);
                                        importWorkpiece.WorkpieceMaterialPeriods.Add(period);
                                    }
                                }
                            }
                            if (importSx1 != null)
                                vfi.WorkpieceMaterialPeriods.AddRange(list);
                            else
                                vfi.ImportWorkpieceMaterials.Add(importWorkpiece);
                            vfi.SaveChanges();
                        }
                        return Json("okie");
                    }
                }
            }
            catch (Exception exception) {
                return Json(@"Error!! " + exception.Message);
            }
            return Json("null");
        }

        public bool UpdateApproveTransactionProduct(long transactionId, bool checkInventory) {
            bool flag = false;
            try {
                if (!Request.IsAuthenticated)
                    throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
                using (var vfi = new tammaContext()) {
                    var productInventories = new List<Vfi.Models.ProductInventory>();
                    var productInventoryPeriods = new List<Vfi.Models.ProductInventoryPeriod>();
                    var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
                    var productInventorys =
                        vfi.ProductInventories.Where(
                            pi =>
                            pi.WarehouseId == transaction.WarehouseIssueId ||
                            pi.WarehouseId == transaction.WarehouseReceiptId);
                    foreach (var transactionDetail in transaction.TransactionDetails) {
                        // kho xuat
                        if (transaction.WarehouseIssueId != null) {
                            // get ton kho
                            var invIssue = productInventorys.FirstOrDefault(pi => pi.ProductInventoryId == transactionDetail.ProductInvId);
                            if (invIssue == null)
                                invIssue =
                                    productInventorys.FirstOrDefault(
                                        pi =>
                                        pi.ProductId == transactionDetail.ReferenceId &&
                                        pi.WarehouseId == transaction.WarehouseIssueId &&
                                        pi.ErrorId == transactionDetail.ErrorId);
                            if (invIssue == null) {
                                invIssue = new Vfi.Models.ProductInventory {
                                    Active = true,
                                    TotalQty = 0,
                                    ProductId = transactionDetail.ReferenceId.Value,
                                    WarehouseId = transaction.WarehouseIssueId.Value,
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    ErrorId = transactionDetail.ErrorId,
                                    VendorId = transactionDetail.VendorId,
                                };
                                productInventories.Add(invIssue);
                            }
                            if (invIssue.ErrorId != null)
                                invIssue.ExportDate = DateTime.Now;
                            var lastPeriodQuantity = invIssue.TotalQty - transactionDetail.Quantity;
                            if ((lastPeriodQuantity + 2) < 0) {
                                if (checkInventory)
                                    throw new ArgumentException(invIssue.Warehouse.WarehouseName +
                                                                " không đủ tồn kho !");
                            }
                            // create luan chuyen kho xuat
                            var pipIssue = new Vfi.Models.ProductInventoryPeriod {
                                TransactionId = transaction.TransactionId,
                                ProductId = transactionDetail.ReferenceId ?? 0,

                                PeriodDate = transaction.CreatedDate,

                                WarehouseId = transaction.WarehouseIssueId.Value,
                                Quantity = transactionDetail.Quantity,

                                UnitMeasure = transactionDetail.UnitMeasure,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                EarlyPeriodQuantity = invIssue.TotalQty,
                                LastPeriodQuantity = lastPeriodQuantity,
                                ProductInventory = invIssue,
                            };
                            //pipIssue.EarlyPeriodQuantity = totalQualityIssue != null
                            //                                   ? totalQualityIssue.TotalQty
                            //                                   : 0;
                            //pipIssue.LastPeriodQuantity = pipIssue.EarlyPeriodQuantity - pipIssue.Quantity;
                            productInventoryPeriods.Add(pipIssue);
                            // update lai ton kho xuat
                            invIssue.TotalQty -= pipIssue.Quantity;
                        }

                        // get ton kho nhap
                        var invReceipt =
                            productInventorys.FirstOrDefault(
                                pi =>
                                pi.ProductId == transactionDetail.ReferenceId &&
                                pi.WarehouseId == transaction.WarehouseReceiptId);
                        if (invReceipt == null) {
                            invReceipt = new Vfi.Models.ProductInventory {
                                ProductId = transactionDetail.ReferenceId ?? 0,
                                TotalQty = 0,
                                WarehouseId = transaction.WarehouseReceiptId.Value,
                                Active = true,
                                UnitMeasure = transactionDetail.UnitMeasure,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                ImportDate = transaction.CreatedDate,
                                ErrorId = transactionDetail.ErrorId,
                                VendorId = transactionDetail.VendorId,
                            };
                            productInventories.Add(invReceipt);
                        }
                        else {
                            if (transaction.WarehouseReceiptId == MyUtilities.Warehouse.Processing &&
                                transactionDetail.ErrorId != null &&
                                invReceipt.ImportDate != transaction.CreatedDate) {
                                invReceipt = new Vfi.Models.ProductInventory {
                                    ProductId = transactionDetail.ReferenceId ?? 0,
                                    TotalQty = 0,
                                    WarehouseId = transaction.WarehouseReceiptId.Value,
                                    Active = true,
                                    UnitMeasure = transactionDetail.UnitMeasure,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    ModifiedDate = DateTime.Now,
                                    ImportDate = transaction.CreatedDate,
                                    ErrorId = transactionDetail.ErrorId,
                                    VendorId = transactionDetail.VendorId,
                                };
                                productInventories.Add(invReceipt);
                            }
                        }
                        if (invReceipt.ImportDate == null)
                            invReceipt.ImportDate = transaction.CreatedDate;
                        // create luan chuyen kho nhap
                        var pipReceipt = new Vfi.Models.ProductInventoryPeriod {
                            TransactionId = transaction.TransactionId,
                            ProductId = transactionDetail.ReferenceId ?? 0,

                            PeriodDate = transaction.CreatedDate,
                            WarehouseId = transaction.WarehouseReceiptId.Value,
                            Quantity = transactionDetail.Quantity,
                            UnitMeasure = transactionDetail.UnitMeasure,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            ProductInventory = invReceipt
                        };
                        pipReceipt.EarlyPeriodQuantity = invReceipt.TotalQty;
                        pipReceipt.LastPeriodQuantity = pipReceipt.EarlyPeriodQuantity + pipReceipt.Quantity;
                        productInventoryPeriods.Add(pipReceipt);
                        invReceipt.TotalQty += pipReceipt.Quantity;
                    }

                    transaction.Status = (byte)MyUtilities.Transaction.Status.Approved;
                    vfi.ProductInventories.AddRange(productInventories);
                    vfi.ProductInventoryPeriods.AddRange(productInventoryPeriods);
                    var rsTran = vfi.SaveChanges();

                    if (rsTran > 0)
                        flag = true;
                }
            }
            catch (ArgumentException ae) {
                using (var vfi = new tammaContext()) {
                    var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
                    transaction.Status = (byte)MyUtilities.Transaction.Status.Open;
                    vfi.SaveChanges();
                }
                throw new AggregateException(ae.Message);
            }
            catch (Exception ex) {
                throw new AggregateException(ex.Message);
            }
            return flag;
        }

        public ActionResult CancelTransactionForProduct(long[] checkedRecords) {
            try {
                if (!Request.IsAuthenticated)
                    return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

                var modifiedUser = HttpContext.User.Identity.Name;
                if (string.IsNullOrWhiteSpace(modifiedUser))
                    return Json(@"Vui lòng đăng nhập hệ thống. (user null). ");
                if (checkedRecords.Count() <= 0)
                    return Json(@"Vui lòng chọn ít nhất 1 lệnh. (checkedRecords <= 0). ");

                using (var vfi = new tammaContext()) {
                    var transactionId = checkedRecords[0];
                    var transaction = vfi.Transactions.FirstOrDefault(x => x.TransactionId == transactionId);
                    if (transaction.WarehouseIssueId == MyUtilities.Warehouse.Production1) {
                        var transactionProduction1 = vfi.Transactions
                            .FirstOrDefault(x => x.WarehouseReceiptId == MyUtilities.Warehouse.Production1 
                                                && x.TransactionCode.Equals(transaction.TransactionCode));
                        if (transactionProduction1 != null) {
                            if (transactionProduction1.Status != (byte)MyUtilities.Transaction.Status.Cancel) {
                                throw new AggregateException("Lỗi! Sản xuất 1 đã duyệt nên không thể huỷ phiếu này.");
                            }
                        }
                    }
                    if (transaction.WarehouseReceiptId == MyUtilities.Warehouse.Processing) {
                        var defectTransaction = vfi.DefectTransactions.FirstOrDefault(x => x.ImportTransactionId == transaction.TransactionId);
                        if (defectTransaction != null) {
                            defectTransaction.Status = (byte)MyUtilities.Transaction.Status.Cancel;
                        }
                    }
                    transaction.Status = (byte)MyUtilities.Transaction.Status.Cancel;
                    vfi.SaveChanges();
                }


            }
            catch (Exception exception) {
                return Json(exception.Message);
            }

            return Json("okie");
        }

        [GridAction]
        public ActionResult SelectExportProcessing() {
            return View(new GridModel(new List<ProductInventoryRotateModel>()));
        }

        [GridAction]
        public ActionResult UpdateExportProcessing(
            [Bind(Prefix = "inserted")] IEnumerable<ProductInventoryRotateModel> inserteds,
            [Bind(Prefix = "updated")] IEnumerable<ProductInventoryRotateModel> updateds,
            [Bind(Prefix = "deleted")] IEnumerable<ProductInventoryRotateModel> deleteds,
           int warehouseId, string date) {
            if (inserteds != null || updateds != null) {
                try {
                    if (!Request.IsAuthenticated)
                        throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

                    var modifiedUser = HttpContext.User.Identity.Name;
                    if (string.IsNullOrWhiteSpace(modifiedUser))
                        throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (user null). ");
                    ModelState.Clear();
                    var ci = new CultureInfo("vi-VN");
                    var createdDate = string.IsNullOrWhiteSpace(date)
                                          ? DateTime.Today
                                          : Convert.ToDateTime(date, ci);
                    var list = new List<ProductInventoryRotateModel>();
                    if (updateds != null && updateds.Any())
                        list.AddRange(updateds.Where(i => i.Quantity > 0).ToList());
                    if (inserteds != null && inserteds.Any())
                        list.AddRange(inserteds.Where(i => i.Quantity > 0).ToList());
                    var transaction = new Vfi.Models.Transaction {
                        //WarehouseIssueId = warehouseIssueId,
                        //WarehouseReceiptId = warehouseReceiptId,
                        WarehouseIssueId = warehouseId,
                        WarehouseReceiptId = MyUtilities.Warehouse.Processing,

                        TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                        EoI = "2",
                        MoP = MyUtilities.Transaction.MoP.Product,
                        CreatedUser = HttpContext.User.Identity.Name,
                        CreatedDate = createdDate,
                        Status = (byte)MyUtilities.Transaction.Status.Open,
                        Active = true,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                    };
                    var msg = "";
                    var checks = list.Where(i => i.ErrorId == 0);
                    if (checks.Any()) {
                        foreach (var detail in checks) {
                            msg += "Vui lòng chọn lỗi ! " + detail.ProductCode + "\n";
                        }
                    }
                    var productIds = list.Select(l => l.ProductId).Distinct().ToList();
                    using (var vfi = new tammaContext()) {
                        // kiem tra thong tin
                        var productInvs =
                            vfi.ProductInventories.Where(
                                pi => pi.WarehouseId == warehouseId && productIds.Contains(pi.ProductId));
                        foreach (var productId in productIds) {
                            var details = list.Where(l => l.ProductId == productId).ToList();
                            var inv =
                                productInvs.FirstOrDefault(
                                    pi => pi.ProductId == productId && pi.WarehouseId == warehouseId);
                            if (inv == null || details.Sum(d => d.Quantity) > inv.TotalQty)
                                msg += "Sản xuất xuất kho lớn hơn tồn ! " + details.FirstOrDefault().ProductCode + "\n";
                        }

                        if (!string.IsNullOrWhiteSpace(msg))
                            throw new AggregateException(msg);

                        foreach (var detail in list) {
                            var productInv = productInvs.FirstOrDefault(ps => ps.ProductId == detail.ProductId);
                            if (productInv == null)
                                throw new AggregateException("Không tìm thấy tồn kho sản phẩm " + detail.ProductCode);
                            var entity = new Vfi.Models.TransactionDetail {
                                ReferenceId = detail.ProductId,
                                MoP = MyUtilities.Transaction.MoP.Product,
                                Quantity = detail.Quantity,
                                UnitMeasure = detail.UnitMeasure,
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                Note = detail.Note,
                                TransactionId = transaction.TransactionId,
                                ErrorId = detail.ErrorId,
                                ProductInvId = productInv.ProductInventoryId
                            };
                            transaction.TransactionDetails.Add(entity);
                        }
                        if (transaction.TransactionDetails.Any()) {
                            vfi.Transactions.Add(transaction);
                            vfi.SaveChanges();
                        }
                    }
                }
                catch (Exception ex) {
                    ModelState.AddModelError("UpdateProduction2Rotate", ex.Message);
                }
            }


            return View(new GridModel(new List<Production2Model>()));
        }

        [HttpPost]
        public ActionResult ChoiceTransactionByStockOrder(long[] checkedRecords) {
            try {
            }
            catch (Exception exception) {
                return Json("loi try-catch. \r\n " + exception.Message);
            }


            return Json("okie");
        }

        [GridAction]
        public ActionResult SelectWaitingProductTransactions() {
            var model = new List<TransactionModel>();
            try {
                model = GetWaitingProductTransactions();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectWaitingProductTransactions", ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult ApproveProductTransaction(TransactionModel entity) {
            try {
                using (var vfi = new tammaContext()) {
                    var transaction =
                        vfi.Transactions.FirstOrDefault(
                            t =>
                                t.TransactionId == entity.TransactionId &&
                                t.Status == (byte)MyUtilities.Transaction.Status.Open);
                    if (transaction == null)
                        throw new AggregateException("Lỗi! Vui lòng làm mới lại danh sách!");
                    if (transaction.WarehouseReceiptId == (byte)MyUtilities.Warehouse.Defect) {
                        transaction.CreatedDate = DateTime.Now.Date;
                    }
                    if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, transaction.CreatedDate)) {
                        throw new AggregateException(
                            @"Không có quyền duyệt phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                    }
                    #region 1. define + validate
                    bool isExportTP = false;
                    bool isSendQuantityBack = false;
                    bool isChangeQuantityBack = false;
                    bool isChangeQuantityTo = false;
                    bool isImportPlating = false;
                    bool isImportPurchase = false;
                    bool isExportProduction2 = false;
                    ExportFormTP_KD exportTP = null;
                    OrderNote orderNote = null;
                    Invoice invoice = null;
                    ExportChangeProduct exportChange = null;
                    PurchaseOrder purchaseOrder = null;
                    if (//transaction.WarehouseReceiptId != (byte) MyUtilities.Warehouse.Business &&
                        transaction.WarehouseIssueId != null) {
                        var productInvIds =
                            transaction.TransactionDetails.Select(td => td.ProductInvId.Value).Distinct().ToList();
                        var productInvs =
                            vfi.ProductInventories.Where(pi => productInvIds.Contains(pi.ProductInventoryId));
                        var msg = "";
                        foreach (var productInvId in productInvIds) {
                            var details =
                                transaction.TransactionDetails.Where(l => l.ProductInvId == productInvId).ToList();
                            var inv = productInvs.FirstOrDefault(pi => pi.ProductInventoryId == productInvId);
                            if (inv == null || details.Sum(d => d.Quantity) > inv.TotalQty)
                                msg += "Số lượng xuất kho lớn hơn tồn ! " + details.FirstOrDefault().Product.ProductCode +
                                       "\n";
                        }
                        if (!string.IsNullOrWhiteSpace(msg)) {
                            throw new AggregateException(msg);
                        }
                    }
                    if (transaction.PoId != null) {
                        var importPo =
                            vfi.ImportPurchaseOrders.FirstOrDefault(
                                i => i.TransactionId == transaction.TransactionId);
                        if (importPo != null)
                            if (importPo.PurchaseOrderId != null)
                                purchaseOrder =
                                    vfi.PurchaseOrders.FirstOrDefault(
                                        po => po.PurchaseOrderId == importPo.PurchaseOrderId);
                        if (purchaseOrder != null)
                            isImportPurchase = true;
                    }

                    if (transaction.WarehouseReceiptId == (byte)MyUtilities.Warehouse.Production1) {
                        var importSx = vfi.ImportFormSX1.FirstOrDefault(i => i.TransactionCode.Equals(transaction.TransactionCode));
                        if (importSx != null) {
                            if (MyUtilities.Transaction.IsLock(transaction.CreatedDate,
                                MyUtilities.Transaction.ProductionLockType.Production1)) {
                                throw new AggregateException(
                                    @"Ngày nhập SX bị khoá do tính lương! /n Vui lòng liên hệ quản lý tính lương !");
                            }
                        }
                    }
                    else if (transaction.WarehouseIssueId == (byte)MyUtilities.Warehouse.Cnc) {
                        var importSx = vfi.ImportFormCncs.FirstOrDefault(i => i.TransactionCode.Equals(transaction.TransactionCode));
                        if (importSx != null) {
                            if (MyUtilities.Transaction.IsLock(transaction.CreatedDate,
                                MyUtilities.Transaction.ProductionLockType.CNC)) {
                                throw new AggregateException(
                                    @"Ngày nhập SX bị khoá do tính lương! /n Vui lòng liên hệ quản lý tính lương !");
                            }
                        }
                    }
                    else if (transaction.WarehouseIssueId == (byte)MyUtilities.Warehouse.Finish &&
                             transaction.WarehouseReceiptId == (byte)MyUtilities.Warehouse.Business) {
                        exportTP =
                            vfi.ExportFormTP_KD.FirstOrDefault(
                                e => e.TransactionCode.Equals(transaction.TransactionCode));
                        if (exportTP != null) {
                            isExportTP = true;
                        }
                        else {
                            exportChange =
                                vfi.ExportChangeProducts.FirstOrDefault(
                                    on => on.TransactionId == transaction.TransactionId);
                            if (exportChange != null) {
                                isChangeQuantityTo = true;
                            }
                        }
                    }
                    //else if (transaction.WarehouseIssueId == null &&
                    //         transaction.WarehouseReceiptId == (byte)MyUtilities.Warehouse.QcA) {
                    //    var importPo =
                    //        vfi.ImportPurchaseOrders.FirstOrDefault(
                    //            i => i.TransactionId == transaction.TransactionId);
                    //    if (importPo != null)
                    //        if (importPo.PurchaseOrderId != null)
                    //            purchaseOrder =
                    //                vfi.PurchaseOrders.FirstOrDefault(
                    //                    po => po.PurchaseOrderId == importPo.PurchaseOrderId);
                    //    if (purchaseOrder != null)
                    //        isImportPurchase = true;
                    //}
                    else if (transaction.WarehouseIssueId == (byte)MyUtilities.Warehouse.Business &&
                             transaction.WarehouseReceiptId == (byte)MyUtilities.Warehouse.Finish) {
                        orderNote =
                            vfi.OrderNotes.FirstOrDefault(
                                on => on.TransactionId == transaction.TransactionId);
                        if (orderNote != null) {
                            if (orderNote.NoteType == 1) {
                                invoice = vfi.Invoices.FirstOrDefault(i => i.InvoiceId == orderNote.InvoiceId);
                                exportTP = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == invoice.ExportId);
                                if (exportTP != null) {
                                    isSendQuantityBack = true;
                                }
                            }
                            else if (orderNote.NoteType == 2) {
                                isChangeQuantityBack = true;
                            }
                        }
                    }
                    else if ((transaction.WarehouseIssueId == (byte)MyUtilities.Warehouse.Plating ||
                              transaction.WarehouseIssueId == (byte)MyUtilities.Warehouse.PlatingTest)
                             && transaction.WarehouseReceiptId == (byte)MyUtilities.Warehouse.QcB) {
                        var importPlating =
                            vfi.ImportNCU_QCB.FirstOrDefault(i => i.TransactionId == transaction.TransactionId);
                        if (importPlating != null) {
                            isImportPlating = true;
                        }
                    }
                    else if (transaction.WarehouseIssueId == (byte)MyUtilities.Warehouse.Production2
                             && (transaction.WarehouseReceiptId == (byte)MyUtilities.Warehouse.Production2B
                                 || transaction.WarehouseReceiptId == (byte)MyUtilities.Warehouse.Production2C)) {
                        var message = "";
                        var transactionDetails =
                            transaction.TransactionDetails.Where(
                                td => !td.Product.ProductionSections.Any(pm => pm.Active == true));
                        foreach (var transactionDetail in transactionDetails) {
                            message += transactionDetail.Product.ProductCode + " cần cập nhật công đoạn sản phẩm \n";
                        }
                        transactionDetails =
                            transaction.TransactionDetails.Where(
                                td => td.Product.Production2Weight == null || td.Product.Production2Weight == 0);
                        foreach (var transactionDetail in transactionDetails) {
                            message += transactionDetail.Product.ProductCode +
                                       " cần cập nhật trọng lượng sản xuất 2 \n";
                        }
                        if (!string.IsNullOrWhiteSpace(message))
                            throw new AggregateException("Lỗi! \n" + message);
                        isExportProduction2 = true;
                    }
                    else if (transaction.WarehouseReceiptId == MyUtilities.Warehouse.Defect) {
                        var importSx1 =
                            vfi.ImportFormSX1.FirstOrDefault(
                                i => i.TransactionCode.Equals(transaction.TransactionCode));
                        if (importSx1 == null) {
                            var message = "";
                            var transactionDetails =
                                transaction.TransactionDetails.Where(
                                    td => !td.Product.ProductionMaterials.Any(pm => pm.Active));
                            foreach (var transactionDetail in transactionDetails) {
                                message += transactionDetail.Product.ProductCode +
                                           " cần cập nhật nguyên liệu sản phẩm \n";
                            }
                            transactionDetails =
                                transaction.TransactionDetails.Where(
                                    td => td.Product.QcWeight == null || td.Product.QcWeight == 0);
                            foreach (var transactionDetail in transactionDetails) {
                                message += transactionDetail.Product.ProductCode + " cần cập nhật trọng lượng QC \n";
                            }
                            if (!string.IsNullOrWhiteSpace(message))
                                throw new AggregateException("Lỗi! \n" + message);
                        }
                    }
                    else if (transaction.WarehouseReceiptId == MyUtilities.Warehouse.Processing) {

                    }
                    if (isExportTP || isSendQuantityBack || isChangeQuantityTo || isImportPlating ||
                        isImportPurchase) {
                        var transactionDetails = transaction.TransactionDetails;
                        foreach (var transactionDetail in transactionDetails) {
                            if (isSendQuantityBack) {
                                var exportDetail =
                                    vfi.ExportFormTP_KDDetail.FirstOrDefault(
                                        ed =>
                                            ed.ProductId == transactionDetail.ReferenceId &&
                                            ed.ExportId == exportTP.ExportId);
                                if (exportDetail.IsInvoiced == true)
                                    throw new ArgumentException(
                                        "Chi tiết xuất đã xuất hóa đơn không thể nhập trả " +
                                        transactionDetail.Product.ProductCode);
                                //if (exportDetail.InvoiceDetails.Where(id => id.Active).Sum(id => id.Piece) <
                                //    transactionDetail.Quantity)
                                //    throw new ArgumentException("Số lượng trả vượt quá số lượng giao !" +
                                //                                transactionDetail.Product.ProductCode);
                                orderNote = vfi.OrderNotes.FirstOrDefault(
                                    on => on.TransactionId == transaction.TransactionId);
                                if (orderNote.NoteType == 1)
                                    exportDetail.Note += "\nTrả hàng :" + transactionDetail.Quantity;
                                else if (orderNote.NoteType == 2)
                                    exportDetail.Note += "\nĐổi hàng :" + transactionDetail.Quantity;
                                var orderNodeDetail =
                                    orderNote.OrderNoteDetails.FirstOrDefault(
                                        ond =>
                                            ond.OrderNote.InvoiceId ==
                                            exportDetail.ExportFormTP_KD.Invoices.FirstOrDefault().InvoiceId
                                            && ond.ProductId == exportDetail.ProductId);
                                var invoiceDetailBack = new InvoiceDetail {
                                    Piece = Convert.ToInt32(transactionDetail.Quantity * -1),
                                    NoteDetailId = orderNodeDetail.NoteDetailId,
                                    //Price = exportDetail.InvoiceDetails.FirstOrDefault(id => id.Active).Price,
                                    ProductId = exportDetail.ProductId,
                                    Active = true,
                                    ExportDetailId = exportDetail.DetailId,
                                    InvoiceId = orderNote.InvoiceId,
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    Note = "Trả hàng " + orderNote.NoteNumber + " " + orderNodeDetail.Note,
                                };
                                invoiceDetailBack.Price = exportDetail.InvoiceDetails.Any(id => id.Active)
                                    ? exportDetail.InvoiceDetails.FirstOrDefault(id => id.Active).Price
                                    : 0;
                                //throw new AggregateException("break");

                                var transactionExportDetail =
                                    vfi.TransactionDetails.FirstOrDefault(
                                        td =>
                                            td.Transaction.TransactionCode ==
                                            exportDetail.ExportFormTP_KD.TransactionCode &&
                                            td.LotNumber.Equals(transactionDetail.LotNumber) &&
                                            td.ReferenceId == transactionDetail.ReferenceId);
                                transactionExportDetail.Quantity += invoiceDetailBack.Piece;
                                exportDetail.Quality += invoiceDetailBack.Piece;
                                var totalSendBack = transactionDetail.Quantity;
                                var invoiceDetails =
                                    exportDetail.InvoiceDetails.Where(id => id.Active && id.Piece > 0);
                                foreach (var invoiceDetail in invoiceDetails) {
                                    var orderDetail = invoiceDetail.OrderDetail;
                                    if (totalSendBack > invoiceDetail.Piece) {
                                        totalSendBack -= invoiceDetail.Piece;
                                        orderDetail.OrderQty -= invoiceDetail.Piece;
                                        orderDetail.Note += " !Trả hàng:" + invoiceDetail.Piece;
                                    }
                                    else {
                                        orderDetail.OrderQty = orderDetail.OrderQty -
                                                               Convert.ToInt32(totalSendBack);
                                        orderDetail.Note += " !Trả hàng:" + totalSendBack;
                                        totalSendBack = 0;
                                    }
                                    if (totalSendBack == 0) break;
                                }
                                vfi.InvoiceDetails.Add(invoiceDetailBack);
                            }
                            else if (isChangeQuantityTo) {
                                var orderNoteDetail =
                                    vfi.OrderNoteDetails.FirstOrDefault(
                                        ond =>
                                            ond.NoteId == exportChange.NoteId &&
                                            ond.ProductId == transactionDetail.ReferenceId);
                                var exportChanges =
                                    vfi.ExportChangeProducts.Where(
                                        ec =>
                                            ec.NoteId == exportChange.NoteId &&
                                            ec.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved);
                                var alreadyChangeQuantity =
                                    exportChanges.Sum(
                                        ec =>
                                            ec.Transaction.TransactionDetails.FirstOrDefault(
                                                td => td.ReferenceId == transactionDetail.ReferenceId).Quantity);
                                if (orderNoteDetail.Quantity.Value - transactionDetail.Quantity - alreadyChangeQuantity < 0)
                                    throw new AggregateException("Không thể đổi quá số lượng cần thiết " +
                                                                 transactionDetail.Product.ProductCode);
                            }
                            else if (isImportPlating) {
                                var importPlating =
                                    vfi.ImportNCU_QCBDetail.FirstOrDefault(
                                        id =>
                                            id.ImportNCU_QCB.TransactionId == transaction.TransactionId &&
                                            id.ProductId == transactionDetail.ReferenceId);
                                var exportPlating =
                                    vfi.ExportGCN_NCUDetail.FirstOrDefault(
                                        ed => ed.DetailId == importPlating.ExportDetailId);
                                var importPlatingElses =
                                    vfi.ImportNCU_QCBDetail.Where(
                                        id =>
                                            id.ExportDetailId == exportPlating.DetailId &&
                                            id.ImportNCU_QCB.Transaction.Status ==
                                            (byte)MyUtilities.Transaction.Status.Approved).ToList();

                                var importElse = 0.0;
                                if (importPlatingElses.Any())
                                    importElse = importPlatingElses.Sum(id => id.RealNumber);
                                if (importPlating.RealNumber > exportPlating.RealNumber - importElse)
                                    throw new AggregateException("Không nhập quá số lượng cần thiết " +
                                                                 transactionDetail.Product.ProductCode +
                                                                 " thiếu nhập " +
                                                                 (exportPlating.RealNumber - importElse));
                                //}
                            }
                            else if (isImportPurchase) {
                                var poDetail =
                                    purchaseOrder.PurchaseOrderDetails.FirstOrDefault(
                                        pod =>
                                            pod.PurchaseOrderId == purchaseOrder.PurchaseOrderId &&
                                            pod.ReferenceId == transactionDetail.ReferenceId);
                                if (poDetail != null) {
                                    poDetail.ReceivedQty += transactionDetail.Quantity;
                                    if (poDetail.OrderQty - poDetail.ReceivedQty - poDetail.RejectedQty <= 0)
                                        poDetail.IsComplete = true;
                                }
                            }
                        }
                        vfi.SaveChanges();
                    }
                    #endregion

                    #region 2. calculate inv
                    var transactionDefect = vfi.DefectTransactions.FirstOrDefault(x => x.ImportTransactionId == transaction.TransactionId);
                    var periods = new List<ProductInventoryPeriod>();
                    //var newInventories = new List<ProductInventory>();
                    if (transactionDefect == null) {
                        UpdateProductInvByTransaction(entity.TransactionId, HttpContext.User.Identity.Name);

                        //foreach (var transactionDetail in transaction.TransactionDetails) {
                        //    var productInvExport =
                        //        vfi.ProductInventories.FirstOrDefault(
                        //            pi => pi.ProductInventoryId == transactionDetail.ProductInvId);
                        //    if (transaction.WarehouseIssueId != null) {
                        //        if (productInvExport == null)
                        //            throw new AggregateException("Lỗi! Không tìm thấy kho cần xuất");
                        //        if (!isExportTP && Math.Round(productInvExport.TotalQty - transactionDetail.Quantity, 2) < 0)
                        //            throw new AggregateException("Lỗi! Tồn kho không đủ xuất " +
                        //                                         productInvExport.Product.ProductCode);
                        //        var period = new ProductInventoryPeriod() {
                        //            PeriodDate = transaction.CreatedDate,
                        //            ModifiedUser = transaction.ModifiedUser,
                        //            ModifiedDate = DateTime.Now,
                        //            ProductId = productInvExport.ProductId,
                        //            ProductInvId = productInvExport.ProductInventoryId,
                        //            WarehouseId = productInvExport.WarehouseId,
                        //            Quantity = transactionDetail.Quantity,
                        //            TransactionId = transaction.TransactionId,
                        //            EarlyPeriodQuantity = productInvExport.TotalQty,
                        //            LastPeriodQuantity = productInvExport.TotalQty - transactionDetail.Quantity,
                        //            UnitMeasure = "Pcs",
                        //            UnitPrice = productInvExport.Product.UnitPrice,
                        //            LotNumber = productInvExport.LotNumber
                        //        };
                        //        productInvExport.TotalQty = period.LastPeriodQuantity;
                        //        if (Math.Round(productInvExport.TotalQty, 2) == 0) {
                        //            productInvExport.TotalQty = 0;
                        //            productInvExport.ExportDate = DateTime.Now;
                        //        }
                        //        periods.Add(period);
                        //    }
                        //    if (transaction.WarehouseReceiptId != null) {
                        //        var lotNumber = (transactionDetail.LotNumber + "").Trim();
                        //        if (transaction.WarehouseReceiptId == MyUtilities.Warehouse.Processing) {
                        //            lotNumber = "";
                        //        }
                        //        var productInvImport =
                        //            vfi.ProductInventories.FirstOrDefault(
                        //                pi => pi.WarehouseId == transaction.WarehouseReceiptId &&
                        //                      pi.ProductId == transactionDetail.ReferenceId &&
                        //                      pi.LotNumber.Equals(lotNumber));
                        //        if (productInvImport == null) {
                        //            productInvImport = new ProductInventory {
                        //                WarehouseId = transaction.WarehouseReceiptId.Value,
                        //                ProductId = transactionDetail.ReferenceId.Value,
                        //                ImportDate = transaction.CreatedDate,
                        //                ModifiedDate = DateTime.Now,
                        //                ModifiedUser = HttpContext.User.Identity.Name,
                        //                TotalQty = 0,
                        //                LotNumber = lotNumber,
                        //                ErrorId = transactionDetail.ErrorId,
                        //                DefectId = transactionDetail.DefectId,
                        //                ByProcessMachineId = transactionDetail.NextProcessId,
                        //                MachineId = transactionDetail.MachineId,
                        //                StoreCode = transactionDetail.StoreCode,
                        //                Active = true
                        //            };
                        //            if (productInvExport != null) {
                        //                productInvImport.MaterialInvId = productInvExport.MaterialInvId;
                        //                productInvImport.MachineId = productInvExport.MachineId;
                        //            }
                        //            vfi.ProductInventories.Add(productInvImport);
                        //            vfi.SaveChanges();
                        //        }
                        //        productInvImport.ExportDate = null;
                        //        var period = new ProductInventoryPeriod() {
                        //            PeriodDate = transaction.CreatedDate,
                        //            ModifiedUser = HttpContext.User.Identity.Name,
                        //            ModifiedDate = DateTime.Now,
                        //            ProductId = productInvImport.ProductId,
                        //            ProductInvId = productInvImport.ProductInventoryId,
                        //            WarehouseId = productInvImport.WarehouseId,
                        //            Quantity = transactionDetail.Quantity,
                        //            TransactionId = transaction.TransactionId,
                        //            EarlyPeriodQuantity = productInvImport.TotalQty,
                        //            LastPeriodQuantity = productInvImport.TotalQty + transactionDetail.Quantity,
                        //            UnitMeasure = "Pcs",
                        //            UnitPrice = transactionDetail.Product.UnitPrice,
                        //            LotNumber = lotNumber
                        //        };
                        //        productInvImport.TotalQty = period.LastPeriodQuantity;
                        //        periods.Add(period);
                        //    }
                        //}
                        //transaction.Status = (byte)MyUtilities.Transaction.Status.Approved;
                        ////vfi.ProductInventories.AddRange(newInventories);
                        //vfi.ProductInventoryPeriods.AddRange(periods);
                        //vfi.SaveChanges();
                    }
                    else {
                         foreach (var transactionDetail in transaction.TransactionDetails) {
                            if (transaction.WarehouseIssueId != null) {
                                var productInvExport =
                                    vfi.ProductInventories.FirstOrDefault(
                                        pi => pi.ProductInventoryId == transactionDetail.ProductInvId);
                                if (productInvExport == null)
                                    throw new AggregateException("Lỗi! Không tìm thấy kho cần xuất");
                                if (!isExportTP && Math.Round(productInvExport.TotalQty - transactionDetail.Quantity, 2) < 0)
                                    throw new AggregateException("Lỗi! Tồn kho không đủ xuất " +
                                                                 productInvExport.Product.ProductCode);
                                var period = new ProductInventoryPeriod() {
                                    PeriodDate = transaction.CreatedDate,
                                    ModifiedUser = transaction.ModifiedUser,
                                    ModifiedDate = DateTime.Now,
                                    ProductId = productInvExport.ProductId,
                                    ProductInvId = productInvExport.ProductInventoryId,
                                    WarehouseId = productInvExport.WarehouseId,
                                    Quantity = transactionDetail.Quantity,
                                    TransactionId = transaction.TransactionId,
                                    EarlyPeriodQuantity = productInvExport.TotalQty,
                                    LastPeriodQuantity = productInvExport.TotalQty - transactionDetail.Quantity,
                                    UnitMeasure = "Pcs",
                                    UnitPrice = productInvExport.Product.UnitPrice,
                                    LotNumber = productInvExport.LotNumber
                                };
                                productInvExport.TotalQty = period.LastPeriodQuantity;
                                if (Math.Round(productInvExport.TotalQty, 2) == 0) {
                                    productInvExport.TotalQty = 0;
                                    productInvExport.ExportDate = DateTime.Now;
                                }
                                periods.Add(period);
                            }
                        }
                        foreach (var detail in transactionDefect.DefectTransactionDetails) {
                            var lotNumber = detail.ProductionDefect.DefectName;
                            //if (!string.IsNullOrWhiteSpace(detail.DefectExpand)) {
                            //    lotNumber += "-" + detail.DefectExpand;
                            //}
                            var productInvImport =
                                vfi.ProductInventories.FirstOrDefault(
                                    pi => pi.WarehouseId == transaction.WarehouseReceiptId &&
                                          pi.ProductId == detail.ProductId &&
                                          pi.LotNumber.Equals(lotNumber));
                            if (productInvImport == null) {
                                productInvImport = new ProductInventory {
                                    WarehouseId = transaction.WarehouseReceiptId.Value,
                                    ProductId = detail.ProductId,
                                    ImportDate = transaction.CreatedDate,
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    TotalQty = 0,
                                    LotNumber = lotNumber,
                                    MachineId = detail.MachineId,
                                    DefectId = detail.DefectId
                                };
                                vfi.ProductInventories.Add(productInvImport);
                                vfi.SaveChanges();
                            }
                            productInvImport.ExportDate = null;
                            var periodImport = new ProductInventoryPeriod() {
                                PeriodDate = transaction.CreatedDate,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                ProductId = productInvImport.ProductId,
                                ProductInvId = productInvImport.ProductInventoryId,
                                WarehouseId = productInvImport.WarehouseId,
                                Quantity = detail.QuantityDefect,
                                TransactionId = transaction.TransactionId,
                                EarlyPeriodQuantity = productInvImport.TotalQty,
                                LastPeriodQuantity = productInvImport.TotalQty + detail.QuantityDefect,
                                UnitMeasure = "Pcs",
                                UnitPrice = detail.Product.UnitPrice,
                                LotNumber = lotNumber
                            };
                            productInvImport.TotalQty = periodImport.LastPeriodQuantity;
                            periods.Add(periodImport);
                            if (!string.IsNullOrWhiteSpace(detail.StoreCode)) {
                                productInvImport.StoreCode = detail.StoreCode;
                            }
                        }

                        transaction.Status = (byte)MyUtilities.Transaction.Status.Approved;
                        //vfi.ProductInventories.AddRange(newInventories);
                        vfi.ProductInventoryPeriods.AddRange(periods);
                        vfi.SaveChanges();
                    }
                    #endregion

                    #region after calculate
                    #region export finish
                    if (isExportTP) {
                        // tao invoice
                        invoice = new Vfi.Models.Invoice();
                        invoice.InvoiceNumber =
                            MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Invoice, 1);
                        invoice.CustomerId = exportTP.CustomerId.Value;
                        invoice.ExportId = exportTP.ExportId;
                        invoice.Active = true;
                        invoice.ShipmentDate = exportTP.DateTransporter;
                        invoice.ModifiedDate = DateTime.Now;
                        invoice.ModifiedUser = HttpContext.User.Identity.Name;
                        invoice.ExchangeRate = 1;
                        invoice.TaxPercent = 0;
                        invoice.Status = (byte)MyUtilities.Sales.Status.Waiting;
                        vfi.Invoices.Add(invoice);
                        vfi.SaveChanges();
                    }
                    #endregion
                    #region send back finish
                    else if (isChangeQuantityBack) {
                        //var transactionReturn = new Vfi.Models.Transaction
                        //{
                        //    WarehouseIssueId = MyUtilities.Warehouse.Business,
                        //    WarehouseReceiptId = MyUtilities.Warehouse.Return,
                        //    TransactionCode = transaction.TransactionCode,
                        //    EoI = "2",
                        //    MoP = false,
                        //    CreatedUser = HttpContext.User.Identity.Name,
                        //    CreatedDate = transaction.CreatedDate,
                        //    Status = (byte) Transaction/StatusEnumModel.Open,
                        //    Active = true,
                        //    ModifiedUser = HttpContext.User.Identity.Name,
                        //    ModifiedDate = DateTime.Now
                        //};
                        //transactionReturn.TransactionDetails.AddRange(transaction.TransactionDetails);
                        //vfi.Transactions.Add(transactionReturn);
                        //vfi.SaveChanges();
                        //approve = ApproveTransactionProduct(transactionReturn.TransactionId, false);
                        //if (!approve)
                        //{
                        //    return
                        //        Json(
                        //            @"Không thể cập nhật trạng thái lệnh. Xin vui lòng thử lại. (transactionReturn).");
                        //}
                    }
                    else if (isSendQuantityBack) {
                        var exportDetails = vfi.ExportFormTP_KDDetail.Where(ed => ed.ExportId == exportTP.ExportId);
                        var isComplete = true;
                        foreach (var exportDetail in exportDetails) {
                            if (exportDetail.InvoiceDetails.Where(id => id.Active).Sum(id => id.Piece) != 0
                                && exportDetail.IsInvoiced == false) {
                                isComplete = false;
                            }
                            else {
                                exportDetail.IsInvoiced = true;
                            }
                        }
                        if (isComplete) {
                            invoice.Status = (byte)MyUtilities.Sales.Status.Completed;
                        }
                        vfi.SaveChanges();
                    }
                    else if (isChangeQuantityTo) {
                        //var transactionReturn = new Vfi.Models.Transaction
                        //{
                        //    WarehouseIssueId = MyUtilities.Warehouse.Return,
                        //    WarehouseReceiptId = MyUtilities.Warehouse.Business,
                        //    TransactionCode = transaction.TransactionCode,
                        //    EoI = "2",
                        //    MoP = false,
                        //    CreatedUser = HttpContext.User.Identity.Name,
                        //    CreatedDate = transaction.CreatedDate,
                        //    Status = (byte) Transaction/StatusEnumModel.Open,
                        //    Active = true,
                        //    ModifiedUser = HttpContext.User.Identity.Name,
                        //    ModifiedDate = DateTime.Now
                        //};
                        //foreach (var detail in transaction.TransactionDetails)
                        //{
                        //    var newDetail = new Vfi.Models.TransactionDetail
                        //    {
                        //        Transaction = transactionReturn,
                        //        TransactionId = transactionReturn.TransactionId,
                        //        ReferenceId = detail.ReferenceId,
                        //        MoP = false,
                        //        Quantity = detail.Quantity,
                        //        Active = true,
                        //        ModifiedUser = HttpContext.User.Identity.Name,
                        //        ModifiedDate = DateTime.Now,
                        //        QuantityKg = detail.QuantityKg,
                        //        // Note = detail.DetailId + "",
                        //    };
                        //    transactionReturn.TransactionDetails.Add(newDetail);
                        //}
                        //vfi.Transactions.Add(transactionReturn);
                        //vfi.SaveChanges();
                        //approve = ApproveTransactionProduct(transactionReturn.TransactionId, false);
                        //if (!approve)
                        //{
                        //    return
                        //        Json(
                        //            @"Không thể cập nhật trạng thái lệnh. Xin vui lòng thử lại. (transactionReturn).");
                        //}
                    }
                    #endregion
                    #region purchase import
                    else if (isImportPurchase) {
                        if (!purchaseOrder.PurchaseOrderDetails.Any(pod => !(pod.IsComplete ?? false))) {
                            purchaseOrder.Status = (byte)MyUtilities.Sales.Status.Completed;
                            vfi.SaveChanges();
                        }
                    }
                    #endregion
                    #region production2
                    else if (isExportProduction2) {
                        //var production2Periods = new List<Production2InventoryPeriod>();
                        //var transaction2 = new Production2Transaction {
                        //    TransactionCode =
                        //        MyUtilities.AutoIncrease.GetParam(
                        //            (int)MyUtilities.AutoIncrease.IncreaseNum.Production2, 1),
                        //    EoI = Convert.ToChar(MyUtilities.Transaction.EoIEnum.Import).ToString(),
                        //    CreateDate = transaction.CreatedDate,
                        //    Status = (byte)MyUtilities.Transaction.Status.Approved,
                        //    ModifiedUser = HttpContext.User.Identity.Name,
                        //    ModifiedDate = DateTime.Now,
                        //    Production2TransactionDetail = new List<Production2TransactionDetail>(),
                        //};
                        foreach (var detail in transaction.TransactionDetails) {
                            var product =
                                vfi.Products.FirstOrDefault(p => p.ProductId == detail.ReferenceId);
                            if (product == null)
                                throw new AggregateException("Lỗi! Không tìm thấy SP");
                            if (product.Production2Weight == 0)
                                throw new AggregateException("Lỗi! Vui lòng cập nhật trọng lượng SX 2 - " +
                                                             product.ProductCode);
                            if (!product.ProductionSections.Any(ps => ps.Active))
                                throw new AggregateException("Lỗi! Liên hệ kỹ thuật cập nhật công đoạn SX 2 - " +
                                                             product.ProductCode);
                            var firstSection =
                                product.ProductionSections.Where(ps => ps.Active)
                                    .OrderBy(ps => ps.SectionIndex)
                                    .FirstOrDefault();

                            //    var transactionDetail = new Production2TransactionDetail {
                            //        Production2Transaction = transaction2,
                            //        TransactionId = transaction2.TransactionId,
                            //        ProductId = detail.ReferenceId.Value,
                            //        SectionIssueId = null,
                            //        SectionReceiptId = firstSection.ProductionSectionId,
                            //        UnitMeasure = "Kg",
                            //        QuantityKg =
                            //            Math.Round((product.ProductionWeight ?? 0) * (detail.Quantity) / 1000, 3),
                            //        Quantity = detail.Quantity,
                            //        Note = detail.Note + "",
                            //        QuantityDefect = 0,
                            //        QuantityLost = 0,
                            //        Time = 0,
                            //        OverTime = 0,
                            //        SectionIndex = firstSection.SectionIndex,
                            //    };
                            //    transaction2.Production2TransactionDetail.Add(transactionDetail);
                            //    var production2Inv = vfi.Production2Inventory.FirstOrDefault(
                            //        pi =>
                            //            pi.ProductionSectionId == firstSection.ProductionSectionId);
                            //    if (production2Inv == null) {
                            //        production2Inv = new Production2Inventory {
                            //            ProductionSectionId = firstSection.ProductionSectionId,
                            //            TotalQuantity = 0,
                            //            Weight = product.ProductionWeight ?? 0,
                            //        };
                            //        vfi.Production2Inventory.Add(production2Inv);
                            //    }
                            //    var period = new Production2InventoryPeriod {
                            //        EarlyQuantity = production2Inv.TotalQuantity,
                            //        Quantity = transactionDetail.QuantityKg,
                            //        LastQuantity = production2Inv.TotalQuantity + transactionDetail.QuantityKg,
                            //        PeriodDate = transaction2.CreateDate,
                            //        TransactionId = transaction2.TransactionId,
                            //        Production2Inventory = production2Inv,
                            //        Weight = production2Inv.Weight,
                            //    };
                            //    production2Inv.TotalQuantity = period.LastQuantity;
                            //    production2Periods.Add(period);
                        }
                        //if (transaction2.Production2TransactionDetail.Any()) {
                        //    vfi.Production2Transaction.Add(transaction2);
                        //    vfi.Production2InventoryPeriod.AddRange(production2Periods);
                        //    vfi.SaveChanges();
                        //}
                    }
                    #endregion
                    #region defect
                    else if (transaction.WarehouseReceiptId == MyUtilities.Warehouse.Defect) {
                        var importSx1 =
                            vfi.ImportFormSX1.FirstOrDefault(
                                i => i.TransactionCode.Equals(transaction.TransactionCode));

                        var importWorkpiece = new ImportWorkpieceMaterial {
                            ImportDate = transaction.CreatedDate,
                            TransactionId = transaction.TransactionId,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                        };
                        if (importSx1 != null) {
                            if (importSx1.ImportWorkpieceMaterials.Any()) {
                                importWorkpiece = importSx1.ImportWorkpieceMaterials.FirstOrDefault();
                            }
                            else {
                                importWorkpiece.ImportSx1Id = importSx1.ImportId;
                                vfi.ImportWorkpieceMaterials.Add(importWorkpiece);
                            }
                        }
                        var list = new List<WorkpieceMaterialPeriod>();
                        foreach (var identity in MaterialIdentityCode.GetMaterialIdentityCodes(0)) {
                            var totalQuantity =
                                vfi.WorkpieceMaterialPeriods.Where(
                                        p => p.IdentityCode.Equals(identity.IdentityCode) && p.Type == 3)
                                    .ToList()
                                    .Sum(p => p.LastQuantity - p.EarlyQuantity);
                            var period = new WorkpieceMaterialPeriod {
                                IdentityCode = identity.IdentityCode,
                                Type = 3,
                                EarlyQuantity = totalQuantity,
                                EoIId = importWorkpiece.ImportId,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                PeriodDate = importWorkpiece.ImportDate.Value,
                                IsDestroy = false,
                                EoI = (int)MyUtilities.Transaction.EoIEnum.Import,
                            };
                            if (importSx1 != null) {
                                var importDetails =
                                    importSx1.ImportFormSX1Detail.Where(
                                        id =>
                                            id.DefectProduct1 + id.DefectProduct2 > 0 &&
                                            id.MaterialInventory.Material.MaterialType.IdentityCode.Equals(
                                                identity.IdentityCode));
                                if (importDetails.Any()) {
                                    period.Weight =
                                        Math.Round(importDetails.Sum(
                                            id => (id.DefectProduct1 + id.DefectProduct2) * id.ProductWeight / 1000), 2);
                                }
                                period.LastQuantity = Math.Round(period.EarlyQuantity + period.Weight, 2);
                                list.Add(period);
                            }
                            else {
                                var transactionDetails =
                                    transaction.TransactionDetails.Where(
                                            td =>
                                                td.Product.ProductionMaterials.Any() &&
                                                td.Product.ProductionMaterials.FirstOrDefault()
                                                    .Material.MaterialType.IdentityCode.Equals(identity.IdentityCode))
                                        .ToList();
                                if (transactionDetails.Any()) {
                                    period.Weight =
                                        Math.Round(
                                            transactionDetails.Sum(td => td.Quantity * td.Product.QcWeight / 1000).Value,
                                            2);
                                    period.LastQuantity = Math.Round(period.EarlyQuantity + period.Weight, 2);
                                    importWorkpiece.WorkpieceMaterialPeriods.Add(period);
                                }
                            }
                        }
                        if (importSx1 != null)
                            vfi.WorkpieceMaterialPeriods.AddRange(list);
                        else
                            vfi.ImportWorkpieceMaterials.Add(importWorkpiece);
                        vfi.SaveChanges();
                    }
                    #endregion
                    #region processing
                    else if (transaction.WarehouseReceiptId == MyUtilities.Warehouse.Processing) {
                        var defectTransaction = vfi.DefectTransactions.FirstOrDefault(x => x.ImportTransactionId == transaction.TransactionId);
                        if (defectTransaction == null) {
                        }
                        else {
                            var warehouseIds = defectTransaction.DefectTransactionDetails.Where(x => x.NextWarehouseProcessId != null)
                                                                                        .Select(x => x.NextWarehouseProcessId).Distinct().ToList();
                            foreach (var warehouseId in warehouseIds) {
                                var defectDetails = defectTransaction.DefectTransactionDetails.Where(x => x.NextWarehouseProcessId == warehouseId);
                                if (!defectDetails.Any()) continue;
                                var newTransaction = new Transaction {
                                    WarehouseIssueId = transaction.WarehouseReceiptId.Value,
                                    WarehouseReceiptId = warehouseId,
                                    TransactionCode =
                                        MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                                    EoI = Convert.ToChar(MyUtilities.Transaction.EoIEnum.Rotate) + "",
                                    MoP = MyUtilities.Transaction.MoP.Product,
                                    CreatedUser = defectTransaction.ModifiedUser,
                                    CreatedDate = transaction.CreatedDate,
                                    Status = (byte)MyUtilities.Transaction.Status.Open,
                                    Active = true,
                                    ModifiedUser = defectTransaction.ModifiedUser,
                                    ModifiedDate = DateTime.Now,
                                };
                                foreach (var defectDetail in defectDetails) {
                                    var lotNumber = defectDetail.ProductionDefect.DefectName;
                                    //if (!string.IsNullOrWhiteSpace(defectDetail.DefectExpand)) {
                                    //    lotNumber += "-" + defectDetail.DefectExpand;
                                    //}
                                    var detail = new TransactionDetail {
                                        ReferenceId = defectDetail.ProductId,
                                        MoP = MyUtilities.Transaction.MoP.Product,
                                        Quantity = defectDetail.QuantityDefect,
                                        UnitMeasure = "Pcs",
                                        Active = true,
                                        ModifiedUser = newTransaction.ModifiedUser,
                                        ModifiedDate = newTransaction.ModifiedDate,
                                        Note = defectDetail.Note,
                                        TransactionId = newTransaction.TransactionId,
                                        LotNumber = lotNumber,
                                        DefectId = defectDetail.DefectId
                                    };

                                    var productInv =
                                        vfi.ProductInventories.FirstOrDefault(
                                            pi => pi.WarehouseId == transaction.WarehouseReceiptId.Value &&
                                                  pi.ProductId == detail.ReferenceId &&
                                                  pi.LotNumber.Equals(detail.LotNumber));
                                    if (productInv == null) {
                                        productInv = new ProductInventory {
                                            WarehouseId = transaction.WarehouseReceiptId.Value,
                                            ProductId = detail.ReferenceId.Value,
                                            ImportDate = newTransaction.CreatedDate,
                                            ModifiedDate = DateTime.Now,
                                            ModifiedUser = HttpContext.User.Identity.Name,
                                            TotalQty = 0,
                                            LotNumber = lotNumber,
                                            ErrorId = detail.ErrorId,
                                            DefectId = defectDetail.DefectId
                                        };
                                        vfi.ProductInventories.Add(productInv);
                                        vfi.SaveChanges();
                                    }
                                    detail.ProductInvId = productInv.ProductInventoryId;
                                    newTransaction.TransactionDetails.Add(detail);
                                }
                                vfi.Transactions.Add(newTransaction);
                            }
                            vfi.SaveChanges();
                        }
                    }
                    #endregion
                    #endregion
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("ApproveProductTransaction", ex.Message);
            }

            return View(new GridModel(GetWaitingProductTransactions()));
        }

        public int UpdateProductInvByTransaction(long transactionId, string userName) {
            if (transactionId == 0) return 0;
            var saved = 0;
            using (var vfi = new tammaContext()) {
                var transaction = vfi.Transactions.FirstOrDefault(x => x.TransactionId == transactionId);
                var periods = new List<ProductInventoryPeriod>();
                foreach (var transactionDetail in transaction.TransactionDetails) {
                    var productInvExport =
                        vfi.ProductInventories.FirstOrDefault(
                            pi => pi.ProductInventoryId == transactionDetail.ProductInvId);
                    if (transaction.WarehouseIssueId != null) {
                        if (productInvExport == null) {
                            throw new AggregateException("Lỗi! Không tìm thấy kho cần xuất");
                        }
                        if (Math.Round(productInvExport.TotalQty - transactionDetail.Quantity, 2) < 0) {
                            throw new AggregateException("Lỗi! Tồn kho không đủ xuất " +
                                                         productInvExport.Product.ProductCode);
                        }
                        var period = new ProductInventoryPeriod() {
                            PeriodDate = transaction.CreatedDate,
                            ModifiedUser = userName,
                            ModifiedDate = DateTime.Now,
                            ProductId = productInvExport.ProductId,
                            ProductInvId = productInvExport.ProductInventoryId,
                            WarehouseId = productInvExport.WarehouseId,
                            Quantity = transactionDetail.Quantity,
                            TransactionId = transaction.TransactionId,
                            EarlyPeriodQuantity = productInvExport.TotalQty,
                            LastPeriodQuantity = productInvExport.TotalQty - transactionDetail.Quantity,
                            UnitMeasure = "Pcs",
                            UnitPrice = productInvExport.Product.UnitPrice,
                            LotNumber = productInvExport.LotNumber
                        };
                        productInvExport.TotalQty = period.LastPeriodQuantity;
                        if (Math.Round(productInvExport.TotalQty, 2) == 0) {
                            productInvExport.TotalQty = 0;
                            productInvExport.ExportDate = DateTime.Now;
                        }
                        periods.Add(period);
                    }
                    if (transaction.WarehouseReceiptId != null) {
                        var lotNumber = (transactionDetail.LotNumber + "").Trim();
                        if (transaction.WarehouseReceiptId == MyUtilities.Warehouse.Processing) {
                            lotNumber = "";
                        }
                        var productInvImport =
                            vfi.ProductInventories.FirstOrDefault(
                                pi => pi.WarehouseId == transaction.WarehouseReceiptId &&
                                      pi.ProductId == transactionDetail.ReferenceId &&
                                      pi.LotNumber.Equals(lotNumber));
                        if (productInvImport == null) {
                            productInvImport = new ProductInventory {
                                WarehouseId = transaction.WarehouseReceiptId.Value,
                                ProductId = transactionDetail.ReferenceId.Value,
                                ImportDate = transaction.CreatedDate,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = userName,
                                TotalQty = 0,
                                LotNumber = lotNumber,
                                ErrorId = transactionDetail.ErrorId,
                                DefectId = transactionDetail.DefectId,
                                ByProcessMachineId = transactionDetail.NextProcessId,
                                MachineId = transactionDetail.MachineId,
                                StoreCode = transactionDetail.StoreCode,
                                Active = true,
                            };
                            if (productInvExport != null) {
                                productInvImport.MaterialInvId = productInvExport.MaterialInvId;
                                productInvImport.MachineId = productInvExport.MachineId;
                            }
                            vfi.ProductInventories.Add(productInvImport);
                            vfi.SaveChanges();
                        }
                        productInvImport.ExportDate = null;
                        var period = new ProductInventoryPeriod() {
                            PeriodDate = transaction.CreatedDate,
                            ModifiedUser = userName,
                            ModifiedDate = DateTime.Now,
                            ProductId = productInvImport.ProductId,
                            ProductInvId = productInvImport.ProductInventoryId,
                            WarehouseId = productInvImport.WarehouseId,
                            Quantity = transactionDetail.Quantity,
                            TransactionId = transaction.TransactionId,
                            EarlyPeriodQuantity = productInvImport.TotalQty,
                            LastPeriodQuantity = productInvImport.TotalQty + transactionDetail.Quantity,
                            UnitMeasure = "Pcs",
                            UnitPrice = transactionDetail.Product.UnitPrice,
                            LotNumber = lotNumber
                        };
                        productInvImport.TotalQty = period.LastPeriodQuantity;
                        periods.Add(period);
                    }
                }
                transaction.Status = (byte)MyUtilities.Transaction.Status.Approved;
                //vfi.ProductInventories.AddRange(newInventories);
                vfi.ProductInventoryPeriods.AddRange(periods);
                vfi.SaveChanges();
            }
            return saved;
        }

        [HttpPost]
        public ActionResult Save(IEnumerable<HttpPostedFileBase> attachments) {
            // The Name of the Upload component is "attachments"       
            try {
                if (attachments.Any()) {
                    foreach (var file in attachments) {
                        // Some browsers send file names with full path. We only care about the file name.
                        var fileName = Path.GetFileName(file.FileName);
                        if (fileName == null) continue;
                        var destinationPath = Path.Combine(Server.MapPath("~/Content/FileUpload"), fileName);

                        byte[] fileBytes = new byte[file.ContentLength];
                        file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                        System.IO.File.WriteAllBytes(destinationPath, fileBytes);
                    }
                    return Json("Oke");
                }
                else {
                    throw new AggregateException("attachments null");
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UploadSave", ex.Message);
                return Json(ex.Message);
            }
            // Redirect to a view showing the result of the form submission.    
            return Json("False");

        }
        private static int oke;
        private static int error;
        private static int duplicate;
        private static string errorIndex;
        [GridAction]
        public ActionResult SelectTransactionPacking(string fileName) {

            var model = new List<ProductInventoryRotateModel>();
            if (string.IsNullOrWhiteSpace(fileName))
                return View(new GridModel(model));
            var dt = new DataTable();
            try {
                using (var conn = new OleDbConnection()) {
                    var destinationPath = Path.Combine(Server.MapPath("~/Content/FileUpload"), fileName);
                    string fileExtension = Path.GetExtension(destinationPath);
                    if (fileExtension == ".xls")
                        conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + destinationPath + ";" +
                                                "Extended Properties='Excel 8.0;HDR=YES;'";
                    if (fileExtension == ".xlsx")
                        conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + destinationPath + ";" +
                                                "Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                    using (var comm = new OleDbCommand()) {
                        //var sheetName = "Sheet1";
                        comm.CommandText = "Select * from [" + "Sheet1" + "$]";
                        comm.Connection = conn;
                        using (var da = new OleDbDataAdapter()) {
                            da.SelectCommand = comm;
                            da.Fill(dt);
                        }
                    }
                }
                using (var vfi = new tammaContext()) {
                    errorIndex = "";
                    oke = error = duplicate = 0;
                    dt.Rows.RemoveAt(0);

                    Session["ListTransactionPackings"] = new List<ProductInventoryRotateModel>();
                    foreach (DataRow row in dt.Rows) {
                        var productCode = row[2].ToString().Trim();
                        if (string.IsNullOrWhiteSpace(productCode)) continue;
                        var product = vfi.Products.FirstOrDefault(p => p.ProductCode.Equals(productCode));
                        if (product == null) {
                            error++;
                            errorIndex += "Error: Không tìm thấy: " + productCode + "\n";
                            continue;
                        }
                        //var entity = model.FirstOrDefault(m => m.ProductId == product.ProductId);
                        //if (entity != null)
                        //{
                        //    duplicate++;
                        //    errorIndex += "Error: Bị trùng mã: " + product.ProductCode + "\n";
                        //    continue;
                        //}
                        var productInventorys =
                            vfi.ProductInventories.Where(
                                pi =>
                                    pi.WarehouseId == MyUtilities.Warehouse.Packing &&
                                    pi.ProductId == product.ProductId);
                        var entity = new ProductInventoryRotateModel {
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            Quantity = 0,
                            QuantityKg = 0,
                            LotNumber = "",

                        };

                        entity.LotNumber = row[4] + "";
                        if (!string.IsNullOrWhiteSpace(row[5].ToString()))
                            try {
                                entity.QuantityKg = Convert.ToDouble(row[5].ToString());
                            }
                            catch (FormatException) {
                            }
                        if (!string.IsNullOrWhiteSpace(row[6].ToString()))
                            try {
                                entity.QuantityKg = Convert.ToDouble(row[6].ToString());
                            }
                            catch (FormatException) {
                            }
                        if (!string.IsNullOrWhiteSpace(row[7].ToString()))
                            try {
                                entity.ModifiedDate = Convert.ToDateTime(row[7].ToString());
                            }
                            catch (FormatException) {
                            }

                        oke++;
                        model.Add(entity);
                    }
                }
            }
            catch (OleDbException oledbEx) {
                ModelState.AddModelError("OleDbException", oledbEx.Message);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectForecastOrder", ex.Message);
            }
            Session["SessionForecastOrder"] = model;
            return View(new GridModel(model));
        }
        //cho upload vao action nao

        public ActionResult GetExcelParseStatus() {
            var data = new object[] { oke + error + duplicate, oke, error, duplicate, errorIndex };
            return Json(data);
        }


        List<TransactionModel> GetWaitingProductTransactions() {
            if (!Request.IsAuthenticated)
                throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
            var model = new List<TransactionModel>();
            var purchasing = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                MyUtilities.UserRole.PurchasingManagement);
            using (var vfi = new tammaContext()) {
                var isInvManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.InvManagementLv2);
                var warehouseIds = new List<int>(); 
                var canApproveDefect = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.ApproveDefect);
                if (isInvManager) {
                    warehouseIds = vfi.Warehouses.Where(x => x.Active).Select(x => x.WarehouseId).ToList();
                }
                else {
                    var userId = vfi.Users.FirstOrDefault(u => u.Username.Equals(HttpContext.User.Identity.Name)).UserId;
                    warehouseIds =
                        vfi.WarehousePermissions.Where(wp => wp.UserId == userId && wp.Import == true)
                            .Select(wp => wp.WarehouseId.Value)
                            .ToList();
                    if (canApproveDefect) {
                        warehouseIds.Add(MyUtilities.Warehouse.Defect);
                    }
                    else {
                        warehouseIds.Remove(MyUtilities.Warehouse.Defect);
                    }
                }

                var canApproveInternal = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.ApproveInternalProduct);
                var transactions = (from t in vfi.Transactions
                                    where t.MoP == false &&
                                   (t.Status == (byte)MyUtilities.Transaction.Status.Open ||
                                    t.Status == (byte)MyUtilities.Transaction.Status.Processing) &&
                                   (t.WarehouseReceiptId != null && warehouseIds.Contains(t.WarehouseReceiptId.Value)
                                   || (t.IsInternal == true && canApproveInternal))
                                    orderby t.CreatedDate, t.ModifiedDate
                                    select new TransactionModel {
                                        TransactionId = t.TransactionId,
                                        TransactionCode = t.TransactionCode,
                                        WarehouseIssueId = t.WarehouseIssueId,
                                        WarehouseIssueName = t.WarehouseIssueId != null
                                            ? t.Warehouse.WarehouseName
                                            : "",
                                        WarehouseReceiptId = t.WarehouseReceiptId,
                                        WarehouseReceiptName = t.WarehouseReceiptId != null
                                            ? t.Warehouse1.WarehouseName
                                            : "",
                                        EoI = t.EoI,
                                        MoP = t.MoP,
                                        Status = t.Status,
                                        Description = t.Description,
                                        CreatedUser = t.CreatedUser,
                                        CreatedDate = t.CreatedDate,
                                        ModifiedUser = t.ModifiedUser,
                                        ModifiedDate = t.ModifiedDate,
                                        AlertColor = 0,
                                        IsInternal = t.IsInternal ?? false,
                                        CanApprove = false,
                                        Highlight = t.WarehouseReceiptId != null
                                            ? t.Warehouse1.IsOutOfProcess
                                            : false,
                                        //t.TransactionDetails,
                                        TotalQuality = t.TransactionDetails.Any()
                                            ? t.TransactionDetails.Sum(y => y.Quantity)
                                            : t.TransactionProducts.Any()
                                                ? t.TransactionProducts.Sum(y => y.Quantity)
                                                : 0
                                    }).ToList();
                foreach (var entity in transactions) {
                    //if (transaction.WarehouseReceiptId == (byte)MyUtilities.Warehouse.Defect) {
                    //    if (!canApproveDefect)
                    //        continue;
                    //}
                    //else if (transaction.WarehouseReceiptId == null) {
                    //    if (!canApproveInternal) continue;
                    //}
                    //else if (!userWarehousePermission.Contains(transaction.WarehouseReceiptId.Value))
                    //    continue;

                    //var entity = new TransactionModel {
                    //    TransactionId = transaction.TransactionId,
                    //    TransactionCode = transaction.TransactionCode,
                    //    WarehouseIssueId = transaction.WarehouseIssueId,
                    //    WarehouseIssueName = transaction.WarehouseIssueId != null
                    //        ? transaction.Warehouse.WarehouseName
                    //        : "",
                    //    WarehouseReceiptId = transaction.WarehouseReceiptId,
                    //    WarehouseReceiptName = transaction.WarehouseReceiptId != null
                    //        ? transaction.Warehouse1.WarehouseName
                    //        : "",
                    //    EoI = transaction.EoI,
                    //    EoIName = MyUtilities.Transaction.CastText.GetTextEoI(transaction.EoI),
                    //    MoP = transaction.MoP,
                    //    Status = transaction.Status,
                    //    StatusName = MyUtilities.Transaction.CastText.GetTextStatus(transaction.Status),
                    //    Description = transaction.Description,
                    //    CreatedUser = transaction.CreatedUser,
                    //    CreatedDate = transaction.CreatedDate,
                    //    ModifiedUser = transaction.ModifiedUser,
                    //    ModifiedDate = transaction.ModifiedDate,
                    //    //TotalQuality = transactionDetails.Sum(t => t.Quantity),
                    //    AlertColor = 0,
                    //    IsInternal = transaction.IsInternal ?? false,
                    //    CanApprove = false
                    //};
                    entity.EoIName = MyUtilities.Transaction.CastText.GetTextEoI(entity.EoI);
                    entity.StatusName = MyUtilities.Transaction.CastText.GetTextStatus(entity.Status);

                    if (entity.Status != (byte)MyUtilities.Transaction.Status.Open) {
                    }
                    else if (entity.IsInternal) {
                        entity.SpecialNote += " (nội bộ)";
                        if (canApproveInternal) {
                            entity.CanApprove = true;
                        }
                        entity.Highlight = true;
                    }
                    else if (entity.WarehouseReceiptId == MyUtilities.Warehouse.Processing) {
                        // phan loi cxl
                        var transactionDefect = vfi.DefectTransactions.FirstOrDefault(x => x.ImportTransactionId == entity.TransactionId);
                        if (transactionDefect == null) {
                            entity.SpecialNote = "Chưa phân lỗi | " + entity.SpecialNote;
                        }
                        else if (transactionDefect.Status != (byte)MyUtilities.Transaction.Status.Approved) {
                            entity.SpecialNote = "Chưa hoàn thành phân lỗi | " + entity.SpecialNote;
                        }
                        else {
                            entity.CanApprove = true;
                        }
                    }
                        else if (entity.WarehouseReceiptId == MyUtilities.Warehouse.Defect && canApproveDefect) {
                        entity.CanApprove = true;
                    }
                    //else if (isInvManager) {
                    //    entity.CanApprove = true;
                    //}
                    else {
                        entity.CanApprove = true;
                    }

                    // chu ky dien tu
                    if (entity.WarehouseIssueId == MyUtilities.Warehouse.Plating &&
                        entity.WarehouseReceiptId == MyUtilities.Warehouse.QcB) {
                        entity.PurchasingSignatureType = 0;
                        var plating =
                            vfi.ImportNCU_QCB.FirstOrDefault(
                                e => e.TransactionId == entity.TransactionId && e.PurchasingSignature == 1);
                        if (plating != null) {
                            entity.PurchasingSignatureType = 1;
                        }
                        else if (purchasing) {
                            entity.PurchasingSignatureType = 2;
                        }
                    }
                    else
                        entity.PurchasingSignatureType = -1;

                    // note va in form dac biet
                    if (entity.WarehouseIssueId == MyUtilities.Warehouse.Production1) {
                        var form = vfi.ImportFormSX1.FirstOrDefault(i => i.TransactionCode.Equals(entity.TransactionCode));
                        if (form != null) {
                            entity.SpecialNote = "Ngày SX:" + form.MaterialUseDate.ToString("dd/MM/yyyy");
                            //entity.SpecialFormType = MyUtilities.Transaction.FormTypeEnum.ImportSX1;
                        }
                    }
                    else if (entity.WarehouseIssueId == MyUtilities.Warehouse.Cnc) {
                        var form = vfi.ImportFormCncs.FirstOrDefault(i => i.TransactionCode.Equals(entity.TransactionCode));
                        if (form != null) {
                            entity.SpecialNote = "Ngày SX:" + form.MaterialUseDate.ToString("dd/MM/yyyy");
                            //entity.SpecialFormType = MyUtilities.Transaction.FormTypeEnum.ImportCNC;
                        }
                    }
                    else if (entity.WarehouseIssueId == MyUtilities.Warehouse.WaitingPlating) {
                        var form = vfi.ExportGCN_NCU.FirstOrDefault(i => i.TransactionId == entity.TransactionId);
                        if (form != null) {
                            var plating = form.PlatingForm;
                            var vendor = plating.Vendor;
                            if (plating.PlatingType == MyUtilities.Warehouse.Plating)
                                entity.SpecialNote = "Xi mạ - ";
                            else if (plating.PlatingType == MyUtilities.Warehouse.PlatingTest)
                                entity.SpecialNote = "GCN - ";
                            entity.SpecialNote += "NCC:" + vendor.VendorName;
                            entity.SpecialFormType = (int)MyUtilities.Transaction.FormTypeEnum.ExportGCN_NCU;
                        }
                    }
                    else if (entity.WarehouseReceiptId == MyUtilities.Warehouse.QcB) {
                        var form = vfi.ImportNCU_QCB.FirstOrDefault(i => i.TransactionId == entity.TransactionId);
                        if (form != null) {
                            var plating = form.PlatingForm;
                            var vendor = plating.Vendor;
                            if (plating.PlatingType == MyUtilities.Warehouse.Plating)
                                entity.SpecialNote = "Xi mạ - ";
                            else if (plating.PlatingType == MyUtilities.Warehouse.PlatingTest)
                                entity.SpecialNote = "GCN - ";
                            entity.SpecialNote += "NCC:" + vendor.VendorName;
                            entity.SpecialFormType = (int)MyUtilities.Transaction.FormTypeEnum.ImportNCU_QCB;
                        }
                    }
                    else if (entity.WarehouseReceiptId == MyUtilities.Warehouse.Business) {
                        var form = vfi.ExportFormTP_KD.FirstOrDefault(i => i.TransactionCode.Equals(entity.TransactionCode));
                        if (form != null) {
                            entity.SpecialNote = "Khách hàng:" + form.Customer.CustomerCode;
                            entity.SpecialNote += " - Tổng thùng:" + form.TotalBox;
                            entity.SpecialFormType = (int)MyUtilities.Transaction.FormTypeEnum.ExportTP;
                        }
                        entity.Highlight = true;
                    }

                    if (entity.CreatedDate > DateTime.Today.AddDays(4) ||
                        entity.CreatedDate < DateTime.Today.AddDays(-4))
                        entity.AlertColor = 2;
                    else if (entity.CreatedDate > DateTime.Today.AddDays(1) ||
                             entity.CreatedDate < DateTime.Today.AddDays(-1))
                        entity.AlertColor = 1;
                    //var transactionDetails = transaction.TransactionDetails;
                    //if (transactionDetails.Any())
                    //    entity.TotalQuality = transactionDetails.Sum(t => t.Quantity);
                    //else {
                    //    var transactionProducts = transaction.TransactionProducts;
                    //    if (transactionProducts.Any())
                    //        entity.TotalQuality = transactionProducts.Sum(t => t.Quantity);
                    //}
                    model.Add(entity);
                }
            }
            return model;
        }

        [GridAction]
        public ActionResult SelectWaitingMaterialTransactions() {

            if (!Request.IsAuthenticated)
                return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

            var model = new List<TransactionModel>();
            try {
                model = GetTransactionOpenMaterialList();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectWaitingMaterialTransactions", ex.Message);
            }
            return View(new GridModel(model.OrderByDescending(f => f.ModifiedDate)));
        }

        public ActionResult ImportNcuSignature(long transactionId) {
            using (var vfi = new tammaContext()) {
                var plating = vfi.ImportNCU_QCB.FirstOrDefault(i => i.TransactionId == transactionId);
                if (plating != null)
                    plating.PurchasingSignature = 1;
                else
                    return Json(9);
                vfi.SaveChanges();
            }
            return null;
        }

        public ActionResult ImportMaterialSignature(long transactionId) {
            using (var vfi = new tammaContext()) {
                var import = vfi.ImportPurchaseOrders.FirstOrDefault(i => i.TransactionId == transactionId);
                if (import != null)
                    import.PurchasingSignature = 1;
                else return Json(9);
                vfi.SaveChanges();
            }
            return null;
        }

        public ActionResult SelectComboBoxTransactionStatus() {
            var val = from MyUtilities.Transaction.Status stt in Enum.GetValues(typeof(MyUtilities.Transaction.Status))
                      select new {
                          Value = (int)Enum.Parse(typeof(MyUtilities.Transaction.Status), stt.ToString()),
                          Text =
                      MyUtilities.Transaction.CastText.GetTextStatus(
                          (int)Enum.Parse(typeof(MyUtilities.Transaction.Status), stt.ToString()))
                      };

            return new JsonResult {
                Data = new SelectList(val, "Value", "Text")
            };
        }

        #endregion

        #region TransactionDetail

        [GridAction]
        public ActionResult SelectTransactionDetailByTransactionId(long transactionId) {
            if (transactionId == 0)
                return View(new GridModel(new List<TransactionDetailModel>()));
            var models = new List<TransactionDetailModel>();
            using (var vfi = new tammaContext()) {
                var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
                // NL
                if (transaction.MoP)
                    models = GetTransactionMaterialDetailByTransactionId(transactionId);
                // SP
                else
                    models = GetTransactionProductDetailByTransactionId(transactionId);

            }
            return View(new GridModel(models));
        }

        List<TransactionDetailModel> GetTransactionMaterialDetailByTransactionId(long transactionId) {
            var model = new List<TransactionDetailModel>();
            var invManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.InvManagement);
            using (var vfi = new tammaContext()) {
                //nhap
                var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
                if (transaction.EoI.Equals("0")) {
                    var transactionDetails = vfi.TransactionDetails.Where(td => td.TransactionId == transactionId);
                    var importPo = vfi.ImportPurchaseOrders.FirstOrDefault(i => i.TransactionId == transactionId);
                    if (importPo == null) throw new AggregateException("Lỗi phiếu nhập nguyên liệu");
                    foreach (var transactionDetail in transactionDetails) {
                        var importDetail = importPo.ImportPurchaseOrderDetails.FirstOrDefault(
                                                id => id.MaterialId == transactionDetail.ReferenceId
                                                && (transactionDetail.LotNumber == null 
                                                    || transactionDetail.LotNumber.Equals(id.LotNumber)));
                        if (transactionDetail.PoDetailId != null) {
                            importDetail =
                               importPo.ImportPurchaseOrderDetails.FirstOrDefault(
                                   id => id.PoDetailId == transactionDetail.PoDetailId);
                        }
                        var material =
                            vfi.Materials.FirstOrDefault(m => m.MaterialId == transactionDetail.ReferenceId);
                        var materialInv =
                            vfi.MaterialInventories.FirstOrDefault(
                                mi =>
                                mi.MaterialId == transactionDetail.ReferenceId &&
                                mi.LotNumber.Equals(importDetail.LotNumber) &&
                                mi.Length == importDetail.Length &&
                                mi.VendorId == importDetail.VendorId);
                        var entity = new TransactionDetailModel {
                            TransactionId = transactionId,
                            TransactionDetailId = transactionDetail.TransactionDetailId,
                            MaterialCode = material.MaterialCode,
                            MaterialName = material.MaterialName,
                            MaterialTypeName = material.MaterialType.MaterialTypeName,
                            VendorName = importDetail.Vendor.VendorName,
                            Quantity = importDetail.Quantity,
                            Note = transactionDetail == null ? "" : transactionDetail.Note,
                            LotNumber = importDetail.LotNumber,
                            AvailableQuantity = materialInv == null ? 0 : materialInv.TotalQty,
                            UnitWeight = importDetail.UnitWeight,
                            QuantityKg = importDetail.UnitWeight * importDetail.Quantity,
                            DesignCode = MyUtilities.Material.GetMaterialDesignNo(material),
                            Length = importDetail.Length,
                            IsManager = invManager,
                            PoDetailId = importDetail.PoDetailId ?? 0
                        };
                        if (importPo.PurchaseOrder != null) {
                            entity.QuantityKg = importDetail.QuantityKg;
                        }
                        model.Add(entity);
                    }
                }
                //xuat
                else if (transaction.EoI.Equals("1")) {
                    var exportMaterial =
                        vfi.ExportMaterials.FirstOrDefault(em => em.TransactionId == transaction.TransactionId);
                    var materialInvIds = exportMaterial.ExportMaterialDetails.Select(emd => emd.MaterialInvId).Distinct();
                    foreach (var materialInvId in materialInvIds) {
                        var materialInv =
                            vfi.MaterialInventories.FirstOrDefault(
                                mi => mi.MaterialInventoryId == materialInvId);

                        var material = materialInv.Material;
                        var exportDetails =
                            exportMaterial.ExportMaterialDetails.Where(emd => emd.MaterialInvId == materialInvId);
                        var entity = new TransactionDetailModel {
                            TransactionId = transactionId,
                            MaterialCode = materialInv.Material.MaterialCode,
                            MaterialName = materialInv.Material.MaterialName,
                            MaterialTypeName = materialInv.Material.MaterialType.MaterialTypeName,
                            Quantity = exportDetails.Sum(emd => emd.Quantity),
                            LotNumber = materialInv.LotNumber,
                            AvailableQuantity = materialInv.TotalQty,
                            UnitPrice = materialInv.UnitPrice,
                            UnitWeight = materialInv.UnitWeight,
                            Length = materialInv.Length,
                            IsManager = invManager
                        };
                        entity.QuantityKg = materialInv.UnitWeight * entity.Quantity;
                        entity.AvailableQuantityKg = entity.AvailableQuantity * entity.UnitWeight;
                        entity.DesignCode = MyUtilities.Material.GetMaterialDesignNo(material);
                        //entity.Note = "Máy: ";
                        foreach (var exportDetail in exportDetails) {
                            if (exportDetail.MachineId != null) {
                                entity.MachineName = exportDetail.Machine.MachineName;
                                entity.Note += exportDetail.Machine.MachineName + "(" + exportDetail.Quantity +
                                               "); ";
                            }
                        }
                        entity.VendorName = materialInv.Vendor.VendorName;
                        model.Add(entity);
                    }
                }
                //return View(new GridModel(models.OrderBy(m => m.MaterialTypeName).ThenBy(m => m.MaterialName)));
            }
            return model
                .OrderBy(m => m.MachineName)
                .ThenBy(m => m.MaterialTypeName)
                .ThenBy(m => m.MaterialName)
                .ThenBy(m => m.DesignCode)
                .ToList();
        }

        List<TransactionDetailModel> GetTransactionProductDetailByTransactionId(long transactionId) {
            var model = new List<TransactionDetailModel>();
            var invManagerLv2 = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                MyUtilities.UserRole.InvManagementLv2);
            using (var vfi = new tammaContext()) {
                var transaction = (from t in vfi.Transactions
                                   where t.TransactionId == transactionId
                                   select new {
                                       t.WarehouseIssueId,
                                       t.WarehouseReceiptId,
                                       t.TransactionCode,
                                       t.TransactionProducts,
                                       t.IsInternal,
                                   }).FirstOrDefault();
                var transactionDetails = (from x in vfi.TransactionDetails
                                         where x.TransactionId == transactionId
                                         select new {
                                             x.TransactionDetailId,
                                             ReferenceId = x.ReferenceId.Value,
                                             x.Product.ProductCode,
                                             x.Product.Customer.CustomerCode,
                                             x.Product.DrawingFinish,
                                             x.Note,
                                             x.Quantity,
                                             x.ProductInvId,
                                             x.LotNumber,
                                             x.IsInternal,
                                             UploadDate = x.Product.UploadDate ?? DateTime.Now,
                                             ProductionMaterial = x.Product.ProductionMaterials.FirstOrDefault(pm => pm.Active)
                                         }).ToList();
                //var productIds = transactionDetails.Select(td => td.ReferenceId).Distinct().ToList();
                //var productWeights = from pw in vfi.Products
                //                     where productIds.Contains(pw.ProductId)
                //                     select new {
                //                         pw.ProductId,
                //                         pw.ProductionWeight,
                //                         pw.CncWeight,
                //                         pw.Production2Weight,
                //                         pw.HeatTreatmentWeight,
                //                         pw.SurfaceTreatmentWeight,
                //                         pw.WaitingPlatingWeight,
                //                         pw.PlatingWeight,
                //                         pw.QcWeight,
                //                         //pw.DrawingFinish,
                //                     };
                var importSx1 = vfi.ImportFormSX1.FirstOrDefault(i => i.TransactionCode.Equals(transaction.TransactionCode));

                var transactionDefect = vfi.DefectTransactions.FirstOrDefault(x => x.ImportTransactionId == transactionId);
                var importPlating = vfi.ImportNCU_QCB.FirstOrDefault(id => id.TransactionId == transactionId);
                var exportPlating = vfi.ExportGCN_NCU.FirstOrDefault(id => id.TransactionId == transactionId);
                foreach (var transactionDetail in transactionDetails) {
                    var entity = new TransactionDetailModel {
                        TransactionDetailId = transactionDetail.TransactionDetailId,
                        LotNumber = transactionDetail.LotNumber,
                        ReferenceId = transactionDetail.ReferenceId,
                        UnitWeight =
                            MyUtilities.Product.GetProductInvWeight(transactionDetail.ReferenceId,
                                transaction.WarehouseIssueId ?? transaction.WarehouseReceiptId.Value),
                        ProductImg = transactionDetail.DrawingFinish,
                        ProductCode = transactionDetail.ProductCode,
                        UploadDate = transactionDetail.UploadDate.ToString("yyyyMMddhhmmss"),
                        CustomerCode = transactionDetail.CustomerCode,
                        Quantity = transactionDetail.Quantity,
                        Note = transactionDetail.Note,
                        IsManagerLv2 = invManagerLv2,
                        IsInternal = transactionDetail.IsInternal ?? false,
                    };
                    if (string.IsNullOrWhiteSpace(entity.ProductImg))
                        entity.ProductImg = "askquestion.jpg";
                    if (transaction.WarehouseIssueId == (byte)MyUtilities.Warehouse.Business) {
                        entity.AvailableQuantity = 0;
                    }
                    else {
                        var availableQuality =
                            vfi.ProductInventories.FirstOrDefault(
                                pi =>
                                pi.ProductInventoryId == transactionDetail.ProductInvId);
                        if (availableQuality == null) {
                            availableQuality = vfi.ProductInventories.FirstOrDefault(x => x.ProductId == transactionDetail.ReferenceId
                                                                && x.WarehouseId == transaction.WarehouseIssueId
                                                                && x.LotNumber.Equals(transactionDetail.LotNumber));
                        }
                        entity.AvailableQuantity = availableQuality != null ? availableQuality.TotalQty : 0;
                    }
                    if ((transaction.WarehouseIssueId == MyUtilities.Warehouse.Plating ||
                     transaction.WarehouseIssueId == MyUtilities.Warehouse.PlatingTest) &&
                         transaction.WarehouseReceiptId == MyUtilities.Warehouse.QcB) {
                        if (importPlating != null) {
                            entity.Note = importPlating.PlatingForm.PlatingFormNumber;
                        }
                    }
                    if (transaction.WarehouseIssueId == MyUtilities.Warehouse.WaitingPlating &&
                        (transaction.WarehouseReceiptId == MyUtilities.Warehouse.PlatingTest ||
                            transaction.WarehouseReceiptId == MyUtilities.Warehouse.Plating)) {
                        if (exportPlating != null) {
                            entity.Note = exportPlating.PlatingForm.PlatingFormNumber;
                        }
                    }
                    if (transaction.WarehouseReceiptId == MyUtilities.Warehouse.Processing) {
                        //if (transactionDetail.ErrorId != null)
                        //    entity.Note += transactionDetail.ProcessError.Description;
                        if (transactionDefect != null) {
                            //var defectDetail  = vfi.DefectTransactionDetails.FirstOrDefault(x=> x.lot
                        }
                    }
                    entity.QuantityKg = entity.UnitWeight * entity.Quantity;
                    //if (exportTpDetails.Any()) {
                    //    var exportDetail = exportTpDetails.FirstOrDefault(ed => ed.TransactionDetailId == transactionDetail.TransactionDetailId);
                    //    if (exportDetail != null)
                    //        entity.QuantityKg = exportDetail.Weight;
                    //}
                    if (importSx1 == null) {
                        var material = transactionDetail.ProductionMaterial;
                        if (material != null) {
                            entity.MaterialCode = material.Material.MaterialCode;
                            entity.MaterialTypeName = material.Material.MaterialType.MaterialTypeName;
                            entity.Identity = material.Material.MaterialType.IdentityCode.Trim();
                        }
                    }
                    else {
                        var importDetail =
                            importSx1.ImportFormSX1Detail.FirstOrDefault(
                                id => id.ProductId == transactionDetail.ReferenceId);
                        if (importDetail != null) {
                            entity.MaterialCode = importDetail.MaterialInventory.Material.MaterialCode;
                            entity.MaterialTypeName =
                                importDetail.MaterialInventory.Material.MaterialType.MaterialTypeName;
                            entity.Identity =
                                importDetail.MaterialInventory.Material.MaterialType.IdentityCode.Trim();
                        }
                    }
                    model.Add(entity);
                }
                if (!transactionDetails.Any()) {
                    //var transactionProducts = transaction.TransactionProducts;

                    var exportTpDetails = (from ed in vfi.ExportFormTP_KDDetail
                                           where ed.ExportFormTP_KD.TransactionCode.Equals(transaction.TransactionCode)
                                           select new {
                                               ed.ProductId,
                                               ed.Quality,
                                               ed.Weight,
                                           }).ToList();
                    var transactionProducts = (from x in vfi.TransactionProducts
                                               where x.TransactionId == transactionId
                                               select new {
                                                   x.DetailId,
                                                   x.ProductId,
                                                   x.Product.ProductCode,
                                                   x.Product.Customer.CustomerCode,
                                                   x.Product.DrawingFinish,
                                                   x.Note,
                                                   x.Quantity,
                                                   UploadDate = x.Product.UploadDate ?? DateTime.Now,
                                                   ProductionMaterial = x.Product.ProductionMaterials.FirstOrDefault(pm => pm.Active)
                                               }).ToList();
                    foreach (var detail in transactionProducts) {
                        var entity = new TransactionDetailModel {
                            TransactionDetailId = detail.DetailId,
                            ProductCode = detail.ProductCode,
                            CustomerCode = detail.CustomerCode,
                            Quantity = detail.Quantity,
                            Note = detail.Note,
                            LotNumber = "",
                            ReferenceId = detail.ProductId,
                            UnitWeight =
                                MyUtilities.Product.GetProductInvWeight(detail.ProductId,
                                    transaction.WarehouseIssueId ?? transaction.WarehouseReceiptId.Value),
                            ProductImg = detail.DrawingFinish,
                            UploadDate = detail.UploadDate.ToString("yyyyMMddhhmmss"),
                        };
                        entity.QuantityKg = entity.UnitWeight * entity.Quantity;
                        if (string.IsNullOrWhiteSpace(entity.ProductImg))
                            entity.ProductImg = "askquestion.jpg";
                        if (transaction.WarehouseIssueId == (byte)MyUtilities.Warehouse.Business) {
                            entity.AvailableQuantity = 0;
                        }
                        else {
                            var invs =
                                vfi.ProductInventories.Where(
                                    pi =>
                                        pi.ProductId == detail.ProductId &&
                                        pi.WarehouseId == (transaction.WarehouseIssueId ?? 0)).ToList();
                            entity.AvailableQuantity = invs.Sum(pi => pi.TotalQty);
                        }
                        if ((transaction.WarehouseIssueId == MyUtilities.Warehouse.Plating ||
                             transaction.WarehouseIssueId == MyUtilities.Warehouse.PlatingTest) &&
                            transaction.WarehouseReceiptId == MyUtilities.Warehouse.QcB) {
                            if (importPlating != null) {
                                entity.Note = importPlating.PlatingForm.PlatingFormNumber;
                            }
                        }
                        if (transaction.WarehouseIssueId == MyUtilities.Warehouse.WaitingPlating &&
                            (transaction.WarehouseReceiptId == MyUtilities.Warehouse.PlatingTest ||
                             transaction.WarehouseReceiptId == MyUtilities.Warehouse.Plating)) {
                            if (exportPlating != null) {
                                entity.Note = exportPlating.PlatingForm.PlatingFormNumber;
                            }
                        }
                        if (transaction.WarehouseReceiptId == MyUtilities.Warehouse.Processing) {
                            //if (transactionDetail.ErrorId != null)
                            //    entity.Note = detail.ProcessError.Description;
                        }
                        if (exportTpDetails.Any()) {
                            var exportDetail = exportTpDetails.FirstOrDefault(ed => ed.ProductId == detail.ProductId);
                            if (exportDetail != null)
                                entity.QuantityKg = exportDetail.Weight;
                        }
                        var material = detail.ProductionMaterial;
                        if (material != null) {
                            entity.MaterialCode = material.Material.MaterialCode;
                            entity.MaterialTypeName = material.Material.MaterialType.MaterialTypeName;
                            entity.Identity = material.Material.MaterialType.IdentityCode.Trim();
                        }

                        model.Add(entity);
                    }

                }
            }

            return model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode).ThenBy(m => m.LotNumber).ToList();
        }

        [GridAction]
        public ActionResult UpdateTransactionDetail(TransactionDetailModel update) {
            long transactionId = 0;
            try {
                using (var vfi = new tammaContext()) {
                    var transactionDetail = vfi.TransactionDetails.FirstOrDefault(td => td.TransactionDetailId == update.TransactionDetailId);
                    if (transactionDetail == null)
                        throw new AggregateException("Lỗi! Không tìm thấy chi tiết cần huỷ");
                    transactionId = transactionDetail.TransactionId.Value;
                    var transaction = transactionDetail.Transaction;
                    if (transactionDetail.Transaction.Status != (byte)MyUtilities.Transaction.Status.Open)
                        throw new AggregateException("Lỗi! Phiếu không trong trạng thái đợi duyệt!");
                    var reduceQuantity = transactionDetail.Quantity - update.Quantity.Value;

                    // xuat ban
                    if (transaction.WarehouseReceiptId == MyUtilities.Warehouse.Business) {
                        var transactionProduct = vfi.TransactionProducts.FirstOrDefault(x => x.TransactionId == transactionId
                            && x.ProductId == transactionDetail.ReferenceId);
                        if (transactionProduct != null) {
                            if (transactionProduct.Quantity > reduceQuantity) {
                                transactionProduct.QuantityKg = reduceQuantity / transactionProduct.Quantity * transactionProduct.QuantityKg;
                                transactionProduct.Quantity -= reduceQuantity;
                            }
                            else {
                                vfi.TransactionProducts.Remove(transactionProduct);
                            }
                        }
                        var detail = vfi.ExportFormTP_KDDetail.FirstOrDefault(x => x.ProductId == transactionDetail.ReferenceId &&
                            x.ExportFormTP_KD.TransactionCode.Equals(transaction.TransactionCode));
                        if (detail != null) {
                            if (detail.Quality > reduceQuantity) {
                                detail.Weight = reduceQuantity / detail.Quality * detail.Weight;
                                detail.Quality -= reduceQuantity;
                            }
                            else {
                                vfi.ExportFormTP_KDDetail.Remove(detail);
                            }
                        }
                    }
                        // tra hang
                    else if (transaction.WarehouseIssueId == MyUtilities.Warehouse.Business
                        && transaction.WarehouseReceiptId == MyUtilities.Warehouse.Finish) {
                        throw new AggregateException("Lỗi! Chưa xử lý huỷ biểu mẫu trả hàng");
                    }
                        // xuat gia cong ngoai
                    else if (transaction.WarehouseIssueId == MyUtilities.Warehouse.WaitingPlating
                        && (transaction.WarehouseReceiptId == MyUtilities.Warehouse.Plating
                        || transaction.WarehouseReceiptId == MyUtilities.Warehouse.PlatingTest)) {
                        var detail = vfi.ExportGCN_NCUDetail.FirstOrDefault(x => x.ProductInvId == transactionDetail.ProductInvId
                        && x.ExportGCN_NCU.TransactionId == transactionId);
                        if (detail != null) {
                            if (detail.RealNumber > reduceQuantity) {
                                detail.Weight = reduceQuantity / detail.RealNumber * detail.Weight;
                                detail.RealNumber -= reduceQuantity;
                            }
                            else {
                                vfi.ExportGCN_NCUDetail.Remove(detail);
                            }
                        }
                    }
                    // nhap gia cong ngoai
                    else if (transaction.WarehouseReceiptId == MyUtilities.Warehouse.QcB
                        && (transaction.WarehouseIssueId == MyUtilities.Warehouse.Plating
                        || transaction.WarehouseIssueId == MyUtilities.Warehouse.PlatingTest)) {
                        var detail = vfi.ImportNCU_QCBDetail.FirstOrDefault(x => x.ProductInvId == transactionDetail.ProductInvId
                            && x.ImportNCU_QCB.TransactionId == transactionId);
                        if (detail != null) {
                            if (detail.RealNumber > reduceQuantity) {
                                detail.Weight = reduceQuantity / detail.RealNumber * detail.Weight;
                                detail.RealNumber -= reduceQuantity;
                            }
                            else {
                                vfi.ImportNCU_QCBDetail.Remove(detail);
                            }
                        }
                    }
                    // san xuat 2
                    else if (transaction.WarehouseIssueId == MyUtilities.Warehouse.Production2) {
                        // chua co quy trinh dac biet
                    }
                    // san xuat phay cnc
                    else if (transaction.WarehouseIssueId == MyUtilities.Warehouse.Cnc) {
                        var form = vfi.ImportFormCncs.FirstOrDefault(x => x.TransactionCode.Equals(transaction.TransactionCode));
                        if (form != null) {
                            throw new AggregateException("Lỗi! Chưa xử lý huỷ biểu mẫu phay CNC");
                        }
                    }
                    // san xuat 1
                    else if (transaction.WarehouseIssueId == MyUtilities.Warehouse.Production1) {
                        var form = vfi.ImportFormSX1.FirstOrDefault(x => x.TransactionCode.Equals(transaction.TransactionCode));
                        if (form != null) {
                            throw new AggregateException("Lỗi! Chưa xử lý huỷ biểu mẫu sản xuất 1");
                        }
                    }


                    if (update.Quantity > 0)
                        transactionDetail.Quantity = update.Quantity.Value;
                    else
                        vfi.TransactionDetails.Remove(transactionDetail);
                    vfi.SaveChanges();

                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateTransactionDetail", ex.Message);
            }
            return View(new GridModel(GetTransactionProductDetailByTransactionId(transactionId)));
        }

        [GridAction]
        public ActionResult UpdateTransactionMaterialDetail(TransactionDetailModel update) {

            try {
                if (!Request.IsAuthenticated)
                    throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
                using (var vfi = new tammaContext()) {
                    var transactionDetail = vfi.TransactionDetails.FirstOrDefault(td => td.TransactionDetailId == update.TransactionDetailId);
                    if (transactionDetail == null)
                        throw new AggregateException("Lỗi! Không tìm thấy phiếu giao dịch nguyên liệu");

                    update.TransactionId = transactionDetail.TransactionId;

                    var transaction = transactionDetail.Transaction;

                    // nhap kho
                    if (transaction.EoI.Equals("0")) {
                        var importPo = vfi.ImportPurchaseOrders.FirstOrDefault(i => i.TransactionId == transaction.TransactionId);
                        if (importPo == null)
                            throw new AggregateException("Lỗi! Không tìm thấy phiếu nhập mua nguyên liệu");
                        var importDetail =
                            importPo.ImportPurchaseOrderDetails.FirstOrDefault(
                                id => id.MaterialId == transactionDetail.ReferenceId);
                        if (importDetail == null)
                            throw new AggregateException("Lỗi! Không tìm thấy chi tiết nhập mua nguyên liệu");

                        if (importDetail.PoReferenceDetailId != null)
                            throw new AggregateException("Lỗi! Không hổ trợ đã sửa chi tiết đã xuất hoá đơn.");

                        //var importDetail = vfi.ImportPurchaseOrders
                        var difQuantity = Math.Round(update.Quantity.Value - transactionDetail.Quantity, 2);
                        if (transaction.Status == (byte)MyUtilities.Transaction.Status.Approved) {
                            var materialInv =
                                vfi.MaterialInventories.FirstOrDefault(
                                    mi =>
                                    mi.MaterialId == transactionDetail.ReferenceId &&
                                    mi.LotNumber.Equals(importDetail.LotNumber) &&
                                    mi.Length == importDetail.Length &&
                                    mi.VendorId == importDetail.VendorId);
                            if (materialInv == null)
                                throw new AggregateException("Lỗi! Không tìm thấy tồn kho - materialInv");
                            if (Math.Round(materialInv.TotalQty + difQuantity) < 0)
                                throw new AggregateException("Lỗi! Không đủ tồn kho để giảm số lượng.");
                            var period = vfi.MaterialInventoryPeriods.FirstOrDefault(mip =>
                                mip.MaterialInventoryId == materialInv.MaterialInventoryId &&
                                mip.TransactionId == transaction.TransactionId);
                            if (period == null)
                                throw new AggregateException("Lỗi! Không tìm thấy tồn kho - period");
                            materialInv.UnitWeight = update.UnitWeight;
                            materialInv.Length = update.Length;
                            materialInv.LotNumber = update.LotNumber;
                            period.Quantity += difQuantity;
                            period.LastPeriodQuantity = period.EarlyPeriodQuantity + period.Quantity;
                            materialInv.TotalQty += difQuantity;
                        }
                        importDetail.UnitWeight = update.UnitWeight;
                        if (importPo.PurchaseOrderId != null) {
                            var purchaseOrderDetail = vfi.PurchaseOrderDetails.FirstOrDefault(pod =>
                                pod.PurchaseOrderId == importPo.PurchaseOrderId &&
                                pod.ReferenceId == transactionDetail.ReferenceId);
                            if (purchaseOrderDetail == null)
                                throw new AggregateException("Lỗi! Không tìm thấy chi tiết mua hàng");
                            if (purchaseOrderDetail.Unit.Contains("Kg"))
                                purchaseOrderDetail.ReceivedQty += (difQuantity * importDetail.UnitWeight);
                            else
                                purchaseOrderDetail.ReceivedQty += (difQuantity);
                        }
                        importDetail.LotNumber = update.LotNumber;
                        importDetail.Length = update.Length;
                        transactionDetail.Quantity = update.Quantity.Value;
                        transactionDetail.QuantityKg = transactionDetail.Quantity * importDetail.UnitWeight;
                        transactionDetail.ModifiedDate = DateTime.Now;
                        transactionDetail.ModifiedUser = HttpContext.User.Identity.Name;
                        importDetail.Quantity = transactionDetail.Quantity;
                        importDetail.QuantityKg = transactionDetail.QuantityKg.Value;
                        //importDetail.PoReferenceDetailId;
                        vfi.SaveChanges();
                    }
                    // xuat kho
                    else if (transaction.EoI.Equals("1")) {
                        throw new AggregateException("Lỗi! Không hổ trợ xuất kho.");
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateTransactionMaterialDetail", ex.Message);
            }
            return View(new GridModel(GetTransactionMaterialDetailByTransactionId(update.TransactionId.Value)));
        }

        #endregion

        #region Input Transaction

        [GridAction]
        public ActionResult ImportTransactionMaterialInventoryByPO(
            [Bind(Prefix = "inserted")]IEnumerable<MaterialInventoryModel> insertedDetails,
            [Bind(Prefix = "updated")]IEnumerable<MaterialInventoryModel> updatedDetails,
            [Bind(Prefix = "deleted")]IEnumerable<MaterialInventoryModel> deletedDetails,
            string monthlyDate, int? poId,
            string exportOrImport, int exchangeRate
            ) {
            if (poId == null || poId == 0) throw new AggregateException("Lỗi phiếu nhập!");
            if (updatedDetails != null) {
                try {
                    if (!Request.IsAuthenticated)
                        throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

                    var createdDate = MyUtilities.Function.ParseDate(monthlyDate);
                    if (createdDate > DateTime.Now.AddDays(1)) {
                        throw new AggregateException("Lỗi! Xem lại ngày ( lớn hơn hiện tại) !");
                    }
                    if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, createdDate)) {
                        throw new AggregateException(
                            @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                    }
                    using (var vfi = new tammaContext()) {
                        var purchaseOrder = vfi.PurchaseOrders.FirstOrDefault(po => po.PurchaseOrderId == poId);
                        if (purchaseOrder == null) throw new AggregateException("Lỗi phiếu nhập!");
                        if (!purchaseOrder.CurrencyCode.Trim().Equals("VND"))
                            if (exchangeRate <= 1)
                                throw new AggregateException("Lỗi tiền tệ ! Chưa nhập tỉ giá.");
                        var transaction = new Vfi.Models.Transaction {
                            TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Material, 1),
                            EoI = exportOrImport,
                            MoP = true,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = createdDate,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            PoId = poId,
                        };
                        var importPO = new ImportPurchaseOrder {
                            ImportDate = createdDate,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            PurchaseOrderId = poId,
                            TransactionId = transaction.TransactionId,
                            Transaction = transaction,
                            ExchangeRate = exchangeRate,
                            PurchasingSignature = 0,
                        };
                        transaction.ReferenceId = importPO.ImportId;
                        var onShelves = new List<OnShelf>();
                        foreach (var detail in updatedDetails) {
                            //if (detail.Quantity == 0 && detail.QuantityKg == 0) continue;
                            ////if (detail.Quantity == 0)
                            ////    throw new AggregateException("Vui lòng nhập số lượng cây - " + detail.MaterialCode);
                            if (detail.QuantityKg == 0)
                                throw new AggregateException("Vui lòng nhập trọng lượng - " + detail.MaterialCode);
                            //if (string.IsNullOrWhiteSpace(detail.LotNumber))
                            //    throw new AggregateException("Số lô không được để trống! " + detail.MaterialCode);
                            if (detail.UnitWeight == 0)
                                throw new AggregateException("Vui lòng nhập trọng lượng cây - " + detail.MaterialCode);
                            if (detail.Length == 0)
                                throw new AggregateException("Vui lòng nhập chiều dài (mm) - " + detail.MaterialCode);
                            var poDetail = purchaseOrder.PurchaseOrderDetails.FirstOrDefault(x => x.PurchaseOrderDetailId == detail.PurchaseOrderDetailId);
                            if (poDetail == null) continue;

                            var transactionDetail = new Vfi.Models.TransactionDetail {
                                ReferenceId = detail.MaterialId,
                                MoP = true,
                                //Quantity = Math.Round(detail.QuantityKg / detail.UnitWeight, 0),
                                //QuantityKg = Math.Round(detail.QuantityKg, 3),
                                Price = detail.Price,
                                UnitMeasure = detail.UnitMeasure,
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                Note = detail.Note,
                                PoDetailId = detail.PurchaseOrderDetailId
                            };
                            if (poDetail.Unit.Contains("Kg")) {
                                transactionDetail.QuantityKg = Math.Round(detail.QuantityKg, 3);
                                transactionDetail.Quantity = Math.Round(transactionDetail.QuantityKg.Value / detail.UnitWeight, 0);
                            }
                            else {
                                transactionDetail.Quantity = Math.Round(detail.QuantityKg, 0);
                                transactionDetail.QuantityKg = Math.Round(transactionDetail.Quantity * detail.UnitWeight, 3);
                            }
                            if (detail.DrawerId > 0) {
                                transactionDetail.DrawerId = detail.DrawerId;
                            }

                            transaction.TransactionDetails.Add(transactionDetail);
                            var importDetail = new ImportPurchaseOrderDetail {
                                ImportId = importPO.ImportId,
                                ImportPurchaseOrder = importPO,
                                Quantity = transactionDetail.Quantity,
                                QuantityKg = transactionDetail.QuantityKg ?? 0,
                                MaterialId = detail.MaterialId,
                                //LotNumber = detail.LotNumber,
                                UnitPrice = detail.UnitPrice * importPO.ExchangeRate,
                                UnitWeight = detail.UnitWeight,
                                Note = detail.Note,
                                VendorId = detail.VendorId,
                                StoreCode = detail.StoreCode,
                                Length = detail.Length,
                                PoDetailId = detail.PurchaseOrderDetailId
                            };
                            importPO.ImportPurchaseOrderDetails.Add(importDetail);
                        }
                        if (importPO.ImportPurchaseOrderDetails.Count != 0 &&
                            transaction.TransactionDetails.Count != 0) {
                            vfi.Transactions.Add(transaction);
                            vfi.ImportPurchaseOrders.Add(importPO);
                            vfi.SaveChanges();
                        }
                    }
                }
                catch (Exception exception) {
                    ModelState.AddModelError("MaterialCodeName", "" + exception.Message);
                }
            }

            return View(new GridModel(new List<MaterialInventoryModel>()));
        }


        [GridAction]
        public ActionResult ExportTransactionMaterialInventory(
            [Bind(Prefix = "inserted")]IEnumerable<MaterialInventoryModel> insertedDetails,
            [Bind(Prefix = "updated")]IEnumerable<MaterialInventoryModel> updatedDetails,
            [Bind(Prefix = "deleted")]IEnumerable<MaterialInventoryModel> deletedDetails,
            string monthlyDate, string exportOrImport, bool isInternal
            ) {
            if (updatedDetails != null) {
                try {

                    if (!Request.IsAuthenticated)
                        throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

                    var createdDate = MyUtilities.Function.ParseDate(monthlyDate);
                    if (createdDate > DateTime.Now.AddDays(1)) {
                        throw new AggregateException("Lỗi! Xem lại ngày ( lớn hơn hiện tại) !");
                    }
                    if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, createdDate)) {
                        throw new AggregateException(
                            @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                    }
                    using (var vfi = new tammaContext()) {
                        var transaction = new Vfi.Models.Transaction {
                            TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Material, 1),
                            EoI = exportOrImport,
                            MoP = true,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = createdDate,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            IsInternal = isInternal
                        };
                        var exportMaterial = new ExportMaterial {
                            ExportDate = createdDate,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            //PurchaseOrderId = poId,
                            TransactionId = transaction.TransactionId,
                            Transaction = transaction,
                        };
                        var materialInvIds = updatedDetails.Select(mi => mi.MaterialInventoryId).ToList();
                        var materialInvs = from mi in vfi.MaterialInventories
                                           where materialInvIds.Contains(mi.MaterialInventoryId)
                                           select mi;
                        foreach (var detail in updatedDetails) {
                            if (detail.Quantity == 0 && !detail.IsDestroy) continue;
                            //if ((detail.Quantity == 0) && !detail.IsDestroy)
                            //    throw new AggregateException("Vui lòng nhập số lượng cây - " + detail.MaterialCode);
                            //if (string.IsNullOrWhiteSpace(detail.LotNumber))
                            //    throw new AggregateException("Số lô không được để trống! " + detail.MaterialCode);

                            var transactionDetail = new Vfi.Models.TransactionDetail {
                                ReferenceId = detail.MaterialId,
                                MoP = true,
                                Quantity = Math.Round(detail.Quantity, 2),
                                //QuantityKg = detail.QuantityKg,
                                Price = detail.Price,
                                UnitMeasure = detail.UnitMeasure,
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                Note = detail.Note,
                                IsInternal = transaction.IsInternal
                            };
                            transactionDetail.QuantityKg = transactionDetail.Quantity * detail.UnitWeight;
                            transaction.TransactionDetails.Add(transactionDetail);

                            var exportDetail = new ExportMaterialDetail {
                                // MachineId = detail.MachineId,
                                ExportId = exportMaterial.ExportId,
                                ExportMaterial = exportMaterial,
                                MaterialInvId = detail.MaterialInventoryId,
                                MaterialId = detail.MaterialId,
                                Quantity = transactionDetail.Quantity,
                                TransactionDetailId = transactionDetail.TransactionDetailId,
                                TransactionDetail = transactionDetail,
                            };
                            if (detail.IsDestroy) {
                                var materialInv =
                                    materialInvs.FirstOrDefault(
                                        mi => mi.MaterialInventoryId == detail.MaterialInventoryId);
                                transactionDetail.Quantity = materialInv.TotalQty;
                                transactionDetail.QuantityKg = materialInv.TotalQtyKg;
                                exportDetail.Quantity = transactionDetail.Quantity;
                                exportDetail.QuantityKg = transactionDetail.QuantityKg;
                            }
                            exportMaterial.ExportMaterialDetails.Add(exportDetail);
                        }
                        if (exportMaterial.ExportMaterialDetails.Count != 0 &&
                            transaction.TransactionDetails.Count != 0) {
                            vfi.Transactions.Add(transaction);
                            vfi.ExportMaterials.Add(exportMaterial);
                            vfi.SaveChanges();
                        }
                    }
                }
                catch (Exception exception) {
                    ModelState.AddModelError("MaterialCodeName", "" + exception.Message);
                }
            }

            Session["SessionExportTransactionMaterialIds"] = null;

            return View(new GridModel(new List<MaterialInventoryModel>()));
        }

        [GridAction]
        public ActionResult ImportTransactionMaterialInventory(
            [Bind(Prefix = "inserted")]IEnumerable<MaterialInventoryModel> insertedDetails,
            [Bind(Prefix = "updated")]IEnumerable<MaterialInventoryModel> updatedDetails,
            [Bind(Prefix = "deleted")]IEnumerable<MaterialInventoryModel> deletedDetails,
            string monthlyDate, string exportOrImport, bool isInternal
            ) {
            if (updatedDetails != null) {
                try {

                    if (!Request.IsAuthenticated)
                        throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

                    var createdDate = MyUtilities.Function.ParseDate(monthlyDate);
                    if (createdDate > DateTime.Now.AddDays(1)) {
                        throw new AggregateException("Lỗi! Xem lại ngày ( lớn hơn hiện tại) !");
                    }
                    if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, createdDate)) {
                        throw new AggregateException(
                            @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                    }
                    using (var vfi = new tammaContext()) {
                        var transaction = new Vfi.Models.Transaction {
                            TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Material, 1),
                            EoI = exportOrImport,
                            MoP = true,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = createdDate,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            IsInternal = isInternal
                        };
                        var importPO = new ImportPurchaseOrder {
                            ImportDate = createdDate,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            //PurchaseOrderId = poId,
                            TransactionId = transaction.TransactionId,
                            Transaction = transaction,
                            ExchangeRate = 1,
                            PurchasingSignature = 0,
                        };

                        foreach (var detail in updatedDetails) {
                            if (detail.Quantity == 0 && detail.QuantityKg == 0) continue;
                            if (detail.Quantity == 0)
                                throw new AggregateException("Vui lòng nhập số lượng cây - " + detail.MaterialCode);
                            //if (string.IsNullOrWhiteSpace(detail.LotNumber))
                            //    throw new AggregateException("Số lô không được để trống! " + detail.MaterialCode);
                            if (detail.Length < 10)
                                throw new AggregateException("Vui lòng nhập đúng chiều dài (>10mm) - " + detail.MaterialCode);
                            var transactionDetail = new Vfi.Models.TransactionDetail {
                                ReferenceId = detail.MaterialId,
                                MoP = true,
                                Quantity = Math.Round(detail.Quantity, 2),
                                Price = Math.Round(detail.Price, 0),
                                UnitMeasure = detail.UnitMeasure,
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                Note = detail.Note,
                                IsInternal = transaction.IsInternal,
                                LotNumber = detail.LotNumber,
                            };
                            transactionDetail.QuantityKg = transactionDetail.Quantity * detail.UnitWeight;
                            transaction.TransactionDetails.Add(transactionDetail);
                            var importDetail = new ImportPurchaseOrderDetail {
                                ImportId = importPO.ImportId,
                                ImportPurchaseOrder = importPO,
                                Quantity = Math.Round(detail.Quantity, 2),
                                QuantityKg = detail.QuantityKg,
                                MaterialId = detail.MaterialId,
                                LotNumber = detail.LotNumber,
                                Note = detail.Note,
                                UnitWeight = detail.UnitWeight,
                                UnitPrice = detail.UnitPrice,
                                VendorId = detail.VendorId,
                                StoreCode = detail.StoreCode,
                                Length = detail.Length,
                            };
                            transactionDetail.QuantityKg = importDetail.Quantity * importDetail.UnitWeight;
                            importPO.ImportPurchaseOrderDetails.Add(importDetail);
                        }
                        if (importPO.ImportPurchaseOrderDetails.Count != 0 &&
                            transaction.TransactionDetails.Count != 0) {
                            vfi.Transactions.Add(transaction);
                            vfi.ImportPurchaseOrders.Add(importPO);
                            vfi.SaveChanges();
                        }
                    }
                }
                catch (Exception exception) {
                    ModelState.AddModelError("MaterialCodeName", "" + exception.Message);
                }
            }

            Session["SessionImportTransactionMaterialIds"] = null;

            return View(new GridModel(new List<MaterialInventoryModel>()));
        }

        [GridAction]
        public ActionResult UpdateTransactionMaterialInventory(
            [Bind(Prefix = "inserted")]IEnumerable<MaterialInventoryModel> insertedMaterialInventoryDetails,
            [Bind(Prefix = "updated")]IEnumerable<MaterialInventoryRotateModel> updatedMaterialInventoryDetails,
            [Bind(Prefix = "deleted")]IEnumerable<MaterialInventoryModel> deletedMaterialInventoryDetails,
            string exportOrImport
            ) {

            return View(new GridModel(new List<MaterialInventoryModel>()));
        }

        public double GetWarehouseInvPeriod(int productId, int warehouseId, string date) {
            var inv = 0.0;
            using (var vfi = new tammaContext()) {

                var ci = new CultureInfo("vi-VN");
                var toDate = string.IsNullOrWhiteSpace(date)
                                      ? DateTime.Today
                                      : Convert.ToDateTime(date, ci);
                var warehouseInv =
                    vfi.ProductInventories.FirstOrDefault(
                        pi => pi.WarehouseId == warehouseId && pi.ProductId == productId);
                if (warehouseInv != null)
                    inv = warehouseInv.TotalQty;
                var periods =
                    vfi.ProductInventoryPeriods.Where(
                        pip => pip.ProductId == productId && pip.WarehouseId == warehouseId && pip.PeriodDate <= toDate);
                if (periods.Any()) {

                }
                var transactionDetails =
                    vfi.TransactionDetails.Where(
                        td =>
                        td.Transaction.Status == (byte)MyUtilities.Transaction.Status.Open &&
                        td.ReferenceId == productId &&
                        td.Transaction.WarehouseIssueId == warehouseId).ToList();
                inv -= transactionDetails.Sum(td => td.Quantity);
            }
            return inv;
        }

        [GridAction]
        public ActionResult CreateImportInternalProductInventory(
            [Bind(Prefix = "inserted")] IEnumerable<ProductInventoryRotateModel> inserteds,
            [Bind(Prefix = "updated")] IEnumerable<ProductInventoryRotateModel> updateds,
            [Bind(Prefix = "deleted")] IEnumerable<ProductInventoryRotateModel> deleteds,
            int warehouseId, string date
        ) {
            if (!Request.IsAuthenticated)
                throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
            if (updateds != null) {
                try {
                    var createdDate = MyUtilities.Function.ParseDate(date);
                    if (createdDate > DateTime.Now.AddDays(1)) {
                        throw new AggregateException("Lỗi! Xem lại ngày ( lớn hơn hiện tại) !");
                    }
                    if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, createdDate)) {
                        throw new AggregateException(
                            @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                    }
                    using (var vfi = new tammaContext()) {
                        var transaction = new Vfi.Models.Transaction {
                            WarehouseIssueId = null,
                            WarehouseReceiptId = warehouseId,
                            TransactionCode =
                                MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                            EoI = Convert.ToChar(MyUtilities.Transaction.EoIEnum.Export) + "",
                            MoP = MyUtilities.Transaction.MoP.Product,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = createdDate,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            IsInternal = true
                        };
                        foreach (var detail in updateds) {
                            detail.Quantity = Math.Round(detail.Quantity);
                            if (detail.Quantity == 0) continue;
                            var model = new Vfi.Models.TransactionDetail {
                                ReferenceId = detail.ProductId,
                                MoP = MyUtilities.Transaction.MoP.Product,
                                Quantity = detail.Quantity,
                                UnitMeasure = detail.UnitMeasure,
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                Note = detail.Note,
                                TransactionId = transaction.TransactionId,
                                LotNumber = (detail.LotNumber + "").Trim(),
                                ProductInvId = null,
                                IsInternal = transaction.IsInternal
                            };
                            transaction.TransactionDetails.Add(model);
                        }
                        if (transaction.TransactionDetails.Any()) {
                            vfi.Transactions.Add(transaction);
                            vfi.SaveChanges();
                        }
                    }
                }
                catch (Exception exception) {
                    ModelState.AddModelError("ProductCodeName", "" + exception.Message);
                }
            }
            Session["SessionImportInternalTransactionProduct"] = new List<ProductInventoryRotateModel>();
            return View(new GridModel(new List<ProductInventoryModel>()));
        }

        [GridAction]
        public ActionResult CreateExportInternalProductInventory(
            [Bind(Prefix = "inserted")] IEnumerable<ProductInventoryRotateModel> inserteds,
            [Bind(Prefix = "updated")] IEnumerable<ProductInventoryRotateModel> updateds,
            [Bind(Prefix = "deleted")] IEnumerable<ProductInventoryRotateModel> deleteds,
            int warehouseId, string date
        ) {
            if (!Request.IsAuthenticated)
                throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
            if (updateds != null) {
                try {
                    var createdDate = MyUtilities.Function.ParseDate(date);
                    if (createdDate > DateTime.Now.AddDays(1)) {
                        throw new AggregateException("Lỗi! Xem lại ngày ( lớn hơn hiện tại) !");
                    }
                    if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, createdDate)) {
                        throw new AggregateException(
                            @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                    }
                    using (var vfi = new tammaContext()) {
                        var transaction = new Vfi.Models.Transaction {
                            WarehouseIssueId = warehouseId,
                            WarehouseReceiptId = null,
                            TransactionCode =
                                MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                            EoI = Convert.ToChar(MyUtilities.Transaction.EoIEnum.Export) + "",
                            MoP = MyUtilities.Transaction.MoP.Product,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = createdDate,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            IsInternal = true
                        };
                        foreach (var detail in updateds) {
                            detail.Quantity = Math.Round(detail.Quantity);
                            if (detail.Quantity == 0) continue;
                            var productInv = vfi.ProductInventories
                                .FirstOrDefault(pi => pi.ProductInventoryId == detail.ProductInventoryId);
                            var quanity = Math.Round(productInv.TotalQty - detail.Quantity, 0);
                            if (quanity < 0)
                                throw new AggregateException("Lỗi! Số lượng tồn kho không đủ xuất.| " +
                                                             detail.ProductCode + " | " + detail.LotNumber);
                            var transactionDetails =
                                vfi.TransactionDetails
                                    .Where(td => td.Transaction.Status == (byte)MyUtilities.Transaction.Status.Open &&
                                                 td.ProductInvId == detail.ProductInventoryId)
                                    .ToList().Sum(td => td.Quantity);
                            quanity = Math.Round(quanity - transactionDetails, 0);
                            if (quanity < 0)
                                throw new AggregateException("Lỗi! Số lượng có thể chuyển không đủ xuất.| " +
                                                            detail.ProductCode + " | " + detail.LotNumber);
                            var model = new Vfi.Models.TransactionDetail {
                                ReferenceId = detail.ProductId,
                                MoP = MyUtilities.Transaction.MoP.Product,
                                Quantity = detail.Quantity,
                                UnitMeasure = detail.UnitMeasure,
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                Note = detail.Note,
                                TransactionId = transaction.TransactionId,
                                LotNumber = (detail.LotNumber + "").Trim(),
                                ProductInvId = detail.ProductInventoryId,
                                IsInternal = transaction.IsInternal
                            };
                            transaction.TransactionDetails.Add(model);
                        }
                        if (transaction.TransactionDetails.Any()) {
                            vfi.Transactions.Add(transaction);
                            vfi.SaveChanges();
                        }
                    }
                }
                catch (Exception exception) {
                    ModelState.AddModelError("ProductCodeName", "" + exception.Message);
                }
            }
            Session["SessionExportInternalTransactionProduct"] = new List<ProductInventoryRotateModel>();
            return View(new GridModel(new List<ProductInventoryModel>()));
        }

        [GridAction]
        public ActionResult CreateCombineProductInventory(
            [Bind(Prefix = "inserted")] IEnumerable<ProductCombinationRecipeModel> inserteds,
            [Bind(Prefix = "updated")] IEnumerable<ProductCombinationRecipeModel> updateds,
            [Bind(Prefix = "deleted")] IEnumerable<ProductCombinationRecipeModel> deleteds,
            int warehouseId, string date
        ) {
            if (!Request.IsAuthenticated)
                throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
            if (updateds != null) {
                try {
                    updateds = updateds.Where(x => Math.Round(x.Quantity) > 0);
                    if (!updateds.Any()) {
                        return View(new GridModel(new List<ProductInventoryModel>()));
                    }
                    var createdDate = MyUtilities.Function.ParseDate(date);
                    if (createdDate > DateTime.Now.AddDays(1)) {
                        throw new AggregateException("Lỗi! Xem lại ngày ( lớn hơn hiện tại) !");
                    }
                    if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, createdDate)) {
                        throw new AggregateException(
                            @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                    }
                    using (var vfi = new tammaContext()) {
                        var code = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1);
                        var transactionExport = new Vfi.Models.Transaction {
                            WarehouseIssueId = warehouseId,
                            WarehouseReceiptId = MyUtilities.Warehouse.Destroy,
                            TransactionCode = code,
                            EoI = Convert.ToChar(MyUtilities.Transaction.EoIEnum.Combine) + "",
                            MoP = MyUtilities.Transaction.MoP.Product,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = createdDate,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                        };
                        var transactionImport = new Vfi.Models.Transaction {
                            WarehouseReceiptId = transactionExport.WarehouseIssueId,
                            TransactionCode = transactionExport.TransactionCode,
                            EoI = transactionExport.EoI,
                            MoP = transactionExport.MoP,
                            CreatedUser = transactionExport.CreatedUser,
                            CreatedDate = transactionExport.CreatedDate,
                            Status = transactionExport.Status,
                            Active = true,
                            ModifiedUser = transactionExport.ModifiedUser,
                            ModifiedDate = transactionExport.ModifiedDate,
                        };
                        foreach (var detail in updateds) {
                            detail.Quantity = Math.Round(detail.Quantity);
                            if (detail.AvailableQuantity < detail.Quantity) {
                                throw new AggregateException("Lỗi! Không đủ số lượng đế ghép");
                            }
                            var recipe = vfi.ProductCombinationRecipes.FirstOrDefault(x => x.RecipeId == detail.RecipeId);
                            var recipeDetails = recipe.ProductCombinationRecipeDetails.Where(x => x.RequireNumber > 0).ToList();
                            var productIds = recipeDetails.Select(x => x.FromProductId).ToList();
                            var productInvs = vfi.ProductInventories.Where(pi => pi.TotalQty > 0 
                                                                        && productIds.Contains(pi.ProductId) 
                                                                        && pi.WarehouseId == warehouseId)
                                                                    .ToList();
                            foreach (var recipeDetail in recipeDetails) {
                                var requireQuantity = detail.Quantity * recipeDetail.RequireNumber;
                                while (requireQuantity > 0) {
                                    var productInvsById = productInvs.Where(x => x.ProductId == recipeDetail.FromProductId);
                                    var recipeQuantity = 0.0;
                                    foreach (var productInv in productInvsById) {
                                        if (productInv.TotalQty > requireQuantity) {
                                            recipeQuantity = requireQuantity;
                                            requireQuantity = 0;
                                        }
                                        else {
                                            recipeQuantity = productInv.TotalQty;
                                            requireQuantity -= productInv.TotalQty;
                                        }
                                        var productBase = new Vfi.Models.TransactionDetail {
                                            ReferenceId = recipeDetail.FromProductId,
                                            MoP = MyUtilities.Transaction.MoP.Product,
                                            Quantity = recipeQuantity,
                                            UnitMeasure = productInv.UnitMeasure,
                                            Active = true,
                                            ModifiedUser = HttpContext.User.Identity.Name,
                                            ModifiedDate = DateTime.Now,
                                            Note = "chuyển đổi " + recipe.Product.ProductCode,
                                            LotNumber = (productInv.LotNumber + "").Trim(),
                                            ProductInvId = productInv.ProductInventoryId
                                        };
                                        transactionExport.TransactionDetails.Add(productBase);
                                        if (requireQuantity <= 0) break;
                                    }
                                }
                            }
                            

                            var productRecipe = new Vfi.Models.TransactionDetail {
                                ReferenceId = recipe.ProductId,
                                MoP = MyUtilities.Transaction.MoP.Product,
                                Quantity = detail.Quantity,
                                QuantityKg = detail.Quantity * detail.ProductWeight,
                                UnitMeasure = "Pcs",
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                Note = detail.DetailDescription,
                                LotNumber = (detail.LotNumber + "").Trim(),
                            };
                            transactionImport.TransactionDetails.Add(productRecipe);
                        }
                        if (transactionImport.TransactionDetails.Any()) {
                            vfi.Transactions.Add(transactionImport);
                            vfi.Transactions.Add(transactionExport);
                            vfi.SaveChanges();
                        }
                    }
                }
                catch (Exception exception) {
                    ModelState.AddModelError("ProductCodeName", "" + exception.Message);
                }
            }
            Session["SessionRecipeTransactionProduct"] = new List<ProductInventoryRotateModel>();
            return View(new GridModel(new List<ProductInventoryModel>()));
        }

        [GridAction]
        public ActionResult CreateRotateProductInventory(
            [Bind(Prefix = "inserted")] IEnumerable<ProductInventoryRotateModel> inserteds,
            [Bind(Prefix = "updated")] IEnumerable<ProductInventoryRotateModel> updateds,
            [Bind(Prefix = "deleted")] IEnumerable<ProductInventoryRotateModel> deleteds,
            int warehouseIssueId, int warehouseReceiptId, string date
        ) {
            if (!Request.IsAuthenticated)
                throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
            if (updateds != null) {
                try {
                    var createdDate = MyUtilities.Function.ParseDate(date);
                    if (createdDate > DateTime.Now.AddDays(1)) {
                        throw new AggregateException("Lỗi! Xem lại ngày ( lớn hơn hiện tại) !");
                    }
                    if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, createdDate)) {
                        throw new AggregateException(
                            @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                    }
                    using (var vfi = new tammaContext()) {
                        var transaction = new Vfi.Models.Transaction {
                            WarehouseIssueId = warehouseIssueId,
                            WarehouseReceiptId = warehouseReceiptId,
                            TransactionCode =
                                MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                            EoI = Convert.ToChar(MyUtilities.Transaction.EoIEnum.Rotate) + "",
                            MoP = MyUtilities.Transaction.MoP.Product,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = createdDate,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                        };
                        foreach (var detail in updateds) {
                            detail.Quantity = Math.Round(detail.Quantity);
                            if (detail.Quantity == 0) continue;
                            var productInv = vfi.ProductInventories
                                .FirstOrDefault(pi => pi.ProductInventoryId == detail.ProductInventoryId);
                            var quanity = Math.Round(productInv.TotalQty - detail.Quantity, 0);
                            if (quanity < 0)
                                throw new AggregateException("Lỗi! Số lượng tồn kho không đủ xuất.| " +
                                                             detail.ProductCode + " | " + detail.LotNumber);
                            var transactionDetails =
                                vfi.TransactionDetails
                                    .Where(td => td.Transaction.Status == (byte)MyUtilities.Transaction.Status.Open &&
                                                 td.ProductInvId == detail.ProductInventoryId)
                                    .ToList().Sum(td => td.Quantity);
                            quanity = Math.Round(quanity - transactionDetails, 0);
                            if (quanity < 0)
                                throw new AggregateException("Lỗi! Số lượng có thể chuyển không đủ xuất.| " +
                                                            detail.ProductCode + " | " + detail.LotNumber);
                            var model = new Vfi.Models.TransactionDetail {
                                ReferenceId = detail.ProductId,
                                MoP = MyUtilities.Transaction.MoP.Product,
                                Quantity = detail.Quantity,
                                UnitMeasure = detail.UnitMeasure,
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                Note = detail.Note,
                                TransactionId = transaction.TransactionId,
                                LotNumber = (detail.LotNumber + "").Trim(),
                                ProductInvId = detail.ProductInventoryId,
                                StoreCode = detail.StoreCode
                            };
                            transaction.TransactionDetails.Add(model);
                        }
                        if (transaction.TransactionDetails.Any()) {
                            vfi.Transactions.Add(transaction);
                            vfi.SaveChanges();
                        }
                    }
                }
                catch (Exception exception) {
                    ModelState.AddModelError("ProductCodeName", "" + exception.Message);
                }
            }
            Session["SessionRotateTransactionProduct"] = new List<ProductInventoryRotateModel>();
            return View(new GridModel(new List<ProductInventoryModel>()));
        }

        [GridAction]
        public ActionResult CreateImportProductInventory(
            [Bind(Prefix = "inserted")] IEnumerable<ProductInventoryRotateModel> inserteds,
            [Bind(Prefix = "updated")] IEnumerable<ProductInventoryRotateModel> updateds,
            [Bind(Prefix = "deleted")] IEnumerable<ProductInventoryRotateModel> deleteds,
            int warehouseReceiptId, string date
        ) {
            if (!Request.IsAuthenticated)
                throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
            if (updateds != null) {
                try {
                    var createdDate = MyUtilities.Function.ParseDate(date);
                    if (createdDate > DateTime.Now.AddDays(1)) {
                        throw new AggregateException("Lỗi! Xem lại ngày ( lớn hơn hiện tại) !");
                    }
                    if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, createdDate)) {
                        throw new AggregateException(
                            @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                    }
                    using (var vfi = new tammaContext()) {
                        var transaction = new Vfi.Models.Transaction {
                            WarehouseIssueId = null,
                            WarehouseReceiptId = warehouseReceiptId,
                            TransactionCode =
                                MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                            EoI = Convert.ToChar(MyUtilities.Transaction.EoIEnum.Import) + "",
                            MoP = MyUtilities.Transaction.MoP.Product,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = createdDate,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                        };
                        foreach (var updated in updateds) {
                            updated.Quantity = Math.Round(updated.Quantity);
                            if (updated.Quantity == 0) continue;
                            var entity = new Vfi.Models.TransactionDetail {
                                ReferenceId = updated.ProductId,
                                MoP = MyUtilities.Transaction.MoP.Product,
                                Quantity = updated.Quantity,
                                UnitMeasure = updated.UnitMeasure,
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                Note = updated.Note,
                                TransactionId = transaction.TransactionId,
                                LotNumber = (updated.LotNumber + "").Trim(),
                            };
                            var productInv = vfi.ProductInventories
                                .FirstOrDefault(pi => pi.LotNumber.Equals(updated.LotNumber) &&
                                                      pi.ProductId == updated.ProductId &&
                                                      pi.WarehouseId == warehouseReceiptId);
                            if (productInv != null) {
                                entity.LotNumber = productInv.LotNumber;
                                entity.NextProcessId = productInv.ByProcessMachineId;
                            }
                            transaction.TransactionDetails.Add(entity);
                        }
                        if (transaction.TransactionDetails.Any()) {
                            vfi.Transactions.Add(transaction);
                            vfi.SaveChanges();
                        }
                    }
                }
                catch (Exception exception) {
                    ModelState.AddModelError("ProductCodeName", "" + exception.Message);
                }
            }
            Session["SessionRotateTransactionProduct"] = new List<ProductInventoryRotateModel>();
            return View(new GridModel(new List<ProductInventoryModel>()));
        }
        [GridAction]
        public ActionResult UpdateTransactionProductInventory(
            [Bind(Prefix = "inserted")] IEnumerable<ProductInventoryRotateModel> inserteds,
            [Bind(Prefix = "updated")] IEnumerable<ProductInventoryRotateModel> updateds,
            [Bind(Prefix = "deleted")] IEnumerable<ProductInventoryRotateModel> deleteds
            , string transactionCode
            , int? warehouseIssueId, int? warehouseReceiptId
            , int? warehouseId
            , bool materialOrProduct, string exportOrImport,
            string date
            ) {

            //if (updateds != null) {
            //    try {

            //        if (!Request.IsAuthenticated)
            //            throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

            //        var modifiedUser = HttpContext.User.Identity.Name;
            //        if (string.IsNullOrWhiteSpace(modifiedUser))
            //            throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (user null). ");
            //        ModelState.Clear();
            //        var ci = new CultureInfo("vi-VN");
            //        var createdDate = string.IsNullOrWhiteSpace(date)
            //                              ? DateTime.Today
            //                              : Convert.ToDateTime(date, ci);

            //        using (var vfi = new tammaContext()) {
            //            if (!exportOrImport.Equals(Convert.ToChar(MyUtilities.Transaction.EoIEnum.Import) + "")) {
            //                var productInvIds = updateds.Select(u => u.ProductInventoryId).Distinct().ToList();
            //                var productInvs =
            //                    vfi.ProductInventories.Where(pi => productInvIds.Contains(pi.ProductInventoryId));
            //                foreach (var productInvId in productInvIds) {
            //                    var updateById = updateds.Where(u => u.ProductInventoryId == productInvId).ToList();
            //                    var totalUpdate = updateById.Sum(u => u.Quantity);
            //                    var productInv = productInvs.FirstOrDefault(pi => pi.ProductInventoryId == productInvId);
            //                    if (productInv == null || totalUpdate > productInv.TotalQty)
            //                        throw new AggregateException("Lỗi! Số lượng tồn kho không đủ xuất.| " +
            //                                                     updateById.FirstOrDefault().ProductCode);
            //                    var transactionDetails =
            //                        vfi.TransactionDetails.Where(
            //                            td =>
            //                            td.Transaction.Status == (byte)MyUtilities.Transaction.Status.Open &&
            //                            td.ProductInvId == productInvId).ToList().Sum(td => td.Quantity);
            //                    if (totalUpdate + transactionDetails > productInv.TotalQty)
            //                        throw new AggregateException("Lỗi! Số lượng có thể chuyển không đủ xuất.| " +
            //                                                     updateById.FirstOrDefault().ProductCode);
            //                }
            //            }
            //            var transaction = new Vfi.Models.Transaction {
            //                //WarehouseIssueId = warehouseIssueId,
            //                //WarehouseReceiptId = warehouseReceiptId,
            //                WarehouseIssueId =
            //                    exportOrImport == Convert.ToChar(MyUtilities.Transaction.EoIEnum.Export).ToString()
            //                        ? warehouseId
            //                        : null,
            //                WarehouseReceiptId =
            //                    exportOrImport == Convert.ToChar(MyUtilities.Transaction.EoIEnum.Import).ToString()
            //                        ? warehouseId
            //                        : null,

            //                TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
            //                EoI = exportOrImport,
            //                MoP = materialOrProduct,
            //                CreatedUser = HttpContext.User.Identity.Name,
            //                CreatedDate = createdDate,
            //                Status = (byte)MyUtilities.Transaction.Status.Open,
            //                Active = true,
            //                ModifiedUser = HttpContext.User.Identity.Name,
            //                ModifiedDate = DateTime.Now,
            //            };
            //            if (exportOrImport == Convert.ToChar(MyUtilities.Transaction.EoIEnum.Rotate).ToString()) {
            //                transaction.WarehouseIssueId = warehouseIssueId;
            //                transaction.WarehouseReceiptId = warehouseReceiptId;
            //            }
            //            var listDetail = new List<Vfi.Models.TransactionDetail>();
            //            foreach (var detail in updateds) {
            //                var model = new Vfi.Models.TransactionDetail {
            //                    ReferenceId = detail.ProductId,
            //                    MoP = materialOrProduct,
            //                    Quantity = detail.Quantity,
            //                    UnitMeasure = detail.UnitMeasure,
            //                    Active = true,
            //                    ModifiedUser = HttpContext.User.Identity.Name,
            //                    ModifiedDate = DateTime.Now,
            //                    Note = detail.Note,
            //                    TransactionId = transaction.TransactionId,
            //                    //ErrorId = detail.ErrorId == 0 ? null : detail.ErrorId,
            //                };
            //                if (!exportOrImport.Equals(Convert.ToChar(MyUtilities.Transaction.EoIEnum.Import) + "")) {
            //                    model.ProductInvId = detail.ProductInventoryId;
            //                }
            //                listDetail.Add(model);
            //            }
            //            vfi.Transactions.Add(transaction);
            //            vfi.TransactionDetails.AddRange(listDetail);
            //            vfi.SaveChanges();
            //        }
            //    }
            //    catch (Exception exception) {
            //        ModelState.AddModelError("ProductCodeName", "" + exception.Message);
            //    }

            //}

            //Session["SessionRotateTransactionProduct"] = new List<ProductInventoryRotateModel>();
            return View(new GridModel(new List<ProductInventoryModel>()));
        }

        #endregion

        #region Tâm ma
        [GridAction]
        public ActionResult SelectFormByStatus(byte status, string transactionId, string fromDate, string toDate, int warehouseId) {
            if (string.IsNullOrWhiteSpace(fromDate))
                return View(new GridModel(new List<ManageImportExportModel>()));
            var model = new List<ManageImportExportModel>();
            var fDate = MyUtilities.Function.ParseDate(fromDate);
            var tDate = MyUtilities.Function.ParseDate(toDate);

            //if (status == 0) { status = (byte)MyUtilities.Transaction.Status.Approved; }
            using (var vfi = new tammaContext()) {
                // function code = ImportSX1
                var productionDates = vfi.ProductionLocks.Where(x => x.LockDate >= fDate && x.LockDate <= tDate).Select(x => new { x.LockDate, x.Shift1Name, x.Shift2Name }).ToList();
                var warehouse = vfi.Warehouses.FirstOrDefault(x => x.WarehouseId == warehouseId);
                if ((warehouse == null || warehouse.IsProduction)
                    && MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.ImportSx1)) {
                    var list = (from x in vfi.ImportFormSX1
                                where x.ImportDate >= fDate && x.ImportDate <= tDate
                                && (status == 0 || x.Status == status)
                                select new ManageImportExportModel {
                                    ModifiedUser = x.ModifiedUser,
                                    ModidifiedDate = x.ModifiedDate,
                                    CreatedDate = x.MaterialUseDate,
                                    ReportDate = x.ImportDate,
                                    TransactionCode = x.TransactionCode,
                                    TransactionId = 0,
                                    TotalQuality = x.ImportFormSX1Detail.Sum(y => y.Number1 + y.Number2 + y.Processing1 + y.Processing2),
                                    StatusInt = x.Status,
                                    //Status = MyUtilities.Transaction.CastText.GetTextStatus(x.Status), // error LINQ
                                    FormType = (byte)MyUtilities.Transaction.FormTypeEnum.ImportSX1,

                                    Shift1Name = x.Shift1Name,
                                    Shift2Name = x.Shift2Name,
                                    WarehouseName = "Sản xuất 1"
                                }).ToList();
                    var transactionCodes = list.Select(x => x.TransactionCode).Distinct().ToList();
                    var transactions = vfi.Transactions.Where(
                                            t => transactionCodes.Contains(t.TransactionCode) &&
                                                t.WarehouseReceiptId != null &&
                                                t.Warehouse1.IsProduction &&
                                                (status == 0 || t.Status == status))
                                        .Select(x => new { x.TransactionId, x.TransactionCode, x.Status, x.Warehouse1.WarehouseName })
                                        .ToList();
                    foreach (var entity in list) {
                        entity.Status = MyUtilities.Transaction.CastText.GetTextStatus(entity.StatusInt);
                        var transaction = transactions.FirstOrDefault(t => t.TransactionCode.Equals(entity.TransactionCode));
                        if (transaction != null) {
                            entity.TransactionId = transaction.TransactionId;
                            entity.WarehouseName = transaction.WarehouseName;
                            if (transaction.Status != entity.StatusInt) {
                                entity.Status += " " + MyUtilities.Transaction.CastText.GetTextStatus(transaction.Status);
                            }
                            var productionDate = productionDates.FirstOrDefault(x => x.LockDate == entity.CreatedDate);
                            if (productionDate != null) {
                                entity.Note = " Ngày SX:" + entity.CreatedDate.ToString("dd/MM/yyyy")
                                        + (string.IsNullOrWhiteSpace(entity.Shift1Name) ? "" : " Ca:" + productionDate.Shift1Name)
                                        + (string.IsNullOrWhiteSpace(entity.Shift2Name) ? "" : " Ca:" + productionDate.Shift2Name);
                            }
                            else {
                                entity.Note = " Ngày SX:" + entity.CreatedDate.ToString("dd/MM/yyyy")
                                        + (string.IsNullOrWhiteSpace(entity.Shift1Name) ? "" : " Ca 1:" + entity.Shift1Name)
                                        + (string.IsNullOrWhiteSpace(entity.Shift2Name) ? "" : " Ca 2:" + entity.Shift2Name);
                            }
                        }
                        model.Add(entity);
                    }
                }

                if ((warehouse == null || warehouse.IsCncMilling)
                    && MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.ImportCNC)) {
                    //var importSX1s = vfi.ImportFormCncs.Where(i => i.ImportDate >= fDate && i.ImportDate <= tDate);
                    var list = (from x in vfi.ImportFormCncs
                                where x.ImportDate >= fDate && x.ImportDate <= tDate
                                      select new ManageImportExportModel {
                                          ModifiedUser = x.ModifiedUser,
                                          ModidifiedDate = x.ModifiedDate,
                                          CreatedDate = x.MaterialUseDate,
                                          ReportDate = x.ImportDate,
                                          TransactionCode = x.TransactionCode,
                                          TransactionId = 0,
                                          TotalQuality = x.ImportFormCncDetails.Sum(y => y.Number1 + y.Number2 + y.Processing1 + y.Processing2),
                                          //StatusInt = x.Status,
                                          FormType = (byte)MyUtilities.Transaction.FormTypeEnum.ImportCNC,

                                          Shift1Name = x.Shift1Name,
                                          Shift2Name = x.Shift2Name,
                                          WarehouseName = "Phay CNC"
                                      }).ToList();
                    var transactionCodes = list.Select(x => x.TransactionCode).Distinct().ToList();
                    var transactions = vfi.Transactions.Where(
                                            t => transactionCodes.Contains(t.TransactionCode) &&
                                                t.Warehouse.IsCncMilling &&
                                                (status == 0 || t.Status == status))
                                        .Select(x => new { x.TransactionId, x.Status, x.TransactionCode, x.Warehouse.WarehouseName })
                                        .ToList();
                    foreach (var entity in list) {
                        var transaction = transactions.FirstOrDefault(t => t.TransactionCode.Equals(entity.TransactionCode));
                        if (transaction != null) {
                            entity.TransactionId = transaction.TransactionId;
                            entity.WarehouseName = transaction.WarehouseName;
                            entity.StatusInt = transaction.Status;
                            entity.Status = MyUtilities.Transaction.CastText.GetTextStatus(transaction.Status);
                            var productionDate = productionDates.FirstOrDefault(x => x.LockDate == entity.CreatedDate);
                            if (productionDate != null) {
                                entity.Note = " Ngày SX:" + entity.CreatedDate.ToString("dd/MM/yyyy")
                                        + (string.IsNullOrWhiteSpace(entity.Shift1Name) ? "" : " Ca:" + productionDate.Shift1Name)
                                        + (string.IsNullOrWhiteSpace(entity.Shift2Name) ? "" : " Ca:" + productionDate.Shift2Name);
                            }
                            else {
                                entity.Note = " Ngày SX:" + entity.CreatedDate.ToString("dd/MM/yyyy")
                                        + (string.IsNullOrWhiteSpace(entity.Shift1Name) ? "" : " Ca 1:" + entity.Shift1Name)
                                        + (string.IsNullOrWhiteSpace(entity.Shift2Name) ? "" : " Ca 2:" + entity.Shift2Name);
                            }
                        }
                        model.Add(entity);
                    }
                }
                // function code = Export GCN - NCU
                if ((warehouse == null || warehouse.IsPlating)
                    && MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.ExportPlating)) {
                    var list = (from x in vfi.ExportGCN_NCU
                                where x.ExportDate >= fDate && x.ExportDate <= tDate
                                select new ManageImportExportModel {
                                    ModifiedUser = x.ModifiedUser,
                                    ModidifiedDate = x.ModifiedDate,
                                    CreatedDate = x.ExportDate.Value,
                                    ReportDate = x.ExportDate.Value,
                                    TransactionCode = x.TransactionCode,
                                    TransactionId = 0,
                                    TotalQuality = x.ExportGCN_NCUDetail.Sum(y => y.RealNumber),
                                    //StatusInt = x.Status,
                                    FormType = (byte)MyUtilities.Transaction.FormTypeEnum.ExportGCN_NCU,
                                    Note = "NCC " + x.PlatingForm.Vendor.VendorName,
                                    WarehouseName = "Xuất GCN"
                                }).ToList();
                    var transactionCodes = list.Select(x => x.TransactionCode).Distinct().ToList();
                    var transactions = vfi.Transactions.Where(
                                                t =>
                                                transactionCodes.Contains(t.TransactionCode) &&
                                                t.Warehouse1.IsPlating &&
                                                (status == 0 || t.Status == status))
                                        .Select(x => new { x.TransactionId, x.Status, x.TransactionCode, x.Warehouse1.WarehouseName })
                                        .ToList();

                    foreach (var entity in list) {
                        var transaction = transactions.FirstOrDefault(t => t.TransactionCode.Equals(entity.TransactionCode));
                        if (transaction != null) {
                            entity.TransactionId = transaction.TransactionId;
                            entity.WarehouseName = "Xuất " + transaction.WarehouseName;
                            entity.StatusInt = transaction.Status;
                            entity.Status = MyUtilities.Transaction.CastText.GetTextStatus(transaction.Status);
                        }
                        model.Add(entity);
                    }

                    //var exportGCN_NCUs = vfi.ExportGCN_NCU.Where(i => i.ExportDate >= fDate && i.ExportDate <= tDate);
                    //foreach (var exportGCN_NCU in exportGCN_NCUs) {
                    //    var transaction =
                    //        vfi.Transactions.FirstOrDefault(
                    //            t =>
                    //            t.TransactionCode.Equals(exportGCN_NCU.TransactionCode) &&
                    //            t.WarehouseIssueId == MyUtilities.Warehouse.WaitingPlating &&
                    //            (t.WarehouseReceiptId == MyUtilities.Warehouse.Plating ||
                    //            t.WarehouseReceiptId == MyUtilities.Warehouse.PlatingTest));
                    //    if (transaction != null && exportGCN_NCU.ExportGCN_NCUDetail.Count > 0) {
                    //        var entity = new ManageImportExportModel {
                    //            ModifiedUser = exportGCN_NCU.ModifiedUser,
                    //            ModidifiedDate = exportGCN_NCU.ModifiedDate,
                    //            CreatedDate = exportGCN_NCU.ExportDate.Value,
                    //            ReportDate = exportGCN_NCU.ExportDate.Value,
                    //            TransactionCode = exportGCN_NCU.TransactionCode,
                    //            TransactionId = transaction.TransactionId,
                    //            TotalQuality =
                    //                transaction.TransactionDetails.Where(td => td.TransactionId == transaction.TransactionId)
                    //                   .Sum(td => td.Quantity),
                    //            Status =
                    //                MyUtilities.Transaction.CastText.GetTextStatus(
                    //                    transaction.Status),
                    //            FormType = (byte)MyUtilities.Transaction.FormTypeEnum.ExportGCN_NCU,
                    //            Note = "Xuất gia công ngoài - " + transaction.Warehouse1.WarehouseName + " - " + exportGCN_NCU.PlatingForm.Vendor.VendorName
                    //        };
                    //        model.Add(entity);
                    //    }
                    //}
                }

                // function code = Import NCU - QCB
                if ((warehouse == null || warehouse.IsPlating)
                    && MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.ImportPlating)) {

                    var list = (from x in vfi.ImportNCU_QCB
                                where x.ImportDate >= fDate && x.ImportDate <= tDate
                                select new ManageImportExportModel {
                                    ModifiedUser = x.ModifiedUser,
                                    ModidifiedDate = x.ModifiedDate,
                                    CreatedDate = x.ImportDate,
                                    ReportDate = x.ImportDate,
                                    TransactionCode = x.TransactionCode,
                                    TransactionId = 0,
                                    TotalQuality = x.ImportNCU_QCBDetail.Sum(y => y.RealNumber),
                                    //StatusInt = x.Status,
                                    FormType = (byte)MyUtilities.Transaction.FormTypeEnum.ImportNCU_QCB,
                                    Note = "NCC " + x.PlatingForm.Vendor.VendorName,
                                    WarehouseName = "Nhập GCN"
                                }).ToList();
                    var transactionCodes = list.Select(x => x.TransactionCode).Distinct().ToList();
                    var transactions = vfi.Transactions.Where(
                                                t =>
                                                transactionCodes.Contains(t.TransactionCode) &&
                                                t.Warehouse.IsPlating &&
                                                (status == 0 || t.Status == status))
                                        .Select(x => new { x.TransactionId, x.Status, x.TransactionCode, x.Warehouse.WarehouseName })
                                        .ToList();

                    foreach (var entity in list) {
                        var transaction = transactions.FirstOrDefault(t => t.TransactionCode.Equals(entity.TransactionCode));
                        if (transaction != null) {
                            entity.TransactionId = transaction.TransactionId;
                            entity.WarehouseName = "Nhập " + transaction.WarehouseName;
                            entity.StatusInt = transaction.Status;
                            entity.Status = MyUtilities.Transaction.CastText.GetTextStatus(transaction.Status);
                        }
                        model.Add(entity);
                    }

                    //var importNCU_QCBs = vfi.ImportNCU_QCB.Where(i => i.ImportDate >= fDate && i.ImportDate <= tDate);
                    //foreach (var importNCU_QCB in importNCU_QCBs) {
                    //    var transaction =
                    //        vfi.Transactions.FirstOrDefault(
                    //            t =>
                    //                t.TransactionCode.Equals(importNCU_QCB.TransactionCode) &&
                    //                (t.WarehouseIssueId == MyUtilities.Warehouse.Plating ||
                    //                 t.WarehouseIssueId == MyUtilities.Warehouse.PlatingTest) &&
                    //                t.WarehouseReceiptId == MyUtilities.Warehouse.QcB);
                    //    if (transaction != null && importNCU_QCB.ImportNCU_QCBDetail.Count > 0) {
                    //        var entity = new ManageImportExportModel {
                    //            ModifiedUser = importNCU_QCB.ModifiedUser,
                    //            ModidifiedDate = importNCU_QCB.ModifiedDate,
                    //            CreatedDate = importNCU_QCB.ImportDate,
                    //            ReportDate = importNCU_QCB.ImportDate,
                    //            TransactionCode = importNCU_QCB.TransactionCode,
                    //            TransactionId = transaction.TransactionId,
                    //            TotalQuality =
                    //                transaction.TransactionDetails.Where(td => td.TransactionId == transaction.TransactionId)
                    //                   .Sum(td => td.Quantity),
                    //            Status =
                    //                    MyUtilities.Transaction.CastText.GetTextStatus(
                    //                        transaction.Status),
                    //            FormType = (byte)MyUtilities.Transaction.FormTypeEnum.ImportNCU_QCB,
                    //            Note = "Nhập gia công ngoài - " + transaction.Warehouse.WarehouseName + " - " + importNCU_QCB.PlatingForm.Vendor.VendorName
                    //        };
                    //        model.Add(entity);
                    //    }
                    //}
                }

                // function code = export tp - kd
                if ((warehouse == null || warehouse.IsFinish)
                    && MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.ExportFinish)) {

                    var list = (from x in vfi.ExportFormTP_KD
                                where x.DateCreate >= fDate && x.DateCreate <= tDate
                                select new ManageImportExportModel {
                                    ModifiedUser = x.ModifiedUser,
                                    ModidifiedDate = x.ModifiedDate,
                                    CreatedDate = x.DateCreate.Value,
                                    ReportDate = x.DateCreate.Value,
                                    TransactionCode = x.TransactionCode,
                                    TransactionId = 0,
                                    TotalQuality = x.ExportFormTP_KDDetail.Sum(y => y.Quality),
                                    Box = x.TotalBox,
                                    FormType = (byte)MyUtilities.Transaction.FormTypeEnum.ExportTP,
                                    Note = "KH: " + x.Customer.CustomerCode,
                                    WarehouseName = "Thành phẩm"
                                }).ToList();
                    var transactionCodes = list.Select(x => x.TransactionCode).Distinct().ToList();
                    var transactions = vfi.Transactions.Where(
                                                t =>
                                                transactionCodes.Contains(t.TransactionCode) &&
                                                t.Warehouse.IsFinish &&
                                                (status == 0 || t.Status == status))
                                        .Select(x => new { x.TransactionId, x.Status, x.TransactionCode, x.Warehouse.WarehouseName })
                                        .ToList();

                    foreach (var entity in list) {
                        var transaction = transactions.FirstOrDefault(t => t.TransactionCode.Equals(entity.TransactionCode));
                        if (transaction != null) {
                            entity.Note += "- Số thùng: " + entity.Box;
                            entity.TransactionId = transaction.TransactionId;
                            entity.WarehouseName = transaction.WarehouseName;
                            entity.StatusInt = transaction.Status;
                            entity.Status = MyUtilities.Transaction.CastText.GetTextStatus(transaction.Status);
                        }
                        model.Add(entity);
                    }

                    //var exportTP_KDs = vfi.ExportFormTP_KD.Where(i => i.DateCreate >= fDate && i.DateCreate <= tDate);
                    //foreach (var export in exportTP_KDs) {
                    //    var transaction =
                    //        vfi.Transactions.FirstOrDefault(
                    //            t =>
                    //            t.TransactionCode.Equals(export.TransactionCode) && t.WarehouseIssueId == (byte)MyUtilities.Warehouse.Finish &&
                    //            t.WarehouseReceiptId == (byte)MyUtilities.Warehouse.Business);
                    //    if (transaction != null && export.ExportFormTP_KDDetail.Count > 0) {
                    //        var entity = new ManageImportExportModel {
                    //            ModifiedUser = export.ModifiedUser,
                    //            ModidifiedDate = export.ModifiedDate,
                    //            CreatedDate = export.DateTransporter.Value,
                    //            ReportDate = export.DateTransporter.Value,
                    //            TransactionCode = export.TransactionCode,
                    //            TransactionId = transaction.TransactionId,
                    //            //TotalQuality =
                    //            //    vfi.TransactionDetails.Where(td => td.TransactionId == transaction.TransactionId)
                    //            //       .Sum(td => td.Quantity),
                    //            Status =
                    //                MyUtilities.Transaction.CastText.GetTextStatus(
                    //                    transaction.Status),
                    //            FormType = (byte)MyUtilities.Transaction.FormTypeEnum.ExportTP,
                    //            Note = "Xuất thành phẩm - Số thùng: " + export.TotalBox + " - " + export.Customer.CustomerCode
                    //        };
                    //        if (transaction.TransactionDetails.Any()) {
                    //            entity.TotalQuality = transaction.TransactionDetails
                    //                .Where(td => td.TransactionId == transaction.TransactionId)
                    //                .Sum(td => td.Quantity);
                    //        }
                    //        else if (transaction.TransactionProducts.Any()) {
                    //            entity.TotalQuality = transaction.TransactionProducts
                    //                .Where(td => td.TransactionId == transaction.TransactionId)
                    //                .Sum(td => td.Quantity);
                    //        }
                    //        model.Add(entity);
                    //    }
                    //}
                }


                //if (!string.IsNullOrWhiteSpace(transactionId))
                //    models = models.Where(ef => ef.TransactionCode.Contains(transactionId)).ToList();

                return View(new GridModel(model.OrderByDescending(m => m.CreatedDate)));
                //return View(new GridModel(new List<ExportForm>()));
            }
            //return View(new GridModel(new List<ExportForm>()));
        }

        [GridAction]
        public ActionResult SelectFormDetailByTransactionId(long transactionId, int formType) {
            var models = new List<ManageImportExportDetailModel>();
            try {
                models = GetListFormDetailByTransactionId(transactionId, formType);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectFormDetailByTransactionId", ex.Message);
            }
            return View(new GridModel(models.OrderBy(m => m.ProductCode)));
        }

        List<ManageImportExportDetailModel> GetListFormDetailByTransactionId(long transactionId, int formType) {

            var models = new List<ManageImportExportDetailModel>();
            //return View(new GridModel(models));
            using (var vfi = new tammaContext()) {
                var isInvManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.InvManagement);

                switch (formType) {
                    case (byte)MyUtilities.Transaction.FormTypeEnum.ImportSX1: {
                            var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId.Equals(transactionId));
                            var import =
                                vfi.ImportFormSX1.FirstOrDefault(i => i.TransactionCode.Equals(transaction.TransactionCode));
                            var importDetails = vfi.ImportFormSX1Detail.Where(id => id.ImportId == import.ImportId);
                            foreach (var detail in importDetails) {
                                var model = new ManageImportExportDetailModel {
                                    DetailId = detail.DetailId,
                                    ProductId = detail.ProductId,
                                    ProductCode = detail.Product.ProductCode,
                                    Number = (detail.Number1 + detail.Number2),
                                    Note = "Máy chạy:" + detail.Machine,
                                    CanEdit = false,
                                    TransactionId = transactionId,
                                    FormType = formType,
                                };
                                model.Weight = model.Number * detail.ProductWeight;
                                if (model.Number > 0) models.Add(model);
                            }
                        }
                        break;
                    case (byte)MyUtilities.Transaction.FormTypeEnum.ImportCNC: {
                            var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId.Equals(transactionId));
                            var import =
                                vfi.ImportFormCncs.FirstOrDefault(i => i.TransactionCode.Equals(transaction.TransactionCode));
                            var importDetails = vfi.ImportFormCncDetails.Where(id => id.ImportId == import.ImportId);
                            foreach (var detail in importDetails) {
                                var model = new ManageImportExportDetailModel {
                                    DetailId = detail.DetailId,
                                    ProductId = detail.ProductId,
                                    ProductCode = detail.Product.ProductCode,
                                    Number = (detail.Number1 + detail.Number2),
                                    Note = "Máy chạy:" + detail.Machine.MachineName,
                                    CanEdit = false,
                                    TransactionId = transactionId,
                                    FormType = formType,
                                };
                                model.Weight = model.Number * detail.ProductWeight;
                                if (model.Number > 0) models.Add(model);
                            }
                        }
                        break;
                    case (byte)MyUtilities.Transaction.FormTypeEnum.ExportGCN_NCU: {
                            var exportPlating = vfi.ExportGCN_NCU.FirstOrDefault(t => t.TransactionId == transactionId);
                            if (exportPlating == null) break;
                            foreach (var detail in exportPlating.ExportGCN_NCUDetail) {
                                var entity = models.FirstOrDefault(m => m.ProductId == detail.ProductId &&
                                    detail.PlatingDetailId == m.PlatingDetailId);
                                if (entity == null) {
                                    entity = new ManageImportExportDetailModel {
                                        DetailId = detail.DetailId,
                                        ProductId = detail.ProductId,
                                        ProductCode = detail.Product.ProductCode,
                                        Number = (detail.RealNumber),
                                        Weight = (detail.Weight) ?? 0,
                                        Note = detail.Note,
                                        Package = detail.Package,
                                        CanEdit = false,
                                        TransactionId = transactionId,
                                        FormType = formType,
                                        PlatingDetailId = detail.PlatingDetailId
                                    };
                                    if (entity.Number > 0) models.Add(entity);
                                }
                                else {
                                    entity.Number += detail.RealNumber;
                                    entity.Weight += (detail.Weight) ?? 0;
                                    entity.Note += (detail.Note + " ");
                                }
                            }
                        }
                        break;
                    case (byte)MyUtilities.Transaction.FormTypeEnum.ImportNCU_QCB: {
                            var importPlating = vfi.ImportNCU_QCB.FirstOrDefault(t => t.TransactionId == transactionId);
                            if (importPlating == null) break;
                            foreach (var detail in importPlating.ImportNCU_QCBDetail) {
                                var entity = models.FirstOrDefault(m => m.ProductId == detail.ProductId &&
                                    detail.ExportGCN_NCUDetail.PlatingDetailId == m.PlatingDetailId);
                                if (entity == null) {
                                    entity = new ManageImportExportDetailModel {
                                        DetailId = detail.DetailId,
                                        ProductId = detail.ProductId,
                                        ProductCode = detail.Product.ProductCode,
                                        Number = (detail.RealNumber),
                                        Weight = (detail.Weight),
                                        Note = detail.Note,
                                        Package = detail.Package,
                                        CanEdit = false,
                                        TransactionId = transactionId,
                                        FormType = formType,
                                        PlatingDetailId = detail.ExportGCN_NCUDetail.PlatingDetailId
                                    };
                                    if (entity.Number > 0) models.Add(entity);
                                }
                                else {
                                    entity.Number += detail.RealNumber;
                                    entity.Weight += (detail.Weight);
                                    entity.Note += (detail.Note + " ");
                                }
                            }
                        }
                        break;
                    case (byte)MyUtilities.Transaction.FormTypeEnum.ExportTP: {
                            var transaction10 = vfi.Transactions.FirstOrDefault(t => t.TransactionId.Equals(transactionId));
                            var export =
                                vfi.ExportFormTP_KD.FirstOrDefault(
                                    e => e.TransactionCode.Equals(transaction10.TransactionCode));
                            foreach (var detail in export.ExportFormTP_KDDetail) {
                                var model = new ManageImportExportDetailModel {
                                    DetailId = detail.DetailId,
                                    ProductId = detail.ProductId ?? 0,
                                    ProductCode = detail.Product.ProductCode,
                                    Number = (detail.Quality),
                                    Note = detail.Note,
                                    CanEdit = isInvManager,
                                    TransactionId = transactionId,
                                    FormType = formType,
                                    Weight = detail.Weight.Value / 1000
                                };
                                if (model.Number > 0) models.Add(model);
                            }
                        }
                        break;
                    case (byte)MyUtilities.Transaction.FormTypeEnum.ExportChange: {
                            var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId.Equals(transactionId));
                            var exportChange =
                                 vfi.ExportFormTP_KD.FirstOrDefault(
                                     e => e.TransactionCode.Equals(transaction.TransactionCode));
                            foreach (var detail in exportChange.ExportFormTP_KDDetail) {
                                var model = new ManageImportExportDetailModel {
                                    DetailId = detail.DetailId,
                                    ProductId = detail.ProductId ?? 0,
                                    ProductCode = detail.Product.ProductCode,
                                    Number = (detail.Quality),
                                    Note = detail.Note,
                                    CanEdit = false,
                                    TransactionId = transactionId,
                                    FormType = formType,
                                    Weight = detail.Weight.Value / 1000
                                };
                                if (model.Number > 0) models.Add(model);
                            }
                        }
                        break;
                }
            }
            return models.OrderBy(m => m.ProductCode).ToList();
        }


        [GridAction]
        public ActionResult UpdateExportPlatingDetail(ManageImportExportDetailModel update) {
            try {
                using (var vfi = new tammaContext()) {
                    var detail = vfi.ExportGCN_NCUDetail.FirstOrDefault(pfd => pfd.DetailId == update.DetailId);
                    if (detail == null) {
                        throw new AggregateException("Lỗi! Không tìm thấy chi tiết cần sửa!");
                    }
                    detail.Package = update.Package;
                    update.TransactionId = detail.ExportGCN_NCU.TransactionId ?? 0;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateExportPlatingDetail", ex.Message);
            }
            return View(new GridModel(GetListFormDetailByTransactionId(update.TransactionId, (int)MyUtilities.Transaction.FormTypeEnum.ExportGCN_NCU)));
        }
        
        [GridAction]
        public ActionResult UpdateImportPlatingDetail(ManageImportExportDetailModel update) {
            try {
                using (var vfi = new tammaContext()) {
                    var detail = vfi.ImportNCU_QCBDetail.FirstOrDefault(pfd => pfd.DetailId == update.DetailId);
                    if (detail == null) {
                        throw new AggregateException("Lỗi! Không tìm thấy chi tiết cần sửa!");
                    }
                    detail.Package = update.Package;
                    update.TransactionId = detail.ImportNCU_QCB.TransactionId ?? 0;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateExportPlatingDetail", ex.Message);
            }
            return View(new GridModel(GetListFormDetailByTransactionId(update.TransactionId, (int)MyUtilities.Transaction.FormTypeEnum.ImportNCU_QCB)));
        }

        [GridAction]
        public ActionResult SelectFormDetailLotNumberByTransactionId(long transactionId, int formType) {
            var models = new List<ManageImportExportDetailModel>();
            try {
                models = GetListFormDetailLotNumberByTransactionId(transactionId, formType);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectFormDetailByTransactionId", ex.Message);
            }
            return View(new GridModel(models.OrderBy(m => m.ProductCode)));
        }

        List<ManageImportExportDetailModel> GetListFormDetailLotNumberByTransactionId(long transactionId, int formType) {

            var models = new List<ManageImportExportDetailModel>();
            //return View(new GridModel(models));
            using (var vfi = new tammaContext()) {
                var isInvManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.InvManagement);

                switch (formType) {
                    case (byte)MyUtilities.Transaction.FormTypeEnum.ImportSX1: {
                            var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId.Equals(transactionId));
                            var import =
                                vfi.ImportFormSX1.FirstOrDefault(i => i.TransactionCode.Equals(transaction.TransactionCode));
                            var importDetails = vfi.ImportFormSX1Detail.Where(id => id.ImportId == import.ImportId);
                            foreach (var detail in importDetails) {
                                var entity = new ManageImportExportDetailModel {
                                    DetailId = detail.DetailId,
                                    ProductId = detail.ProductId,
                                    ProductCode = detail.Product.ProductCode,
                                    Number = (detail.Number1 + detail.Number2),
                                    Note = "Máy chạy:" + detail.Machine,
                                    CanEdit = false,
                                    TransactionId = transactionId,
                                    FormType = formType,
                                    LotNumber = detail.LotNumber,
                                };
                                entity.Weight = entity.Number * detail.ProductWeight;
                                if (entity.Number > 0) models.Add(entity);
                            }
                        }
                        break;
                    case (byte)MyUtilities.Transaction.FormTypeEnum.ImportCNC: {
                            var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId.Equals(transactionId));
                            var import =
                                vfi.ImportFormCncs.FirstOrDefault(i => i.TransactionCode.Equals(transaction.TransactionCode));
                            var importDetails = vfi.ImportFormCncDetails.Where(id => id.ImportId == import.ImportId);
                            foreach (var detail in importDetails) {
                                var entity = new ManageImportExportDetailModel {
                                    DetailId = detail.DetailId,
                                    ProductId = detail.ProductId,
                                    ProductCode = detail.Product.ProductCode,
                                    Number = (detail.Number1 + detail.Number2),
                                    Note = "Máy chạy:" + detail.Machine.MachineName,
                                    CanEdit = false,
                                    TransactionId = transactionId,
                                    FormType = formType,
                                    
                                };
                                if (detail.ProductInventory != null) {
                                    entity.LotNumber = detail.ProductInventory.LotNumber;
                                }
                                entity.Weight = entity.Number * detail.ProductWeight;
                                if (entity.Number > 0) models.Add(entity);
                            }
                        }
                        break;
                    case (byte)MyUtilities.Transaction.FormTypeEnum.ExportGCN_NCU: {
                            var exportPlating = vfi.ExportGCN_NCU.FirstOrDefault(t => t.TransactionId == transactionId);
                            if (exportPlating == null) break;
                            foreach (var detail in exportPlating.ExportGCN_NCUDetail) {
                                var entity = models.FirstOrDefault(m => m.ProductId == detail.ProductId &&
                                    detail.PlatingDetailId == m.PlatingDetailId);
                                if (entity == null) {
                                    entity = new ManageImportExportDetailModel {
                                        DetailId = detail.DetailId,
                                        ProductId = detail.ProductId,
                                        ProductCode = detail.Product.ProductCode,
                                        Number = (detail.RealNumber),
                                        Weight = (detail.Weight) ?? 0,
                                        Note = detail.Note,
                                        Package = detail.Package,
                                        CanEdit = false,
                                        TransactionId = transactionId,
                                        FormType = formType,
                                        PlatingDetailId = detail.PlatingDetailId
                                    };
                                    if (detail.ProductInventory != null) {
                                        entity.LotNumber = detail.ProductInventory.LotNumber;
                                    }
                                    if (entity.Number > 0) models.Add(entity);
                                }
                                else {
                                    entity.Number += detail.RealNumber;
                                    entity.Weight += (detail.Weight) ?? 0;
                                    entity.Note += (detail.Note + " ");
                                }
                            }
                        }
                        break;
                    case (byte)MyUtilities.Transaction.FormTypeEnum.ImportNCU_QCB: {
                            var importPlating = vfi.ImportNCU_QCB.FirstOrDefault(t => t.TransactionId == transactionId);
                            if (importPlating == null) break;
                            foreach (var detail in importPlating.ImportNCU_QCBDetail) {
                                var entity = models.FirstOrDefault(m => m.ProductId == detail.ProductId &&
                                    detail.ExportGCN_NCUDetail.PlatingDetailId == m.PlatingDetailId);
                                if (entity == null) {
                                    entity = new ManageImportExportDetailModel {
                                        DetailId = detail.DetailId,
                                        ProductId = detail.ProductId,
                                        ProductCode = detail.Product.ProductCode,
                                        Number = (detail.RealNumber),
                                        Weight = (detail.Weight),
                                        Note = detail.Note,
                                        Package = detail.Package,
                                        CanEdit = false,
                                        TransactionId = transactionId,
                                        FormType = formType,
                                        PlatingDetailId = detail.ExportGCN_NCUDetail.PlatingDetailId
                                    };
                                    if (detail.ProductInventory != null) {
                                        entity.LotNumber = detail.ProductInventory.LotNumber;
                                    }
                                    if (entity.Number > 0) models.Add(entity);
                                }
                                else {
                                    entity.Number += detail.RealNumber;
                                    entity.Weight += (detail.Weight);
                                    entity.Note += (detail.Note + " ");
                                }
                            }
                        }
                        break;
                    case (byte)MyUtilities.Transaction.FormTypeEnum.ExportTP: {
                            var transaction10 = vfi.Transactions.FirstOrDefault(t => t.TransactionId.Equals(transactionId));
                            var export =
                                vfi.ExportFormTP_KD.FirstOrDefault(
                                    e => e.TransactionCode.Equals(transaction10.TransactionCode));
                            var exportDetails = vfi.TransactionProducts.Where(x => x.TransactionId == transactionId);
                            foreach (var detail in transaction10.TransactionDetails) {
                                var model = new ManageImportExportDetailModel {
                                    DetailId = detail.TransactionDetailId,
                                    ProductId = detail.ReferenceId ?? 0,
                                    ProductCode = detail.Product.ProductCode,
                                    Number = (detail.Quantity),
                                    Weight = (detail.QuantityKg ?? 0),
                                    Note = detail.Note,
                                    CanEdit = isInvManager,
                                    TransactionId = transactionId,
                                    FormType = formType,
                                    LotNumber = detail.LotNumber
                                };
                                if (model.Number > 0) models.Add(model);
                            }
                        }
                        break;
                    case (byte)MyUtilities.Transaction.FormTypeEnum.ExportChange: {
                            var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId.Equals(transactionId));
                            var exportChange =
                                 vfi.ExportFormTP_KD.FirstOrDefault(
                                     e => e.TransactionCode.Equals(transaction.TransactionCode));
                            foreach (var detail in transaction.TransactionDetails) {
                                var model = new ManageImportExportDetailModel {
                                    DetailId = detail.TransactionDetailId,
                                    ProductId = detail.ReferenceId ?? 0,
                                    ProductCode = detail.Product.ProductCode,
                                    Number = (detail.Quantity),
                                    Weight = (detail.QuantityKg ?? 0),
                                    Note = detail.Note,
                                    CanEdit = isInvManager,
                                    TransactionId = transactionId,
                                    FormType = formType,
                                    LotNumber = detail.LotNumber
                                };
                                if (model.Number > 0) models.Add(model);
                            }
                        }
                        break;
                }
            }
            return models.OrderBy(m => m.ProductCode).ToList();
        }

        [GridAction]
        public ActionResult UpdateFormDetailByTransactionId(ManageImportExportDetailModel update) {
            //var models = new List<ManageImportExportDetailModel>();
            //return View(new GridModel(models));

            try {

                using (var vfi = new tammaContext()) {
                    var isInvManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.InvManagement);

                    if (!isInvManager)
                        throw new AggregateException("Lỗi! Không có quyền sửa sản phẩm!");
                    switch (update.FormType) {
                        case (byte)MyUtilities.Transaction.FormTypeEnum.ImportSX1:
                            throw new AggregateException("Chưa hổ trợ phần này");
                        case (byte)MyUtilities.Transaction.FormTypeEnum.ExportGCN_NCU:
                            throw new AggregateException("Chưa hổ trợ phần này");
                        case (byte)MyUtilities.Transaction.FormTypeEnum.ImportNCU_QCB:
                            throw new AggregateException("Chưa hổ trợ phần này");
                        case (byte)MyUtilities.Transaction.FormTypeEnum.ExportTP:
                            throw new AggregateException("Chưa hổ trợ phần này"); 
                            {
                                var exportDetail =
                                    vfi.ExportFormTP_KDDetail.FirstOrDefault(ed => ed.DetailId == update.DetailId);
                                if (exportDetail == null)
                                    throw new AggregateException("Lỗi! Không tìm thấy chi tiết");
                                if (exportDetail.InvoiceDetails.Any(id => id.Active))
                                    throw new AggregateException(
                                        "Lỗi! Chi tiết xuất đã phân đơn hàng! Hủy phân đơn hàng trước.");
                                var newProductId = 0;
                                try {
                                    newProductId = Convert.ToInt32(update.ProductCode);
                                }
                                catch (FormatException) {
                                    newProductId =
                                        vfi.Products.FirstOrDefault(p => p.ProductCode.Equals(update.ProductCode))
                                           .ProductId;
                                }
                                if (newProductId != 0 && newProductId != exportDetail.ProductId) {
                                    var newProduct = vfi.Products.FirstOrDefault(p => p.ProductId == newProductId);
                                    var oldInventoryIssue =
                                        vfi.ProductInventories.FirstOrDefault(
                                            pi =>
                                            pi.WarehouseId == MyUtilities.Warehouse.Finish &&
                                            pi.ProductId == exportDetail.ProductId);
                                    var oldInventoryReceipt =
                                        vfi.ProductInventories.FirstOrDefault(
                                            pi =>
                                            pi.WarehouseId == MyUtilities.Warehouse.Business &&
                                            pi.ProductId == exportDetail.ProductId);
                                    var newInventoryIssue =
                                        vfi.ProductInventories.FirstOrDefault(
                                            pi =>
                                            pi.WarehouseId == MyUtilities.Warehouse.Finish && pi.ProductId == newProductId);
                                    var newInventoryReceipt =
                                        vfi.ProductInventories.FirstOrDefault(
                                            pi =>
                                            pi.WarehouseId == MyUtilities.Warehouse.Business &&
                                            pi.ProductId == newProductId);
                                    if (newInventoryReceipt == null) {
                                        newInventoryReceipt = new ProductInventory {
                                            ProductId = newProductId,
                                            Active = true,
                                            TotalQty = 0,
                                            ModifiedDate = DateTime.Now,
                                            ModifiedUser = HttpContext.User.Identity.Name,
                                            WarehouseId = MyUtilities.Warehouse.Business,
                                        };
                                        vfi.ProductInventories.Add(newInventoryReceipt);
                                    }
                                    //
                                    var productPeriodIssue =
                                        vfi.ProductInventoryPeriods.FirstOrDefault(
                                            pip =>
                                            pip.ProductId == exportDetail.ProductId &&
                                            pip.WarehouseId == MyUtilities.Warehouse.Finish &&
                                            pip.TransactionId == update.TransactionId);
                                    productPeriodIssue.ProductId = newProductId;
                                    productPeriodIssue.EarlyPeriodQuantity = newInventoryIssue.TotalQty;
                                    productPeriodIssue.LastPeriodQuantity = productPeriodIssue.EarlyPeriodQuantity -
                                                                            productPeriodIssue.Quantity;
                                    oldInventoryIssue.TotalQty += productPeriodIssue.Quantity;
                                    newInventoryIssue.TotalQty -= productPeriodIssue.Quantity;
                                    //
                                    var productPeriodReceipt =
                                        vfi.ProductInventoryPeriods.FirstOrDefault(
                                            pip =>
                                            pip.ProductId == exportDetail.ProductId &&
                                            pip.WarehouseId == MyUtilities.Warehouse.Business &&
                                            pip.TransactionId == update.TransactionId);
                                    productPeriodReceipt.ProductId = newProductId;
                                    productPeriodReceipt.EarlyPeriodQuantity = newInventoryReceipt.TotalQty;
                                    productPeriodReceipt.LastPeriodQuantity = productPeriodReceipt.EarlyPeriodQuantity +
                                                                              productPeriodReceipt.Quantity;
                                    oldInventoryReceipt.TotalQty -= productPeriodIssue.Quantity;
                                    newInventoryReceipt.TotalQty += productPeriodIssue.Quantity;
                                    //
                                    var transactionDetail =
                                        vfi.TransactionDetails.FirstOrDefault(
                                            td =>
                                            td.TransactionId == update.TransactionId &&
                                            td.TransactionDetailId == exportDetail.TransactionDetailId);
                                    transactionDetail.ReferenceId = newProductId;
                                    transactionDetail.Note += "Đổi mã xuất:" + exportDetail.Product.ProductCode + "->" +
                                                              newProduct.ProductCode;
                                    exportDetail.ProductId = newProductId;
                                    if (exportDetail.InvoiceDetails.Any())
                                        vfi.InvoiceDetails.RemoveRange(exportDetail.InvoiceDetails);
                                    vfi.SaveChanges();
                                }
                            }
                            break;
                        case (byte)MyUtilities.Transaction.FormTypeEnum.ExportChange:
                            throw new AggregateException("Chưa hổ trợ phần này");
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateFormDetailByTransactionId", ex.Message);
            }
            return View(new GridModel(GetListFormDetailByTransactionId(update.TransactionId, update.FormType)));
        }

        public int UpdateExportFinishForm(ManageImportExportDetailModel update) {
            var saved = 0;
            using (var vfi = new tammaContext()) {
                var exportDetail = vfi.ExportFormTP_KDDetail.FirstOrDefault(ed => ed.DetailId == update.DetailId);
                if (exportDetail == null)
                    throw new AggregateException("Lỗi! Không tìm thấy chi tiết");
                if (exportDetail.InvoiceDetails.Any(id => id.Active))
                    throw new AggregateException(
                        "Lỗi! Chi tiết xuất đã phân đơn hàng! Hủy phân đơn hàng trước.");
                if (update.Number != exportDetail.Quality) {
                    var transaction = vfi.Transactions.FirstOrDefault(x => x.TransactionCode.Equals(exportDetail.ExportFormTP_KD.TransactionCode));
                    if (transaction == null) return 0;
                    var transactionProducts = transaction.TransactionProducts.Where(x => x.ProductId == exportDetail.ProductId);
                    var transactionDetails = transaction.TransactionDetails.Where(x => x.ReferenceId == exportDetail.ProductId);
                }

                //var newProductId = 0;
                //try {
                //    newProductId = Convert.ToInt32(update.ProductCode);
                //}
                //catch (FormatException) {
                //    newProductId = vfi.Products.FirstOrDefault(p => p.ProductCode.Equals(update.ProductCode)).ProductId;
                //}

                //if (newProductId != 0 && newProductId != exportDetail.ProductId) {
                //    var newProduct = vfi.Products.FirstOrDefault(p => p.ProductId == newProductId);
                //    var oldInventoryIssue =
                //        vfi.ProductInventories.FirstOrDefault(
                //            pi =>
                //            pi.WarehouseId == MyUtilities.Warehouse.Finish &&
                //            pi.ProductId == exportDetail.ProductId);
                //    var oldInventoryReceipt =
                //        vfi.ProductInventories.FirstOrDefault(
                //            pi =>
                //            pi.WarehouseId == MyUtilities.Warehouse.Business &&
                //            pi.ProductId == exportDetail.ProductId);
                //    var newInventoryIssue =
                //        vfi.ProductInventories.FirstOrDefault(
                //            pi =>
                //            pi.WarehouseId == MyUtilities.Warehouse.Finish && pi.ProductId == newProductId);
                //    var newInventoryReceipt =
                //        vfi.ProductInventories.FirstOrDefault(
                //            pi =>
                //            pi.WarehouseId == MyUtilities.Warehouse.Business &&
                //            pi.ProductId == newProductId);
                //    if (newInventoryReceipt == null) {
                //        newInventoryReceipt = new ProductInventory {
                //            ProductId = newProductId,
                //            Active = true,
                //            TotalQty = 0,
                //            ModifiedDate = DateTime.Now,
                //            ModifiedUser = HttpContext.User.Identity.Name,
                //            WarehouseId = MyUtilities.Warehouse.Business,
                //        };
                //        vfi.ProductInventories.Add(newInventoryReceipt);
                //    }
                //    //
                //    var productPeriodIssue =
                //        vfi.ProductInventoryPeriods.FirstOrDefault(
                //            pip =>
                //            pip.ProductId == exportDetail.ProductId &&
                //            pip.WarehouseId == MyUtilities.Warehouse.Finish &&
                //            pip.TransactionId == update.TransactionId);
                //    productPeriodIssue.ProductId = newProductId;
                //    productPeriodIssue.EarlyPeriodQuantity = newInventoryIssue.TotalQty;
                //    productPeriodIssue.LastPeriodQuantity = productPeriodIssue.EarlyPeriodQuantity -
                //                                            productPeriodIssue.Quantity;
                //    oldInventoryIssue.TotalQty += productPeriodIssue.Quantity;
                //    newInventoryIssue.TotalQty -= productPeriodIssue.Quantity;
                //    //
                //    var productPeriodReceipt =
                //        vfi.ProductInventoryPeriods.FirstOrDefault(
                //            pip =>
                //            pip.ProductId == exportDetail.ProductId &&
                //            pip.WarehouseId == MyUtilities.Warehouse.Business &&
                //            pip.TransactionId == update.TransactionId);
                //    productPeriodReceipt.ProductId = newProductId;
                //    productPeriodReceipt.EarlyPeriodQuantity = newInventoryReceipt.TotalQty;
                //    productPeriodReceipt.LastPeriodQuantity = productPeriodReceipt.EarlyPeriodQuantity +
                //                                              productPeriodReceipt.Quantity;
                //    oldInventoryReceipt.TotalQty -= productPeriodIssue.Quantity;
                //    newInventoryReceipt.TotalQty += productPeriodIssue.Quantity;
                //    //
                //    var transactionDetail =
                //        vfi.TransactionDetails.FirstOrDefault(
                //            td =>
                //            td.TransactionId == update.TransactionId &&
                //            td.TransactionDetailId == exportDetail.TransactionDetailId);
                //    transactionDetail.ReferenceId = newProductId;
                //    transactionDetail.Note += "Đổi mã xuất:" + exportDetail.Product.ProductCode + "->" +
                //                              newProduct.ProductCode;
                //    exportDetail.ProductId = newProductId;
                //    if (exportDetail.InvoiceDetails.Any())
                //        vfi.InvoiceDetails.RemoveRange(exportDetail.InvoiceDetails);
                //    vfi.SaveChanges();
                //}
            }
            return saved;
        }

        [GridAction]
        public ActionResult SelectFormDetailByTransactionCode_test() {
            var models = new List<ManageImportExportDetailModel>();
            return View(new GridModel(models));
        }

        [GridAction]
        public ActionResult ProductExportTP(
            [Bind(Prefix = "inserted")] IEnumerable<ExportFormTP_KDDetailsModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<ExportFormTP_KDDetailsModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<ExportFormTP_KDDetailsModel> deletedDetails
            , string transactionCode
            , int? id, string transporter
            , string companyTransporter, string carNumber
            , string transportDate
            , int totalBox
            , int type
            ) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode", "Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<ExportFormTP_KDDetailsModel>()));
            }
            if (updatedDetails != null) {
                try {
                    using (var vfi = new tammaContext()) {
                        var ci = new CultureInfo("vi-VN");

                        var tDate = string.IsNullOrWhiteSpace(transportDate)
                                        ? DateTime.Today
                                        : Convert.ToDateTime(transportDate, ci);
                        //xuat ban
                        if (type == 1) {
                            var order = vfi.Orders.FirstOrDefault(o => o.OrderId == id);
                            var transaction = new Vfi.Models.Transaction {
                                WarehouseIssueId = MyUtilities.Warehouse.Finish,
                                WarehouseReceiptId = MyUtilities.Warehouse.Business,

                                TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                                EoI = "2",
                                MoP = false,
                                CreatedUser = HttpContext.User.Identity.Name,
                                CreatedDate = tDate,
                                Status = (byte)MyUtilities.Transaction.Status.Open,
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                            };
                            var productExport = new ExportFormTP_KD() {
                                CustomerId = order.CustomerId,
                                Transporter = transporter,
                                TransactionCode = transaction.TransactionCode,
                                CompanyTransporter = companyTransporter,
                                CarNumber = carNumber,
                                DateTransporter = tDate,
                                DateCreate = DateTime.Today,
                                TotalBox = totalBox,
                                OrderId = id,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                            };
                            List<Vfi.Models.TransactionDetail> transactionDetails =
                                new List<Vfi.Models.TransactionDetail>();
                            List<ExportFormTP_KDDetail> productExportDetails = new List<ExportFormTP_KDDetail>();
                            //List<Vfi.Models.ProductInventoryPeriod> productInvPeriods = new List<Vfi.Models.ProductInventoryPeriod>();
                            foreach (var detail in updatedDetails) {
                                if (detail.Quality == 0) continue;
                                var orderDetail =
                                    vfi.OrderDetails.FirstOrDefault(od => od.OrderDetailId == detail.DetailId);
                                if (orderDetail == null ||
                                    (orderDetail.RequiedNumber - detail.Quality) < 0)
                                    throw new ArgumentException("Quá số lượng còn lại trong đơn hàng - " +
                                                                detail.ProductCode);
                                if (detail.Weight == 0)
                                    throw new ArgumentException("Trọng lượng không được để 0 - " +
                                                                detail.ProductCode);
                                var transactionDetail = new Vfi.Models.TransactionDetail {
                                    Transaction = transaction,
                                    TransactionId = transaction.TransactionId,
                                    ReferenceId = detail.ProductId,
                                    MoP = false,
                                    Quantity = detail.Quality,
                                    Active = true,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    ModifiedDate = DateTime.Now,
                                    QuantityKg = detail.Weight,
                                    // Note = detail.DetailId + "",
                                };
                                transactionDetails.Add(transactionDetail);
                                var productExportDetail = new ExportFormTP_KDDetail {
                                    ExportFormTP_KD = productExport,
                                    ExportId = productExport.ExportId,
                                    ProductId = detail.ProductId,
                                    // BigBox = productInventoryRotateModel.BigBoxNum,
                                    // SmallBox = productInventoryRotateModel.SmallBoxNum,
                                    Quality = detail.Quality,
                                    Weight = detail.Weight,
                                    TransactionDetailId = transactionDetail.TransactionDetailId,
                                    TransactionDetail = transactionDetail,
                                    OrderDetail = orderDetail,
                                    OrderDetailId = orderDetail.OrderDetailId,
                                    IsInvoiced = false,
                                };
                                productExportDetails.Add(productExportDetail);
                            }
                            if (transactionDetails.Any()) {
                                vfi.Transactions.Add(transaction);
                                vfi.ExportFormTP_KD.Add(productExport);
                                vfi.SaveChanges();
                                vfi.TransactionDetails.AddRange(transactionDetails);
                                vfi.ExportFormTP_KDDetail.AddRange(productExportDetails);
                                vfi.SaveChanges();
                            }
                        }
                        // doi hang
                        else if (type == 2) {
                            var orderNote = vfi.OrderNotes.FirstOrDefault(o => o.NoteId == id);
                            if (orderNote == null)
                                throw new ArgumentException("Không tìm thấy phiếu đổi hàng!");
                            var transaction = new Vfi.Models.Transaction {
                                WarehouseIssueId = MyUtilities.Warehouse.Finish,
                                WarehouseReceiptId = MyUtilities.Warehouse.Business,
                                TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                                EoI = "2",
                                MoP = false,
                                CreatedUser = HttpContext.User.Identity.Name,
                                CreatedDate = tDate,
                                Status = (byte)MyUtilities.Transaction.Status.Open,
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                            };
                            var productExport = new ExportChangeProduct {
                                Transporter = transporter,
                                CompanyTransport = companyTransporter,
                                CarNumber = carNumber,
                                ExportDate = tDate,
                                Totalbox = totalBox,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                NoteId = id,
                                TransactionId = transaction.TransactionId,
                                Transaction = transaction,
                            };
                            //var exportChanges =
                            //    vfi.ExportChangeProducts.Where(
                            //        ec =>
                            //        ec.NoteId == orderNote.NoteId && ec.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved);
                            var productIds = updatedDetails.Select(p => p.ProductId).Distinct();
                            var tdChanges = from td in vfi.TransactionDetails
                                            where td.Transaction.ExportChangeProducts.Any() &&
                                                  td.Transaction.ExportChangeProducts.FirstOrDefault().NoteId == id &&
                                                  td.Transaction.Status != (byte)MyUtilities.Transaction.Status.Cancel &&
                                                  productIds.Contains(td.ReferenceId ?? 0)
                                            select td;
                            var total = 0.0;
                            List<Vfi.Models.TransactionDetail> transactionDetails =
                                new List<Vfi.Models.TransactionDetail>();
                            foreach (var update in updatedDetails) {
                                var orderNoteDetail =
                                    orderNote.OrderNoteDetails.FirstOrDefault(od => od.ProductId == update.ProductId);
                                if (orderNoteDetail == null ||
                                    (orderNoteDetail.Quantity - update.Quality) < 0)
                                    throw new ArgumentException("Quá số lượng yêu cầu - " + update.ProductCode);
                                if (update.Weight == 0)
                                    throw new ArgumentException("Trọng lượng không được để 0 - " + update.ProductCode);
                                //var tdChangeById =
                                //    transactionDetailChanges.Where(td => td.ReferenceId == update.ProductId);
                                if (tdChanges.Any())
                                    total = tdChanges.Sum(td => td.Quantity);
                                //if (exportChanges.Any())
                                //{
                                //    foreach (var exportChange in exportChanges)
                                //    {
                                //        var transactionDetailChange =
                                //            vfi.TransactionDetails.FirstOrDefault(
                                //                td =>
                                //                td.ReferenceId == update.ProductId &&
                                //                td.TransactionId == exportChange.TransactionId);
                                //        if (transactionDetailChange != null)
                                //            total += (transactionDetailChange.Quantity ?? 0.0);
                                //    }
                                //}
                                if ((orderNoteDetail.Quantity - update.Quality - total) < 0)
                                    throw new ArgumentException("Quá số lượng yêu cầu - " + update.ProductCode);
                                var transactionDetail = new Vfi.Models.TransactionDetail {
                                    Transaction = transaction,
                                    TransactionId = transaction.TransactionId,
                                    ReferenceId = update.ProductId,
                                    MoP = false,
                                    Quantity = update.Quality,
                                    Active = true,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    ModifiedDate = DateTime.Now,
                                    QuantityKg = update.Weight
                                };
                                transactionDetails.Add(transactionDetail);
                            }
                            if (transactionDetails.Any()) {
                                vfi.Transactions.Add(transaction);
                                vfi.ExportChangeProducts.Add(productExport);
                                vfi.SaveChanges();
                                vfi.TransactionDetails.AddRange(transactionDetails);
                                vfi.SaveChanges();
                            }

                        }
                        // Session["SessionProductExportTerm"] = new List<Vfi.Models.ExportFormTP_KD>();
                    }
                }
                catch (Exception exception) {
                    ModelState.AddModelError("Export TP", "" + exception.Message);
                }
            }
            // Session["SessionProductExportTerm"] = null;
            return View(new GridModel(new List<ExportFormTP_KDDetailsModel>()));
        }

        [GridAction]
        public ActionResult ProductExportTP_NewType2(
            [Bind(Prefix = "inserted")] IEnumerable<ProductInventoryRotateModel> inserteds,
            [Bind(Prefix = "updated")] IEnumerable<ProductInventoryRotateModel> updateds,
            [Bind(Prefix = "deleted")] IEnumerable<ProductInventoryRotateModel> deleteds,
            int? customerId, string transporter, string companyTransporter,
            string carNumber, string transportDate, int totalBox
        ) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode", "Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<ProductInventoryRotateModel>()));
            }

            try {
                using (var vfi = new tammaContext()) {
                    var ci = new CultureInfo("vi-VN");
                    if (customerId == null || customerId == 0)
                        throw new AggregateException("Lỗi! Chưa chọn khách hàng.");
                    var tDate = string.IsNullOrWhiteSpace(transportDate)
                                    ? DateTime.Today
                                    : Convert.ToDateTime(transportDate, ci);
                    var transaction = new Vfi.Models.Transaction {
                        WarehouseIssueId = MyUtilities.Warehouse.Finish,
                        WarehouseReceiptId = MyUtilities.Warehouse.Business,

                        TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                        EoI = "2",
                        MoP = false,
                        CreatedUser = HttpContext.User.Identity.Name,
                        CreatedDate = tDate,
                        Status = (byte)MyUtilities.Transaction.Status.Open,
                        Active = true,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                    };
                    var productExport = new ExportFormTP_KD() {
                        CustomerId = customerId,
                        Transporter = transporter,
                        TransactionCode = transaction.TransactionCode,
                        CompanyTransporter = companyTransporter,
                        CarNumber = carNumber,
                        DateTransporter = tDate,
                        DateCreate = DateTime.Today,
                        TotalBox = totalBox,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,

                    };
                    foreach (var detail in updateds) {
                        if (detail.Quantity == 0) continue;
                        if (detail.QuantityKg == 0)
                            throw new ArgumentException("Trọng lượng không được để 0 - " +
                                                        detail.ProductCode);
                        var productInv = vfi.ProductInventories
                            .FirstOrDefault(pi => pi.ProductInventoryId == detail.ProductInventoryId);
                        if (productInv == null || detail.Quantity > productInv.TotalQty)
                            throw new AggregateException("Lỗi! Số lượng tồn kho không đủ xuất.| " +
                                                         detail.ProductCode + " | " + detail.LotNumber);
                        var transactionDetails =
                            vfi.TransactionDetails
                                .Where(td => td.Transaction.Status == (byte)MyUtilities.Transaction.Status.Open &&
                                             td.ProductInvId == detail.ProductInventoryId)
                                .ToList().Sum(td => td.Quantity);
                        if (detail.Quantity + transactionDetails > productInv.TotalQty)
                            throw new AggregateException("Lỗi! Số lượng có thể chuyển không đủ xuất.| " +
                                                        detail.ProductCode + " | " + detail.LotNumber);
                        var transactionDetail = new Vfi.Models.TransactionDetail {
                            Transaction = transaction,
                            TransactionId = transaction.TransactionId,
                            ReferenceId = detail.ProductId,
                            MoP = false,
                            Quantity = detail.Quantity,
                            Active = true,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            QuantityKg = detail.QuantityKg,
                            ProductInventory = productInv,
                            LotNumber = (productInv.LotNumber + "").Trim(),
                            NextProcessId = productInv.ByProcessMachineId,
                        };
                        transaction.TransactionDetails.Add(transactionDetail);
                        var productExportDetail = new ExportFormTP_KDDetail {
                            ExportFormTP_KD = productExport,
                            ExportId = productExport.ExportId,
                            ProductId = detail.ProductId,
                            Quality = detail.Quantity,
                            Weight = detail.QuantityKg,
                            TransactionDetailId = transactionDetail.TransactionDetailId,
                            TransactionDetail = transactionDetail,
                            IsInvoiced = false,
                            ProductInvId = productInv.ProductInventoryId,
                        };
                        productExport.ExportFormTP_KDDetail.Add(productExportDetail);
                    }
                    if (transaction.TransactionDetails.Any()) {
                        vfi.Transactions.Add(transaction);
                        vfi.ExportFormTP_KD.Add(productExport);
                        vfi.SaveChanges();
                    }
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("Export TP", exception.Message);
            }
            return View(new GridModel(new List<ProductInventoryRotateModel>()));
        }

        [GridAction]
        public ActionResult ProductExportTP_NewType(
            [Bind(Prefix = "inserted")] IEnumerable<ExportFormTP_KDDetailsModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<ExportFormTP_KDDetailsModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<ExportFormTP_KDDetailsModel> deletedDetails
            , int? customerId, string transporter
            , string companyTransporter, string carNumber
            , string transportDate
            , int totalBox
            ) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode", "Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<ExportFormTP_KDDetailsModel>()));
            }

            try {
                using (var vfi = new tammaContext()) {
                    var ci = new CultureInfo("vi-VN");
                    if (customerId == null || customerId == 0)
                        throw new AggregateException("Lỗi! Chưa chọn khách hàng.");
                    var tDate = string.IsNullOrWhiteSpace(transportDate)
                                    ? DateTime.Today
                                    : Convert.ToDateTime(transportDate, ci);
                    //var order = vfi.Orders.FirstOrDefault(o => o.OrderId == id);
                    var transaction = new Vfi.Models.Transaction {
                        WarehouseIssueId = MyUtilities.Warehouse.Finish,
                        WarehouseReceiptId = MyUtilities.Warehouse.Business,

                        TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                        EoI = "2",
                        MoP = false,
                        CreatedUser = HttpContext.User.Identity.Name,
                        CreatedDate = tDate,
                        Status = (byte)MyUtilities.Transaction.Status.Open,
                        Active = true,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                    };
                    var productExport = new ExportFormTP_KD() {
                        CustomerId = customerId,
                        Transporter = transporter,
                        TransactionCode = transaction.TransactionCode,
                        CompanyTransporter = companyTransporter,
                        CarNumber = carNumber,
                        DateTransporter = tDate,
                        DateCreate = DateTime.Today,
                        TotalBox = totalBox,
                        //OrderId = id,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,

                    };
                    foreach (var detail in insertedDetails) {
                        if (detail.Quality == 0) continue;
                        if (detail.Weight == 0)
                            throw new ArgumentException("Trọng lượng không được để 0 - " +
                                                        detail.ProductInvCode);
                        var productInv =
                            vfi.ProductInventories.FirstOrDefault(
                                pi => pi.ProductInventoryId == detail.ProductInvId);
                        //if (productInv == null)
                        //{
                        //    productInv = new ProductInventory
                        //        {
                        //            WarehouseId =  MyUtilities.Warehouse.Finish,
                        //            ProductId = detail.ProductId,
                        //            TotalQty =   0,
                        //            ImportDate = transaction.CreatedDate,
                        //            ModifiedDate = DateTime.Now,
                        //            ModifiedUser = HttpContext.User.Identity.Name,
                        //            LotNumber = "",
                        //        };
                        //    vfi.ProductInventories.Add(productInv);
                        //}
                        if (detail.Quality > productInv.TotalQty)
                            throw new ArgumentException("Vượt quá số tồn kho - " +
                                                        detail.ProductInvCode);
                        var transactionDetail = new Vfi.Models.TransactionDetail {
                            Transaction = transaction,
                            TransactionId = transaction.TransactionId,
                            ReferenceId = productInv.ProductId,
                            MoP = false,
                            Quantity = detail.Quality,
                            Active = true,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            QuantityKg = detail.Weight,
                            // Note = detail.DetailId + "",
                            ProductInventory = productInv,
                            LotNumber = productInv.LotNumber,
                        };
                        transaction.TransactionDetails.Add(transactionDetail);
                        var productExportDetail = new ExportFormTP_KDDetail {
                            ExportFormTP_KD = productExport,
                            ExportId = productExport.ExportId,
                            ProductId = productInv.ProductId,
                            Quality = detail.Quality,
                            Weight = detail.Weight,
                            TransactionDetailId = transactionDetail.TransactionDetailId,
                            TransactionDetail = transactionDetail,
                            IsInvoiced = false,
                            ProductInvId = productInv.ProductInventoryId,
                        };
                        productExport.ExportFormTP_KDDetail.Add(productExportDetail);
                    }
                    if (transaction.TransactionDetails.Any()) {
                        vfi.Transactions.Add(transaction);
                        vfi.ExportFormTP_KD.Add(productExport);
                        vfi.SaveChanges();
                    }
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("Export TP", "" + exception.Message);
            }
            return View(new GridModel(new List<ExportFormTP_KDDetailsModel>()));
        }

        [GridAction]
        public ActionResult ProductExportTP_NewType3(
            [Bind(Prefix = "inserted")] IEnumerable<ExportFormTP_KDDetailsModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<ExportFormTP_KDDetailsModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<ExportFormTP_KDDetailsModel> deletedDetails
            , int? customerId, string transporter
            , string companyTransporter, string carNumber
            , string transportDate
            , int totalBox
            ) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode", "Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<ExportFormTP_KDDetailsModel>()));
            }
            var ci = new CultureInfo("vi-VN");
            if (customerId == null || customerId == 0)
                throw new AggregateException("Lỗi! Chưa chọn khách hàng.");
            var tDate = string.IsNullOrWhiteSpace(transportDate)
                            ? DateTime.Today
                            : Convert.ToDateTime(transportDate, ci);
            try {
                if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, tDate)) {
                    throw new AggregateException(
                        @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                }
                using (var vfi = new tammaContext()) {
                    var transaction = new Vfi.Models.Transaction {
                        WarehouseIssueId = MyUtilities.Warehouse.Finish,
                        WarehouseReceiptId = MyUtilities.Warehouse.Business,
                        TransactionCode = MyUtilities.AutoIncrease.GetParam(
                            (int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                        EoI = "2",
                        MoP = false,
                        CreatedUser = HttpContext.User.Identity.Name,
                        CreatedDate = tDate,
                        Status = (byte)MyUtilities.Transaction.Status.Processing,
                        Active = true,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                    };
                    var productExport = new ExportFormTP_KD() {
                        CustomerId = customerId,
                        Transporter = transporter,
                        TransactionCode = transaction.TransactionCode,
                        CompanyTransporter = companyTransporter,
                        CarNumber = carNumber,
                        DateTransporter = tDate,
                        DateCreate = DateTime.Today,
                        TotalBox = totalBox,
                        //OrderId = id,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,

                    };
                    foreach (var detail in insertedDetails) {
                        if (detail.Quality == 0) continue;
                        if (detail.Weight == 0)
                            throw new ArgumentException("Trọng lượng không được để 0 - " +
                                                        detail.ProductCode);
                        var transactionProduct = new Vfi.Models.TransactionProduct {
                            Transaction = transaction,
                            TransactionId = transaction.TransactionId,
                            ProductId = detail.ProductId,
                            Quantity = detail.Quality,
                            QuantityKg = detail.Weight,
                            Note = detail.Note,
                        };
                        transaction.TransactionProducts.Add(transactionProduct);
                        var productExportDetail = new ExportFormTP_KDDetail {
                            ExportFormTP_KD = productExport,
                            ExportId = productExport.ExportId,
                            ProductId = detail.ProductId,
                            Quality = detail.Quality,
                            Weight = detail.Weight,
                            IsInvoiced = false,
                        };
                        productExport.ExportFormTP_KDDetail.Add(productExportDetail);
                    }
                    if (productExport.ExportFormTP_KDDetail.Any()) {
                        vfi.Transactions.Add(transaction);
                        vfi.ExportFormTP_KD.Add(productExport);
                        vfi.SaveChanges();
                    }
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("Export TP", "" + exception.Message);
            }
            return View(new GridModel(new List<ExportFormTP_KDDetailsModel>()));
        }
        [GridAction]
        public ActionResult SelectImportFormSX1Detail() {
            return View(new GridModel(new List<ImportSX1DetailModel>()));
        }
        [HttpPost]
        [GridAction]
        public ActionResult UpdateImportFormSX1Detail(
            [Bind(Prefix = "inserted")]IEnumerable<ImportSX1DetailModel> insertedImportDetails,
            [Bind(Prefix = "updated")]IEnumerable<ImportSX1DetailModel> updatedSalesOrderDetails,
            [Bind(Prefix = "deleted")]IEnumerable<ImportSX1DetailModel> deletedSalesOrderDetails,
             string transactionCode, string importDate, string ca1, string ca2) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode", "Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<ImportSX1DetailModel>()));
            }


            using (var vfi = new tammaContext()) {
                try {
                    var ci = new CultureInfo("vi-VN");
                    var date = string.IsNullOrWhiteSpace(importDate)
                                     ? DateTime.Today
                                     : Convert.ToDateTime(importDate, ci);
                    var import = new ImportFormSX1 {
                        ImportDate = date,
                        TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        Shift1Name = ca1,
                        Shift2Name = ca2,
                        Status = (byte)MyUtilities.Transaction.Status.Approved
                    };
                    var transactionSX1 = new Vfi.Models.Transaction {
                        TransactionCode = import.TransactionCode,
                        CreatedUser = HttpContext.User.Identity.Name,
                        CreatedDate = date,
                        WarehouseIssueId = null,
                        WarehouseReceiptId = 1,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        Status = (byte)MyUtilities.Transaction.Status.Open,
                        Active = true,
                        EoI = "0",
                        MoP = false,

                    };
                    var transactionPP = new Vfi.Models.Transaction {
                        TransactionCode = import.TransactionCode,
                        CreatedUser = HttpContext.User.Identity.Name,
                        CreatedDate = date,
                        WarehouseIssueId = null,
                        WarehouseReceiptId = 9,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        Status = (byte)MyUtilities.Transaction.Status.Open,
                        Active = true,
                        EoI = "0",
                        MoP = false,
                    };

                    var importDetails = new List<ImportFormSX1Detail>();
                    var transactionDetailsSX1 = new List<Vfi.Models.TransactionDetail>();
                    var transactionDetailsPP = new List<Vfi.Models.TransactionDetail>();

                    foreach (var entity in insertedImportDetails) {
                        if (entity.Number1 + entity.Number2 + entity.DefectProduct1 + entity.DefectProduct2 > 0) {
                            var importDetail = new ImportFormSX1Detail {
                                ImportFormSX1 = import,
                                Machine = entity.Machine,
                                ProductId = entity.ProductId,
                                //Shift1 = ca1,
                                //Shift2 = ca2,
                                Number1 = entity.Number1,
                                Number2 = entity.Number2,
                                DefectProduct1 = entity.DefectProduct1,
                                DefectProduct2 = entity.DefectProduct2,
                            };
                            importDetails.Add(importDetail);
                        }
                        if (entity.Number1 + entity.Number2 != 0) {
                            var transactionDetailSX1 = new Vfi.Models.TransactionDetail {
                                Transaction = transactionSX1,
                                TransactionId = transactionSX1.TransactionId,
                                ReferenceId = entity.ProductId,
                                MoP = false,
                                Quantity = entity.Number1 + entity.Number2,
                                UnitMeasure = null,
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                QuantityKg = 0,
                            };
                            transactionDetailsSX1.Add(transactionDetailSX1);
                        }

                        if (entity.DefectProduct1 + entity.DefectProduct2 != 0) {
                            var transactionDetailPP = new Vfi.Models.TransactionDetail {
                                Transaction = transactionPP,
                                TransactionId = transactionPP.TransactionId,
                                ReferenceId = entity.ProductId,
                                MoP = false,
                                Quantity = entity.DefectProduct1 + entity.DefectProduct2,
                                UnitMeasure = null,
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                QuantityKg = 0,

                            };
                            transactionDetailsPP.Add(transactionDetailPP);
                        }
                    }
                    vfi.ImportFormSX1.Add(import);
                    if (transactionDetailsSX1.Count > 0) {
                        vfi.Transactions.Add(transactionSX1);
                        vfi.TransactionDetails.AddRange(transactionDetailsSX1);
                    }
                    if (transactionDetailsPP.Count > 0) {
                        vfi.Transactions.Add(transactionPP);
                        vfi.TransactionDetails.AddRange(transactionDetailsPP);
                    }
                    vfi.ImportFormSX1Detail.AddRange(importDetails);
                    vfi.SaveChanges();

                }
                catch (Exception exception) {
                    ModelState.AddModelError("ProductCode", "" + exception.Message);
                }
            }
            // Session["SessionImportSX1Detail"] = null;
            return View(new GridModel(new List<ImportSX1DetailModel>()));
        }

        List<TransactionModel> GetTransactions(string productCode, int warehouseId, byte status, bool mop, string fromDate, string toDate) {
            var model = new List<TransactionModel>();
            var ci = new CultureInfo("vi-VN");
            DateTime toDay = DateTime.Today;

            var fDate = MyUtilities.Function.ParseDate(fromDate);
            var tDate = MyUtilities.Function.ParseLastDateTime(toDate);

            var invManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.InvManagement);
            using (var vfi = new tammaContext()) {
                //vfi.Configuration.LazyLoadingEnabled = false;
                if (!mop) {
                    var userId = vfi.Users.FirstOrDefault(u => u.Username.Equals(HttpContext.User.Identity.Name)).UserId;
                    var userWarehousePermissionImport =
                        vfi.WarehousePermissions.Where(wp => wp.UserId == userId && wp.Import == true)
                           .Select(wp => wp.WarehouseId.Value).ToList();
                    var userWarehousePermissionRotate =
                        vfi.WarehousePermissions.Where(wp => wp.UserId == userId && wp.Rotate == true)
                           .Select(wp => wp.WarehouseId.Value).ToList();
                    userWarehousePermissionImport.AddRange(userWarehousePermissionRotate);
                    var canApproveDefect = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.ApproveDefect);
                    if (canApproveDefect) {
                        userWarehousePermissionImport.Add(MyUtilities.Warehouse.Defect);
                    }
                    var canApproveInternal = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.ApproveInternalProduct);
                    var warehouseIds = userWarehousePermissionImport.Distinct().ToList();
                    if (warehouseId > 0) {
                        warehouseIds = new List<int> { warehouseId };
                    }
                    var transactions = (from t in vfi.Transactions
                                        where t.Status == status &&
                                              t.MoP == mop &&
                                              t.CreatedDate >= fDate &&
                                              t.CreatedDate <= tDate &&
                                              ((t.WarehouseIssueId != null && warehouseIds.Contains(t.WarehouseIssueId.Value))
                                              || (t.WarehouseReceiptId != null && warehouseIds.Contains(t.WarehouseReceiptId.Value))
                                              || (t.IsInternal == true && canApproveInternal))
                                        select t
                                    ).ToList();
                    if (!string.IsNullOrWhiteSpace(productCode)) {
                        var productIds = vfi.Products.Where(t => t.ProductCode.Contains(productCode)).Select(t => t.ProductId).ToList();
                        if (productIds.Any()) {
                            transactions = transactions.Where(t => t.TransactionDetails.Any(td => productIds.Contains(td.ReferenceId.Value))).ToList();
                        }
                    }
                    foreach (var transaction in transactions) {

                        //if (!(
                        //         (userWarehousePermissionImport.Contains(transaction.WarehouseReceiptId.Value))
                        //         ||
                        //         (transaction.WarehouseIssueId != null &&
                        //          userWarehousePermissionImport.Contains(transaction.WarehouseIssueId.Value) &&
                        //          userWarehousePermissionRotate.Contains(transaction.WarehouseReceiptId.Value))
                        //         ||
                        //         (transaction.WarehouseIssueId == null &&
                        //          transaction.WarehouseReceiptId == (byte)MyUtilities.Warehouse.Defect &&
                        //          permission.FirstOrDefault(p => p.FunctionID == 43) != null
                        //         )
                        //     )
                        //    ) {
                        //    continue;
                        //}

                        var entity = new TransactionModel {
                            TransactionId = transaction.TransactionId,
                            TransactionCode = transaction.TransactionCode,
                            StockOrderId = transaction.StockOrderId,
                            StockOrderCode =
                                transaction.StockOrder != null ? transaction.StockOrder.StockOrderCode : "",
                            WarehouseIssueId = transaction.WarehouseIssueId,
                            WarehouseIssueName = transaction.WarehouseIssueId != null
                                                     ? transaction.Warehouse.WarehouseName
                                                     : "",
                            WarehouseReceiptId = transaction.WarehouseReceiptId,
                            WarehouseReceiptName = transaction.WarehouseReceiptId != null
                                                     ? transaction.Warehouse1.WarehouseName
                                                       : "",
                            EoI = transaction.EoI,
                            EoIName = MyUtilities.Transaction.CastText.GetTextEoI(transaction.EoI),
                            MoP = transaction.MoP,
                            Status = transaction.Status,
                            StatusName = MyUtilities.Transaction.CastText.GetTextStatus(transaction.Status),
                            Description = transaction.Description ?? "",
                            Active = transaction.Active,
                            CreatedUser = transaction.CreatedUser,
                            CreatedDate = transaction.CreatedDate,
                            ModifiedUser = transaction.ModifiedUser,
                            ModifiedDate = transaction.ModifiedDate,
                            TotalQuality = transaction.TransactionDetails.Sum(t => t.Quantity),
                            CanApprove = invManager,
                            IsInternal = transaction.IsInternal ?? false
                        };

                        if (entity.WarehouseIssueId == MyUtilities.Warehouse.Production1) {
                            var form = vfi.ImportFormSX1.FirstOrDefault(i => i.TransactionCode.Equals(entity.TransactionCode));
                            if (form != null) {
                                entity.SpecialNote = "Ngày SX:" + form.MaterialUseDate.ToString("dd/MM/yyyy");
                            }
                        }
                        else if (entity.WarehouseIssueId == MyUtilities.Warehouse.Cnc) {
                            var form = vfi.ImportFormCncs.FirstOrDefault(i => i.TransactionCode.Equals(entity.TransactionCode));
                            if (form != null) {
                                entity.SpecialNote = "Ngày SX:" + form.MaterialUseDate.ToString("dd/MM/yyyy");
                            }
                        }
                        else if (entity.WarehouseIssueId == MyUtilities.Warehouse.WaitingPlating) {
                            var form = vfi.ExportGCN_NCU.FirstOrDefault(i => i.TransactionId == entity.TransactionId);
                            if (form != null) {
                                var plating = form.PlatingForm;
                                var vendor = plating.Vendor;
                                if (plating.PlatingType == MyUtilities.Warehouse.Plating)
                                    entity.SpecialNote = "Xi mạ - ";
                                else if (plating.PlatingType == MyUtilities.Warehouse.PlatingTest)
                                    entity.SpecialNote = "GCN - ";
                                entity.SpecialNote += "NCC:" + vendor.VendorName;
                                entity.SpecialFormType = (int)MyUtilities.Transaction.FormTypeEnum.ExportGCN_NCU;
                            }
                        }
                        else if (entity.WarehouseReceiptId == MyUtilities.Warehouse.QcB) {
                            var form = vfi.ImportNCU_QCB.FirstOrDefault(i => i.TransactionId == entity.TransactionId);
                            if (form != null) {
                                var plating = form.PlatingForm;
                                var vendor = plating.Vendor;
                                if (plating.PlatingType == MyUtilities.Warehouse.Plating)
                                    entity.SpecialNote = "Xi mạ - ";
                                else if (plating.PlatingType == MyUtilities.Warehouse.PlatingTest)
                                    entity.SpecialNote = "GCN - ";
                                entity.SpecialNote += "NCC:" + vendor.VendorName;
                                entity.SpecialFormType = (int)MyUtilities.Transaction.FormTypeEnum.ImportNCU_QCB;
                            }
                        }
                        else if (entity.WarehouseReceiptId == MyUtilities.Warehouse.Business) {
                            var form = vfi.ExportFormTP_KD.FirstOrDefault(i => i.TransactionCode.Equals(entity.TransactionCode));
                            if (form != null) {
                                entity.SpecialNote = "Khách hàng:" + form.Customer.CustomerCode;
                                entity.SpecialNote += " - Tổng thùng:" + form.TotalBox;
                                entity.SpecialFormType = (int)MyUtilities.Transaction.FormTypeEnum.ExportTP;
                            }
                        }
                        model.Add(entity);
                    }
                }
                else {
                    var transactions = (from t in vfi.Transactions
                                        where t.Status == status &&
                                              t.MoP == mop &&
                                              t.CreatedDate >= fDate &&
                                              t.CreatedDate <= tDate
                                        select t
                                    ).ToList();
                    foreach (var transaction in transactions) {
                        var transactionDetails =
                            vfi.TransactionDetails.Where(td => td.TransactionId == transaction.TransactionId);

                        if (!transactionDetails.Any()) continue;
                        var entity = new TransactionModel {
                            TransactionId = transaction.TransactionId,
                            TransactionCode = transaction.TransactionCode,
                            EoI = transaction.EoI,
                            EoIName = MyUtilities.Transaction.CastText.GetTextEoI(transaction.EoI),
                            MoP = transaction.MoP,
                            Status = transaction.Status,
                            StatusName =
                                MyUtilities.Transaction.CastText.GetTextStatus(transaction.Status),
                            Description = transaction.Description,
                            Active = transaction.Active,
                            CreatedUser = transaction.CreatedUser,
                            CreatedDate = transaction.CreatedDate,
                            ModifiedUser = transaction.ModifiedUser,
                            ModifiedDate = transaction.ModifiedDate,
                            TotalQuality = transactionDetails.Sum(t => t.Quantity),
                            TotalQualityKg = transactionDetails.Sum(t => t.QuantityKg ?? 0.0),
                            CanApprove = invManager,
                        };
                        if (entity.EoI == Convert.ToChar(MyUtilities.Transaction.EoIEnum.Export).ToString()) {
                            var exportMaterial = vfi.ExportMaterials.FirstOrDefault(em => em.TransactionId == transaction.TransactionId);
                            if (exportMaterial != null) {
                                //entity.TotalQuality = exportMaterial.ExportMaterialDetails.Sum(emd => emd.Quantity ?? 0);
                                //entity.TotalQualityKg =
                                //    exportMaterial.ExportMaterialDetails.Sum(emd => emd.QuantityKg ?? 0);
                                if (exportMaterial.ShiftType == null && exportMaterial.ShiftName == null) {
                                    if (transaction.IsInternal == true) { entity.EoIName += " nội bộ"; }
                                    else { entity.EoIName += " hủy"; }
                                }
                                else
                                    entity.EoIName += " ca: " + exportMaterial.ShiftType + exportMaterial.ShiftName;
                            }
                        }
                        else if (entity.EoI == Convert.ToChar(MyUtilities.Transaction.EoIEnum.Import).ToString()) {
                            var import = vfi.ImportPurchaseOrders.FirstOrDefault(e => e.TransactionId == entity.TransactionId);
                            entity.PurchasingSignatureType = (int)import.PurchasingSignature;
                            if (import == null)
                                throw new AggregateException("Lỗi");
                            if (import.PurchaseOrderId == null) {
                                if (transaction.IsInternal == true) { entity.EoIName += " nội bộ"; }
                                else { entity.EoIName += " thêm"; }
                            }
                            else {
                                entity.SpecialNote += "PO:" + import.PurchaseOrder.RevisionNumber;
                            }
                        }
                        model.Add(entity);
                    }
                }

            }
            return model.OrderBy(x => x.Status).ThenBy(x => x.CreatedDate).ToList();
        }

        [GridAction]
        public ActionResult SelectTransactionReviewByStatus(string productCode, int warehouseId, byte status, bool mop, string fromDate, string toDate) {
            if (status == null || status == 0) {
                return View(new GridModel(new List<TransactionModel>()));
            }
            var model = new List<TransactionModel>();
            try {
                model = GetTransactions(productCode, warehouseId, status, mop, fromDate, toDate);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectTransactionReviewByStatus", ex.Message);
            }
            return View(new GridModel(model.OrderByDescending(f => f.CreatedDate)));
        }

        public ActionResult CreateRollbackTransactionMaterial(long transactionId) {
            try {
                using (var vfi = new tammaContext()) {
                    var transaction = vfi.Transactions.FirstOrDefault(x => x.TransactionId == transactionId); 
                    if (transaction == null) {
                        return Json(new MyUtilities.Monitor.MyJsonResult(
                            (int)MyUtilities.Monitor.ErrorCode.NotFound,
                            "",
                            0));
                    }
                    //if (transaction.Status != (byte)MyUtilities.Transaction.Status.Approved) {
                    //    return Json(new MyUtilities.Monitor.MyJsonResult(
                    //        (int)MyUtilities.Monitor.ErrorCode.StatusChanged,
                    //        "Phiếu chưa duyệt không thể trả phiếu",
                    //        0));
                    //}
                    if (transaction.Status == (byte)MyUtilities.Transaction.Status.Cancel) {
                        transaction.Status = (byte)MyUtilities.Transaction.Status.Open;
                        var save = vfi.SaveChanges();
                        return Json(new MyUtilities.Monitor.MyJsonResult(
                        (int)MyUtilities.Monitor.ErrorCode.NoError,
                         "",
                         save));
                    }
                    else {
                        return Json(new MyUtilities.Monitor.MyJsonResult(
                            (int)MyUtilities.Monitor.ErrorCode.StatusChanged,
                            "Phiếu chưa duyệt không thể trả phiếu",
                            0));
                    }

                    if (transaction.IsInternal == true) {
                    }
                    else if (transaction.EoI == Convert.ToChar(MyUtilities.Transaction.EoIEnum.Export).ToString()) {
                        var exportMaterial = transaction.ExportMaterials.FirstOrDefault();
                        if (exportMaterial != null && exportMaterial.ShiftType != null) {
                            return Json(new MyUtilities.Monitor.MyJsonResult(
                                (int)MyUtilities.Monitor.ErrorCode.NotImplement,
                                "Chưa xử lý phát nguyên liệu",
                                0));
                        }
                    }
                    var result = RollbackMaterialInventory(transactionId);
                    if (result.Code != (int)MyUtilities.Monitor.ErrorCode.NoError) {
                        return Json(result);
                    }
                    transaction.Status = (byte)MyUtilities.Transaction.Status.Open;
                    var saved = vfi.SaveChanges();

                    return Json(new MyUtilities.Monitor.MyJsonResult(
                        (int)MyUtilities.Monitor.ErrorCode.NoError,
                        "",
                        saved));
                }
            }
            catch (Exception ex) {
                return Json(new MyUtilities.Monitor.MyJsonResult(
                    (int)MyUtilities.Monitor.ErrorCode.Exception,
                    ex.Message,
                    0));
            }
        }

        public ActionResult CreateRollbackTransaction(long transactionId) {
            try {
                using (var vfi = new tammaContext()) {
                    var transaction = vfi.Transactions.FirstOrDefault(x => x.TransactionId == transactionId);
                    if (transaction == null) {
                        return Json(new MyUtilities.Monitor.MyJsonResult(
                            (int)MyUtilities.Monitor.ErrorCode.NotFound,
                            "",
                            0));
                    }
                    if (transaction.Status != (byte)MyUtilities.Transaction.Status.Approved
                    &&transaction.Status == (byte)MyUtilities.Transaction.Status.Cancel){
                            transaction.Status = (byte)MyUtilities.Transaction.Status.Open;
                            var save = vfi.SaveChanges();
                            return Json(new MyUtilities.Monitor.MyJsonResult(
                            (int)MyUtilities.Monitor.ErrorCode.NoError,
                             "",
                             save));
                        }
                        else {
                            return Json(new MyUtilities.Monitor.MyJsonResult(
                                (int)MyUtilities.Monitor.ErrorCode.StatusChanged,
                                "Phiếu chưa duyệt không thể trả phiếu",
                                0));
                        }
                    
                    if (transaction.IsInternal == true) {
                    }
                    else if (transaction.WarehouseReceiptId == MyUtilities.Warehouse.Production1) {
                        var form = vfi.ImportFormSX1.FirstOrDefault(x => x.TransactionCode.Equals(transaction.TransactionCode));
                        if (form != null) {
                            return Json(new MyUtilities.Monitor.MyJsonResult(
                                (int)MyUtilities.Monitor.ErrorCode.NotImplement,
                                "",
                                0));
                        }
                    }
                    else if (transaction.WarehouseReceiptId == MyUtilities.Warehouse.Cnc) {
                        var form = vfi.ImportFormCncs.FirstOrDefault(x => x.TransactionCode.Equals(transaction.TransactionCode));
                        if (form != null) {
                            return Json(new MyUtilities.Monitor.MyJsonResult(
                                (int)MyUtilities.Monitor.ErrorCode.NotImplement,
                                "",
                                0));
                        }
                    }
                    else if (transaction.WarehouseReceiptId == MyUtilities.Warehouse.Production2) {
                        //return Json((int)MyUtilities.Monitor.ErrorCode.NotImplement);
                    }
                    else if (transaction.WarehouseIssueId == MyUtilities.Warehouse.WaitingPlating) {
                        var form = vfi.ExportGCN_NCU.FirstOrDefault(x => x.TransactionId == transactionId);
                        if (form != null) {
                            return Json(new MyUtilities.Monitor.MyJsonResult(
                                (int)MyUtilities.Monitor.ErrorCode.NotImplement,
                                "",
                                0));
                        }
                    }
                    else if (transaction.WarehouseReceiptId == MyUtilities.Warehouse.QcB) {
                        var form = vfi.ImportNCU_QCB.FirstOrDefault(x => x.TransactionId == transactionId);
                        if (form != null) {
                            return Json(new MyUtilities.Monitor.MyJsonResult(
                                (int)MyUtilities.Monitor.ErrorCode.NotImplement,
                                "",
                                0));
                        }
                    }
                    else if (transaction.WarehouseReceiptId == MyUtilities.Warehouse.Processing) {
                        var form = vfi.DefectTransactions.FirstOrDefault(x => x.ImportTransactionId == transactionId);
                        if (form != null) {
                        }
                    }
                    else if (transaction.WarehouseReceiptId == MyUtilities.Warehouse.Business) {
                        var form = vfi.ExportFormTP_KD.FirstOrDefault(x => x.TransactionCode.Equals(transaction.TransactionCode));
                        if (form != null) {
                            return Json(new MyUtilities.Monitor.MyJsonResult(
                                (int)MyUtilities.Monitor.ErrorCode.NotImplement,
                                "",
                                0));
                        }
                    }

                    var result = RollbackProductInventory(transactionId);
                    if (result.Code != (int)MyUtilities.Monitor.ErrorCode.NoError) {
                        return Json(result);
                    }
                    
                    transaction.Status = (byte)MyUtilities.Transaction.Status.Open;
                    var saved = vfi.SaveChanges();

                    return Json(new MyUtilities.Monitor.MyJsonResult(
                        (int)MyUtilities.Monitor.ErrorCode.NoError,
                        "",
                        saved));
                }
            }
            catch (Exception ex) {
                return Json(new MyUtilities.Monitor.MyJsonResult(
                    (int)MyUtilities.Monitor.ErrorCode.Exception,
                    ex.Message,
                    0));
            }
        }

        MyUtilities.Monitor.MyJsonResult RollbackProductInventory(long transactionId) {
            using (var vfi = new tammaContext()) {
                var productPeriods = vfi.ProductInventoryPeriods.Where(x => x.TransactionId == transactionId);
                if (!productPeriods.Any()) {
                    return new MyUtilities.Monitor.MyJsonResult(
                        (int)MyUtilities.Monitor.ErrorCode.NoError, "", 0);
                }
                var productInvIds = productPeriods.Select(x => x.ProductInvId).Distinct().ToList();
                var productInvs = vfi.ProductInventories.Where(x => productInvIds.Contains(x.ProductInventoryId));
                foreach (var period in productPeriods) {
                    var productInv = productInvs.FirstOrDefault(x => x.ProductInventoryId == period.ProductInvId);
                    if (period.LastPeriodQuantity > period.EarlyPeriodQuantity) {
                        if (productInv.TotalQty < period.Quantity) {
                            return new MyUtilities.Monitor.MyJsonResult(
                                (int)MyUtilities.Monitor.ErrorCode.ReferenceError, "Lỗi! Tồn kho không đủ để trả phiếu", 0);
                        }
                        productInv.TotalQty -= period.Quantity;
                    }
                    else {
                        productInv.TotalQty += period.Quantity;
                    }
                }
                vfi.ProductInventoryPeriods.RemoveRange(productPeriods);
                var saved = vfi.SaveChanges();
                return new MyUtilities.Monitor.MyJsonResult(
                    (int)MyUtilities.Monitor.ErrorCode.NoError,
                    "",
                    saved);
            }
        }

        MyUtilities.Monitor.MyJsonResult RollbackMaterialInventory(long transactionId) {
            using (var vfi = new tammaContext()) {
                var periods = vfi.MaterialInventoryPeriods.Where(x => x.TransactionId == transactionId);
                if (!periods.Any()) {
                    return new MyUtilities.Monitor.MyJsonResult(
                        (int)MyUtilities.Monitor.ErrorCode.NoError, "", 0);
                }
                var invIds = periods.Select(x => x.MaterialInventoryId).Distinct().ToList();
                var invs = vfi.MaterialInventories.Where(x => invIds.Contains(x.MaterialInventoryId));
                foreach (var period in periods) {
                    var inv = invs.FirstOrDefault(x => x.MaterialInventoryId == period.MaterialInventoryId);
                    if (period.LastPeriodQuantity > period.EarlyPeriodQuantity) {
                        if (inv.TotalQty < period.Quantity) {
                            return new MyUtilities.Monitor.MyJsonResult(
                                (int)MyUtilities.Monitor.ErrorCode.ReferenceError, "Lỗi! Tồn kho không đủ để trả phiếu", 0);
                        }
                        inv.TotalQty -= period.Quantity;
                    }
                    else {
                        inv.TotalQty += period.Quantity;
                    }
                }
                vfi.MaterialInventoryPeriods.RemoveRange(periods);
                var saved = vfi.SaveChanges();
                return new MyUtilities.Monitor.MyJsonResult(
                    (int)MyUtilities.Monitor.ErrorCode.NoError,
                    "",
                    saved);
            }
        }


        [GridAction]
        public ActionResult UpdateTransactions(TransactionModel update) {
            try {
                using (var vfi = new tammaContext()) {
                    var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId == update.TransactionId);
                    if (transaction == null)
                        throw new AggregateException("Lỗi! Không tìm thấy phiếu giao dịch! ");
                    if (transaction.WarehouseReceiptId == MyUtilities.Warehouse.Production1)
                        throw new AggregateException("Lỗi! Không hổ trợ chỉnh sản xuất 1! ");
                    var date = update.CreatedDate;
                    if (transaction.Status == (byte)MyUtilities.Transaction.Status.Approved) {
                        var periods = vfi.ProductInventoryPeriods.Where(pip => pip.TransactionId == transaction.TransactionId);
                        foreach (var period in periods) {
                            period.PeriodDate = date;
                        }
                    }
                    var transactionCode = transaction.TransactionCode.Trim();
                    if (transaction.WarehouseIssueId == MyUtilities.Warehouse.Cnc) {
                        var cncForm = vfi.ImportFormCncs.FirstOrDefault(ic => ic.TransactionCode.Contains(transactionCode));
                        if (cncForm != null)
                            throw new AggregateException("Lỗi! Không hổ trợ chỉnh nhập cnc! ");
                    }
                    else if (transaction.WarehouseIssueId == MyUtilities.Warehouse.WaitingPlating &&
                        (transaction.WarehouseReceiptId == MyUtilities.Warehouse.Plating ||
                        transaction.WarehouseReceiptId == MyUtilities.Warehouse.PlatingTest)) {
                        var exportPlating = vfi.ExportGCN_NCU.FirstOrDefault(e => e.TransactionId == transaction.TransactionId);
                        if (exportPlating == null)
                            goto save;
                        exportPlating.ExportDate = date;
                    }
                    else if ((transaction.WarehouseIssueId == MyUtilities.Warehouse.Plating ||
                        transaction.WarehouseIssueId == MyUtilities.Warehouse.PlatingTest) &&
                        transaction.WarehouseReceiptId == MyUtilities.Warehouse.QcB) {
                        var impotPlating = vfi.ImportNCU_QCB.FirstOrDefault(e => e.TransactionId == transaction.TransactionId);
                        if (impotPlating == null)
                            goto save;
                        impotPlating.ImportDate = date;
                    }
                    else if (transaction.WarehouseIssueId == MyUtilities.Warehouse.Finish &&
                        transaction.WarehouseReceiptId == MyUtilities.Warehouse.Business) {
                        var exportTp = vfi.ExportFormTP_KD.FirstOrDefault(e => e.TransactionCode.Contains(transactionCode));
                        if (exportTp != null)
                            throw new AggregateException("Lỗi! Không hổ trợ chỉnh xuất thành phẩm! ");
                    }
                save:
                    transaction.CreatedDate = date;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateTransactions", ex.Message);
            }
            return View(new GridModel(new List<TransactionModel>()));
        }

        [GridAction]
        public ActionResult SelectImportNCU_QCBDetail() {
            return View(new GridModel(new List<ImportNCU_QCBDetailModel>()));
        }

        [GridAction]
        public ActionResult SelectImportPlating(int platingFormId) {
            if (platingFormId == 0)
                return View(new GridModel(new List<ImportNCU_QCBDetailModel>()));
            try {
                using (var vfi = new tammaContext()) {
                    var platingForm =
                        vfi.PlatingForms.FirstOrDefault(
                            pf => pf.FormId == platingFormId &&
                                  pf.Status == (byte)MyUtilities.Sales.Status.InProcess &&
                                  pf.ExportGCN_NCU.Any());
                    if (platingForm == null)
                        throw new AggregateException("Lỗi! Không tìm thấy biên bản GCN");
                    var model = new List<ImportNCU_QCBDetailModel>();
                    foreach (var export in platingForm.ExportGCN_NCU) {
                        if (export.Transaction.Status != (byte)MyUtilities.Transaction.Status.Approved)
                            continue;
                        foreach (var exportDetail in export.ExportGCN_NCUDetail) {
                            var productInv =
                                vfi.ProductInventories.FirstOrDefault(
                                    pi =>
                                        pi.ProductInventoryId == exportDetail.ProductInvId);
                            if (productInv == null) continue;

                            var entity = new ImportNCU_QCBDetailModel {
                                ProductId = exportDetail.ProductId,
                                ProductCode = exportDetail.Product.ProductCode,
                                ExportDetailId = exportDetail.DetailId,
                                //RequestNumber = exportDetail.RealNumber,
                                RealNumber = 0,
                                Weight = 0,
                                ExportDate = export.ExportDate ?? DateTime.Today,
                                TransactionCode = export.TransactionCode,
                                ProductWeight = exportDetail.Product.PlatingWeight ?? 0,
                                ProductInventory = productInv != null ? (productInv.TotalQty) : 0,
                                Note = exportDetail.Note,
                                Package = exportDetail.Package + "",
                                Import = 0,
                                LotNumber = productInv.LotNumber,
                                ProductInvId = productInv.ProductInventoryId,
                                ProductImg = exportDetail.Product.DrawingFinish,
                                UploadDate = (exportDetail.Product.UploadDate ?? DateTime.Now).ToString("yyyyMMddhhmmss"),
                                ExportQuantity = exportDetail.RealNumber,
                                ExportWeight = exportDetail.Weight ?? 0
                            };
                            if (string.IsNullOrWhiteSpace(entity.ProductImg))
                                entity.ProductImg = "askquestion.jpg";
                            var unit = exportDetail.PlatingFormDetail.Unit;
                            if (unit.Contains("Kg")) {
                                entity.RequestNumber = exportDetail.Weight.Value / 1000;
                                entity.RequestNumberString = string.Format("{0:n3}", entity.RequestNumber) + " " +
                                                             exportDetail.PlatingFormDetail.Unit;
                                if (exportDetail.ImportNCU_QCBDetail.Any(id =>
                                            id.ImportNCU_QCB.Transaction.Status != (byte)MyUtilities.Transaction.Status.Cancel))
                                    entity.Import =
                                        exportDetail.ImportNCU_QCBDetail.Where(
                                            id =>
                                            id.ImportNCU_QCB.Transaction.Status != (byte)MyUtilities.Transaction.Status.Cancel)
                                                    .Sum(id => id.Weight) / 1000;
                                entity.ImportString = string.Format("{0:n3}", entity.Import) + " " +
                                                             exportDetail.PlatingFormDetail.Unit;
                            }
                            else {
                                entity.RequestNumber = exportDetail.RealNumber;
                                entity.RequestNumberString = string.Format("{0:n0}", entity.RequestNumber) + " " +
                                                             exportDetail.PlatingFormDetail.Unit;
                                if (exportDetail.ImportNCU_QCBDetail.Any(id =>
                                            id.ImportNCU_QCB.Transaction.Status != (byte)MyUtilities.Transaction.Status.Cancel))
                                    entity.Import =
                                        exportDetail.ImportNCU_QCBDetail.Where(
                                            id =>
                                            id.ImportNCU_QCB.Transaction.Status != (byte)MyUtilities.Transaction.Status.Cancel)
                                                    .Sum(id => id.RealNumber);
                                entity.ImportString = string.Format("{0:n0}", entity.Import) + " " +
                                                             exportDetail.PlatingFormDetail.Unit;
                            }
                            model.Add(entity);
                        }
                    }
                    return View(new GridModel(model));
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectImportPlating", "" + ex.Message);
            }
            return View(new GridModel(new List<ImportNCU_QCBDetailModel>()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateImportNCU_QCBDetail(
            [Bind(Prefix = "inserted")]IEnumerable<ImportNCU_QCBDetailModel> inserteDetails,
            [Bind(Prefix = "updated")]IEnumerable<ImportNCU_QCBDetailModel> updatedDetails,
            [Bind(Prefix = "deleted")]IEnumerable<ImportNCU_QCBDetailModel> deletedDetails,
             string transactionCode, string importDate, string providerName, int boxNumber) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode", "Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<ImportSX1DetailModel>()));
            }
            using (var vfi = new tammaContext()) {
                try {
                    var ci = new CultureInfo("vi-VN");
                    var date = string.IsNullOrWhiteSpace(importDate)
                                     ? DateTime.Today
                                     : Convert.ToDateTime(importDate, ci);

                    var transaction = new Vfi.Models.Transaction {
                        TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                        CreatedUser = HttpContext.User.Identity.Name,
                        CreatedDate = date,
                        WarehouseIssueId = MyUtilities.Warehouse.Plating,
                        WarehouseReceiptId = MyUtilities.Warehouse.QcB,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        Status = (byte)MyUtilities.Transaction.Status.Open,
                        Active = true,
                        EoI = "2",
                        MoP = false,
                    };
                    var import = new ImportNCU_QCB() {
                        ImportDate = date,
                        TransactionCode = transaction.TransactionCode,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        BoxNumber = boxNumber,
                        ProviderName = providerName,
                        PurchasingSignature = 0,

                    };
                    var details = new List<ImportNCU_QCBDetail>();
                    var transactionDetails = new List<Vfi.Models.TransactionDetail>();

                    foreach (var entity in inserteDetails) {
                        if (entity.RealNumber == 0 || entity.Weight == 0)
                            throw new AggregateException("");
                        var detail = new ImportNCU_QCBDetail {
                            ImportId = import.ImportId,
                            ProductId = entity.ProductId,
                            Note = entity.Note,
                            RealNumber = entity.RealNumber,
                            RequestNumber = entity.RequestNumber,
                            Weight = entity.Weight,
                        };
                        var transactionDetail = new Vfi.Models.TransactionDetail {
                            Transaction = transaction,
                            TransactionId = transaction.TransactionId,
                            ReferenceId = entity.ProductId,
                            MoP = false,
                            Quantity = entity.RealNumber,
                            UnitMeasure = null,
                            Active = true,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            QuantityKg = entity.Weight,
                        };

                        details.Add(detail);
                        transactionDetails.Add(transactionDetail);
                    }
                    if (transactionDetails.Any()) {
                        vfi.ImportNCU_QCB.Add(import);
                        vfi.Transactions.Add(transaction);
                        vfi.ImportNCU_QCBDetail.AddRange(details);
                        vfi.TransactionDetails.AddRange(transactionDetails);
                        vfi.SaveChanges();

                    }

                }
                catch (Exception exception) {
                    ModelState.AddModelError("ProductCode", "" + exception.Message);
                }
            }
            Session["SessionExportQCDetail"] = null;
            return View(new GridModel(new List<ExportFormQC_TPDetailModel>()));
        }


        [HttpPost]
        [GridAction]
        public ActionResult CreateImportPlatingDetail(
            [Bind(Prefix = "inserted")]IEnumerable<ImportNCU_QCBDetailModel> inserts,
            [Bind(Prefix = "updated")]IEnumerable<ImportNCU_QCBDetailModel> updates,
            [Bind(Prefix = "deleted")]IEnumerable<ImportNCU_QCBDetailModel> deletes,
             int formId, string importDate, int boxNumber) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode", "Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<ImportSX1DetailModel>()));
            }
            var ci = new CultureInfo("vi-VN");
            var date = string.IsNullOrWhiteSpace(importDate)
                             ? DateTime.Today
                             : Convert.ToDateTime(importDate, ci);
            try {
                if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, date)) {
                    throw new AggregateException(
                        @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                }
                using (var vfi = new tammaContext()) {
                    var platingForm = vfi.PlatingForms.FirstOrDefault(pf => pf.FormId == formId);
                    if (platingForm == null)
                        throw new AggregateException("Lỗi! Không tìm thấy phiếu!");
                    var transaction = new Vfi.Models.Transaction {
                        TransactionCode =
                            MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                        CreatedUser = HttpContext.User.Identity.Name,
                        CreatedDate = date,
                        WarehouseIssueId = platingForm.PlatingType,
                        WarehouseReceiptId = MyUtilities.Warehouse.QcB,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        Status = (byte)MyUtilities.Transaction.Status.Open,
                        Active = true,
                        EoI = "2",
                        MoP = false,
                    };
                    var import = new ImportNCU_QCB() {
                        ImportDate = date,
                        TransactionCode = transaction.TransactionCode,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        BoxNumber = boxNumber,
                        PlatingFormId = formId,
                        Transaction = transaction,
                        TransactionId = transaction.TransactionId,
                        PurchasingSignature = 0,
                    };

                    foreach (var update in updates) {
                        if (update.ProductWeight == 0)
                            throw new AggregateException("Lỗi! Cập nhật trọng lượng SP trước");
                        if (update.RealNumber == 0) continue;
                        var exportDetail =
                            vfi.ExportGCN_NCUDetail.FirstOrDefault(ed => ed.DetailId == update.ExportDetailId);

                        var importQuantiy =
                            exportDetail.ImportNCU_QCBDetail.Where(
                                id => id.ImportNCU_QCB.Transaction.Status != (byte)MyUtilities.Transaction.Status.Cancel)
                                        .Sum(id => id.RealNumber);
                        if (Math.Round(exportDetail.RealNumber - importQuantiy - update.RealNumber) < 0) {
                            throw new AggregateException("Lỗi! Số lượng nhập về vượt quá số lượng xuất hàng");
                        }
                        var productInv =
                            vfi.ProductInventories.FirstOrDefault(pi => pi.ProductInventoryId == update.ProductInvId);
                        if (productInv == null)
                            throw new AggregateException("Lỗi! không tìm thấy lô sản phẩm");
                        var transactionDetail = new Vfi.Models.TransactionDetail {
                            Transaction = transaction,
                            TransactionId = transaction.TransactionId,
                            ReferenceId = update.ProductId,
                            MoP = false,
                            Quantity = update.RealNumber,
                            UnitMeasure = null,
                            Active = true,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            QuantityKg = update.Weight,
                            ProductInvId = update.ProductInvId,
                            LotNumber = (productInv.LotNumber + "").Trim(),
                        };
                        transaction.TransactionDetails.Add(transactionDetail);
                        var productInvImport =
                                vfi.ProductInventories.FirstOrDefault(
                                    pi => pi.WarehouseId == transaction.WarehouseReceiptId &&
                                          pi.ProductId == transactionDetail.ReferenceId &&
                                          pi.LotNumber.Equals((transactionDetail.LotNumber + "").Trim()) &&
                                          transactionDetail.NextProcessId == pi.ByProcessMachineId);
                        if (productInvImport == null) {
                            productInvImport = new ProductInventory {
                                WarehouseId = transaction.WarehouseReceiptId.Value,
                                ProductId = transactionDetail.ReferenceId.Value,
                                ImportDate = transaction.CreatedDate,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                TotalQty = 0,
                                LotNumber = transactionDetail.LotNumber,
                                ErrorId = transactionDetail.ErrorId,
                                ByProcessMachineId = transactionDetail.NextProcessId
                            };
                            productInvImport.MaterialInvId = productInv.MaterialInvId;
                            productInvImport.MachineId = productInv.MachineId;

                            vfi.ProductInventories.Add(productInvImport);
                            vfi.SaveChanges();
                        }
                        var detail = new ImportNCU_QCBDetail {
                            ImportId = import.ImportId,
                            ProductId = update.ProductId,
                            Note = update.Note,
                            RealNumber = update.RealNumber,
                            RequestNumber = update.RequestNumber,
                            Weight = update.Weight,
                            ExportDetailId = update.ExportDetailId,
                            Package = update.Package + "",
                            ProductInvId = productInvImport.ProductInventoryId,
                        };
                        import.ImportNCU_QCBDetail.Add(detail);
                    }
                    if (import.ImportNCU_QCBDetail.Any()) {
                        vfi.Transactions.Add(transaction);
                        vfi.ImportNCU_QCB.Add(import);
                        vfi.SaveChanges();
                    }
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("CreateImportPlatingDetail", "" + exception.Message);
            }
            Session["SessionExportQCDetail"] = null;
            return View(new GridModel(new List<ExportFormQC_TPDetailModel>()));
        }

        [GridAction]
        public ActionResult SelectExportGCN_NCUDetail() {
            return View(new GridModel(new List<ExportGCN_NCUDetailModel>()));
        }

        [GridAction]
        public ActionResult SelectExportPlating(int platingFormId) {
            if (platingFormId == 0)
                return View(new GridModel(new List<ExportGCN_NCUDetailModel>()));
            try {
                using (var vfi = new tammaContext()) {
                    var platingForm =
                        vfi.PlatingForms.FirstOrDefault(
                            pf => pf.FormId == platingFormId &&
                                  pf.Status == (byte)MyUtilities.Sales.Status.InProcess);
                    if (platingForm == null)
                        throw new AggregateException("Lỗi! Không tìm thấy biên bản GCN");
                    var model = new List<ExportGCN_NCUDetailModel>();
                    var i = 1;
                    var details = platingForm.PlatingFormDetails.OrderBy(x => x.Product.ProductCode);
                    foreach (var detail in details) {
                        var productInvs = vfi.ProductInventories.Where(pi => pi.ProductId == detail.ProductId &&
                                                                             pi.WarehouseId ==
                                                                             MyUtilities.Warehouse.WaitingPlating &&
                                                                             pi.TotalQty > 0);
                        foreach (var productInv in productInvs) {
                            var entity = new ExportGCN_NCUDetailModel {
                                ProductId = detail.ProductId,
                                ProductCode = detail.Product.ProductCode,
                                RequestNumber = detail.QuantityRequirement,
                                RealNumber = 0,
                                ProductWeight = detail.Product.WaitingPlatingWeight ?? 0,
                                Weight = 0,
                                ProductInventory = 0,
                                PlatingDetailId = detail.DetailId,
                                Note = detail.Note,
                                ProductInvId = productInv.ProductInventoryId,
                                ProductInvCode = productInv.Product.Customer.CustomerCode
                                                 + " - " + productInv.Product.ProductCode
                                                 + " - " + productInv.LotNumber,
                                LotNumber = productInv.LotNumber,
                                ProductImg = detail.Product.DrawingFinish,
                                UploadDate = (detail.Product.UploadDate ?? DateTime.Now).ToString("yyyyMMddhhmmss"),
                                GroupIndex = i
                            };
                            if (string.IsNullOrWhiteSpace(entity.ProductImg))
                                entity.ProductImg = "askquestion.jpg";
                            entity.RequestNumberString = string.Format("{0:n0}", detail.QuantityRequirement) + " " +
                                                         detail.Unit;
                            entity.ProductInventory = productInv.TotalQty;
                            entity.AvailableInventory = GetWarehouseInvPeriod(productInv.ProductInventoryId);
                            model.Add(entity);
                        }
                        if (!productInvs.Any()) {
                            var entity = new ExportGCN_NCUDetailModel {
                                ProductId = detail.ProductId,
                                ProductCode = detail.Product.ProductCode,
                                RequestNumber = detail.QuantityRequirement,
                                RealNumber = 0,
                                ProductWeight = detail.Product.WaitingPlatingWeight ?? 0,
                                Weight = 0,
                                ProductInventory = 0,
                                PlatingDetailId = detail.DetailId,
                                Note = detail.Note,
                                ProductInvId = 0,
                                ProductInvCode = detail.Product.ProductCode + " - không có tồn kho",
                                LotNumber = "không có tồn kho",
                                AvailableInventory = 0,
                                ProductImg = detail.Product.DrawingFinish,
                                GroupIndex = i
                            };
                            if (string.IsNullOrWhiteSpace(entity.ProductImg))
                                entity.ProductImg = "askquestion.jpg";
                            model.Add(entity);

                        }
                        i++;
                    }
                    return View(new GridModel(model));
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectExportPlating", "" + ex.Message);
            }
            return View(new GridModel(new List<ExportGCN_NCUDetailModel>()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateExportGCN_NCUDetail(
            [Bind(Prefix = "inserted")]IEnumerable<ExportGCN_NCUDetailModel> inserteExportDetails,
            [Bind(Prefix = "updated")]IEnumerable<ExportGCN_NCUDetailModel> updatedExportDetails,
            [Bind(Prefix = "deleted")]IEnumerable<ExportGCN_NCUDetailModel> deletedExportDetails,
             string transactionCode, string exportDate, string providerName, int boxNumber, int blockNumber) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode", "Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<ExportGCN_NCUDetailModel>()));
            }


            using (var vfi = new tammaContext()) {
                try {
                    var ci = new CultureInfo("vi-VN");
                    var date = string.IsNullOrWhiteSpace(exportDate)
                                     ? DateTime.Today
                                     : Convert.ToDateTime(exportDate, ci);

                    var transaction = new Vfi.Models.Transaction {
                        TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                        CreatedUser = HttpContext.User.Identity.Name,
                        CreatedDate = date,
                        WarehouseIssueId = MyUtilities.Warehouse.WaitingPlating,
                        WarehouseReceiptId = MyUtilities.Warehouse.Plating,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        Status = (byte)MyUtilities.Transaction.Status.Open,
                        Active = true,
                        EoI = "2",
                        MoP = false,

                    };
                    var export = new ExportGCN_NCU() {
                        ExportDate = date,
                        TransactionCode = transaction.TransactionCode,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        ProviderName = providerName,
                        BoxNumber = boxNumber,
                        BlockNumber = blockNumber,

                    };
                    var exportDetails = new List<ExportGCN_NCUDetail>();
                    var transactionDetails = new List<Vfi.Models.TransactionDetail>();

                    foreach (var entity in inserteExportDetails) {
                        var exportDetail = new ExportGCN_NCUDetail {
                            ExportId = export.ExportId,
                            ProductId = entity.ProductId,
                            Note = entity.Note,
                            Weight = entity.Weight,
                            RealNumber = entity.RealNumber,
                            RequestNumber = entity.RequestNumber,
                        };
                        var transactionDetail = new Vfi.Models.TransactionDetail {
                            Transaction = transaction,
                            TransactionId = transaction.TransactionId,
                            ReferenceId = entity.ProductId,
                            MoP = false,
                            Quantity = entity.RealNumber,
                            UnitMeasure = null,
                            Active = true,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            QuantityKg = 0,
                        };

                        exportDetails.Add(exportDetail);
                        transactionDetails.Add(transactionDetail);
                    }
                    vfi.ExportGCN_NCU.Add(export);
                    vfi.Transactions.Add(transaction);
                    vfi.ExportGCN_NCUDetail.AddRange(exportDetails);
                    vfi.TransactionDetails.AddRange(transactionDetails);
                    vfi.SaveChanges();

                }
                catch (Exception exception) {
                    ModelState.AddModelError("ProductCode", "" + exception.Message);
                }
            }
            Session["ExportGCN_NCUDetail"] = null;
            return View(new GridModel(new List<ExportFormQC_TPDetailModel>()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult CreateExportPlatingDetail(
            [Bind(Prefix = "inserted")]IEnumerable<ExportGCN_NCUDetailModel> inserts,
            [Bind(Prefix = "updated")]IEnumerable<ExportGCN_NCUDetailModel> updates,
            [Bind(Prefix = "deleted")]IEnumerable<ExportGCN_NCUDetailModel> deletes,
             int formId, string exportDate) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode", "Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<ExportGCN_NCUDetailModel>()));
            }


            try {

                var date = MyUtilities.Function.ParseDate(exportDate);
                if (date > DateTime.Now.AddDays(1)) {
                    throw new AggregateException("Lỗi! Xem lại ngày ( lớn hơn hiện tại) !");
                }
                if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, date)) {
                    throw new AggregateException(
                        @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                }

                using (var vfi = new tammaContext()) {
                    var productInvIds = updates.Select(u => u.ProductInvId).Distinct().ToList();
                    var msg = "";
                    foreach (var productInvId in productInvIds) {
                        var productInv =
                            vfi.ProductInventories.FirstOrDefault(pi => pi.ProductInventoryId == productInvId);
                        if (productInv == null)
                            throw new AggregateException("Lỗi! Không tìm thấy kho sản phẩm");
                        if (!(productInv.Product.WaitingPlatingWeight > 0))
                            msg += "Lỗi! Sản phẩm " + productInv.Product.ProductCode + " chưa cập nhật tồn kho";

                        var details = updates.Where(u => u.ProductInvId == productInvId);
                        if (productInv.TotalQty - details.Sum(d => d.RealNumber) < 0)
                            msg += "Lỗi! Sản phẩm " + productInv.Product.ProductCode
                                   + " lô " + productInv.LotNumber + " tồn kho không đủ";
                    }
                    if (!string.IsNullOrWhiteSpace(msg))
                        throw new AggregateException(msg);
                    var platingForm = vfi.PlatingForms.FirstOrDefault(pf => pf.FormId == formId);
                    if (platingForm == null)
                        throw new AggregateException("Lỗi! Không tìm thấy phiếu!");
                    var transaction = new Vfi.Models.Transaction {
                        TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                        CreatedUser = HttpContext.User.Identity.Name,
                        CreatedDate = date,
                        WarehouseIssueId = MyUtilities.Warehouse.WaitingPlating,
                        WarehouseReceiptId = platingForm.PlatingType,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        Status = (byte)MyUtilities.Transaction.Status.Open,
                        Active = true,
                        EoI = "2",
                        MoP = false,
                    };
                    var export = new ExportGCN_NCU() {
                        ExportDate = date,
                        TransactionCode = transaction.TransactionCode,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        //ProviderName = providerName,
                        BoxNumber = 0,
                        BlockNumber = 0,
                        PlatingFormId = formId,
                        TransactionId = transaction.TransactionId,
                        Transaction = transaction
                    };
                    foreach (var update in updates) {
                        if (update.RealNumber == 0) continue;
                        var productInv =
                            vfi.ProductInventories.FirstOrDefault(pi => pi.ProductInventoryId == update.ProductInvId);
                        if (productInv == null) continue;
                        var transactionDetail = new Vfi.Models.TransactionDetail {
                            Transaction = transaction,
                            TransactionId = transaction.TransactionId,
                            ReferenceId = productInv.ProductId,
                            MoP = false,
                            Quantity = update.RealNumber,
                            UnitMeasure = null,
                            Active = true,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            QuantityKg = 0,
                            ProductInvId = productInv.ProductInventoryId,
                            LotNumber = (productInv.LotNumber + "").Trim(),
                        };
                        transaction.TransactionDetails.Add(transactionDetail);
                        var productInvImport =
                                vfi.ProductInventories.FirstOrDefault(
                                    pi => pi.WarehouseId == transaction.WarehouseReceiptId &&
                                          pi.ProductId == transactionDetail.ReferenceId &&
                                          pi.LotNumber.Equals((transactionDetail.LotNumber + "").Trim()) &&
                                          transactionDetail.NextProcessId == pi.ByProcessMachineId);
                        if (productInvImport == null) {
                            productInvImport = new ProductInventory {
                                WarehouseId = transaction.WarehouseReceiptId.Value,
                                ProductId = transactionDetail.ReferenceId.Value,
                                ImportDate = transaction.CreatedDate,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                TotalQty = 0,
                                LotNumber = transactionDetail.LotNumber,
                                ErrorId = transactionDetail.ErrorId,
                                ByProcessMachineId = transactionDetail.NextProcessId
                            };
                            //if (productInvExport != null)
                            //{
                            productInvImport.MaterialInvId = productInv.MaterialInvId;
                            productInvImport.MachineId = productInv.MachineId;
                            //}
                            vfi.ProductInventories.Add(productInvImport);
                            vfi.SaveChanges();
                        }
                        var exportDetail = new ExportGCN_NCUDetail {
                            ExportId = update.ExportId,
                            ProductId = update.ProductId,
                            Note = update.Note,
                            Weight = update.Weight,
                            RealNumber = update.RealNumber,
                            RequestNumber = update.RequestNumber,
                            PlatingDetailId = update.PlatingDetailId,
                            Package = update.Package + "",
                            ProductInvId = productInvImport.ProductInventoryId,
                        };
                        export.ExportGCN_NCUDetail.Add(exportDetail);
                    }
                    if (export.ExportGCN_NCUDetail.Any()) {
                        vfi.Transactions.Add(transaction);
                        vfi.ExportGCN_NCU.Add(export);
                        vfi.SaveChanges();
                    }

                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("CreateExportPlatingDetail", "" + exception.Message);
            }

            return View(new GridModel(new List<ExportFormQC_TPDetailModel>()));
        }

        public ActionResult CheckProductInventoryById(int productInvId, int quality) {
            // code, quantity error
            int[] result = new int[] { 0, 0 };
            result[0] = 0;
            if (productInvId == 0)
                result[0] = 9;
            else if (quality == 0)
                result[0] = 7;
            else {
                using (var vfi = new tammaContext()) {
                    //var productId = vfi.Products.FirstOrDefault(p => p.produ.Equals(productName)).ProductId;
                    var productInventory = vfi.ProductInventories
                        .FirstOrDefault(pi => pi.ProductInventoryId == productInvId);
                    if (productInventory == null) {
                        result[0] = 0;
                        result[1] = 0;
                    }
                    else {
                        var inventoryQuality = productInventory.TotalQty;

                        inventoryQuality -= quality;
                        if (inventoryQuality < 0) {
                            result[0] = 0;
                            result[1] = Convert.ToInt32(inventoryQuality);
                        }
                        else {
                            var transactionDetails = vfi.TransactionDetails.Where(x => x.Transaction.Status == (byte)MyUtilities.Transaction.Status.Open 
                                && x.Transaction.WarehouseIssueId == productInventory.WarehouseId 
                                && x.ProductInvId == productInvId).ToList();
                            var waitingQuantity = transactionDetails.Sum(x => x.Quantity);
                            inventoryQuality -= waitingQuantity;

                            if (inventoryQuality < 0) {
                                result[0] = 0;
                                result[1] = Convert.ToInt32(inventoryQuality);
                            }

                            result[0] = 1;
                        }
                    }
                }
            }
            return Json(result);
        }


        public ActionResult CheckProductInventory(int warehouseId, int productId, int quality) {
            // code, quantity error
            int[] result = new int[] { 0, 0 };
            result[0] = 0;
            if (warehouseId == 0)
                result[0] = 9;
            else if (productId == 0)
                result[0] = 8;
            else if (quality == 0)
                result[0] = 7;
            else {
                using (var vfi = new tammaContext()) {
                    //var productId = vfi.Products.FirstOrDefault(p => p.produ.Equals(productName)).ProductId;
                    var productInventory =
                        vfi.ProductInventories.FirstOrDefault(
                            pi => pi.WarehouseId == warehouseId & pi.ProductId == productId);
                    if (productInventory == null) {
                        result[0] = 0;
                        result[1] = 0;
                    }
                    else {
                        var inventoryQuality = productInventory.TotalQty;

                        inventoryQuality -= quality;
                        if (inventoryQuality < 0) {
                            result[0] = 0;
                            result[1] = Convert.ToInt32(inventoryQuality);
                        }

                        else {
                            var transactionDetails = vfi.TransactionDetails.Where(x => x.Transaction.Status == (byte)MyUtilities.Transaction.Status.Open
                                && x.Transaction.WarehouseIssueId == productInventory.WarehouseId
                                && x.ProductInvId == productInventory.ProductInventoryId).ToList();
                            var waitingQuantity = transactionDetails.Sum(x => x.Quantity);

                            inventoryQuality -= waitingQuantity;
                            if (inventoryQuality < 0) {
                                result[0] = 0;
                                result[1] = Convert.ToInt32(inventoryQuality);
                            }

                            result[0] = 1;
                        }
                    }
                }
            }
            return Json(result);
        }
        public ActionResult CheckProductInOrder(int? orderId, int productId, int quality) {
            if (orderId == null || orderId == 0)
                return Json(9);
            if (productId == 0)
                return Json(8);
            if (quality == 0)
                return Json(7);
            using (var vfi = new tammaContext()) {
                //var productId = vfi.Products.FirstOrDefault(p => p.ProductCode.Equals(productName)).ProductId;
                var productInventory =
                    vfi.ProductInventories.FirstOrDefault(
                        pi => pi.WarehouseId == MyUtilities.Warehouse.Finish & pi.ProductId == productId);
                if (productInventory == null)
                    return Json(0);

                var inventoryQuality = productInventory.TotalQty;
                inventoryQuality -= quality;
                if (inventoryQuality < 0) return Json(0);

                var transactions = vfi.Transactions.Where(t => t.Status == 1 & t.WarehouseIssueId == (byte)MyUtilities.Warehouse.Finish);
                foreach (var transaction in transactions) {
                    var transactionDetail =
                        transaction.TransactionDetails.FirstOrDefault(td => td.ReferenceId == productId);
                    if (transactionDetail == null) continue;

                    inventoryQuality -= transactionDetail.Quantity;
                    if (inventoryQuality < 0) return Json(0);
                }
                //////
                var orderDetailQuality = vfi.OrderDetails.FirstOrDefault(o => o.OrderId == orderId && o.ProductId == productId).OrderQty;
                var lastExport = vfi.ExportFormTP_KD.Where(e => e.OrderId == orderId);

                double assignQuality = 0.0;
                foreach (var exportFormTpKd in lastExport) {
                    var transaction =
                        vfi.Transactions.FirstOrDefault(t => t.TransactionCode.Equals(exportFormTpKd.TransactionCode));
                    if (transaction.Status != (byte)MyUtilities.Transaction.Status.Approved) continue;
                    var tDetail = exportFormTpKd.ExportFormTP_KDDetail.FirstOrDefault(e => e.ProductId == productId);
                    if (tDetail == null) continue;
                    assignQuality += (tDetail.Quality);
                }

                if ((assignQuality + quality) > orderDetailQuality) return Json(2);

                return Json(1);
            }
        }



        #endregion

        #region Workpiece Material

        List<WorkpieceMaterialModel> GetWorkPriceMaterialModel(string monthlyDate) {
            var model = new List<WorkpieceMaterialModel>();
            var ci = new CultureInfo("vi-VN");
            var reportDate = string.IsNullOrWhiteSpace(monthlyDate)
                              ? DateTime.Today
                              : Convert.ToDateTime(monthlyDate, ci);
            reportDate = reportDate.AddDays(1).AddSeconds(-1);
            //var startDate = reportDate;
            //var endDate = reportDate;
            try {
                using (var vfi = new tammaContext()) {
                    var sx1Details = (from id in vfi.ImportFormSX1Detail
                                      where
                                          id.ImportFormSX1.MaterialUseDate < reportDate &&
                                          id.ImportFormSX1.ImportWorkpieceMaterials.Any() &&
                                          id.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                                          id.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault()
                                            .Transaction.Status ==
                                          (byte)MyUtilities.Transaction.Status.Approved
                                      orderby id.ImportFormSX1.MaterialUseDate
                                      select new {
                                          IdentityCode =
                                      id.MaterialInventory.Material.MaterialType.IdentityCode.Trim(),
                                          MaterialUse = (id.MaterialUse1) + (id.MaterialUse2),
                                          MaterialWeight = id.MaterialInventory.UnitWeight,
                                          Production = (id.Number1) + (id.Number2) +
                                                       (id.Processing1) + (id.Processing2),
                                          ProductionWeight = id.ProductWeight,
                                          id.ImportFormSX1.MaterialUseDate,
                                          id.ImportFormSX1.ImportDate,
                                      }).ToList();
                    var lastDate = sx1Details.LastOrDefault().ImportDate;
                    var startDate =
                        sx1Details.FirstOrDefault(
                            id => id.ImportDate.Month == lastDate.Month && id.ImportDate.Year == lastDate.Year).
                                   MaterialUseDate;
                    //if (reportDate.Month == lastDate.Month && reportDate.Year == lastDate.Year)
                    //{
                    //    startDate = sx1Details.FirstOrDefault(id => id.ImportDate.Month == lastDate.Month &&
                    //                                                id.ImportDate.Year == lastDate.Year).MaterialUseDate;
                    //}

                    var importWorkpieceDetails = from iwd in vfi.ImportWorkpieceMaterialDetails
                                                 where
                                                     iwd.ImportWorkpieceMaterial.ImportDate <= reportDate &&
                                                     iwd.ImportWorkpieceMaterial.ImportSx1Id == null &&
                                                     iwd.ImportWorkpieceMaterial.TransactionId == null
                                                 select new {
                                                     iwd.ImportWorkpieceMaterial.ImportDate,
                                                     IdentityCode = iwd.IdentityCode.Trim(),
                                                     iwd.Weight,
                                                     iwd.Note,
                                                 };
                    var exportWorkpieceDetails = from ewd in vfi.ExportWorkpieceMaterialDetails
                                                 where ewd.ExportWorkpieceMaterial.ExportDate <= reportDate
                                                 select new {
                                                     ewd.ExportWorkpieceMaterial.ExportDate,
                                                     IdentityCode = ewd.IdentityCode.Trim(),
                                                     ewd.Weight,
                                                     ewd.Note
                                                 };
                    var identityCodes = MaterialIdentityCode.GetMaterialIdentityCodes(0);
                    foreach (var identityCode in identityCodes) {
                        var entity = new WorkpieceMaterialModel {
                            Group = identityCode.IdentityCode,
                            MaterialType = identityCode.MaterialTypeShortName,
                            EarlyInventory = 0,
                            LastInventory = 0,
                            Import = 0,
                            Export = 0,
                            TotalImport = 0,
                            TotalExport = 0,
                            InventoryStartMonth = 0,
                            ReportDate = reportDate,
                            ReportDateString = monthlyDate,
                        };
                        //start month
                        var sx1StartMonth =
                            sx1Details.Where(
                                id =>
                                id.IdentityCode.Equals(identityCode.IdentityCode) && id.MaterialUseDate < startDate);
                        if (sx1StartMonth.Any()) {
                            entity.InventoryStartMonth += (sx1StartMonth.Sum(id => id.MaterialUse * id.MaterialWeight)) -
                                                          ((sx1StartMonth.Sum(id => id.Production * id.ProductionWeight)) /
                                                           1000);
                        }
                        var importStartMonth =
                            importWorkpieceDetails.Where(
                                iwd => iwd.IdentityCode.Equals(identityCode.IdentityCode) && iwd.ImportDate < startDate);
                        if (importStartMonth.Any()) {
                            entity.InventoryStartMonth += importStartMonth.Sum(iwd => iwd.Weight.Value);
                        }
                        var exportStartMonth =
                            exportWorkpieceDetails.Where(
                                iwd => iwd.IdentityCode.Equals(identityCode.IdentityCode) && iwd.ExportDate < startDate);
                        if (exportStartMonth.Any()) {
                            entity.InventoryStartMonth -= exportStartMonth.Sum(iwd => iwd.Weight.Value);
                        }
                        // total in month
                        var sx1Total =
                            sx1Details.Where(
                                iwd => iwd.IdentityCode.Equals(identityCode.IdentityCode) &&
                                       iwd.MaterialUseDate >= startDate &&
                                       iwd.MaterialUseDate <= reportDate);
                        if (sx1Total.Any()) {
                            entity.TotalImport += (sx1Total.Sum(id => id.MaterialUse * id.MaterialWeight)) -
                                                  ((sx1Total.Sum(id => id.Production * id.ProductionWeight)) /
                                                   1000);
                        }
                        var importTotal =
                            importWorkpieceDetails.Where(iwd =>
                                                         iwd.IdentityCode.Equals(identityCode.IdentityCode) &&
                                                         iwd.ImportDate >= startDate &&
                                                         iwd.ImportDate <= reportDate);
                        if (importTotal.Any()) {
                            entity.TotalImport += importTotal.Sum(iwd => iwd.Weight.Value);
                        }
                        var exportTotal =
                            exportWorkpieceDetails.Where(
                                iwd => iwd.IdentityCode.Equals(identityCode.IdentityCode) &&
                                                         iwd.ExportDate >= startDate &&
                                                         iwd.ExportDate <= reportDate);
                        if (exportTotal.Any()) {
                            entity.TotalExport = exportTotal.Sum(iwd => iwd.Weight.Value);
                        }
                        // in day
                        var importSx1InDay =
                           sx1Details.Where(
                               iwd => iwd.IdentityCode.Equals(identityCode.IdentityCode) &&
                                      iwd.MaterialUseDate.Day == reportDate.Day &&
                                      iwd.MaterialUseDate.Month == reportDate.Month &&
                                      iwd.MaterialUseDate.Year == reportDate.Year);
                        if (importSx1InDay.Any()) {
                            entity.Import += (importSx1InDay.Sum(id => id.MaterialUse * id.MaterialWeight)) -
                                                          ((importSx1InDay.Sum(id => id.Production * id.ProductionWeight)) /
                                                           1000);
                        }
                        var importsInDay =
                            importWorkpieceDetails.Where(iwd =>
                                                         iwd.IdentityCode.Equals(identityCode.IdentityCode) &&
                                                         iwd.ImportDate.Value.Day == reportDate.Day &&
                                                         iwd.ImportDate.Value.Month == reportDate.Month &&
                                                         iwd.ImportDate.Value.Year == reportDate.Year);
                        if (importsInDay.Any()) {
                            entity.Import += importsInDay.Sum(iwd => iwd.Weight.Value);
                        }
                        var exportsInDay =
                            exportWorkpieceDetails.Where(iwd =>
                                                         iwd.IdentityCode.Equals(identityCode.IdentityCode) &&
                                                         iwd.ExportDate.Value.Day == reportDate.Day &&
                                                         iwd.ExportDate.Value.Month == reportDate.Month &&
                                                         iwd.ExportDate.Value.Year == reportDate.Year);
                        if (exportsInDay.Any()) {
                            entity.Export = exportsInDay.Sum(iwd => iwd.Weight.Value);
                        }
                        entity.EarlyInventory = entity.InventoryStartMonth + (entity.TotalImport - entity.Import) -
                                                (entity.TotalExport - entity.Export);
                        entity.LastInventory = entity.EarlyInventory + entity.Import - entity.Export;
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("PrintWorkpieceMaterial", ex.Message);
            }
            return model;
        }

        [GridAction]
        public ActionResult SelectImportWorkpieceDetail(int importId) {
            var model = new List<WorkpieceMaterialPeriodModel>();
            if (importId == -1)
                return View(new GridModel(model));
            try {
                //using (var vfi = new tammaContext())
                //{
                var identities = MaterialIdentityCode.GetMaterialIdentityCodes(0).OrderBy(l => l.IdentityCode);
                using (var vfi = new tammaContext()) {
                    var periods = vfi.WorkpieceMaterialPeriods.ToList();
                    foreach (var identity in identities) {
                        foreach (var type in MyUtilities.Material.GetMaterialIdentityType()) {
                            var entity = new WorkpieceMaterialPeriodModel {
                                IdentityCode = identity.IdentityCode.Trim(),
                                MaterialTypeName = identity.MaterialTypeShortName,
                                Type = type,
                                Weight = 0,
                                EoI = (int)MyUtilities.Transaction.EoIEnum.Import,
                                IsDestroy = false,
                            };
                            entity.TotalQuantity =
                                periods.Where(p => p.IdentityCode.Equals(entity.IdentityCode) && p.Type == entity.Type)
                                       .Sum(p => p.LastQuantity - p.EarlyQuantity);
                            if (importId > 0) {
                                if (type == (int)MyUtilities.Material.WorkpieceType.Defect) // la phế phẩm sẽ ko phân - duyệt nhập kho phế phẩm
                                    continue;
                                var workpieces =
                                    vfi.ImportFormSX1Detail.Where(
                                        iwd =>
                                        iwd.MaterialInventory.Material.MaterialType.IdentityCode.Equals(
                                            entity.IdentityCode) &&
                                        iwd.ImportId == importId).ToList();
                                var importWorkpiece =
                                    vfi.ImportWorkpieceMaterials.FirstOrDefault(iwm => iwm.ImportSx1Id == importId);
                                var totalImport =
                                    vfi.WorkpieceMaterialPeriods
                                       .Where(wmp =>
                                              wmp.EoI == (byte)MyUtilities.Transaction.EoIEnum.Import &&
                                              wmp.EoIId == importWorkpiece.ImportId &&
                                              wmp.Type != (int)MyUtilities.Material.WorkpieceType.Defect &&
                                              wmp.IdentityCode.Equals(identity.IdentityCode))
                                              .ToList();
                                var import = totalImport.Sum(wmp => wmp.Weight);
                                entity.Import =
                                    Math.Round(
                                    workpieces.Sum(
                                        id =>
                                        (((id.MaterialUse1 + id.MaterialUse2) *
                                          id.MaterialInventory.UnitWeight) -
                                         ((id.Number1 + id.Number2 +
                                           id.Processing1 + id.Processing2 +
                                           id.DefectProduct1 + id.DefectProduct2)
                                          * id.ProductWeight / 1000))), 2);
                                if (Math.Round(entity.Import - import, 2) == 0)
                                    continue;
                                entity.Import -= import;
                            }
                            model.Add(entity);
                        }
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectImportWorkpieceDetail", ex.Message);
            }
            return View(new GridModel(model));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateImportWorkpieceDetail(
            [Bind(Prefix = "inserted")] IEnumerable<ImportWorkpieceDetailModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<WorkpieceMaterialPeriodModel> updateds,
            [Bind(Prefix = "deleted")] IEnumerable<ImportWorkpieceDetailModel> deletedDetails,
            string importDate, int importId) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ImportWorkpiece",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         @"1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         @"Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<ImportWorkpieceDetailModel>()));
            }
            updateds = updateds.Where(u => u.Weight != 0);
            if (!updateds.Any())
                throw new AggregateException("Không có cập nhật");
            try {
                using (var vfi = new tammaContext()) {
                    var date = MyUtilities.Function.ParseDate(importDate);
                    if (date > DateTime.Now.AddDays(1)) {
                        throw new AggregateException("Lỗi! Xem lại ngày ( lớn hơn hiện tại) !");
                    }
                    else if (importId > 0) {
                        date = vfi.ImportFormSX1.FirstOrDefault(i => i.ImportId == importId).MaterialUseDate;
                        var identities = updateds.Select(u => u.IdentityCode);
                        foreach (var identity in identities) {
                            var workpieces =
                                vfi.ImportFormSX1Detail.Where(
                                    iwd =>
                                    iwd.MaterialInventory.Material.MaterialType.IdentityCode.Equals(
                                        identity) &&
                                    iwd.ImportId == importId).ToList();
                            var workpieceQuantity =
                                Math.Round(
                                workpieces.Sum(
                                    id =>
                                    (((id.MaterialUse1 + id.MaterialUse2) *
                                      id.MaterialInventory.UnitWeight) -
                                     ((id.Number1 + id.Number2 +
                                       id.Processing1 + id.Processing2 +
                                       id.DefectProduct1 + id.DefectProduct2)
                                      * id.ProductWeight / 1000))), 2);

                            var importWorkpiece =
                                vfi.ImportWorkpieceMaterials.FirstOrDefault(iwm => iwm.ImportSx1Id == importId);
                            var lastImport =
                                vfi.WorkpieceMaterialPeriods
                                   .Where(wmp =>
                                          wmp.EoI == (byte)MyUtilities.Transaction.EoIEnum.Import &&
                                          wmp.EoIId == importWorkpiece.ImportId &&
                                          wmp.Type != (int)MyUtilities.Material.WorkpieceType.Defect &&
                                          wmp.IdentityCode.Equals(identity))
                                          .ToList()
                                          .Sum(wmp => wmp.Weight);
                            workpieceQuantity -= lastImport;
                            var totalImport =
                                updateds.Where(u => u.IdentityCode.Equals(identity) && u.Type != 3).Sum(u => u.Weight);
                            if (Math.Round(totalImport - workpieceQuantity, 2) != 0)
                                throw new AggregateException("Lỗi! Loại " + identity + " phân sai số lượng! Lệch " +
                                                             Math.Round(totalImport - workpieceQuantity, 2));
                        }
                    }
                    var import = new ImportWorkpieceMaterial {
                        ImportDate = date,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                    };
                    if (importId > 0)
                        import = vfi.ImportWorkpieceMaterials.FirstOrDefault(iwm => iwm.ImportSx1Id == importId);
                    var periods = vfi.WorkpieceMaterialPeriods.ToList();
                    var list = new List<WorkpieceMaterialPeriod>();
                    foreach (var detail in updateds) {
                        var totalQuantity =
                            periods.Where(p => p.IdentityCode.Equals(detail.IdentityCode) && p.Type == detail.Type)
                            .ToList()
                                   .Sum(p => p.LastQuantity - p.EarlyQuantity);
                        var period = new WorkpieceMaterialPeriod {
                            IdentityCode = detail.IdentityCode,
                            EoI = (int)MyUtilities.Transaction.EoIEnum.Import,
                            //EoIId = import.ImportId,
                            //ImportWorkpieceMaterial = import,
                            Type = detail.Type,
                            Note = detail.Note,
                            Weight = Math.Round(detail.Weight, 2),
                            ModifiedDate = DateTime.Now,
                            PeriodDate = date,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            EarlyQuantity = totalQuantity,
                            LastQuantity = Math.Round(totalQuantity + detail.Weight, 2),
                            IsDestroy = detail.IsDestroy,
                        };
                        if (importId > 0)
                            period.EoIId = import.ImportId;
                        import.WorkpieceMaterialPeriods.Add(period);
                        list.Add(period);
                    }
                    if (import.WorkpieceMaterialPeriods.Any()) {
                        if (importId == 0)
                            vfi.ImportWorkpieceMaterials.Add(import);
                        else
                            vfi.WorkpieceMaterialPeriods.AddRange(list);

                        vfi.SaveChanges();
                    }
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("UpdateImportWorkpieceDetail", "" + exception.Message);
            }
            return View(new GridModel(new List<ImportWorkpieceDetailModel>()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateExportWorkpieceDetail(
            [Bind(Prefix = "inserted")] IEnumerable<ImportWorkpieceDetailModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<WorkpieceMaterialPeriodModel> updateds,
            [Bind(Prefix = "deleted")] IEnumerable<ImportWorkpieceDetailModel> deletedDetails,
            string exportDate) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ImportWorkpiece",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         @"1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         @"Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<ImportWorkpieceDetailModel>()));
            }
            updateds = updateds.Where(d => d.Weight > 0);
            if (!updateds.Any())
                throw new AggregateException("Không cập nhật");
            try {
                var date = MyUtilities.Function.ParseDate(exportDate);
                if (date > DateTime.Now.AddDays(1)) {
                    throw new AggregateException("Lỗi! Xem lại ngày ( lớn hơn hiện tại) !");
                }
                using (var vfi = new tammaContext()) {
                    var export = new ExportWorkpieceMaterial() {
                        ExportDate = date,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,

                    };
                    var periods = vfi.WorkpieceMaterialPeriods.ToList();
                    foreach (var detail in updateds) {
                        var totalQuantity =
                             periods.Where(p => p.IdentityCode.Equals(detail.IdentityCode) && p.Type == detail.Type)
                             .ToList()
                                    .Sum(p => p.LastQuantity - p.EarlyQuantity);
                        if (Math.Round(detail.Weight - totalQuantity, 2) > 0)
                            throw new AggregateException("Lỗi! Xuất quá số lượng tồn kho " + detail.MaterialTypeName +
                                                         Math.Round(detail.Weight - totalQuantity, 2));
                        var period = new WorkpieceMaterialPeriod {
                            IdentityCode = detail.IdentityCode,
                            EoI = (int)MyUtilities.Transaction.EoIEnum.Export,
                            Type = detail.Type,
                            Note = detail.Note,
                            Weight = Math.Round(detail.Weight, 2),
                            ModifiedDate = DateTime.Now,
                            PeriodDate = date,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            EarlyQuantity = totalQuantity,
                            LastQuantity = Math.Round(totalQuantity - detail.Weight, 2),
                            IsDestroy = detail.IsDestroy,
                            ExportType = (byte)MyUtilities.Material.WorkpieceExport.Sales
                        };
                        if (detail.IsDestroy)
                            period.ExportType = (byte)MyUtilities.Material.WorkpieceExport.Destroy;
                        else if (detail.IsProcess)
                            period.ExportType = (byte)MyUtilities.Material.WorkpieceExport.Process;

                        export.WorkpieceMaterialPeriods.Add(period);
                    }
                    if (export.WorkpieceMaterialPeriods.Any()) {
                        vfi.ExportWorkpieceMaterials.Add(export);
                        vfi.SaveChanges();
                    }
                }

            }
            catch (Exception exception) {
                ModelState.AddModelError("UpdateExportWorkpieceDetail", "" + exception.Message);
            }
            return View(new GridModel(new List<ImportWorkpieceDetailModel>()));
        }


        public ActionResult SelectComboBoxImportSx1() {
            var model = new List<ImportSX1Model>();
            using (var vfi = new tammaContext()) {

                var sx1s = (from i in vfi.ImportFormSX1
                            where
                                i.ImportDate > MyUtilities.Product.StartWorkpieceDate &&
                                i.ImportWorkpieceMaterials.Any() &&
                                i.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                                i.ImportWorkpieceMaterials.FirstOrDefault()
                                  .Transaction.Status ==
                                (byte)MyUtilities.Transaction.Status.Approved &&
                                i.AddWorkpieceMaterial != true
                            orderby i.MaterialUseDate
                            select i).ToList();
                foreach (var sx1 in sx1s) {
                    var entity = new ImportSX1Model {
                        ImportId = sx1.ImportId,
                        TransactionCode =
                            sx1.TransactionCode + "-" + sx1.MaterialUseDate.ToString("dd/MM/yyyy")
                    };
                    var identities = MaterialIdentityCode.GetMaterialIdentityCodes(0).Select(ic => ic.IdentityCode);
                    var check = false;
                    var importWorkpiece = sx1.ImportWorkpieceMaterials.FirstOrDefault();
                    foreach (var identity in identities) {
                        var workpieces =
                            vfi.ImportFormSX1Detail.Where(
                                iwd =>
                                iwd.MaterialInventory.Material.MaterialType.IdentityCode.Equals(
                                    identity) &&
                                iwd.ImportId == sx1.ImportId).ToList();
                        var workpieceQuantity =
                            Math.Round(
                            workpieces.Sum(
                                id =>
                                (((id.MaterialUse1 + id.MaterialUse2) *
                                  id.MaterialInventory.UnitWeight) -
                                 ((id.Number1 + id.Number2 +
                                   id.Processing1 + id.Processing2 +
                                   id.DefectProduct1 + id.DefectProduct2)
                                  * id.ProductWeight / 1000))), 2);
                        var workpiece =
                            vfi.WorkpieceMaterialPeriods
                               .Where(wmp =>
                                      wmp.EoI == (byte)MyUtilities.Transaction.EoIEnum.Import &&
                                      wmp.EoIId == importWorkpiece.ImportId &&
                                      wmp.IdentityCode.Equals(identity) && wmp.Type != 3).ToList();
                        var totalImport = workpiece.Sum(wmp => wmp.Weight);

                        if (Math.Round(totalImport - workpieceQuantity, 2) != 0)
                            check = true;
                    }
                    if (check)
                        model.Add(entity);
                    else {
                        var production1 = vfi.ImportFormSX1.FirstOrDefault(i => i.ImportId == sx1.ImportId);
                        production1.AddWorkpieceMaterial = true;
                        vfi.SaveChanges();
                    }
                }
            }
            return new JsonResult {
                Data = new SelectList(model, "ImportId", "TransactionCode"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }


        #endregion

        #region smart production

        [HttpPost]
        public ActionResult PostSx1Excel(IEnumerable<HttpPostedFileBase> attachments) {
            // The Name of the Upload component is "attachments"       
            try {
                if (attachments.Any()) {
                    foreach (var file in attachments) {
                        // Some browsers send file names with full path. We only care about the file name.
                        var fileName = Path.GetFileName(file.FileName);
                        if (fileName == null) continue;
                        var destinationPath = Path.Combine(Server.MapPath("~/Content/FileUpload"), fileName);

                        byte[] fileBytes = new byte[file.ContentLength];
                        file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                        System.IO.File.WriteAllBytes(destinationPath, fileBytes);
                    }
                    return Json("Oke");
                }
                else {
                    throw new AggregateException("attachments null");
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UploadSave", ex.Message);
                return Json(ex.Message);
            }
            // Redirect to a view showing the result of the form submission.    
            return Json("False");

        }

        [GridAction]
        public ActionResult SelectExcelProduction(string fileName, string password, string sheetName) {
            var model = new List<ForecastOrderDetailModel>();
            if (string.IsNullOrWhiteSpace(fileName))
                return View(new GridModel(model));
            var dt = new DataTable();
            try {
                using (var conn = new OleDbConnection()) {
                    var destinationPath = Path.Combine(Server.MapPath("~/Content/FileUpload"), fileName);
                    string fileExtension = Path.GetExtension(destinationPath);
                    if (fileExtension == ".xls") {
                        conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + destinationPath;
                        if (!string.IsNullOrWhiteSpace(password))
                            conn.ConnectionString += (";Password=" + password);
                        conn.ConnectionString += ";Extended Properties='Excel 8.0;HDR=YES;'";
                    }
                    if (fileExtension == ".xlsx") {
                        conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + destinationPath;
                        if (!string.IsNullOrWhiteSpace(password))
                            conn.ConnectionString += (";Password=" + password);
                        conn.ConnectionString += ";Extended Properties='Excel 12.0;HDR=YES;'";
                    }
                    using (var comm = new OleDbCommand()) {
                        //var sheetName = "Sheet1";
                        comm.CommandText = "Select * from [" + sheetName + "$]";
                        comm.Connection = conn;
                        using (var da = new OleDbDataAdapter()) {
                            da.SelectCommand = comm;
                            da.Fill(dt);
                        }
                    }
                }
                using (var vfi = new tammaContext()) {
                    errorIndex = "";
                    oke = error = duplicate = 0;
                    //dt.Rows.RemoveAt(0);
                    //dt.Rows.RemoveAt(1);

                    Session["SessionForecastOrder"] = new List<ForecastOrderDetailModel>();
                    foreach (DataRow row in dt.Rows) {
                        var machineName = row[0].ToString().Trim();
                        var productCode = row[1].ToString().Trim();
                        //if (string.IsNullOrWhiteSpace(productCode)) continue;
                        //var product = vfi.Products.FirstOrDefault(p => p.ProductCode.Equals(productCode));
                        //if (product == null)
                        //{
                        //    error++;
                        //    errorIndex += "Error: Không tìm thấy: " + productCode + "\n";
                        //    continue;
                        //}
                        //var entity = model.FirstOrDefault(m => m.ProductId == product.ProductId);
                        //if (entity != null)
                        //{
                        //    duplicate++;
                        //    errorIndex += "Error: Bị trùng mã: " + product.ProductCode + "\n";
                        //    continue;
                        //}
                        //var productInventorys =
                        //  vfi.ProductInventories.Where(
                        //      pi =>
                        //      pi.WarehouseId != 1 &&
                        //      pi.WarehouseId != 9 &&
                        //      pi.WarehouseId != 11 &&
                        //      pi.WarehouseId != 15 &&
                        //      pi.ProductId == product.ProductId);
                        //entity = new ForecastOrderDetailModel
                        //{
                        //    CustomerId = product.CustomerId,
                        //    Index = oke + error + 1,
                        //    ProductCode = product.ProductCode,
                        //    ProductName = product.ProductName,
                        //    ProductId = product.ProductId,
                        //    CustomerCode = product.Customer.CustomerCode,
                        //    CustomerProductCode = product.DesignNo,
                        //    UploadDate = DateTime.Now,
                        //    PlanQuantity = 0,
                        //    RealQuantity = 0,
                        //    TotalInventory = 0,
                        //};
                        //if (productInventorys.Any())
                        //    entity.TotalInventory = productInventorys.Sum(pi => pi.TotalQty.Value);
                        //var finishInventory = productInventorys.FirstOrDefault(pi => pi.WarehouseId == w10w);
                        //if (finishInventory != null)
                        //    entity.FinishInventory = finishInventory.TotalQty ?? 0;
                        //if (columnIndex == 0)
                        //{
                        //    if (!string.IsNullOrWhiteSpace(row[9].ToString()))
                        //        try
                        //        {
                        //            entity.PlanQuantity = Convert.ToDouble(row[9]);
                        //        }
                        //        catch (FormatException)
                        //        {
                        //        }
                        //    if (!string.IsNullOrWhiteSpace(row[10].ToString()))
                        //        try
                        //        {
                        //            entity.RealQuantity = Convert.ToDouble(row[10]);
                        //        }
                        //        catch (FormatException)
                        //        {
                        //        }
                        //}
                        //else
                        //{
                        //    if (!string.IsNullOrWhiteSpace(row[columnIndex - 1].ToString()))
                        //        try
                        //        {
                        //            entity.PlanQuantity = Convert.ToDouble(row[columnIndex - 1]);
                        //        }
                        //        catch (FormatException)
                        //        {
                        //        }
                        //    if (!string.IsNullOrWhiteSpace(row[columnIndex].ToString()))
                        //        try
                        //        {
                        //            entity.RealQuantity = Convert.ToDouble(row[columnIndex]);
                        //        }
                        //        catch (FormatException)
                        //        {
                        //        }

                        //}
                        //oke++;
                        //model.Add(entity);
                    }
                }
            }
            catch (OleDbException oledbEx) {
                ModelState.AddModelError("OleDbException", oledbEx.Message);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectForecastOrder", ex.Message);
            }
            Session["SessionForecastOrder"] = model;
            return View(new GridModel(model));
        }

        CncFormDetailModel GetProcessInventory(int productInvId) {
            var entity = new CncFormDetailModel();

            try {
                using (var vfi = new tammaContext()) {
                    var productInv = vfi.ProductInventories.FirstOrDefault(pi => pi.ProductInventoryId == productInvId);
                    if (productInv != null)
                        entity.InvQuantity = productInv.TotalQty;

                    var process =
                        vfi.ProductionProcessByMachines
                            .FirstOrDefault(p => p.DetailId == productInv.ByProcessMachineId);
                    if (process == null) {
                        process =
                            vfi.ProductionProcessByMachines
                                .FirstOrDefault(p => p.ProductId == productInv.ProductId &&
                                                     p.MachineId == productInv.MachineId &&
                                                     p.WarehouseId == MyUtilities.Warehouse.Cnc &&
                                                     p.Active);
                    }
                    if (process != null) {
                        var nextProcess =
                            vfi.ProductionProcessByMachines
                                .Where(p => p.ProductId == productInv.ProductId &&
                                            p.MachineId == productInv.MachineId &&
                                            p.ProcessIndex > process.ProcessIndex &&
                                            p.Active)
                                .OrderBy(p => p.ProcessIndex)
                                .FirstOrDefault();
                        if (nextProcess != null) {
                            entity.MachineId = nextProcess.DetailId;
                            entity.WarehouseExportId = nextProcess.WarehouseId;
                            entity.WarehouseExportName = nextProcess.Warehouse.ShortName;
                        }
                    }
                    if (entity.WarehouseExportId == 0) {
                        var globalProcess =
                            vfi.ProductionProcesses
                                .FirstOrDefault(p => p.ProductId == productInv.ProductId &&
                                            p.WarehouseId == MyUtilities.Warehouse.Cnc &&
                                            p.IsNecessary);
                        if (globalProcess != null) {
                            var nextProcess =
                                vfi.ProductionProcesses
                                    .Where(p => p.ProductId == productInv.ProductId &&
                                                p.ProcessIndex > globalProcess.ProcessIndex &&
                                                p.IsNecessary)
                                    .OrderBy(p => p.ProcessIndex)
                                    .FirstOrDefault();
                            if (nextProcess != null) {
                                entity.WarehouseExportId = nextProcess.WarehouseId;
                                entity.WarehouseExportName = nextProcess.Warehouse.ShortName;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return entity;
        }

        [HttpPost]
        public ActionResult GetProductInventory(int productInvId) {
            try {
                var entity = GetProcessInventory(productInvId);

                return Json(new object[]
                    {
                        string.Format("{0:n0}", entity.InvQuantity),
                        entity.MachineId,
                        entity.WarehouseExportId,
                        entity.WarehouseExportName,
                    });
            }
            catch (Exception ex) {
                return Json("e");
            }
            //return Json("0");
        }

        //[HttpPost]
        //public ActionResult GetProductInventory(int productId, int warehouseId)
        //{
        //    try
        //    {

        //        using (var vfi = new tammaContext())
        //        {
        //            var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
        //            if (product == null)
        //                return Json("9");
        //            var total = "0";
        //            var productInv =
        //                vfi.ProductInventories.FirstOrDefault(
        //                    pi => pi.ProductId == productId && pi.WarehouseId == warehouseId);
        //            if (productInv != null)
        //                total = string.Format("{0:n0}", productInv.TotalQty);
        //            return Json(new object[] { total });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json("e");
        //    }
        //    //return Json("0");
        //}

        [GridAction]
        public ActionResult SelectCreateCncForm() {
            var model = new List<CncFormDetailModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var machines = (from x in vfi.Machines
                                    where x.Active && x.ProcessingType.Warehouse.IsCncMilling
                                    select new { x.MachineId, x.MachineName }).ToList();
                    var machineIds = machines.Select(x=> x.MachineId).ToList();
                    var smartProductions = (from x in vfi.SmartProductions
                                            where x.MachineId != null && machineIds.Contains(x.MachineId.Value)
                                            select new { x.MachineId, x.ProductInvId }).ToList();
                    //var productIds = smartProductions.Where(x => x.ProductId != null).Select(x => x.ProductId.Value);
                    foreach (var machine in machines) {
                        var entity = new CncFormDetailModel {
                            MachineId = machine.MachineId,
                            MachineName = machine.MachineName,
                            InvQuantity = 0,
                            ProcessByMachineId = 0,
                        };
                        var smartProduction = smartProductions.FirstOrDefault(sp => sp.MachineId == machine.MachineId);
                        if (smartProduction != null) {
                            if (smartProduction.ProductInvId != null) {
                                var productInv =
                                    vfi.ProductInventories.FirstOrDefault(
                                        pi =>
                                            pi.ProductInventoryId == smartProduction.ProductInvId);
                                if (productInv != null) {
                                    entity.ProductInvId = productInv.ProductInventoryId;
                                    entity.ProductInvCode = productInv.Product.Customer.CustomerCode + " - " +
                                                            productInv.Product.ProductCode + " - " +
                                                            productInv.LotNumber;
                                    entity.InvQuantity = productInv.TotalQty;
                                    var processByMachine =
                                        vfi.ProductionProcessByMachines
                                            .FirstOrDefault(p => p.DetailId == productInv.ByProcessMachineId);
                                    if (processByMachine == null) {
                                        processByMachine =
                                            vfi.ProductionProcessByMachines
                                                .FirstOrDefault(p => p.ProductId == productInv.ProductId &&
                                                                     p.MachineId == productInv.MachineId &&
                                                                     p.WarehouseId == MyUtilities.Warehouse.Cnc &&
                                                                     p.Active);
                                    }
                                    if (processByMachine != null) {
                                        var nextProcess =
                                            vfi.ProductionProcessByMachines
                                                .Where(p => p.ProductId == productInv.ProductId &&
                                                            p.MachineId == productInv.MachineId &&
                                                            p.ProcessIndex > processByMachine.ProcessIndex &&
                                                            p.Active)
                                                .OrderBy(p => p.ProcessIndex)
                                                .FirstOrDefault();
                                        if (nextProcess != null) {
                                            entity.ProcessByMachineId = nextProcess.DetailId;
                                            entity.WarehouseExportId = nextProcess.WarehouseId;
                                            entity.WarehouseExportName = nextProcess.Warehouse.ShortName;
                                        }
                                    }
                                    else {
                                        var process =
                                            vfi.ProductionProcesses
                                                .FirstOrDefault(p => p.ProductId == productInv.ProductId &&
                                                                     p.WarehouseId == MyUtilities.Warehouse.Cnc &&
                                                                     p.IsNecessary);
                                        if (process != null) {
                                            var nextProcess =
                                                vfi.ProductionProcesses
                                                    .Where(pi => pi.ProductId == productInv.ProductId &&
                                                                pi.ProcessIndex > process.ProcessIndex &&
                                                                pi.IsNecessary)
                                                    .OrderBy(pi => pi.ProcessIndex)
                                                    .FirstOrDefault();
                                            if (nextProcess != null) {
                                                entity.WarehouseExportId = nextProcess.WarehouseId;
                                                entity.WarehouseExportName = nextProcess.Warehouse.ShortName;
                                            }
                                        }
                                    }
                                }
                            }
                            //if (smartProduction.WarehouseId != null)
                            //{
                            //    entity.WarehouseExportId = smartProduction.WarehouseId.Value;
                            //    entity.WarehouseExportName = smartProduction.Warehouse.WarehouseName;
                            //}
                        }

                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectCreateCncForm", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.MachineName)));
        }



        [HttpPost]
        [GridAction]
        public ActionResult UpdateCncForm(
            [Bind(Prefix = "inserted")] IEnumerable<CncFormDetailModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<CncFormDetailModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<CncFormDetailModel> deletedDetails,
            string importDate, string materialUseDate, string ca1, string ca2) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode",
                    "Bạn đã bị mất quyền đăng nhập. " +
                    "\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                    "\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<SmartProductionModel>()));
            }
            if (!updatedDetails.Any()) {
            }
            try {
                if (insertedDetails != null)
                    updatedDetails = updatedDetails.Union(insertedDetails);
                using (var vfi = new tammaContext()) {
                    var iDate = MyUtilities.Function.ParseDate(importDate);
                    var useDate = MyUtilities.Function.ParseDate(materialUseDate);
                    if (iDate > DateTime.Now.AddDays(1) || useDate > DateTime.Now.AddDays(1)) {
                        throw new AggregateException("Lỗi! Xem lại ngày ( lớn hơn hiện tại) !");
                    }
                    if (useDate >= iDate) {
                        throw new AggregateException("Lỗi! Xem lại ngày báo cáo!");
                    }
                    if (MyUtilities.Transaction.IsLock(useDate, MyUtilities.Transaction.ProductionLockType.CNC)) {
                        throw new AggregateException(
                            @"Ngày nhập SX bị khoá do tính lương! /n Vui lòng liên hệ quản lý tính lương !");
                    }
                    if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, iDate)) {
                        throw new AggregateException(
                            @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                    }
                    var productionDate = vfi.ProductionLocks.FirstOrDefault(x => x.LockDate == useDate);
                    if (productionDate != null) {
                        ca1 = productionDate.Shift1Name;
                        ca2 = productionDate.Shift2Name;
                    }
                    else {
                        ca1 = (ca1 + "").ToUpper();
                        ca2 = (ca2 + "").ToUpper();
                    }
                    //kiem tra ton kho
                    var mess = "";
                    var productInvIds = updatedDetails.Select(detail => detail.ProductInvId).Distinct();
                    foreach (var productInvId in productInvIds) {
                        var productInv =
                            vfi.ProductInventories.FirstOrDefault(pi => pi.ProductInventoryId == productInvId);
                        if (productInv == null || productInv.TotalQty == 0) {
                            mess += productInv.Product.ProductCode + " không có tồn kho \n";
                            continue;
                        }
                        var totalExport = updatedDetails.Where(u => u.ProductInvId == productInvId)
                            .Sum(d => d.Number1 + d.Number2 +
                                      d.Processing1 + d.Processing2 +
                                      d.DefectProduct1 + d.DefectProduct2);
                        var quanity = Math.Round(productInv.TotalQty - totalExport, 0);
                        if (quanity < 0) {
                            mess += productInv.Product.ProductCode + " không đủ tồn kho "
                                    + (totalExport - productInv.TotalQty) + "\n";
                        }
                        else {
                            var transactionDetails =
                                vfi.TransactionDetails
                                    .Where(td => td.Transaction.Status == (byte)MyUtilities.Transaction.Status.Open &&
                                                 td.ProductInvId == productInvId)
                                    .ToList().Sum(td => td.Quantity);
                            quanity = Math.Round(quanity - transactionDetails, 0);
                            if (quanity < 0)
                                mess += ("Lỗi! Số lượng có thể chuyển không đủ xuất.| " +
                                                            productInv.Product.ProductCode + " | " + productInv.LotNumber);
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(mess))
                        throw new AggregateException(mess);

                    var import = new ImportFormCnc {
                        ImportDate = iDate,
                        TransactionCode =
                            MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        Shift1Name = ca1,
                        Shift2Name = ca2,
                        MaterialUseDate = useDate,
                    };
                    var transactionCXL = new Vfi.Models.Transaction {
                        TransactionCode = import.TransactionCode,
                        CreatedUser = HttpContext.User.Identity.Name,
                        CreatedDate = iDate,
                        WarehouseIssueId = MyUtilities.Warehouse.Cnc,
                        WarehouseReceiptId = MyUtilities.Warehouse.Processing,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        Status = (byte)MyUtilities.Transaction.Status.Open,
                        Active = true,
                        EoI = "0",
                        MoP = false,
                        ReferenceId = import.ImportId
                    };
                    var transactionPP = new Vfi.Models.Transaction {
                        TransactionCode = import.TransactionCode,
                        CreatedUser = HttpContext.User.Identity.Name,
                        CreatedDate = iDate,
                        WarehouseIssueId = MyUtilities.Warehouse.Cnc,
                        WarehouseReceiptId = MyUtilities.Warehouse.Defect,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        Status = (byte)MyUtilities.Transaction.Status.Open,
                        Active = true,
                        EoI = "0",
                        MoP = false,
                        ReferenceId = import.ImportId
                    };
                    foreach (var entity in updatedDetails) {
                        if (entity.WarehouseExportId == 0) {
                            entity.WarehouseExportId = GetProcessInventory(entity.ProductInvId).WarehouseExportId;
                            if (entity.WarehouseExportId == 0)
                                throw new AggregateException("Thiếu dữ liệu công đoạn kế của máy " + entity.MachineName);
                        }
                        var productInv =
                            vfi.ProductInventories.FirstOrDefault(pi => pi.ProductInventoryId == entity.ProductInvId);
                        if (productInv == null) continue;
                        if (entity.Number1 + entity.Number2 + entity.Processing1 +
                            entity.Processing2 + entity.DefectProduct1 + entity.DefectProduct2 > 0) {
                            if (entity.ProductInvId == 0 || entity.MachineId == 0) {
                                throw new AggregateException("Lỗi! Chọn lại sản phẩm ở máy " + entity.MachineName);
                            }
                            var lastTrack = MyUtilities.Machine.LastTrackUpMachine(entity.MachineId, null, productInv.ProductId, import.MaterialUseDate);
                            var importDetail = new ImportFormCncDetail {
                                ImportFormCnc = import,
                                MachineId = entity.MachineId,
                                ProductId = productInv.ProductId,
                                ProductInvId = productInv.ProductInventoryId,
                                Number1 = entity.Number1,
                                Number2 = entity.Number2,
                                Processing1 = entity.Processing1,
                                Processing2 = entity.Processing2,
                                DefectProduct1 = entity.DefectProduct1,
                                DefectProduct2 = entity.DefectProduct2,
                                ProductWeight = productInv.Product.CncWeight ?? 0,
                                Rate = lastTrack == null
                                    ? 0
                                    : lastTrack.RealRate,
                                Productivity =
                                    lastTrack == null
                                        ? 0
                                        : lastTrack.RealProductivity,
                            };
                            import.ImportFormCncDetails.Add(importDetail);
                            var smartProduction =
                                vfi.SmartProductions.FirstOrDefault(sp => sp.MachineId == entity.MachineId);
                            if (smartProduction == null) {
                                smartProduction = new SmartProduction();
                                smartProduction.MachineId = entity.MachineId;
                                smartProduction.ProductId = productInv.ProductId;
                                smartProduction.WarehouseId = entity.WarehouseExportId;
                                smartProduction.ProductInvId = productInv.ProductInventoryId;
                                vfi.SmartProductions.Add(smartProduction);
                            }
                            else {
                                smartProduction.ProductId = productInv.ProductId;
                                smartProduction.WarehouseId = entity.WarehouseExportId;
                                smartProduction.ProductInvId = productInv.ProductInventoryId;
                            }
                        }
                        //
                        if (entity.DefectProduct1 + entity.DefectProduct2 != 0) {
                            var transactionDetailPP = new Vfi.Models.TransactionDetail {
                                Transaction = transactionPP,
                                TransactionId = transactionPP.TransactionId,
                                ReferenceId = productInv.ProductId,
                                MoP = false,
                                Quantity = entity.DefectProduct1 + entity.DefectProduct2,
                                UnitMeasure = null,
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                QuantityKg = 0,
                                ProductInvId = productInv.ProductInventoryId,
                                LotNumber = productInv.LotNumber,
                                NextProcessId = productInv.ByProcessMachineId,
                            };
                            transactionPP.TransactionDetails.Add(transactionDetailPP);
                        }
                        if (entity.Processing1 + entity.Processing2 != 0) {
                            var transactionDetailCXL = new Vfi.Models.TransactionDetail {
                                Transaction = transactionCXL,
                                TransactionId = transactionCXL.TransactionId,
                                ReferenceId = productInv.ProductId,
                                MoP = false,
                                Quantity = entity.Processing1 + entity.Processing2,
                                UnitMeasure = null,
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                QuantityKg = 0,
                                ProductInvId = productInv.ProductInventoryId,
                                LotNumber = productInv.LotNumber,
                                NextProcessId = productInv.ByProcessMachineId,
                            };
                            transactionCXL.TransactionDetails.Add(transactionDetailCXL);
                        }
                    }
                    var warehouses = updatedDetails.Select(i => i.WarehouseExportId).Distinct();
                    var transacions = new List<Vfi.Models.Transaction>();
                    foreach (var warehouseId in warehouses) {
                        var transactionExport = new Vfi.Models.Transaction {
                            TransactionCode = import.TransactionCode,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = iDate,
                            WarehouseIssueId = MyUtilities.Warehouse.Cnc,
                            WarehouseReceiptId = warehouseId,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            EoI = "0",
                            MoP = false,
                            ReferenceId = import.ImportId
                        };
                        var importDetailOnWahouses = updatedDetails.Where(i => i.WarehouseExportId == warehouseId);
                        foreach (var update in importDetailOnWahouses) {
                            var productInv =
                                vfi.ProductInventories.FirstOrDefault(pi => pi.ProductInventoryId == update.ProductInvId);
                            if (update.Number1 + update.Number2 > 0) {
                                var transactionDetailExport = new Vfi.Models.TransactionDetail {
                                    Transaction = transactionExport,
                                    TransactionId = transactionExport.TransactionId,
                                    ReferenceId = productInv.ProductId,
                                    MoP = false,
                                    Quantity = update.Number1 + update.Number2,
                                    UnitMeasure = null,
                                    Active = true,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    ModifiedDate = DateTime.Now,
                                    QuantityKg = 0,
                                    ProductInvId = productInv.ProductInventoryId,
                                    LotNumber = productInv.LotNumber,
                                };
                                if (update.ProcessByMachineId != 0) {
                                    transactionDetailExport.NextProcessId = update.ProcessByMachineId;
                                }
                                transactionExport.TransactionDetails.Add(transactionDetailExport);
                            }
                        }
                        if (transactionExport.TransactionDetails.Any())
                            transacions.Add(transactionExport);
                    }
                    if (import.ImportFormCncDetails.Any()) {
                        vfi.ImportFormCncs.Add(import);
                        vfi.SaveChanges();
                        if (transactionPP.TransactionDetails.Any()) {
                            transactionPP.ReferenceId = import.ImportId;
                            vfi.Transactions.Add(transactionPP);
                        }
                        if (transactionCXL.TransactionDetails.Any()) {
                            transactionCXL.ReferenceId = import.ImportId;
                            vfi.Transactions.Add(transactionCXL);
                        }
                        if (transacions.Any()) {
                            transacions.ForEach(x => x.ReferenceId = import.ImportId);
                            vfi.Transactions.AddRange(transacions);
                        }
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("ProductCode", exception.Message);
            }
            //Session["SessionImportSX1Detail"] = null;

            return View(new GridModel(new List<ImportSX1DetailModel>()));
        }

        [GridAction]
        public ActionResult SelectMaterialInvOnMachine(int useId, int shift) {
            if (useId == 0)
                return View(new GridModel(new List<SmartProductionModel>()));
            Session["SessionImportSX1Detail"] = new List<ForecastOrderDetailModel>();
            var model = new List<SmartProductionModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var date = (from mu in vfi.MaterialUseInShifts
                                where mu.UseId == useId
                                select mu.UsedDate).FirstOrDefault();
                    var materialUseDetail = from mud in vfi.MaterialUseDetails
                                            where mud.UseId == useId
                                            select mud;
                    if (shift == 1)
                        materialUseDetail = materialUseDetail.Where(mud => mud.EditQuantity > 0);
                    else if (shift == 2)
                        materialUseDetail = materialUseDetail.Where(mud => mud.EditQuantity2 > 0);
                    if (materialUseDetail.Count() <= 0)
                        return View(new GridModel(new List<SmartProductionModel>()));
                    var machineIds = materialUseDetail.Select(mud => mud.MachineId).Distinct().ToList();
                    var smartProductionModels = vfi.SmartProductions.Where(sp => sp.MachineId != null &&
                                                                            machineIds.Contains(sp.MachineId.Value));
                    foreach (var useDetail in materialUseDetail) {
                        var entity = new SmartProductionModel {
                            MachineName = useDetail.Machine.MachineName,
                            MachineId = useDetail.MachineId,
                            MaterialInventoryId = useDetail.MaterialInvId,
                            MaterialUse1 = 0,
                            MaterialUseId = useDetail.DetailId,
                            MaterialInventoryCode =
                                MyUtilities.Material.GetMaterialInvDesignNo(useDetail.MaterialInventory),
                            MaterialId = useDetail.MaterialInventory.MaterialId,
                            LotNumber = useDetail.Lot.Trim() + "-" + useDetail.MaterialInventory.LotNumber.Trim(),
                        };
                        if (shift == 1)
                            entity.MaterialUse1 = useDetail.EditQuantity;
                        else if (shift == 2)
                            entity.MaterialUse1 = useDetail.EditQuantity2;
                        entity.BoxNumber = MyUtilities.Function.RoundUp(entity.MaterialUse1);
                        var lastTrack = MyUtilities.Machine.LastTrackUpMachine(entity.MachineId, entity.MaterialId, null, date);
                        if (lastTrack != null) {
                            if (lastTrack.ProductId == 1512) {
                                var lastProduction =
                                    vfi.ImportFormSX1Detail.OrderByDescending(id => id.ImportFormSX1.MaterialUseDate)
                                       .FirstOrDefault(id => id.MachineId == entity.MachineId);
                                if (lastProduction != null) {
                                    entity.ProductId = lastProduction.ProductId;
                                    entity.ProductCode = lastProduction.Product.ProductCode;
                                    entity.ProductWeight = lastProduction.Product.ProductionWeight ?? 0;
                                    entity.Length = lastProduction.Product.Length ?? 0;
                                    entity.UnitMeasure = lastProduction.UnitMeasure;
                                    var nextProcess =
                                        lastProduction.Product.ProductionProcesses.Where(p => p.WarehouseId != 1 && p.IsNecessary)
                                                 .OrderBy(p => p.ProcessIndex).FirstOrDefault();
                                    //entity.ProcessByMachineId = nextProcess.id;
                                    entity.WarehouseExportId = nextProcess.WarehouseId;
                                    entity.WarehouseExportName = nextProcess.Warehouse.ShortName;
                                }
                            }
                            else {
                                entity.ProductId = lastTrack.ProductId;
                                entity.ProductCode = lastTrack.ProductCode;
                                entity.ProductWeight = lastTrack.ProductionWeight;
                                entity.Length = lastTrack.ProductLength;
                                var nextProcess =
                                    vfi.ProductionProcessByMachines
                                        .Where(p => p.ProductId == entity.ProductId &&
                                                    p.MachineId == entity.MachineId &&
                                                    p.WarehouseId != MyUtilities.Warehouse.Production1 &&
                                                    p.Active)
                                        .OrderBy(p => p.ProcessIndex)
                                        .FirstOrDefault();
                                if (nextProcess != null) {
                                    entity.ProcessByMachineId = nextProcess.DetailId;
                                    entity.WarehouseExportId = nextProcess.WarehouseId;
                                    entity.WarehouseExportName = nextProcess.Warehouse.ShortName;
                                }
                            }
                            entity.ProductionRate =
                                MyUtilities.Product.GetProductRate(useDetail.MaterialInventory.Length,
                                    lastTrack.WorkPiece,
                                    entity.Length,
                                    lastTrack.KnifeCut);
                        }
                        var smartProductionModel = smartProductionModels.FirstOrDefault(sp => sp.MachineId == entity.MachineId &&
                                                                                        sp.ProductId == entity.ProductId);
                        if (smartProductionModel != null) {
                            entity.UnitMeasure = smartProductionModel.UnitMeasure;
                            entity.BoxWeight = smartProductionModel.BoxWeight ?? 0;
                        }
                        // 2020-2
                        entity.QuantityExport1 = entity.MaterialUse1 * entity.ProductionRate;
                        if (!string.IsNullOrWhiteSpace(entity.UnitMeasure) && entity.UnitMeasure.Equals("g")) {
                            entity.QuantityExport1 = entity.QuantityExport1 * entity.ProductWeight;
                        }
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                throw new AggregateException(ex.Message);
            }
            Session["SessionImportSX1Detail"] = model;
            return View(new GridModel(model.OrderBy(m => m.MachineName)));
        }

        public ActionResult GetShiftNameType(int useId, int shift) {
            using (var vfi = new tammaContext()) {
                var materialUse = vfi.MaterialUseInShifts.FirstOrDefault(mu => mu.UseId == useId);
                if (materialUse == null) {
                    return Json("");
                }
                if (shift == 1)
                    return Json("1" + materialUse.Shift1);
                if (shift == 2)
                    return Json("2" + materialUse.Shift2);

            }
            return Json("");
        }

        [GridAction]
        public ActionResult SelectAllMaterialInvOnMachine() {
            var model = new List<MaterialUseInShiftDetailModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var miOnMachines = vfi.MaterialInvOnMachines.Where(mim => Math.Round(mim.TotalQuantity, 2) > 0);
                    var waitingUse =
                        vfi.MaterialUseDetails.Where(
                            mud => mud.MaterialUseInShift.Status == (byte)MyUtilities.Transaction.Status.Open);

                    foreach (var miOnMachine in miOnMachines) {
                        var materialInv =
                            vfi.MaterialInventories.FirstOrDefault(
                                mi => mi.MaterialInventoryId == miOnMachine.MaterialInvId);
                        if (materialInv == null)
                            throw new AggregateException("Lỗi tồn kho nguyên liệu");
                        var entity = new MaterialUseInShiftDetailModel {
                            MachineId = miOnMachine.MachineId.Value,
                            MachineName = miOnMachine.Machine.MachineName,
                            LastQuantity = miOnMachine.TotalQuantity,
                            MaterialInvId = materialInv.MaterialInventoryId,
                            MaterialCode = materialInv.Material.MaterialCode,
                            LotNumber = materialInv.LotNumber,
                            Length = materialInv.Length,
                            DetailId = miOnMachine.Id,
                            EarlyQuantity = materialInv.TotalQty,
                            MaterialDesign = materialInv.Vendor.VendorCode,
                        };
                        var smartProduction =
                            vfi.SmartProductions.FirstOrDefault(sm => sm.MachineId == miOnMachine.MachineId);
                        if (smartProduction != null) {
                            if (smartProduction.ProductId != null)
                                entity.ProductCode = smartProduction.Product.ProductCode;
                        }
                        var waitingById =
                            waitingUse.Where(
                                mud => mud.MachineId == entity.MachineId && mud.MaterialInvId == entity.MaterialInvId);
                        if (waitingById.Any()) {
                            entity.WaitingNumber = waitingById.Sum(mud => mud.EditQuantity + mud.EditQuantity2);
                        }
                        entity.AssignQuantity = entity.LastQuantity - entity.WaitingNumber;
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("ProductCode",
                                         "" + ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.MachineName)));
        }

        [GridAction]
        public ActionResult SelectMaterialInvOnMachineToUse(string onDate, string ca1Name, string ca2Name, string factory) {
            if (string.IsNullOrWhiteSpace(onDate))
                return View(new GridModel(new List<MaterialUseInShiftDetailModel>()));
            if (string.IsNullOrWhiteSpace(ca1Name) && string.IsNullOrWhiteSpace(ca2Name))
                return View(new GridModel(new List<MaterialUseInShiftDetailModel>()));
            var model = new List<MaterialUseInShiftDetailModel>();
            var ci = new CultureInfo("vi-VN");
            var date = Convert.ToDateTime(onDate, ci);
            //var earlyDate = new DateTime(date.Year, date.Month, date.Day).AddSeconds(-1);
            using (var vfi = new tammaContext()) {
                var assignMaterials = (from ed in vfi.ExportMaterialDetails
                                      where
                                          //am.ShiftType == 1 &&
                                          //am.ShiftName.Equals(ca1Name) &&
                                          ed.ExportMaterial.ExportDate == date &&
                                          ed.MachineId != null &&
                                          ed.ExportMaterial.Transaction.Status ==
                                          (byte)MyUtilities.Transaction.Status.Approved
                                      select new {
                                          ed.MachineId,
                                          ed.MaterialInvId,
                                          ed.Quantity
                                      }).ToList();
                var materialOnMachines = vfi.MaterialInvOnMachines.Where(mim => Math.Round(mim.TotalQuantity, 2) > 0);
                //if (!string.IsNullOrWhiteSpace(factory)) {
                //    if (factory.Equals(MyUtilities.Machine.FactoryVF2)) {
                //        materialOnMachines = materialOnMachines.Where(x => x.Machine.MachineName.Contains(MyUtilities.Machine.FactoryVF2));
                //    }
                //    else {
                //        materialOnMachines = materialOnMachines.Where(x => !x.Machine.MachineName.Contains(MyUtilities.Machine.FactoryVF2));
                //    }
                //}
                var waitingUses = (from mud in vfi.MaterialUseDetails
                                   where mud.MaterialUseInShift.Status == (byte)MyUtilities.Transaction.Status.Open
                                   select new {
                                       mud.MachineId,
                                       mud.MaterialInvId,
                                       mud.EditQuantity,
                                       mud.EditQuantity2
                                   }).ToList();
                foreach (var materialInvOnMachine in materialOnMachines) {
                    var entity = new MaterialUseInShiftDetailModel {
                        MachineId = materialInvOnMachine.MachineId.Value,
                        MachineName = materialInvOnMachine.Machine.MachineName,
                        MaterialInvId = materialInvOnMachine.MaterialInvId.Value,
                        MaterialCode = MyUtilities.Material.GetMaterialInvDesignNo(materialInvOnMachine.MaterialInventory),
                        EditQuantity2 = materialInvOnMachine.TotalQuantity,
                        LastQuantity = materialInvOnMachine.TotalQuantity,
                        AssignQuantity = 0,
                        BoxNumber = 0,
                        EditQuantity = 0,
                        LotNumber = materialInvOnMachine.MaterialInventory.LotNumber,
                        Lot = "",
                        Length = materialInvOnMachine.MaterialInventory.Length / 1000,
                    };
                    var assign =
                        assignMaterials.Where(
                            ed => ed.MachineId == entity.MachineId && ed.MaterialInvId == entity.MaterialInvId);
                    if (assign.Any()) {
                        entity.AssignQuantity = assign.Sum(ed => ed.Quantity);
                    }
                    //var lastTrack =
                    //    vfi.TrackUpMachines.Where(
                    //        t =>
                    //        t.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                    //        t.MachineId == entity.MachineId && t.DeliveryDate <= date &&
                    //        t.MaterialId == materialInvOnMachine.MaterialInventory.MaterialId)
                    //       .OrderByDescending(t => t.DeliveryDate)
                    //       .FirstOrDefault();
                    var lastTrack = MyUtilities.Machine.LastTrackUpMachine(entity.MachineId, materialInvOnMachine.MaterialInventory.MaterialId, null, date);
                    if (lastTrack != null) {
                        entity.ProductCode = lastTrack.ProductCode;
                        var realRate = MyUtilities.Product.GetProductRate(3000, lastTrack.WorkPiece, lastTrack.ProductLength, lastTrack.KnifeCut);
                        entity.EditQuantity =
                                MyUtilities.Product.GetMaterialRateInFactoryFullShiftTime(
                                                            lastTrack.RealProductivity,
                                                            realRate);
                    }
                    var waitingById =
                        waitingUses.Where(
                            mud => mud.MachineId == entity.MachineId && mud.MaterialInvId == entity.MaterialInvId);
                    if (waitingById.Any()) {
                        entity.WaitingNumber = waitingById.Sum(mud => mud.EditQuantity + mud.EditQuantity2);
                    }
                    entity.EarlyQuantity = entity.EditQuantity2 - entity.WaitingNumber;
                    entity.LastQuantity = entity.EarlyQuantity - entity.Quantity - entity.Quantity2;
                    //var smartProduct = vfi.SmartProductions.FirstOrDefault(sp => sp.MachineId == materialInvOnMachine.MachineId);
                    //if (smartProduct != null)
                    //{
                    //    if (smartProduct.ProductId != null)
                    //    {
                    //        entity.ProductCode = smartProduct.Product.ProductCode;
                    //    }
                    //}
                    model.Add(entity);
                }
            }
            return View(new GridModel(model.OrderBy(m => m.MachineName)));
        }

        [GridAction]
        public ActionResult SelectSmartProduction() {
            var model = new List<SmartProductionModel>();
            using (var vfi = new tammaContext()) {
                var machines = vfi.Machines.Where(m => m.Active).OrderBy(m => m.MachineName);
                foreach (var machine in machines) {
                    var smartProduct = vfi.SmartProductions.FirstOrDefault(sp => sp.MachineId == machine.MachineId);
                    var entity = new SmartProductionModel();
                    entity.MachineName = machine.MachineName;
                    entity.MachineId = machine.MachineId;
                    if (smartProduct == null) {
                        entity.ProductId = 0;
                        entity.MaterialId = 0;
                        entity.WarehouseExportId = 0;
                        entity.MaterialInventoryId = 0;
                        entity.FuelInvId = 0;
                        entity.ToolInvId = 0;
                    }
                    else {
                        entity.ProductId = smartProduct.ProductId ?? 0;
                        entity.MaterialInventoryId = smartProduct.MaterialInvId ?? 0;
                        entity.MaterialId = 0;
                        entity.WarehouseExportId = smartProduct.WarehouseId ?? 0;
                        entity.FuelInvId = smartProduct.FuelInvId ?? 0;
                        entity.ToolInvId = smartProduct.ToolInvId ?? 0;
                    }
                    if (entity.ProductId != 0) {
                        entity.ProductCode = smartProduct.Product.ProductCode;
                    }
                    if (entity.MaterialInventoryId != 0) {
                        var materialInv =
                            vfi.MaterialInventories.FirstOrDefault(
                                mi => mi.MaterialInventoryId == entity.MaterialInventoryId);
                        if (materialInv != null) {
                            entity.MaterialInventoryCode = MyUtilities.Material.GetMaterialInvDesignNo(materialInv);
                            entity.MaterialInvTotal = (materialInv.TotalQty);
                            var materialInvOnMachine =
                                vfi.MaterialInvOnMachines.FirstOrDefault(
                                    mim =>
                                    mim.MachineId == entity.MachineId &&
                                    mim.MaterialInvId == materialInv.MaterialInventoryId);
                            if (materialInvOnMachine != null) {
                                entity.MaterialInvOnMachineId = materialInvOnMachine.Id;
                                entity.MaterialInvOnMachine = materialInvOnMachine.TotalQuantity;
                            }
                        }
                    }
                    if (entity.FuelInvId != 0) {
                        var fuelInv =
                            vfi.FuelInventories.FirstOrDefault(
                                mi => mi.FuelInvId == entity.FuelInvId);
                        if (fuelInv != null) {
                            entity.FuelFullCode = fuelInv.Vendor.VendorCode +
                                                           fuelInv.Fuel.FuelFullCode + "-" +
                                                           fuelInv.LotNumber;
                            entity.FuelInvTotal = fuelInv.TotalQuantity;
                        }
                    }
                    if (entity.ToolInvId != 0) {
                        var toolInv =
                            vfi.ToolInventories.FirstOrDefault(
                                mi => mi.ToolInvId == entity.ToolInvId);
                        if (toolInv != null) {
                            entity.ToolFullCode = toolInv.Vendor.VendorCode +
                                                           toolInv.Tool.ToolFullCode + "-" +
                                                           toolInv.LotNumber;
                            entity.ToolInvTotal = toolInv.TotalQuantity;
                        }
                    }
                    model.Add(entity);
                }
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectSmartProductionForNa() {
            var model = new List<SmartProductionModel>();
            using (var vfi = new tammaContext()) {
                vfi.Configuration.LazyLoadingEnabled = false;
                var machines = vfi.Machines.Where(m => m.Active).OrderBy(m => m.MachineName);
                foreach (var machine in machines) {
                    var smartProduct =
                        (from s in vfi.SmartProductions.Where(sp => sp.MachineId == machine.MachineId)
                         select new {
                             ProductId = s.ProductId,
                             Product = s.Product,
                             WarehouseId = s.WarehouseId,
                             Warehouse = s.Warehouse,
                         }).FirstOrDefault();
                    var entity = new SmartProductionModel();
                    entity.MachineName = machine.MachineName;
                    entity.MachineId = machine.MachineId;
                    entity.MaterialInvTotal = 0;
                    if (smartProduct == null) {
                        entity.ProductId = 0;
                        // entity.MaterialId = 0;
                        entity.WarehouseExportId = 0;
                        //entity.MaterialInventoryId = 0;
                    }
                    else {
                        entity.ProductId = smartProduct.ProductId ?? 0;
                        entity.ProductCode = entity.ProductId == 0 ? "" : smartProduct.Product.ProductCode;
                        //entity.MaterialInventoryId = smartProduct.MaterialInvId ?? 0;
                        //entity.MaterialId = 0;
                        entity.WarehouseExportId = smartProduct.WarehouseId ?? 0;
                        entity.WarehouseExportName = entity.WarehouseExportId == 0
                                                         ? ""
                                                         : smartProduct.Warehouse.WarehouseName;
                    }
                    model.Add(entity);
                }
            }
            return View(new GridModel(model));
        }
        [GridAction]
        public ActionResult SelectSmartProductionHaveMaterial(string importDate) {
            var model = new List<SmartProductionModel>();
            if (string.IsNullOrWhiteSpace(importDate))
                return View(new GridModel(model));
            var ci = new CultureInfo("vi-VN");
            var date = string.IsNullOrWhiteSpace(importDate)
                             ? DateTime.Today
                             : Convert.ToDateTime(importDate, ci);
            using (var vfi = new tammaContext()) {
                var import =
                    vfi.ImportFormSX1.Where(
                        i =>
                        i.ImportDate.Day == date.Day &&
                        i.ImportDate.Month == date.Month &&
                        i.ImportDate.Year == date.Year);
                foreach (var importFormSx1 in import) {
                    var transaction =
                        vfi.Transactions.FirstOrDefault(t => t.TransactionCode.Equals(importFormSx1.TransactionCode));
                    if (transaction == null) continue;
                    if (transaction.Status != (byte)MyUtilities.Transaction.Status.Approved) continue;
                    foreach (var importDetail in importFormSx1.ImportFormSX1Detail) {
                        var smartProduction =
                            vfi.SmartProductions.FirstOrDefault(sm => sm.MachineId == importDetail.MachineId);
                        var machine = vfi.Machines.FirstOrDefault(m => m.MachineId == importDetail.MachineId);
                        var materialInv =
                            vfi.MaterialInventories.FirstOrDefault(
                                mi => mi.MaterialInventoryId == importDetail.MaterialInvId);
                        var entity = new SmartProductionModel();
                        entity.MaterialUse1 = importDetail.MaterialUse1;
                        entity.MaterialUse2 = importDetail.MaterialUse2;
                        entity.MaterialInventoryId = importDetail.MaterialInvId.Value;
                        entity.MaterialInventoryCode = materialInv.Material.MaterialCode + "-" +
                                                       importDetail.MaterialInventory.LotNumber;
                        entity.MachineId = importDetail.MachineId ?? 0;
                        entity.MachineName = machine.MachineName;
                        // entity.Note = transactionDetail.Note ?? "";
                        if (smartProduction.WarehouseId == null) entity.WarehouseExportName = "";
                        if (smartProduction.ProductId == null) entity.ProductCode = "";
                        model.Add(entity);
                    }
                }
            }
            return View(new GridModel(model));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateSmartProductionForNa(
            [Bind(Prefix = "inserted")] IEnumerable<SmartProductionModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<SmartProductionModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<SmartProductionModel> deletedDetails,
            string importDate, string ca1, string ca2) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode",
                                         "Bạn đã bị mất quyền đăng nhập. " +
                                         "\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         "\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<SmartProductionModel>()));
            }
            if (updatedDetails.Any()) {
                try {
                    if (insertedDetails != null)
                        updatedDetails = updatedDetails.Union(insertedDetails);
                    using (var vfi = new tammaContext()) {
                        var ci = new CultureInfo("vi-VN");
                        var date = string.IsNullOrWhiteSpace(importDate)
                                       ? DateTime.Today
                                       : Convert.ToDateTime(importDate, ci);
                        var import = new ImportFormSX1 {
                            ImportDate = date,
                            TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            Shift1Name = ca1,
                            Shift2Name = ca2,
                            Status = (byte)MyUtilities.Transaction.Status.Approved
                        };
                        var transactionSX1 = new Vfi.Models.Transaction {
                            TransactionCode = import.TransactionCode,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = date,
                            WarehouseIssueId = null,
                            WarehouseReceiptId = 1,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            EoI = "0",
                            MoP = false

                        };
                        var transactionCXL = new Vfi.Models.Transaction {
                            TransactionCode = import.TransactionCode,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = date,
                            WarehouseIssueId = 1,
                            WarehouseReceiptId = 8,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            EoI = "0",
                            MoP = false,
                        };
                        var transactionPP = new Vfi.Models.Transaction {
                            TransactionCode = import.TransactionCode,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = date,
                            WarehouseIssueId = null,
                            WarehouseReceiptId = 9,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            EoI = "0",
                            MoP = false,
                        };

                        var importDetails = new List<ImportFormSX1Detail>();
                        var transactionDetailsSX1 = new List<Vfi.Models.TransactionDetail>();
                        var transactionDetailsCXL = new List<Vfi.Models.TransactionDetail>();
                        var transactionDetailsPP = new List<Vfi.Models.TransactionDetail>();
                        foreach (var entity in updatedDetails) {
                            if (entity.QuantityExport1 + entity.QuantityExport2 + entity.QuantityPending1 +
                                entity.QuantityPending2 + entity.QuantityDefect1 + entity.QuantityDefect2 > 0) {
                                if (entity.ProductId == 0 || entity.WarehouseExportId == 0 || entity.MachineId == 0) {
                                    throw new AggregateException(
                                        "Có nhập số lượng thì phải nhập đủ các điều kiện khác " +
                                        entity.MachineName);
                                }
                                var importDetail = new ImportFormSX1Detail {
                                    ImportFormSX1 = import,
                                    Machine = entity.MachineName,
                                    MachineId = entity.MachineId,
                                    ProductId = entity.ProductId,
                                    //Shift1 = ca1,
                                    //Shift2 = ca2,
                                    Number1 = entity.QuantityExport1 + entity.QuantityPending1,
                                    Number2 = entity.QuantityExport2 + entity.QuantityPending2,
                                    DefectProduct1 = entity.QuantityDefect1,
                                    DefectProduct2 = entity.QuantityDefect2,
                                };
                                importDetails.Add(importDetail);
                                var smartProduction =
                                    vfi.SmartProductions.FirstOrDefault(sp => sp.MachineId == entity.MachineId);
                                if (smartProduction == null) {
                                    smartProduction = new SmartProduction();
                                    smartProduction.MachineId = entity.MachineId;
                                    smartProduction.ProductId = entity.ProductId;
                                    smartProduction.WarehouseId = entity.WarehouseExportId;
                                    vfi.SmartProductions.Add(smartProduction);
                                }
                                else {
                                    smartProduction.ProductId = entity.ProductId;
                                    smartProduction.WarehouseId = entity.WarehouseExportId;
                                }
                            }
                            else {
                                var smartProduction =
                                    vfi.SmartProductions.FirstOrDefault(sp => sp.MachineId == entity.MachineId);
                                if (smartProduction == null) {
                                }
                                else {
                                    if (string.IsNullOrWhiteSpace(entity.ProductCode))
                                        smartProduction.ProductId = null;
                                    if (string.IsNullOrWhiteSpace(entity.WarehouseExportName))
                                        smartProduction.WarehouseId = null;
                                }

                            }
                            if (entity.QuantityExport1 + entity.QuantityExport2 +
                                entity.QuantityPending1 + entity.QuantityPending2 != 0) {
                                var transactionDetailSX1 =
                                    transactionDetailsSX1.FirstOrDefault(td => td.ReferenceId == entity.ProductId);
                                if (transactionDetailSX1 == null) {
                                    transactionDetailSX1 = new Vfi.Models.TransactionDetail {
                                        Transaction = transactionSX1,
                                        TransactionId = transactionSX1.TransactionId,
                                        ReferenceId = entity.ProductId,
                                        MoP = false,
                                        Quantity =
                                            entity.QuantityExport1 + entity.QuantityExport2 +
                                            entity.QuantityPending1 + entity.QuantityPending2,
                                        UnitMeasure = null,
                                        Active = true,
                                        ModifiedUser = HttpContext.User.Identity.Name,
                                        ModifiedDate = DateTime.Now,
                                        QuantityKg = 0,
                                    };
                                    transactionDetailsSX1.Add(transactionDetailSX1);
                                }
                                else {
                                    transactionDetailSX1.Quantity += entity.QuantityExport1 + entity.QuantityExport2 +
                                                                     entity.QuantityPending1 + entity.QuantityPending2;
                                }
                            }

                            if (entity.QuantityDefect1 + entity.QuantityDefect2 != 0) {
                                var transactionDetailPP =
                                    transactionDetailsPP.FirstOrDefault(td => td.ReferenceId == entity.ProductId);
                                if (transactionDetailPP == null) {
                                    transactionDetailPP = new Vfi.Models.TransactionDetail {
                                        Transaction = transactionPP,
                                        TransactionId = transactionPP.TransactionId,
                                        ReferenceId = entity.ProductId,
                                        MoP = false,
                                        Quantity = entity.QuantityDefect1 + entity.QuantityDefect2,
                                        UnitMeasure = null,
                                        Active = true,
                                        ModifiedUser = HttpContext.User.Identity.Name,
                                        ModifiedDate = DateTime.Now,
                                        QuantityKg = 0,

                                    };
                                    transactionDetailsPP.Add(transactionDetailPP);
                                }
                                else {
                                    transactionDetailPP.Quantity += entity.QuantityDefect1 + entity.QuantityDefect2;
                                }
                            }
                            if (entity.QuantityPending1 + entity.QuantityPending2 != 0) {
                                var transactionDetailCXL =
                                    transactionDetailsCXL.FirstOrDefault(td => td.ReferenceId == entity.ProductId);
                                if (transactionDetailCXL == null) {
                                    transactionDetailCXL = new Vfi.Models.TransactionDetail {
                                        Transaction = transactionCXL,
                                        TransactionId = transactionCXL.TransactionId,
                                        ReferenceId = entity.ProductId,
                                        MoP = false,
                                        Quantity = entity.QuantityPending1 + entity.QuantityPending2,
                                        UnitMeasure = null,
                                        Active = true,
                                        ModifiedUser = HttpContext.User.Identity.Name,
                                        ModifiedDate = DateTime.Now,
                                        QuantityKg = 0,

                                    };
                                    transactionDetailsCXL.Add(transactionDetailCXL);
                                }
                                else {
                                    transactionDetailCXL.Quantity += entity.QuantityPending1 + entity.QuantityPending2;
                                }
                            }


                        }
                        var warehouses = updatedDetails.Select(i => i.WarehouseExportId).Distinct();
                        var transacions = new List<Vfi.Models.Transaction>();
                        foreach (var warehouseId in warehouses) {
                            var transactionExport = new Vfi.Models.Transaction {
                                TransactionCode = import.TransactionCode,
                                CreatedUser = HttpContext.User.Identity.Name,
                                CreatedDate = date,
                                WarehouseIssueId = 1,
                                WarehouseReceiptId = warehouseId,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                Status = (byte)MyUtilities.Transaction.Status.Open,
                                Active = true,
                                EoI = "0",
                                MoP = false,
                            };
                            var importDetailOnWahouses = updatedDetails.Where(i => i.WarehouseExportId == warehouseId);
                            foreach (var importDetailOnWahouse in importDetailOnWahouses) {
                                if (importDetailOnWahouse.QuantityExport1 + importDetailOnWahouse.QuantityExport2 > 0) {
                                    var transactionDetailExport =
                                        transactionExport.TransactionDetails.FirstOrDefault(
                                            td => td.ReferenceId == importDetailOnWahouse.ProductId);
                                    if (transactionDetailExport == null) {
                                        transactionDetailExport = new Vfi.Models.TransactionDetail {
                                            Transaction = transactionExport,
                                            TransactionId = transactionExport.TransactionId,
                                            ReferenceId = importDetailOnWahouse.ProductId,
                                            MoP = false,
                                            Quantity =
                                                importDetailOnWahouse.QuantityExport1 +
                                                importDetailOnWahouse.QuantityExport2,
                                            UnitMeasure = null,
                                            Active = true,
                                            ModifiedUser = HttpContext.User.Identity.Name,
                                            ModifiedDate = DateTime.Now,
                                            QuantityKg = 0,
                                        };
                                        transactionExport.TransactionDetails.Add(transactionDetailExport);
                                    }
                                    else {
                                        transactionDetailExport.Quantity += importDetailOnWahouse.QuantityExport1 +
                                                                            importDetailOnWahouse.QuantityExport2;
                                    }
                                }
                            }
                            if (transactionExport.TransactionDetails.Any())
                                transacions.Add(transactionExport);
                        }

                        if (importDetails.Count > 0) {
                            vfi.ImportFormSX1.Add(import);
                            vfi.ImportFormSX1Detail.AddRange(importDetails);
                        }
                        if (transactionDetailsSX1.Count > 0) {
                            vfi.Transactions.Add(transactionSX1);
                            vfi.TransactionDetails.AddRange(transactionDetailsSX1);
                        }
                        if (transactionDetailsPP.Count > 0) {
                            vfi.Transactions.Add(transactionPP);
                            vfi.TransactionDetails.AddRange(transactionDetailsPP);
                        }
                        if (transactionDetailsCXL.Count > 0) {
                            vfi.Transactions.Add(transactionCXL);
                            vfi.TransactionDetails.AddRange(transactionDetailsCXL);
                        }
                        if (transacions.Any()) {
                            vfi.Transactions.AddRange(transacions);
                        }
                        vfi.SaveChanges();
                    }
                }
                catch (Exception exception) {
                    ModelState.AddModelError("ProductCode",
                                             "" + exception.Message);
                }
                //Session["SessionImportSX1Detail"] = null;
            }
            return View(new GridModel(new List<ImportSX1DetailModel>()));
        }

        [HttpPost]
        public ActionResult GetProductionWeight(int productId, int machineId, int materialInvId) {
            try {
                using (var vfi = new tammaContext()) {
                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
                    if (product == null)
                        return Json("9");

                    var nextProcess =
                        vfi.ProductionProcessByMachines
                            .Where(p => p.ProductId == productId &&
                                        p.MachineId == machineId &&
                                        p.WarehouseId != MyUtilities.Warehouse.Production1 &&
                                        p.Active)
                            .OrderBy(p => p.ProcessIndex)
                            .FirstOrDefault();
                    var warehouseId = 0;
                    var warehouseName = "";
                    var nextProcessId = 0;
                    if (nextProcess != null) {
                        nextProcessId = nextProcess.DetailId;
                        warehouseId = nextProcess.WarehouseId;
                        warehouseName = nextProcess.Warehouse.ShortName;
                    }
                    else {
                        var nextDefaultProcess =
                            vfi.ProductionProcesses.Where(p => p.WarehouseId != MyUtilities.Warehouse.Production1 &&
                                                               p.ProductId == productId
                                                               && p.IsNecessary)
                                .OrderBy(p => p.ProcessIndex)
                                .FirstOrDefault();
                        warehouseId = nextDefaultProcess.WarehouseId;
                        warehouseName = nextDefaultProcess.Warehouse.ShortName;
                    }
                    var rate = 0;
                    var materialInv =
                        vfi.MaterialInventories.FirstOrDefault(mi => mi.MaterialInventoryId == materialInvId);
                    if (materialInv != null) {
                        var lastTrack = MyUtilities.Machine.LastTrackUpMachine(machineId, materialInv.MaterialId, null, DateTime.Now);
                        //var lastTrack =
                        // (from t in vfi.TrackUpMachines
                        //  where
                        //  t.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                        //  t.MachineId == machineId &&
                        //  ((t.DeliveryDate != null ? t.DeliveryDate.Value <= DateTime.Now : t.StartDate <= DateTime.Now) ||
                        //   (t.StartDate <= DateTime.Now)) &&
                        //  t.MaterialId == materialInv.MaterialId
                        //  select new {
                        //      t.MachineId,
                        //      t.ProductId,
                        //      t.Product,
                        //      t.Product.ProductCode,
                        //      t.RealProductivity,
                        //      t.RealRate,
                        //      t.TrackUpMaterials,
                        //      t.MaterialId,
                        //      t.Material,
                        //      t.WorkPiece,
                        //      t.KnifeCut,
                        //      Length = t.Product.Length ?? 0,
                        //      Date = t.DeliveryDate != null ? t.DeliveryDate.Value : t.StartDate
                        //  })
                        // .OrderByDescending(t => t.Date)
                        // .FirstOrDefault();
                        if (lastTrack != null) {
                            rate = MyUtilities.Product.GetProductRate(materialInv.Length,
                                lastTrack.WorkPiece,
                                lastTrack.ProductLength,
                                lastTrack.KnifeCut);
                        }
                    }

                    return Json(new object[]
                        {
                            product.ProductionWeight ?? 0,
                            nextProcessId,
                            warehouseId,
                            warehouseName,
                            rate,
                        });
                }
            }
            catch (Exception ex) {
                return Json("0");
            }
            return Json("0");
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateSmartProductionFull(
            [Bind(Prefix = "inserted")] IEnumerable<SmartProductionModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<SmartProductionModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<SmartProductionModel> deletedDetails,
            string importDate, int useId) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode",
                                         @"Bạn đã bị mất quyền đăng nhập. " +
                                         @"\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         @"\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<SmartProductionModel>()));
            }
            if (updatedDetails.Any()) {
                try {
                    if (insertedDetails != null)
                        updatedDetails = updatedDetails.Union(insertedDetails);
                    using (var vfi = new tammaContext()) {
                        if (string.IsNullOrWhiteSpace(importDate))
                            throw new AggregateException("Ngày báo cáo và ngày dùng NL ko được để trống");
                        var ci = new CultureInfo("vi-VN");
                        var date = string.IsNullOrWhiteSpace(importDate)
                                       ? DateTime.Today
                                       : Convert.ToDateTime(importDate, ci);
                        //var useDate = string.IsNullOrWhiteSpace(materialUseDate)
                        //               ? DateTime.Today
                        //               : Convert.ToDateTime(materialUseDate, ci);
                        var materialUse = vfi.MaterialUseInShifts.FirstOrDefault(mu => mu.UseId == useId);
                        var import = new ImportFormSX1 {
                            ImportDate = date,
                            TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            Shift1Name = materialUse.Shift1,
                            Shift2Name = materialUse.Shift2,
                            MaterialUseDate = materialUse.UsedDate,
                            Status = (byte)MyUtilities.Transaction.Status.Approved
                        };
                        var transactionSX1 = new Vfi.Models.Transaction {
                            TransactionCode = import.TransactionCode,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = date,
                            WarehouseIssueId = null,
                            WarehouseReceiptId = 1,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            EoI = "0",
                            MoP = false

                        };
                        var transactionCXL = new Vfi.Models.Transaction {
                            TransactionCode = import.TransactionCode,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = date,
                            WarehouseIssueId = 1,
                            WarehouseReceiptId = 8,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            EoI = "0",
                            MoP = false,
                        };
                        var transactionPP = new Vfi.Models.Transaction {
                            TransactionCode = import.TransactionCode,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = date,
                            WarehouseIssueId = null,
                            WarehouseReceiptId = 9,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            EoI = "0",
                            MoP = false,
                        };
                        var importWorkpiece = new ImportWorkpieceMaterial {
                            ImportDate = materialUse.UsedDate,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            ImportFormSX1 = import,
                            ImportSx1Id = import.ImportId,
                            Transaction = transactionSX1,
                            TransactionId = transactionSX1.TransactionId,
                        };
                        var existImportDetails = (from id in vfi.ImportFormSX1Detail
                                                  where id.MaterialUseDetail.UseId == useId &&
                                                        id.ImportFormSX1.ImportWorkpieceMaterials.Any() &&
                                                        id.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                                                        id.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault()
                                                          .Transaction.Status ==
                                                        (byte)MyUtilities.Transaction.Status.Approved
                                                  select id.UseDetailId).ToList();
                        var importDetails = new List<ImportFormSX1Detail>();
                        var transactionDetailsSX1 = new List<Vfi.Models.TransactionDetail>();
                        var transactionDetailsCXL = new List<Vfi.Models.TransactionDetail>();
                        var transactionDetailsPP = new List<Vfi.Models.TransactionDetail>();
                        var machineLogs = new List<MachineLog>();
                        var listProductInv = new List<ProductInventory>();
                        foreach (var update in updatedDetails) {
                            if (update.QuantityExport1 + update.QuantityPending1 + update.QuantityDefect1 > 0) {
                                var importDetail = new ImportFormSX1Detail {
                                    ImportFormSX1 = import,
                                    Machine = update.MachineName,
                                    MachineId = update.MachineId,
                                    ProductId = update.ProductId,
                                    Shift1 = materialUse.Shift1,
                                    Shift2 = materialUse.Shift2,
                                    //Number1 = entity.QuantityExport1,
                                    //Number2 = entity.QuantityExport2,
                                    //Processing1 = entity.QuantityPending1,
                                    //Processing2 = entity.QuantityPending2,
                                    //DefectProduct1 = entity.QuantityDefect1,
                                    //DefectProduct2 = entity.QuantityDefect2,
                                    MaterialInvId = update.MaterialInventoryId,
                                    //MaterialUse1 = entity.MaterialUse1,
                                    //MaterialUse2 = entity.MaterialUse2,
                                    ProductWeight = update.ProductWeight,
                                    ProductionRate = update.ProductionRate,
                                    UseDetailId = update.MaterialUseId
                                };
                                if (!string.IsNullOrWhiteSpace(materialUse.Shift1)) {
                                    importDetail.Number1 = update.QuantityExport1;
                                    importDetail.Processing1 = update.QuantityPending1;
                                    importDetail.DefectProduct1 = update.QuantityDefect1;
                                    importDetail.MaterialUse1 = update.MaterialUse1;
                                }
                                else if (!string.IsNullOrWhiteSpace(materialUse.Shift2)) {
                                    importDetail.Number2 = update.QuantityExport1;
                                    importDetail.Processing2 = update.QuantityPending1;
                                    importDetail.DefectProduct2 = update.QuantityDefect1;
                                    importDetail.MaterialUse2 = update.MaterialUse1;
                                }
                                if (existImportDetails.Contains(update.MaterialUseId)) {
                                    importDetail.MaterialUse1 = 0;
                                    importDetail.MaterialUse2 = 0;
                                }
                                var log = new MachineLog {
                                    MachineId = update.MachineId,
                                    DateLog = date,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    ModifiedDate = DateTime.Now,
                                    Note = update.GetProductionLog(),
                                    Type = (byte)MachineLogTypeEnum.Product,
                                };
                                machineLogs.Add(log);
                                importDetails.Add(importDetail);
                                var smartProduction =
                                    vfi.SmartProductions.FirstOrDefault(sp => sp.MachineId == update.MachineId);
                                if (smartProduction == null) {
                                    smartProduction = new SmartProduction();
                                    smartProduction.MachineId = update.MachineId;
                                    smartProduction.ProductId = update.ProductId;
                                    smartProduction.WarehouseId = update.WarehouseExportId;
                                    //smartProduction.ProductionRate = entity.ProductionRate;
                                    vfi.SmartProductions.Add(smartProduction);
                                }
                                else {
                                    smartProduction.ProductId = update.ProductId;
                                    smartProduction.WarehouseId = update.WarehouseExportId;
                                    //smartProduction.ProductionRate = entity.ProductionRate;
                                }
                                var product = vfi.Products.FirstOrDefault(p => p.ProductId == importDetail.ProductId);
                                if (product.ProductionRate == null || product.ProductionRate == 0)
                                    product.ProductionRate = importDetail.ProductionRate;
                                if (importDetail.ProductWeight != 1 &&
                                    product.ProductionWeight != importDetail.ProductWeight) {
                                    product.ProductionWeight = importDetail.ProductWeight;
                                }
                                if (product.QcWeight == null || product.QcWeight == 0 || product.QcWeight == 1)
                                    product.QcWeight = product.ProductionWeight;
                            }
                            else {
                                var smartProduction =
                                    vfi.SmartProductions.FirstOrDefault(sp => sp.MachineId == update.MachineId);
                                if (smartProduction == null) {
                                }
                                else {
                                    if (string.IsNullOrWhiteSpace(update.ProductCode))
                                        smartProduction.ProductId = null;
                                    if (string.IsNullOrWhiteSpace(update.WarehouseExportName))
                                        smartProduction.WarehouseId = null;
                                }

                            }
                            if (update.QuantityExport1 + update.QuantityPending1 != 0) {
                                var transactionDetailSX1 =
                                    transactionDetailsSX1.FirstOrDefault(td => td.ReferenceId == update.ProductId);
                                if (transactionDetailSX1 == null) {
                                    transactionDetailSX1 = new Vfi.Models.TransactionDetail {
                                        Transaction = transactionSX1,
                                        TransactionId = transactionSX1.TransactionId,
                                        ReferenceId = update.ProductId,
                                        MoP = false,
                                        Quantity = update.QuantityExport1 + update.QuantityPending1,
                                        UnitMeasure = null,
                                        Active = true,
                                        ModifiedUser = HttpContext.User.Identity.Name,
                                        ModifiedDate = DateTime.Now,
                                        QuantityKg = 0,
                                        Note = update.MachineName,
                                    };
                                    transactionDetailsSX1.Add(transactionDetailSX1);
                                    var productInv =
                                        vfi.ProductInventories.FirstOrDefault(
                                            pi =>
                                            pi.WarehouseId == MyUtilities.Warehouse.Production1 &&
                                            pi.ProductId == transactionDetailSX1.ReferenceId);
                                    if (productInv == null) {
                                        productInv = new ProductInventory {
                                            WarehouseId = MyUtilities.Warehouse.Production1,
                                            ProductId = transactionDetailSX1.ReferenceId.Value,
                                            ImportDate = transactionSX1.CreatedDate,
                                            ModifiedDate = DateTime.Now,
                                            ModifiedUser = HttpContext.User.Identity.Name,
                                            TotalQty = 0,
                                        };
                                        vfi.ProductInventories.Add(productInv);
                                        vfi.SaveChanges();
                                    }
                                    transactionDetailSX1.ProductInventory = productInv;
                                }
                                else {
                                    transactionDetailSX1.Quantity += update.QuantityExport1 + update.QuantityPending1;
                                    transactionDetailSX1.Note += update.MachineName + ",";
                                }
                            }

                            if (update.QuantityDefect1 > 0) {
                                var transactionDetailPP =
                                    transactionDetailsPP.FirstOrDefault(td => td.ReferenceId == update.ProductId);
                                if (transactionDetailPP == null) {
                                    transactionDetailPP = new Vfi.Models.TransactionDetail {
                                        Transaction = transactionPP,
                                        TransactionId = transactionPP.TransactionId,
                                        ReferenceId = update.ProductId,
                                        MoP = false,
                                        Quantity = update.QuantityDefect1,
                                        UnitMeasure = null,
                                        Active = true,
                                        ModifiedUser = HttpContext.User.Identity.Name,
                                        ModifiedDate = DateTime.Now,
                                        QuantityKg = 0,
                                        Note = update.MachineName,
                                    };
                                    transactionDetailsPP.Add(transactionDetailPP);
                                }
                                else {
                                    transactionDetailPP.Quantity += update.QuantityDefect1;
                                    transactionDetailPP.Note += update.MachineName + ",";
                                }
                            }
                            if (update.QuantityPending1 > 0) {
                                var transactionDetailCXL =
                                    transactionDetailsCXL.FirstOrDefault(td => td.ReferenceId == update.ProductId);
                                if (transactionDetailCXL == null) {
                                    transactionDetailCXL = new Vfi.Models.TransactionDetail {
                                        Transaction = transactionCXL,
                                        TransactionId = transactionCXL.TransactionId,
                                        ReferenceId = update.ProductId,
                                        MoP = false,
                                        Quantity = update.QuantityPending1,
                                        UnitMeasure = null,
                                        Active = true,
                                        ModifiedUser = HttpContext.User.Identity.Name,
                                        ModifiedDate = DateTime.Now,
                                        QuantityKg = 0,
                                        Note = update.MachineName,
                                    };
                                    transactionDetailsCXL.Add(transactionDetailCXL);
                                    var productInv =
                                        vfi.ProductInventories.FirstOrDefault(
                                            pi =>
                                            pi.WarehouseId == MyUtilities.Warehouse.Production1 &&
                                            pi.ProductId == transactionDetailCXL.ReferenceId);
                                    if (productInv == null) {
                                        productInv =
                                            listProductInv.FirstOrDefault(
                                                pi =>
                                                pi.WarehouseId == MyUtilities.Warehouse.Production1 &&
                                                pi.ProductId == transactionDetailCXL.ReferenceId);
                                    }
                                    transactionDetailCXL.ProductInventory = productInv;
                                }
                                else {
                                    transactionDetailCXL.Quantity += update.QuantityPending1;
                                    transactionDetailCXL.Note += update.MachineName + ",";
                                }
                            }
                        }
                        var warehouses = updatedDetails.Select(i => i.WarehouseExportId).Distinct();
                        var transacions = new List<Vfi.Models.Transaction>();
                        foreach (var warehouseId in warehouses) {
                            var transactionExport = new Vfi.Models.Transaction {
                                TransactionCode = import.TransactionCode,
                                CreatedUser = HttpContext.User.Identity.Name,
                                CreatedDate = date,
                                WarehouseIssueId = 1,
                                WarehouseReceiptId = warehouseId,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                Status = (byte)MyUtilities.Transaction.Status.Open,
                                Active = true,
                                EoI = "0",
                                MoP = false,
                            };
                            var importDetailOnWahouses = updatedDetails.Where(i => i.WarehouseExportId == warehouseId);
                            foreach (var importDetailOnWahouse in importDetailOnWahouses) {
                                if (importDetailOnWahouse.QuantityExport1 > 0) {
                                    var transactionDetailExport =
                                        transactionExport.TransactionDetails.FirstOrDefault(
                                            td => td.ReferenceId == importDetailOnWahouse.ProductId);
                                    if (transactionDetailExport == null) {
                                        transactionDetailExport = new Vfi.Models.TransactionDetail {
                                            Transaction = transactionExport,
                                            TransactionId = transactionExport.TransactionId,
                                            ReferenceId = importDetailOnWahouse.ProductId,
                                            MoP = false,
                                            Quantity =
                                                importDetailOnWahouse.QuantityExport1,
                                            UnitMeasure = null,
                                            Active = true,
                                            ModifiedUser = HttpContext.User.Identity.Name,
                                            ModifiedDate = DateTime.Now,
                                            QuantityKg = 0,
                                        };
                                        transactionExport.TransactionDetails.Add(transactionDetailExport);
                                        var productInv =
                                vfi.ProductInventories.FirstOrDefault(
                                    pi =>
                                    pi.WarehouseId == MyUtilities.Warehouse.Production1 &&
                                    pi.ProductId == transactionDetailExport.ReferenceId);
                                        if (productInv == null) {
                                            productInv = new ProductInventory {
                                                WarehouseId = MyUtilities.Warehouse.Production1,
                                                ProductId = transactionDetailExport.ReferenceId.Value,
                                                ImportDate = transactionSX1.CreatedDate,
                                                ModifiedDate = DateTime.Now,
                                                ModifiedUser = HttpContext.User.Identity.Name,
                                                TotalQty = 0,
                                            };
                                            listProductInv.Add(productInv);
                                        }
                                        transactionDetailExport.ProductInventory = productInv;
                                    }
                                    else {
                                        transactionDetailExport.Quantity += importDetailOnWahouse.QuantityExport1;
                                    }
                                }
                            }
                            if (transactionExport.TransactionDetails.Any())
                                transacions.Add(transactionExport);
                        }
                        if (importDetails.Count > 0) {
                            vfi.ImportFormSX1.Add(import);
                            vfi.ImportFormSX1Detail.AddRange(importDetails);
                            vfi.MachineLogs.AddRange(machineLogs);
                        }
                        if (transactionDetailsSX1.Count > 0) {
                            vfi.Transactions.Add(transactionSX1);
                            vfi.TransactionDetails.AddRange(transactionDetailsSX1);
                            vfi.ImportWorkpieceMaterials.Add(importWorkpiece);
                        }
                        if (transactionDetailsPP.Count > 0) {
                            vfi.Transactions.Add(transactionPP);
                            vfi.TransactionDetails.AddRange(transactionDetailsPP);
                        }
                        if (transactionDetailsCXL.Count > 0) {
                            vfi.Transactions.Add(transactionCXL);
                            vfi.TransactionDetails.AddRange(transactionDetailsCXL);
                        }
                        if (transacions.Any()) {
                            vfi.Transactions.AddRange(transacions);
                        }
                        if (listProductInv.Any())
                            vfi.ProductInventories.AddRange(listProductInv);
                        vfi.SaveChanges();
                    }
                }
                catch (Exception exception) {
                    ModelState.AddModelError("UpdateSX1", exception.Message);
                }
                //Session["SessionImportSX1Detail"] = null;
            }
            return View(new GridModel(new List<ImportSX1DetailModel>()));
        }


        [HttpPost]
        [GridAction]
        public ActionResult UpdateSmartProductionFull2(
            [Bind(Prefix = "inserted")] IEnumerable<SmartProductionModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<SmartProductionModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<SmartProductionModel> deletedDetails,
            string importDate, int useId) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode",
                                         @"Bạn đã bị mất quyền đăng nhập. " +
                                         @"\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         @"\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<SmartProductionModel>()));
            }
            if (string.IsNullOrWhiteSpace(importDate))
                throw new AggregateException("Ngày báo cáo và ngày dùng NL ko được để trống");
            if (updatedDetails.Any()) {
                try {
                    var ci = new CultureInfo("vi-VN");
                    var date = string.IsNullOrWhiteSpace(importDate)
                                   ? DateTime.Today
                                   : Convert.ToDateTime(importDate, ci);
                    if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, date)) {
                        throw new AggregateException(
                            @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                    }
                    if (insertedDetails != null)
                        updatedDetails = updatedDetails.Union(insertedDetails);
                    using (var vfi = new tammaContext()) {
                        var materialUse = vfi.MaterialUseInShifts.FirstOrDefault(mu => mu.UseId == useId);
                        if (materialUse.UsedDate >= date) {
                            throw new AggregateException("Ngày báo cáo sai");
                        }

                        var import = new ImportFormSX1 {
                            ImportDate = date,
                            TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            Shift1Name = materialUse.Shift1,
                            Shift2Name = materialUse.Shift2,
                            MaterialUseDate = materialUse.UsedDate,
                            Status = (byte)MyUtilities.Transaction.Status.Approved
                        };
                        var transactionSX1 = new Vfi.Models.Transaction {
                            TransactionCode = import.TransactionCode,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = date,
                            WarehouseIssueId = null,
                            WarehouseReceiptId = 1,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            EoI = "0",
                            MoP = false

                        };
                        var transactionCXL = new Vfi.Models.Transaction {
                            TransactionCode = import.TransactionCode,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = date,
                            WarehouseIssueId = 1,
                            WarehouseReceiptId = 8,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            EoI = "0",
                            MoP = false,
                        };
                        var transactionPP = new Vfi.Models.Transaction {
                            TransactionCode = import.TransactionCode,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = date,
                            WarehouseIssueId = null,
                            WarehouseReceiptId = 9,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            EoI = "0",
                            MoP = false,
                        };
                        var importWorkpiece = new ImportWorkpieceMaterial {
                            ImportDate = materialUse.UsedDate,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            ImportFormSX1 = import,
                            ImportSx1Id = import.ImportId,
                            Transaction = transactionSX1,
                            TransactionId = transactionSX1.TransactionId,
                        };
                        var existImportDetails = (from id in vfi.ImportFormSX1Detail
                                                  where id.MaterialUseDetail.UseId == useId &&
                                                        id.ImportFormSX1.ImportWorkpieceMaterials.Any() &&
                                                        id.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                                                        id.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault()
                                                          .Transaction.Status ==
                                                        (byte)MyUtilities.Transaction.Status.Approved
                                                  select id.UseDetailId).ToList();
                        foreach (var update in updatedDetails) {
                            if (update.QuantityExport1 + update.QuantityPending1 + update.QuantityDefect1 > 0) {
                                var importDetail = new ImportFormSX1Detail {
                                    ImportFormSX1 = import,
                                    Machine = update.MachineName,
                                    MachineId = update.MachineId,
                                    ProductId = update.ProductId,
                                    Shift1 = materialUse.Shift1,
                                    Shift2 = materialUse.Shift2,
                                    MaterialInvId = update.MaterialInventoryId,
                                    ProductWeight = update.ProductWeight,
                                    ProductionRate = update.ProductionRate,
                                    UseDetailId = update.MaterialUseId,
                                    //LotNumber = update.LotNumber,
                                };
                                importDetail.LotNumber =
                                    MyUtilities.Product.GetProductionLot(update.LotNumber,
                                        update.ProductId,
                                        update.MaterialInventoryId);
                                update.LotNumber = importDetail.LotNumber;
                                if (!string.IsNullOrWhiteSpace(materialUse.Shift1)) {
                                    importDetail.Number1 = update.QuantityExport1;
                                    importDetail.Processing1 = update.QuantityPending1;
                                    importDetail.DefectProduct1 = update.QuantityDefect1;
                                    importDetail.MaterialUse1 = update.MaterialUse1;
                                }
                                else if (!string.IsNullOrWhiteSpace(materialUse.Shift2)) {
                                    importDetail.Number2 = update.QuantityExport1;
                                    importDetail.Processing2 = update.QuantityPending1;
                                    importDetail.DefectProduct2 = update.QuantityDefect1;
                                    importDetail.MaterialUse2 = update.MaterialUse1;
                                }
                                if (existImportDetails.Contains(update.MaterialUseId)) {
                                    importDetail.MaterialUse1 = 0;
                                    importDetail.MaterialUse2 = 0;
                                }
                                import.ImportFormSX1Detail.Add(importDetail);
                                var smartProduction =
                                    vfi.SmartProductions.FirstOrDefault(sp => sp.MachineId == update.MachineId);
                                if (smartProduction == null) {
                                    smartProduction = new SmartProduction();
                                    smartProduction.MachineId = update.MachineId;
                                    smartProduction.ProductId = update.ProductId;
                                    smartProduction.WarehouseId = update.WarehouseExportId;
                                    vfi.SmartProductions.Add(smartProduction);
                                }
                                else {
                                    smartProduction.ProductId = update.ProductId;
                                    smartProduction.WarehouseId = update.WarehouseExportId;
                                }
                                var product = vfi.Products.FirstOrDefault(p => p.ProductId == importDetail.ProductId);
                                if (product.ProductionRate == null || product.ProductionRate == 0)
                                    product.ProductionRate = importDetail.ProductionRate;
                                if (importDetail.ProductWeight != 1 &&
                                    product.ProductionWeight != importDetail.ProductWeight) {
                                    product.ProductionWeight = importDetail.ProductWeight;
                                }
                                if (product.QcWeight == null || product.QcWeight == 0 || product.QcWeight == 1)
                                    product.QcWeight = product.ProductionWeight;
                            }
                            else {
                                var smartProduction =
                                    vfi.SmartProductions.FirstOrDefault(sp => sp.MachineId == update.MachineId);
                                if (smartProduction == null) {
                                }
                                else {
                                    if (string.IsNullOrWhiteSpace(update.ProductCode))
                                        smartProduction.ProductId = null;
                                    if (string.IsNullOrWhiteSpace(update.WarehouseExportName))
                                        smartProduction.WarehouseId = null;
                                }
                            }
                            if (update.QuantityExport1 + update.QuantityPending1 != 0) {
                                var transactionDetailSX1 = new Vfi.Models.TransactionDetail {
                                    Transaction = transactionSX1,
                                    TransactionId = transactionSX1.TransactionId,
                                    ReferenceId = update.ProductId,
                                    MoP = false,
                                    Quantity = update.QuantityExport1 + update.QuantityPending1,
                                    UnitMeasure = null,
                                    Active = true,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    ModifiedDate = DateTime.Now,
                                    QuantityKg = 0,
                                    Note = update.LotNumber,
                                    LotNumber = update.LotNumber,
                                    MachineId = update.MachineId
                                };
                                transactionSX1.TransactionDetails.Add(transactionDetailSX1);
                                var productInv =
                                    vfi.ProductInventories.FirstOrDefault(
                                        pi =>
                                            pi.WarehouseId == MyUtilities.Warehouse.Production1 &&
                                            pi.ProductId == update.ProductId &&
                                            pi.LotNumber.Equals(update.LotNumber));
                                if (productInv == null) {
                                    productInv = new ProductInventory {
                                        WarehouseId = MyUtilities.Warehouse.Production1,
                                        ProductId = update.ProductId,
                                        ImportDate = transactionSX1.CreatedDate,
                                        ModifiedDate = DateTime.Now,
                                        ModifiedUser = HttpContext.User.Identity.Name,
                                        TotalQty = 0,
                                        LotNumber = update.LotNumber,
                                        MachineId = update.MachineId,
                                        MaterialInvId = update.MaterialInventoryId,
                                    };
                                    vfi.ProductInventories.Add(productInv);
                                    vfi.SaveChanges();
                                }
                            }
                            if (update.QuantityDefect1 > 0) {
                                var transactionDetailPP = new Vfi.Models.TransactionDetail {
                                    Transaction = transactionPP,
                                    TransactionId = transactionPP.TransactionId,
                                    ReferenceId = update.ProductId,
                                    MoP = false,
                                    Quantity = update.QuantityDefect1,
                                    UnitMeasure = null,
                                    Active = true,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    ModifiedDate = DateTime.Now,
                                    QuantityKg = 0,
                                    Note = update.LotNumber,
                                    LotNumber = update.LotNumber,
                                    MachineId = update.MachineId
                                };
                                transactionPP.TransactionDetails.Add(transactionDetailPP);
                            }
                            if (update.QuantityPending1 > 0) {
                                var transactionDetailCXL = new Vfi.Models.TransactionDetail {
                                    Transaction = transactionCXL,
                                    TransactionId = transactionCXL.TransactionId,
                                    ReferenceId = update.ProductId,
                                    MoP = false,
                                    Quantity = update.QuantityPending1,
                                    UnitMeasure = null,
                                    Active = true,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    ModifiedDate = DateTime.Now,
                                    QuantityKg = 0,
                                    Note = update.LotNumber,
                                    LotNumber = update.LotNumber,
                                    MachineId = update.MachineId
                                };
                                transactionCXL.TransactionDetails.Add(transactionDetailCXL);
                                var productInv =
                                    vfi.ProductInventories.FirstOrDefault(
                                        pi =>
                                            pi.WarehouseId == MyUtilities.Warehouse.Production1 &&
                                            pi.ProductId == update.ProductId &&
                                            pi.LotNumber.Equals(update.LotNumber));
                                if (productInv == null) {
                                    productInv = new ProductInventory {
                                        WarehouseId = MyUtilities.Warehouse.Production1,
                                        ProductId = update.ProductId,
                                        ImportDate = transactionSX1.CreatedDate,
                                        ModifiedDate = DateTime.Now,
                                        ModifiedUser = HttpContext.User.Identity.Name,
                                        TotalQty = 0,
                                        LotNumber = update.LotNumber,
                                        MachineId = update.MachineId,
                                        MaterialInvId = update.MaterialInventoryId,
                                    };
                                    vfi.ProductInventories.Add(productInv);
                                    vfi.SaveChanges();
                                }
                                transactionDetailCXL.ProductInventory = productInv;
                            }
                        }
                        var warehouses = updatedDetails.Select(i => i.WarehouseExportId).Distinct();
                        var transacions = new List<Vfi.Models.Transaction>();
                        foreach (var warehouseId in warehouses) {
                            var transactionExport = new Vfi.Models.Transaction {
                                TransactionCode = import.TransactionCode,
                                CreatedUser = HttpContext.User.Identity.Name,
                                CreatedDate = date,
                                WarehouseIssueId = 1,
                                WarehouseReceiptId = warehouseId,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                Status = (byte)MyUtilities.Transaction.Status.Open,
                                Active = true,
                                EoI = "0",
                                MoP = false,
                            };
                            var importDetailOnWahouses = updatedDetails.Where(i => i.WarehouseExportId == warehouseId);
                            foreach (var update in importDetailOnWahouses) {
                                if (update.QuantityExport1 > 0) {
                                    var transactionDetailExport = new Vfi.Models.TransactionDetail {
                                        Transaction = transactionExport,
                                        TransactionId = transactionExport.TransactionId,
                                        ReferenceId = update.ProductId,
                                        MoP = false,
                                        Quantity = update.QuantityExport1,
                                        UnitMeasure = null,
                                        Active = true,
                                        ModifiedUser = HttpContext.User.Identity.Name,
                                        ModifiedDate = DateTime.Now,
                                        QuantityKg = 0,
                                        LotNumber = update.LotNumber,
                                        Note = update.LotNumber,
                                        MachineId = update.MachineId
                                    };
                                    if (update.ProcessByMachineId != 0) {
                                        transactionDetailExport.NextProcessId = update.ProcessByMachineId;
                                    }
                                    transactionExport.TransactionDetails.Add(transactionDetailExport);
                                    var productInv =
                                        vfi.ProductInventories.FirstOrDefault(
                                            pi =>
                                                pi.WarehouseId == MyUtilities.Warehouse.Production1 &&
                                                pi.ProductId == update.ProductId &&
                                                pi.LotNumber.Equals(update.LotNumber));
                                    if (productInv == null) {
                                        productInv = new ProductInventory {
                                            WarehouseId = MyUtilities.Warehouse.Production1,
                                            ProductId = update.ProductId,
                                            ImportDate = transactionSX1.CreatedDate,
                                            ModifiedDate = DateTime.Now,
                                            ModifiedUser = HttpContext.User.Identity.Name,
                                            TotalQty = 0,
                                            LotNumber = update.LotNumber,
                                            MachineId = update.MachineId,
                                            MaterialInvId = update.MaterialInventoryId,
                                        };
                                        vfi.ProductInventories.Add(productInv);
                                        vfi.SaveChanges();
                                    }
                                    transactionDetailExport.ProductInventory = productInv;
                                }
                            }
                            if (transactionExport.TransactionDetails.Any())
                                transacions.Add(transactionExport);
                        }
                        if (import.ImportFormSX1Detail.Count > 0) {
                            vfi.ImportFormSX1.Add(import);
                        }
                        if (transactionSX1.TransactionDetails.Count > 0) {
                            vfi.Transactions.Add(transactionSX1);
                            vfi.ImportWorkpieceMaterials.Add(importWorkpiece);
                        }
                        if (transactionPP.TransactionDetails.Count > 0) {
                            vfi.Transactions.Add(transactionPP);
                        }
                        if (transactionCXL.TransactionDetails.Count > 0) {
                            vfi.Transactions.Add(transactionCXL);
                        }
                        if (transacions.Any()) {
                            vfi.Transactions.AddRange(transacions);
                        }
                        vfi.SaveChanges();
                    }
                }
                catch (Exception exception) {
                    ModelState.AddModelError("UpdateSX1", exception.Message);
                }
                //Session["SessionImportSX1Detail"] = null;
            }
            return View(new GridModel(new List<ImportSX1DetailModel>()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult SaveImportProduction1(
            [Bind(Prefix = "inserted")] IEnumerable<SmartProductionModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<SmartProductionModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<SmartProductionModel> deletedDetails,
            string importDate, int useId) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode",
                                         @"Bạn đã bị mất quyền đăng nhập. " +
                                         @"\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         @"\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<SmartProductionModel>()));
            }
            if (string.IsNullOrWhiteSpace(importDate))
                throw new AggregateException("Ngày báo cáo và ngày dùng NL ko được để trống");
            var count = 0;
            if (updatedDetails.Any()) {
                try {
                    var ci = new CultureInfo("vi-VN");
                    var date = string.IsNullOrWhiteSpace(importDate)
                                   ? DateTime.Today
                                   : Convert.ToDateTime(importDate, ci);
                    if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, date)) {
                        throw new AggregateException(
                            @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                    }
                    if (insertedDetails != null)
                        updatedDetails = updatedDetails.Union(insertedDetails);
                    using (var vfi = new tammaContext()) {
                        var materialUse = vfi.MaterialUseInShifts.FirstOrDefault(mu => mu.UseId == useId);
                        if (materialUse.UsedDate >= date) {
                            throw new AggregateException("Ngày báo cáo sai");
                        }

                        var useDetailIds = materialUse.MaterialUseDetails.Select(mud => mud.DetailId).ToList();

                        var import = new ImportFormSX1 {
                            ImportDate = date,
                            TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Production1, 1),
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            Shift1Name = materialUse.Shift1,
                            Shift2Name = materialUse.Shift2,
                            MaterialUseDate = materialUse.UsedDate,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                        };
                        //var existImportDetails = (from id in vfi.ImportFormSX1Detail
                        //                          where id.MaterialUseDetail.UseId == useId &&
                        //                                id.ImportFormSX1.ImportWorkpieceMaterials.Any() &&
                        //                                id.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                        //                                id.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault()
                        //                                  .Transaction.Status ==
                        //                                (byte)MyUtilities.Transaction.Status.Approved
                        //                          select id.UseDetailId).ToList();
                        var existUseDetails = (from id in vfi.ImportFormSX1Detail
                                               where id.UseDetailId != null && useDetailIds.Contains(id.UseDetailId.Value) &&
                                               id.ImportFormSX1.Status != (byte)MyUtilities.Transaction.Status.Cancel
                                               select id.UseDetailId.Value)
                                               .ToList();
                        foreach (var update in updatedDetails) {
                            var smartProduction = vfi.SmartProductions.FirstOrDefault(sp => sp.MachineId == update.MachineId);
                            if (smartProduction == null) {
                                smartProduction = new SmartProduction();
                                smartProduction.MachineId = update.MachineId;
                                vfi.SmartProductions.Add(smartProduction);
                            }
                            smartProduction.ProductId = update.ProductId;
                            smartProduction.WarehouseId = update.WarehouseExportId;
                            smartProduction.UnitMeasure = update.UnitMeasure;
                            smartProduction.BoxWeight = update.BoxWeight;
                            if (update.QuantityExport1 + update.QuantityPending1 + update.QuantityDefect1 + +update.QuantityExport2 + update.QuantityPending2 + update.QuantityDefect2 <= 0) continue;
                            if (String.IsNullOrWhiteSpace(update.UnitMeasure) || (!update.UnitMeasure.Equals("g") && !update.UnitMeasure.Equals("Pcs"))) {
                                throw new AggregateException("Lỗi đơn vị tính máy " + update.MachineName);
                            }
                            var importDetail = new ImportFormSX1Detail {
                                ImportFormSX1 = import,
                                Machine = update.MachineName,
                                MachineId = update.MachineId,
                                ProductId = update.ProductId,
                                Shift1 = materialUse.Shift1,
                                Shift2 = materialUse.Shift2,
                                MaterialInvId = update.MaterialInventoryId,
                                ProductWeight = update.ProductWeight,
                                ProductionRate = update.ProductionRate,
                                UseDetailId = update.MaterialUseId,
                                WarehouseExportId = update.WarehouseExportId,
                                UnitMeasure = update.UnitMeasure,
                                LotNumber = update.LotNumber
                            };

                            var number = 0.0;
                            var processing = 0.0;
                            var defect = 0.0;
                            if (update.UnitMeasure.Equals("g")) {
                                if (update.ProductWeight <= 0) {
                                    throw new AggregateException("Lỗi trọng lượng SP máy:" + update.MachineName);
                                }
                                number = MyUtilities.Function.RoundDown((update.QuantityExport1 + update.QuantityExport2 + update.QuantityPending2 + update.QuantityDefect2 
                                                                        - (update.BoxNumber * update.BoxWeight)) 
                                                                        / update.ProductWeight);
                                processing = MyUtilities.Function.RoundDown(update.QuantityPending1 / update.ProductWeight);
                                defect = MyUtilities.Function.RoundDown(update.QuantityDefect1 / update.ProductWeight);
                            }
                            else {
                                number = update.QuantityExport1 + update.QuantityExport2 + update.QuantityPending2 + update.QuantityDefect2;
                                processing = update.QuantityPending1;
                                defect = update.QuantityDefect1;
                            }
                            if (!string.IsNullOrWhiteSpace(materialUse.Shift1)) {
                                importDetail.MaterialUse1 = update.MaterialUse1;
                                importDetail.Number1 = number;
                                importDetail.Processing1 = processing;
                                importDetail.DefectProduct1 = defect;
                            }
                            else if (!string.IsNullOrWhiteSpace(materialUse.Shift2)) {
                                importDetail.MaterialUse2 = update.MaterialUse1;
                                importDetail.Number2 = number;
                                importDetail.Processing2 = processing;
                                importDetail.DefectProduct2 = defect;
                            }
                            if (existUseDetails.Contains(update.MaterialUseId)) {
                                importDetail.MaterialUse1 = 0;
                                importDetail.MaterialUse2 = 0;
                            }
                            //importDetail.LotNumber =
                            //    MyUtilities.Product.GetProductionLot(update.LotNumber,
                            //        update.ProductId,
                            //        update.MaterialInventoryId);
                            import.ImportFormSX1Detail.Add(importDetail);
                            count = import.ImportFormSX1Detail.Count;
                        }
                        if (import.ImportFormSX1Detail.Count > 0) {
                            vfi.ImportFormSX1.Add(import);
                        }
                        vfi.SaveChanges();
                    }
                }
                catch (Exception exception) {
                    ModelState.AddModelError("UpdateSX1", exception.Message);
                }
            }
            return View(new GridModel(new List<ImportSX1DetailModel>()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult SaveImportProduction1_Auto(
            [Bind(Prefix = "inserted")] IEnumerable<SmartProductionModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<SmartProductionModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<SmartProductionModel> deletedDetails,
            string importDate, int useId) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode",
                                         @"Bạn đã bị mất quyền đăng nhập. " +
                                         @"\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         @"\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<SmartProductionModel>()));
            }
            if (string.IsNullOrWhiteSpace(importDate))
                throw new AggregateException("Ngày báo cáo và ngày dùng NL ko được để trống");
            if (updatedDetails.Any()) {
                try {
                    var date = MyUtilities.Function.ParseDate(importDate);
                    if (date > DateTime.Now.AddDays(1)) {
                        throw new AggregateException("Lỗi! Xem lại ngày ( lớn hơn hiện tại) !");
                    }
                    if (MyUtilities.Transaction.IsLock(date, MyUtilities.Transaction.ProductionLockType.Production1)) {
                        throw new AggregateException(
                            @"Ngày nhập SX bị khoá do tính lương! /n Vui lòng liên hệ quản lý tính lương !");
                    }
                    if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, date)) {
                        throw new AggregateException(
                            @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                    }
                    if (insertedDetails != null)
                        updatedDetails = updatedDetails.Union(insertedDetails);
                    var baseDetails = (List<SmartProductionModel>)Session["SessionImportSX1Detail"];
                    using (var vfi = new tammaContext()) {
                        var materialUse = vfi.MaterialUseInShifts.FirstOrDefault(mu => mu.UseId == useId);
                        if (materialUse.UsedDate >= date) {
                            throw new AggregateException("Ngày báo cáo sai");
                        }
                        if (MyUtilities.Transaction.IsLock(materialUse.UsedDate, MyUtilities.Transaction.ProductionLockType.Production1)) {
                            throw new AggregateException(
                                @"Ngày nhập SX bị khoá do tính lương! /n Vui lòng liên hệ quản lý tính lương !");
                        }

                        var useDetailIds = materialUse.MaterialUseDetails.Select(mud => mud.DetailId).ToList();

                        var import = new ImportFormSX1 {
                            ImportDate = date,
                            TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Production1, 1),
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            Shift1Name = materialUse.Shift1,
                            Shift2Name = materialUse.Shift2,
                            MaterialUseDate = materialUse.UsedDate,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                        };
                        var existUseDetails = (from id in vfi.ImportFormSX1Detail
                                               where id.UseDetailId != null && useDetailIds.Contains(id.UseDetailId.Value) &&
                                               id.ImportFormSX1.Status != (byte)MyUtilities.Transaction.Status.Cancel
                                               select id.UseDetailId.Value)
                                               .ToList();
                        foreach (var detail in baseDetails) {
                            var update = detail;
                            if (updatedDetails.Any()) {
                                var updatedDetail = updatedDetails.FirstOrDefault(ud => ud.MaterialUseId == detail.MaterialUseId);
                                if (updatedDetail != null)
                                    update = updatedDetail;
                            }
                            var diff = 0.0;
                            var processing = 0.0;
                            var defect = 0.0;
                            var materialQuantity = update.MaterialUse1;
                            if (string.IsNullOrWhiteSpace(update.UnitMeasure)) {
                                throw new AggregateException("Lỗi ĐVT cân hàng SP máy:" + update.MachineName);
                            }
                            if (update.UnitMeasure.Equals("g")) {
                                if (update.ProductWeight <= 0) {
                                    throw new AggregateException("Lỗi trọng lượng SP máy:" + update.MachineName);
                                }
                                diff = MyUtilities.Function.RoundDown(update.QuantityDiff1 / update.ProductWeight);
                                processing = MyUtilities.Function.RoundDown(update.QuantityPending1 / update.ProductWeight);
                                defect = MyUtilities.Function.RoundDown(update.QuantityDefect1 / update.ProductWeight);
                            }
                            else {
                                diff = update.QuantityDiff1;
                                processing = update.QuantityPending1;
                                defect = update.QuantityDefect1;
                            }
                            if (existUseDetails.Contains(update.MaterialUseId)) {
                                materialQuantity = 0;
                            }
                            processing = MyUtilities.Function.RoundUp(processing);
                            defect = MyUtilities.Function.RoundUp(defect);
                            var number = MyUtilities.Function.RoundUp((materialQuantity * update.ProductionRate) - processing - defect + diff);
                            if (number < 0) number = 0;
                            if (number + processing + defect <= 0)
                                continue;

                            var smartProduction = vfi.SmartProductions.FirstOrDefault(sp => sp.MachineId == update.MachineId);
                            if (smartProduction == null) {
                                smartProduction = new SmartProduction();
                                smartProduction.MachineId = update.MachineId;
                                vfi.SmartProductions.Add(smartProduction);
                            }
                            smartProduction.ProductId = update.ProductId;
                            smartProduction.UnitMeasure = update.UnitMeasure;
                            if (update.WarehouseExportId > 0)
                                smartProduction.WarehouseId = update.WarehouseExportId;

                             if (String.IsNullOrWhiteSpace(update.UnitMeasure) || (!update.UnitMeasure.Equals("g") && !update.UnitMeasure.Equals("Pcs"))) {
                                throw new AggregateException("Lỗi đơn vị tính máy " + update.MachineName);
                            }
                            var importDetail = new ImportFormSX1Detail {
                                ImportFormSX1 = import,
                                Machine = update.MachineName,
                                MachineId = update.MachineId,
                                ProductId = update.ProductId,
                                Shift1 = materialUse.Shift1,
                                Shift2 = materialUse.Shift2,
                                MaterialInvId = update.MaterialInventoryId,
                                ProductWeight = update.ProductWeight,
                                ProductionRate = update.ProductionRate,
                                UseDetailId = update.MaterialUseId,
                                WarehouseExportId = update.WarehouseExportId,
                                UnitMeasure = update.UnitMeasure,
                                LotNumber = update.LotNumber
                            };
                            if (!string.IsNullOrWhiteSpace(materialUse.Shift1)) {
                                importDetail.MaterialUse1 = materialQuantity;
                                importDetail.Number1 = number;
                                importDetail.Processing1 = processing;
                                importDetail.DefectProduct1 = defect;
                            }
                            else if (!string.IsNullOrWhiteSpace(materialUse.Shift2)) {
                                importDetail.MaterialUse2 = materialQuantity;
                                importDetail.Number2 = number;
                                importDetail.Processing2 = processing;
                                importDetail.DefectProduct2 = defect;
                            }
                            import.ImportFormSX1Detail.Add(importDetail);
                        }
                        if (import.ImportFormSX1Detail.Count > 0) {
                            vfi.ImportFormSX1.Add(import);
                        }
                        vfi.SaveChanges();
                        Session["SessionImportSX1Detail"] = null;
                    }
                }
                catch (Exception exception) {
                    ModelState.AddModelError("SaveImportProduction1_Auto", exception.Message);
                }
            }
            return View(new GridModel(new List<ImportSX1DetailModel>()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateSavedImportProduction1(
            [Bind(Prefix = "inserted")] IEnumerable<SmartProductionModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<SmartProductionModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<SmartProductionModel> deletedDetails,
            int importId) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode",
                                         @"Bạn đã bị mất quyền đăng nhập. " +
                                         @"\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         @"\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<SmartProductionModel>()));
            }
            if (updatedDetails.Any()) {
                try {
                    using (var vfi = new tammaContext()) {
                        var importProduction1 = vfi.ImportFormSX1.FirstOrDefault(sx1 => sx1.ImportId == importId);
                        if (importProduction1.Status != (byte)MyUtilities.Transaction.Status.Open) {
                            throw new AggregateException("Lỗi! Phiếu " + importProduction1.TransactionCode + " đã thay đổi trạng thái!");
                        }
                        var detailIds = updatedDetails.Select(id => id.SmartId).ToList();
                        var importDetails = vfi.ImportFormSX1Detail.Where(id => detailIds.Contains(id.DetailId));
                        foreach (var update in updatedDetails) {
                            var importDetail = importDetails.FirstOrDefault(id => id.DetailId == update.SmartId);
                            if (importDetail == null) continue;

                            importDetail.ProductId = update.ProductId;
                            importDetail.ProductWeight = update.ProductWeight;
                            importDetail.ProductionRate = update.ProductionRate;
                            importDetail.WarehouseExportId = update.WarehouseExportId;
                            importDetail.ProcessByMachineId = update.ProcessByMachineId;

                            if (!string.IsNullOrWhiteSpace(importProduction1.Shift1Name)) {
                                importDetail.Number1 = Math.Round(update.QuantityExport1);
                                importDetail.Processing1 = Math.Round(update.QuantityPending1);
                                importDetail.DefectProduct1 = Math.Round(update.QuantityDefect1);
                            }
                            else if (!string.IsNullOrWhiteSpace(importProduction1.Shift2Name)) {
                                importDetail.Number2 = Math.Round(update.QuantityExport1);
                                importDetail.Processing2 = Math.Round(update.QuantityPending1);
                                importDetail.DefectProduct2 = Math.Round(update.QuantityDefect1);
                            }
                        }
                        vfi.SaveChanges();
                    }
                }
                catch (Exception exception) {
                    ModelState.AddModelError("UpdateSavedImportProduction1", exception.Message);
                }
            }
            return SelectSavedOpenProduction1(importId);
        }

        [HttpPost]
        public ActionResult CancelTransactionForImportProduction1(int importId) {
            try {
                if (!Request.IsAuthenticated)
                    return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

                var modifiedUser = HttpContext.User.Identity.Name;
                if (string.IsNullOrWhiteSpace(modifiedUser))
                    return Json(@"Vui lòng đăng nhập hệ thống. (user null). ");
                if (importId == 0)
                    return Json(@"Vui lòng chọn ít nhất 1 lệnh. ");
                using (var vfi = new tammaContext()) {
                    var import = vfi.ImportFormSX1.FirstOrDefault(sx1 => sx1.ImportId == importId);
                    if (import == null || import.Status != (byte)MyUtilities.Transaction.Status.Open) {
                        return Json(@"Không tìm thấy lệnh nhập sản xuất. ");
                    }
                    import.Status = (byte)MyUtilities.Transaction.Status.Cancel;
                    vfi.SaveChanges();
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("UpdateSavedImportProduction1", exception.Message);
            }
            return View(new GridModel(new List<ImportSX1DetailModel>()));
        }

        [HttpPost]
        public ActionResult ApproveTransactionForImportProduction1(int importId) {
            try {
                if (!Request.IsAuthenticated)
                    return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

                var modifiedUser = HttpContext.User.Identity.Name;
                if (string.IsNullOrWhiteSpace(modifiedUser))
                    return Json(@"Vui lòng đăng nhập hệ thống. (user null). ");
                if (importId == 0)
                    return Json(@"Vui lòng chọn ít nhất 1 lệnh. ");
                using (var vfi = new tammaContext()) {
                    var import = vfi.ImportFormSX1.FirstOrDefault(sx1 => sx1.ImportId == importId);
                    if (import == null || import.Status != (byte)MyUtilities.Transaction.Status.Open) {
                        return Json(@"Không tìm thấy lệnh nhập sản xuất. ");
                    }
                    var transactionSX1 = new Vfi.Models.Transaction {
                        TransactionCode = import.TransactionCode,
                        CreatedUser = HttpContext.User.Identity.Name,
                        CreatedDate = import.ImportDate,
                        WarehouseIssueId = null,
                        WarehouseReceiptId = MyUtilities.Warehouse.Production1,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        Status = (byte)MyUtilities.Transaction.Status.Open,
                        Active = true,
                        EoI = "0",
                        MoP = false,
                        ReferenceId = importId
                    };
                    var transactionCXL = new Vfi.Models.Transaction {
                        TransactionCode = import.TransactionCode,
                        CreatedUser = HttpContext.User.Identity.Name,
                        CreatedDate = import.ImportDate,
                        WarehouseIssueId = transactionSX1.WarehouseReceiptId,
                        WarehouseReceiptId = MyUtilities.Warehouse.Processing,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        Status = (byte)MyUtilities.Transaction.Status.Open,
                        Active = true,
                        EoI = "0",
                        MoP = false,
                        ReferenceId = importId
                    };
                    var transactionPP = new Vfi.Models.Transaction {
                        TransactionCode = import.TransactionCode,
                        CreatedUser = HttpContext.User.Identity.Name,
                        CreatedDate = import.ImportDate,
                        WarehouseIssueId = null,
                        WarehouseReceiptId = MyUtilities.Warehouse.Defect,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        Status = (byte)MyUtilities.Transaction.Status.Open,
                        Active = true,
                        EoI = "0",
                        MoP = false,
                        ReferenceId = importId
                    };
                    var importWorkpiece = new ImportWorkpieceMaterial {
                        ImportDate = import.MaterialUseDate,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        ImportFormSX1 = import,
                        ImportSx1Id = import.ImportId,
                        Transaction = transactionSX1,
                        TransactionId = transactionSX1.TransactionId,
                    };
                    foreach (var detail in import.ImportFormSX1Detail) {
                        //var smartProduction =
                        //    vfi.SmartProductions.FirstOrDefault(sp => sp.MachineId == detail.MachineId);
                        //if (smartProduction == null) {
                        //    smartProduction = new SmartProduction();
                        //    smartProduction.MachineId = detail.MachineId;
                        //    smartProduction.ProductId = detail.ProductId;
                        //    smartProduction.WarehouseId = detail.WarehouseExportId;
                        //    smartProduction.UnitMeasure = detail.UnitMeasure;
                        //    vfi.SmartProductions.Add(smartProduction);
                        //}
                        //else {
                        //    smartProduction.ProductId = detail.ProductId;
                        //    smartProduction.WarehouseId = detail.WarehouseExportId;
                        //    smartProduction.UnitMeasure = detail.UnitMeasure;
                        //}
                        var product = vfi.Products.FirstOrDefault(p => p.ProductId == detail.ProductId);
                        if (product.ProductionRate == null || product.ProductionRate == 0)
                            product.ProductionRate = detail.ProductionRate;
                        if (detail.ProductWeight != 1 && product.ProductionWeight != detail.ProductWeight) {
                            product.ProductionWeight = detail.ProductWeight;
                        }
                        if (product.QcWeight == null || product.QcWeight == 0 || product.QcWeight == 1)
                            product.QcWeight = product.ProductionWeight;

                        if (detail.Number1 + detail.Processing1 + detail.Number2 + detail.Processing2 > 0) {
                            var transactionDetailSX1 = new Vfi.Models.TransactionDetail {
                                Transaction = transactionSX1,
                                TransactionId = transactionSX1.TransactionId,
                                ReferenceId = detail.ProductId,
                                MoP = false,
                                Quantity = detail.Number1 + detail.Processing1 + detail.Number2 + detail.Processing2,
                                UnitMeasure = null,
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                QuantityKg = 0,
                                Note = detail.LotNumber,
                                LotNumber = detail.LotNumber,
                                MachineId = detail.MachineId
                            };
                            transactionSX1.TransactionDetails.Add(transactionDetailSX1);
                            //var productInv =
                            //    vfi.ProductInventories.FirstOrDefault(
                            //        pi =>
                            //            pi.WarehouseId == MyUtilities.Warehouse.Production1 &&
                            //            pi.ProductId == detail.ProductId &&
                            //            pi.LotNumber.Equals(detail.LotNumber));
                            //if (productInv == null) {
                            //    productInv = new ProductInventory {
                            //        WarehouseId = MyUtilities.Warehouse.Production1,
                            //        ProductId = detail.ProductId,
                            //        ImportDate = transactionSX1.CreatedDate,
                            //        ModifiedDate = DateTime.Now,
                            //        ModifiedUser = HttpContext.User.Identity.Name,
                            //        TotalQty = 0,
                            //        LotNumber = detail.LotNumber,
                            //        MachineId = detail.MachineId,
                            //        MaterialInvId = detail.MaterialInvId,
                            //    };
                            //    vfi.ProductInventories.Add(productInv);
                            //    vfi.SaveChanges();
                            //}
                        }
                        if (detail.DefectProduct1 + detail.DefectProduct2 > 0) {
                            var transactionDetailPP = new Vfi.Models.TransactionDetail {
                                Transaction = transactionPP,
                                TransactionId = transactionPP.TransactionId,
                                ReferenceId = detail.ProductId,
                                MoP = false,
                                Quantity = detail.DefectProduct1 + detail.DefectProduct2,
                                UnitMeasure = null,
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                QuantityKg = 0,
                                Note = detail.LotNumber,
                                LotNumber = detail.LotNumber,
                                MachineId = detail.MachineId
                            };
                            transactionPP.TransactionDetails.Add(transactionDetailPP);
                        }
                        if (detail.Processing1 + detail.Processing2 > 0) {
                            var transactionDetailCXL = new Vfi.Models.TransactionDetail {
                                Transaction = transactionCXL,
                                TransactionId = transactionCXL.TransactionId,
                                ReferenceId = detail.ProductId,
                                MoP = false,
                                Quantity = detail.Processing1 + detail.Processing2,
                                UnitMeasure = null,
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                QuantityKg = 0,
                                Note = detail.LotNumber,
                                LotNumber = detail.LotNumber,
                                MachineId = detail.MachineId
                            };
                            transactionCXL.TransactionDetails.Add(transactionDetailCXL);
                            var productInv =
                                vfi.ProductInventories.FirstOrDefault(
                                    pi =>
                                        pi.WarehouseId == MyUtilities.Warehouse.Production1 &&
                                        pi.ProductId == detail.ProductId &&
                                        pi.LotNumber.Equals(detail.LotNumber));
                            if (productInv == null) {
                                productInv = new ProductInventory {
                                    WarehouseId = MyUtilities.Warehouse.Production1,
                                    ProductId = detail.ProductId,
                                    ImportDate = transactionSX1.CreatedDate,
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    TotalQty = 0,
                                    LotNumber = detail.LotNumber,
                                    MachineId = detail.MachineId,
                                    MaterialInvId = detail.MaterialInvId,
                                };
                                vfi.ProductInventories.Add(productInv);
                                vfi.SaveChanges();
                            }
                            transactionDetailCXL.ProductInventory = productInv;
                        }
                        if (detail.Product.MaterialId == null && detail.MaterialInvId != null) {
                            detail.Product.MaterialId = detail.MaterialInventory.MaterialId;
                        }
                    }
                    var warehouseIds = import.ImportFormSX1Detail.Select(i => i.WarehouseExportId ?? 0).Distinct();
                    var transacions = new List<Vfi.Models.Transaction>();
                    foreach (var warehouseId in warehouseIds) {
                        var transactionExport = new Vfi.Models.Transaction {
                            TransactionCode = import.TransactionCode,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = import.ImportDate,
                            WarehouseIssueId = 1,
                            WarehouseReceiptId = warehouseId,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            EoI = "0",
                            MoP = false,
                            ReferenceId = importId
                        };
                        var detailOnWarehouses = import.ImportFormSX1Detail.Where(id => id.WarehouseExportId == warehouseId &&
                                                                                        id.Number1 + id.Number2 > 0);
                        foreach (var detail in detailOnWarehouses) {
                            var transactionDetailExport = new Vfi.Models.TransactionDetail {
                                Transaction = transactionExport,
                                TransactionId = transactionExport.TransactionId,
                                ReferenceId = detail.ProductId,
                                MoP = false,
                                Quantity = detail.Number1 + detail.Number2,
                                UnitMeasure = null,
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                QuantityKg = 0,
                                LotNumber = detail.LotNumber,
                                Note = detail.LotNumber,
                                MachineId = detail.MachineId
                            };
                            if (detail.ProcessByMachineId != 0) {
                                transactionDetailExport.NextProcessId = detail.ProcessByMachineId;
                            }
                            transactionExport.TransactionDetails.Add(transactionDetailExport);
                            var productInv =
                                vfi.ProductInventories.FirstOrDefault(
                                    pi =>
                                        pi.WarehouseId == MyUtilities.Warehouse.Production1 &&
                                        pi.ProductId == detail.ProductId &&
                                        pi.LotNumber.Equals(detail.LotNumber));
                            if (productInv == null) {
                                productInv = new ProductInventory {
                                    WarehouseId = MyUtilities.Warehouse.Production1,
                                    ProductId = detail.ProductId,
                                    ImportDate = transactionSX1.CreatedDate,
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    TotalQty = 0,
                                    LotNumber = detail.LotNumber,
                                    MachineId = detail.MachineId,
                                    MaterialInvId = detail.MaterialInvId,
                                };
                                vfi.ProductInventories.Add(productInv);
                                vfi.SaveChanges();
                            }
                            transactionDetailExport.ProductInventory = productInv;
                        }
                        if (transactionExport.TransactionDetails.Any())
                            transacions.Add(transactionExport);
                    }
                    import.Status = (byte)MyUtilities.Transaction.Status.Approved;
                    if (transactionSX1.TransactionDetails.Count > 0) {
                        vfi.Transactions.Add(transactionSX1);
                        vfi.ImportWorkpieceMaterials.Add(importWorkpiece);
                    }
                    if (transactionPP.TransactionDetails.Count > 0) {
                        vfi.Transactions.Add(transactionPP);
                    }
                    if (transactionCXL.TransactionDetails.Count > 0) {
                        vfi.Transactions.Add(transactionCXL);
                    }
                    if (transacions.Any()) {
                        vfi.Transactions.AddRange(transacions);
                    }
                    vfi.SaveChanges();
                }

            }
            catch (Exception exception) {
                return
                    Json(@"Lỗi giá trị nhập. (try-catch). " + exception.Message + "\n");
            }
            return Json("okie");
        }

        public ActionResult SelectComboboxOpenSavedImportProduction1() {
            var model = new List<ImportSX1Model>();
            using (var vfi = new tammaContext()) {
                var importProduction1s = (from sx1 in vfi.ImportFormSX1
                                       where sx1.Status == (byte) MyUtilities.Transaction.Status.Open
                                       orderby sx1.MaterialUseDate
                                       select sx1).Distinct();
                foreach (var importProduction1 in importProduction1s) {

                    var entity = new ImportSX1Model {
                        ImportId = importProduction1.ImportId,
                        TransactionCode =
                            importProduction1.TransactionCode.Trim()
                    };

                    entity.TransactionCode += " SX: " + importProduction1.MaterialUseDate.ToString("dd/MM/yyyy");
                    if (string.IsNullOrWhiteSpace(importProduction1.Shift1Name)) {
                        entity.TransactionCode += importProduction1.Shift1Name.Trim();
                    }
                    if (string.IsNullOrWhiteSpace(importProduction1.Shift2Name)) {
                        entity.TransactionCode += importProduction1.Shift2Name.Trim();
                    }
                    model.Add(entity);
                }
            }
            return new JsonResult {
                Data =
                    new SelectList(model, "ImportId", "TransactionCode"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }


        [GridAction]
        public ActionResult SelectSavedOpenProduction1(int importId) {
            if (importId == 0)
                return View(new GridModel(new List<SmartProductionModel>()));
            var model = new List<SmartProductionModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var importProduction1 = (from sx1 in vfi.ImportFormSX1
                                             where sx1.ImportId == importId
                                             select sx1).FirstOrDefault();
                    if (importProduction1.Status != (byte)MyUtilities.Transaction.Status.Open) {
                        throw new AggregateException("Lỗi! Phiếu " + importProduction1.TransactionCode + " đã thay đổi trạng thái!");
                    }
                    var productIds = importProduction1.ImportFormSX1Detail.Select(x => x.ProductId).Distinct().ToList();
                    var machineIds = importProduction1.ImportFormSX1Detail.Select(x => x.MachineId).Distinct().ToList();
                    var productionProcesses = vfi.ProductionProcesses.Where(x => productIds.Contains(x.ProductId)
                        && x.IsNecessary)
                        .OrderBy(x => x.ProcessIndex)
                        .ToList();
                    var productionProcessesByMachine = vfi.ProductionProcessByMachines.Where(x => productIds.Contains(x.ProductId)
                                                && machineIds.Contains(x.MachineId)
                                                && x.Active)
                        .OrderBy(x => x.ProcessIndex)
                        .ToList();
                    foreach (var detail in importProduction1.ImportFormSX1Detail) {
                        var entity = new SmartProductionModel {
                            SmartId = detail.DetailId,
                            MachineName = detail.Machine,
                            MachineId = detail.MachineId ?? 0,
                            MaterialInventoryId = detail.MaterialInvId ?? 0,
                            MaterialUse1 = 0,
                            MaterialUseId = detail.UseDetailId ?? 0,
                            MaterialInventoryCode =
                                MyUtilities.Material.GetMaterialInvDesignNo(detail.MaterialInventory),
                            MaterialId = detail.MaterialInventory.MaterialId,
                            LotNumber = detail.LotNumber,
                            ProductionRateStatus = 0,
                            ProductId = detail.ProductId,
                            ProductCode = detail.Product.ProductCode,
                            ProductWeight = detail.ProductWeight,
                            ProductionRate = detail.ProductionRate,
                            WarehouseExportId = detail.WarehouseExportId ?? 0,

                        };
                        if (!string.IsNullOrWhiteSpace(importProduction1.Shift1Name)) {
                            entity.MaterialUse1 = detail.MaterialUse1;
                            entity.QuantityExport1 = detail.Number1;
                            entity.QuantityPending1 = detail.Processing1;
                            entity.QuantityDefect1 = detail.DefectProduct1;
                        }
                        else if (!string.IsNullOrWhiteSpace(importProduction1.Shift2Name)) {
                            entity.MaterialUse1 = detail.MaterialUse2;
                            entity.QuantityExport1 = detail.Number2;
                            entity.QuantityPending1 = detail.Processing2;
                            entity.QuantityDefect1 = detail.DefectProduct2;
                        }
                        var lastTrack = MyUtilities.Machine.LastTrackUpMachine(
                            entity.MachineId, 
                            entity.MaterialId, 
                            entity.ProductId, 
                            importProduction1.MaterialUseDate);
                        if (lastTrack != null) {
                            if (lastTrack.ProductId == 1512) {
                                var lastProduction =
                                    vfi.ImportFormSX1Detail.OrderByDescending(id => id.ImportFormSX1.MaterialUseDate)
                                       .FirstOrDefault(id => id.MachineId == entity.MachineId);
                                if (lastProduction != null) {
                                    entity.ProductId = lastProduction.ProductId;
                                    entity.ProductCode = lastProduction.Product.ProductCode;
                                    entity.ProductWeight = lastProduction.Product.ProductionWeight ?? 0;
                                    entity.Length = lastProduction.Product.Length ?? 0;
                                    var nextProcess =
                                        lastProduction.Product.ProductionProcesses.Where(p => p.WarehouseId != 1 && p.IsNecessary)
                                                 .OrderBy(p => p.ProcessIndex).FirstOrDefault();
                                    //entity.ProcessByMachineId = nextProcess.id;
                                    entity.WarehouseExportId = nextProcess.WarehouseId;
                                    entity.WarehouseExportName = nextProcess.Warehouse.ShortName;
                                }
                            }
                            else {
                                entity.ProductId = lastTrack.ProductId;
                                entity.ProductCode = lastTrack.ProductCode;
                                entity.ProductWeight = lastTrack.ProductionWeight;
                                entity.Length = lastTrack.ProductLength;
                            }
                            entity.ProductionRate =
                                MyUtilities.Product.GetProductRate(detail.MaterialInventory.Length,
                                    lastTrack.WorkPiece,
                                    entity.Length,
                                    lastTrack.KnifeCut);
                        }
                        if (entity.ProductId != 1512) {
                            var nextProcess =
                                productionProcessesByMachine
                                    .FirstOrDefault(p => p.ProductId == entity.ProductId &&
                                                p.MachineId == entity.MachineId &&
                                                p.WarehouseId != MyUtilities.Warehouse.Production1);
                            if (nextProcess != null) {
                                entity.ProcessByMachineId = nextProcess.DetailId;
                                entity.WarehouseExportId = nextProcess.WarehouseId;
                            }
                        }
                        if (!(entity.WarehouseExportId > 0)) {
                            var nextProcess = productionProcesses.FirstOrDefault(p => p.ProductId == entity.ProductId
                                                                            && p.WarehouseId != 1);
                            if (nextProcess != null) {
                                entity.WarehouseExportId = nextProcess.WarehouseId;
                            }
                        }
                        entity.ProductionQuantity = entity.MaterialUse1 * entity.ProductionRate;
                        entity.DiffProduction = entity.QuantityExport1 + entity.QuantityPending1 + entity.QuantityDefect1 - entity.ProductionQuantity;
                        if (entity.ProductionRate > 0) {
                            entity.DiffMaterial = entity.DiffProduction / entity.ProductionRate;
                            if (entity.MaterialUse1 <= 0) {
                                entity.ProductionRateStatus = 1;
                            }
                            else if (Math.Abs(entity.DiffMaterial) / entity.MaterialUse1 >= 0.1) {
                                entity.ProductionRateStatus = 1;
                            }
                            else if (Math.Abs(entity.DiffMaterial) / entity.MaterialUse1 >= 0.2) {
                                entity.ProductionRateStatus = 2;
                            }
                        }

                        var warehouse = vfi.Warehouses.FirstOrDefault(w => w.WarehouseId == entity.WarehouseExportId);
                        if (warehouse != null) {
                            entity.WarehouseExportName = warehouse.ShortName;
                        }
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                throw new AggregateException(ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.MachineName)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateAddProduction(
            [Bind(Prefix = "inserted")] IEnumerable<SmartProductionModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<SmartProductionModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<SmartProductionModel> deletedDetails,
            string importDate, string materialUseDate, int shiftType, string shiftName) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode",
                                         @"Bạn đã bị mất quyền đăng nhập. " +
                                         @"\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         @"\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<SmartProductionModel>()));
            }
            if (insertedDetails.Any()) {
                try {
                    shiftName = (shiftName + "").ToUpper();
                    //if (insertedDetails != null)
                    updatedDetails =
                        insertedDetails.Where(
                            id => id.QuantityExport1 + id.QuantityPending1 + id.QuantityDefect1 > 0);
                    using (var vfi = new tammaContext()) {
                        if (string.IsNullOrWhiteSpace(importDate))
                            throw new AggregateException("Ngày báo cáo và ngày dùng NL ko được để trống");
                        var ci = new CultureInfo("vi-VN");
                        var date = string.IsNullOrWhiteSpace(importDate)
                                       ? DateTime.Today
                                       : Convert.ToDateTime(importDate, ci);
                        var useDate = string.IsNullOrWhiteSpace(materialUseDate)
                                       ? DateTime.Today
                                       : Convert.ToDateTime(materialUseDate, ci);
                        if (MyUtilities.Transaction.IsLock(useDate, MyUtilities.Transaction.ProductionLockType.Production1)) {
                            throw new AggregateException(
                                @"Ngày nhập SX bị khoá do tính lương! /n Vui lòng liên hệ quản lý tính lương !");
                        }
                        if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, date)) {
                            throw new AggregateException(
                                @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                        }
                        if (useDate >= date)
                            throw new AggregateException("Lỗi! Xem lại ngày báo cáo");
                        //var materialUse = vfi.MaterialUseInShifts.FirstOrDefault(mu => mu.UseId == useId);
                        var import = new ImportFormSX1 {
                            ImportDate = date,
                            TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            //Shift1Name = materialUse.Shift1,
                            //Shift2Name = materialUse.Shift2,
                            MaterialUseDate = useDate,
                            Shift1Name = "",
                            Shift2Name = "",
                            Status = (byte) MyUtilities.Transaction.Status.Approved
                        };
                        if (shiftType == 1)
                            import.Shift1Name = shiftName;
                        else
                            import.Shift2Name = shiftName;
                        var transactionSX1 = new Vfi.Models.Transaction {
                            TransactionCode = import.TransactionCode,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = date,
                            WarehouseIssueId = null,
                            WarehouseReceiptId = 1,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            EoI = "0",
                            MoP = false,
                            ReferenceId = import.ImportId
                        };
                        var transactionCXL = new Vfi.Models.Transaction {
                            TransactionCode = import.TransactionCode,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = date,
                            WarehouseIssueId = 1,
                            WarehouseReceiptId = 8,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            EoI = "0",
                            MoP = false,
                            ReferenceId = import.ImportId
                        };
                        var transactionPP = new Vfi.Models.Transaction {
                            TransactionCode = import.TransactionCode,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = date,
                            WarehouseIssueId = null,
                            WarehouseReceiptId = 9,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            EoI = "0",
                            MoP = false,
                            ReferenceId = import.ImportId
                        };
                        var importWorkpiece = new ImportWorkpieceMaterial {
                            ImportDate = useDate,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            ImportFormSX1 = import,
                            ImportSx1Id = import.ImportId,
                            Transaction = transactionSX1,
                            TransactionId = transactionSX1.TransactionId,
                        };
                        foreach (var update in updatedDetails) {
                            if (update.QuantityExport1 + update.QuantityPending1 + update.QuantityDefect1 > 0) {
                                var importDetail = new ImportFormSX1Detail {
                                    ImportFormSX1 = import,
                                    Machine = update.MachineName,
                                    MachineId = update.MachineId,
                                    ProductId = update.ProductId,
                                    Shift1 = import.Shift1Name,
                                    Shift2 = import.Shift2Name,
                                    MaterialInvId = update.MaterialInventoryId,
                                    ProductWeight = update.ProductWeight,
                                    ProductionRate = update.ProductionRate,
                                    LotNumber = ""
                                };
                                var lastImport =
                                    vfi.ImportFormSX1Detail.Where(
                                            id =>
                                                id.ProductId == importDetail.ProductId &&
                                                id.MachineId == importDetail.MachineId)
                                        .ToList()
                                        .OrderByDescending(id=> id.ImportFormSX1.MaterialUseDate)
                                        .FirstOrDefault();
                                if (lastImport != null) {
                                    importDetail.LotNumber = lastImport.LotNumber;
                                    importDetail.MaterialInvId = lastImport.MaterialInvId;
                                    update.MaterialInventoryId = lastImport.MaterialInvId.Value;
                                }
                                update.LotNumber = importDetail.LotNumber;
                                if (shiftType == 1) {
                                    importDetail.Number1 = update.QuantityExport1;
                                    importDetail.Processing1 = update.QuantityPending1;
                                    importDetail.DefectProduct1 = update.QuantityDefect1;
                                }
                                else {
                                    importDetail.Number2 = update.QuantityExport1;
                                    importDetail.Processing2 = update.QuantityPending1;
                                    importDetail.DefectProduct2 = update.QuantityDefect1;
                                }
                                import.ImportFormSX1Detail.Add(importDetail);
                                var smartProduction =
                                    vfi.SmartProductions.FirstOrDefault(sp => sp.MachineId == update.MachineId);
                                if (smartProduction == null) {
                                    smartProduction = new SmartProduction();
                                    smartProduction.MachineId = update.MachineId;
                                    smartProduction.ProductId = update.ProductId;
                                    smartProduction.WarehouseId = update.WarehouseExportId;
                                    vfi.SmartProductions.Add(smartProduction);
                                }
                                else {
                                    smartProduction.ProductId = update.ProductId;
                                    smartProduction.WarehouseId = update.WarehouseExportId;
                                }
                                var product = vfi.Products.FirstOrDefault(p => p.ProductId == importDetail.ProductId);
                                if (product.ProductionRate == null || product.ProductionRate == 0)
                                    product.ProductionRate = importDetail.ProductionRate;
                                if (importDetail.ProductWeight != 1 &&
                                    product.ProductionWeight != importDetail.ProductWeight) {
                                    product.ProductionWeight = importDetail.ProductWeight;
                                }
                                if (product.QcWeight == null || product.QcWeight == 0 || product.QcWeight == 1)
                                    product.QcWeight = product.ProductionWeight;
                            }
                            else {
                                var smartProduction =
                                    vfi.SmartProductions.FirstOrDefault(sp => sp.MachineId == update.MachineId);
                                if (smartProduction == null) {
                                }
                                else {
                                    if (string.IsNullOrWhiteSpace(update.ProductCode))
                                        smartProduction.ProductId = null;
                                    if (string.IsNullOrWhiteSpace(update.WarehouseExportName))
                                        smartProduction.WarehouseId = null;
                                }
                            }
                            if (update.QuantityExport1 + update.QuantityPending1 != 0) {
                                var transactionDetailSX1 = new Vfi.Models.TransactionDetail {
                                    Transaction = transactionSX1,
                                    TransactionId = transactionSX1.TransactionId,
                                    ReferenceId = update.ProductId,
                                    MoP = false,
                                    Quantity = update.QuantityExport1 + update.QuantityPending1,
                                    UnitMeasure = null,
                                    Active = true,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    ModifiedDate = DateTime.Now,
                                    QuantityKg = 0,
                                    Note = update.LotNumber,
                                    LotNumber = update.LotNumber
                                };
                                transactionSX1.TransactionDetails.Add(transactionDetailSX1);
                                var productInv =
                                    vfi.ProductInventories.FirstOrDefault(
                                        pi =>
                                            pi.WarehouseId == MyUtilities.Warehouse.Production1 &&
                                            pi.ProductId == update.ProductId &&
                                            pi.LotNumber.Equals(update.LotNumber));
                                if (productInv == null) {
                                    productInv = new ProductInventory {
                                        WarehouseId = MyUtilities.Warehouse.Production1,
                                        ProductId = update.ProductId,
                                        ImportDate = transactionSX1.CreatedDate,
                                        ModifiedDate = DateTime.Now,
                                        ModifiedUser = HttpContext.User.Identity.Name,
                                        TotalQty = 0,
                                        LotNumber = update.LotNumber,
                                        MachineId = update.MachineId,
                                        //MaterialInvId = update.MaterialInventoryId,
                                    };
                                    if (update.MaterialInventoryId != 0) {
                                        productInv.MaterialInvId = update.MaterialInventoryId;
                                    }
                                    vfi.ProductInventories.Add(productInv);
                                    vfi.SaveChanges();
                                }
                            }

                            if (update.QuantityDefect1 > 0) {
                                var transactionDetailPP = new Vfi.Models.TransactionDetail {
                                    Transaction = transactionPP,
                                    TransactionId = transactionPP.TransactionId,
                                    ReferenceId = update.ProductId,
                                    MoP = false,
                                    Quantity = update.QuantityDefect1,
                                    UnitMeasure = null,
                                    Active = true,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    ModifiedDate = DateTime.Now,
                                    QuantityKg = 0,
                                    Note = update.LotNumber,
                                    LotNumber = update.LotNumber
                                };
                                transactionPP.TransactionDetails.Add(transactionDetailPP);
                            }
                            if (update.QuantityPending1 > 0) {
                                var transactionDetailCXL = new Vfi.Models.TransactionDetail {
                                    Transaction = transactionCXL,
                                    TransactionId = transactionCXL.TransactionId,
                                    ReferenceId = update.ProductId,
                                    MoP = false,
                                    Quantity = update.QuantityPending1,
                                    UnitMeasure = null,
                                    Active = true,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    ModifiedDate = DateTime.Now,
                                    QuantityKg = 0,
                                    Note = update.LotNumber,
                                    LotNumber = update.LotNumber,
                                };
                                transactionCXL.TransactionDetails.Add(transactionDetailCXL);
                                var productInv =
                                    vfi.ProductInventories.FirstOrDefault(
                                        pi =>
                                            pi.WarehouseId == MyUtilities.Warehouse.Production1 &&
                                            pi.ProductId == update.ProductId &&
                                            pi.LotNumber.Equals(update.LotNumber));
                                if (productInv == null) {
                                    productInv = new ProductInventory {
                                        WarehouseId = MyUtilities.Warehouse.Production1,
                                        ProductId = update.ProductId,
                                        ImportDate = transactionSX1.CreatedDate,
                                        ModifiedDate = DateTime.Now,
                                        ModifiedUser = HttpContext.User.Identity.Name,
                                        TotalQty = 0,
                                        LotNumber = update.LotNumber,
                                        MachineId = update.MachineId,
                                        //MaterialInvId = update.MaterialInventoryId,
                                    };
                                    if (update.MaterialInventoryId != 0) {
                                        productInv.MaterialInvId = update.MaterialInventoryId;
                                    }
                                    vfi.ProductInventories.Add(productInv);
                                    vfi.SaveChanges();
                                }
                                transactionDetailCXL.ProductInventory = productInv;
                            }
                        }
                        var warehouses = updatedDetails.Select(i => i.WarehouseExportId).Distinct();
                        var transacions = new List<Vfi.Models.Transaction>();
                        foreach (var warehouseId in warehouses) {
                            var transactionExport = new Vfi.Models.Transaction {
                                TransactionCode = import.TransactionCode,
                                CreatedUser = HttpContext.User.Identity.Name,
                                CreatedDate = date,
                                WarehouseIssueId = 1,
                                WarehouseReceiptId = warehouseId,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                Status = (byte)MyUtilities.Transaction.Status.Open,
                                Active = true,
                                EoI = "0",
                                MoP = false,
                                ReferenceId = import.ImportId
                            };
                            var importDetailOnWahouses = updatedDetails.Where(i => i.WarehouseExportId == warehouseId);
                            foreach (var update in importDetailOnWahouses) {
                                if (update.QuantityExport1 > 0) {
                                    var transactionDetailExport = new Vfi.Models.TransactionDetail {
                                        Transaction = transactionExport,
                                        TransactionId = transactionExport.TransactionId,
                                        ReferenceId = update.ProductId,
                                        MoP = false,
                                        Quantity = update.QuantityExport1,
                                        UnitMeasure = null,
                                        Active = true,
                                        ModifiedUser = HttpContext.User.Identity.Name,
                                        ModifiedDate = DateTime.Now,
                                        QuantityKg = 0,
                                        LotNumber = update.LotNumber,
                                    };
                                    if (update.ProcessByMachineId != 0) {
                                        transactionDetailExport.NextProcessId = update.ProcessByMachineId;
                                    }
                                    transactionExport.TransactionDetails.Add(transactionDetailExport);
                                    var productInv =
                                        vfi.ProductInventories.FirstOrDefault(
                                            pi =>
                                                pi.WarehouseId == MyUtilities.Warehouse.Production1 &&
                                                pi.ProductId == update.ProductId &&
                                                pi.LotNumber.Equals(update.LotNumber));
                                    if (productInv == null) {
                                        productInv = new ProductInventory {
                                            WarehouseId = MyUtilities.Warehouse.Production1,
                                            ProductId = update.ProductId,
                                            ImportDate = transactionSX1.CreatedDate,
                                            ModifiedDate = DateTime.Now,
                                            ModifiedUser = HttpContext.User.Identity.Name,
                                            TotalQty = 0,
                                            LotNumber = update.LotNumber,
                                            MachineId = update.MachineId,
                                            //MaterialInvId = update.MaterialInventoryId,
                                        };
                                        if (update.MaterialInventoryId != 0) {
                                            productInv.MaterialInvId = update.MaterialInventoryId;
                                        }
                                        vfi.ProductInventories.Add(productInv);
                                        vfi.SaveChanges();
                                    }
                                    transactionDetailExport.ProductInventory = productInv;
                                }
                            }
                            if (transactionExport.TransactionDetails.Any())
                                transacions.Add(transactionExport);
                        }
                        if (import.ImportFormSX1Detail.Count > 0) {
                            vfi.ImportFormSX1.Add(import);
                        }
                        vfi.SaveChanges();
                        if (transactionSX1.TransactionDetails.Count > 0) {
                            vfi.Transactions.Add(transactionSX1);
                            transactionSX1.ReferenceId = import.ImportId;
                            vfi.ImportWorkpieceMaterials.Add(importWorkpiece);
                        }
                        if (transactionPP.TransactionDetails.Count > 0) {
                            transactionPP.ReferenceId = import.ImportId;
                            vfi.Transactions.Add(transactionPP);
                        }
                        if (transactionCXL.TransactionDetails.Count > 0) {
                            transactionCXL.ReferenceId = import.ImportId;
                            vfi.Transactions.Add(transactionCXL);
                        }
                        if (transacions.Any()) {
                            transacions.ForEach(x => x.ReferenceId = import.ImportId);
                            vfi.Transactions.AddRange(transacions);
                        }
                        vfi.SaveChanges();
                    }
                }
                catch (Exception exception) {
                    ModelState.AddModelError("UpdateSX1", exception.Message);
                }
                //Session["SessionImportSX1Detail"] = null;
            }
            return View(new GridModel(new List<ImportSX1DetailModel>()));
        }
        [HttpPost]
        [GridAction]
        public ActionResult CancelTransactionMaterial(int transactionId) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode",
                                         "Bạn đã bị mất quyền đăng nhập. " +
                                         "\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         "\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<TransactionModel>()));
            }
            using (var vfi = new tammaContext()) {
                var transaction = vfi.Transactions.FirstOrDefault(td => td.TransactionId == transactionId);
                if (transaction.Status == (byte)MyUtilities.Transaction.Status.Open) {
                    transaction.Status = (byte)MyUtilities.Transaction.Status.Cancel;
                    vfi.SaveChanges();
                }

                return View(new GridModel(GetTransactionOpenMaterialList().OrderBy(m => m.CreatedDate)));
            }
        }

        List<TransactionModel> GetTransactionOpenMaterialList() {
            try {
                var model = new List<TransactionModel>();
                using (var vfi = new tammaContext()) {
                    var purchasing = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.PurchasingManagement);
                    var canApproveInternal = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.ApproveInternalMaterial);
                    var transactions = from t in vfi.Transactions
                                       where t.Status == (byte)MyUtilities.Transaction.Status.Open
                                            && t.MoP
                                            && (t.IsInternal != true || canApproveInternal)
                                       select t;

                    foreach (var transaction in transactions) {
                        var transactionDetails =
                            vfi.TransactionDetails.Where(td => td.TransactionId == transaction.TransactionId);

                        if (!transactionDetails.Any()) continue;
                        var entity = new TransactionModel();
                        entity.TransactionId = transaction.TransactionId;
                        entity.TransactionCode = transaction.TransactionCode;
                        entity.EoI = transaction.EoI;
                        entity.EoIName = MyUtilities.Transaction.CastText.GetTextEoI(transaction.EoI);
                        entity.MoP = transaction.MoP;
                        entity.Status = transaction.Status;
                        entity.StatusName = MyUtilities.Transaction.CastText.GetTextStatus(transaction.Status);
                        entity.Description = transaction.Description;
                        entity.Active = transaction.Active;
                        entity.CreatedUser = transaction.CreatedUser;
                        entity.CreatedDate = transaction.CreatedDate;
                        entity.ModifiedUser = transaction.ModifiedUser;
                        entity.ModifiedDate = transaction.ModifiedDate;
                        entity.IsInternal = transaction.IsInternal ?? false;
                        entity.TotalQuality = transactionDetails.Sum(t => t.Quantity);
                        entity.PurchasingSignatureType = -1;
                        if (entity.EoI == Convert.ToChar(MyUtilities.Transaction.EoIEnum.Export).ToString()) {
                            var exportMaterial = vfi.ExportMaterials.FirstOrDefault(em => em.TransactionId == transaction.TransactionId);
                            if (exportMaterial != null) {
                                entity.TotalQuality = exportMaterial.ExportMaterialDetails.Sum(emd => emd.Quantity);
                                //entity.TotalQualityKg =
                                //    exportMaterial.ExportMaterialDetails.Sum(emd => emd.QuantityKg ?? 0);
                                if (exportMaterial.ShiftType == null && exportMaterial.ShiftName == null) {
                                    if (transaction.IsInternal == true) entity.EoIName += " nội bộ";
                                    else  entity.EoIName += " hủy";
                                }
                                else
                                    entity.EoIName += " ca: " + exportMaterial.ShiftType + exportMaterial.ShiftName;
                            }
                        }
                        else if (entity.EoI == Convert.ToChar(MyUtilities.Transaction.EoIEnum.Import).ToString()) {
                            entity.PurchasingSignatureType = 0;
                            var import = vfi.ImportPurchaseOrders.FirstOrDefault(e => e.TransactionId == entity.TransactionId);
                            if (import == null)
                                throw new AggregateException("Lỗi");
                            if (import.PurchasingSignature == 1)
                                entity.PurchasingSignatureType = 1;
                            else if (purchasing) {
                                entity.PurchasingSignatureType = 2;
                            }
                            if (import.PurchaseOrderId == null) {
                                if (transaction.IsInternal == true) {
                                    entity.PurchasingSignatureType = -1;
                                    entity.EoIName += " nội bộ";
                                }
                                else entity.EoIName += " thêm";
                            }
                            else {
                                entity.SpecialNote += "PO:" + import.PurchaseOrder.RevisionNumber;
                            }
                        }

                        entity.AlertColor = 0;
                        if (entity.CreatedDate > DateTime.Today.AddDays(4) ||
                            entity.CreatedDate < DateTime.Today.AddDays(-4))
                            entity.AlertColor = 2;
                        else if (entity.CreatedDate > DateTime.Today.AddDays(1) ||
                                 entity.CreatedDate < DateTime.Today.AddDays(-1))
                            entity.AlertColor = 1;
                        model.Add(entity);
                    }
                }
                return model;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateAssignMaterialsProduction(
            [Bind(Prefix = "inserted")] IEnumerable<SmartProductionModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<SmartProductionModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<SmartProductionModel> deletedDetails,
            string importDate, int caNumber, string caName) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode",
                                         "Bạn đã bị mất quyền đăng nhập. " +
                                         "\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         "\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<ImportSX1DetailModel>()));
            }
            if (updatedDetails.Any()) {
                try {
                    var date = MyUtilities.Function.ParseDate(importDate);
                    if (date > DateTime.Now.AddDays(1)) {
                        throw new AggregateException("Lỗi! Xem lại ngày ( lớn hơn hiện tại) !");
                    }
                    if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, date)) {
                        throw new AggregateException(
                            @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                    }
                    if (insertedDetails != null)
                        updatedDetails = updatedDetails.Union(insertedDetails);
                    updatedDetails = updatedDetails.Where(x => x.MaterialUse1 + x.MaterialUse2 > 0);
                    using (var vfi = new tammaContext()) {
                        // kiem tra han muc nguyen lieu
                        //var check = updatedDetails.FirstOrDefault(u => !u.IsLimit);
                        //if (check != null)
                        //    throw new AggregateException("Có nguyên liệu vượt hạn mức cho phép !");
                        // kiem tra ton kho nguyen lieu theo lo
                        var materialInvIds = updatedDetails.Select(u => u.MaterialInventoryId).Distinct();
                        foreach (var materialInvId in materialInvIds) {
                            var updateds = updatedDetails.Where(u => u.MaterialInventoryId == materialInvId);
                            if (string.IsNullOrWhiteSpace(updateds.FirstOrDefault().MaterialInventoryCode)) continue;
                            var materialInventory =
                                vfi.MaterialInventories.FirstOrDefault(
                                    mi => mi.MaterialInventoryId == materialInvId && Math.Round(mi.TotalQty) > 0);
                            if (materialInventory == null) {
                                throw new AggregateException("Nguyên liệu " +
                                                             updateds.FirstOrDefault().MaterialInventoryCode +
                                                             " đã hết!" + updateds.FirstOrDefault().MachineName);
                            }
                            var exportMaterialQuantity = Math.Round(updateds.Sum(u => u.MaterialUse1 + u.MaterialUse2) - materialInventory.TotalQty,
                                                                    2);
                            if (exportMaterialQuantity > 0) {
                                throw new AggregateException("Nguyên liệu " +
                                                             updateds.FirstOrDefault().MaterialInventoryCode +
                                                             " không đủ!\n"
                                                             + updateds.FirstOrDefault().MachineName + " thiếu " +
                                                             Math.Abs(exportMaterialQuantity));
                            }
                        }
                        var transactionMaterial = new Vfi.Models.Transaction {
                            TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Material, 1),
                            EoI = Convert.ToChar(MyUtilities.Transaction.EoIEnum.Export).ToString(),
                            MoP = true,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = date,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                        };
                        //
                        var exportMaterial = new ExportMaterial {
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            Transaction = transactionMaterial,
                            TransactionId = transactionMaterial.TransactionId,
                            ExportDate = date,
                            ShiftName = Convert.ToChar(caName).ToString(),
                            ShiftType = (byte)caNumber,
                        };

                        var trackDetailIds = updatedDetails.Select(ud => ud.SmartId);
                        var trackDetails = vfi.TrackUpMaterials.Where(tm => trackDetailIds.Contains(tm.DetailId));
                        foreach (var detail in updatedDetails) {
                            var quantity = Math.Round(detail.MaterialUse1 + detail.MaterialUse2, 2);
                            if (quantity == 0) continue;
                            var smartProduction = vfi.SmartProductions.FirstOrDefault(sp => sp.MachineId == detail.MachineId);
                            if (smartProduction == null) {
                                smartProduction = new SmartProduction();
                                smartProduction.MachineId = detail.MachineId;
                                smartProduction.MaterialInvId = detail.MaterialInventoryId;
                                vfi.SmartProductions.Add(smartProduction);
                            }
                            else {
                                smartProduction.MaterialInvId = detail.MaterialInventoryId;
                            }
                            var materialInv =
                                vfi.MaterialInventories.FirstOrDefault(
                                    m => m.MaterialInventoryId == detail.MaterialInventoryId);
                            var transactionMaterialDetail = new Vfi.Models.TransactionDetail {
                                Transaction = transactionMaterial,
                                TransactionId = transactionMaterial.TransactionId,
                                ReferenceId = materialInv.MaterialId,
                                MoP = true,
                                Quantity = quantity,
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                Note = detail.Note + detail.MachineName,
                                LotNumber = materialInv.LotNumber,
                            };
                            transactionMaterialDetail.QuantityKg = quantity * (materialInv.Material.Weight);
                            transactionMaterial.TransactionDetails.Add(transactionMaterialDetail);
                            var exportDetail = new ExportMaterialDetail {
                                MachineId = detail.MachineId,
                                ExportId = exportMaterial.ExportId,
                                ExportMaterial = exportMaterial,
                                MaterialInvId = detail.MaterialInventoryId,
                                MaterialId = materialInv.MaterialId,
                                Quantity = quantity,
                                TransactionDetailId = transactionMaterialDetail.TransactionDetailId,
                                TransactionDetail = transactionMaterialDetail,
                            };
                            exportMaterial.ExportMaterialDetails.Add(exportDetail);
                        }
                        if (transactionMaterial.TransactionDetails.Any()) {
                            vfi.Transactions.Add(transactionMaterial);
                            vfi.ExportMaterials.Add(exportMaterial);
                            vfi.SaveChanges();
                        }
                    }
                }
                catch (DbEntityValidationException ex) {
                    foreach (var validationErrors in ex.EntityValidationErrors) {
                        foreach (var validationError in validationErrors.ValidationErrors) {
                            ModelState.AddModelError("db sx1",
                                                     string.Format("Property: {0} Error: {1}",
                                                                   validationError.PropertyName,
                                                                   validationError.ErrorMessage));
                        }
                    }
                }

                catch (Exception ex) {
                    ModelState.AddModelError("Material SX1", ex.Message);
                }
            }
            return View(new GridModel(new List<SmartProductionModel>()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateMaterialRetrieve(
            [Bind(Prefix = "inserted")] IEnumerable<MaterialUseInShiftDetailModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<MaterialUseInShiftDetailModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<MaterialUseInShiftDetailModel> deletedDetails,
            string importDate) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("UpdateMaterialRetrieve",
                                         "Bạn đã bị mất quyền đăng nhập. " +
                                         "\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         "\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<MaterialUseInShiftDetailModel>()));
            }
            if (updatedDetails.Any()) {
                try {
                    var date = MyUtilities.Function.ParseDate(importDate);
                    if (date > DateTime.Now.AddDays(1)) {
                        throw new AggregateException("Lỗi! Xem lại ngày ( lớn hơn hiện tại) !");
                    }
                    if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, date)) {//1
                        throw new AggregateException(
                            @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                    }
                    using (var vfi = new tammaContext()) {
                        var materialUse = new MaterialUseInShift() {
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            UsedDate = date,
                            UsedCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.MaterialUse, 1),
                            Type = (int) MyUtilities.Material.UseType.SendBack,
                        };

                        foreach (var detail in updatedDetails) {
                            if (detail.Quantity == 0) continue;
                            var miOnMachine =
                                vfi.MaterialInvOnMachines.FirstOrDefault(mim => mim.Id == detail.DetailId);
                            if (miOnMachine == null)
                                throw new AggregateException("Lỗi nguyên liệu trên máy!");
                            if (miOnMachine.TotalQuantity == 0)
                                throw new AggregateException(detail.MachineName + ": máy đã hết tồn nguyên liệu!");
                            if (Math.Round(miOnMachine.TotalQuantity, 2) < Math.Round(detail.Quantity, 2))
                                throw new AggregateException(detail.MachineName +
                                                             ": nguyên liệu trả không được nhiều hơn tồn!");
                            //if(string.IsNullOrWhiteSpace(detail.Note))
                            //    throw new AggregateException(detail.MachineName+": nguyên liệu trả phải có lý do!");
                            var materialUseDetail = new MaterialUseDetail() {
                                MaterialUseInShift = materialUse,
                                UseId = materialUse.UseId,
                                MachineId = detail.MachineId,
                                MaterialInvId = detail.MaterialInvId,
                                EditQuantity = detail.Quantity,
                                Quantity = detail.Quantity,
                                Note = detail.Note,
                                Quantity2 = 0,
                                EditQuantity2 = 0,
                                IsDetroy = detail.IsDestroy,
                                //ToMachineId = detail.ToMachineId
                            };
                            if (detail.ToMachineId != 0)
                                materialUseDetail.ToMachineId = detail.ToMachineId;
                            materialUse.MaterialUseDetails.Add(materialUseDetail);
                        }
                        if (materialUse.MaterialUseDetails.Any()) {
                            vfi.MaterialUseInShifts.Add(materialUse);
                            vfi.SaveChanges();
                        }
                    }
                }
                catch (Exception ex) {
                    ModelState.AddModelError("UpdateMaterialRetrieve", ex.Message);
                }
            }

            return View(new GridModel(new List<MaterialUseInShiftDetailModel>()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateMaterialUse(
            [Bind(Prefix = "inserted")] IEnumerable<MaterialUseInShiftDetailModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<MaterialUseInShiftDetailModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<MaterialUseInShiftDetailModel> deletedDetails,
            string importDate, string ca1Name, string ca2Name) {
            if (updatedDetails.Any()) {
                using (var vfi = new tammaContext()) {
                    try {
                        if (!Request.IsAuthenticated)
                            throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
                        var date = MyUtilities.Function.ParseDate(importDate);
                        if (date > DateTime.Now.AddDays(1)) {
                            throw new AggregateException("Lỗi! Xem lại ngày ( lớn hơn hiện tại) !");
                        }
                        if (MyUtilities.Transaction.IsLock(date, MyUtilities.Transaction.ProductionLockType.Production1)) {
                            throw new AggregateException(
                                @"Ngày nhập SX bị khoá do tính lương! \n Vui lòng liên hệ quản lý tính lương !");
                        }
                        if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, date)) {
                            throw new AggregateException(
                                @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                        }
                        ca1Name = (ca1Name + "").ToUpper();
                        ca2Name = (ca2Name + "").ToUpper();

                        var msg = "";
                        foreach (var detail in updatedDetails) {
                            var quantity = Math.Round(detail.Quantity + detail.Quantity2 - detail.EarlyQuantity, 2);
                            if (quantity > 0)
                                msg += (detail.MachineName + " " + detail.MaterialCode + " thiếu " + (quantity) + "\n");
                        }
                        if (!string.IsNullOrWhiteSpace(msg))
                            throw new AggregateException(msg);

                        var materialUse1 = new MaterialUseInShift() {
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            Shift1 = ca1Name,
                            Shift2 = "",
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            UsedDate = date,
                            UsedCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.MaterialUse, 1),
                            Type = (int)MyUtilities.Material.UseType.Using,
                        };
                        var list = new List<MaterialUseDetail>();
                        var updatedDetails1 = updatedDetails.Where(u => u.Quantity > 0);
                        var lot = MyUtilities.MySystem.LotNumber_Weekly(date);
                        foreach (var detail in updatedDetails1) {
                            if (detail.Quantity <= 0) continue;
                            var materialUseDetail = new MaterialUseDetail {
                                MachineId = detail.MachineId,
                                MaterialInvId = detail.MaterialInvId,
                                UseId = materialUse1.UseId,
                                // MaterialUseForm = materialUse,
                                EditQuantity = Math.Round(detail.Quantity, 2),
                                EditQuantity2 = 0,
                                Quantity = Math.Round(detail.Quantity, 2),
                                Quantity2 = 0,
                                Note = detail.Note,
                                //BoxNumber = detail.BoxNumber,
                                Lot = lot + detail.MachineName,
                            };
                            materialUse1.MaterialUseDetails.Add(materialUseDetail);
                        }
                        var materialUse2 = new MaterialUseInShift() {
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            Shift1 = "",
                            Shift2 = ca2Name,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            UsedDate = date,
                            UsedCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.MaterialUse, 2),
                            Type = (int)MyUtilities.Material.UseType.Using,
                        };
                        updatedDetails1 = updatedDetails.Where(u => u.Quantity2 > 0);
                        foreach (var detail in updatedDetails1) {
                            if (detail.Quantity2 <= 0) continue;
                            var materialUseDetail = new MaterialUseDetail {
                                MachineId = detail.MachineId,
                                MaterialInvId = detail.MaterialInvId,
                                UseId = materialUse2.UseId,
                                // MaterialUseForm = materialUse,
                                EditQuantity = 0,
                                EditQuantity2 = Math.Round(detail.Quantity2, 2),
                                Quantity = 0,
                                Quantity2 = Math.Round(detail.Quantity2, 2),
                                Note = detail.Note,
                                //BoxNumber = detail.BoxNumber,
                                Lot = lot + detail.MachineName,
                            };
                            materialUse2.MaterialUseDetails.Add(materialUseDetail);
                        }
                        if (materialUse1.MaterialUseDetails.Any()) {
                            vfi.MaterialUseInShifts.Add(materialUse1);
                        }
                        if (materialUse2.MaterialUseDetails.Any()) {
                            vfi.MaterialUseInShifts.Add(materialUse2);
                        }
                        vfi.SaveChanges();
                    }
                    catch (Exception ex) {
                        ModelState.AddModelError("Material SX1", ex.Message);
                    }
                }

            }
            return View(new GridModel(new List<MaterialUseInShiftDetailModel>()));
        }


        public ActionResult SelectComboBoxtMaterialUseForm(string date) {
            var model = new List<MaterialUseInShiftModel>();
            try {
                model = GetAllMaterialUseForm((byte)MyUtilities.Transaction.Status.Approved, "", 1, date, date);
            }
            catch (Exception ex) {

            }
            return new JsonResult {
                Data = new SelectList(model, "UseId", "CodeName")
            };

        }

        [GridAction]
        public ActionResult SelectMaterialUseForm(byte? status, string formCode, int type, string fromDate, string toDate) {
            return View(new GridModel(GetAllMaterialUseForm(status, formCode, type, fromDate, toDate).OrderBy(mud => mud.UsedDate)));
        }

        [GridAction]
        public ActionResult CancelMaterialUseForm(int? useId) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("CancelMaterialUseForm",
                                         "Bạn đã bị mất quyền đăng nhập. " +
                                         "\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         "\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<MaterialUseInShiftModel>()));
            }
            if (useId != null && useId != 0)
                using (var vfi = new tammaContext()) {
                    var materialUse = vfi.MaterialUseInShifts.FirstOrDefault(mu => mu.UseId == useId);
                    if (materialUse.Status != (byte)MyUtilities.Transaction.Status.Open)
                        throw new AggregateException("Vui lòng cập nhật lại (F5)!");
                    materialUse.Status = (byte)MyUtilities.Transaction.Status.Cancel;
                    vfi.SaveChanges();
                }
            return View(new GridModel(GetAllMaterialUseForm(1, "", 0, "", "").OrderBy(mud => mud.UsedDate)));
        }

        List<MaterialUseInShiftModel> GetAllMaterialUseForm(byte? status, string formCode, int type, string fromDate, string toDate) {
            var model = new List<MaterialUseInShiftModel>();
            using (var vfi = new tammaContext()) {
                if (status == null || status == 0)
                    return model;
                var materialUseForms =
                    vfi.MaterialUseInShifts.Where(
                        mu => mu.Status == status
                    //&& (type == 0 || mu.Type == type) &&
                    //mu.UsedCode.Contains(formCode)
                            );
                if (!string.IsNullOrWhiteSpace(formCode))
                    materialUseForms = materialUseForms.Where(mu => mu.UsedCode.Equals(formCode));
                if (type != 0)
                    materialUseForms = materialUseForms.Where(mu => mu.Type == type);
                if (!string.IsNullOrWhiteSpace(fromDate) && !string.IsNullOrWhiteSpace(toDate)) {
                    var ci = new CultureInfo("vi-VN");
                    var fDate = string.IsNullOrWhiteSpace(fromDate)
                                   ? DateTime.Today
                                   : Convert.ToDateTime(fromDate, ci);
                    var tDate = string.IsNullOrWhiteSpace(toDate)
                                   ? DateTime.Today
                                   : Convert.ToDateTime(toDate, ci);
                    materialUseForms = materialUseForms.Where(mu => mu.UsedDate >= fDate && mu.UsedDate <= tDate);
                }
                foreach (var form in materialUseForms) {
                    var entity = new MaterialUseInShiftModel {
                        UseId = form.UseId,
                        UsedCode = form.UsedCode.Trim(),
                        UsedDate = form.UsedDate,
                        ModifiedDate = form.ModifiedDate,
                        ModifiedUser = form.ModifiedUser,
                        //ShiftName = "1" + form.Shift1 + " - 2" + form.Shift2,
                        StatusName =
                            MyUtilities.Transaction.CastText.GetTextStatus(form.Status),
                    };
                    if (!string.IsNullOrWhiteSpace(form.Shift1))
                        entity.ShiftName += " | 1" + form.Shift1;
                    if (!string.IsNullOrWhiteSpace(form.Shift2))
                        entity.ShiftName += " | 2" + form.Shift2;
                    entity.TypeName = form.Type == 1 ? "Sử dụng: " + entity.ShiftName : "Thu hồi NL";
                    entity.CodeName = entity.UsedCode + entity.ShiftName;
                    model.Add(entity);
                }
            }
            return model;
        }
        [GridAction]
        public ActionResult SelectMaterialUseDetailByFormId(int formId) {
            var model = new List<MaterialUseInShiftDetailModel>();
            try {
                model = GetMaterialUseDetail(formId).OrderBy(mud => mud.MachineName).ToList();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMaterialUseDetailByFormId", ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult UpdateMaterialUseDetail(int formId, int detailId, double editQuantity, double editQuantity2) {
            try {
                if (!Request.IsAuthenticated) {
                    ModelState.AddModelError("UpdateMaterialUseDetail",
                                             "Bạn đã bị mất quyền đăng nhập. " +
                                             "\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                             "\r\n Xin vui lòng đăng nhập lại hệ thống.");
                    return View(new GridModel(new List<MaterialUseInShiftDetailModel>()));
                }
                if (editQuantity < 0 || editQuantity2 < 0) {
                    throw new AggregateException("Nguyên liệu sử dụng ko được < 0 ! Vui lòng cập nhật lại (F5)!");
                }
                using (var vfi = new tammaContext()) {
                    var materialUse = vfi.MaterialUseInShifts.FirstOrDefault(mu => mu.UseId == formId);
                    if (materialUse == null) {
                        throw new AggregateException("Không tìm thấy phiếu SDNL ! Vui lòng cập nhật lại (F5)!");
                    }
                    var detail = vfi.MaterialUseDetails.FirstOrDefault(mud => mud.DetailId == detailId);
                    if (detail == null) {
                        throw new AggregateException("Không tìm thấy phiếu chi tiết SDNL ! Vui lòng cập nhật lại (F5)!");
                    }
                    if (materialUse.Status == (byte)MyUtilities.Transaction.Status.Open) {
                        // update material use details
                        detail.EditQuantity = Math.Round(editQuantity, 2);
                        detail.EditQuantity2 = Math.Round(editQuantity2, 2);
                        vfi.SaveChanges();
                    }
                    else if (materialUse.Status == (byte)MyUtilities.Transaction.Status.Approved) {
                        var inv = vfi.MaterialInvOnMachines.FirstOrDefault(mi => 
                            mi.MachineId == detail.MachineId && 
                            mi.MaterialInvId == detail.MaterialInvId);
                        if (inv == null) {
                            throw new AggregateException("Không tìm thấy tồn NL trên máy ! Vui lòng liên hệ người quản lý !");
                        }
                        var productionDetails = vfi.ImportFormSX1Detail.Where(id => 
                            id.UseDetailId == detailId && 
                            id.ImportFormSX1.Status == (byte)MyUtilities.Transaction.Status.Approved);
                        if (productionDetails.Any()) {
                            throw new AggregateException("Nguyên liệu sử dụng đã được duyệt sản xuất ! Không thể sửa !!! Vui lòng liên hệ người quản lý để biết thêm chi tiết !");
                        }

                        var changeSide = detail.EditQuantity > 0 ? 1 : detail.EditQuantity2 > 0 ? 2 : 0;
                        if (changeSide == 1) {
                            var diffQuantity = Math.Round(detail.EditQuantity - editQuantity, 2);
                            if (diffQuantity == 0)
                                throw new AggregateException("Số lượng mới không thể cập nhật ! Vui lòng cập nhật lại (F5) hoặc liên hệ quản lý!");
                            // update period
                            var usedPeriod = vfi.MaterialInvOnMachinePeriods.FirstOrDefault(pi =>
                                pi.MaterialInvId == detail.MaterialInvId &&
                                pi.MachineId == detail.MachineId &&
                                pi.PeriodDate == materialUse.UsedDate &&
                                pi.Quantity == detail.EditQuantity &&
                                pi.LastQuantity < pi.EarlyQuantity);
                            if (usedPeriod != null) {
                                usedPeriod.Quantity -= diffQuantity;
                                usedPeriod.LastQuantity = usedPeriod.EarlyQuantity - usedPeriod.Quantity;
                            }
                            // update sx1
                            var productionDetail = vfi.ImportFormSX1Detail.FirstOrDefault(id =>
                                id.UseDetailId == detailId &&
                                id.ImportFormSX1.Status != (byte)MyUtilities.Transaction.Status.Cancel &&
                                id.MaterialUse1 == detail.EditQuantity);
                            if (productionDetail != null) {
                                productionDetail.MaterialUse1 -= diffQuantity;
                            }
                            // update material inv on machine 
                            inv.TotalQuantity += diffQuantity;
                            // update material use details
                            detail.EditQuantity = Math.Round(editQuantity, 2);
                            vfi.SaveChanges();
                        }
                        else if (changeSide == 2) {
                            var diffQuantity = Math.Round(detail.EditQuantity2 - editQuantity2, 2);
                            if (diffQuantity == 0)
                                throw new AggregateException("Số lượng mới không thể cập nhật ! Vui lòng cập nhật lại (F5) hoặc liên hệ quản lý!");
                            // update period
                            var usedPeriod = vfi.MaterialInvOnMachinePeriods.FirstOrDefault(pi =>
                                pi.MaterialInvId == detail.MaterialInvId &&
                                pi.MachineId == detail.MachineId &&
                                pi.PeriodDate == materialUse.UsedDate &&
                                pi.Quantity == detail.EditQuantity2 &&
                                pi.LastQuantity < pi.EarlyQuantity);
                            if (usedPeriod != null) {
                                usedPeriod.Quantity -= diffQuantity;
                                usedPeriod.LastQuantity = usedPeriod.EarlyQuantity - usedPeriod.Quantity;
                            }
                            // update sx1
                            var productionDetail = vfi.ImportFormSX1Detail.FirstOrDefault(id =>
                                id.UseDetailId == detailId &&
                                id.ImportFormSX1.Status != (byte)MyUtilities.Transaction.Status.Cancel &&
                                id.MaterialUse1 == detail.EditQuantity2);
                            if (productionDetail != null) {
                                productionDetail.MaterialUse2 -= diffQuantity;
                            }
                            // update material inv on machine 
                            inv.TotalQuantity += diffQuantity;
                            // update material use details
                            detail.EditQuantity2 = Math.Round(editQuantity2, 2);
                            vfi.SaveChanges();
                        }
                        else {
                            throw new AggregateException("Số lượng cũ = 0 nên không thể cập nhật ! Vui lòng cập nhật lại (F5) hoặc liên hệ quản lý!");
                        }
                    }
                    else {
                        throw new AggregateException("Phiếu trong trạng thái không thể cập nhật ! Vui lòng cập nhật lại (F5) hoặc liên hệ quản lý!");
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateMaterialUseDetail", ex.Message);
            }
            return View(new GridModel(
                                   GetMaterialUseDetail(formId).OrderBy(mud => mud.MachineName)));
        }

        public List<MaterialUseInShiftDetailModel> GetMaterialUseDetail(int formId) {
            var model = new List<MaterialUseInShiftDetailModel>();
            try {
                var manager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.InvManagementLv2);

                using (var vfi = new tammaContext()) {
                    var materialUse = vfi.MaterialUseInShifts.FirstOrDefault(mus => mus.UseId == formId);
                    var waitingApproce =
                        vfi.MaterialUseDetails.Where(
                            md =>
                            md.MaterialUseInShift.Status == (byte)MyUtilities.Transaction.Status.Open &&
                            md.UseId != formId);
                    var assignMaterials = from em in vfi.ExportMaterialDetails
                                          where
                                              em.ExportMaterial.ExportDate == materialUse.UsedDate
                                          select new {
                                              em.MachineId,
                                              em.MaterialInvId,
                                              em.Quantity,
                                          };
                    foreach (var useDetail in materialUse.MaterialUseDetails) {
                        var materialInvOnMachine =
                            vfi.MaterialInvOnMachines.FirstOrDefault(
                                mim =>
                                mim.MachineId == useDetail.MachineId && mim.MaterialInvId == useDetail.MaterialInvId);
                        var materialInv =
                            vfi.MaterialInventories.FirstOrDefault(
                                mi => mi.MaterialInventoryId == useDetail.MaterialInvId);
                        var machine = vfi.Machines.FirstOrDefault(m => m.MachineId == useDetail.MachineId);
                        var entity = new MaterialUseInShiftDetailModel {
                            MachineId = useDetail.MachineId,
                            MachineName = machine.MachineName,
                            MaterialInvId = useDetail.MaterialInvId,
                            MaterialCode = MyUtilities.Material.GetMaterialInvDesignNo(useDetail.MaterialInventory),
                            EarlyQuantity = Math.Round(materialInvOnMachine.TotalQuantity, 2),
                            //LastQuantity = materialInvOnMachine.TotalQuantity ?? 0,
                            AssignQuantity = 0,
                            BoxNumber = 0,
                            Quantity = Math.Round(useDetail.Quantity, 2),
                            Quantity2 = Math.Round(useDetail.Quantity2, 2),
                            EditQuantity = Math.Round(useDetail.EditQuantity, 2),
                            EditQuantity2 = Math.Round(useDetail.EditQuantity2, 2),
                            LotNumber = materialInv.LotNumber,
                            Lot = "",
                            DetailId = useDetail.DetailId,
                            FormId = useDetail.UseId,
                            IsDestroy = useDetail.IsDetroy,
                            CanReUpdate = manager
                        };
                        if (entity.IsDestroy) {
                            entity.Note += "| Hủy nguyên liệu trên máy";
                        }
                        var assign =
                        assignMaterials.Where(
                            ed => ed.MachineId == entity.MachineId && ed.MaterialInvId == entity.MaterialInvId);
                        if (assign.Any()) {
                            entity.AssignQuantity = assign.Sum(ed => ed.Quantity);
                        }
                        var lastTrack =
                            vfi.TrackUpMachines.Where(
                                t =>
                                t.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                t.MachineId == entity.MachineId && t.DeliveryDate <= materialUse.UsedDate &&
                                t.MaterialId == materialInvOnMachine.MaterialInventory.MaterialId)
                               .OrderByDescending(t => t.DeliveryDate)
                               .FirstOrDefault();
                        if (lastTrack != null) {
                            entity.ProductCode = lastTrack.Product.ProductCode;
                            var realRate = MyUtilities.Product.GetProductRate(3000, lastTrack.WorkPiece, lastTrack.Product.Length ?? 0, lastTrack.KnifeCut); 
                            entity.WaitingNumber =
                                    MyUtilities.Product.GetMaterialRateInFactoryFullShiftTime(
                                                                lastTrack.RealProductivity,
                                                                realRate);
                        }
                        var waiting =
                            waitingApproce.Where(
                                md => md.MachineId == entity.MachineId && md.MaterialInvId == entity.MaterialInvId);
                        if (waiting.Any()) {
                            entity.Note += ("Đợi duyệt " + waiting.Sum(md => md.EditQuantity + md.EditQuantity2));
                        }
                        entity.LastQuantity = entity.EarlyQuantity - entity.EditQuantity - entity.EditQuantity2;
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return model;
        }

        [HttpPost]
        public ActionResult ApproveMaterialUseById(long[] checkedRecords) {
            try {
                if (!Request.IsAuthenticated)
                    return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

                var modifiedUser = HttpContext.User.Identity.Name;
                if (string.IsNullOrWhiteSpace(modifiedUser))
                    return Json(@"Vui lòng đăng nhập hệ thống. (user null). ");
                if (checkedRecords.Count() <= 0)
                    return Json(@"Vui lòng chọn ít nhất 1 lệnh. (checkedRecords <= 0). ");

                // material inventory - material inventory period
                using (var vfi = new tammaContext()) {
                    var materialUses = vfi.MaterialUseInShifts.Where(f => checkedRecords.Contains(f.UseId));
                    var endMaterialInvIds = new List<int>();
                    foreach (var useInShift in materialUses) {
                        if (useInShift.Status != (byte)MyUtilities.Transaction.Status.Open) continue;

                        if (MyUtilities.Transaction.IsLock(useInShift.UsedDate, MyUtilities.Transaction.ProductionLockType.Production1)) {
                            throw new AggregateException(
                                @"Ngày nhập SX bị khoá do tính lương! /n Vui lòng liên hệ quản lý tính lương !");
                        }
                        if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, useInShift.UsedDate)) {
                            throw new AggregateException(
                                @"Không có quyền duyệt phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                        }
                        foreach (var useDetail in useInShift.MaterialUseDetails) {
                            var materialOnMachine =
                                vfi.MaterialInvOnMachines.FirstOrDefault(
                                    mim =>
                                    mim.MachineId == useDetail.MachineId &&
                                    mim.MaterialInvId == useDetail.MaterialInvId);
                            if (materialOnMachine == null)
                                throw new AggregateException("(Null Error) Liên hệ Admin !\n" +
                                                             useDetail.Machine.MachineName);

                            var quantity = (Math.Round(useDetail.EditQuantity, 2) +
                                            Math.Round(useDetail.EditQuantity2, 2));
                            var invQuantity = Math.Round(materialOnMachine.TotalQuantity, 2);
                            if (invQuantity - quantity < 0)
                                throw new AggregateException("Nguyên liệu trên máy không đủ sử dụng !\n" +
                                                             useDetail.Machine.MachineName);
                            var period = new MaterialInvOnMachinePeriod {
                                EarlyQuantity = invQuantity,
                                Quantity = quantity,
                                LastQuantity = invQuantity - quantity,
                                MachineId = materialOnMachine.MachineId,
                                MaterialInvId = materialOnMachine.MaterialInvId,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                Note = useDetail.Note,
                                PeriodDate = useInShift.UsedDate,
                            };
                            if (useDetail.IsDetroy)
                                period.Note += " Hủy NL trên máy";
                            vfi.MaterialInvOnMachinePeriods.Add(period);
                            materialOnMachine.TotalQuantity -= quantity;
                            if (materialOnMachine.MaterialInventory.FirstUseDate == null)
                                materialOnMachine.MaterialInventory.FirstUseDate = useInShift.UsedDate;
                            if (Math.Round(materialOnMachine.TotalQuantity, 2) == 0) {
                                materialOnMachine.TotalQuantity = 0;
                                if (useInShift.Type == (int)MyUtilities.Material.UseType.Using || useDetail.IsDetroy) {
                                    if (Math.Round(materialOnMachine.MaterialInventory.TotalQty, 2) == 0) {
                                        endMaterialInvIds.Add(materialOnMachine.MaterialInvId.Value);
                                    }
                                }
                            }
                        }
                        if (useInShift.Type == (int)MyUtilities.Material.UseType.SendBack) {
                            var materialInvIds = useInShift.MaterialUseDetails.Select(mu => mu.MaterialInvId).Distinct();
                            var transaction = new Vfi.Models.Transaction {
                                TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Material, 1),
                                EoI = Convert.ToChar(MyUtilities.Transaction.EoIEnum.Import).ToString(),
                                MoP = true,
                                CreatedUser = HttpContext.User.Identity.Name,
                                CreatedDate = useInShift.UsedDate,
                                Status = (byte)MyUtilities.Transaction.Status.Approved,
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                            };
                            foreach (var materialInvId in materialInvIds) {
                                var materialUseDetailsById =
                                    useInShift.MaterialUseDetails.Where(
                                        mu => mu.MaterialInvId == materialInvId && !mu.IsDetroy);
                                var quantity =
                                    Math.Round(
                                        materialUseDetailsById.Sum(
                                            mu => (mu.EditQuantity + mu.EditQuantity2)), 2);
                                var materialInv =
                                    vfi.MaterialInventories.FirstOrDefault(mi => mi.MaterialInventoryId == materialInvId);
                                if (materialInv == null) throw new AggregateException("Lỗi hệ thống! Liên hệ Admin");
                                var materialInvPeriod = new MaterialInventoryPeriod {
                                    EarlyPeriodQuantity = materialInv.TotalQty,
                                    EarlyPeriodQuantityKg = materialInv.TotalQtyKg,
                                    Quantity = quantity,
                                    //LastPeriodQuantity = materialInv.TotalQty + quantity,
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    PeriodDate = useInShift.UsedDate,
                                    PeriodDay = useInShift.UsedDate.Day,
                                    PeriodMonth = useInShift.UsedDate.Month,
                                    PeriodYear = useInShift.UsedDate.Year,
                                    MaterialId = materialInv.MaterialId,
                                    MaterialInventoryId = materialInvId,
                                    TransactionId = transaction.TransactionId,
                                    Transaction = transaction,
                                };
                                materialInv.ModifiedDate = DateTime.Now;
                                materialInv.ModifiedUser = HttpContext.User.Identity.Name;
                                materialInv.TotalQty = Math.Round(materialInv.TotalQty + quantity, 2);
                                materialInv.TotalQtyKg = Math.Round((materialInv.TotalQty * materialInv.UnitWeight), 3);
                                materialInvPeriod.LastPeriodQuantity = materialInv.TotalQty;
                                materialInvPeriod.LastPeriodQuantityKg = materialInv.TotalQtyKg;
                                materialInv.EndDate = null;
                                vfi.MaterialInventoryPeriods.Add(materialInvPeriod);
                            }
                            vfi.Transactions.Add(transaction);
                        }
                        useInShift.Status = (byte)MyUtilities.Transaction.Status.Approved;
                    }
                    vfi.SaveChanges();
                    UpdateMaterialInventoryEndState(endMaterialInvIds);
                }
            }
            catch (Exception exception) {
                return
                    Json(@"Lỗi (try-catch). " + exception.Message);
            }
            return Json("okie");
        }
        #endregion

        int UpdateMaterialInventoryEndState(List<int> invIds) {
            var a = 0;
            using (var vfi = new tammaContext()) {
                var materialInvs = vfi.MaterialInventories.Where(x => invIds.Contains(x.MaterialInventoryId));
                foreach (var materialInv in materialInvs) {
                    if (Math.Round(materialInv.TotalQty, 2) == 0) {
                        materialInv.TotalQty = 0;
                        materialInv.TotalQtyKg = 0;
                        if (!materialInv.MaterialInvOnMachines.Any(x => x.TotalQuantity > 0)) {
                            materialInv.EndDate = null;
                        }
                        else {
                            materialInv.EndDate = DateTime.Now;
                        }
                    }
                    else {
                        materialInv.EndDate = null;
                    }
                }
                a = vfi.SaveChanges();
            }
            return a;
        }

        #region upload excel production

        [GridAction]
        public ActionResult SelectProductionDetailByExcel(string fileName, string sheetName) {
            var model = new List<SmartProductionModel>();
            if (string.IsNullOrWhiteSpace(fileName))
                return View(new GridModel(model));
            var dt = new DataTable();
            try {
                using (var conn = new OleDbConnection()) {
                    var destinationPath = Path.Combine(Server.MapPath("~/Content/FileUpload"), fileName);
                    string fileExtension = Path.GetExtension(destinationPath);
                    if (fileExtension == ".xls")
                        conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + destinationPath + ";" +
                                                "Extended Properties='Excel 8.0;HDR=YES;'";
                    if (fileExtension == ".xlsx")
                        conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + destinationPath + ";" +
                                                "Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                    using (var comm = new OleDbCommand()) {
                        //var sheetName = "Sheet1";
                        if (string.IsNullOrWhiteSpace(sheetName))
                            comm.CommandText = "Select * from [Data$]";
                        else
                            comm.CommandText = "Select * from [" + sheetName.Trim() + "$]";
                        comm.Connection = conn;
                        using (var da = new OleDbDataAdapter()) {
                            da.SelectCommand = comm;
                            da.Fill(dt);
                        }
                    }
                }
                using (var vfi = new tammaContext()) {
                    errorIndex = "";
                    oke = error = duplicate = 0;
                    dt.Rows.RemoveAt(0);
                    dt.Rows.RemoveAt(0);
                    dt.Rows.RemoveAt(0);

                    Session["SessionSmartProductionModel"] = new List<SmartProductionModel>();
                    foreach (DataRow row in dt.Rows) {
                        var nullable = row[0] + "";
                        if (string.IsNullOrWhiteSpace(nullable)) break;
                        var warehouseId = Convert.ToInt32(row[11] + "");
                        if (warehouseId == 0) continue;
                        var warehouse = vfi.Warehouses.FirstOrDefault(w => w.WarehouseId == warehouseId);
                        var machineName = row[1] + "";
                        var machine = vfi.Machines.FirstOrDefault(m => m.MachineName.Equals(machineName));
                        if (machine == null) {
                            errorIndex += "Error: Không tìm thấy máy: " + row[1] + ", vị trí " + nullable + "\n";
                            error++;
                            continue;
                        }
                        var productCode = row[2] + "";
                        var product = vfi.Products.FirstOrDefault(m => m.ProductCode.Equals(productCode));
                        if (product == null) {
                            errorIndex += "Error: Không tìm thấy SP: " + row[2] + ", vị trí " + nullable + "\n";
                            error++;
                            continue;
                        }
                        var materialName = row[3] + "";
                        var outDiameter = Convert.ToDouble(row[5]);
                        var inDiameter = Convert.ToDouble(row[6]);
                        var shape = row[7] + "";
                        var diameterType = row[8] + "";
                        Material material;
                        try {
                            material =
                                vfi.Materials.FirstOrDefault(
                                    m =>
                                    m.MaterialName.Equals(materialName) &&
                                    m.OutDiameter == outDiameter &&
                                    m.InDiameter == inDiameter &&
                                    m.Shape.Equals(shape) &&
                                    m.DiameterType.Equals(diameterType.Trim()));
                        }
                        catch (FormatException ex) {
                            errorIndex += "Error: format error: " + row[3] + ", vị trí " + nullable + "\n";
                            error++;
                            continue;
                        }
                        if (material == null) {
                            errorIndex += "Error: Không tìm thấy nguyên liệu: " + row[3] + ", vị trí " + nullable + "\n";
                            error++;
                            continue;
                        }
                        var vendor = row[4] + "";
                        var lotNumber = row[10] + "";
                        var length = Convert.ToDouble(row[9]) * 1000;
                        var materialInv = (from mi in vfi.MaterialInventories
                                           where
                                               mi.Vendor.VendorCode.Equals(vendor) &&
                                               mi.LotNumber.Equals(lotNumber) &&
                                               mi.MaterialId == material.MaterialId &&
                                               mi.Length == length
                                           select mi).FirstOrDefault();
                        if (materialInv == null) {
                            errorIndex += "Error: Không tìm thấy tồn nguyên liệu: " + row[3] + ", vị trí " + nullable +
                                          "\n";
                            error++;
                            continue;
                        }
                        var materialOnMachine = (from mim in vfi.MaterialInvOnMachines
                                                 where
                                                     mim.MaterialInvId == materialInv.MaterialInventoryId &&
                                                     mim.MachineId == machine.MachineId
                                                 select mim).FirstOrDefault();
                        if (materialOnMachine == null) {
                            errorIndex += "Error: Không tìm thấy nguyên liệu trên máy: " + row[3] + ", vị trí " +
                                          nullable + "\n";
                            error++;
                            continue;
                        }
                        var entity = new SmartProductionModel {
                            MachineId = machine.MachineId,
                            MachineName = machine.MachineName,
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            MaterialInvOnMachineId = materialOnMachine.Id,
                            MaterialInvTotal = materialOnMachine.TotalQuantity,
                            MaterialUse1 = Convert.ToDouble(row[12].ToString()),
                            QuantityExport1 = Convert.ToDouble(row[13].ToString()),
                            QuantityPending1 = Convert.ToDouble(row[14].ToString()),
                            QuantityDefect1 = Convert.ToDouble(row[15].ToString()),
                            MaterialUse2 = Convert.ToDouble(row[16].ToString()),
                            QuantityExport2 = Convert.ToDouble(row[17].ToString()),
                            QuantityPending2 = Convert.ToDouble(row[18].ToString()),
                            QuantityDefect2 = Convert.ToDouble(row[19].ToString()),
                            MaterialInventoryCode = materialInv.Vendor.VendorCode +
                                                    material.MaterialCode + "-" +
                                                    materialInv.LotNumber,
                            WarehouseExportId = warehouseId,
                            WarehouseExportName = warehouse.WarehouseName,
                        };

                        oke++;
                        model.Add(entity);
                    }
                }
            }
            catch (OleDbException oledbEx) {
                ModelState.AddModelError("OleDbException", oledbEx.Message);
            }
            catch (FormatException formatEx) {
                ModelState.AddModelError("FormatException", formatEx.Message);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectSessionSmartProductionByExcel", ex.Message);
            }
            Session["SessionSmartProductionModel"] = model;
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectProductionDetailByExcel2(string fileName, string sheetName, string materialUseDate) {
            if (string.IsNullOrWhiteSpace(materialUseDate))
                return View(new GridModel(new List<SmartProductionModel>()));
            var model = new List<SmartProductionModel>();
            if (string.IsNullOrWhiteSpace(fileName))
                return View(new GridModel(model));
            var dt = new DataTable();
            try {
                using (var conn = new OleDbConnection()) {
                    var destinationPath = Path.Combine(Server.MapPath("~/Content/FileUpload"), fileName);
                    string fileExtension = Path.GetExtension(destinationPath);
                    if (fileExtension == ".xls")
                        conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + destinationPath + ";" +
                                                "Extended Properties='Excel 8.0;HDR=YES;'";
                    if (fileExtension == ".xlsx")
                        conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + destinationPath + ";" +
                                                "Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                    using (var comm = new OleDbCommand()) {
                        //var sheetName = "Sheet1";
                        if (string.IsNullOrWhiteSpace(sheetName))
                            comm.CommandText = "Select * from [Data$]";
                        else
                            comm.CommandText = "Select * from [" + sheetName.Trim() + "$]";
                        comm.Connection = conn;
                        using (var da = new OleDbDataAdapter()) {
                            da.SelectCommand = comm;
                            da.Fill(dt);
                        }
                    }
                }

                using (var vfi = new tammaContext()) {
                    errorIndex = "";
                    oke = error = duplicate = 0;
                    dt.Rows.RemoveAt(0);
                    dt.Rows.RemoveAt(0);
                    dt.Rows.RemoveAt(0);
                    var ci = new CultureInfo("vi-VN");
                    var useDate = Convert.ToDateTime(materialUseDate, ci);
                    var trackUpProducions = vfi.TrackUpMachines.Where(
                        tp =>
                        tp.DeliveryDate != null && tp.DeliveryDate <= useDate &&
                        tp.Status != (byte)MyUtilities.Transaction.Status.Cancel)
                                               .OrderByDescending(tp => tp.DeliveryDate)
                                               .ThenByDescending(tp => tp.ModifiedDate);
                    Session["SessionSmartProductionModel"] = new List<SmartProductionModel>();
                    foreach (DataRow row in dt.Rows) {
                        var nullable = row[0] + "";
                        if (string.IsNullOrWhiteSpace(nullable)) break;
                        var warehouseId = Convert.ToInt32(row[13] + "");
                        if (warehouseId == 0) continue;
                        var warehouse = vfi.Warehouses.FirstOrDefault(w => w.WarehouseId == warehouseId);
                        if (warehouse == null) {
                            errorIndex += "Error: Lỗi kho: " + warehouseId + ", vị trí " + nullable + "\n";
                            error++;
                            continue;
                        }
                        var machineName = row[1] + "";
                        var machine = vfi.Machines.FirstOrDefault(m => m.MachineName.Equals(machineName));
                        if (machine == null) {
                            errorIndex += "Error: Không tìm thấy máy: " + machineName + ", vị trí " + nullable + "\n";
                            error++;
                            continue;
                        }

                        //if (machine.MachineId < 50) continue;
                        var productCode = row[2] + "";
                        var product = (from p in vfi.Products
                                       where p.ProductCode.Equals(productCode) && p.Active
                                       select new {
                                           p.ProductId,
                                           p.ProductCode,
                                           DiameterTypeDesign = p.DiameterTypeDesign + "",
                                           MaterialNameDesign = p.MaterialNameDesign + "",
                                           OutDiameterDesign = p.OutDiameterDesign ?? 0,
                                           InDiameterDesign = p.InDiameterDesign ?? 0,
                                           ShapeDesign = p.ShapeDesign + "",
                                           ProductionRate = p.ProductionRate ?? 0,
                                           ProductionWeight = p.ProductionWeight ?? 0,
                                           p.ProductionMaterials
                                       }).FirstOrDefault();
                        if (product == null) {
                            errorIndex += "Error: Không tìm thấy SP: " + productCode + ", vị trí " + nullable + "\n";
                            error++;
                            continue;
                        }
                        var materialName = row[3] + "";
                        var outDiameter = Convert.ToDouble(row[5]);
                        var inDiameter = Convert.ToDouble(row[6]);
                        var shape = row[7] + "";
                        var diameterType = row[8] + "";
                        Material material;
                        try {
                            material =
                                vfi.Materials.FirstOrDefault(
                                    m =>
                                    m.MaterialName.Equals(materialName) &&
                                    m.OutDiameter == outDiameter &&
                                    m.InDiameter == inDiameter &&
                                    m.Shape.Equals(shape) &&
                                    m.DiameterType.Equals(diameterType.Trim()));
                        }
                        catch (FormatException ex) {
                            errorIndex += "Error: format error: " + materialName + ", vị trí " + nullable + "\n";
                            error++;
                            continue;
                        }
                        if (material == null) {
                            errorIndex += "Error: Không tìm thấy nguyên liệu: " + materialName + ", vị trí " + nullable +
                                          "\n";
                            error++;
                            continue;
                        }
                        var vendor = row[4] + "";
                        var lotNumber = row[10] + "";
                        var length = Convert.ToDouble(row[9]) * 1000;
                        var materialInv = (from mi in vfi.MaterialInventories
                                           where
                                               mi.Vendor.VendorCode.Equals(vendor) &&
                                               mi.LotNumber.Equals(lotNumber) &&
                                               mi.Length == length &&
                                               mi.MaterialId == material.MaterialId
                                           select mi).FirstOrDefault();
                        if (materialInv == null) {
                            errorIndex += "Error: Không tìm thấy tồn nguyên liệu: " + materialName + ", vị trí " +
                                          nullable +
                                          "\n";
                            error++;
                            continue;
                        }
                        var materialOnMachine = (from mim in vfi.MaterialInvOnMachines
                                                 where
                                                     mim.MaterialInvId == materialInv.MaterialInventoryId &&
                                                     mim.MachineId == machine.MachineId
                                                 select mim).FirstOrDefault();
                        if (materialOnMachine == null) {
                            errorIndex += "Error: Không tìm thấy nguyên liệu trên máy: " + materialName + ", vị trí " +
                                          nullable + "\n";
                            error++;
                            continue;
                        }
                        var materialUse1 = row[14].ToString().Equals("-") ? 0 : Convert.ToDouble(row[14].ToString());
                        var materialUse2 = row[18].ToString().Equals("-") ? 0 : Convert.ToDouble(row[18].ToString());
                        var export1 = row[15].ToString().Equals("-") ? 0 : Convert.ToDouble(row[15].ToString());
                        var export2 = row[19].ToString().Equals("-") ? 0 : Convert.ToDouble(row[19].ToString());
                        var pending1 = row[16].ToString().Equals("-") ? 0 : Convert.ToDouble(row[16].ToString());
                        var pending2 = row[20].ToString().Equals("-") ? 0 : Convert.ToDouble(row[20].ToString());
                        var defect1 = row[17].ToString().Equals("-") ? 0 : Convert.ToDouble(row[17].ToString());
                        var defect2 = row[21].ToString().Equals("-") ? 0 : Convert.ToDouble(row[21].ToString());
                        var weight = row[11].ToString().Equals("-") ? 0 : Convert.ToDouble(row[11].ToString());
                        var rate = row[12].ToString().Equals("-") ? 0 : Convert.ToInt32(row[12].ToString());
                        var entity = new SmartProductionModel {
                            MachineId = machine.MachineId,
                            MachineName = machine.MachineName,
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            MaterialInvOnMachineId = materialOnMachine.Id,
                            MaterialInventoryId = materialInv.MaterialInventoryId,
                            MaterialId = material.MaterialId,
                            MaterialInvTotal = materialOnMachine.TotalQuantity,
                            MaterialUse1 = materialUse1,
                            QuantityExport1 = export1,
                            QuantityPending1 = pending1,
                            QuantityDefect1 = defect1,
                            MaterialUse2 = materialUse2,
                            QuantityExport2 = export2,
                            QuantityPending2 = pending2,
                            QuantityDefect2 = defect2,
                            ProductWeight = weight,
                            ProductionRate = rate,
                            MaterialInventoryCode = MyUtilities.Material.GetMaterialInvDesignNo(materialInv),
                            MaterialAlert = 1,
                            WarehouseExportId = warehouseId,
                            WarehouseExportName = warehouse.WarehouseName,
                            ProductAlert = 0,
                            UseDate = useDate,
                            UseDateString = useDate.ToString("dd/MM/yyyy")
                        };

                        if (entity.ProductWeight != product.ProductionWeight)
                            entity.ProductWeightStatus = 1;
                        var trackUpProducion =
                            trackUpProducions.FirstOrDefault(
                                tp => tp.MachineId == entity.MachineId);
                        if (trackUpProducion == null) {
                            entity.ProductAlert = 1; // yellow

                            if (entity.ProductionRate != product.ProductionRate)
                                entity.ProductionRateStatus = 1;

                            if (product.ProductionMaterials != null && product.ProductionMaterials.Any()) {
                                var productionMaterial =
                                    product.ProductionMaterials
                                           .FirstOrDefault(
                                               pm =>
                                               pm.Material.MaterialName.Equals(materialName) &&
                                               pm.Material.OutDiameter == outDiameter &&
                                               pm.Material.InDiameter == inDiameter &&
                                               pm.Material.Shape.Equals(shape) &&
                                               pm.Material.DiameterType.Equals(diameterType.Trim()));
                                if (productionMaterial == null)
                                    entity.MaterialStatus = 1;
                            }
                            else
                                entity.MaterialStatus = 1;
                        }
                        else {
                            if (trackUpProducion.ProductId != entity.ProductId)
                                entity.ProductAlert = 1; // yellow
                            if (trackUpProducion.MaterialId != entity.MaterialId)
                                entity.MaterialStatus = 1; // yellow
                            if (trackUpProducion.RealRate != entity.ProductionRate)
                                entity.ProductionRateStatus = 1;
                        }
                        oke++;
                        model.Add(entity);
                    }
                }
            }
            catch (OleDbException oledbEx) {
                ModelState.AddModelError("OleDbException", oledbEx.Message);
            }
            catch (FormatException formatEx) {
                ModelState.AddModelError("FormatException", formatEx.Message);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectSessionSmartProductionByExcel", ex.Message);
            }
            Session["SessionSmartProductionModel"] = model;
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectTrackUpByMachine(int machineId, string useDateString) {
            try {
                using (var vfi = new tammaContext()) {
                    var model = new List<TrackUpMachineModel>();
                    var ci = new CultureInfo("vi-VN");
                    var useDate = Convert.ToDateTime(useDateString, ci);
                    var tracks =
                        vfi.TrackUpMachines.Where(
                            tp =>
                            tp.Status != (byte)MyUtilities.Transaction.Status.Cancel &&
                            tp.DeliveryDate <= useDate && tp.MachineId == machineId)
                           .OrderByDescending(tp => tp.DeliveryDate).ToList();
                    var productIds = tracks.Select(t => t.ProductId).Distinct();
                    foreach (var productId in productIds) {
                        var track = tracks.FirstOrDefault(t => t.ProductId == productId);
                        var entity = new TrackUpMachineModel {
                            TrackId = track.TrackId,
                            MachineName = track.Machine.MachineName,
                            ProductCode = track.Product.ProductCode,
                            MaterialId = track.MaterialId,
                            MaterialCode = track.Material == null ? "" : track.Material.MaterialCode,
                            Productivity = track.Product.Productivity ?? 0,
                            ProductRate = track.Product.ProductionRate ?? 0,
                            DeliveryDate = track.DeliveryDate,
                            StartDate = track.StartDate,
                            RealProductivity = track.RealProductivity,
                            RealRate = track.RealRate,
                            KnifeCut = track.KnifeCut,
                            DeliveryEmployee = track.DeliveryEmployee,
                            Phase = track.Phase,
                            ReceiveEmployee = track.ReceiveEmployee,
                            WorkPiece = track.WorkPiece,
                            RoundPerMinute = track.RoundPerMinute,
                            Note = track.Note,
                            StatusColor = track.Status == 3 ? 2 : 1,
                            ModifiedUser = track.ModifiedUser,
                            ModifiedDate = track.ModifiedDate
                        };
                        model.Add(entity);
                    }
                    return View(new GridModel(model));
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectTrackUpInfo", ex.Message);
            }
            return View(new GridModel(new List<ProductModel>()));
        }

        public ActionResult SaveAllSmartProduction(string importDate, string ca1, string ca2, string materialUseDate) {
            if (!Request.IsAuthenticated) {
                return Json(@"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.", JsonRequestBehavior.AllowGet);
                //return View(new GridModel(new List<SalesOrderDetailModel>()));
            }
            try {
                if (string.IsNullOrWhiteSpace(importDate) || string.IsNullOrWhiteSpace(materialUseDate) ||
                    string.IsNullOrWhiteSpace(ca1) || string.IsNullOrWhiteSpace(ca2)) {
                    return Json("Lỗi! Vui lòng cập nhật đầy đủ dữ liệu", JsonRequestBehavior.AllowGet);
                }
                var ci = new CultureInfo("vi-VN");
                var productionDate = Convert.ToDateTime(importDate, ci);
                var useDate = Convert.ToDateTime(materialUseDate, ci);
                using (var vfi = new tammaContext()) {
                    var model = (List<SmartProductionModel>)Session["SessionSmartProductionModel"];
                    var import = new ImportFormSX1 {
                        ImportDate = productionDate,
                        TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        Shift1Name = ca1,
                        Shift2Name = ca2,
                        MaterialUseDate = useDate,
                        Status = (byte)MyUtilities.Transaction.Status.Approved
                    };
                    var transactionSX1 = new Vfi.Models.Transaction {
                        TransactionCode = import.TransactionCode,
                        CreatedUser = HttpContext.User.Identity.Name,
                        CreatedDate = productionDate,
                        WarehouseIssueId = null,
                        WarehouseReceiptId = 1,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        Status = (byte)MyUtilities.Transaction.Status.Open,
                        Active = true,
                        EoI = "0",
                        MoP = false

                    };
                    var transactionCXL = new Vfi.Models.Transaction {
                        TransactionCode = import.TransactionCode,
                        CreatedUser = HttpContext.User.Identity.Name,
                        CreatedDate = productionDate,
                        WarehouseIssueId = 1,
                        WarehouseReceiptId = 8,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        Status = (byte)MyUtilities.Transaction.Status.Open,
                        Active = true,
                        EoI = "0",
                        MoP = false,
                    };
                    var transactionPP = new Vfi.Models.Transaction {
                        TransactionCode = import.TransactionCode,
                        CreatedUser = HttpContext.User.Identity.Name,
                        CreatedDate = productionDate,
                        WarehouseIssueId = null,
                        WarehouseReceiptId = 9,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        Status = (byte)MyUtilities.Transaction.Status.Open,
                        Active = true,
                        EoI = "0",
                        MoP = false,
                    };
                    var importWorkpiece = new ImportWorkpieceMaterial {
                        ImportDate = useDate,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        ImportFormSX1 = import,
                        ImportSx1Id = import.ImportId,
                        Transaction = transactionSX1,
                        TransactionId = transactionSX1.TransactionId,
                    };
                    var useShift1 = ca1;
                    var useShift2 = ca2;
                    if (useShift1.Contains("A")) useShift1 = "A";
                    else if (useShift1.Contains("B")) useShift1 = "B";
                    else useShift1 = "C";
                    if (useShift2.Contains("A")) useShift2 = "A";
                    else if (useShift2.Contains("B")) useShift2 = "B";
                    else useShift2 = "C";
                    var materialUse = new MaterialUseInShift() {
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        Shift1 = useShift1,
                        Shift2 = useShift2,
                        Status = (byte)MyUtilities.Transaction.Status.Approved,
                        UsedDate = useDate,
                        UsedCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.MaterialUse, 1),
                        Type = 1,
                    };
                    var miMachine = vfi.MaterialInvOnMachines;
                    var importDetails = new List<ImportFormSX1Detail>();
                    var transactionDetailsSX1 = new List<Vfi.Models.TransactionDetail>();
                    var transactionDetailsCXL = new List<Vfi.Models.TransactionDetail>();
                    var transactionDetailsPP = new List<Vfi.Models.TransactionDetail>();
                    var machineLogs = new List<MachineLog>();
                    var mimPeriod = new List<MaterialInvOnMachinePeriod>();
                    var listMaterialDesign = new List<ProductionMaterial>();
                    var listProductInv = new List<ProductInventory>();
                    var lot = MyUtilities.MySystem.LotNumber_Weekly(useDate);
                    foreach (var entity in model) {
                        var materialInv =
                            miMachine.FirstOrDefault(
                                mim =>
                                mim.MachineId == entity.MachineId && mim.MaterialInvId == entity.MaterialInventoryId);
                        if (materialInv == null)
                            return Json("Lỗi! Không tìm thấy nguyên liệu tồn trên máy " +
                                                         entity.MaterialInventoryCode, JsonRequestBehavior.AllowGet);
                        var quantity =
                            Math.Round(materialInv.TotalQuantity - entity.MaterialUse1 - entity.MaterialUse2, 2);
                        if (quantity < 0)
                            return Json("Lỗi! Nguyên liệu tồn trên máy không đủ sử dụng " +
                                                         entity.MaterialInventoryCode, JsonRequestBehavior.AllowGet);
                        var materialUseDetail = new MaterialUseDetail {
                            MachineId = entity.MachineId,
                            MaterialInvId = entity.MaterialInventoryId,
                            UseId = materialUse.UseId,
                            // MaterialUseForm = materialUse,
                            EditQuantity = Math.Round(entity.MaterialUse1, 2),
                            EditQuantity2 = Math.Round(entity.MaterialUse2, 2),
                            Quantity = Math.Round(entity.MaterialUse1, 2),
                            Quantity2 = Math.Round(entity.MaterialUse2, 2),
                            Note = "Upload excel",
                            //BoxNumber = detail.BoxNumber,
                            Lot = lot + entity.MachineName,
                            IsDetroy = false,
                        };
                        materialInv.TotalQuantity = quantity;
                        materialInv.MaterialInventory.EndDate = DateTime.Now;
                        var materialOnMachinePeriod = new MaterialInvOnMachinePeriod {
                            EarlyQuantity = Math.Round(quantity + entity.MaterialUse1 + entity.MaterialUse2, 2),
                            Quantity = Math.Round(entity.MaterialUse1 + entity.MaterialUse2, 2),
                            LastQuantity = quantity,
                            MachineId = entity.MachineId,
                            MaterialInvId = entity.MaterialInventoryId,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            Note = "Upload excel",
                            PeriodDate = useDate,
                        };
                        mimPeriod.Add(materialOnMachinePeriod);
                        materialUse.MaterialUseDetails.Add(materialUseDetail);
                        if (entity.QuantityExport1 + entity.QuantityExport2 + entity.QuantityPending1 +
                            entity.QuantityPending2 + entity.QuantityDefect1 + entity.QuantityDefect2 > 0) {
                            var mUse1 = entity.MaterialUse1;
                            var mUse2 = entity.MaterialUse2;
                            if (entity.MaterialUse1 == 0) {
                                if (entity.QuantityDefect1 + entity.QuantityExport1 + entity.QuantityPending1 > 0)
                                    return Json("Ca 1 không sử dụng nguyên liệu trên máy " + entity.MachineName,
                                        JsonRequestBehavior.AllowGet);

                            }
                            else if (entity.MaterialUse2 == 0) {
                                if (entity.QuantityDefect2 + entity.QuantityExport2 + entity.QuantityPending2 > 0)
                                    return Json(
                                        "Ca 2 không sử dụng nguyên liệu trên máy " + entity.MachineName,
                                        JsonRequestBehavior.AllowGet);
                            }
                            if (entity.ProductId == 0 || entity.WarehouseExportId == 0 || entity.MachineId == 0) {
                                return Json("Vui lòng điền đầy đủ thông tin ở máy " + entity.MachineName,
                                            JsonRequestBehavior.AllowGet);
                            }
                            if (entity.ProductionRate <= 0)
                                return Json("Vui lòng điền định mức ở máy " + entity.MachineName,
                                            JsonRequestBehavior.AllowGet);
                            if (entity.ProductWeight <= 0)
                                return Json("Vui lòng điền trọng lượng SP ở máy " + entity.MachineName,
                                            JsonRequestBehavior.AllowGet);
                            var importDetail = new ImportFormSX1Detail {
                                ImportFormSX1 = import,
                                Machine = entity.MachineName,
                                MachineId = entity.MachineId,
                                ProductId = entity.ProductId,
                                //Shift1 = ca1,
                                //Shift2 = ca2,
                                Number1 = entity.QuantityExport1,
                                Number2 = entity.QuantityExport2,
                                Processing1 = entity.QuantityPending1,
                                Processing2 = entity.QuantityPending2,
                                DefectProduct1 = entity.QuantityDefect1,
                                DefectProduct2 = entity.QuantityDefect2,
                                MaterialInvId = entity.MaterialInventoryId,
                                MaterialUse1 = mUse1,
                                MaterialUse2 = mUse2,
                                ProductionRate = entity.ProductionRate,
                                ProductWeight = entity.ProductWeight,
                            };
                            entity.MaterialUse1 = mUse1;
                            entity.MaterialUse2 = mUse2;
                            var log = new MachineLog {
                                MachineId = entity.MachineId,
                                DateLog = productionDate,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                Note = entity.GetProductionLog(),
                                Type = (byte)MachineLogTypeEnum.Product
                            };
                            machineLogs.Add(log);
                            importDetails.Add(importDetail);
                            var smartProduction =
                                vfi.SmartProductions.FirstOrDefault(sp => sp.MachineId == entity.MachineId);
                            if (smartProduction == null) {
                                smartProduction = new SmartProduction();
                                smartProduction.MachineId = entity.MachineId;
                                smartProduction.ProductId = entity.ProductId;
                                smartProduction.WarehouseId = entity.WarehouseExportId;
                                //smartProduction.ProductionRate = entity.ProductionRate;
                                vfi.SmartProductions.Add(smartProduction);
                            }
                            else {
                                smartProduction.ProductId = entity.ProductId;
                                smartProduction.WarehouseId = entity.WarehouseExportId;
                                //smartProduction.ProductionRate = entity.ProductionRate;
                            }//
                            var product = vfi.Products.FirstOrDefault(p => p.ProductId == importDetail.ProductId);
                            if (product.ProductionRate == null || product.ProductionRate == 0)
                                product.ProductionRate = importDetail.ProductionRate;
                            if (importDetail.ProductWeight != 1 &&
                                product.ProductionWeight != importDetail.ProductWeight) {
                                product.ProductionWeight = importDetail.ProductWeight;
                            }
                            if (product.QcWeight == null || product.QcWeight == 0 || product.QcWeight == 1)
                                product.QcWeight = product.ProductionWeight;
                            //
                            var materialDesign =
                                vfi.ProductionMaterials.FirstOrDefault(
                                    pm => pm.ProductId == product.ProductId && pm.Active);
                            if (materialDesign == null) {
                                var materialInv2 =
                                    vfi.MaterialInventories.FirstOrDefault(
                                        mi => mi.MaterialInventoryId == entity.MaterialInventoryId);
                                materialDesign = new ProductionMaterial {
                                    ProductId = product.ProductId,
                                    Priority = 1,
                                    Note = "",
                                    MaterialId = materialInv2.MaterialId,
                                    UnitWeightByMaterial = MyUtilities.Product
                                        .GetProductWeight(materialInv2.Material.MaterialName,
                                                          materialInv2.Material.OutDiameter,
                                                          materialInv2.Material.InDiameter,
                                                          product.Length ?? 0,
                                                          product.KnifeCut ?? 0,
                                                          materialInv2.Material.Shape + ""),
                                    Active = true,
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                };
                                listMaterialDesign.Add(materialDesign);
                            }

                        }
                        else {
                            var smartProduction =
                                vfi.SmartProductions.FirstOrDefault(sp => sp.MachineId == entity.MachineId);
                            if (smartProduction == null) {
                            }
                            else {
                                if (string.IsNullOrWhiteSpace(entity.ProductCode))
                                    smartProduction.ProductId = null;
                                if (string.IsNullOrWhiteSpace(entity.WarehouseExportName))
                                    smartProduction.WarehouseId = null;
                            }

                        }
                        if (entity.QuantityExport1 + entity.QuantityExport2 +
                            entity.QuantityPending1 + entity.QuantityPending2 != 0) {
                            var transactionDetailSX1 =
                                transactionDetailsSX1.FirstOrDefault(td => td.ReferenceId == entity.ProductId);
                            if (transactionDetailSX1 == null) {
                                transactionDetailSX1 = new Vfi.Models.TransactionDetail {
                                    Transaction = transactionSX1,
                                    TransactionId = transactionSX1.TransactionId,
                                    ReferenceId = entity.ProductId,
                                    MoP = false,
                                    Quantity =
                                        entity.QuantityExport1 + entity.QuantityExport2 +
                                        entity.QuantityPending1 + entity.QuantityPending2,
                                    UnitMeasure = null,
                                    Active = true,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    ModifiedDate = DateTime.Now,
                                    QuantityKg = 0,
                                    Note = entity.MachineName,
                                };
                                transactionDetailsSX1.Add(transactionDetailSX1);
                            }
                            else {
                                transactionDetailSX1.Quantity += entity.QuantityExport1 + entity.QuantityExport2 +
                                                                 entity.QuantityPending1 + entity.QuantityPending2;
                                transactionDetailSX1.Note += entity.MachineName + ",";
                            }
                            var productInv =
                                vfi.ProductInventories.FirstOrDefault(
                                    pi =>
                                    pi.WarehouseId == MyUtilities.Warehouse.Production1 &&
                                    pi.ProductId == transactionDetailSX1.ReferenceId);
                            if (productInv == null) {
                                productInv = new ProductInventory {
                                    WarehouseId = MyUtilities.Warehouse.Production1,
                                    ProductId = transactionDetailSX1.ReferenceId.Value,
                                    ImportDate = transactionSX1.CreatedDate,
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    TotalQty = 0,
                                };
                                listProductInv.Add(productInv);
                            }
                        }

                        if (entity.QuantityDefect1 + entity.QuantityDefect2 != 0) {
                            var transactionDetailPP =
                                transactionDetailsPP.FirstOrDefault(td => td.ReferenceId == entity.ProductId);
                            if (transactionDetailPP == null) {
                                transactionDetailPP = new Vfi.Models.TransactionDetail {
                                    Transaction = transactionPP,
                                    TransactionId = transactionPP.TransactionId,
                                    ReferenceId = entity.ProductId,
                                    MoP = false,
                                    Quantity = entity.QuantityDefect1 + entity.QuantityDefect2,
                                    UnitMeasure = null,
                                    Active = true,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    ModifiedDate = DateTime.Now,
                                    QuantityKg = 0,
                                    Note = entity.MachineName,
                                };
                                transactionDetailsPP.Add(transactionDetailPP);
                            }
                            else {
                                transactionDetailPP.Quantity += entity.QuantityDefect1 + entity.QuantityDefect2;
                                transactionDetailPP.Note += entity.MachineName + ",";
                            }
                        }
                        if (entity.QuantityPending1 + entity.QuantityPending2 != 0) {
                            var transactionDetailCXL =
                                transactionDetailsCXL.FirstOrDefault(td => td.ReferenceId == entity.ProductId);
                            if (transactionDetailCXL == null) {
                                transactionDetailCXL = new Vfi.Models.TransactionDetail {
                                    Transaction = transactionCXL,
                                    TransactionId = transactionCXL.TransactionId,
                                    ReferenceId = entity.ProductId,
                                    MoP = false,
                                    Quantity = entity.QuantityPending1 + entity.QuantityPending2,
                                    UnitMeasure = null,
                                    Active = true,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    ModifiedDate = DateTime.Now,
                                    QuantityKg = 0,
                                    Note = entity.MachineName,
                                    ErrorId = 1
                                };
                                transactionDetailsCXL.Add(transactionDetailCXL);
                                var productInv =
                                    vfi.ProductInventories.FirstOrDefault(
                                        pi =>
                                        pi.WarehouseId == MyUtilities.Warehouse.Production1 &&
                                        pi.ProductId == transactionDetailCXL.ReferenceId);
                                if (productInv == null) {
                                    productInv =
                                        listProductInv.FirstOrDefault(
                                            pi =>
                                            pi.WarehouseId == MyUtilities.Warehouse.Production1 &&
                                            pi.ProductId == transactionDetailCXL.ReferenceId);
                                }
                                transactionDetailCXL.ProductInventory = productInv;
                            }
                            else {
                                transactionDetailCXL.Quantity += entity.QuantityPending1 + entity.QuantityPending2;
                                transactionDetailCXL.Note += entity.MachineName + ",";
                            }
                        }
                    }
                    var warehouses = model.Select(i => i.WarehouseExportId).Distinct();
                    var transacions = new List<Vfi.Models.Transaction>();
                    foreach (var warehouseId in warehouses) {
                        var transactionExport = new Vfi.Models.Transaction {
                            TransactionCode = import.TransactionCode,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = productionDate,
                            WarehouseIssueId = 1,
                            WarehouseReceiptId = warehouseId,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            EoI = "0",
                            MoP = false,
                        };
                        var importDetailOnWahouses = model.Where(i => i.WarehouseExportId == warehouseId);
                        foreach (var importDetailOnWahouse in importDetailOnWahouses) {
                            if (importDetailOnWahouse.QuantityExport1 + importDetailOnWahouse.QuantityExport2 > 0) {
                                var transactionDetailExport =
                                    transactionExport.TransactionDetails.FirstOrDefault(
                                        td => td.ReferenceId == importDetailOnWahouse.ProductId);
                                if (transactionDetailExport == null) {
                                    transactionDetailExport = new Vfi.Models.TransactionDetail {
                                        Transaction = transactionExport,
                                        TransactionId = transactionExport.TransactionId,
                                        ReferenceId = importDetailOnWahouse.ProductId,
                                        MoP = false,
                                        Quantity =
                                            importDetailOnWahouse.QuantityExport1 +
                                            importDetailOnWahouse.QuantityExport2,
                                        UnitMeasure = null,
                                        Active = true,
                                        ModifiedUser = HttpContext.User.Identity.Name,
                                        ModifiedDate = DateTime.Now,
                                        QuantityKg = 0,
                                    };
                                    transactionExport.TransactionDetails.Add(transactionDetailExport);
                                    var productInv =
                                    vfi.ProductInventories.FirstOrDefault(
                                        pi =>
                                        pi.WarehouseId == MyUtilities.Warehouse.Production1 &&
                                        pi.ProductId == transactionDetailExport.ReferenceId);
                                    if (productInv == null) {
                                        productInv =
                                            listProductInv.FirstOrDefault(
                                                pi =>
                                                pi.WarehouseId == MyUtilities.Warehouse.Production1 &&
                                                pi.ProductId == transactionDetailExport.ReferenceId);
                                    }
                                    transactionDetailExport.ProductInventory = productInv;
                                }
                                else {
                                    transactionDetailExport.Quantity += importDetailOnWahouse.QuantityExport1 +
                                                                        importDetailOnWahouse.QuantityExport2;
                                }
                            }
                        }
                        if (transactionExport.TransactionDetails.Any())
                            transacions.Add(transactionExport);
                    }
                    if (importDetails.Count > 0) {
                        vfi.ProductInventories.AddRange(listProductInv);
                        vfi.ImportFormSX1.Add(import);
                        vfi.ImportFormSX1Detail.AddRange(importDetails);
                        vfi.MachineLogs.AddRange(machineLogs);
                        vfi.MaterialUseInShifts.Add(materialUse);
                        vfi.MaterialInvOnMachinePeriods.AddRange(mimPeriod);
                    }
                    if (transactionDetailsSX1.Count > 0) {
                        vfi.Transactions.Add(transactionSX1);
                        vfi.TransactionDetails.AddRange(transactionDetailsSX1);
                        vfi.ImportWorkpieceMaterials.Add(importWorkpiece);
                    }
                    if (transactionDetailsPP.Count > 0) {
                        vfi.Transactions.Add(transactionPP);
                        vfi.TransactionDetails.AddRange(transactionDetailsPP);
                    }
                    if (transactionDetailsCXL.Count > 0) {
                        vfi.Transactions.Add(transactionCXL);
                        vfi.TransactionDetails.AddRange(transactionDetailsCXL);
                    }
                    if (transacions.Any()) {
                        vfi.Transactions.AddRange(transacions);
                    }
                    if (listMaterialDesign.Any())
                        vfi.ProductionMaterials.AddRange(listMaterialDesign);
                    vfi.SaveChanges();
                    Session["SessionSmartProductionModel"] = new List<SmartProductionModel>();
                    return Json("Ok All", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
            return Json("Failed", JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}