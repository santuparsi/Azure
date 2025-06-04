using HandsOnEFCodeFirst.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandsOnEFCodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly UworldDBContext uworldDBContext;

        public ProductController(UworldDBContext uworldDBContext)
        {
            this.uworldDBContext = uworldDBContext;
        }
        [HttpPost,Route("AddProduct")]
        public IActionResult Add(Product product)
        {
            try
            {
                uworldDBContext.Products.Add(product);
                uworldDBContext.SaveChanges();
                return StatusCode(200, product);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,Route("GetAllProducts")]
        public IActionResult GetAll()
        {
            try
            {
              
                return StatusCode(200, uworldDBContext.Products.ToList()); //return all products
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,Route("GetProductById/{name}")]
        public IActionResult Get(string name)
        {
            try
            {
                // Product product = uworldDBContext.Products.Find(1); //search by using primary key column value
                Product product = uworldDBContext.Products.SingleOrDefault(p => p.Name == name);
                if (product != null)
                    return StatusCode(200, product); //return all products
                else
                    return StatusCode(401, "Invalid Product Name");
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPut, Route("EditProduct")]
        public IActionResult Edit(Product product)
        {
            try
            {
                uworldDBContext.Products.Update(product);
                uworldDBContext.SaveChanges();
                return StatusCode(200, product);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete,Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Product product = uworldDBContext.Products.Find(id);
                uworldDBContext.Products.Remove(product);
                uworldDBContext.SaveChanges();
                return StatusCode(200, new JsonResult("Product Deleted"));
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
