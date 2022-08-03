using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeRazor.Models;
using FluentValidation.Results;
namespace RecipeRazor.Pages;

public class RecipesPageModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;
    public RecipesPageModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

    // Allows us to create recipes from our custom recipe view.
    [BindProperty]
    public Recipe recipe { get; set; }
    public List<string> categ { get; set; } = new List<string>();
    public List<string> alerts = new();
    public async Task OnGet(List<string> messages)
    {
        var client = _httpClientFactory.CreateClient("Recipes");
        categ = await client.GetFromJsonAsync<List<string>>("categories");
        alerts = messages;
    }
    public async Task<IActionResult> OnPost(List<string> data)
    {
        var client = _httpClientFactory.CreateClient("Recipes");
        WebApplication3.Models.RecipesValidator validator = new();
        recipe.Categories = data;
        ValidationResult results = validator.Validate(recipe);
        if (results.IsValid)
        {
            recipe.Ingredients = recipe.Ingredients[0].Split("\r\n").ToList();
            recipe.Instructions = recipe.Instructions[0].Split("\r\n").ToList();
            var response = await client.PostAsJsonAsync("recipes/add-recipe", recipe);
            if (!response.IsSuccessStatusCode)
            {
                alerts.Add("error");
            }
            else
            {
                alerts.Add("no error");
            }
        }
        else
        {
            alerts.Add("error");
        }
        return RedirectToPage("./RecipesPage", new { messages = alerts });
    }
}
