using Domain.Entities;

namespace Domain.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<Product> GetProductById(int id);
    Task<bool> CheckExistanceByTitle(string title);
}