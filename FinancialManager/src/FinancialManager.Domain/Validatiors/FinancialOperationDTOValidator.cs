using FinancialManager.Shared.DTOs;
using FluentValidation;

namespace FinancialManager.Domain.Validatiors
{
    public class FinancialOperationDTOValidator : AbstractValidator<FinancialOperationDTO>
    {
        public FinancialOperationDTOValidator() 
        {
            RuleFor(f => f.Amount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("You can`t get or spend less than 0");

            RuleFor(f => f.OperationTypeId)
                .NotEqual(0)
                .WithMessage("Plese, select operation type");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<FinancialOperationDTO>
                .CreateWithOptions((FinancialOperationDTO)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
            {
                return Array.Empty<string>();
            }
            return result.Errors.Select(e => e.ErrorMessage);
        };

    }
}
