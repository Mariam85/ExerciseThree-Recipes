using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeRazor.Models;

namespace RecipeRazor.Pages
{
    public class EditRecipesModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public EditRecipesModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        [BindProperty]
        public List<Recipe> recipesList { get; set; } = new List<Recipe>();

        public async Task OnGet()
        {
            var client = _httpClientFactory.CreateClient("Recipes");
            recipesList = await client.GetFromJsonAsync<List<Recipe>>("recipes");
        }
        public async Task OnPostUpdate(string info)
        {
            var client = _httpClientFactory.CreateClient("Recipes");
            recipesList = await client.GetFromJsonAsync<List<Recipe>>("recipes");
            // Get the required recipe.
        }
    }
}
