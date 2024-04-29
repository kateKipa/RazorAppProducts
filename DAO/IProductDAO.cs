using WebRazorAppProducts.Models;

namespace WebRazorAppProducts.DAO
{
    public interface IProductDAO
    {
        Product? Insert(Product product);
        Product? Update(Product product);
        void Delete(int id);
        Product? GetById(int id);
        IList<Product>? GetAll();
        
    }
}
