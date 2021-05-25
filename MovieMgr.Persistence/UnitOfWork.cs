using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MovieMgr.Core.Contracts;
using MovieMgr.Persistence;
using MovieMgr.Core.Entities;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using Utils;

namespace MovieMgr.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        const string FILENAME = "movies.csv";

        private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();
        private bool _disposed;


        public UnitOfWork()
        {
            _dbContext = new ApplicationDbContext();
            MovieRepository = new MovieRepository(_dbContext);
            CategoryRepository = new CategoryRepository(_dbContext);
        }

        public IMovieRepository MovieRepository { get; }

        public ICategoryRepository CategoryRepository { get; }


        /// <summary>
        ///     Repository-übergreifendes Speichern der Änderungen
        /// </summary>
        public void Save()
        {
            var entities = _dbContext.ChangeTracker.Entries()
               .Where(entity => entity.State == EntityState.Added
                                || entity.State == EntityState.Modified)
               .Select(e => e.Entity).ToList();
            foreach (var entity in entities)
            {
                ValidateEntity(entity);
            }
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Validierungen auf DbContext-Ebene
        /// </summary>
        /// <param name="entity"></param>
        private void ValidateEntity(object entity)
        {
            
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void DeleteDatabase()
        {
            _dbContext.Database.EnsureDeleted();
        }

        public void MigrateDatabase()
        {
            try
            {
                _dbContext.Database.Migrate();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void FillDb()
        {
            this.DeleteDatabase();
            this.MigrateDatabase();

            string[][] csvMovies = MyFile.ReadStringMatrixFromCsv(FILENAME,true);

            List<Category> categories = csvMovies.GroupBy(line => line[2]).Select(grp => new Category() { CategoryName = grp.Key }).ToList();
            List<Movie> movies = csvMovies.Select(line =>
              new Movie()
              {
                  Category = categories.Single(cat => cat.CategoryName == line[2]),
                  Duration = int.Parse(line[3]),
                  Title = line[0],
                  Year = int.Parse(line[1]),
              }).ToList();

            
            _dbContext.Categories.AddRange(categories);
            _dbContext.Movies.AddRange(movies);
            Save();
        }
    }

   
}
