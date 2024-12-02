using Stripe;
using StripeApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Stripe settings
builder.Services.Configure<StripeApi.Models.StripeOptions>(builder.Configuration.GetSection("Stripe"));

// Set the Stripe API key
var stripeOptions = new StripeOptions();
builder.Configuration.GetSection("Stripe").Bind(stripeOptions);
StripeConfiguration.ApiKey = stripeOptions.ApiKey;

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
