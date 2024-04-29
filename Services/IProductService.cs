using WebRazorAppProducts.DTO;
using WebRazorAppProducts.Models;

namespace WebRazorAppProducts.Services
{
    public interface IProductService
    {
        Product? InsertProduct(ProductInsertDTO dto);
        Product? UpdateProduct(ProductUpdateDTO dto);
        Product? GetProduct(int id);
        Product? DeleteProduct(int id);
        IList<Product>? GetAllProducts();


    }
}
