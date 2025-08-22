using MovieRental.Data;

namespace MovieRental.Movie
{
	public class MovieFeatures : IMovieFeatures
	{
		private readonly MovieRentalDbContext _movieRentalDb;
		public MovieFeatures(MovieRentalDbContext movieRentalDb)
		{
			_movieRentalDb = movieRentalDb;
		}
		
		public Movie Save(Movie movie)
		{
			_movieRentalDb.Movies.Add(movie);
			_movieRentalDb.SaveChanges();
			return movie;
		}

		//Performance can be a concern, since there is no filter and the Movies table can have a lot of records
		//There is no pagination either so a huge volume of records can deteriorate user experience and increase memory usage
		public List<Movie> GetAll()
		{
			return _movieRentalDb.Movies.ToList();
		}


	}
}
