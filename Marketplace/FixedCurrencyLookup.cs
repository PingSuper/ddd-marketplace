using System;
using Marketplace.Domain;
using Marketplace.Domain.Interfaces;

namespace Marketplace
{
	public class FixedCurrencyLookup: ICurrencyLookup
    {
        private static readonly IEnumerable<Currency> _currencies =
            new[]
            {
                new Currency{
                    CurrencyCode = "EUR",
                    DecimalPlaces = 2,
                    InUse = true
                },
                new Currency{
                    CurrencyCode = "USD",
                    DecimalPlaces = 2,
                    InUse = true
                },
                new Currency{
                    CurrencyCode = "JPY",
                    DecimalPlaces = 0,
                    InUse = true
                },
                new Currency{
                    CurrencyCode = "DEM",
                    DecimalPlaces = 2,
                    InUse = false
                },
            };

        public FixedCurrencyLookup()
		{
		}

        public Currency FindCurrency(string currencyCode)
        {
            var currency = _currencies.FirstOrDefault(x => x.CurrencyCode == currencyCode);
            return currency ?? Currency.None;
        }
    }
}

