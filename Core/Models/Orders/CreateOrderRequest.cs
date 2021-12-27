using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums;

namespace Core.Models.Orders
{
    public class CreateOrderRequest : Order
    {
        public PaymentGateway PaymentGateway { get; set; }
        public CreateOrderRequest(): base()
        {

        }
        public CreateOrderRequest(CreateOrderRequest createOrderRequest) : base(createOrderRequest.UserId, createOrderRequest.OrderNumber, createOrderRequest.PayableAmount, createOrderRequest.Description)
        {
            PaymentGateway = createOrderRequest.PaymentGateway;
        }
        public CreateOrderRequest(Order order, PaymentGateway paymentGateway) : base(order)
        {
            PaymentGateway = paymentGateway;
        }

    }
}
