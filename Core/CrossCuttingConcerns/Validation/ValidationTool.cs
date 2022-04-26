using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        public static bool Validate(IValidator ValidClass, object entitiy)
        {
            var context = new ValidationContext<object>(entitiy);
            
            var result = ValidClass.Validate(context);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            //


            return false;
        }
    }
}
