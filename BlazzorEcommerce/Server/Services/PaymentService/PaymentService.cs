﻿using Stripe;
using Stripe.Checkout;

namespace BlazzorEcommerce.Server.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly ICartService _cartService;
        private readonly IAuthService _authService;
        private readonly IOrderService _orderService;
        const string secret = "whsec_2a94cbfc89f25c586716746ae871dab8939d61ebc718ebcfcfd39e710e08d1a9";

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
                ShippingAddressCollection=new SessionShippingAddressCollectionOptions { AllowedCountries=new List<string> { "US"} },
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

        public async Task<ServiceResponse<bool>> FullfillOrder(HttpRequest request)
        {
            var json = await new StreamReader(request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    request.Headers["Stripe-Signature"],
                    secret);

                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Session;
                    var user = await _authService.GetUserByEmail(session.CustomerEmail);
                    await _orderService.PlaceOrder(user.Id);
                }
                return new ServiceResponse<bool> { Data = true };
            }
            catch (StripeException e)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = e.Message
                };
            }
        }
    }
}
