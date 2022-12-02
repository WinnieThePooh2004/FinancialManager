using FinancialManager.Shared.DTOs;
using FluentValidation;

namespace FinancialManager.Domain.Validatiors
{
    public class OperationTypeDTOValidator : AbstractValidator<OperationTypeDTO>
    {
        public OperationTypeDTOValidator()
        {
            RuleFor(o => o.Name)
                .NotEmpty()
                .WithMessage("Please, enter name");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<OperationTypeDTO>
                .CreateWithOptions((OperationTypeDTO)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
            {
                return Array.Empty<string>();
            }
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
