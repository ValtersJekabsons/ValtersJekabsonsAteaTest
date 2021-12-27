using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Orders
{
    public class CreateOrderResponse : CreateOrderRequest
    {
        public bool OrderCreated { get; set; }
        public string DeclineReason { get; set; }
        public CreateOrderResponse(): base()
        {

        }
        public CreateOrderResponse(CreateOrderRequest createOrderRequest, bool orderCreated, string declineReason) : base(createOrderRequest)
        {
            OrderCreated = orderCreated;
            DeclineReason = declineReason;
        }
    }
}
