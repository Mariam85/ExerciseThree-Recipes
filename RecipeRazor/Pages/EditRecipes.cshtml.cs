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

        public async Task<IActionResult> OnGet()
        {
            var client = _httpClientFactory.CreateClient("Recipes");
            recipesList = await client.GetFromJsonAsync<List<Recipe>>("recipes");
            return Page();
        }

        public async Task<IActionResult> OnPostUpdate(Guid id, string choiceEdit, string afterEdit)
        {
            var client = _httpClientFactory.CreateClient("Recipes");
            var response = await client.PutAsync($"recipes/edit-recipe/{id}?attributeName={choiceEdit}&editedParameter={afterEdit}", null);
            return RedirectToPage("./EditRecipes");
        }
    }
}
