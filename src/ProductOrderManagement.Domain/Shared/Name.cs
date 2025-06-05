namespace ProductOrderManagement.Domain.Shared;

public sealed record Name
{
    public string Value { get; }

    public Name(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
            throw new ArgumentException("Name must be at least 3 characters long", nameof(value));

        Value = value;
    }

    public override string ToString() => Value;
}