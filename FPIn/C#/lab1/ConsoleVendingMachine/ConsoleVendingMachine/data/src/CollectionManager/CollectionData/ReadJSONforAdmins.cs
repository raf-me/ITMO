namespace ConsoleVendingMachine.data.CollectionManager.CollectionData;
using System.Text.Json;

public class ReadJSONforAdmins
{
    public class adminRecord
    {
        public string Name { get; set; } = "";
        public int Key { get; set; }
    }
    
    private readonly string _fileName;

    public ReadJSONforAdmins(string fileName = "../../../data/recources/person.json")
    {
        _fileName = fileName;
    } 
    
    protected List<adminRecord>? LoadAdmins()
    {
        var json = File.ReadAllText(_fileName);
        var opts = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
        return JsonSerializer.Deserialize<List<adminRecord>>(json, opts) ?? new List<adminRecord>();;
    }
}