using Stripe;
using Stripe.Checkout;

namespace BlazzorEcommerce.Server.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly ICartService _cartService;
        private readonly IAuthService _authService;
        private readonly IOrderService _orderService;

        public PaymentService(ICartService cartService,IAuthService authService,IOrderService orderService) {

            StripeConfiguration.ApiKey = "sk_test_51K7MeKFtdIvgWQov1K21ZX6F2mWhP1sueCoqMsPv9anaU2SZ92kKFyhT3cP89gYOMcyZYx59wQEB3rHP5oXKyXHc007Hu4hmwy";
            _cartService = cartService;
            _authService = authService;
            _orderService = orderService;
        }
        public async Task<Session> CreateCheckoutSession()
        {
            var Products = (await _cartService.GetDbCartProducts()).Data;
            var lineItems = new List<SessionLineItemOptions>();
            Products.ForEach(product => lineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmountDecimal = product.Price * 100,
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = product.Title,
                        Images = new List<string> { product.ImageUrl }
                    }
                },
                Quantity=product.Quantity
            }));

            var options = new SessionCreateOptions
            {
                CustomerEmail = _authService.GetUserEmail(),
                PaymentMethodTypes = new List<string>
               {
                   "card"
               },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "https://localhost:7205/order-success",
                CancelUrl = "https://localhost:7205/cart"
            };
            var service = new SessionService();
            Session session = service.Create(options);
            return session;
        }
    }
}
