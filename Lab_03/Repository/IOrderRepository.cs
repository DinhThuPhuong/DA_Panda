using Lab_03.Models;


namespace Lab_03.Repository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(int id);
        Task UpdateAsync(Order order);
        Task DeleteAsync(int id);
    }
}
