using CsvValidator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CsvValidator.Services
{
    public class Validator<T> where T : IValidatorModel
    {
        protected IList<string> _columns;
        private readonly IList<string> _errorMessage;
        private readonly IEnumerable<T> _data;
        public Validator() : this(null) { }

        public Validator(IEnumerable<T> data)
        {
            this._data = data;
            this._errorMessage = new List<string>();
            this._columns = new List<string>();
        }

        public Validator<T> ValidateMaxLength<TProperty>(Expression<Func<T, TProperty>> expression, int maxLength)
        {
            var memberExpression = expression.Body as MemberExpression;
            var propertyName = memberExpression?.Member.Name;
            if (string.IsNullOrEmpty(propertyName)) return this;

            foreach (var validatorModel in this._data)
            {
                var propertyInfo = validatorModel.GetType().GetProperty(propertyName);
                var value = Convert.ChangeType(propertyInfo?.GetValue(validatorModel), expression.Body.Type);
                if (value == null || value.ToString().Length > maxLength)
                {
                    this._errorMessage.Add($"{propertyName} - {value} is more than {maxLength}");
                }
            }
            return this;
        }

        public Validator<T> CustomValidator<TProperty>(Expression<Func<T, TProperty>> expression,
            Func<TProperty, bool> func)
        {
            var memberExpression = expression.Body as MemberExpression;
            var propertyName = memberExpression?.Member.Name;
            if (string.IsNullOrEmpty(propertyName)) return this;

            foreach (var validatorModel in this._data)
            {
                var propertyInfo = validatorModel.GetType().GetProperty(propertyName);
                var value = Convert.ChangeType(propertyInfo?.GetValue(validatorModel), expression.Body.Type);
                if (value == null || func((TProperty)value))
                {
                    this._errorMessage.Add($"{propertyName} - {value} is invalid by custom validator");
                }
            }
            return this;
        }

        public ColumnValidator<T> ColumnOrder<TProperty>(Expression<Func<T, TProperty>> expression, string columnName = null)
        {
            if (string.IsNullOrEmpty(columnName))
            {
                var memberExpression = expression.Body as MemberExpression;
                this._columns.Add(memberExpression?.Member.Name);
            }
            else
            {
                this._columns.Add(columnName);
            }

            return new ColumnValidator<T>();
        }

        public IList<string> Validate()
        {
            if (this._columns.Any())
            {

            }

            return this._errorMessage;
        }
    }
}
