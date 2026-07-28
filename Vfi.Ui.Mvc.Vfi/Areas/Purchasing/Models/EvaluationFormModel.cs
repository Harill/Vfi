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

    public class LogisticsModel {
        public bool PlaceA { get; set; }
        public string PlaceAStr {
            get {
                return PlaceA == true
                    ? Count.ToString()
                    : "";
            }
        }

        public bool PlaceB { get; set; }
        public string PlaceBStr {
            get {
                return PlaceB == true
                    ? Count.ToString()
                    : "";
            }
        }

        public bool PlaceC { get; set; }
        public string PlaceCStr {
            get {
                return PlaceC == true
                    ? Count.ToString()
                    : "";
            }
        }

        public bool PlaceD { get; set; }
        public string PlaceDStr {
            get {
                return PlaceD == true
                    ? Count.ToString()
                    : "";
            }
        }

        public int Count { get; set; }
        public string BlankSpace {get; set;}
        //public double Quanitty { get; set; }
    }


    public class NGQuantityModel {
        public bool PlaceA { get; set; }
        public string PlaceAStr {
            get {
                return PlaceA == true
                    ? Count.ToString()
                    : "";
            }
        }

        public bool PlaceB { get; set; }
        public string PlaceBStr {
            get {
                return PlaceB == true
                    ? Count.ToString()
                    : "";
            }
        }

        public bool PlaceC { get; set; }
        public string PlaceCStr {
            get {
                return PlaceC == true
                    ? Count.ToString()
                    : "";
            }
        }

        public bool PlaceD { get; set; }
        public string PlaceDStr {
            get {
                return PlaceD == true
                    ? Count.ToString()
                    : "";
            }
        }

        public double Count { get; set; }
        public string BlankSpace { get; set; }
    }

    public class PrintEvaluationForm {
        public Nullable<int> PricePoint { get; set; }
        public Nullable<int> PricePointNextYear {
            get {
                return PricePoint < 10
                    ? PricePoint + 5
                    : PricePoint;
            }
        }

        public Nullable<int> TechSpPoint { get; set; }
        public Nullable<int> TechSpPointNextYear {
            get {
                return TechSpPoint < 10
                    ? TechSpPoint + 5
                    : TechSpPoint;
            }
        }

        public Nullable<int> LogisticsPoint { get; set; }
        public Nullable<int> LogisticsPointNextYear {
            get {
                return LogisticsPoint < 20 && LogisticsPoint > 0
                    ? LogisticsPoint + 5
                    : LogisticsPoint == 0
                        ? LogisticsPoint + 10
                        : LogisticsPoint;
            }
        }

        public Nullable<int> QuantityDeliveryPoint { get; set; }
        public Nullable<int> QuantityDeliveryPointNextYear {
            get {
                return QuantityDeliveryPoint < 20 && QuantityDeliveryPoint > 0
                    ? QuantityDeliveryPoint + 5
                    : QuantityDeliveryPoint == 0
                        ? QuantityDeliveryPoint + 10
                        : QuantityDeliveryPoint;
            }
        }


        public Nullable<int> NGTimePoint { get; set; }
        public Nullable<int> NGTimePointNextYear {
            get {
                return NGTimePoint < 20 && NGTimePoint > 0
                    ? NGTimePoint + 5
                    : NGTimePoint == 0
                        ? NGTimePoint + 10
                        : NGTimePoint;
            }
        }


        public Nullable<int> NGNumberPoint { get; set; }
        public Nullable<int> NGNumberPointNextYear {
            get {
                return NGNumberPoint < 20 && NGNumberPoint > 0
                    ? NGNumberPoint + 5
                    : NGNumberPoint == 0
                        ? NGNumberPoint + 10
                        : NGNumberPoint;
            }
        }
        public Nullable<int> TotalPoint { get; set; }
        public Nullable<double> NGQuantity { get; set; }
        public Nullable<int> OrderQuantity { get; set; }

        public int FormId { get; set; }
        public int VendorId { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public string StatusStr { 
            get{
                return Status == 1
                    ? "(PENDING)"
                    : "(APPROVED)";
            }
        }

        public string Note { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        // Property hiển thị định dạng MMM/dd/yyyy
        public string FromDateFormatted {
            get { return FromDate.ToString("MMM dd, yyyy"); }
        }

        public string ToDateFormatted {
            get { return ToDate.ToString("MMM dd, yyyy"); }
        }
        public string ModifiedDateFormatted {
            get { return ModifiedDate.ToString("MMM dd, yyyy"); }
        }

        public string VendorName { get; set; }
        public string Grade { get; set; }
        public int MaterialClassifiedId { get; set; }

        public double TotalOrderQuantity { get; set; }
        public int TotalOrder { get; set; }


        public int LateTimes { get; set; }
        public int LessTimes { get; set; }
        public int NGTimes { get; set; }
        public int Year { get; set; }
        public int NextYear { get; set; }
        public WorkGroupInfo Info { get; set; }

        public Nullable<double> PercentNGQuantity { get; set; }


        public Nullable<int> TotalPointNextYear {
            get {
                return LogisticsPointNextYear + 
                    NGNumberPointNextYear + 
                    NGTimePointNextYear + 
                    PricePointNextYear + 
                    QuantityDeliveryPointNextYear + 
                    TechSpPointNextYear;
            }
        }


        // Commitment
        //public int CommitmentPricePoint { get; set; }
        //public string CommitmentPricePointStr {
        //    get {
        //        return CommitmentTotalPoint == 0
        //            ? ""
        //            : CommitmentPricePoint.ToString();
        //    }
        //}

        //public int CommitmentTechSpPoint { get; set; }
        //public string CommitmentTechSpPointStr {
        //    get {
        //        return CommitmentTotalPoint == 0
        //            ? ""
        //            : CommitmentTechSpPoint.ToString();
        //    }
        //}

        //public int CommitmentLateTimes { get; set; }
        //public string CommitmentLateTimesStr {
        //    get {
        //        return CommitmentTotalPoint == 0
        //            ? ""
        //            : CommitmentLateTimes.ToString();
        //    }
        //}

        //public int CommitmentLessTimes { get; set; }
        //public string CommitmentLessTimesStr {
        //    get {
        //        return CommitmentTotalPoint == 0
        //            ? ""
        //            : CommitmentLessTimes.ToString();
        //    }
        //}

        //public int CommitmentNGTimes { get; set; }
        //public string CommitmentNGTimesStr {
        //    get {
        //        return CommitmentTotalPoint == 0
        //            ? ""
        //            : CommitmentNGTimes.ToString();
        //    }
        //}

        //public double CommitmentPercentNGQuanity { get; set; }
        //public string CommitmentPercentNGQuanityStr {
        //    get {
        //        return CommitmentTotalPoint == 0
        //            ? ""
        //            : CommitmentPercentNGQuanity.ToString() + "%";
        //    }
        //}

        //public int CommitmentTotalPoint { get; set; }
        //public string CommitmentTotalPointStr {
        //    get {
        //        return CommitmentTotalPoint == 0
        //            ? ""
        //            : CommitmentTotalPoint.ToString();
        //    }
        //}


        //// Objective
        //public int ObjectivePricePoint { get; set; }
        //public string ObjectivePricePointStr {
        //    get {
        //        return ObjectiveTotalPoint == 0
        //            ? ""
        //            : ObjectivePricePoint.ToString();
        //    }
        //}
        //public int ObjectiveTechSpPoint { get; set; }
        //public string ObjectiveTechSpPointStr {
        //    get {
        //        return ObjectiveTotalPoint == 0
        //            ? ""
        //            : ObjectiveTechSpPoint.ToString();
        //    }
        //}
        //public int ObjectiveLateTimes { get; set; }
        //public string ObjectiveLateTimesStr {
        //    get {
        //        return ObjectiveTotalPoint == 0
        //            ? ""
        //            : ObjectiveLateTimes.ToString();
        //    }
        //}
        //public int ObjectiveLessTimes { get; set; }
        //public string ObjectiveLessTimesStr {
        //    get {
        //        return ObjectiveTotalPoint == 0
        //            ? ""
        //            : ObjectiveLessTimes.ToString();
        //    }
        //}
        //public int ObjectiveNGTimes { get; set; }
        //public string ObjectiveNGTimesStr {
        //    get {
        //        return ObjectiveTotalPoint == 0
        //            ? ""
        //            : ObjectiveNGTimes.ToString();
        //    }
        //}
        //public double ObjectivePercentNGQuanity { get; set; }
        //public string ObjectivePercentNGQuanityStr {
        //    get {
        //        return ObjectiveTotalPoint == 0
        //            ? ""
        //            : ObjectivePercentNGQuanity.ToString() + "%";
        //    }
        //}
        //public int ObjectiveTotalPoint { get; set; }
        //public string ObjectiveTotalPointyStr {
        //    get {
        //        return ObjectiveTotalPoint == 0
        //            ? ""
        //            : ObjectiveTotalPoint.ToString();
        //    }
        //}



        // String
        public string EvaluationStr {
            get {
                return "SUPPLIER EVALUATION " + Year + " " + StatusStr;
            }
        }

        public string SupplierStr {
            get {
                return "Supplier: " + VendorName;
            }
        }

        public string DateStr {
            get {
                return "Evaluation Period: " + FromDateFormatted + " - " + ToDateFormatted;
            }
        }



        public string DeliveryPerformance {
            get {
                return "Delivery Performance (" + TotalOrder +" Deliveries)";
            }
        }

        public string DeliveryDemerit {
            get {
                return "Delivery Demerit (" + TotalOrder + " Deliveries)";
            }
        }


        public string CommitmentStr {
            get {
                return "TAGET SCORE " + Year + ":";
            }
        }
        public string OverrallPerformance{
            get {
                return "OVERALL PERFORMANCES " + Year + ":";
            }
        }
        public string SupplierClassification {
            get {
                return "SUPPLIER CLASSIFICATION " + Year + ":";
            }
        }

        public string ObjectiveNextYear {
            get {
                return "TAGET SCORE " + NextYear + ":";
            }
        }

        public string LateDeliveriesStr {
            get{
                return LogisticsPoint + " ("+ LateTimes + " Late Deliveries)";
            }
        }

        public string ShortDeliveriesStr {
            get {
                return QuantityDeliveryPoint + " (" + LessTimes + " Short Deliveries) ";
            }
        }

        public string NonconformitiesStr {
            get {
                return NGTimePoint + " (" + NGTimes + " Nonconformities) " ;
            }
        }


        public string ProductNonconformitiesStr{
            get {
                return NGNumberPoint + " ("  +PercentNGQuantity + "%)";
            }
        }

        //public string TechSpPointStr {
        //    get {
        //        return TechSpPoint + "";
        //    }
        //}

        //public string CompetitivityStr {
        //    get {
        //        return PricePoint + "";
        //    }
        //}

        public string TotalPointStr {
            get {
                return TotalPoint + "";
            }
        }


    }
}