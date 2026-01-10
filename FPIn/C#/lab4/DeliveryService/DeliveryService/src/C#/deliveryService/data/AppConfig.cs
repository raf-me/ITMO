namespace DeliveryService.C_.deliveryService.data;

public class AppConfig
{
    private static AppConfig instance;

    public decimal DeliveryPrice;
    public decimal FastDeliveryExtra;
    public decimal Tax;
    public decimal Discount;

    private AppConfig()
    {
        DeliveryPrice = 50;
        FastDeliveryExtra = 150;
        Tax = 0.2m;   
        Discount = 0.1m;
    }

    public static AppConfig GetInstance()
    {
        if (instance == null)
            instance = new AppConfig();

        return instance;
    }
}