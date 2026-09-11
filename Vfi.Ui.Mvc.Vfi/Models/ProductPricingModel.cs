using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vfi.Ui.Mvc.Vfi.Utilities;

namespace Vfi.Ui.Mvc.Vfi.Models {
    public class ProductPricingModel {
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; }

        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDesignNo { get; set; }
        public string ProductCurrency { get; set; }

        public double OutDiameterDesign { get; set; }
        public double Length { get; set; }
        public double LengthOrigin { get; set; }
        public double MaterialWeight { get; set; }
        public double ProductWeight { get; set; }
        public double Diameter { get; set; }

        public int MaterialId { get; set; }
        public string MaterialCode { get; set; }
        public double MaterialPrice { get; set; }
        public double ProductMaterialPrice {
            get {
                return MaterialWeight * MaterialPrice / 1000;
            }
        }


        public double ProductPrice { get; set; }

        public double DiffPrice {
            get {
                return ProductPrice - ProductMaterialPrice;
            }
        }

        public double DiffPriceRate {
            get {
                return ProductPrice > 0 ? ProductMaterialPrice / ProductPrice * 100 : 0;
            }
        }

        public double WorkpiecePrice {
            get {
                return (MaterialWeight - ProductWeight) * 80;
            }
        }

        public double WorkpiecePriceRate {
            get {
                return ProductPrice > 0 ? WorkpiecePrice / ProductPrice * 100 : 0;
            }
        }

    }
}