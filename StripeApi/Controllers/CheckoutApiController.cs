using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using StripeApi.Models;

namespace StripeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckoutApiController : ControllerBase
    {
        private readonly StripeOptions _stripeOptions;

        public CheckoutApiController(StripeOptions stripeOptions)
        {
            _stripeOptions = stripeOptions;
        }

        [HttpPost("create")]
        public ActionResult Create()
        {
            var domain = _stripeOptions.Domain;
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                        Price = "price_1Nc6p5DfJxDQAwq1weZX3atY",
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = domain + "/success.html",
                CancelUrl = domain + "/cancel.html",
            };
            var service = new SessionService();
            Session session = service.Create(options);

            return Redirect(session.Url);
        }
    }
}
