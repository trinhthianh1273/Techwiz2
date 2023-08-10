using Microsoft.EntityFrameworkCore.Infrastructure;
using SoccerManager.Models;
using System.Composition;
using System.Net;
using System.Security;

namespace SoccerManager.DTO.Response
{
    public class OrderRespone
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public int AddressId { get; set; }
        public DateTime OrderDate { get; set; }
        public int StatusId { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? PaymentMethodId { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expire { get; set; }
        public int? SecurityCode { get; set; }
        public int? PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public double? TotalAmount { get; set; }
        public int? TotalProduct { get; set; }

        public virtual Address Address { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<OrderContent> OrderContent { get; set; }
        public List<Products> ListProduct { get; set; }

        public OrderRespone()
        {
        }

        public OrderRespone(
            int orderId,
            int customerId,
            int employeeId,
            int addressId,
            DateTime orderDate,
            int statusId,
            DateTime? shippedDate,
            int? paymentMethodId,
            string cardName,
            string cardNumber,
            string expire,
            int? securityCode,
            int? paymentStatus,
            DateTime? paymentDate,
            double? totalAmount,
            int? totalProduct,
            Address address,
            Customer customer,
            Employee employee,
            PaymentMethod paymentMethod,
            Status status,
            List<Products> listProduct
        )
        {
            OrderId = orderId;
            CustomerId = customerId;
            EmployeeId = employeeId;
            AddressId = addressId;
            OrderDate = orderDate;
            StatusId = statusId;
            ShippedDate = shippedDate;
            PaymentMethodId = paymentMethodId;
            CardName = cardName;
            CardNumber = cardNumber;
            Expire = expire;
            SecurityCode = securityCode;
            PaymentStatus = paymentStatus;
            PaymentDate = paymentDate;
            TotalAmount = totalAmount;
            TotalProduct = totalProduct;
            Address = address;
            Customer = customer;
            Employee = employee;
            PaymentMethod = paymentMethod;
            Status = status;
            OrderContent = new HashSet<OrderContent>();
            ListProduct = listProduct;
        }

        public static OrderRespone ConvertToOrderResponse(Orders order)
        {
            double? totalPayment = order.OrderContent.Sum(oc => oc.Quantity * oc.Price);
            var TotalProduct = order.OrderContent.Sum(oc => oc.Quantity);

            var orderResponse = new OrderRespone {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                EmployeeId = order.EmployeeId,
                AddressId = order.AddressId,
                OrderDate = order.OrderDate,
                StatusId = order.StatusId,
                ShippedDate = order.ShippedDate,
                PaymentMethodId = order.PaymentMethodId,
                CardName = order.CardName,
                CardNumber = order.CardNumber,
                Expire = order.Expire,
                SecurityCode = order.SecurityCode,
                PaymentStatus = order.PaymentStatus,
                PaymentDate = order.PaymentDate,
                TotalAmount = order.OrderContent.Sum(oc => oc.Quantity * oc.Price),
                TotalProduct = order.OrderContent.Sum(oc => oc.Quantity),
                Address = order.Address,
                Customer = order.Customer,
                Employee = order.Employee,
                PaymentMethod = order.PaymentMethod,
                Status = order.Status,
                OrderContent = order.OrderContent,
                ListProduct = order.OrderContent.Select(oc => oc.Product).ToList()
        };
            return orderResponse;
        }
    }
}
