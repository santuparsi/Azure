using HandsOnEFCodeFirst.Entities;
using HandsOnEFCodeFirst.Models;
using System.Collections.Generic;
namespace HandsOnEFCodeFirst.Repositories
{
    public interface IMovieRepository
    {
        List<Movie> GetAll();
        Movie Get(int id);
        void Update(MovieModel movieModel);
        void Delete(int id);
        void Add(Movie movie);
    }
}
