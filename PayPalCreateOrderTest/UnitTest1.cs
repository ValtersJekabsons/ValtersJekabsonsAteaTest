using Core.Models.Orders;
using Core.Models.PaymentGateways.PayPal;
using Core.Models.PaymentGateways.Swift;
using Core.Models.Pdf;
using OrderProcessing;
using System.IO;
using Xunit;

namespace PayPalCreateOrderTest
{
    public class UnitTest1
    {

        private IPayPalService _PayPalService;
        private ISwiftService _SwiftService;
        private IPdfRepository _PdfRepository;
        private IOrderProcessingService _orderProcessingService;
        public UnitTest1()
        {
            _PayPalService = TestRepo.CreatePayPalService();
            _SwiftService = TestRepo.CreateSwiftService();
            _PdfRepository = TestRepo.CreatePdfRepository();
            _orderProcessingService = TestRepo.CreateOrderProcessingService();
        }
        [Fact]
        public void PayPal()
        {
            var order = new Order(213434,43,0.99,"Test Description") ;
            _PayPalService.ProcessOrder(new CreateOrderRequest(order, Core.Enums.PaymentGateway.PayPal));
        }
        [Fact]
        public void Swift()
        {
            var order = new Order(213434,43,0.99,"Test Description") ;
            _SwiftService.ProcessOrder(new CreateOrderRequest(order, Core.Enums.PaymentGateway.Swift));
        }
        [Fact]
        public void FailPdf()
        {
            var res =_PdfRepository.CreateOrderReceiptPdf(new CreateOrderResponse 
            { 
                DeclineReason= "Test decline reason",
                Description="Test Description",
                OrderCreated=false,
                OrderNumber=44,
                PayableAmount=1.00,
                PaymentGateway=Core.Enums.PaymentGateway.PayPal,
                UserId=45234234
            });
            File.WriteAllBytes(@"C:\Users\gulbj\source\repos\ValtersJekabsonsAteaTest\PayPalCreateOrderTest\testpdf.pdf", res);
        }
        [Fact]
        public void SuccessPdf()
        {
            var res = _PdfRepository.CreateOrderReceiptPdf(new CreateOrderResponse
            {
                DeclineReason = "Test decline reason",
                Description = "Test Description",
                OrderCreated = true,
                OrderNumber = 44,
                PayableAmount = 1.00,
                PaymentGateway = Core.Enums.PaymentGateway.PayPal,
                UserId = 45234234
            });
            File.WriteAllBytes(@"C:\Users\gulbj\source\repos\ValtersJekabsonsAteaTest\PayPalCreateOrderTest\testpdf.pdf", res);
        }
        [Fact]
        public void OrderProcessingTest()
        {

            var order = new Order(213434, 43, 0.99, "Test Description");
            _orderProcessingService.CreateOrder(new CreateOrderRequest(order, Core.Enums.PaymentGateway.Swift));
        }

    }
}