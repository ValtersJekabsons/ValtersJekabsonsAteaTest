using Core.Models.Orders;
using System.Text;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace Core.Models.Pdf
{
    public interface IPdfRepository
    {
        byte[] CreateOrderReceiptPdf(CreateOrderResponse order);
    }

    public class PdfRepository : IPdfRepository
    {
        string htmlTemplateLocation;
        public PdfRepository(string htmlTemplateLocation = "../Core/Models/Templates/") {
            this.htmlTemplateLocation = htmlTemplateLocation;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
        public byte[] CreateOrderReceiptPdf(CreateOrderResponse order)
        {
            var estEncoding = Encoding.GetEncoding(1252);
            var receiptTemplate = File.ReadAllText(htmlTemplateLocation + "ReceiptTemplate.html", estEncoding);
            var descriptionTemplate = File.ReadAllText(htmlTemplateLocation + "DescriptionTemplate.html", estEncoding);
            var transactionFailedTemplate = File.ReadAllText(htmlTemplateLocation + "TransactionFailedTemplate.html", estEncoding);

            var transactionFailedReplacements = new Dictionary<string, string>(){
                {"{{description}}", order.DeclineReason}
            };
            var transactionFailed = order.OrderCreated ? "" : FillTemplate(transactionFailedTemplate, transactionFailedReplacements);

            var descriptionReplacements = new Dictionary<string, string>(){
                {"{{description}}", order.Description}
            };
            var description = order.Description == "" ? "" : FillTemplate(descriptionTemplate, descriptionReplacements);

            var receiptReplacements = new Dictionary<string, string>(){
                {"{{orderNumber}}", order.OrderNumber.ToString()},
                {"{{paymentGateway}}", order.PaymentGateway.ToString()},
                {"{{payableAmount}}", string.Format("{0:0.00}", order.PayableAmount)},
                {"{{optionalDescription}}", description },
                {"{{transactionFailedRow}}", transactionFailed },
            };

            var filledHtmlTemplate = FillTemplate(receiptTemplate, receiptReplacements);
            return CreatePdf(filledHtmlTemplate);
        }

        string FillTemplate(string template, Dictionary<string, string> replacements)
        {
            var filledTemplate = template;
            foreach (string key in replacements.Keys)
            {
                filledTemplate = filledTemplate.Replace(key, replacements[key]);
            }
            return filledTemplate;
        }

        byte[] CreatePdf(string html)
        {
            byte[] res = null;

            using (MemoryStream ms = new MemoryStream())
            {
                var pdf = PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
                pdf.Save(ms);
                res = ms.ToArray();
            }
            return res;
        }

    }
}
