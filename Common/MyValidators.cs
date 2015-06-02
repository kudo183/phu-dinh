using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common
{
    public static class MyValidators
    {
        public static List<ValidationResult> Validate_RequiredField(object obj, string propertyName, string errMsg = "Khong duoc de trong")
        {
            var result = new List<ValidationResult>();

            var value = obj.GetType().GetProperty(propertyName).GetValue(obj);
            if (value == null)
            {
                result.Add(new ValidationResult(errMsg, new[] { propertyName }));
            }
            return result;
        }

        public static List<ValidationResult> Validate_RequiredText(object obj, string propertyName, string errMsg = "Khong duoc de trong")
        {
            var result = new List<ValidationResult>();

            var value = obj.GetType().GetProperty(propertyName).GetValue(obj) as string;
            if (string.IsNullOrEmpty(value))
            {
                result.Add(new ValidationResult(errMsg, new[] { propertyName }));
            }

            return result;
        }

        public static List<ValidationResult> Validate_NumberRange(object obj, string propertyName, double lower, double upper, string errMsg = "Gia tri khong nam trong khoang")
        {
            var result = new List<ValidationResult>();

            var value = obj.GetType().GetProperty(propertyName).GetValue(obj);
            double d;
            var isValid = true;
            if (double.TryParse(value.ToString(), out d))
            {
                if (d < lower || d > upper)
                {
                    isValid = false;
                }
            }
            else
            {
                isValid = false;
            }

            if (isValid == false)
            {
                result.Add(new ValidationResult(errMsg, new[] { propertyName }));
            }
            return result;
        }
    }
}
