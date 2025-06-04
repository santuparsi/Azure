using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HandsOnEFCodeFirst.Repositories;
using HandsOnEFCodeFirst.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using HandsOnEFCodeFirst.Models;
using Azure.Storage.Blobs;

namespace HandsOnEFCodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly string _storageConnectionString;
        private readonly string _storageContainerName;
        public MovieController(IConfiguration configuration)
        {
            _movieRepository = new MovieRepository();
            _storageConnectionString = configuration.GetValue<string>("BlobConnectionString");
            _storageContainerName = configuration.GetValue<string>("BlobContainerName");
        }
        [HttpGet, Route("GetAllMovies")]
        public IActionResult GetAll()
        {
            try
            {

                return StatusCode(200, _movieRepository.GetAll()); //Movie list is sending as josn form
            }
            catch (System.Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet, Route("GetMovieById/{id}")]
        public IActionResult GetMovie(int id)
        {
            try
            {
                Movie movie = _movieRepository.Get(id);
                if (movie != null)
                {
                    return StatusCode(200, movie);
                }
                else
                {
                    return StatusCode(404, "Invalid Id");
                }
            }
            catch (System.Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost, Route("AddMovie")]
        public IActionResult AddMovie(Movie movie)
        {
            try
            {
                
                _movieRepository.Add(movie);
                return StatusCode(200, "Movie Added");
            }
            catch (System.Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost, Route("UploadBanner")]
        public async Task<IActionResult> UploadBanner(int movieId, IFormFile file)
        {
            try
            {
                BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);
                BlobClient client = container.GetBlobClient(file.FileName);
                await using (Stream? data = file.OpenReadStream())
                {
                    await client.UploadAsync(data);
                }
                _movieRepository.UpdateBanner(movieId,client.Uri.AbsoluteUri);
                return StatusCode(200, client);
            }
            catch (System.Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut, Route("EditMovie")]
        public IActionResult EditMovie(MovieModel movie)
        {
            try
            {
                _movieRepository.Update(movie);
                return StatusCode(200, "Movie Edited");
            }
            catch (System.Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete, Route("DeleteMovie/{id}")]
        public IActionResult DeleteMovie(int id)
        {
            try
            {
                _movieRepository.Delete(id);
                return StatusCode(200, "Movie Deleted");
            }
            catch (System.Exception)
            {

                throw;
            }
        }

    }
}
