using System;
namespace Vfi.Ui.Mvc.Vfi.Areas.Purchasing.Models
{
    public class DeliveryAddressModel 
    {
        public int AddressId { get; set; }
        public string AddressName { get; set; }
        public string AddressShortName { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public bool Active { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
    }
}