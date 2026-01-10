namespace DeliveryService.C_.deliveryService.data;

public class FastOrderFactory : OrderFactory
{
    public override Order CreateOrder()
    {
        Order order = new Order();
        order.TotalPrice += 300;
        return order;
    }
}