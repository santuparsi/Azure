using HandsOnEFCodeFirst.Entities;
using HandsOnEFCodeFirst.Models;
using System.Collections.Generic;
using System.Linq;

namespace HandsOnEFCodeFirst.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly UworldDBContext db;
        public MovieRepository()
        {
            db = new UworldDBContext();
        }
        public void Add(Movie movie)
        {
           db.Movies.Add(movie);
            db.SaveChanges();
        }

      

        public void Delete(int id)
        {
           //Movie movie=db.Movies.Find(id);
           Movie movie=db.Movies.SingleOrDefault(m=>m.MovieId==id);
            db.Movies.Remove(movie);
            db.SaveChanges();
        }

        public Movie Get(int id)
        {
            return db.Movies.Find(id);
        }

        public List<Movie> GetAll()
        {
            return db.Movies.ToList();
        }

        public void Update(MovieModel movieModel)
        {
            Movie movie = db.Movies.Find(movieModel.MovieId);
            movie.Actor = movieModel.Actor;
            movie.Language = movieModel.Language;
            movie.ReleaseYear = movieModel.ReleaseYear;
            //db.Movies.Update(movie);
            db.SaveChanges();
        }
        public void UpdateBanner(int id, string banner)
        {
            Movie movie = db.Movies.Find(id);
            movie.BannerUrl=banner;
            db.SaveChanges();

        }


    }
}
