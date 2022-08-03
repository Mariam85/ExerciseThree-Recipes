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
        public List<string> isError { get; set; } = new();
        public async Task OnGet(List<string> messages)
        {
            var client = _httpClientFactory.CreateClient("Recipes");
            recipesList = await client.GetFromJsonAsync<List<Recipe>>("recipes");
            isError = messages;
        }

        public async Task<IActionResult> OnPostUpdate(Guid id, string choiceEdit, string afterEdit)
        {
            var client = _httpClientFactory.CreateClient("Recipes");
            if (choiceEdit != null && afterEdit != null)
            {
                string temp = choiceEdit.Trim();
                string tmp2 = afterEdit.Trim();
                if (temp.Length != 0 && tmp2.Length != 0)
                {
                    var response = await client.PutAsync($"recipes/edit-recipe/{id}?attributeName={choiceEdit}&editedParameter={afterEdit}", null);
                    if (response.IsSuccessStatusCode)
                    {
                        isError = new List<string> { };
                        isError.Add("a");
                        isError.Add("b");
                    }
                }
                else
                {
                    isError = new List<string> { };
                    isError.Add("1");
                    isError.Add("2");
                }
            }
            else
            {
                isError = new List<string> { };
                isError.Add("1");
                isError.Add("2");
            }
            return RedirectToPage("./EditRecipes", new { messages = isError });
        }

    }
}
