using ConsoleVendingMachine.data.CollectionManager.CollectionData;

namespace ConsoleVendingMachine.data.CollectionManager.CollectionAdmin;
using System.Text.Json;

public class DataAdmin : ReadJSONforAdmins
{
    public List<adminRecord> Admins { get; private set; }
    
    public DataAdmin(string filename = "../../../data/recources/person.json") : base(filename)
    {
        Admins = LoadAdmins();
    }

    public void AddAdmin(string name, int key)
    {
        Admins.Add(new adminRecord() { Name = name, Key = key });
    }
}