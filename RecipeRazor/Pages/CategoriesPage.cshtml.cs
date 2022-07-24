using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeRazor.Models;

namespace RecipeRazor.Pages
{
    public class CategoriesPageModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CategoriesPageModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        [BindProperty]
        public List<string> categories { get; set; } = new List<string>();
        public Recipe recipe { get; set; }
        public IEnumerable<Recipe> foundRecipe { get; set; } = new List<Recipe>();
        public string numberOfResults { get; set; } = "";

        public async Task<IActionResult> OnPostUpdate(string newCategory,string oldCategory)
        {
            return Page();
   
        }

        public async Task<IActionResult> OnPostDelete()
        {
            return Page();

        }
        public async Task OnGet()
        {
            var client = _httpClientFactory.CreateClient("Recipes");
            categories = await client.GetFromJsonAsync<List<string>>("categories");
        }
    }
}
