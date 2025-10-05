using ConsoleVendingMachine.data.FunctionalManager.AdminManager;

namespace ConsoleVendingMachine.data.CollectionManager.CollectionData;
using System.Text.Json;

public class ReadJSONforAssortments
{
    public class AssortmentCollection
    {
        public string Name { get; set; } = "";
        public float Price { get; set; }
        public int Quantity { get; set; }
    }

    private readonly string _fileName;

    public ReadJSONforAssortments(string fileName = "person.json")
    {
        _fileName = fileName;
    }

    protected List<AssortmentCollection>? LoadAssortment()
    {
        var json =  File.ReadAllText(_fileName);
        var opts = new JsonSerializerOptions();
        return JsonSerializer.Deserialize<List<AssortmentCollection>>(json, opts) ?? new List<AssortmentCollection>();;
    }
}