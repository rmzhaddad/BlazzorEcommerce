using Stripe.Checkout;

namespace BlazzorEcommerce.Server.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<Session> CreateCheckoutSession();
    }
}
