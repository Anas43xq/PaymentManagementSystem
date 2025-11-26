using System;
using System.Collections.Generic;
using System.Linq;

namespace PaymentManagement
{
    public class CurrencyConverter
    {
        private static readonly Dictionary<string, decimal> ExchangeRates = new Dictionary<string, decimal>
        {
            { "USD", 1.0m },
            { "LBP", 89500m },
            { "AED", 3.67m }
        };

        /// <summary>
        /// Converts an amount from one currency to USD
        /// </summary>
        public static decimal ConvertToUSD(decimal amount, string fromCurrency)
        {
            if (string.IsNullOrWhiteSpace(fromCurrency))
                return amount;

            fromCurrency = fromCurrency.ToUpper();

            if (!ExchangeRates.ContainsKey(fromCurrency))
                return amount;

            if (fromCurrency == "USD")
                return amount;

            return amount / ExchangeRates[fromCurrency];
        }

        /// <summary>
        /// Converts an amount from USD to another currency
        /// </summary>
        public static decimal ConvertFromUSD(decimal amountInUSD, string toCurrency)
        {
            if (string.IsNullOrWhiteSpace(toCurrency))
                return amountInUSD;

            toCurrency = toCurrency.ToUpper();

            if (!ExchangeRates.ContainsKey(toCurrency))
                return amountInUSD;

            if (toCurrency == "USD")
                return amountInUSD;

            return amountInUSD * ExchangeRates[toCurrency];
        }

        /// <summary>
        /// Converts between any two currencies
        /// </summary>
        public static decimal Convert(decimal amount, string fromCurrency, string toCurrency)
        {
            decimal amountInUSD = ConvertToUSD(amount, fromCurrency);
            return ConvertFromUSD(amountInUSD, toCurrency);
        }

        /// <summary>
        /// Gets the exchange rate for a currency relative to USD
        /// </summary>
        public static decimal GetRate(string currency)
        {
            if (string.IsNullOrWhiteSpace(currency))
                return 1.0m;

            currency = currency.ToUpper();

            return ExchangeRates.ContainsKey(currency) ? ExchangeRates[currency] : 1.0m;
        }

        /// <summary>
        /// Updates exchange rate for a currency
        /// </summary>
        public static void UpdateRate(string currency, decimal rate)
        {
            if (string.IsNullOrWhiteSpace(currency) || rate <= 0)
                return;

            currency = currency.ToUpper();

            if (ExchangeRates.ContainsKey(currency))
                ExchangeRates[currency] = rate;
        }
    }
}
