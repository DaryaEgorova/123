using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Maganizer_Project.BLL.Infrastructure
{
    public class MaxTagsAmount: ValidationAttribute
    {
        private readonly int _maxTagAmount;

        public MaxTagsAmount(int maxTagAmount)
        {
            _maxTagAmount = maxTagAmount;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var input = value as string;
            if (input != null)
            {
                var inputSplit = input.Split(" ").Distinct();
                if (inputSplit.Count() > _maxTagAmount)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"You can add maximum {_maxTagAmount} tags for one post";
        }
    }

}
