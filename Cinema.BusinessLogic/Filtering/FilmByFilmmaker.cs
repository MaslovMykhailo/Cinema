using Cinema.Persisted.Entities;
using System;
using System.Linq.Expressions;

namespace Cinema.BusinessLogic.Filtering
{
    public class FilmByFilmmaker : Specification<Film>
    {
        private readonly string _filmmaker;

        public FilmByFilmmaker(string filmmaker)
        {
            _filmmaker = filmmaker;
        }

        public override Expression<Func<Film, bool>> IsSatisfiedBy()
        {
            return c => c.Filmmaker == _filmmaker;
        }
    }
}
