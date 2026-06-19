using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vfi.Ui.Mvc.Vfi.Models;
using System.ComponentModel.DataAnnotations;
using Vfi.Ui.Mvc.Vfi.Models.Production;

namespace Vfi.Ui.Mvc.Vfi.Areas.Purchasing.Models
{
    public class EvaluationFormModel
    {

        public int FormId { get; set; }
        public int VendorId { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public string ModifiedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ApprovedUser { get; set; }
        public DateTime? ApprovedDate { get; set; }

        public Nullable<int> TechSpPoint { get; set; }
        public Nullable<int> LogisticsPoint { get; set; }
        public Nullable<int> QuantityDeliveryPoint { get; set; }
        public Nullable<int> NGTimePoint { get; set; }
        public Nullable<int> NGNumberPoint { get; set; }
        public Nullable<int> PricePoint { get; set; }
        public Nullable<int> TotalPoint { get; set; }
        public Nullable<int> ZeroPointCount { get; set; }

        public int LateDeliveryTimes { get; set; }      // 1.so lan giao hang tre
        public int DeliveryLessThenOrder { get; set; }  // 2.So lan Giao hang thieu
        public int NGTimes { get; set; }                // 3.so lan bi NG
        public int TotalNGQuantity { get; set; }        // 4.So luong hang` bi NG

        public int Index { get; set; }
        public string VendorName { get; set; }
        public string ClassifiedName { get; set; }
        // 1 + 2 + 3 + 4
        public string PurchaseOrderCode { get; set; }
        public string ImportCode { get; set; }

        public DateTime? OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryDateString {
            get {
                return DeliveryDate != DateTime.MinValue
                    ? DeliveryDate.ToString("dd/MM/yyyy") // hoặc format khác tùy nhu cầu
                    : null;
            }
        }
        public int CountLateDelivery { get; set; }
        public string CountLateDeliveryStr {
            get {
                return CountLateDelivery == 1
                    ? "Trễ"
                    : null;
            }
        }


        public double OrderQuantity { get; set; }
        public double ReceivedQuantity { get; set; }
        public double RemainingQuantity { get; set; }
        public int CountLessThanOrder { get; set; }
        public string CountLessThanOrderStr {
            get {
                return CountLessThanOrder == 1
                    ? "Thiếu"
                    : null;
            }
        }
        public int CountNG { get; set; }
        public string CountNGStr {
            get {
                return CountNG == 1
                    ? "NG"
                    : null;
            }
        }


        public double NGQuantity { get; set; }

        public string MaterialCode { set; get; }
        public string LotNumber { get; set; }
        public string UnitMeasure { get; set; }
        public string Grade { get; set; }



    }
}