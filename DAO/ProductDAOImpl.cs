using System.Data.SqlClient;
using WebRazorAppProducts.Models;
using WebRazorAppProducts.Services;

namespace WebRazorAppProducts.DAO
{
    public class ProductDAOImpl : IProductDAO
    {
        public void Delete(int id)
        {
            string sql = "DELETE FROM PRODUCTS WHERE ID = @id";

            using SqlConnection? conn = DBHelper.GetConnection();
            if (conn is not null) conn.Open();
            using SqlCommand command = new(sql, conn);

            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }

        public IList<Product> GetAll()
        {
            string sql = "SELECT * FROM PRODUCTS";
   
            var products = new List<Product>();

            using SqlConnection? conn = DBHelper.GetConnection();
            if (conn is not null) conn.Open();
            using SqlCommand command = new(sql, conn);
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var product = new Product
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ID")),
                    Name = reader.GetString(reader.GetOrdinal("NAME")),
                    Description = reader.GetString(reader.GetOrdinal("DESCRIPTION")),
                    Price = reader.GetDecimal(reader.GetOrdinal("PRICE"))
                };
                products.Add(product);
            }
            return products;
        }

        public Product? GetById(int id)
        {
            string sql = "SELECT * FROM PRODUCTS WHERE ID = @id";
            Product? product = null;

            using SqlConnection? conn = DBHelper.GetConnection();
            if (conn is not null) conn.Open();
            using SqlCommand command = new(sql, conn);
            command.Parameters.AddWithValue("@id", id);

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                product = new()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ID")),
                    Name = reader.GetString(reader.GetOrdinal("NAME")),
                    Description = reader.GetString(reader.GetOrdinal("DESCRIPTION")),
                    Price = reader.GetDecimal(reader.GetOrdinal("PRICE"))
                };
            }
            return product;
        }

        public Product? Insert(Product? product)
        {

            if (product == null) return null;

            string sql = "INSERT INTO PRODUCTS (NAME, DESCRIPTION, PRICE) VALUES " +
                "(@name, @description, @price); SELECT SCOPE_IDENTITY();";

            int insertedId = 0;
            Product? insertedProduct = null;

            using SqlConnection? conn = DBHelper.GetConnection();
            if (conn is not null) conn.Open();
            using SqlCommand command = new(sql, conn);

            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@description", product.Description);
            command.Parameters.AddWithValue("@price", product.Price);

            object insertedObj = command.ExecuteScalar();

            if (insertedObj is not null)
            {
                if (!int.TryParse(insertedObj.ToString(), out insertedId)) 
                {
                    throw new Exception("Error in Id of the inserted Product");
                }
            }

            string sqlSelected = "SELECT * FROM PRODUCTS WHERE ID = @id";

            using SqlCommand sqlCommand = new(sqlSelected, conn);
            sqlCommand.Parameters.AddWithValue("@id", insertedId);

            using SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.Read())
            {
                insertedProduct = new()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ID")),
                    Name = reader.GetString(reader.GetOrdinal("NAME")),
                    Description = reader.GetString(reader.GetOrdinal("DESCRIPTION")),
                    Price = reader.GetDecimal(reader.GetOrdinal("PRICE"))
                };
            }
            return insertedProduct;
        }

        public Product? Update(Product product)
        {
            if (product == null) return null;

            string? sql = "UPDATE PRODUCTS SET NAME = @name, DESCRIPTION = @description, PRICE = @price WHERE ID = @id";

            using SqlConnection? conn = DBHelper.GetConnection();
            if (conn is not null) conn.Open();
            using SqlCommand command = new(sql, conn);

            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@description", product.Description);
            command.Parameters.AddWithValue("@price", product.Price);
            command.Parameters.AddWithValue("@id", product.Id);

            command.ExecuteNonQuery();

            return product;

        }
    }
}
