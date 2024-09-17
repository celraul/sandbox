using FluentValidation;

namespace Cupcake.Application.UseCases.Adm.ProductUseCases.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(item => item.CreateProductModel.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(item => item.CreateProductModel.Type)
           .IsInEnum()
           .WithMessage("{PropertyName} is required.");

        RuleFor(item => item.CreateProductModel.CostPrice)
            .NotNull()
            .GreaterThan(0)
            .WithMessage("{PropertyName} is required.");

        RuleFor(item => item.CreateProductModel.Price)
            .NotNull()
            .GreaterThan(0)
            .WithMessage("{PropertyName} is required.");

        RuleFor(item => item.CreateProductModel.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(item => item.CreateProductModel.Pictures)
            .NotNull()
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(item => item.CreateProductModel.Pictures)
           .NotNull()
           .NotEmpty()
           .WithMessage("{PropertyName} is required.");
    }
}
