using ConsoleVendingMachine.data.CollectionManager.CollectionAdmin;
using ConsoleVendingMachine.data.CollectionManager.CollectionData;
using ConsoleVendingMachine.data.FunctionalManager.AssortmentManager;

namespace ConsoleVendingMachine.data.FunctionalManager.AdminManager;

public class AssortmentAdmin
{
    private string name;
    private float price;
    private int quantity;
    public void AddManager()
    {
        var dataCollection = new DataCollection();
        
        NameProduct nameProductCheck = new NameProduct(); 
        nameProductCheck.Print(name);
        
        Price priceCheck = new Price();
        priceCheck.priceWrite(price);

        Quantity quantityCheck = new Quantity();
        quantityCheck.Check(quantity);
        
        dataCollection.AddProduct(name, price, quantity);
    }

    public void RemoveManager()
    {
        var dataCollection = new DataCollection();
        
        NameProduct nameProductCheck = new NameProduct(); 
        nameProductCheck.Print(name);
        
        Price priceCheck = new Price();
        priceCheck.priceWrite(price);

        Quantity quantityCheck = new Quantity();
        quantityCheck.Check(quantity);

        dataCollection.RemoveProduct(name);
    }
}