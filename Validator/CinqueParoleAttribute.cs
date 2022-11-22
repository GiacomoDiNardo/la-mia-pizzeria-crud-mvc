using Microsoft.IdentityModel.Protocols;
using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Validator
{
    public class CinqueParoleAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string fieldValue = (string)value;
            int count = 0;

            if (fieldValue == null)
            {
                return new ValidationResult("Il campo deve contenere almeno cinque parole");
            } else if (count == 0)
            {
                string desc = fieldValue.Trim();

                for (int i = 0; i < desc.Length; i++)
                {
                    char c = Convert.ToChar(" ");

                    if (desc[i] == c)
                    {
                        count++;
                    }
                }

                if (count < 4)
                {
                    return new ValidationResult("Il campo deve contenere almeno cinque parole");
                }
            }

            return ValidationResult.Success;
        }
    }
}
