using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vfi.Ui.Mvc.Vfi.Models;

namespace Vfi.Ui.Mvc.Vfi.Areas.Factory.Models
{
    public class TrackUpMachineModel {
        public string Title { get; set; }
        public DateTime ReportDate { get; set; }
        public int TrackId { get; set; }
        [UIHint("_DateTemplateNullable")]
        public DateTime? StartDate { get; set; }
        public DateTime ForecastDate { get; set; }
        [DataType("NumberAsInt")]
        public int ForecastDay { get; set; }
        public double ForecastQuantity { get; set; }
        [UIHint("_DateTemplateNullable")]
        public DateTime? DeliveryDate { get; set; }
        public int MachineId { get; set; }
        [UIHint("_MachineEditTemplate")]
        public string MachineName { get; set; }
        public int ProductId { get; set; }
        [UIHint("_ProductCodeNameTemplate2")]
        public string ProductCode { get; set; }
        public string Phase { get; set; }
        [DataType("NumberAsInt")]
        public int RoundPerMinute { get; set; }
        [DataType("NumberAsInt")]
        public int Quantity { get; set; }
        [DataType("Number")]
        public double RealProductivity { get; set; }
        public double Productivity { get; set; }
        public int ProductivityColor { get; set; }
        [DataType("NumberAsInt")]
        public int RealRate { get; set; }
        public int ProductRate { get; set; }
        public int ProductRateColor { get; set; }
        [DataType("Number")]
        public double WorkPiece { get; set; }
        [DataType("Number")]
        public double KnifeCut { get; set; }
        public string DeliveryEmployee { get; set; }
        public string ReceiveEmployee { get; set; }
        public string Note { get; set; }
        public byte Status { get; set; }
        public string StatusName { get; set; }
        public int StatusColor { get; set; }
        public int PermisstionType { get; set; }

        public int? MaterialId { get; set; }
        [UIHint("_MaterialEditTemplate2")]
        public string MaterialCode { get; set; }
        public int MaterialInvId { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public double TotalInv { get; set; }
        public List<Process> Process { get; set; }
        public int ProcessDay { get; set; }
        public List<Process> Production { get; set; }
        public int ProductionDay { get; set; }
        public double ProductionQuantity { get; set; }
        public int MoreDay { get; set; }
        public double ProductLength { get; set; }
        public int MaxAssign { get; set; }

        public string ProductImg { get; set; }
        public string DrawingImg { get; set; }
        public string UploadDate { get; set; }
        public double ProductionWeight { get; set; }






        public List<TrackUpMaterial> TrackUpMaterials { get; set; }
    }

    public class Process
    {
        public Process(int day, int type)
        {
            Day = day;
            Quantity = 0;
            Type = type;
        }

        public int Day { get; set; }
        public int Type { get; set; }
        public double Quantity { get; set; }
        public string TypeSet
        {
            get
            {
                switch (Type)
                {
                        //blank
                    case 0:
                        return "progress-bar-blank";
                    //process
                    case 1:
                        return "progress-bar-plan";
                    //process
                    case 2:
                        return "progress-bar-prepare";
                    //process
                    case 3:
                        return "progress-bar-production";
                    //warning
                    case 4:
                        return "progress-bar-warning";
                    default:
                        return "";
                }
            }
        }
    }
}