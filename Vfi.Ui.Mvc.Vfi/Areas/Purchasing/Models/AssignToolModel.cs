using System;
namespace Vfi.Ui.Mvc.Vfi.Areas.Purchasing.Models
{
    public class AssignToolModel 
    {
        public int ToolId { get; set; }
        public double TotalInv { get; set; }
        public string ToolCode { get; set; }
        public string ToolName { get; set; }
        public string ToolDesignNo { get; set; }
        public string ToolFullCodeName { get; set; }
        public string LotNumber { get; set; }
        public int ToolInvId { get; set; }
        public int VendorId { get; set; }
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public string UnitMeasure { get; set; }
        public double AvailInv { get; set; }
        public bool NG { get; set; }
        public bool Lock { get; set; }

        public string VendorCodeName {
            get{
                return VendorCode + " - " + VendorName;
                }
        }
    }
}