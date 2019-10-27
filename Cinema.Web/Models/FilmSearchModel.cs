namespace Cinema.Web.Models
{
    public class FilmSearchModel
    {
        public string Name { get; set; }
        public DurationFilter Duration { get; set; }
        public string FilmMaker { get; set; }

        public FilmSearchModel()
        {
            Name = "";
            Duration = new DurationFilter();
            FilmMaker = "";
        }

        public class DurationFilter
        {
            public float From { get; set; }
            public float To { get; set; }

            public DurationFilter()
            {
                From = 0;
                To = 0;
            }
        }
    }
}
