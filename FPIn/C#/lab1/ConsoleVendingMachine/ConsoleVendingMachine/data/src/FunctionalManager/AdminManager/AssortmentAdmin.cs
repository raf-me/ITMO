using ConsoleVendingMachine.data.CollectionManager.CollectionAdmin;
using ConsoleVendingMachine.data.CollectionManager.CollectionData;
using ConsoleVendingMachine.data.FunctionalManager.AssortmentManager;

namespace ConsoleVendingMachine.data.FunctionalManager.AdminManager;

public class AssortmentAdmin
{
    public void AddManager()
    {
        var dataCollection = new DataCollection();
        
        NameProduct nameProductCheck = new NameProduct(); 
        string name = nameProductCheck.Print();
        
        Price priceCheck = new Price();
        float price = priceCheck.priceWrite();

        Quantity quantityCheck = new Quantity();
        int quantity = quantityCheck.Check();
        
        dataCollection.AddProduct(name, price, quantity);
        dataCollection.SaveAssortment();
    }

    public void RemoveManager()
    {
        var dataCollection = new DataCollection();
        
        NameProduct nameProductCheck = new NameProduct(); 
        string name = nameProductCheck.Print();

        dataCollection.RemoveProduct(name);
        dataCollection.SaveAssortment();
    }
}