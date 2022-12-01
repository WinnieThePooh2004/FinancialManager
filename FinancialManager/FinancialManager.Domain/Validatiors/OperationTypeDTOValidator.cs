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
    }
}
