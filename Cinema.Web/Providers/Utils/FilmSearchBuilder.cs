using Cinema.Persisted.Entities;
using Cinema.Web.Models;
using CinemaSearcher.Business.Searching;
using System;
using System.Linq.Expressions;

namespace Cinema.Web.Providers.Utils
{
    public class FilmFilterBuilder
    {
        public Expression<Func<Film, bool>> Filter { get; set; }
        public FilmSearchModel SearchModel { get; set; }

        public FilmFilterBuilder(FilmSearchModel searchModel)
        {
            SearchModel = searchModel;
            Filter = PredicateBuilder.True<Film>();
        }
        public Expression<Func<Film, bool>> Build()
        {
            AddNameFilter(SearchModel.Name)
                .AddFilmMakerNameFilter(SearchModel.FilmMaker)
                .AddFromDurationFilter(SearchModel.Duration.From)
                .AddToDurationFilter(SearchModel.Duration.To);
            return Filter;
        }

        private FilmFilterBuilder AddNameFilter(string namePart)
        {
            Filter = PredicateBuilder.And(Filter, film => film.Name.ToLower().StartsWith(namePart.ToLower()));
            return this;
        }

        private FilmFilterBuilder AddFilmMakerNameFilter(string filmMakerNamePart)
        {
            Filter = PredicateBuilder.And(Filter, film => film.Filmmaker.ToLower().StartsWith(filmMakerNamePart.ToLower()));
            return this;
        }

        private FilmFilterBuilder AddToDurationFilter(float? durationTo)
        {
            Filter = PredicateBuilder.And(Filter, film => (durationTo > 0) ? film.DurationTime < durationTo : true);
            return this;
        }

        private FilmFilterBuilder AddFromDurationFilter(float? durationFrom)
        {
            Filter = PredicateBuilder.And(Filter, film => (durationFrom > 0) ? film.DurationTime > durationFrom : true);
            return this;
        }
    }
}
