using Core.Models.Orders;
using Core.Models.PaymentGateways.PayPal;
using Core.Models.PaymentGateways.Swift;
using Core.Models.Pdf;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;
using static Core.Enums;

namespace OrderProcessing
{
    public interface IOrderProcessingService
    {
        byte[] CreateOrder(CreateOrderRequest createOrderRequest);
    }

    public class OrderProcessingService : IOrderProcessingService
    {
        private readonly IPdfRepository pdfRepository;
        private readonly IPayPalService payPalService;
        private readonly ISwiftService swiftService;
        public OrderProcessingService(IPdfRepository pdfRepository, IPayPalService payPalService, ISwiftService swiftService)
        {
            this.pdfRepository = pdfRepository;
            this.payPalService = payPalService;
            this.swiftService = swiftService;
        }

        public byte[] CreateOrder(CreateOrderRequest createOrderRequest)
        {

            var orderResponse = ProcessOrderByGateway(createOrderRequest);
            var orderPdf = pdfRepository.CreateOrderReceiptPdf(orderResponse);


            return orderPdf;

        }
        CreateOrderResponse ProcessOrderByGateway(CreateOrderRequest createOrderRequest)
        {
            switch (createOrderRequest.PaymentGateway)
            {
                case PaymentGateway.PayPal:
                    return payPalService.ProcessOrder(createOrderRequest);
                default:
                    return swiftService.ProcessOrder(createOrderRequest);
            }

        }
    }
}