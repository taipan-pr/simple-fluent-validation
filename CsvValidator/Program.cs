using CsvValidator.Models;
using CsvValidator.Services;
using System;

namespace CsvValidator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var data = new[]
            {
                new TypeA
                {
                    FirstName = "Taipan",
                    LastName = "Prasithpongchai",
                    Amount = 2
                },
                new TypeA
                {
                    FirstName = "BBB",
                    LastName = "CCC",
                    Amount = 7
                }
            };

            var validateResult = ValidatorService<TypeA>
                .WithEnumerable(data)
                .ColumnOrder(e => e.FirstName)
                .FollowBy(e => e.LastName)
                .ValidateMaxLength(e => e.FirstName, 5)
                .ValidateMaxLength(e => e.LastName, 8)
                .CustomValidator(e => e.FirstName, f => f.StartsWith("T"))
                .CustomValidator(e => e.Amount, f => f > 5)
                .Validate();

            Console.WriteLine(validateResult.ToString());
        }
    }
}
