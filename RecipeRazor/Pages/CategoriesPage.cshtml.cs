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
        public List<string> categories { get; set; } = new();

        public async Task<IActionResult> OnPostDelete(string deletedValue)
        {
            var client = _httpClientFactory.CreateClient("Recipes");
            categories = await client.GetFromJsonAsync<List<string>>("categories");
            var response = await client.DeleteAsync($"recipes/remove-category/{categories[Int32.Parse(deletedValue)]}");
            return RedirectToPage("./CategoriesPage");
        }
        public async Task<IActionResult> OnGet()
        {
            var client = _httpClientFactory.CreateClient("Recipes");
            categories = await client.GetFromJsonAsync<List<string>>("categories");
            return Page();
        }

        public async Task<IActionResult> OnPostUpdate(string newCategory, string oldValue)
        {
            var client = _httpClientFactory.CreateClient("Recipes");
            categories = await client.GetFromJsonAsync<List<string>>("categories");
            var response = await client.PutAsync($"recipes/rename-category?oldName={categories[Int32.Parse(oldValue)]}&newName={newCategory}", null);
            return RedirectToPage("./CategoriesPage");
        }
    }
}
