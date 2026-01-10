namespace DeliveryService.C_.deliveryService.data;

public class StandardOrderFactory : OrderFactory
{
    public override Order CreateOrder()
    {
        return new Order();
    }
}