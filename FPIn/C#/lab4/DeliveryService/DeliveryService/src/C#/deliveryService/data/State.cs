namespace DeliveryService.C_.deliveryService.data;

public interface State
{
    string GetStatus();
}

public class PreparingState : State
{
    public string GetStatus() => "Готовится";
}

public class DeliveringState : State
{
    public string GetStatus() => "В пути";
}

public class CompletedState : State
{
    public string GetStatus() => "Выполнен";
}