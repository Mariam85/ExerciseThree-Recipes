using FluentValidation;
namespace WebApplication3.Models
{
    public class RecipesValidator : AbstractValidator<Recipe>
    {
        public RecipesValidator()
        {
            RuleFor(recipe => recipe.Ingredients).NotNull().NotEmpty();
            RuleFor(recipe => recipe.Title).NotNull().NotEmpty();
            RuleFor(recipe => recipe.Instructions).NotNull().NotEmpty();
            RuleFor(recipe => recipe.Categories).NotNull().NotEmpty();
        }
    }
}

