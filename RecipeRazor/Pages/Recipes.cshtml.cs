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
            // Todo: check the response status.
            return Redirect("Successful");
        }
    }
}
