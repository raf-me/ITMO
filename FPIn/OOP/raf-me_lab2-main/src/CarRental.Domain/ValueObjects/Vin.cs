namespace CarRental.Domain.ValueObjects;

public sealed class Vin : IEquatable<Vin>
{
    public string Value { get; }

    private Vin(string value) => Value = value;

    public static Vin Create(string value) => throw new NotImplementedException();

    public bool Equals(Vin? other) => throw new NotImplementedException();

    public override bool Equals(object? obj) => throw new NotImplementedException();

    public override int GetHashCode() => throw new NotImplementedException();

    public override string ToString() => throw new NotImplementedException();
}
