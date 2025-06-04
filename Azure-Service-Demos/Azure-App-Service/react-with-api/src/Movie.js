import { useState, useEffect } from "react";
import axios from "axios";
const Movie = () => {
  const [movie, setMovies] = useState([]);
  const [id, setId] = useState();
  const [title, setTitle] = useState();
  const [lang, setLang] = useState();
  const [year, setYear] = useState();
  const [actor, setactor] = useState();
  const [director, setDirector] = useState();

  useEffect(() => {
    axios
      .get("https://movieapi67.azurewebsites.net/api/Movie/GetAllMovies")
      .then((response) => {
        setMovies(response.data);
        console.log(response.data);
      })
      .catch((error) => {
        console.log(this.error);
      });
  }, []);
  const getMovieById = (event) => {
    axios
      .get("https://movieapi67.azurewebsites.net/api/Movie/GetMovieById/" + id)
      .then((response) => {
        //(response.data);
        console.log(response.data);
        setLang(response.data.language);
        setTitle(response.data.title);
        setDirector(response.data.director);
        setYear(response.data.releaseYear);
        setactor(response.data.actor);
      })
      .catch((error) => {
        console.log(this.error);
      });

    event.preventDefault();
  };
  const addMovie = (event) => {
    let movie = {
      title: title,
      language: lang,
      director: director,
      actor: actor,
      releaseYear: year,
      bannerUrl:'empty'
    };
    axios
      .post("https://movieapi67.azurewebsites.net/api/Movie/AddMovie/", movie)
      .then((response) => {
        console.log(response.data);
      })
      .catch((error) => {
        console.log(this.error);
      });
    event.preventDefault();
  };
  const deletMobieById = (event) => {
    axios
      .delete(
        "https://movieapi67.azurewebsites.net/api/Movie/DeleteMovie/" + id
      )
      .then((response) => {
        console.log(response.data);
      })
      .catch((error) => {
        console.log(this.error);
      });
    event.preventDefault();
  };
  const editMovie = (event) => {
    let movie = {
      movieId: id,
      title: title,
      language: lang,
      director: director,
      actor: actor,
      releaseYear: year,
    };
    console.log(movie);
    axios
      .put("https://movieapi67.azurewebsites.net/api/Movie/EditMovie/", movie)
      .then((response) => {
        console.log(response.data);
      })
      .catch((error) => {
        console.log(this.error);
      });
    event.preventDefault();
  };
  return (
    <div className="container">
      <form>
        <table border="1" style={{ width: "300px" }}>
          <tr>
            <td>Id</td>
            <td>
              <input
                type="text"
                value={id}
                onChange={(event) => setId(event.target.value)}
              ></input>
            </td>
          </tr>
          <tr>
            <td>Title</td>
            <td>
              <input
                type="text"
                value={title}
                onChange={(event) => setTitle(event.target.value)}
              ></input>
            </td>
          </tr>
          <tr>
            <td>Language</td>
            <td>
              <input
                type="text"
                value={lang}
                onChange={(event) => setLang(event.target.value)}
              ></input>
            </td>
          </tr>
          <tr>
            <td>Year</td>
            <td>
              <input
                type="text"
                value={year}
                onChange={(event) => setYear(event.target.value)}
              ></input>
            </td>
          </tr>
          <tr>
            <td>Actor</td>
            <td>
              <input
                type="text"
                value={actor}
                onChange={(event) => setactor(event.target.value)}
              ></input>
            </td>
          </tr>
          <tr>
            <td>Director</td>
            <td>
              <input
                type="text"
                value={director}
                onChange={(event) => setDirector(event.target.value)}
              ></input>
            </td>
          </tr>
          <tr>
            <td colSpan="2">
              <button onClick={getMovieById} style={{ margin: "10px" }}>
                Search
              </button>
              <button onClick={addMovie} style={{ margin: "10px" }}>
                Add
              </button>
              <button onClick={editMovie} style={{ margin: "10px" }}>
                Edit
              </button>
              <button onClick={deletMobieById} style={{ margin: "10px" }}>
                Delete
              </button>
            </td>
          </tr>
        </table>
      </form>
      <h1 className="text-center">All Movies</h1>
      <table className="table table-bordered">
        <tr>
          <th>MovieId</th>
          <th>Title</th>
          <th>Language</th>
          <th>RealseYear</th>
          <th>Actor</th>
          <th>Director</th>
          <th>Banner</th>
        </tr>
        {movie.map((m) => (
          <tr>
            <td>{m.movieId}</td>
            <td>{m.title}</td>
            <td>{m.language}</td>
            <td>{m.releaseYear}</td>
            <td>{m.actor}</td>
            <td>{m.director}</td>
            <td>
              <img width={50} height={50} src={m.bannerUrl} alt="" />
            </td>
          </tr>
        ))}
      </table>
    </div>
  );
};
export default Movie;
