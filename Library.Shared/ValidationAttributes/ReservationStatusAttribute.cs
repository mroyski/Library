using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Library.Api.ValidationAttributes
{
    public class ReservationStatusAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (Constants.ReservationStatuses.Statuses.Contains(value))
                return ValidationResult.Success;

            return new ValidationResult($"{value} is not a valid status");
        }
    }
}
