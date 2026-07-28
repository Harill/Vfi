using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vfi.Ui.Mvc.Vfi.Areas.Purchasing.Models
{
    public class PurchaseOrderModel
    {
        public long PurchaseOrderId { get; set; }

        // Vendor
        public int VendorId { get; set; }

        [DisplayName("Tên nhà cung cấp")]
        public virtual string VendorName { get; set; }
        [DisplayName("Mã nhà cung cấp")]
        public virtual string VendorCode { get; set; }
        [DisplayName("Nhà cung cấp")]
        [UIHint("_VendorEditTemplate")]
        public virtual string VendorCodeName { get { return VendorCode + " -- " + VendorName; } set { VendorCode = value; } }
        // material classtified

        public virtual int MaterialClasstifiedId { get; set; }
        [DisplayName("Loại hình cung cấp")]
        public virtual string MaterialClasstifiedName { get; set; }
        // vendor

        public virtual int ShipMethodId { get; set; }
        [DisplayName("Phương thức vận chuyển")]
        [UIHint("_DeliveryByTemplate")]
        public virtual string ShipMethodName { get; set; }
        /// <summary>
        /// ShipMethodId
        /// </summary>
        public virtual int Method3Id { get { return ShipMethodId; } set { ShipMethodId = value; } }
        /// <summary>
        /// ShipMethodName
        /// </summary>
        [DisplayName("Phương thức vận chuyển")]
        public virtual string Method3Name { get { return ShipMethodName; } set { ShipMethodName = value; } }

        public virtual int DeliveryMethodId { get; set; }
        [DisplayName("Địa điểm giao hàng")]
        [UIHint("_ConditionDeliveryTemplate")]
        public virtual string DeliveryMethodName { get; set; }
        /// <summary>
        /// DeliveryMethodId
        /// </summary>
        public virtual int MethodId { get { return DeliveryMethodId; } set { DeliveryMethodId = value; } }
        /// <summary>
        /// DeliveryMethodName
        /// </summary>
        [DisplayName("Địa điểm giao hàng")]
        public virtual string MethodName { get { return DeliveryMethodName; } set { DeliveryMethodName = value; } }

        public virtual int PackagedMethodId { get; set; }
        [DisplayName("Phương thức đóng gói")]
        public virtual string PackagedMethodName { get; set; }
        /// <summary>
        /// PackagedMethodId
        /// </summary>
        public virtual int Method1Id { get { return PackagedMethodId; } set { PackagedMethodId = value; } }
        /// <summary>
        /// PackagedMethodName
        /// </summary>
        [DisplayName("Phương thức đóng gói")]
        public virtual string Method1Name { get { return PackagedMethodName; } set { PackagedMethodName = value; } }

        public virtual int PaymentMethodId { get; set; }
        [DisplayName("Phương thức thanh toán")]
        [UIHint("_PaymentTemplate")]
        public virtual string PaymentMethodName { get; set; }
        /// <summary>
        /// PaymentMethodId
        /// </summary>
        public virtual int Method2Id { get { return PaymentMethodId; } set { PaymentMethodId = value; } }
        /// <summary>
        /// PaymentMethodName
        /// </summary>
        [DisplayName("Phương thức thanh toán")]
        public virtual string Method2Name { get { return PaymentMethodName; } set { PaymentMethodName = value; } }

        public virtual int EmployeeId { get; set; }
        [DisplayName("Nhân viên")]
        public virtual string EmployeeName { get; set; }

        [DisplayName("Số đơn")]
        public virtual string RevisionNumber { get; set; }

        [DisplayName("Trạng thái")]
        public virtual byte Status { get; set; }
        [DisplayName("Trạng thái")]
        [UIHint("_StatusEditTemplate")]
        public virtual string StatusName { get; set; }

        [DisplayName("Ngày đặt lệnh")]
        [DataType("DateNonNullable")]
        public virtual DateTime OrderDate { get; set; }

        [DisplayName("Ngày giao")]
        [DataType(DataType.Date)]
        [UIHint("_DateTemplate")]
        public virtual DateTime? ShipDate { get; set; }

        [DisplayName("Dung sai")]
        public virtual int Tolerance { get; set; }
        //[UIHint("_PurchaseOrderDetailEditTemplate")]
        //public virtual string GridDetail { get; set; }

        [DisplayName("Kích hoạt")]
        public bool Active { get; set; }
        [DisplayName("Người lập")]
        public string ModifiedUser { get; set; }
        [DisplayName("Ngày lập")]
        public DateTime? ModifiedDate { get; set; }
        public string Note { get; set; }
        
        [DisplayName("Tổng số lượng")]
        public double? TotalQuality { get; set; }
        [DisplayName("Tiền tệ")]

        [DataType("_CurrencyEditTemplate")]
        public string CurrencyCode { get; set; }
        [DisplayName("Số hợp đồng")]
        public string ContractNumber { get; set; }

        public int AddressId { get; set; }
        [UIHint("_DeliveryAddressTemplate")]
        public string AddressName { get; set; }
        public string Address { get; set; }
        public string AddressFullName { get; set; }

        public bool Approve { get; set; }

        public int BillToId { get; set; }
        [UIHint("_BillToAddressTemplate")]
        public string BillTo { get; set; }
        public string BillToAddress { get; set; }
        public string BillToFullName { get; set; }
        public string ReceiptBill { get; set; }
        public string ReceiptBillPhoneNumber { get; set; }


        public string BillOfLanding { get; set; }

    }
}