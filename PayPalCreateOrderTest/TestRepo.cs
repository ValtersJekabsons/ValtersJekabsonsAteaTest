using Core.Models.PaymentGateways.PayPal;
using Core.Models.PaymentGateways.Swift;
using Core.Models.Pdf;
using OrderProcessing;

namespace PayPalCreateOrderTest
{
    public class TestRepo
    {
        public static IPayPalService CreatePayPalService()
        {
            return new PayPalService();
        }
        public static ISwiftService CreateSwiftService()
        {
            return new SwiftService();
        }
        public static IPdfRepository CreatePdfRepository()
        {
            return new PdfRepository("../../../../Core/Models/Templates/");
        }
        public static IOrderProcessingService CreateOrderProcessingService()
        {
            return new OrderProcessingService(CreatePdfRepository(), CreatePayPalService(), CreateSwiftService());
        }

    }
}
