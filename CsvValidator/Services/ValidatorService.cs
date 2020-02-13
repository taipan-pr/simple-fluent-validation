using System.Collections.Generic;
using CsvValidator.Models;

namespace CsvValidator.Services
{
    public class ValidatorService<T> where T : IValidatorModel
    {
        public static Validator<T> WithFile(string filePath)
        {
            return new Validator<T>();
        }

        public static Validator<T> WithEnumerable(IEnumerable<T> data)
        {
            return new Validator<T>(data);
        }
    }
}
