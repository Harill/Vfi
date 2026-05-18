using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vfi.Ui.Mvc.Vfi.Models;
using System.ComponentModel.DataAnnotations;
using Vfi.Ui.Mvc.Vfi.Models.Production;

namespace Vfi.Ui.Mvc.Vfi.Areas.Purchasing.Models
{
    public class InquiryPoModel
    {
        public InquiryPoModel()
        {
            OrderQty = 0;
            UnitPrice = 0;
            Currency = "";
        }
        public long InquiryId { get; set; }
        public string InquiryNumber { get; set; }

        public int VendorId { get; set; }
        [DataType("_VendorEditByClasstifiedTemplate")]
        public string VendorCode { get; set; }

        public int ReferenceId { get; set; }
        [DataType("_MaterialEditByTypeTemplate")]
        public string ReferenceCode { get; set; }
        public string ReferenceName { get; set; }

        public int TypeId { get; set; }
        public string TypeName { get; set; }

        public int ClassifiedId { get; set; }
        public string ClassifiedName { get; set; }

        public double TotalInv { get; set; }

        public long PoDetailId { get; set; }
        public long PoId { get; set; }
        public long PoNumber { get; set; }

        [DataType("_PlatingUnitTemplate")]
        public string Unit { get; set; }
        [DataType("Number2")]
        public double OrderQty { get; set; }

        public double Price { get { return OrderQty * UnitPrice; } }
        public string PriceStr
        {
            get {
                return Currency.Equals("VND")
                        ? string.Format("{0:n0}", Price)
                        : string.Format("{0:n2}", Price);
            }
        }

        [DataType("Number4")]
        public double UnitPrice { get; set; }
        public string UnitPriceStr
        {
            get
            {
                return (string.IsNullOrWhiteSpace(Currency) && UnitPrice == 0)
                    ? "" : Currency.Equals("VND") ?
                    string.Format("{0:n0}", UnitPrice) :
                    string.Format("{0:n4}", UnitPrice);
            }
        }

        [DataType("_DateTemplate")]
        public DateTime? DueDate { get; set; }
        public byte Status { get; set; }

        [DataType("_CurrencyEditTemplate")]
        public string Currency { get; set; }
        public string Note { get; set; }

        public int State {
            get {
                if (DueDate == null) return 1;
                if (DueDate.Value <= DateTime.Today.AddDays(3)) return 1; // red
                if (DueDate.Value <= DateTime.Today.AddDays(7)) return 2; // yellow
                return 0;
            }
        }

        public string ModifiedUser { get; set; }
        public DateTime ModifiedDate { get; set; }

        public WorkGroupInfo Info { get; set; }

        public int PurchasingSignatureType { get; set; }

        public double Total3MonthsUsed { get; set; }

        public string Standard { get; set; }
        

    }
}