using System;
using System.Collections.Generic;
using System.Text;
using MovieMgr.Core.Entities;

namespace MovieMgr.Core.Contracts
{
    public interface IMovieRepository
    {
        int GetCount();
        List<Movie> GetAllMovies();
        Movie GetMovieById(int id);
        void InsertMovie(Movie movie);
        void UpdateMovie(Movie movie);
        void DeleteMovie(Movie movie);
        List<Movie> GetMoviesByCategoryId(int? categoryId);
    }
}
