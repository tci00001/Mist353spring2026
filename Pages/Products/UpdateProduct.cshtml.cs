using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore_Spr2026.Data;
using SportsStore_Spr2026.Models;

namespace SportsStore_Spr2026.Pages.Products
{
    public class UpdateProductModel : PageModel
    {
        [BindProperty]
        public Product product { get; set; }

        private readonly IProductRepository _productRepository; //blank

        public UpdateProductModel(IProductRepository productRepository)//dependency injection
        {
            _productRepository = productRepository; //saves the reference to the productrepository
            // object in the field so that we can access it throughout the class
            
        }

        



        public void OnGet(int id)//when the user clicks the update product information button, the id is passed in as Querystring
        {
           product = _productRepository.GetProductById(id);// saves the fetched product in the product variable
            // which is then displayed by the view

        }

       public IActionResult OnPost()
        {
            if(ModelState.IsValid)//will be true if all fields are proveided
            {
               bool isUpdated = _productRepository.UpdateProduct(product);
                if(isUpdated)
                {
                    return RedirectToPage("/Products/Index");// if the product is succesfully updated, we return to the index page
                }
                else
                {
                    ModelState.AddModelError("", "Failed to update Product");
                }
            }
            return Page();

        }

        //in this case the Update product uses an Onget as well as an Onpost because we need to get the product that we want to update and then we need to post the updated product back to the database so that we can update the product in the database.

    }
}
