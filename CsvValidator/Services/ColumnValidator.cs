using CsvValidator.Models;
using System;
using System.Linq.Expressions;

namespace CsvValidator.Services
{
    public class ColumnValidator<T> : Validator<T> where T : IValidatorModel
    {
        public ColumnValidator<T> FollowBy<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            this._columns.Add(memberExpression?.Member.Name);
            return this;
        }
    }
}
