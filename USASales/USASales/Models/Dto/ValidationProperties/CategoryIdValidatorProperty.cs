using System.ComponentModel.DataAnnotations;
using USASales.Repositories;

namespace USASales.Models.Dto.ValidationProperties
{
    public class CategoryIdValidatorProperty : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var categoriesRepository = (ICategoriesRepository)validationContext.GetService(typeof(ICategoriesRepository));
            if (categoriesRepository != null)
            {
                if (categoriesRepository.IsIdValid((int)value))
                    return ValidationResult.Success;

                return new ValidationResult("Wrong category id");
            }

            return base.IsValid(value, validationContext);
        }
    }
}
