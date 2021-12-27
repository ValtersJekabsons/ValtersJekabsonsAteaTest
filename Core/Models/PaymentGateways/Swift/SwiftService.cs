using Core.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.PaymentGateways.Swift
{
    public interface ISwiftService
    {
        CreateOrderResponse ProcessOrder(CreateOrderRequest request);
    }

    public class SwiftService : ISwiftService
    {
        const bool approveOrder = true;
        public SwiftService()
        {

        }
        public CreateOrderResponse ProcessOrder(CreateOrderRequest request)
        {
            return new CreateOrderResponse(request, approveOrder, "");
        }
    }
}
