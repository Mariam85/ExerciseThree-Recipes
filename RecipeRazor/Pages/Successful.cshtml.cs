using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RecipeRazor.Pages
{
    public class SuccessfulModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public RecipesModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        public void OnGet()
        {
        }
        public IActionResult OnPost(string submit)
        {
            switch (submit)
            {
                case "Main menu":
                    // Redirect to main menu page.
                    break;
                case "Recipes":
                    // Redirect to recipes page.
                    break;
                case "Categories":
                    // Redirect to categories page.
                    break;
                default:
                    throw new Exception();
            }

            // This line will be removed.
            return Redirect("Recipes");
        }
    }
}
