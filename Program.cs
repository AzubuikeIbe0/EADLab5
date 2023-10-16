using System;

namespace MoneyStructure
{
    public struct Money
    {
        public double Amount { get; }
        public string Currency { get; }

        public Money(double amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public Money Convert(string newCurrency)
        {
            double newAmount;

            if (newCurrency == Currency)
            {
                return new Money(Amount, Currency);
            }

            switch (Currency.ToLower())
            {
                case "euro":
                    switch (newCurrency.ToLower())
                    {
                        case "dollar":
                            newAmount = Amount * EURUSD;
                            break;
                        case "yen":
                            newAmount = Amount * EURJPY;
                            break;
                        default:
                            throw new ArgumentException("Invalid Currency");
                    }
                    break;

                case "dollar":
                    switch (newCurrency.ToLower())
                    {
                        case "euro":
                            newAmount = Amount * USDEUR;
                            break;
                        case "yen":
                            newAmount = Amount * USDJPY;
                            break;
                        default:
                            throw new ArgumentException("Invalid Currency");
                    }
                    break;

                case "yen":
                    switch (newCurrency.ToLower())
                    {
                        case "euro":
                            newAmount = Amount * JPYEUR;
                            break;
                        case "dollar":
                            newAmount = Amount * JPYUSD;
                            break;
                        default:
                            throw new ArgumentException("Invalid Currency");
                    }
                    break;

                default:
                    throw new ArgumentException("Invalid currency");
            }

            return new Money(newAmount, newCurrency);
        }

        public static Money Add(Money m1, Money m2)
        {
            if (m1.Currency != m2.Currency)
            {
                m2 = m2.Convert(m1.Currency);
            }

            double totalAmount = m1.Amount + m2.Amount;
            return new Money(totalAmount, m1.Currency);
        }

        public const double EURUSD = 1.16;
        public const double EURJPY = 129.76;
        public const double USDEUR = 0.86;
        public const double USDJPY = 111.83;
        public const double JPYEUR = 0.008;
        public const double JPYUSD = 0.009;
    }

    public static class TestProgram
    {
        static void Main(string[] args)
        {
            Money money1 = new Money(100, "euro");
            Money money2 = new Money(50, "dollar");

            Money sum = Money.Add(money1, money2);
            Console.WriteLine($"{money1.Amount} {money1.Currency} + {money2.Amount} {money2.Currency} = {sum.Amount} {sum.Currency}");
        }
    }
}
