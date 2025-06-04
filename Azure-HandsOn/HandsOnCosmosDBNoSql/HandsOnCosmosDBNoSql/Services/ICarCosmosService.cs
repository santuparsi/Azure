using HandsOnCosmosDBNoSql.Models;

namespace HandsOnCosmosDBNoSql.Services
{
    public interface ICarCosmosService
    {
        Task<List<Car>> Get(string sqlCosmosQuery);
        Task<Car> Add(Car newCar);
        Task<Car> Update(Car carToUpdate);
        Task Delete(string id, string make);
    }
}
