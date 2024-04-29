using AutoMapper;
using System.Transactions;
using WebRazorAppProducts.DAO;
using WebRazorAppProducts.DTO;
using WebRazorAppProducts.Models;

namespace WebRazorAppProducts.Services
{
    public class ProductServiceImpl : IProductService
    {
      
        private readonly IProductDAO _productDAO;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductServiceImpl> _logger;

        public ProductServiceImpl(IProductDAO productDAO, IMapper mapper, ILogger<ProductServiceImpl> logger)
        {
            _productDAO = productDAO;
            _mapper = mapper;
            _logger = logger;
        }

        public Product? DeleteProduct(int id)
        {
            Product? productToReturn = null;

            try
            {
                using TransactionScope scope = new();
                productToReturn = _productDAO.GetById(id);
                if (productToReturn == null) return null;

                _productDAO.Delete(id);

                scope.Complete();

                _logger!.LogInformation("Delete Success");
                return productToReturn;

            } catch (Exception ex)
            {
                _logger!.LogError("An error occured while deleting product: " + ex.Message);
                throw;
            }
        }

        public IList<Product>? GetAllProducts()
        {
            try
            {
                IList<Product>? products = _productDAO.GetAll();
                return products;

            }catch (Exception ex)
            {
                _logger!.LogError("An error occured while fetching products: " + ex.Message);
                throw;
            }
        }

        public Product? GetProduct(int id)
        {
            try
            {
                Product productToReturn = _productDAO.GetById(id);
                return productToReturn;
            } catch (Exception ex)
            {
                _logger!.LogError("An error occured while fetcing the product: " + ex.Message);
                throw;
            }
        }

        public Product? InsertProduct(ProductInsertDTO dto)
        {
            if (dto is null) return null;

            try
            {
                var product = _mapper.Map<Product>(dto);

                using TransactionScope scope = new();

                Product? insertedProduct = _productDAO!.Insert(product);

                scope.Complete();
                _logger!.LogInformation("Success in insert");
                return insertedProduct;
            } catch (Exception ex)
            {
                _logger!.LogError("An error occured while inserting a product: " + ex.Message);
                throw;
            }
        }

        public Product? UpdateProduct(ProductUpdateDTO dto)
        {
            if (dto is null) return null;
            Product? updatedProduct = null;

            try
            {
                var product = _mapper.Map<Product>(dto);

                using TransactionScope scope = new();

                updatedProduct = _productDAO.Update(product);

                scope.Complete();
                _logger!.LogInformation("Success in update");
                return updatedProduct;
            } catch (Exception e)
            {
                _logger!.LogError("An error occured while updating product: " + e.Message);
                throw;
            }
        }
    }
}
