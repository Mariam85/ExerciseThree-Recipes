using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeRazor.Models;

namespace RecipeRazor.Pages
{
    public class RecipesModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public RecipesModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        // Allows us to create recipes from our custom recipe view.
        [BindProperty]
        public Recipe recipe { get; set; }
        
        public async Task<IActionResult> OnPost()
        {
            var client = _httpClientFactory.CreateClient("Recipes");
            var response = await client.PostAsJsonAsync("recipes/add-recipe", recipe);
            recipe.Ingredients = recipe.Ingredients[0].Split("\r\n").ToList();
            recipe.Categories = recipe.Categories[0].Split("\r\n").ToList();
            recipe.Instructions = recipe.Instructions[0].Split("\r\n").ToList();
            
            // Todo: check the response status.
            return Redirect("Successful");
        }
    }
}
