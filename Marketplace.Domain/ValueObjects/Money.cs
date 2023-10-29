using System;
using Marketplace.Domain.Interfaces;
using Marketplace.Framework;

namespace Marketplace.Domain.ValueObjects
{
	public class Money: Value<Money>
	{
		private const string DefaultCurrency = "EUR";

		public decimal Amount { get; }
		public Currency Currency { get; }

		public static Money FromDecimal(decimal amount, string currencyCode, ICurrencyLookup currencyLookup) =>
			new Money(amount, currencyCode, currencyLookup);

		public static Money FromString(string amount, string currencyCode, ICurrencyLookup currencyLookup) =>
			new Money(decimal.Parse(amount), currencyCode, currencyLookup);

		protected Money(decimal amount, string currencyCode, ICurrencyLookup currencyLookup)
		{
			if (string.IsNullOrEmpty(currencyCode))
				throw new ArgumentNullException(nameof(currencyCode), "Currency code must be specified");

			var currency = currencyLookup.FindCurrency(currencyCode);

			if (!currency.InUse)
				throw new ArgumentException(nameof(currencyCode), $"Currency {currencyCode} is not valid");

			if(decimal.Round(amount, currency.DecimalPlaces) != amount) {
				throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot have more than two decimals");
			}
			Amount = amount;
			Currency = currency;
		}

        protected Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }


        public Money Add(Money summand)
		{
			if (Currency != summand.Currency)
				throw new CurrencyMismatchException("Cannot sum amounts with different currencies");

			return new Money(Amount + summand.Amount, Currency);
        }

		public Money Subtract(Money subtrahend)
		{
            if (Currency != subtrahend.Currency)
                throw new CurrencyMismatchException("Cannot subtract amounts with different currencies");

            return new Money(Amount - subtrahend.Amount, Currency);
		}

		public static Money operator +(Money summand1, Money summand2) =>
			summand1.Add(summand2);

		public static Money operator -(Money minuend, Money subtrahend) =>
			minuend.Subtract(subtrahend);

		public override string ToString() => $"{Currency.CurrencyCode} {Amount}";
        
    }


	public class CurrencyMismatchException: Exception
	{
		public CurrencyMismatchException(string message) : base(message)
		{
		}
	}

}

