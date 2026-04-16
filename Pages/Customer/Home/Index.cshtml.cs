using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using SportsStore_Spr2026.Data;
using SportsStore_Spr2026.Models;

namespace SportsStore_Spr2026.Pages.Customer.Home
{
    public class IndexModel : PageModel // where is the pagemodel that index model inher
    {
        private readonly IProductRepository productRepository;

        public IndexModel(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }


        


        public List<Product> ProductList { get; set; } // this is the property that will hold the list of products that we will get from the database and pass it to the view.
        
        public void OnGet()
        {
            //what is the true difference between an api controller and a razor page, the razor page is a page that will be rendered on the server and sent to the client, while the api controller is a controller that will handle the http requests and return data in json format to the client.
            //the on get method is the method that will be called when the page is loaded, we can use this method to get the data from the database and pass it to the view.
            //so it handles the http get request and we can use it to get the data from the database and pass it to the view.

            //create a product list

            ProductList = productRepository.GetProductList();
            //to get product repository we need to do dependency injection, we need to inject the product repository into the constructor of the index model and then we can use it to get the data from the database and pass it to the view.
            //which you do by 

            //pure C# code goes here, we can talk to the database and get the data we need and pass it to the view.
        }
    }
}
