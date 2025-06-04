using HandsOnCosmosDBNoSql.Models;
using HandsOnCosmosDBNoSql.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandsOnCosmosDBNoSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        public readonly ICarCosmosService _carCosmosService;
        public CarController(ICarCosmosService carCosmosService)
        {
            _carCosmosService = carCosmosService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sqlCosmosQuery = "Select * from c";
            var result = await _carCosmosService.Get(sqlCosmosQuery);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Post(Car newCar)
        {
            newCar.Id = Guid.NewGuid().ToString();
            var result = await _carCosmosService.Add(newCar);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Put(Car carToUpdate)
        {
            var result = await _carCosmosService.Update(carToUpdate);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id, string make)
        {
            await _carCosmosService.Delete(id, make);
            return Ok();
        }
    }
}
