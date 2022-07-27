namespace Sales.Application.Validators.CustomerValidators;

public class UpdateCustomerDtoValidator : AbstractValidator<UpdateCustomerDto>
{
    public UpdateCustomerDtoValidator()
    {
        RuleFor(x => x.AccountNumber)
            .NotEmpty()
            .WithMessage("{PropertyName} can not be empty")
            .NotNull()
            .WithMessage("{PropertyName} can not be null")
            .MaximumLength(10)
            .WithMessage("{PropertyName} can not exceed length of 10 characters");

        RuleFor(x => x.ModifiedDate)
            .NotEmpty()
            .WithMessage("{PropertyName} can not be empty")
            .NotNull()
            .WithMessage("{PropertyName} can not be null");

        RuleFor(x => x.StoreId)
            .NotEmpty()
            .WithMessage("{PropertyName} can not be empty")
            .NotNull()
            .WithMessage("{PropertyName} can not be null");

        RuleFor(x => x.TerritoryId)
            .NotEmpty()
            .WithMessage("{PropertyName} can not be empty")
            .NotNull()
            .WithMessage("{PropertyName} can not be null");

    }
}