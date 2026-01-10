namespace DeliveryService.C_.deliveryService.data;

public class OrderBuilder
{
    private Order order;

    public OrderBuilder(Order order)
    {
        this.order = order;
    }

    public void AddDish(string name, decimal price)
    {
        order.Dishes.Add($"{name} - {price} руб.");
        order.BasePrice += price;
    }
    
    public void SetAddress(string address)
    {
        order.Address = address;
    }

    public Order Build() { return order;}
}