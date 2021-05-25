using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieMgr.Core.Contracts;
using MovieMgr.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMgr.Web.ApiControllers
{
    [Route("api/[controller]",Name = "movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        public MoviesController(IUnitOfWork uow)
        {
            _uow = uow;
        }


        [HttpGet]
        public IActionResult Get()
        {
            List<Movie> returnList = _uow.MovieRepository.GetAllMovies();
            return Ok(returnList);
        }

        //[Route("getById")]//?param1=wert&param2=wert&...
        //[HttpGet]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Movie movie = _uow.MovieRepository.GetMovieById(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [Route("getByFilter")]
        [HttpGet]
        public IActionResult GetByFilter(int? categoryId, int? duration, int? year)
        {
            List<Movie> movies = _uow.MovieRepository.GetMoviesByCategoryId(categoryId);
            if (year != null)
            {
                movies.RemoveAll(m=>m.Year!=year);
            }
            if (duration != null)
            {
                movies.RemoveAll(m => m.Duration < duration);
            }

            return Ok(movies);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _uow.MovieRepository.InsertMovie(movie);
            _uow.Save();
            return new CreatedAtActionResult(nameof(GetById),nameof(MoviesController),new { id = movie.Id },movie);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] Movie movie)
        {
            if(!ModelState.IsValid || id != movie.Id)
            {
                return BadRequest();
            }
            _uow.MovieRepository.UpdateMovie(movie);
            try
            {
                _uow.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (_uow.MovieRepository.GetMovieById(id)==null)
                {
                    return NotFound();
                }
                else
                {
                    throw new DbUpdateException();
                }
            }
            
            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Movie movie = _uow.MovieRepository.GetMovieById(id);
            if (movie == null)
            {
                return NotFound();
            }
            _uow.MovieRepository.DeleteMovie(movie);
            _uow.Save();
            return Ok(movie);
        }
    }
}
