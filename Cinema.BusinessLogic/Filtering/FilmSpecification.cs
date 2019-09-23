using Cinema.Persisted.Entities;

namespace Cinema.BusinessLogic.Filtering
{
    public class FilmSpecification
    {
        public static Specification<Film> Duration(float duration)
        {
            return new FilmsByDurationTime(duration);
        }

        public static Specification<Film> Name(string name)
        {
            return new FilmByName(name);
        }
    }
}
