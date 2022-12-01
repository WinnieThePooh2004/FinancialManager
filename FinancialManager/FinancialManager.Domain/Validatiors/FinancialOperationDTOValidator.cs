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
        }
    }
}
