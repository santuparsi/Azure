namespace HandsOnEFCodeFirst.Models
{
    public class MovieModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public int ReleaseYear { get; set; }
        public string Actor { get; set; }
        public string Director { get; set; }
        public IFormFile File { get; set; }
    }
}
