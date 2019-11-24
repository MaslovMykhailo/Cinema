namespace Cinema.Web.Models
{
    public class FilmSearchModel
    {
        private const int DEFAULT_FILMS_COUNT = 5000;
        public string Name { get; set; }
        public DurationFilter Duration { get; set; }
        public string FilmMaker { get; set; }
        public int Count { get; set; }

        public FilmSearchModel()
        {
            Name = string.Empty;
            Duration = new DurationFilter();
            FilmMaker = string.Empty;
            Count = DEFAULT_FILMS_COUNT;
        }

        public static FilmSearchModel Ensure(FilmSearchModel model)
        {
            var ensuredModel = new FilmSearchModel();
            ensuredModel.Duration = model.Duration;
            if(model.Count > 0)
            {
                ensuredModel.Count = model.Count;
            }

            if (model.Name != null)
            {
                ensuredModel.Name = model.Name;
            }
            if (model.FilmMaker != null)
            {
                ensuredModel.FilmMaker = model.FilmMaker;
            }

            return ensuredModel;
        }

        public class DurationFilter
        {
            public float? From { get; set; }
            public float? To { get; set; }

            public DurationFilter()
            {
                From = 0;
                To = 0;
            }
        }

    }
}
