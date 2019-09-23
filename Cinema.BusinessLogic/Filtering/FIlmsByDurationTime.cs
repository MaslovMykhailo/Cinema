using Cinema.Persisted.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Cinema.BusinessLogic.Filtering
{
    public class FilmsByDurationTime : Specification<Film>
    {
        private readonly float _duration;

        public FilmsByDurationTime(float duration)
        {
            _duration = duration;
        }

        public override Expression<Func<Film, bool>> IsSatisfiedBy()
        {
            return c => c.DurationTime == _duration;
        }
    }
}
