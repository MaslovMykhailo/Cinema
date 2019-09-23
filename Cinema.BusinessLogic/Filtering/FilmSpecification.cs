using Cinema.Persisted.Entities;

namespace Cinema.BusinessLogic.Filtering
{
    public class FilmSpecification
    {
        public static Specification<Film> Duration(float duration)
        {
            return new FilmsByDurationTime(duration);
        }
    }
}
