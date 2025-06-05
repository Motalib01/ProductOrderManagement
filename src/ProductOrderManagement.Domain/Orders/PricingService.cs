using ProductOrderManagement.Domain.Abstractions;
using ProductOrderManagement.Domain.Shared;

namespace ProductOrderManagement.Domain.Order;

public class PricingService
{
    public Result<Money> CalculateTotal(IEnumerable<OrderItem> items)
    {
        if (items == null || !items.Any())
            return Result.Failure<Money>(new Error("Pricing.EmptyItems", "No items to price."));

        var firstCurrency = items.First().Price.Currency;

        if (items.Any(i => i.Price.Currency != firstCurrency))
            return Result.Failure<Money>(
                new Error("Pricing.CurrencyMismatch", "All items must have the same currency."));

        var totalAmount = items.Sum(i => i.Price.Amount * i.Quantity);

        return Result.Success(new Money(totalAmount, firstCurrency));
    }

    
}