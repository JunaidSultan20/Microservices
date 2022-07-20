using FluentValidation;

namespace Sales.Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerByIdCommandValidator : AbstractValidator<UpdateCustomerByIdCommand>
{
    public UpdateCustomerByIdCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithErrorCode("400")
            .WithMessage("Id can not be less than 1")
            .WithSeverity(Severity.Error);

        RuleFor(x => x.Customer.AccountNumber)
            .NotEmpty().WithMessage("AccountNumber field cannot be empty")
            .NotNull().WithMessage("AccountNumber field cannot be null")
            .MaximumLength(10).WithMessage("Maximum length for AccountNumber should be 10")
    }
}