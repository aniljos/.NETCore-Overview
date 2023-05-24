using SampleApp.Model;

namespace SampleApp.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> FetchAll();
        Task<Product> FetchById(int id);
        Task<bool> Save(Product product);
        Task<bool> Update(Product product);
        Task<bool> UpdatePrice(int productId, double price);
        Task<bool> Delete(int id);

    }
}
