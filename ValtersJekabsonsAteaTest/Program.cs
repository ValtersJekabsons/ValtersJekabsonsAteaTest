using Core.Models.PaymentGateways.PayPal;
using Core.Models.PaymentGateways.Swift;
using Core.Models.Pdf;
using OrderProcessing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddTransient<IPdfRepository, PdfRepository>();
builder.Services.AddTransient<IPayPalService, PayPalService>();
builder.Services.AddTransient<ISwiftService, SwiftService>();
builder.Services.AddTransient<IOrderProcessingService, OrderProcessingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
