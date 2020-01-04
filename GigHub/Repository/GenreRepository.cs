using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Repository
{
    public class GenreRepository
    {
        ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Genre> GetAllGenre()
        {
            return _context.Genres.ToList();
        }
    }
}