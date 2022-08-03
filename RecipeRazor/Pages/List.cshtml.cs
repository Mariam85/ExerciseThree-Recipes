using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeRazor.Models;

namespace RecipeRazor.Pages
{
    public class ListModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ListModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        [BindProperty]
        public Recipe recipe { get; set; }
        public IEnumerable<Recipe> foundRecipe { get; set; } = new List<Recipe>();
        public string numberOfResults { get; set; } = "";

        public async Task<IActionResult> OnPost()
        {
            var client = _httpClientFactory.CreateClient("Recipes");
            try
            {
                await client.GetFromJsonAsync<List<Recipe>>($"recipes/list-recipe/{recipe.Title}");
            }
            catch
            {
                numberOfResults = "no results";
                return Page();
            }
            foundRecipe = await client.GetFromJsonAsync<List<Recipe>>($"recipes/list-recipe/{recipe.Title}");
            int num = foundRecipe.Count();
            numberOfResults = num.ToString();
            return Page();
        }
    }
}
