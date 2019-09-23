using Cinema.Persisted.Entities;
using System;
using System.Linq.Expressions;

namespace Cinema.BusinessLogic.Filtering
{
    public class FilmByName : Specification<Film>
    {
        private readonly string _name;

        public FilmByName(string name)
        {
            _name = name;
        }

        public override Expression<Func<Film, bool>> IsSatisfiedBy()
        {
            return c => c.Name == _name;
        }
    }
}
