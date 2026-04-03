using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vfi.Ui.Mvc.Vfi.Areas.Factory.Models
{
    public class MachineHistoryModel
    {
        public MachineHistoryModel() {
            ProductDetails = new List<ProductDetailModel>();
            MaterialDetails = new List<MaterialDetailModel>();
            ToolDetails = new List<ToolDetailModel>();
            RepairDetails = new List<RepairDetailModel>();
        }
        public double CountProductId { get; set; }                                 //25/03/2026
        public double TotalProduction { get; set; }
        public double TotalDefect { get; set; }
        public double ProductProduction { get; set; }
        public double TotalProductPrice { get; set; }
        public double TotalDefectPrice { get; set; }
        public double TotalAll { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public double ProductUnitPrice { get; set; }
        public int ProductId { get; set; }
        public string ProductCurrency { get; set; }



        public double CountMaterialId { get; set; }                                 
        public double TotalMaterialUsed { get; set; }
        public double TotalMaterialCost { get; set; }
        public string MaterialName { get; set; }
        public double MaterialUnitPrice { get; set; }
        public int MaterialId { get; set; }
        public int MaterialInvId { get; set; }
        public double MaterialUnitWeight { set; get; }

        public double CountToolId { get; set; }                                    
        public double TotalToolUsed { get; set; }
        public double TotalToolPrice { get; set; }
        public string ToolName { get; set; }
        public double ToolUnitPrice { get; set; }
      
        
        public int Index { get; set; }                                          
        public int MachineId { get; set; }
        public string MachineName { get; set; }


        public double MaxProductionTime { get; set; }
        public int CountRepairTimes { get; set; }
        public double TotalRepairTime { get; set; }





        public List<ProductDetailModel> ProductDetails { get; set; }
        public List<MaterialDetailModel> MaterialDetails { get; set; }
        public List<ToolDetailModel> ToolDetails { get; set; }
        public List<RepairDetailModel> RepairDetails { get; set; }



    }

    public class ProductDetailModel {
        public int ProductId { get; set; }
        public int Index { get; set; }
        public string ProductCode { get; set; }
        public double ProductUnitPrice { get; set; }
        public double TotalProduct { get; set; }
        public double TotalDefect { get; set; }
        public string Currency { get; set; }
        public double ProductPrice { get; set; }
        public double DefectPrice { get; set; }
        public double TotalPrice { get; set; }



    }


    public class MaterialDetailModel {
        public int MaterialId { get; set; }
        public string MaterialCode { get; set; }
        public double MaterialUnitPrice { get; set; }
        public double TotalMaterial { get; set; }
        public double MaterialPrice { get; set; }
        public double MaterialUnitWeight { get; set; }
        public string MaterialLot { get; set; }
        public int Index { get; set; }


    }

    public class ToolDetailModel {
        public int ToolId { get; set; }
        public string ToolCode { get; set; }        
        public string ToolName { get; set; }

        public double ToolUnitPrice { get; set; }
        public double TotalToolUsed { get; set; }
        public double ToolPrice { get; set; }

        public int ProductId { get; set; }
        public int Index { get; set; }

    }



    public class RepairDetailModel {
        public string ProductCode { get; set; }
        public string ErrorCause { get; set; }
        public string HowToFix { get; set; }
        public double FixTime { get; set; }

        public DateTime CauseDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }

        public string StatusMachine { get; set; }
        public int Index { get; set; }

    }
}
