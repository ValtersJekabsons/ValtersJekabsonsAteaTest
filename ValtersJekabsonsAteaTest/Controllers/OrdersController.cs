using Core.Models.Orders;
using Microsoft.AspNetCore.Mvc;
using OrderProcessing;

namespace ValtersJekabsonsAteaTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {

        private readonly ILogger<OrdersController> logger;
        private readonly IOrderProcessingService _orderProcessingService;

        public OrdersController(ILogger<OrdersController> logger, IOrderProcessingService orderProcessingService): base()
        {
            this.logger = logger;
            _orderProcessingService = orderProcessingService;
        }

        [HttpPost(Name = "CreateOrder")]
        public FileResult CreateOrder([FromBody] CreateOrderRequest createOrderRequest)
        {
            return File(_orderProcessingService.CreateOrder(createOrderRequest), "application/octet-stream", createOrderRequest.OrderNumber + ".pdf");
        }
    }
}
