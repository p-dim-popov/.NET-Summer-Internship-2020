using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MarketStore
{
    static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DateTime lastMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1);

                Person firstPerson = new Person("FirstName1", "LastName1", "0123456789");
                Person secondPerson = new Person("FirstName2", "LastName2", "1123456789");
                Person thirdPerson = new Person("FirstName3", "LastName3", "2123456789");

                DiscountCard firstPersonDiscountCard = new BronzeDiscountCard(firstPerson);
                DiscountCard secondPersonDiscountCard = new SilverDiscountCard(secondPerson);
                DiscountCard thirdPersonDiscountCard = new GoldDiscountCard(thirdPerson);

                firstPersonDiscountCard.CreatePurchase(150);
                if (firstPersonDiscountCard.LoadLastPurchase())
                {
                    Console.WriteLine($"Purchase value: ${firstPersonDiscountCard.LastPrice():f2}");
                    Console.WriteLine($"Discount rate: {firstPersonDiscountCard.LastDiscountRatePercent():f1}%");
                    Console.WriteLine($"Discount: ${firstPersonDiscountCard.LastDiscount():f2}");
                    Console.WriteLine($"Total: ${firstPersonDiscountCard.LastFinalPrice():f2}");
                    Console.WriteLine();
                }

                // add $600 purchase to aquire bigger discount rate
                secondPersonDiscountCard.AddPurchase(new Purchase(secondPerson, 600, 0, lastMonth));
                Purchase secondPersonPurchase = secondPersonDiscountCard
                    .CreatePurchase(850);
                secondPersonPurchase.PrintPurchase();
                Console.WriteLine();

                // add $1500 purchase to aquire bigger discount rate
                thirdPersonDiscountCard.AddPurchase(new Purchase(thirdPerson, 1500, 0, lastMonth));
                Purchase thirdPersonPurchase = thirdPersonDiscountCard
                    .CreatePurchase(1300);
                thirdPersonPurchase.PrintPurchase();
                Console.WriteLine();

                // person with gold discount card and purchase of 1500 but more than a month ago => no additional discounts
                Person fourthPerson = new Person("FirstName4", "LastName4", "3123456789");
                DiscountCard fourthPersonDiscountCard = new GoldDiscountCard(fourthPerson);
                fourthPersonDiscountCard.AddPurchase(new Purchase(fourthPerson, 1500, 0, new DateTime(1990, 01, 01)));
                fourthPersonDiscountCard
                    .CreatePurchase(50)
                    .PrintPurchase(shouldPrintPersonDetails: true);
                Console.WriteLine();

                ExampleExceptionHandling();
                Console.WriteLine("Examples completed successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void ExampleExceptionHandling(bool shouldPersonBeNull = true)
        {
            if (shouldPersonBeNull)
            {
                try
                {
                    Person nullPerson = null;
                    DiscountCard discountCard = new GoldDiscountCard(nullPerson);
                    discountCard.CreatePurchase(-333);
                    if (discountCard.LoadLastPurchase())
                    {
                        Console.WriteLine($"{nullPerson.FirstName} {nullPerson.LastName} made a purchase of {discountCard.LastFinalPrice():f2} value!");
                    }
                }
                catch (ArgumentNullException exception)
                {
                    // Example with nested exception handling
                    ExampleExceptionHandling(shouldPersonBeNull: false);

                    // Inner exception is handled and logged before this
                    Debug.WriteLine(exception.Message);

                    // delegating and masking exception trace
                    throw new Exception("Error happened. See logs!");
                }
            }
            else
            {
                try
                {
                    Person nullPerson = new Person("Null", "Person", "1234567890");
                    DiscountCard discountCard = new GoldDiscountCard(nullPerson);
                    discountCard.CreatePurchase(-333);
                    if (discountCard.LoadLastPurchase())
                    {
                        Console.WriteLine($"{nullPerson.FirstName} {nullPerson.LastName} made a purchase of {discountCard.LastFinalPrice():f2} value!");
                    }
                }
                catch (ArgumentException exception)
                {
                    Debug.WriteLine(exception.Message);
                }
            }
        }

        static void AddPurchase(this DiscountCard discountCard, Purchase purchase)
        {
            var personPurchases = typeof(DiscountCard)
                .GetField("_purchases", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.GetValue(discountCard) as List<Purchase>;
            personPurchases
                ?.Add(purchase);
        }

        static void PrintPurchase(this Purchase purchase, bool shouldPrintPersonDetails = false)
        {
            if (shouldPrintPersonDetails)
            {
                Console.WriteLine($"Person name: {purchase.Client.FirstName} {purchase.Client.LastName}");
            }
            Console.WriteLine($"Purchase value: ${purchase.Price:f2}");
            Console.WriteLine($"Discount rate: {purchase.DiscountRatePercent:f1}%");
            Console.WriteLine($"Discount: ${purchase.Discount:f2}");
            Console.WriteLine($"Total: ${purchase.FinalPrice:f2}");
        }
    }
}
