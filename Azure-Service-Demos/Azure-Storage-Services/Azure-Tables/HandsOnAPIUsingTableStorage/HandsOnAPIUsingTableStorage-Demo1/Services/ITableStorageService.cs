using HandsOnAPIUsingTableStorage_Demo1.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HandsOnAPIUsingTableStorage_Demo1.Services
{
    public interface ITableStorageService
    {
        //Task<List<GroceryItemEntity>> GetEntitiesAsync();
        Task<GroceryItemEntity> GetEntityAsync(string category, string id);
        Task<GroceryItemEntity> UpsertEntityAsync(GroceryItemEntity entity);
        Task DeleteEntityAsync(string category, string id);
    }
}
