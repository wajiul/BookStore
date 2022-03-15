using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Validaton_in_MVC {
    public class MyCustomValidation : ValidationAttribute {
        public MyCustomValidation(string _CustomField) {
            CustomField = _CustomField;
        }
        public string CustomField { get; set; }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
            if (value != null) {
                string str = value.ToString();
                if (str.Contains(CustomField)) {
                    return ValidationResult.Success;
                }
            }
            ErrorMessage = ErrorMessage ?? $"Field must contain {CustomField}";
            // if error message is null i.e not passed from model this msg shows
            return new ValidationResult(ErrorMessage);
        }
    }
}

