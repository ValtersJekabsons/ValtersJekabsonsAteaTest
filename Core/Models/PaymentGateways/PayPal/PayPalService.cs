using Core.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.PaymentGateways.PayPal
{
    public interface IPayPalService
    {
        CreateOrderResponse ProcessOrder(CreateOrderRequest request);
    }

    public class PayPalService : IPayPalService
    {
        const bool approveOrder = true;
        public PayPalService()
        {

        }
        public CreateOrderResponse ProcessOrder(CreateOrderRequest request)
        {
            return new CreateOrderResponse(request, approveOrder, "");
        }
    }
}
