using Xunit;
using Marketplace.Domain.Shared;

namespace Marketplace.Test;
public class MoneyTest
{
    private static readonly ICurrencyLookup _currencyLookup = new FakeCurrencyLookup();

    [Fact]
    public void Money_objects_with_same_amount_should_be_equal()
    {
        var firstAmt = Money.FromDecimal(5, "USD", _currencyLookup);
        var secAmt = Money.FromDecimal(5, "USD", _currencyLookup);

        Assert.Equal(firstAmt, secAmt);
    }

    [Fact]
    public void Money_add_operator()
    {
        var first = Money.FromDecimal(2, "USD", _currencyLookup);
        var sec = Money.FromDecimal(3, "USD", _currencyLookup);

        Assert.Equal(first + sec, Money.FromDecimal(5, "USD", _currencyLookup));
    }

    [Fact]
    public void Two_of_same_amount_but_differentCurrencies_should_not_be_equal()
    {
        var first = Money.FromDecimal(5, "USD", _currencyLookup);
        var sec = Money.FromDecimal(5, "JPY", _currencyLookup);

        Assert.NotEqual(first, sec);
    }

    [Fact]
    public void Unused_currency_should_not_be_allowed()
    {
        Assert.Throws<ArgumentException>(() => Money.FromDecimal(5, "DEM", _currencyLookup));
    }

}
