using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Orders
{
    public class Order
    {
        public int UserId { get; set; }
        public int OrderNumber { get; set; }
        public double PayableAmount { get; set; }
        public string Description { get; set; }
        public Order() { }
        public Order(int userId, int orderNumber, double payableAmount, string description)
        {
            UserId = userId;
            OrderNumber = orderNumber;
            PayableAmount = payableAmount;
            Description = description;
        }
        public Order(Order order) : this(order.UserId, order.OrderNumber, order.PayableAmount, order.Description) { }
    }
}
