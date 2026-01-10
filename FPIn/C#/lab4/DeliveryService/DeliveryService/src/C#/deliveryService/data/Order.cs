namespace DeliveryService.C_.deliveryService.data;

public class Order
{
    public int OrderID;
    public List<string> Dishes;
    public decimal BasePrice;
    public decimal TotalPrice;
    public bool IsFast;
    public bool UseDiscount;
    public string Address;
    public State State;

    public Order()
    {
        Dishes = new List<string>();
        State = new PreparingState();
        BasePrice = 0;
        TotalPrice = 0;
    }
}