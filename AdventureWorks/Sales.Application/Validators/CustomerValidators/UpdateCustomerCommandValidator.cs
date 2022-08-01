namespace Sales.Application.Validators.CustomerValidators;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        //RuleFor(x => x.Id)
        //    .NotEmpty()
        //    .WithMessage("Id can not be empty")
        //    .NotNull()
        //    .WithMessage("Id can not be null")
        //    .GreaterThan(0)
        //    .WithMessage("Id should be greater than 1");

        //RuleFor(x => x.Customer.AccountNumber)
        //    .NotEmpty()
        //    .WithMessage("{PropertyName} can not be empty")
        //    .NotNull()
        //    .WithMessage("{PropertyName} can not be null")
        //    .MaximumLength(10)
        //    .WithMessage("{PropertyName} can not exceed length of 10 characters");

        //RuleFor(x => x.Customer.ModifiedDate)
        //    .NotEmpty()
        //    .WithMessage("{PropertyName} can not be empty")
        //    .NotNull()
        //    .WithMessage("{PropertyName} can not be null");

        //RuleFor(x => x.Customer.StoreId)
        //    .NotEmpty()
        //    .WithMessage("{PropertyName} can not be empty")
        //    .NotNull()
        //    .WithMessage("{PropertyName} can not be null");

        //RuleFor(x => x.Customer.TerritoryId)
        //    .NotEmpty()
        //    .WithMessage("{PropertyName} can not be empty")
        //    .NotNull()
        //    .WithMessage("{PropertyName} can not be null");
    }
}