using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeRazor.Models;

namespace RecipeRazor.Pages
{
    public class RecipesPageModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public RecipesPageModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        // Allows us to create recipes from our custom recipe view.
        [BindProperty]
        public Recipe recipe { get; set; }
        public List<string> categ { get; set; } = new List<string>();

        public async Task OnGet()
        {
            var client = _httpClientFactory.CreateClient("Recipes");
            categ = await client.GetFromJsonAsync<List<string>>("categories");
        }
        public async Task<IActionResult> OnPost(List<string> data)
        {
            var client = _httpClientFactory.CreateClient("Recipes");
            recipe.Ingredients = recipe.Ingredients[0].Split("\r\n").ToList();
            recipe.Instructions = recipe.Instructions[0].Split("\r\n").ToList();
            recipe.Categories = data;
            var response = await client.PostAsJsonAsync("recipes/add-recipe", recipe);
            // Todo: check the response status.
            return Page();
        }
    }
}
