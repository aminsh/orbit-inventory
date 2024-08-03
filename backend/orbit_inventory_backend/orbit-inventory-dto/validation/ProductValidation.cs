using FluentValidation;

namespace orbit_inventory_dto.validation;

public class ProductValidation: AbstractValidator<ProductDto>
{
    public ProductValidation()
    {
        RuleFor(p => p.Upc)
            .NotEmpty().WithMessage(Resources.Required);
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage(Resources.Required);
    }
}