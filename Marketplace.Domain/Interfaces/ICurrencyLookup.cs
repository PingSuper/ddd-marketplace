using System;
namespace Marketplace.Domain.Interfaces
{
	public interface ICurrencyLookup
	{
		Currency FindCurrency(string currencyCode);
	}
}

