using Stripe.Checkout;

namespace BlazzorEcommerce.Server.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<Session> CreateCheckoutSession();
        Task<ServiceResponse<bool>> FullfillOrder(HttpRequest request);

    }
}
