namespace L04.Services;

public class PaymentService
{
    public bool Transact(Guid orderId, string cardNumber, decimal price)
    {
        // TODO: request to outer payment provider
        return FakeOuterPaymentProvider(cardNumber, price);
    }

    private static bool FakeOuterPaymentProvider(string cardNumber, decimal price)
    {
        var rand = Random.Shared.Next(0, 3);
        return rand != 0;
    }
}
